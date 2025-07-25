using System;
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
    public static ImageFileOptimizationResult ConvertWmfOrEmfToPngFile(FileInfo file, DirectoryInfo workingFolder)
    {
        // 在 Windows 上，直接使用 GDI+ 将 WMF 或 EMF 文件转换为 PNG 文件
        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            return ConvertInWindows(file, workingFolder);
        }

        if (OperatingSystem.IsLinux())
        {
            // 在 Linux 上，先尝试使用 Inkscape 进行转换，如失败，再使用 libwmf 进行转换
            return ConvertInLinux(file, workingFolder);
        }

        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = null,
            FailureReason = ImageFileOptimizationFailureReason.NotSupported
        };
    }

    [SupportedOSPlatform("linux")]
    private static ImageFileOptimizationResult ConvertInLinux(FileInfo file, DirectoryInfo workingFolder)
    {
        // 在 Linux 上，先尝试使用 Inkscape 进行转换，如失败，再使用 libwmf 进行转换
        // 调用 Inkscape 进行转换
        var svgFile = Path.Join(workingFolder.FullName, $"{Path.GetFileNameWithoutExtension(file.Name)}_{Path.GetRandomFileName()}.svg");

        {
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
                    var convertSvgToPngFile = ImageFileOptimization.ConvertSvgToPngFile(new FileInfo(svgFile), workingFolder);
                    if (convertSvgToPngFile is not null)
                    {
                        return new ImageFileOptimizationResult()
                        {
                            OptimizedImageFile = convertSvgToPngFile
                        };
                    }
                }
            }
            catch (Exception e)
            {
                // 失败了，继续调用 libwmf 进行转换
            }
        }

        // 继续执行 libwmf 的转换，此时不支持 emf 格式
        if (string.Equals(file.Extension, ".emf"))
        {
            return new ImageFileOptimizationResult()
            {
                OptimizedImageFile = null,
                FailureReason = ImageFileOptimizationFailureReason.NotSupported
            };
        }

        // 使用 libwmf 进行转换

        {
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

            using var process = Process.Start(processStartInfo);
            process?.WaitForExit(TimeSpan.FromSeconds(5));
            if (process?.ExitCode == 0 && File.Exists(svgFile))
            {
                // 转换成功，再次执行 SVG 转 PNG 的转换
                var convertedFile = ImageFileOptimization.FixSvgInvalidCharacter(new FileInfo(svgFile),workingFolder);

                var convertSvgToPngFile = ImageFileOptimization.ConvertSvgToPngFile(convertedFile, workingFolder);
                if (convertSvgToPngFile is not null)
                {
                    return new ImageFileOptimizationResult()
                    {
                        OptimizedImageFile = convertSvgToPngFile
                    };
                }
            }
        }

        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = null,
            FailureReason = ImageFileOptimizationFailureReason.NotSupported
        };
    }

    [SupportedOSPlatform("windows6.1")]
    private static ImageFileOptimizationResult ConvertInWindows(FileInfo file, DirectoryInfo workingFolder)
    {
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
            return new ImageFileOptimizationResult
            {
                OptimizedImageFile = null,
                Exception = e,
                FailureReason = ImageFileOptimizationFailureReason.GdiException
            };
        }
    }
}