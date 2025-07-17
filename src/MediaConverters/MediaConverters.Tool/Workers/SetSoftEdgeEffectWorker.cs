using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetSoftEdgeEffectWorker : WorkerBase<SetSoftEdgeEffectTask>
{
    protected override void RunCore(Image<Rgba32> image, SetSoftEdgeEffectTask task)
    {
        if (task.ImageWidth is not null || task.ImageHeight is not null)
        {
            var width = task.ImageWidth ?? image.Width;
            var height = task.ImageHeight ?? image.Height;
            image.Mutate(context => context.Resize(width, height));
        }

        image.SetSoftEdgeEffect(task.Radius);
    }
}