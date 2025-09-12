using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

public static class ImageFileOptimization
{
    /// <summary>
    /// 对插入的图片进行一些预处理，以解决各种各样的图片处理问题（如 DPI 问题、内存问题、后缀名错误等）。
    /// 其中，最大宽高是图片为 96.0 DPI 下的逻辑宽高。
    /// 在处理完后，将返回一张新的图片路径。
    /// </summary>
    /// <param name="copyNewFile">是否先行拷贝新的文件，再进行处理，避免图片占用。默认为 true。</param>
    /// <param name="context"></param>
    /// <param name="useAreaSizeLimit">当包含宽度高度限制时，采用面积限制。采用面积限制时，可能宽度或高度依然超过限制的最大宽度高度。采用面积限制时，可以保证最大像素数量小于限制数量的同时，让图片可以达到最大尺寸</param>
    /// <returns></returns>
    public static async Task<ImageFileOptimizationResult> OptimizeImageFileAsync(ImageFileOptimizationContext context, bool useAreaSizeLimit = true, bool copyNewFile = true)
    {
        var imageFile = context.ImageFile;
        var workingFolder = context.WorkingFolder;
        var maxImageWidth = context.MaxImageWidth;
        var maxImageHeight = context.MaxImageHeight;

        context.LogMessage($"Start optimize image file. File:'{imageFile}'");

        if (!File.Exists(imageFile.FullName))
        {
            // 不能依靠 imageFile.Exists 属性，因为属性可能还没更新
            return new ImageFileOptimizationResult
            {
                OptimizedImageFile = null,
                FailureReason = ImageFileOptimizationFailureReason.FileNotFound
            };
        }

        Directory.CreateDirectory(workingFolder.FullName);

        if (copyNewFile)
        {
            var file = imageFile;
            var newFilePath = Path.Join(workingFolder.FullName, $"Copy_{Path.GetRandomFileName()}_{imageFile.Name}");
            file.CopyTo(newFilePath);
            context.LogMessage($"Copy new file to '{newFilePath}'");
            file = new FileInfo(newFilePath);

            context = context with
            {
                ImageFile = file
            };
        }

        if (IsExtension(".svg"))
        {
            return ImageFileOptimizationResult.NotSupported();
        }
        else if (IsExtension(".wmf") ||
                 IsExtension(".emf"))
        {
            return ImageFileOptimizationResult.NotSupported();
        }

        context.LogMessage($"Start optimize image with ImageSharp. ImageFile: '{context.ImageFile.FullName}'");

        Image<Rgba32> image;
        try
        {
            await using var fileStream = new FileStream(context.ImageFile.FullName, FileMode.Open, FileAccess.Read,
                FileShare.Read);

            image = await Image.LoadAsync<Rgba32>(fileStream);
        }
        catch (ImageFormatException e)
        {
            context.LogMessage(
                $"Fail to load image with ImageSharp. ImageFile: '{context.ImageFile.FullName}' Exception: {e}");

            // 这里是明确的图片处理的错误，可以转换为输出信息。如果是其他错误，继续抛出
            ImageFileOptimizationFailureReason failureReason = default;

            if (e is UnknownImageFormatException)
            {
                failureReason = ImageFileOptimizationFailureReason.UnknownImageFormat;
            }
            else if (e is InvalidImageContentException)
            {
                failureReason = ImageFileOptimizationFailureReason.InvalidImageContent;
            }

            return new ImageFileOptimizationResult()
            {
                OptimizedImageFile = null,
                Exception = e,
                FailureReason = failureReason
            };
        }
        catch (Exception e)
        {
            context.LogMessage($"Fail to load image with ImageSharp. ImageFile: '{context.ImageFile.FullName}' Exception: {e}");

            throw;
        }

        try
        {
            if (image.Metadata.DecodedImageFormat is GifFormat)
            {
                context.LogMessage($"Image format is Gif. NotSupported.");

                image.Dispose();
                return new ImageFileOptimizationResult()
                {
                    OptimizedImageFile = null,
                    FailureReason = ImageFileOptimizationFailureReason.NotSupported
                };
            }

            OptimizeImage(image, maxImageWidth, maxImageHeight, useAreaSizeLimit);

            var outputImageFilePath = Path.Join(workingFolder.FullName, $"{Path.GetRandomFileName()}.png");
            if (context.ShouldSaveToPngFile)
            {
                // 重新保存即可，保存就等于解决了各种格式问题，输出为标准的格式
                await image.SaveAsPngAsync(outputImageFilePath, new PngEncoder()
                {
                    ColorType = PngColorType.RgbWithAlpha,
                    BitDepth = PngBitDepth.Bit8,
                    CompressionLevel = ((PngCompressionLevel?) context.PngCompressionLevel) ?? PngCompressionLevel.DefaultCompression
                });
            }

            return new ImageFileOptimizationResult()
            {
                Image = image,
                OptimizedImageFile = new FileInfo(outputImageFilePath),
                FailureReason = ImageFileOptimizationFailureReason.Ok
            };
        }
        catch (Exception e)
        {
            context.LogMessage($"Fail to optimize image with ImageSharp. ImageFile: '{context.ImageFile.FullName}' Exception: {e}");

            image.Dispose();
            throw;
        }

        bool IsExtension(string extension)
        {
            return string.Equals(context.ImageFile.Extension, extension, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// 优化图片，包括自动旋转、限制图片尺寸等
    /// </summary>
    /// <param name="image"></param>
    /// <param name="maxImageWidth"></param>
    /// <param name="maxImageHeight"></param>
    /// <param name="useAreaSizeLimit"></param>
    public static void OptimizeImage(Image<Rgba32> image, int? maxImageWidth = null, int? maxImageHeight = null, bool useAreaSizeLimit = true)
    {
        // 额外输出旋转图片的情况
        //if (image.Metadata.ExifProfile is not null && image.Metadata.ExifProfile.TryGetValue(SixLabors.ImageSharp.Metadata.Profiles.Exif.ExifTag.Orientation,out var value))
        //{
        //}
        // 不需要去解析 Exif 信息，因为 ImageSharp 会自动处理 Exif 信息
        image.Mutate(context => context.AutoOrient());

        // 不带点的后缀名
        //var fileExtension = image.Metadata.DecodedImageFormat?.FileExtensions.FirstOrDefault();

        if (useAreaSizeLimit && maxImageWidth is not null && maxImageHeight is not null)
        {
            LimitImageSize(image, maxImageWidth.Value * maxImageHeight.Value);
        }
        else
        {
            LimitImageSize(image, maxImageWidth, maxImageHeight);
        }
    }

    /// <summary>
    /// 限制图片尺寸。按照面积方式限制，保持比例
    /// </summary>
    /// <param name="image"></param>
    /// <param name="maxPixelCount"></param>
    public static void LimitImageSize(Image<Rgba32> image, int maxPixelCount)
    {
        if (image.Width * image.Height <= maxPixelCount)
        {
            // 尺寸没有超过限制，直接返回
            return;
        }

        // 提前计算 wh 和 hw 的值。而不是使用 maxPixelCount * Width / Height 的写法
        // 避免大图的 maxPixelCount * Width 出现越界。如传入尺寸为
        // maxImageWidth	507	
        // maxImageHeight	1095
        // maxPixelCount = maxImageWidth x maxImageHeight = 555165
        // 图片的尺寸为 4000x4000
        // 于是就有 maxPixelCount x Width = 555165 x 4000 = 2220660000 > int.MaxValue = 2147483647
        // 出现越界错误

        double wh = image.Width / (double) image.Height;
        double hw = image.Height / (double) image.Width;

        var pixelWidth = (int) Math.Sqrt(maxPixelCount * wh);
        var pixelHeight = (int) Math.Sqrt(maxPixelCount * hw);
        image.Mutate(context => context.Resize(new Size(pixelWidth, pixelHeight), compand: true));
    }

    /// <summary>
    /// 限制图片尺寸。保持比例
    /// </summary>
    /// <param name="image"></param>
    /// <param name="maxImageWidth"></param>
    /// <param name="maxImageHeight"></param>
    public static void LimitImageSize(Image<Rgba32> image, int? maxImageWidth, int? maxImageHeight)
    {
        if (maxImageWidth is null && maxImageHeight is null)
        {
            return;
        }

        if (image.Width <= maxImageWidth && image.Height <= maxImageHeight)
        {
            return;
        }

        var pixelWidth = image.Width;
        var pixelHeight = image.Height;

        var scale = pixelWidth / (double) pixelHeight;

        // 如果图片的宽度超过限制，则缩放到限制的宽度
        if (maxImageWidth is not null && pixelWidth > maxImageWidth)
        {
            pixelWidth = maxImageWidth.Value;
            pixelHeight = (int) Math.Round(pixelWidth / scale, MidpointRounding.AwayFromZero);
        }

        if (maxImageHeight is not null && pixelHeight > maxImageHeight)
        {
            pixelHeight = maxImageHeight.Value;
            pixelWidth = (int) Math.Round(pixelHeight * scale, MidpointRounding.AwayFromZero);
        }

        image.Mutate(context => context.Resize(new Size(pixelWidth, pixelHeight), compand: true));
    }

    /// <summary>
    /// 图片压缩的最大宽度
    /// </summary>
    private const int MaxWidth = 3840;

    /// <summary>
    /// 图片压缩的最大高度
    /// </summary>
    private const int MaxHeight = 2160;
}