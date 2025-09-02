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
using System.Xml.Linq;
using ImageFileOptimizationContext = DotNetCampus.MediaConverters.Imaging.Optimizations.EnhancedGraphicsMetafileOptimizationContext;
using ImageFileOptimizationResult = DotNetCampus.MediaConverters.Imaging.Optimizations.EnhancedGraphicsMetafileOptimizationResult;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

/// <summary>
/// 图片文件优化上下文信息
/// </summary>
public readonly record struct EnhancedGraphicsMetafileOptimizationContext()
{
    public string TraceId { get; init; } = Guid.NewGuid().ToString("N");

    public required FileInfo ImageFile { get; init; }
    public required DirectoryInfo WorkingFolder { get; init; }
    public required int? MaxImageWidth { get; init; }
    public required int? MaxImageHeight { get; init; } = null;

    public bool ShouldLogToConsole { get; init; } = false;

    public bool ShouldLogToFile { get; init; } = false;

    public string LogFileName { get; init; } = "Log.txt";

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
/// 图片文件优化结果
/// </summary>
public readonly record struct EnhancedGraphicsMetafileOptimizationResult()
{
    [MemberNotNullWhen(returnValue: true)]
    public bool IsSuccess => OptimizedImageFile is not null;

    /// <summary>
    /// 优化后的图片文件
    /// </summary>
    public FileInfo? OptimizedImageFile { get; init; }

    public bool IsNotSupport { get; init; }

    public Exception? Exception { get; init; }

    public static ImageFileOptimizationResult NotSupported()
    {
        return new EnhancedGraphicsMetafileOptimizationResult()
        {
            IsNotSupport = true,
        };
    }

    public static ImageFileOptimizationResult FailException(Exception exception)
    {
        return new EnhancedGraphicsMetafileOptimizationResult()
        {
            Exception = exception,
        };
    }
}

/// <summary>
/// 增强图元优化方法，用于优化 emf 和 wmf 图片
/// </summary>
public static class EnhancedGraphicsMetafileOptimization
{
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
    /// 使用自己写的基于 Oxage.Wmf 的 SkiaWmfRenderer 进行转换，可以比较好处理公式内容
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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
    /// 转换 svg 文件为 png 文件
    /// </summary>
    /// <returns></returns>
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