// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text.Json;

using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Optimizations;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SourceGenerationContext = DotNetCampus.MediaConverters.Contexts.SourceGenerationContext;

namespace DotNetCampus.MediaConverters;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var options = DotNetCampus.Cli.CommandLine.Parse(args).As<Options>();

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
                    break;
                case ImageFileOptimizationFailureReason.InvalidImageContent:
                    return ErrorCode.InvalidImageContent;
                    break;
                case ImageFileOptimizationFailureReason.FileNotFound:
                    return ErrorCode.ImageFileNotFound;
                    break;
                case ImageFileOptimizationFailureReason.NotSupported:
                    return ErrorCode.NotSupported;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return ErrorCode.UnknownError;
        }

        FileInfo optimizedImageFile = imageFileOptimizationResult.OptimizedImageFile;

        if (imageConvertContext.ImageConvertTaskList is { } list)
        {
            using var image = await Image.LoadAsync<Rgba32>(optimizedImageFile.FullName);

            foreach (var imageConvertTask in list)
            {
                imageConvertTask.Run(image);
            }
        }

        optimizedImageFile.CopyTo(options.OutputFile, overwrite: true);

        return 0;
    }
}
