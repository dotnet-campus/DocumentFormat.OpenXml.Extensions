using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ColorMatrix = DotNetCampus.MediaConverters.Imaging.Effect.Color.ColorMatrix5x5;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Color;

/// <summary>
///     颜色转换矩阵
/// </summary>
public static class ColorMatrices
{
    /// <summary>
    /// 在图像上执行颜色矩阵的应用。
    /// </summary>
    /// <param name="color">颜色</param>
    /// <param name="m">颜色处理矩阵.</param>
    /// <returns>处理后的颜色</returns>
    public static ColorMetadata ApplyMatrix(in ColorMetadata color, in ColorMatrix m)
    {
        // 应用颜色转换矩阵将RGB颜色转换为新颜色
        var r = color.ARGB.R;
        var g = color.ARGB.G;
        var b = color.ARGB.B;
        var a = color.ARGB.A;
        var w = 1.0f;
        Span<float> newColor =
        [
            (m.Matrix00 * r) + (m.Matrix10 * g) + (m.Matrix20 * b) + (m.Matrix30 * a) + (m.Matrix40 * w),
                (m.Matrix01 * r) + (m.Matrix11 * g) + (m.Matrix21 * b) + (m.Matrix31 * a) + (m.Matrix41 * w),
                (m.Matrix02 * r) + (m.Matrix12 * g) + (m.Matrix22 * b) + (m.Matrix32 * a) + (m.Matrix42 * w),
                (m.Matrix03 * r) + (m.Matrix13 * g) + (m.Matrix23 * b) + (m.Matrix33 * a) + (m.Matrix43 * w),
                (m.Matrix04 * r) + (m.Matrix14 * g) + (m.Matrix24 * b) + (m.Matrix34 * a) + (m.Matrix44 * w)
        ];

        if (newColor[0] < 0) newColor[0] = 0;
        if (newColor[0] > 1) newColor[0] = 1;
        if (newColor[1] < 0) newColor[1] = 0;
        if (newColor[1] > 1) newColor[1] = 1;
        if (newColor[2] < 0) newColor[2] = 0;
        if (newColor[2] > 1) newColor[2] = 1;
        if (newColor[3] < 0) newColor[3] = 0;
        if (newColor[3] > 1) newColor[3] = 1;
        if (newColor[4] < 0) newColor[4] = 0;
        if (newColor[4] > 1) newColor[4] = 1;

        return new ColorMetadata(newColor[0], newColor[1], newColor[2], newColor[3]);
    }

    /// <summary>
    /// 使用给定的数量创建亮度过滤器矩阵。
    /// <para>
    /// 使用算法<see href="https://cs.chromium.org/chromium/src/cc/paint/render_surface_filters.cc"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// 值为 0 将创建一个完全黑色的图像。值为 1 时输入保持不变。
    /// 其他值是效果的线性乘数。允许超过 1 的值，从而提供更明亮的结果。
    /// </remarks>
    /// <param name="amount">转化比例，必须大于或等于 0。</param>
    /// <returns>The <see cref="ColorMatrix"/>.</returns>
    public static ColorMatrix CreateBrightnessFilter(float amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Threshold must be >= 0");
        }

        return new ColorMatrix
        {
            Matrix00 = amount,
            Matrix11 = amount,
            Matrix22 = amount,
            Matrix33 = 1F
        };
    }

    /// <summary>
    /// 使用给定的数量创建灰度滤波器矩阵。
    /// <para>
    /// 使用算法<see href="https://en.wikipedia.org/wiki/Luma_%28video%29#Rec._601_luma_versus_Rec._709_luma_coefficients"/>
    /// </para>
    /// </summary>
    /// <param name="amount">转化比例，必须大于或等于 0 小于等于 1。</param>amount
    /// <returns>The <see cref="ColorMatrix"/>.</returns>
    public static ColorMatrix CreateGrayScaleFilter(float amount)
    {
        if (amount < 0 || amount > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Threshold must be in range 0..1");
        }

        amount = 1F - amount;

        var matrix = new ColorMatrix
        {
            Matrix00 = .299F + (.701F * amount),
            Matrix10 = .587F - (.587F * amount),
            Matrix01 = .299F - (.299F * amount),
            Matrix11 = .587F + (.2848F * amount),
            Matrix02 = .299F - (.299F * amount),
            Matrix12 = .587F - (.587F * amount),
            Matrix33 = 1F
        };

        matrix = matrix with
        {
            Matrix20 = 1F - (matrix.Matrix00 + matrix.Matrix10),
            Matrix21 = 1F - (matrix.Matrix01 + matrix.Matrix11),
            Matrix22 = 1F - (matrix.Matrix02 + matrix.Matrix12),
        };

        return matrix;
    }

    /// <summary>
    /// 使用给定的数量创建对比度过滤器矩阵。
    /// <para>
    /// 使用算法<see href="https://cs.chromium.org/chromium/src/cc/paint/render_surface_filters.cc"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// 值为 0 将创建一个完全灰色的图像。值为 1 时输入保持不变。
    /// 其他值是效果的线性乘数。允许超过 1 的值，从而提供具有更高对比度的结果。
    /// </remarks>
    /// <param name="amount">转化比例，必须大于或等于 0。</param>
    /// <returns>The <see cref="ColorMatrix"/>.</returns>
    public static ColorMatrix CreateContrastFilter(float amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Threshold must be >= 0");
        }

        var contrast = (-.5F * amount) + .5F;

        return new ColorMatrix
        {
            Matrix00 = amount,
            Matrix11 = amount,
            Matrix22 = amount,
            Matrix33 = 1F,
            Matrix40 = contrast,
            Matrix41 = contrast,
            Matrix42 = contrast
        };
    }

    /// <summary>
    /// 使用给定的数量创建饱和度过滤器矩阵。
    /// <para>
    /// 使用算法<see href="https://cs.chromium.org/chromium/src/cc/paint/render_surface_filters.cc"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// 0 值是完全不饱和的。值为 1 时输入保持不变。
    /// 其他值是效果的线性乘数。允许超过 1 的值，提供超饱和结果。
    /// </remarks>
    /// <param name="amount">转化比例，必须大于或等于 0。</param>
    /// <returns>The <see cref="ColorMatrix"/>.</returns>
    public static ColorMatrix CreateSaturationFilter(float amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Threshold must be >= 0");
        }

        var matrix = new ColorMatrix
        {
            Matrix00 = .213F + (.787F * amount),
            Matrix10 = .715F - (.715F * amount),
            Matrix01 = .213F - (.213F * amount),
            Matrix11 = .715F + (.285F * amount),
            Matrix02 = .213F - (.213F * amount),
            Matrix12 = .715F - (.715F * amount),
            Matrix33 = 1F
        };

        matrix = matrix with
        {
            Matrix20 = 1F - (matrix.Matrix00 + matrix.Matrix10),
            Matrix21 = 1F - (matrix.Matrix01 + matrix.Matrix11),
            Matrix22 = 1F - (matrix.Matrix02 + matrix.Matrix12),
        };

        return matrix;
    }
}