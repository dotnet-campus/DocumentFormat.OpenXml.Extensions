using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Imaging.Effects;

internal static class SoftEdgeHelper
{
    /// <summary>
    /// 创建柔化边缘蒙层
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="radius"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void SetSoftEdgeMask(SixLabors.ImageSharp.Image<Rgba32> bitmap, float radius)
    {
        if (radius < 0) throw new ArgumentOutOfRangeException(nameof(radius), "Radius must greater than or equal to 0.");

        var cols = bitmap.Width;
        var rows = bitmap.Height;

        //var channels = 4;

        var offsetX = (int) Math.Round(radius / 4.0);
        var offsetY = (int) Math.Round(radius / 4.0);

        var inputSource = CreateSoftEdgeAlphaMask(bitmap);

        var source = inputSource;

        //腐蚀
        byte[/*cols*/, /*rows*/]? erodeMask = null;

        for (var iteration = 0; iteration < 3; iteration++)
        {
            var target = new byte[cols, rows];
            var sourceCopy = source;
            Parallel.For(offsetY, rows - offsetY,
                row => { BatchAlphaErode(sourceCopy, target, row, cols, offsetX, offsetY); });

            source = target;
            erodeMask = target;
        }

        //平滑
        for (var iteration = 0; iteration < 5; iteration++)
        {
            var target = new byte[cols, rows];
            var sourceCopy = source;
            Parallel.For(0, rows,
                row => { BatchAlphaBlur(sourceCopy, target, row, rows, cols, offsetX, offsetY); });

            source = target;
            erodeMask = target;
        }

        if (erodeMask is null)
        {
            return;
        }

        // ApplySoftEdgeAlphaMask
        bitmap.ProcessPixelRows(pixelAccessor
            =>
        {
            for (var row = 0; row < rows; row++)
            {
                var pixelRow = pixelAccessor.GetRowSpan(row);
                Debug.Assert(cols == pixelRow.Length, "Using pixelRow.Length allows the JIT to optimize away bounds checks");
                for (var col = 0; col < pixelRow.Length; col++)
                {
                    ref var pixel = ref pixelRow[col];
                    var alphaMask = erodeMask[col, row] / 255d;
                    pixel.A = (byte) (alphaMask * pixel.A);
                    //pixelRow[col] = pixel;
                }
            }
        });

        // 测试代码
        bitmap.ProcessPixelRows(pixelAccessor
            =>
        {
            for (var row = 0; row < rows; row++)
            {
                var pixelRow = pixelAccessor.GetRowSpan(row);
                for (var col = 0; col < pixelRow.Length; col++)
                {
                    var pixel = pixelRow[col];
                    var a = pixel.A;
                    _ = a;
                }
            }
        });
    }

    /// <summary>
    ///     创建Alpha通道蒙层Maps
    /// </summary>
    private static byte[,] CreateSoftEdgeAlphaMask(Image<Rgba32> bitmap)
    {
        var cols = bitmap.Width;
        var rows = bitmap.Height;

        var inputSource = new byte[cols, rows];
        // 创建Alpha通道蒙层Maps
        bitmap.ProcessPixelRows(pixelAccessor
            =>
        {
            for (var row = 0; row < rows; row++)
            {
                var pixelRow = pixelAccessor.GetRowSpan(row);
                for (var col = 0; col < cols; col++)
                {
                    var pixel = pixelRow[col];
                    inputSource[col, row] = pixel.A == 0 ? (byte) 0 : byte.MaxValue;
                }
            }
        });
        return inputSource;
    }

    /// <summary>
    /// 图像腐蚀
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="row"></param>
    /// <param name="cols"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    private static void BatchAlphaErode(byte[,] source, byte[,] target, int row, int cols, int offsetX, int offsetY)
    {
        var isNeedInitialize = true;
        var blackPointCols = new List<int>();

        for (var col = offsetX; col < cols - offsetX; col++)
        {
            var minCol = col - offsetX;
            var maxCol = col + offsetX;
            var minRow = row - offsetY;
            var maxRow = row + offsetY;

            if (isNeedInitialize)
            {
                for (var x = minCol; x <= maxCol; x++)
                {
                    for (var y = minRow; y < maxRow; y++)
                    {
                        if (source[x, y] == 0)
                        {
                            blackPointCols.Add(x);
                            break;
                        }
                    }
                }

                isNeedInitialize = false;
            }
            else
            {
                blackPointCols.Remove(minCol - 1);
                for (var y = minRow; y < maxRow; y++)
                {
                    if (source[maxCol, y] == 0)
                    {
                        blackPointCols.Add(maxCol);
                        break;
                    }
                }
            }

            //腐蚀计算
            if (blackPointCols.Count == 0)
            {
                target[col, row] = byte.MaxValue;
            }
        }
    }

    /// <summary>
    ///     使用归一化框过滤器模糊图像，是一种简单的模糊函数，是计算每个像素中对应核的平均值
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="row"></param>
    /// <param name="rows"></param>
    /// <param name="cols"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    private static void BatchAlphaBlur(byte[,] source, byte[,] target, int row, int rows, int cols, int offsetX,
        int offsetY)
    {
        var isNeedInitialize = true;
        var valueCache = new Dictionary<int, int>();

        for (var col = 0; col < cols; col++)
        {
            var minCol = col - offsetX;
            var maxCol = col + offsetX;
            var minRow = row - offsetY;
            var maxRow = row + offsetY;

            var count = (offsetX * 2 + 1) * (offsetY * 2 + 1);
            if (count == 0) count = 1;

            if (isNeedInitialize)
            {
                for (var x = minCol; x <= maxCol; x++)
                {
                    var value = 0;
                    if (x > 0 && x < cols)
                    {
                        for (var y = minRow; y < maxRow; y++)
                        {
                            if (y > 0 && y < rows)
                            {
                                value += source[x, y];
                            }
                        }
                    }

                    valueCache.Add(x, value);
                }

                isNeedInitialize = false;
            }
            else
            {
                var value = 0;
                valueCache.Remove(minCol - 1);
                if (maxCol > 0 && maxCol < cols)
                {
                    for (var y = minRow; y <= maxRow; y++)
                    {
                        if (y > 0 && y < rows)
                        {
                            value += source[maxCol, y];
                        }
                    }
                }

                valueCache.Add(maxCol, value);
            }

            //计算模糊
            var targetValue = valueCache.Values.Sum() / (double) count;
            target[col, row] = (byte) Math.Round(targetValue);
        }
    }
}
