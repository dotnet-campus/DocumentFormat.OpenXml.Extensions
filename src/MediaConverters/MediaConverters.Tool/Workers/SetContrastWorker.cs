using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

internal class SetContrastWorker : WorkerBase<SetContrastTask>
{
    protected override void RunCore(Image<Rgba32> image, SetContrastTask task)
    {
        image.SetContrast(task.Percentage);
    }
}