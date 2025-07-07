using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetSoftEdgeEffectWorker : WorkerBase<SetSoftEdgeEffectTask>
{
    protected override void RunCore(Image<Rgba32> image, SetSoftEdgeEffectTask task)
    {
        image.SetSoftEdgeEffect(task.Radius);
    }
}