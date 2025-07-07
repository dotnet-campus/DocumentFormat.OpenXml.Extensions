using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetLuminanceEffectWorker : WorkerBase<SetLuminanceEffectTask>
{
    protected override void RunCore(Image<Rgba32> image, SetLuminanceEffectTask task)
    {
        _ = task;
        image.SetLuminanceEffect();
    }
}