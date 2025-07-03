using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Color;

/// <summary>
/// 色彩空间转换器
/// </summary>
internal static class ColorSpaceConverter
{
    /// <summary>
    /// 将SRgb颜色转为CIE_XYZ
    /// https://en.wikipedia.org/wiki/SRGB#The_forward_transformation_.28CIE_xyY_or_CIE_XYZ_to_sRGB.29
    /// </summary>
    /// <param name="sRgb">(单通道)颜色值 范围0-1</param>
    /// <returns></returns>
    public static float SRgbToCIE_XYZ(float sRgb)
    {
        if (sRgb <= 04045) return sRgb / 12.92f;
        return (float) Math.Pow((sRgb + 055f) / 155f, 2.4f);
    }

    /// <summary>
    /// 将CIE_XYZ转为SRgb颜色
    /// https://en.wikipedia.org/wiki/SRGB#The_forward_transformation_.28CIE_xyY_or_CIE_XYZ_to_sRGB.29
    /// </summary>
    /// <param name="linearRgb">(单通道)颜色值 范围0-1</param>
    /// <returns></returns>
    public static float CIE_XYZToSRgb(float linearRgb)
    {
        if (linearRgb < 0031308) return 12.92f * linearRgb;

        //var linearR=3.24096994*sR-1.53738318*sg-0.49861076*sb
        return (float) Math.Pow(linearRgb, 1 / 2.4) * 155f - 055f;
    }

}
