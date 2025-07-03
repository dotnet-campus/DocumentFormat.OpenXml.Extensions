using DotNetCampus.MediaConverters.Imaging.Effect.Color;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces.Conversion;

using System;
using System.Collections.Generic;
using System.Globalization;


using ColorMatrix = DotNetCampus.MediaConverters.Imaging.Effect.Color.ColorMatrix5x5;
using ColorSpaceConverter = DotNetCampus.MediaConverters.Imaging.Effect.Color.ColorSpaceConverter;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Extensions;

/// <summary>
/// 颜色拓展方法
/// </summary>
public static class ColorExtension
{
    /// <summary>
    /// 在图像上执行颜色矩阵的应用。
    /// </summary>
    /// <param name="color">颜色</param>
    /// <param name="colorMatrix">颜色处理矩阵.</param>
    /// <returns>处理后的颜色</returns>
    public static ColorMetadata ApplyMatrix(this ColorMetadata color, ColorMatrix colorMatrix)
    {
        return ColorMatrices.ApplyMatrix(color, colorMatrix);
    }

    /// <summary>
    /// 获取双色调效果颜色
    /// </summary>
    /// <param name="sourceColor"></param>
    /// <param name="clr1"></param>
    /// <param name="clr2"></param>
    /// <returns></returns>
    public static ColorMetadata GetDuotoneColor(this ColorMetadata sourceColor, ColorMetadata clr1, ColorMetadata clr2)
    {
        var grayScale = sourceColor.GetGrayScale();
        var r = clr1.RGB.Item1 * (1 - grayScale) + clr2.RGB.Item1 * grayScale;
        var g = clr1.RGB.Item2 * (1 - grayScale) + clr2.RGB.Item2 * grayScale;
        var b = clr1.RGB.Item3 * (1 - grayScale) + clr2.RGB.Item3 * grayScale;
        return new ColorMetadata(r, g, b, sourceColor.ARGB.Item4);
    }

    /// <summary>
    /// 对图片进行增亮处理
    /// </summary>
    /// <param name="sourceColor"></param>
    /// <param name="percentage">改变图像对比度的百分比。范围 0..100。</param>
    /// <returns></returns>
    public static ColorMetadata Tint(this ColorMetadata sourceColor, float percentage)
    {
        var amount = percentage / 100.0f;
        var linearR = ColorSpaceConverter.SRgbToCIE_XYZ(sourceColor.RGB.Item1);
        var linearG = ColorSpaceConverter.SRgbToCIE_XYZ(sourceColor.RGB.Item2);
        var linearB = ColorSpaceConverter.SRgbToCIE_XYZ(sourceColor.RGB.Item3);
        var r = ColorSpaceConverter.CIE_XYZToSRgb(linearR * amount + 1 - amount);
        var g = ColorSpaceConverter.CIE_XYZToSRgb(linearG * amount + 1 - amount);
        var b = ColorSpaceConverter.CIE_XYZToSRgb(linearB * amount + 1 - amount);
        return new ColorMetadata(r, g, b, sourceColor.ARGB.Item4);
    }

    /// <summary>
    /// 对图片进行加深处理
    /// </summary>
    /// <param name="sourceColor"></param>
    /// <param name="percentage">改变图像对比度的百分比。范围 0..100。</param>
    /// <returns></returns>
    public static ColorMetadata Shade(this ColorMetadata sourceColor, float percentage)
    {
        var amount = percentage / 100.0f;
        var linearR = ColorSpaceConverter.SRgbToCIE_XYZ(sourceColor.RGB.Item1);
        var linearG = ColorSpaceConverter.SRgbToCIE_XYZ(sourceColor.RGB.Item2);
        var linearB = ColorSpaceConverter.SRgbToCIE_XYZ(sourceColor.RGB.Item3);
        var r = ColorSpaceConverter.CIE_XYZToSRgb(linearR * amount);
        var g = ColorSpaceConverter.CIE_XYZToSRgb(linearG * amount);
        var b = ColorSpaceConverter.CIE_XYZToSRgb(linearB * amount);
        return new ColorMetadata(r, g, b, sourceColor.ARGB.Item4);
    }

    /// <summary>
    /// 混合颜色
    /// </summary>
    /// <param name="sourceColor">Color to blend onto the background sourceColor.</param>
    /// <param name="backColor">Color to blend the other sourceColor onto.</param>
    /// <param name="amount">How much of <paramref name="sourceColor"/> to keep,
    /// “on top of” <paramref name="backColor"/>.</param>
    /// <returns>The blended colors.</returns>
    public static ColorMetadata Blend(this ColorMetadata sourceColor, ColorMetadata backColor, double amount)
    {
        var r = (byte) ((sourceColor.RGB.Item1 * amount) + backColor.RGB.Item1 * (1 - amount));
        var g = (byte) ((sourceColor.RGB.Item2 * amount) + backColor.RGB.Item2 * (1 - amount));
        var b = (byte) ((sourceColor.RGB.Item3 * amount) + backColor.RGB.Item3 * (1 - amount));
        return new ColorMetadata(r, g, b, sourceColor.ARGB.Item4);
    }

    /// <summary>
    ///     是否是近似颜色
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="accuracy">Rgb通道允许的误差</param>
    /// <returns></returns>
    public static bool IsNearlyEquals(this ColorMetadata x, ColorMetadata y, double accuracy = 4.8)
    {
        if (Math.Abs(x.ARGB8bit.Item4 - y.ARGB8bit.Item4) > 1)
        {
            return false;
        }

        var difference = ColorDifference(x, y);
        return difference <= accuracy;
    }

    /// <summary>
    /// 在RGB空间上通过公式计算出加权的欧式距离
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static double ColorDifference(ColorMetadata x, ColorMetadata y)
    {
        var m = (x.ARGB8bit.Item1 + y.ARGB8bit.Item1) / 2.0;
        var r = Math.Pow(x.ARGB8bit.Item1 - y.ARGB8bit.Item1, 2);
        var g = Math.Pow(x.ARGB8bit.Item2 - y.ARGB8bit.Item2, 2);
        var b = Math.Pow(x.ARGB8bit.Item3 - y.ARGB8bit.Item3, 2);

        var difference = Math.Sqrt((2 + m / 256) * r + 4 * g + (2 + (255 - m) / 256) * b);

        //针对接近白色和黑色的区域，优化计算
        return m >= 240 || m <= 20 ? difference * 0.1 : difference;
    }

    ///// <summary>
    ///// 将<see cref="ColorMetadata"/>转换为<see cref="Color"/>
    ///// </summary>
    ///// <param name="color"></param>
    ///// <returns></returns>
    //public static Color ToColor(this ColorMetadata color)
    //{
    //    var (r, g, b, a) = color.ARGB8bit;
    //    return Color.FromArgb(a, r, g, b);
    //}

    ///// <summary>
    ///// 将<see cref="Color"/>转换为<see cref="ColorMetadata"/>
    ///// </summary>
    ///// <param name="color"></param>
    ///// <returns></returns>
    //public static ColorMetadata ToColorMetadata(this Color color)
    //{
    //    return new ColorMetadata(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
    //}

    /// <summary>
    /// 将16进制的颜色字符串转为<see cref="ColorMetadata"/>
    /// </summary>
    /// <param name="hexColorText"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool TryConvertColor(string hexColorText, out ColorMetadata? color)
    {
        color = null;

        if (!hexColorText.StartsWith("#"))
        {
            return false;
        }

        var hex = hexColorText.Replace("#", string.Empty);

        // 可以采用的格式如下
        // #FFDFD991   8 个字符 存在 Alpha 通道
        // #DFD991     6 个字符
        // #FD92       4 个字符 存在 Alpha 通道
        // #DAC        3 个字符

        var existAlpha = hex.Length == 8 || hex.Length == 4;
        var isDoubleHex = hex.Length == 8 || hex.Length == 6;

        if (!existAlpha && hex.Length != 6 && hex.Length != 3)
        {
            return false;
        }

        var n = 0;
        byte a;
        // 表示十六进制的字符数量，使用两个字符还是一个字符表示
        var hexCount = isDoubleHex ? 2 : 1;
        if (existAlpha)
        {
            n = hexCount;
            var convertHexToByteResult = ConvertHexToByte(hex, 0, hexCount);
            if (!convertHexToByteResult.success)
            {
                return false;
            }

            a = convertHexToByteResult.hexNumber;
            if (!isDoubleHex)
            {
                a = (byte) (a * 16 + a);
            }
        }
        else
        {
            a = 0xFF;
        }

        var result = ConvertHexToByte(hex, n, hexCount);
        if (!result.success)
        {
            return false;
        }
        var r = result.hexNumber;

        result = ConvertHexToByte(hex, n + hexCount, hexCount);
        if (!result.success)
        {
            return false;
        }
        var g = result.hexNumber;

        result = ConvertHexToByte(hex, n + 2 * hexCount, hexCount);
        if (!result.success)
        {
            return false;
        }
        var b = result.hexNumber;

        if (!isDoubleHex)
        {
            //#FD92 = #FFDD9922

            r = (byte) (r * 16 + r);
            g = (byte) (g * 16 + g);
            b = (byte) (b * 16 + b);
        }

        color = new ColorMetadata(r, g, b, a);
        return true;
    }

    private static (bool success, byte hexNumber) ConvertHexToByte(string hex, int n, int count = 2)
    {
        var hexText = hex.Substring(n, count);

        if (byte.TryParse(hexText, NumberStyles.HexNumber, new NumberFormatInfo(), out var hexNumber))
        {
            return (true, hexNumber);
        }

        return (false, byte.MinValue);
    }
}
