using DotNetCampus.MediaConverters.Imaging.Effects;
using DotNetCampus.MediaConverters.Imaging.Effects.Colors;
using DotNetCampus.MediaConverters.Utils;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Contexts;

public class ReplaceColorTask : IImageConvertTask
{
    public List<ReplaceColorInfo>? ReplaceColorInfoList { get; init; }

    public void Run(Image<Rgba32> image)
    {
        if (ReplaceColorInfoList is null || ReplaceColorInfoList.Count == 0)
        {
            return;
        }

        var dictionary = new List<(ColorMetadata SourceColor, ColorMetadata TargetColor)>(ReplaceColorInfoList.Count);
        foreach (var replaceColorInfo in ReplaceColorInfoList)
        {
            var (oldColor, newColor) = replaceColorInfo.ToRgba32Pair();
            var oldMetadata = new ColorMetadata(oldColor);
            var newMetadata = new ColorMetadata(newColor);
            dictionary.Add((oldMetadata, newMetadata));
        }

        image.ReplaceColor(dictionary);
    }
}