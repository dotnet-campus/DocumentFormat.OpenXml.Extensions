using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCampus.MediaConverters.Imaging.Effect.Colors;
using DotNetCampus.MediaConverters.Imaging.Effect.Extensions;
using SixLabors.ImageSharp.Advanced;

namespace DotNetCampus.MediaConverters.Imaging.Effect;

/// <summary>
/// 为<see cref="Image{T}"/>提供效果拓展
/// </summary>
public static class BitmapEffectExtension
{
    /// <summary>
    ///     将图片<paramref name="bitmap"/>上指定的颜色<paramref name="sourceColor"/>替换为颜色<paramref name="targetColor"/>
    /// </summary>
    /// <param name="bitmap">图片</param>
    /// <param name="sourceColor">原始颜色</param>
    /// <param name="targetColor">目标颜色</param>
    public static void ReplaceColor(this Image<Rgba32> bitmap, ColorMetadata sourceColor, ColorMetadata targetColor)
    {
        bitmap.PerPixelProcess(color =>
            color.IsNearlyEquals(sourceColor) ? targetColor : color);
    }

    /// <summary>
    ///     将图片<paramref name="image"/>上指定的颜色<paramref name="sourceColor"/>替换为颜色<paramref name="targetColor"/>
    /// </summary>
    /// <param name="image">图片</param>
    /// <param name="sourceColor">原始颜色</param>
    /// <param name="targetColor">目标颜色</param>
    public static void ReplaceColor(this Image<Rgba32> image, Rgba32 sourceColor, Rgba32 targetColor)
    {
        var sourceMetadata = new ColorMetadata(sourceColor);

        Parallel.For(0, image.Height, rowIndex =>
        {
            Memory<Rgba32> rowMemory = image.DangerousGetPixelRowMemory(rowIndex);

            var span = rowMemory.Span;

            for (int colIndex = 0; colIndex < span.Length; colIndex++)
            {
                //获取颜色
                Rgba32 pixel = span[colIndex];
                // 快速分支
                if (pixel.Equals(sourceColor))
                {
                    span[colIndex] = targetColor;
                }
                else
                {
                    var color = new ColorMetadata(pixel);
                    //处理颜色
                    if (color.IsNearlyEquals(sourceMetadata))
                    {
                        span[colIndex] = targetColor;
                    }
                }
            }
        });
    }

    /// <summary>
    ///     将图片<paramref name="bitmap"/>上指定的一部分颜色替换为指定的对应颜色
    /// </summary>
    /// <param name="bitmap">图片</param>
    /// <param name="colorInfos">存储替换信息的颜色组</param>
    public static void ReplaceColor(this Image<Rgba32> bitmap, Dictionary<ColorMetadata, ColorMetadata> colorInfos)
    {
        bitmap.PerPixelProcess(color =>
        {
            foreach (var colorInfo in colorInfos)
            {
                if (color.IsNearlyEquals(colorInfo.Key))
                {
                    return colorInfo.Value;
                }
            }

            return color;
        });
    }

    /// <summary>
    ///     对图片<paramref name="bitmap"/>设置(DuotoneEffect)双色调效果
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="color1"></param>
    /// <param name="color2"></param>
    public static void SetDuotoneEffect(this Image<Rgba32> bitmap, ColorMetadata color1, ColorMetadata color2)
    {
        bitmap.PerPixelProcess(color => color.GetDuotoneColor(color1, color2));
    }

    /// <summary>
    ///     设置黑白图效果
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="threshold">像素灰度大于该阈值设为白色，否则为黑色。范围 0-1</param>
    public static void SetBlackWhiteEffect(this Image<Rgba32> bitmap, float threshold)
    {
        bitmap.PerPixelProcess(color =>
        {
            var v = color.GetGrayScale() >= threshold ? 1 : 0;
            return new ColorMetadata(v, v, v, color.ARGB.Item4);
        });
    }

    /// <summary>
    ///     更改当前图像的亮度。
    /// </summary>
    /// <param name="bitmap">图片</param>
    /// <param name="percentage">转化比例，必须大于或等于 0。
    /// <remarks>
    /// 值为 0 将创建一个完全黑色的图像。值为 1 时输入保持不变。
    /// 其他值是效果的线性乘数。允许超过 1 的值，从而提供更明亮的结果。
    /// </remarks>
    /// </param>
    public static void SetBrightness(this Image<Rgba32> bitmap, float percentage)
    {
        var colorMatrix = ColorMatrices.CreateBrightnessFilter(percentage);
        bitmap.PerPixelProcess(color => color.ApplyMatrix(colorMatrix));
    }

    /// <summary>
    ///     更改当前图像的对比度。
    /// </summary>
    /// <param name="bitmap">图片</param>
    /// <param name="percentage">转化比例，必须大于或等于 0。
    /// <remarks>
    /// 值为 0 将创建一个完全灰色的图像。值为 1 时输入保持不变。
    /// 其他值是效果的线性乘数。允许超过 1 的值，从而提供具有更高对比度的结果。
    /// </remarks>
    /// </param>
    public static void SetContrast(this Image<Rgba32> bitmap, float percentage)
    {
        var colorMatrix = ColorMatrices.CreateContrastFilter(percentage);
        bitmap.PerPixelProcess(color => color.ApplyMatrix(colorMatrix));
    }

    /// <summary>
    ///     设置灰度图效果。
    /// </summary>
    /// <param name="bitmap">图片</param>
    public static void SetGrayScaleEffect(this Image<Rgba32> bitmap)
    {
        var colorMatrix = ColorMatrices.CreateGrayScaleFilter(1);
        bitmap.PerPixelProcess(color => color.ApplyMatrix(colorMatrix));
    }

    /// <summary>
    ///     设置冲蚀效果。
    /// </summary>
    /// <param name="bitmap">图片</param>
    public static void SetLuminanceEffect(this Image<Rgba32> bitmap)
    {
        bitmap.SetContrast(0.2f);
        bitmap.SetBrightness(1.9f);
    }

    /// <summary>
    ///     设置柔化边缘效果
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="radius"></param>
    public static void SetSoftEdgeEffect(this Image<Rgba32> bitmap, float radius)
    {
        //var pixelFormat = bitmap.PixelFormat;
        //if (pixelFormat != PixelFormat.Format32bppArgb)
        //{
        //    throw new NotSupportedException($"Only {PixelFormat.Format32bppArgb} image pixel format is supported.");
        //}

        SoftEdgeHelper.SetSoftEdgeMask(bitmap, radius);
    }

    /// <summary>
    /// 获取一张图中颜色数量最多的颜色
    /// </summary>
    /// <param name="image"></param>
    public static ColorCount GetMaxCountColor(this Image<Rgba32> image)
    {
        var dictionary = new Dictionary<Rgba32, int>();
        image.ProcessPixelRows(accessor =>
        {
            for (int row = 0; row < accessor.Height; row++)
            {
                var pixelRow = accessor.GetRowSpan(row);
                foreach (var pixel in pixelRow)
                {
                    if (dictionary.TryGetValue(pixel, out int count))
                    {
                        dictionary[pixel] = count + 1;
                    }
                    else
                    {
                        dictionary[pixel] = 1;
                    }
                }
            }
        });

        var (color, count) = dictionary.MaxBy(pair => pair.Value);
        return new ColorCount(color, count);
    }
}

public readonly record struct ColorCount(Rgba32 Color, int Count);