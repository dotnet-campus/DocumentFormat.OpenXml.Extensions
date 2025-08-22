// See https://aka.ms/new-console-template for more information

using System;
using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Optimizations;
using DotNetCampus.MediaConverters.Workers;

using SixLabors.ImageSharp;

using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Formats.Png;
using ErrorCode = DotNetCampus.MediaConverters.Contexts.MediaConverterErrorCode;

namespace DotNetCampus.MediaConverters;

class Program
{
    static async Task<int> Main(string[] args)
    {
        if (args.Length == 0 || args.Length == 1)
        {
            // 调试模式
            var inputFile = "image.wmf";
            if (args.Length == 1)
            {
                inputFile = args[0];
            }

            var imageConvertContext = new ImageConvertContext()
            {
                MaxImageWidth = 1000,
                MaxImageHeight = 1000,
            };

            var testFolder =
                Directory.CreateDirectory(Path.Join(AppContext.BaseDirectory, $"Test_{Path.GetRandomFileName()}"));
            var jsonText = imageConvertContext.ToJsonText();
            var configurationFile = Path.Join(testFolder.FullName, "image-convert.json");
            await File.WriteAllTextAsync(configurationFile, jsonText);
            var outputFile = Path.Join(testFolder.FullName, "1.png");

            return await RunAsync(new Options()
            {
                InputFile = inputFile,
                ConvertConfigurationFile = configurationFile,
                WorkingFolder = testFolder.FullName,
                OutputFile = outputFile,

                ShouldLogToConsole = true,
                ShouldLogToFile = true,
            });
        }

        var options = DotNetCampus.Cli.CommandLine.Parse(args).As<Options>();

        return await RunAsync(options);
    }

    internal static async Task<ErrorCode> RunAsync(Options options)
    {
        var stopwatch = Stopwatch.StartNew();

        var jsonText = await File.ReadAllTextAsync(options.ConvertConfigurationFile);

        var imageConvertContext = ImageConvertContext.FromJsonText(jsonText);

        if (imageConvertContext is null)
        {
            Console.Error.WriteLine("无法解析转换配置文件，请检查配置文件内容是否正确。");
            return ErrorCode.UnknownError;
        }

        var inputFile = new FileInfo(options.InputFile);

        var workingFolder = Directory.CreateDirectory(options.WorkingFolder);

        var useAreaSizeLimit = imageConvertContext.UseAreaSizeLimit ?? true;
        var copyNewFile = imageConvertContext.ShouldCopyNewFile ?? true;

        var context = new ImageFileOptimizationContext(inputFile, workingFolder, imageConvertContext.MaxImageWidth,
            imageConvertContext.MaxImageHeight)
        {
            ShouldLogToConsole = options.ShouldLogToConsole ?? false,
            ShouldLogToFile = options.ShouldLogToFile ?? false,
        };
        using var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(context, useAreaSizeLimit, copyNewFile);

        if (!imageFileOptimizationResult.IsSuccess)
        {
            var errorMessage = $"Failed to convert image file '{inputFile.FullName}'. Reason: {imageFileOptimizationResult.FailureReason}";
            if (imageFileOptimizationResult.Exception is { } exception)
            {
                errorMessage += $". Exception: {exception}";
            }

            Console.Error.WriteLine(errorMessage);
            
            switch (imageFileOptimizationResult.FailureReason)
            {
                case ImageFileOptimizationFailureReason.Ok:
                    Debug.Fail("返回成功时，不可能状态是失败");
                    break;
                case ImageFileOptimizationFailureReason.UnknownImageFormat:
                    return ErrorCode.UnknownImageFormat;
                case ImageFileOptimizationFailureReason.InvalidImageContent:
                    return ErrorCode.InvalidImageContent;
                case ImageFileOptimizationFailureReason.FileNotFound:
                    return ErrorCode.ImageFileNotFound;
                case ImageFileOptimizationFailureReason.NotSupported:
                    return ErrorCode.NotSupported;
                case ImageFileOptimizationFailureReason.GdiException:
                    return ErrorCode.GdiException;
                case ImageFileOptimizationFailureReason.Exception:
                    return ErrorCode.UnknownException;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown FailureReason. FailureReason={imageFileOptimizationResult.FailureReason}");
            }

            return ErrorCode.UnknownError;
        }

        FileInfo optimizedImageFile = imageFileOptimizationResult.OptimizedImageFile;
        var image = imageFileOptimizationResult.Image;

        if (image is not null && imageConvertContext.ImageConvertTaskList is { } list)
        {
            var workerProvider = new WorkerProvider();

            foreach (IImageConvertTask imageConvertTask in list)
            {
                workerProvider.Run(image, imageConvertTask);
            }

            await image.SaveAsPngAsync(options.OutputFile, new PngEncoder()
            {
                ColorType = PngColorType.RgbWithAlpha,
                BitDepth = PngBitDepth.Bit8,
            });
        }
        else
        {
            optimizedImageFile.CopyTo(options.OutputFile, overwrite: true);
        }

        stopwatch.Stop();
        context.LogMessage($"Success converted image. Cost {stopwatch.ElapsedMilliseconds}ms. OutputFile:'{options.OutputFile}'");

        return ErrorCode.Success;
    }
}
