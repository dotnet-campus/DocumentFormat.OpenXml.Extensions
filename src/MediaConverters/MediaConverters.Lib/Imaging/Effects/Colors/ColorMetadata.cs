using System;

using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Imaging.Effects.Colors;

public readonly struct ColorMetadata : IEquatable<ColorMetadata>
{
    public ColorMetadata(Rgba32 color)
    {
        Color = color;
        ARGB = (color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
    }

    /// <summary>
    /// 使用RGB数据进行构造
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <param name="a"></param>
    public ColorMetadata(float r, float g, float b, float a = 1)
    {
        ARGB = (r, g, b, a);
        Color = new Rgba32(To8BitValue(r), To8BitValue(g), To8BitValue(b), To8BitValue(a));

        static byte To8BitValue(float value)
        {
            if (value < 0)
            {
                return 0;
            }
            else if (value >= 1)
            {
                return byte.MaxValue;
            }

            return (byte) Math.Min(Math.Round(value * byte.MaxValue), byte.MaxValue);
        }
    }

    public Rgba32 Color { get; init; }

    /// <summary>
    /// RGB模式的红色、绿色、蓝色、透明度通道值[范围0-1]
    /// 注意Alpha通道是最后一个值
    /// </summary>
    public (float R, float G, float B, float A) ARGB { get; }

    /// <summary>
    /// RGB模式的红色、绿色、蓝色通道值[范围0-1]
    /// </summary>
    public (float R, float G, float B) RGB => (ARGB.R, ARGB.G, ARGB.B);

    /// <summary>
    /// RGB模式的红色、绿色、蓝色、透明度通道值[范围0-255]
    /// 注意Alpha通道是最后一个值
    /// </summary>
    public (byte R, byte G, byte B, byte A) ARGB8bit => (Color.R, Color.G, Color.B, Color.A);

    /// <summary>
    /// 获取颜色的灰度值
    /// </summary>
    /// <returns></returns>
    public float GetGrayScale()
    {
        var r = ARGB.R;
        var g = ARGB.G;
        var b = ARGB.B;
        return 0.30f * r + 0.59f * g + 0.11f * b;
    }

    public bool Equals(ColorMetadata other)
    {
        return Color.Equals(other.Color);
    }

    public override bool Equals(object? obj)
    {
        return obj is ColorMetadata other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode();
    }
}
