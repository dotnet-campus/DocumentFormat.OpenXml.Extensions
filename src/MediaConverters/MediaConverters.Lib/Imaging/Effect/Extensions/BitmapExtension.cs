using DotNetCampus.MediaConverters.Imaging.Effect.Color;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Advanced;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Extensions;

/// <summary>
/// <see cref="Image{Rgba32}"/>拓展
/// </summary>
internal static class BitmapExtension
{
    #region 逐像素处理

    /// <summary>
    /// 将<see cref="Image{Rgba32}"/>逐像素处理
    /// </summary>
    /// <returns></returns>
    public static void PerPixelProcess(this Image<Rgba32> image, Func<ColorMetadata, ColorMetadata> func)
    {
        //var stopwatch = new Stopwatch();
        //stopwatch.Start();
        Parallel.For(0, image.Height, rowIndex =>
        {
            Memory<Rgba32> rowMemory = image.DangerousGetPixelRowMemory(rowIndex);

            var span = rowMemory.Span;

            for (int colIndex = 0; colIndex < span.Length; colIndex++)
            {
                //获取颜色
                var color = new ColorMetadata(span[colIndex]);
                //处理颜色
                var targetColor = func(color);
                //保存颜色
                span[colIndex] = targetColor.Color;
            }
        });
        //stopwatch.Stop();

        //var pixelFormat = bitmap.PixelFormat;

        //if (pixelFormat != PixelFormat.Format32bppArgb && pixelFormat != PixelFormat.Format24bppRgb)
        //{
        //    throw new NotSupportedException($"Unsupported image pixel format {nameof(pixelFormat)} is used.");
        //}

        //var cols = bitmap.Width;
        //var rows = bitmap.Height;
        //var channels = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
        //var total = cols * rows * channels;

        ////锁定图片并拷贝图片像素
        //var rect = new Rectangle(0, 0, cols, rows);
        //var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
        //var iPtr = bitmapData.Scan0;
        //var data = new byte[total];
        //Marshal.Copy(iPtr, data, 0, total);

        ////逐像素处理
        //Parallel.For(0, rows, row =>
        //{
        //    for (var col = 0; col < cols; col++)
        //    {
        //        var indexOffset = (row * cols + col) * channels;
        //        var sourceColorMetadata = CreateColorFromData(data, indexOffset, channels);
        //        var targetColorMetadata = func(sourceColorMetadata);
        //        SaveToData(targetColorMetadata, data, indexOffset, channels);
        //    }
        //});

        //Marshal.Copy(data, 0, iPtr, total);
        //bitmap.UnlockBits(bitmapData);
    }

    /// <summary>
    ///     尝试获取颜色
    /// </summary>
    /// <param name="colorData"></param>
    /// <param name="offset"></param>
    /// <param name="channels"></param>
    /// <returns></returns>
    private static ColorMetadata CreateColorFromData(byte[] colorData, int offset, int channels)
    {
        //需要考虑大小端
        var isLittleEndian = BitConverter.IsLittleEndian;

        if (channels == 3)
        {
            var r = isLittleEndian ? colorData[offset + 2] : colorData[offset];
            var g = colorData[offset + 1];
            var b = isLittleEndian ? colorData[offset] : colorData[offset + 2];
            return new ColorMetadata(r / 255f, g / 255f, b / 255f);
        }

        if (channels == 4)
        {
            var a = isLittleEndian ? colorData[offset + 3] : colorData[offset + 0];
            var r = isLittleEndian ? colorData[offset + 2] : colorData[offset + 1];
            var g = isLittleEndian ? colorData[offset + 1] : colorData[offset + 2];
            var b = isLittleEndian ? colorData[offset + 0] : colorData[offset + 3];
            return new ColorMetadata(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        return new ColorMetadata(0 / 255f, 0 / 255f, 0 / 255f);
    }

    /// <summary>
    ///     保存颜色到数组
    /// </summary>
    /// <param name="color"></param>
    /// <param name="colorData"></param>
    /// <param name="offset"></param>
    /// <param name="channels"></param>
    /// <returns></returns>
    private static void SaveToData(ColorMetadata color, byte[] colorData, int offset, int channels)
    {
        //需要考虑大小端
        var isLittleEndian = BitConverter.IsLittleEndian;

        var r = color.ARGB8bit.Item1;
        var g = color.ARGB8bit.Item2;
        var b = color.ARGB8bit.Item3;
        var a = color.ARGB8bit.Item4;

        if (channels == 3)
        {
            colorData[offset] = isLittleEndian ? b : r;
            colorData[offset + 1] = g;
            colorData[offset + 2] = isLittleEndian ? r : b;
        }

        if (channels == 4)
        {
            colorData[offset + 0] = isLittleEndian ? b : a;
            colorData[offset + 1] = isLittleEndian ? g : r;
            colorData[offset + 2] = isLittleEndian ? r : g;
            colorData[offset + 3] = isLittleEndian ? a : b;
        }
    }

    #endregion
}
