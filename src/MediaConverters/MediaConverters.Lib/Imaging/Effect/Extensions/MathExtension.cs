using System;
using System.Diagnostics.CodeAnalysis;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Extensions;

/// <summary>
/// 数学拓展方法
/// </summary>
[SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
internal static class MathExtension
{
    /// <summary>
    /// 比较两个double值的大小
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public static bool AlmostEquals(this double a, double b, double epsilon = 0.00001)
    {
        if (double.IsNaN(a) || double.IsNaN(b))
        {
            return false;
        }

        if (double.IsPositiveInfinity(a) || double.IsPositiveInfinity(b))
        {
            return a == b;
        }

        if (double.IsNegativeInfinity(a) || double.IsNegativeInfinity(b))
        {
            return a == b;
        }

        return Math.Abs(a - b) < epsilon;
    }

    /// <summary>
    /// 比较两个float值的大小
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public static bool AlmostEquals(this float a, float b, float epsilon = 0.00001f)
    {
        if (float.IsNaN(a) || float.IsNaN(b))
        {
            return false;
        }

        if (float.IsPositiveInfinity(a) || float.IsPositiveInfinity(b))
        {
            return a == b;
        }

        if (float.IsNegativeInfinity(a) || float.IsNegativeInfinity(b))
        {
            return a == b;
        }

        return Math.Abs(a - b) < epsilon;
    }

    /// <summary>
    /// 矩阵乘法
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static float[,] MultiplyMatrices(float[,] a, float[,] b)
    {
        var aRows = a.GetLength(0);
        var aCols = a.GetLength(1);
        var bRows = b.GetLength(0);
        var bCols = b.GetLength(1);

        if (aCols != bRows)
            throw new Exception("Inner matrix dimensions must match.");

        var result = new float[aRows, bCols];

        for (var i = 0; i < aRows; ++i)
        {
            for (var j = 0; j < bCols; ++j)
            {
                float sum = 0;
                for (var k = 0; k < aCols; ++k)
                {
                    sum += (a[i, k] * b[k, j]);
                }
                result[i, j] = sum;
            }
        }

        return result;
    }
}