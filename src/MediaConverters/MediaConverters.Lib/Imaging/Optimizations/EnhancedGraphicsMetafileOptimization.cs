using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

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

        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = null,
            FailureReason = ImageFileOptimizationFailureReason.NotSupported
        };
    }

    [SupportedOSPlatform("linux")]
    private static ImageFileOptimizationResult ConvertInLinux(ImageFileOptimizationContext context)
    {
        var file = context.ImageFile;

        // 在 Linux 上，先尝试使用 Inkscape 进行转换，如失败，再使用 libwmf 进行转换
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

        // 继续执行 libwmf 的转换，此时不支持 emf 格式
        if (string.Equals(file.Extension, ".emf"))
        {
            context.LogMessage($"Convert emf to png is not supported with libwmf. File:'{context.ImageFile.FullName}'");

            return new ImageFileOptimizationResult()
            {
                OptimizedImageFile = null,
                FailureReason = ImageFileOptimizationFailureReason.NotSupported
            };
        }

        // 使用 libwmf 进行转换

        result = ConvertWithInkscapeLibWmf(context);
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
                var convertSvgToPngFile = ImageFileOptimization.ConvertSvgToPngFile(context with
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
                    return new ImageFileOptimizationResult()
                    {
                        OptimizedImageFile = null,
                        FailureReason = ImageFileOptimizationFailureReason.NotSupported
                    };
                }
            }
            catch (Exception e)
            {
                context.LogMessage($"Convert svg to png file failed. File: '{svgImageFile.FullName}' Exception: {e}");

                return ImageFileOptimizationResult.FailException(e);
            }
        }
    }

    [SupportedOSPlatform("linux")]
    private static ImageFileOptimizationResult ConvertWithInkscapeLibWmf(ImageFileOptimizationContext context)
    {
        var file = context.ImageFile;
        var workingFolder = context.WorkingFolder;

        var svgFile = Path.Join(workingFolder.FullName, $"{Path.GetFileNameWithoutExtension(file.Name)}_{Path.GetRandomFileName()}.svg");

        // ./wmf2svg -o 1.svg image.wmf
        var processStartInfo = new ProcessStartInfo("wmf2svg")
        {
            ArgumentList =
            {
                "-o",
                svgFile,
                file.FullName,
            }
        };

        var fontFolder = Path.Join(AppContext.BaseDirectory, "fonts");
        if (Directory.Exists(fontFolder))
        {
            processStartInfo.ArgumentList.Add($"--wmf-fontdir={fontFolder}");
        }

        try
        {
            using var process = Process.Start(processStartInfo);
            process?.WaitForExit(TimeSpan.FromSeconds(5));
            if (process?.ExitCode == 0 && File.Exists(svgFile))
            {
                // 转换成功，再次执行 SVG 转 PNG 的转换
                // 由于可能存在 SVG 文件中包含无效字符的问题，因此需要修复一下
                var convertedFile = ImageFileOptimization.FixSvgInvalidCharacter(context with
                {
                    ImageFile = new FileInfo(svgFile)
                });

                return new ImageFileOptimizationResult()
                {
                    OptimizedImageFile = convertedFile
                };
            }
        }
        catch (Exception e)
        {
            // 比如 wmf2svg: error while loading shared libraries: libwmf-0.2.so.7: cannot open shared object file: No such file or directory 等错误
            context.LogMessage($"Convert emf or wmf to svg by libwmf failed. File:'{file}' Exception: {e}");
            return ImageFileOptimizationResult.FailException(e);
        }

        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = null,
            FailureReason = ImageFileOptimizationFailureReason.NotSupported,
        };
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
            process?.WaitForExit(TimeSpan.FromSeconds(5));
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

        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = null,
            FailureReason = ImageFileOptimizationFailureReason.NotSupported,
        };
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

            return new ImageFileOptimizationResult
            {
                OptimizedImageFile = null,
                Exception = e,
                FailureReason = ImageFileOptimizationFailureReason.GdiException
            };
        }
    }
}