using System;
using DotNetCampus.MediaConverters.Imaging.Effect.Extensions;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Colors;

/// <summary>
/// 色彩模式转换器
/// </summary>
public static class ColorModeConverter
{
    /// <summary>
    /// RGB转HSL（参数值取值范围为0-1）
    /// 注：Windows使用的色彩模式是HSL
    /// </summary>
    /// <param name="r">红色[范围0-1]</param>
    /// <param name="g">绿色[范围0-1]</param>
    /// <param name="b">蓝色[范围0-1]</param>
    /// <returns> 色相[范围0-1]， 饱和度[范围0-1]， 明度[范围0-1]</returns>
    public static (float hue, float saturation, float brightness) RgbToHsl(float r, float g, float b)
    {
        var max = Math.Max(Math.Max(r, g), b);
        var min = Math.Min(Math.Min(r, g), b);

        //计算色相
        float hue;
        var delta = max - min;
        if (r.AlmostEquals(max))
            hue = (g - b) / delta;
        else if (g.AlmostEquals(max))
            hue = (b - r) / delta + 2f;
        else
            hue = (r - g) / delta + 4f;
        hue *= 60f;
        if (hue < 0f)
            hue += 360f;

        //计算饱和度
        var saturation = 0f;
        if (!r.AlmostEquals(g) || b.AlmostEquals(g))
        {
            var div = max + min;
            if (div > 1f)
                div = 1f * 2 - max - min;
            saturation = (max - min) / div;
        }

        //计算明度
        var brightness = (max + min) / 2;

        //值归一化
        return (hue / 360f, saturation, brightness);
    }

    /// <summary>
    /// HSL转RBG（参数值取值范围为0-1）
    /// 注：Windows使用的色彩模式是HSL
    /// </summary>
    /// <param name="hue">色相[范围0-1]</param>
    /// <param name="saturation">饱和度[范围0-1]</param>
    /// <param name="lightness">明度[范围0-1]</param>
    /// <returns>红色[范围0-1]， 绿色[范围0-1]， 蓝色[范围0-1]</returns>
    public static (float, float, float) HslToRgb(float hue, float saturation, float lightness)
    {
        if (saturation == 0)
        {
            return (lightness, lightness, lightness);
        }
        else
        {
            var q = lightness < 0.5
                ? lightness * (1 + saturation)
                : lightness + saturation - lightness * saturation;
            var p = 2 * lightness - q;
            var rgb = new[] { hue + 1 / 3f, hue, hue - 1 / 3f };
            for (var i = 0; i < 3; i++)
            {
                if (rgb[i] < 0)
                    rgb[i] += 1;
                if (rgb[i] > 1)
                    rgb[i] -= 1;
                if (rgb[i] * 6 < 1)
                {
                    rgb[i] = p + (q - p) * 6 * rgb[i];
                }
                else if (rgb[i] * 2 < 1)
                {
                    rgb[i] = q;
                }
                else if (rgb[i] * 3 < 2)
                {
                    rgb[i] = p + (q - p) * (2 / 3f - rgb[i]) * 6;
                }
                else
                    rgb[i] = p;
            }

            return (rgb[0], rgb[1], rgb[2]);
        }
    }
}
