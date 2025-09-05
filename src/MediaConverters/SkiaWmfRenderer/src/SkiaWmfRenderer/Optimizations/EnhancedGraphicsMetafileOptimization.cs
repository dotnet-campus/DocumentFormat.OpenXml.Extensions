using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace DotNetCampus.MediaConverter.SkiaWmfRenderer.Optimizations;

/// <summary>
/// 提供用于将增强型图元（WMF/EMF）转换为 PNG 或 SVG 的工具方法。
/// 此类会根据运行时平台选择合适的实现（Windows 使用 GDI+，Linux 使用 Inkscape/Skia/libwmf 等）。
/// </summary>
public static class EnhancedGraphicsMetafileOptimization
{
    /// <summary>
    /// 尝试将矢量图文件（SVG/WMF/EMF）转换为 PNG 文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryOptimizeSvgOrWmf(EnhancedGraphicsMetafileOptimizationContext context,
        out EnhancedGraphicsMetafileOptimizationResult result)
    {
        if (IsExtension(".svg"))
        {
            // 如果是 svg 那就直接转换了，因为后续叠加特效等逻辑都不能支持 SVG 格式
            try
            {
                var outputFilePath = SvgFileOptimization.ConvertSvgToPngFile(in context);
                result = new EnhancedGraphicsMetafileOptimizationResult()
                {
                    OptimizedImageFile = outputFilePath,
                };
            }
            catch (Exception e)
            {
                context.LogMessage($"Convert SVG to PNG failed: {e}");

                result = EnhancedGraphicsMetafileOptimizationResult.FailException(e);
            }

            return true;
        }
        else if (IsExtension(".wmf") ||
                 IsExtension(".emf"))
        {
            result = ConvertWmfOrEmfToPngFile(in context);

            return true;
        }
        else
        {
            result = new EnhancedGraphicsMetafileOptimizationResult()
            {
                // 无需转换
                NotNeedOptimize = true,
            };
            return false;
        }

        bool IsExtension(string extension)
        {
            return string.Equals(context.ImageFile.Extension, extension, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// 将 WMF 或 EMF 文件转换为 PNG 文件。根据当前运行平台选择合适的转换实现。
    /// </summary>
    /// <param name="context">包含要转换文件和工作目录等设置的上下文。</param>
    /// <returns>转换操作的结果，包含生成的文件或错误信息。</returns>
    public static EnhancedGraphicsMetafileOptimizationResult ConvertWmfOrEmfToPngFile(in EnhancedGraphicsMetafileOptimizationContext context)
    {
        // 在 Windows 上，直接使用 GDI+ 将 WMF 或 EMF 文件转换为 PNG 文件
        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            return ConvertInWindows(in context);
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

                return EnhancedGraphicsMetafileOptimizationResult.FailException(e);
            }
        }

        return EnhancedGraphicsMetafileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("linux")]
    private static EnhancedGraphicsMetafileOptimizationResult ConvertInLinux(EnhancedGraphicsMetafileOptimizationContext context)
    {
        var file = context.ImageFile;

        // 在 Linux 上，先尝试使用 Inkscape 进行转换，如失败，
        // 先尝试 Oxage.Wmf 的 SkiaWmfRenderer 进行转换，如果发现不能很好支持，
        // 再使用 libwmf 进行转换

        // 调用 Inkscape 进行转换
        EnhancedGraphicsMetafileOptimizationResult result = ConvertWithInkscape(context);
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

            return EnhancedGraphicsMetafileOptimizationResult.NotSupported();
        }

        // 使用 SkiaWmfRenderer 进行转换
        result = ConvertWithSkiaWmfRenderer(in context);
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

        result = ConvertWithLibWmf(in context);
        if (result.OptimizedImageFile is { } svgLibWmfFile)
        {
            return ConvertSvgToPngFile(svgLibWmfFile);
        }
        else
        {
            return result;
        }

        EnhancedGraphicsMetafileOptimizationResult ConvertSvgToPngFile(FileInfo svgImageFile)
        {
            try
            {
                var convertSvgToPngFile = SvgFileOptimization.ConvertSvgToPngFile(context with
                {
                    ImageFile = svgImageFile
                });
                if (convertSvgToPngFile is not null)
                {
                    return new EnhancedGraphicsMetafileOptimizationResult()
                    {
                        OptimizedImageFile = convertSvgToPngFile
                    };
                }
                else
                {
                    return EnhancedGraphicsMetafileOptimizationResult.NotSupported();
                }
            }
            catch (Exception e)
            {
                context.LogMessage($"Convert svg to png file failed. File: '{svgImageFile.FullName}' Exception: {e}");

                return EnhancedGraphicsMetafileOptimizationResult.FailException(e);
            }
        }
    }

    /// <summary>
    /// 使用基于 Oxage.Wmf 的 SkiaWmfRenderer 将 WMF/EMF 转换为 PNG。该方法适用于需要更好文本/公式支持的场景。
    /// </summary>
    /// <param name="context">包含输入文件、输出目录和尺寸限制等信息的上下文。</param>
    /// <returns>如果转换成功则返回包含输出 PNG 文件的结果，否则返回不支持或失败的结果。</returns>
    private static EnhancedGraphicsMetafileOptimizationResult ConvertWithSkiaWmfRenderer(in EnhancedGraphicsMetafileOptimizationContext context)
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

            return new EnhancedGraphicsMetafileOptimizationResult()
            {
                OptimizedImageFile = outputPngFile
            };
        }

        context.LogMessage($"Fail convert wmf to png by SkiaWmfRenderer. File:'{file}'");
        return EnhancedGraphicsMetafileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("linux")]
    private static EnhancedGraphicsMetafileOptimizationResult ConvertWithLibWmf(in EnhancedGraphicsMetafileOptimizationContext context)
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

                return new EnhancedGraphicsMetafileOptimizationResult()
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
            return EnhancedGraphicsMetafileOptimizationResult.FailException(e);
        }

        return EnhancedGraphicsMetafileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("linux")]
    private static EnhancedGraphicsMetafileOptimizationResult ConvertWithInkscape(EnhancedGraphicsMetafileOptimizationContext context)
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
                return new EnhancedGraphicsMetafileOptimizationResult()
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

            return EnhancedGraphicsMetafileOptimizationResult.FailException(e);
        }

        return EnhancedGraphicsMetafileOptimizationResult.NotSupported();
    }

    [SupportedOSPlatform("windows6.1")]
    private static EnhancedGraphicsMetafileOptimizationResult ConvertInWindows(in EnhancedGraphicsMetafileOptimizationContext context)
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

            return new EnhancedGraphicsMetafileOptimizationResult()
            {
                OptimizedImageFile = new FileInfo(convertedFile),
            };
        }
        catch (Exception e)
        {
            context.LogMessage($"Fail convert emf or wmf to png by GDI. File:'{file}' Exception:{e}");

            return EnhancedGraphicsMetafileOptimizationResult.FailException(e);
        }
    }
}