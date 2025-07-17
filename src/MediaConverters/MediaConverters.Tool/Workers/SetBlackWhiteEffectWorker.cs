using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetBlackWhiteEffectWorker : WorkerBase<SetBlackWhiteEffectTask>
{
    protected override void RunCore(Image<Rgba32> image, SetBlackWhiteEffectTask task)
    {
        image.SetBlackWhiteEffect(task.Threshold);
    }
}