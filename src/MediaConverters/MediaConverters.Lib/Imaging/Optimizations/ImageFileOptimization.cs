using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

public static class ImageFileOptimization
{
    /// <summary>
    /// 对插入的图片进行一些预处理，以解决各种各样的图片处理问题（如 DPI 问题、内存问题、后缀名错误等）。
    /// 其中，最大宽高是图片为 96.0 DPI 下的逻辑宽高。
    /// 在处理完后，将返回一张新的图片路径。
    /// </summary>
    /// <param name="imageFile"></param>
    /// <param name="maxImageWidth">限制图片的最大宽度。为空则表示不限制</param>
    /// <param name="maxImageHeight">限制图片的最大高度。为空则表示不限制</param>
    /// <param name="copyNewFile">是否先行拷贝新的文件，再进行处理，避免图片占用。默认为 true。</param>
    /// <param name="workingFolder"></param>
    /// <param name="useAreaSizeLimit">当包含宽度高度限制时，采用面积限制。采用面积限制时，可能宽度或高度依然超过限制的最大宽度高度。采用面积限制时，可以保证最大像素数量小于限制数量的同时，让图片可以达到最大尺寸</param>
    /// <returns></returns>
    public static async Task<ImageFileOptimizationResult> OptimizeImageFileAsync(FileInfo imageFile,
        DirectoryInfo workingFolder, int? maxImageWidth = null, int? maxImageHeight = null, bool useAreaSizeLimit = true, bool copyNewFile = true)
    {
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

        var file = imageFile;
        if (copyNewFile)
        {
            var newFilePath = Path.Join(workingFolder.FullName, $"{Path.GetRandomFileName()}_{imageFile.Name}");
            file.CopyTo(newFilePath);
            file = new FileInfo(newFilePath);
        }

        Image<Rgba32> image;
        try
        {
            await using var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

            image = await Image.LoadAsync<Rgba32>(fileStream);
        }
        catch (ImageFormatException e)
        {
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

        using var _ = image;

        if (image.Metadata.DecodedImageFormat is GifFormat)
        {
            return new ImageFileOptimizationResult()
            {
                OptimizedImageFile = null,
                FailureReason = ImageFileOptimizationFailureReason.NotSupported
            };
        }

        OptimizeImage(image, maxImageWidth, maxImageHeight, useAreaSizeLimit);

        // 重新保存即可
        var outputImageFilePath = Path.Join(workingFolder.FullName, $"{Path.GetRandomFileName()}.png");
        await image.SaveAsPngAsync(outputImageFilePath, new PngEncoder()
        {
            ColorType = PngColorType.RgbWithAlpha,
            BitDepth = PngBitDepth.Bit8,
        });

        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = new FileInfo(outputImageFilePath),
            FailureReason = ImageFileOptimizationFailureReason.Ok
        };
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

        var pixelWidth = (int) Math.Sqrt(maxPixelCount * image.Width / (double) image.Height);
        var pixelHeight = (int) Math.Sqrt(maxPixelCount * image.Height / (double) image.Width);
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