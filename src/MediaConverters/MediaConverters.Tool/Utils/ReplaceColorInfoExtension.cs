using System;
using DotNetCampus.MediaConverters.Contexts;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Utils;

internal static class ReplaceColorInfoExtension
{
    public static (Rgba32 OldColor, Rgba32 NewColor) ToRgba32Pair(this ReplaceColorInfo info)
    {
        if (ColorConverter.TryConvertToColor(info.OldColor, out var oldColor) && ColorConverter.TryConvertToColor(info.NewColor, out var newColor))
        {
            return (oldColor,newColor);
        }

        throw new FormatException();
    }
}