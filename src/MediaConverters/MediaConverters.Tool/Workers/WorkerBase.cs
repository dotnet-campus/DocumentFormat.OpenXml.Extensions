using DotNetCampus.MediaConverters.Contexts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

abstract class WorkerBase<T> : IWorker
    where T : IImageConvertTask
{
    public void Run(Image<Rgba32> image, IImageConvertTask task)
    {
        RunCore(image, (T) task);
    }

    protected abstract void RunCore(Image<Rgba32> image, T task);
    public Type TaskType => typeof(T);
}