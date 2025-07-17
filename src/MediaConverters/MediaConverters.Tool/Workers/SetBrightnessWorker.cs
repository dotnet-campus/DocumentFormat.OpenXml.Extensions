using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetBrightnessWorker : WorkerBase<SetBrightnessTask>
{
    protected override void RunCore(Image<Rgba32> image, SetBrightnessTask task)
    {
        image.SetBrightness(task.Percentage);
    }
}