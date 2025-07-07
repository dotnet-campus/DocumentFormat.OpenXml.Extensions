using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using DotNetCampus.MediaConverters.Imaging.Effects.Colors;
using DotNetCampus.MediaConverters.Utils;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class ReplaceColorWorker : WorkerBase<ReplaceColorTask>
{
    protected override void RunCore(Image<Rgba32> image, ReplaceColorTask task)
    {
        if (task.ReplaceColorInfoList is null || task.ReplaceColorInfoList.Count == 0)
        {
            return;
        }

        var dictionary = new List<(ColorMetadata SourceColor, ColorMetadata TargetColor)>(task.ReplaceColorInfoList.Count);
        foreach (var replaceColorInfo in task.ReplaceColorInfoList)
        {
            var (oldColor, newColor) = replaceColorInfo.ToRgba32Pair();
            var oldMetadata = new ColorMetadata(oldColor);
            var newMetadata = new ColorMetadata(newColor);
            dictionary.Add((oldMetadata, newMetadata));
        }

        image.ReplaceColor(dictionary);
    }
}