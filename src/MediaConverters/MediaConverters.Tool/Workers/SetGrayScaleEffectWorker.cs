using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetGrayScaleEffectWorker : WorkerBase<SetGrayScaleEffectTask>
{
    protected override void RunCore(Image<Rgba32> image, SetGrayScaleEffectTask task)
    {
        _ = task;
        image.SetGrayScaleEffect();
    }
}