using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using DotNetCampus.MediaConverters.Imaging.Effects.Colors;
using DotNetCampus.MediaConverters.Utils;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetDuotoneEffectWorker : WorkerBase<SetDuotoneEffectTask>
{
    protected override void RunCore(Image<Rgba32> image, SetDuotoneEffectTask task)
    {
        if (string.IsNullOrEmpty(task.ArgbFormatColor1) || string.IsNullOrEmpty(task.ArgbFormatColor2))
        {
            return;
        }

        if (ColorConverter.TryConvertToColor(task.ArgbFormatColor1, out var color1) && ColorConverter.TryConvertToColor(task.ArgbFormatColor2, out var color2))
        {
            image.SetDuotoneEffect(new ColorMetadata(color1), new ColorMetadata(color2));
        }
    }
}