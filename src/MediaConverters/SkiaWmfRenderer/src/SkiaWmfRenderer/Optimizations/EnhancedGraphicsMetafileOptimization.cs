using DotNetCampus.MediaConverter.SkiaWmfRenderer;

using SkiaSharp;

using Svg.Skia;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Xml.Linq;
using ImageFileOptimizationContext = DotNetCampus.MediaConverters.Imaging.Optimizations.EnhancedGraphicsMetafileOptimizationContext;
using ImageFileOptimizationResult = DotNetCampus.MediaConverters.Imaging.Optimizations.EnhancedGraphicsMetafileOptimizationResult;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

/// <summary>
/// 包含用于增强图元（EMF/WMF）优化操作的上下文信息。
/// </summary>
public readonly record struct EnhancedGraphicsMetafileOptimizationContext()
{
    /// <summary>
    /// 跟踪标识符，用于日志定位。
    /// </summary>
    public string TraceId { get; init; } = Guid.NewGuid().ToString("N");

    /// <summary>
    /// 要优化的图像文件信息。
    /// </summary>
    public required FileInfo ImageFile { get; init; }

    /// <summary>
    /// 工作目录，临时输出文件将写入此目录。
    /// </summary>
    public required DirectoryInfo WorkingFolder { get; init; }

    /// <summary>
    /// 请求的最大图像宽度（像素）。null 表示不限制。
    /// </summary>
    public required int? MaxImageWidth { get; init; }

    /// <summary>
    /// 请求的最大图像高度（像素）。null 表示不限制。
    /// </summary>
    public required int? MaxImageHeight { get; init; } = null;

    /// <summary>
    /// 是否将日志输出到控制台。
    /// </summary>
    public bool ShouldLogToConsole { get; init; } = false;

    /// <summary>
    /// 是否将日志写入文件。
    /// </summary>
    public bool ShouldLogToFile { get; init; } = false;

    /// <summary>
    /// 日志文件名（相对于 <see cref="WorkingFolder"/>）。
    /// </summary>
    public string LogFileName { get; init; } = "Log.txt";

    /// <summary>
    /// 将一条日志消息输出到控制台或工作目录中的日志文件（取决于配置）。
    /// </summary>
    /// <param name="message">要记录的消息内容。</param>
    public void LogMessage(string message)
    {
        if (!ShouldLogToConsole && !ShouldLogToFile)
        {
            return;
        }

        if (ShouldLogToConsole)
        {
            Console.WriteLine(message);
        }

        if (ShouldLogToFile)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}][{TraceId}] {message}";

            var logFile = Path.Join(WorkingFolder.FullName, LogFileName ?? "Log.txt");

            File.AppendAllLines(logFile, [logMessage]);
        }
    }
}

/// <summary>
/// 表示增强图元优化操作的结果。
/// </summary>
public readonly record struct EnhancedGraphicsMetafileOptimizationResult()
{
    /// <summary>
    /// 如果优化过程成功并产生了输出文件，则为 true。
    /// </summary>
    [MemberNotNullWhen(returnValue: true)]
    public bool IsSuccess => OptimizedImageFile is not null;

    /// <summary>
    /// 优化后生成的图像文件（如果成功）。
    /// </summary>
    public FileInfo? OptimizedImageFile { get; init; }

    /// <summary>
    /// 表示该操作不被支持（例如平台或格式不支持）。
    /// </summary>
    public bool IsNotSupport { get; init; }

    /// <summary>
    /// 如果发生异常则包含异常对象，否则为 null。
    /// </summary>
    public Exception? Exception { get; init; }

    /// <summary>
    /// 创建一个表示不支持的结果实例。
    /// </summary>
    /// <returns>标识操作不被支持的结果。</returns>
    public static ImageFileOptimizationResult NotSupported()
    {
        return new EnhancedGraphicsMetafileOptimizationResult()
        {
            IsNotSupport = true,
        };
    }

    /// <summary>
    /// 创建一个包含异常信息的失败结果实例。
    /// </summary>
    /// <param name="exception">导致失败的异常。</param>
    /// <returns>包含异常的失败结果。</returns>
    public static ImageFileOptimizationResult FailException(Exception exception)
    {
        return new EnhancedGraphicsMetafileOptimizationResult()
        {
            Exception = exception,
        };
    }
}

/// <summary>
/// 提供用于将增强型图元（WMF/EMF）转换为 PNG 或 SVG 的工具方法。
/// 此类会根据运行时平台选择合适的实现（Windows 使用 GDI+，Linux 使用 Inkscape/Skia/libwmf 等）。
/// </summary>
public static class EnhancedGraphicsMetafileOptimization
{
    /// <summary>
    /// 将 WMF 或 EMF 文件转换为 PNG 文件。根据当前运行平台选择合适的转换实现。
    /// </summary>
    /// <param name="context">包含要转换文件和工作目录等设置的上下文。</param>
    /// <returns>转换操作的结果，包含生成的文件或错误信息。</returns>
    public static ImageFileOptimizationResult ConvertWmfOrEmfToPngFile(ImageFileOptimizationContext context)
    {
        // 在 Windows 上，直接使用 GDI+ 将 WMF 或 EMF 文件转换为 PNG 文件
        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            return ConvertInWindows(context);
        }

        if (OperatingSystem.IsLinux())
        {
            // 在 Linux 上，先尝试使用 Inkscape 进行转换，如失败，再使用 libwmf 进行转换
            try
            {
                return ConvertInLinux(context);
            }
            catch (Exception e)
            {
                context.LogMessage($"Convert wmf or emf in linux Fail. Exception: {e}");

                return ImageFileOptimizationResult.FailException(e);
            }
        }

        return ImageFileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("linux")]
    private static ImageFileOptimizationResult ConvertInLinux(ImageFileOptimizationContext context)
    {
        var file = context.ImageFile;

        // 在 Linux 上，先尝试使用 Inkscape 进行转换，如失败，
        // 先尝试 Oxage.Wmf 的 SkiaWmfRenderer 进行转换，如果发现不能很好支持，
        // 再使用 libwmf 进行转换

        // 调用 Inkscape 进行转换
        ImageFileOptimizationResult result = ConvertWithInkscape(context);
        if (result.OptimizedImageFile is { } svgFile)
        {
            return ConvertSvgToPngFile(svgFile);
        }
        else
        {
            // 失败了，没关系，继续使用 libwmf 进行转换
        }

        // 继续执行 Oxage.Wmf 或 libwmf 的转换，此时不支持 emf 格式
        if (string.Equals(file.Extension, ".emf"))
        {
            context.LogMessage($"Convert emf to png is not supported with libwmf. File:'{context.ImageFile.FullName}'");

            return ImageFileOptimizationResult.NotSupported();
        }

        // 使用 SkiaWmfRenderer 进行转换
        result = ConvertWithSkiaWmfRenderer(context);
        if (result.IsSuccess)
        {
            return result;
        }
        else
        {
            // 失败了，继续使用 libwmf 进行转换
            // 使用 libwmf 进行转换时，对于文本绘制支持很弱。这就是为什么优先使用  SkiaWmfRenderer 的原因
        }

        // 使用 libwmf 进行转换

        result = ConvertWithLibWmf(context);
        if (result.OptimizedImageFile is { } svgLibWmfFile)
        {
            return ConvertSvgToPngFile(svgLibWmfFile);
        }
        else
        {
            return result;
        }

        ImageFileOptimizationResult ConvertSvgToPngFile(FileInfo svgImageFile)
        {
            try
            {
                var convertSvgToPngFile = SvgFileOptimization.ConvertSvgToPngFile(context with
                {
                    ImageFile = svgImageFile
                });
                if (convertSvgToPngFile is not null)
                {
                    return new ImageFileOptimizationResult()
                    {
                        OptimizedImageFile = convertSvgToPngFile
                    };
                }
                else
                {
                    return ImageFileOptimizationResult.NotSupported();
                }
            }
            catch (Exception e)
            {
                context.LogMessage($"Convert svg to png file failed. File: '{svgImageFile.FullName}' Exception: {e}");

                return ImageFileOptimizationResult.FailException(e);
            }
        }
    }

    /// <summary>
    /// 使用基于 Oxage.Wmf 的 SkiaWmfRenderer 将 WMF/EMF 转换为 PNG。该方法适用于需要更好文本/公式支持的场景。
    /// </summary>
    /// <param name="context">包含输入文件、输出目录和尺寸限制等信息的上下文。</param>
    /// <returns>如果转换成功则返回包含输出 PNG 文件的结果，否则返回不支持或失败的结果。</returns>
    private static ImageFileOptimizationResult ConvertWithSkiaWmfRenderer(ImageFileOptimizationContext context)
    {
        int requestWidth = context.MaxImageWidth ?? 0;
        int requestHeight = context.MaxImageHeight ?? 0;

        var outputPngFile = new FileInfo(Path.Join(context.WorkingFolder.FullName, $"WmfRender_{Path.GetRandomFileName()}.png"));

        var file = context.ImageFile;

        context.LogMessage($"Start convert wmf to png by SkiaWmfRenderer. File:'{file}'");

        var skiaWmfRenderConfiguration = new SkiaWmfRenderConfiguration()
        {
            RequestWidth = requestWidth,
            RequestHeight = requestHeight,
            FontFolder = new DirectoryInfo(Path.Join(AppContext.BaseDirectory, "Assets", "Fonts"))
        };
        if (SkiaWmfRenderHelper.TryConvertToPng(file, outputPngFile, skiaWmfRenderConfiguration) && File.Exists(outputPngFile.FullName))
        {
            context.LogMessage($"Success converted wmf to png by SkiaWmfRenderer. File:'{file}' Output:'{outputPngFile}'");

            return new ImageFileOptimizationResult()
            {
                OptimizedImageFile = outputPngFile
            };
        }

        context.LogMessage($"Fail convert wmf to png by SkiaWmfRenderer. File:'{file}'");
        return ImageFileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("linux")]
    private static ImageFileOptimizationResult ConvertWithLibWmf(ImageFileOptimizationContext context)
    {
        var file = context.ImageFile;
        var workingFolder = context.WorkingFolder;

        var svgFile = Path.Join(workingFolder.FullName, $"{Path.GetFileNameWithoutExtension(file.Name)}_{Path.GetRandomFileName()}.svg");

        var wmf2svgFolder = Path.Join(AppContext.BaseDirectory, "Assets", RuntimeInformation.RuntimeIdentifier);
        var wmf2svgFile = Path.Join(wmf2svgFolder, "wmf2svg");

        context.LogMessage($"Start convert wmf to svg by libwmf. File:'{file}' wmf2svg='{wmf2svgFile}'");

#if NET7_0_OR_GREATER
        try
        {
            File.SetUnixFileMode(wmf2svgFile, UnixFileMode.UserExecute);
        }
        catch (Exception e)
        {
            context.LogMessage($"File.SetUnixFileMode +x Fail. wmf2svgFile='{wmf2svgFile}'. Exception: {e}");
        }
#endif

        // ./wmf2svg -o 1.svg image.wmf
        var processStartInfo = new ProcessStartInfo(wmf2svgFile)
        {
            ArgumentList =
            {
                "-o",
                svgFile,
                file.FullName,
            },
            Environment =
            {
                {"LD_LIBRARY_PATH", wmf2svgFolder}
            }
        };

        var fontFolder = Path.Join(AppContext.BaseDirectory, "Assets", "gsfonts");
        if (Directory.Exists(fontFolder))
        {
            processStartInfo.ArgumentList.Add($"--wmf-fontdir={fontFolder}");
        }

        try
        {
            using var process = Process.Start(processStartInfo);
            process?.WaitForExit(5000);
            if (process?.ExitCode == 0 && File.Exists(svgFile))
            {
                // 转换成功，再次执行 SVG 转 PNG 的转换
                // 由于可能存在 SVG 文件中包含无效字符的问题，因此需要修复一下
                var convertedFile = SvgFileOptimization.FixSvgInvalidCharacter(context with
                {
                    ImageFile = new FileInfo(svgFile)
                });

                return new ImageFileOptimizationResult()
                {
                    OptimizedImageFile = convertedFile
                };
            }
            else
            {
                context.LogMessage($"Convert emf or wmf to svg by libwmf failed. File:'{file}' ExitCode:{process?.ExitCode}");
            }
        }
        catch (Exception e)
        {
            // 比如 wmf2svg: error while loading shared libraries: libwmf-0.2.so.7: cannot open shared object file: No such file or directory 等错误
            context.LogMessage($"Convert emf or wmf to svg by libwmf failed. File:'{file}' Exception: {e}");
            return ImageFileOptimizationResult.FailException(e);
        }

        return ImageFileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("linux")]
    private static ImageFileOptimizationResult ConvertWithInkscape(ImageFileOptimizationContext context)
    {
        var file = context.ImageFile;
        var workingFolder = context.WorkingFolder;

        var svgFile = Path.Join(workingFolder.FullName, $"{Path.GetFileNameWithoutExtension(file.Name)}_{Path.GetRandomFileName()}.svg");

        context.LogMessage($"Start convert emf or wmf to png by Inkscape. File:'{file}'");

        var processStartInfo = new ProcessStartInfo("inkscape")
        {
            ArgumentList =
            {
                "--export-plain-svg",
                $"--export-filename={svgFile}",
                file.FullName,
            }
        };
        try
        {
            using var process = Process.Start(processStartInfo);
            process?.WaitForExit(5000);
            if (process?.ExitCode == 0 && File.Exists(svgFile))
            {
                // 转换成功，再次执行 SVG 转 PNG 的转换
                return new ImageFileOptimizationResult()
                {
                    OptimizedImageFile = new FileInfo(svgFile)
                };
            }
            else
            {
                context.LogMessage($"Convert emf or wmf to svg by Inkscape failed. File:'{file}' ExitCode:{process?.ExitCode}");
            }
        }
        catch (Exception e)
        {
            // 失败了，继续调用 libwmf 进行转换
            const int NoSuchFileErrorCode = 2;
            if (e is Win32Exception win32Exception && win32Exception.NativeErrorCode == NoSuchFileErrorCode)
            {
                // 明确不存在，那就不记录错误信息了
                // 大概耗时 17 毫秒
                context.LogMessage($"Convert emf or wmf to svg by Inkscape failed. Because not found Inkscape application. Please make sure Inkscape be installed. We will continue use libwmf to convert the image.");
            }
            else
            {
                context.LogMessage($"Convert emf or wmf to svg by Inkscape failed. We will continue use libwmf to convert the image. File:'{file}' Exception: {e}");
            }

            return ImageFileOptimizationResult.FailException(e);
        }

        return ImageFileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("windows6.1")]
    private static ImageFileOptimizationResult ConvertInWindows(ImageFileOptimizationContext context)
    {
        var file = context.ImageFile;
        var workingFolder = context.WorkingFolder;

        context.LogMessage($"Start convert emf or wmf to png by GDI. File:'{file}'");

        try
        {
            using var emf = new Bitmap(file.FullName);
            var convertedFile = Path.Join(workingFolder.FullName, $"GDI_{Path.GetRandomFileName()}.png");

            // 将增强型图形元文件转成位图。
            emf.Save(convertedFile, ImageFormat.Png);

            return new ImageFileOptimizationResult()
            {
                OptimizedImageFile = new FileInfo(convertedFile),
            };
        }
        catch (Exception e)
        {
            context.LogMessage($"Fail convert emf or wmf to png by GDI. File:'{file}' Exception:{e}");

            return ImageFileOptimizationResult.FailException(e);
        }
    }
}

/// <summary>
/// 提供 SVG 文件相关的优化与修复方法，例如将 SVG 转换为 PNG，或修复 SVG 中的无效字符。
/// </summary>
public static class SvgFileOptimization
{
    /*
     if (IsExtension(".svg"))
       {
           // 如果是 svg 那就直接转换了，因为后续叠加特效等逻辑都不能支持 SVG 格式
           try
           {
               var outputFilePath = ConvertSvgToPngFile(context);
               if (outputFilePath is null)
               {
                   return new ImageFileOptimizationResult()
                   {
                       OptimizedImageFile = null,
                       FailureReason = ImageFileOptimizationFailureReason.NotSupported
                   };
               }
               else
               {
                   context.LogMessage($"Success ConvertSvgToPngFile. Update current image file to '{outputFilePath.FullName}'");
                   context = context with
                   {
                       ImageFile = outputFilePath
                   };
               }
           }
           catch (Exception e)
           {
               context.LogMessage($"Convert SVG to PNG failed: {e}");

               return ImageFileOptimizationResult.FailException(e);
           }
       }
       else if (IsExtension(".wmf") ||
                IsExtension(".emf"))
       {
           var result = EnhancedGraphicsMetafileOptimization.ConvertWmfOrEmfToPngFile(context);
           if (result.OptimizedImageFile is not null)
           {
               context.LogMessage($"Success ConvertWmfOrEmfToPngFile. Update current image file to '{result.OptimizedImageFile}'");
               context = context with
               {
                   ImageFile = result.OptimizedImageFile
               };
           }
           else
           {
               return result;
           }
       }
     */

    /// <summary>
    /// 将指定上下文中的 SVG 文件渲染并保存为 PNG 文件。
    /// </summary>
    /// <param name="context">包含 SVG 文件和工作目录等信息的上下文。</param>
    /// <returns>成功时返回生成的 PNG 文件信息；失败时返回 null。</returns>
    public static FileInfo? ConvertSvgToPngFile(ImageFileOptimizationContext context)
    {
        var imageFile = context.ImageFile;
        var workingFolder = context.WorkingFolder;

        using var skSvg = new SKSvg();
        using var skPicture = skSvg.Load(imageFile.FullName);
        var outputFile = Path.Join(workingFolder.FullName,
            $"SVG_{Path.GetRandomFileName()}.png");
        var canSave = skSvg.Save(outputFile, SKColors.Transparent);
        if (canSave && File.Exists(outputFile))
        {
            return new FileInfo(outputFile);
        }

        // 转换失败
        return null;
    }

    /// <summary>
    /// 异步修复 SVG 文件中可能包含的无效字符（例如替换或删除不可见占位符），并在需要时将修复后的文件写入工作目录。
    /// </summary>
    /// <param name="svgFile">要修复的 SVG 文件。</param>
    /// <param name="workingFolder">修复后文件写入的工作目录。</param>
    /// <returns>修复后文件的 <see cref="FileInfo"/>；如果未作修改则返回原始文件实例。</returns>
    public static async Task<FileInfo> FixSvgInvalidCharacterAsync(FileInfo svgFile,
        DirectoryInfo workingFolder)
    {
        using var fileStream = svgFile.OpenRead();
        using var streamReader = new StreamReader(fileStream);

        var xDocument = await XDocument.LoadAsync(streamReader, LoadOptions.SetLineInfo, CancellationToken.None);
        bool anyUpdate = false;

        foreach (var xElement in xDocument.Descendants("text"))
        {
            var value = xElement.Value;
            if (!string.IsNullOrEmpty(value) && value.Length > 0 && value[0] is var c && c == 0xFFFD)
            {
                // 0xFFFFD 是 utf8 特殊字符
                // 画出来就是�符号，不如删掉
                xElement.Value = string.Empty;

                anyUpdate = true;
            }
        }

        if (anyUpdate)
        {
            var convertedFile = Path.Join(workingFolder.FullName, $"FixSVG_{Path.GetRandomFileName()}.svg");
            using var stream = File.Create(convertedFile);
            await xDocument.SaveAsync(stream, SaveOptions.None, CancellationToken.None);
            return new FileInfo(convertedFile);
        }

        // 啥都不用改，返回原图
        return svgFile;
    }

    /// <summary>
    /// 同步修复 SVG 文件中可能包含的无效字符（例如替换或删除不可见占位符），并在需要时将修复后的文件写入工作目录。
    /// </summary>
    /// <param name="context">包含 SVG 文件和工作目录等信息的上下文。</param>
    /// <returns>修复后文件的 <see cref="FileInfo"/>；如果未作修改则返回原始文件实例。</returns>
    public static FileInfo FixSvgInvalidCharacter(ImageFileOptimizationContext context)
    {
        FileInfo svgFile = context.ImageFile;
        DirectoryInfo workingFolder = context.WorkingFolder;

        using var fileStream = svgFile.OpenRead();
        using var streamReader = new StreamReader(fileStream);

        var xDocument = XDocument.Load(streamReader, LoadOptions.SetLineInfo);
        bool anyUpdate = false;

        foreach (var xElement in xDocument.Descendants("text"))
        {
            var value = xElement.Value;
            if (!string.IsNullOrEmpty(value) && value.Length > 0 && value[0] is var c && c == 0xFFFD)
            {
                // 0xFFFFD 是 utf8 特殊字符
                // 画出来就是�符号，不如删掉
                xElement.Value = string.Empty;

                anyUpdate = true;
            }
        }

        if (anyUpdate)
        {
            var convertedFile = Path.Join(workingFolder.FullName, $"FixSVG_{Path.GetRandomFileName()}.svg");
            xDocument.Save(convertedFile);
            return new FileInfo(convertedFile);
        }

        // 啥都不用改，返回原图
        return svgFile;
    }
}