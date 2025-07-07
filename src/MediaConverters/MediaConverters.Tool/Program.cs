// See https://aka.ms/new-console-template for more information

using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Optimizations;
using DotNetCampus.MediaConverters.Workers;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using System.Diagnostics;
using System.Text.Json;

using SourceGenerationContext = DotNetCampus.MediaConverters.Contexts.SourceGenerationContext;

namespace DotNetCampus.MediaConverters;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var options = DotNetCampus.Cli.CommandLine.Parse(args).As<Options>();

        return await RunAsync(options);
    }

    internal static async Task<ErrorCode> RunAsync(Options options)
    {
        var jsonText = await File.ReadAllTextAsync(options.ConvertConfigurationFile);
        var imageConvertContext = JsonSerializer.Deserialize<ImageConvertContext>(jsonText, SourceGenerationContext.Default.Options);

        if (imageConvertContext is null)
        {
            Console.Error.WriteLine("无法解析转换配置文件，请检查配置文件内容是否正确。");
            return ErrorCode.UnknownError;
        }

        var inputFile = new FileInfo(options.InputFile);

        var workingFolder = Directory.CreateDirectory(options.WorkingFolder);

        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(inputFile, workingFolder, imageConvertContext.MaxImageWidth, imageConvertContext.MaxImageHeight, imageConvertContext.UseAreaSizeLimit ?? true);

        if (!imageFileOptimizationResult.IsSuccess)
        {
            Console.Error.WriteLine(imageFileOptimizationResult);

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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return ErrorCode.UnknownError;
        }

        FileInfo optimizedImageFile = imageFileOptimizationResult.OptimizedImageFile;

        if (imageConvertContext.ImageConvertTaskList is { } list)
        {
            using var image = await Image.LoadAsync<Rgba32>(optimizedImageFile.FullName);
            var workerProvider = new WorkerProvider();

            foreach (IImageConvertTask imageConvertTask in list)
            {
                workerProvider.Run(image, imageConvertTask);
            }
        }

        optimizedImageFile.CopyTo(options.OutputFile, overwrite: true);

        return ErrorCode.Success;
    }
}
