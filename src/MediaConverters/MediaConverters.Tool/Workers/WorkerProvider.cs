using DotNetCampus.MediaConverters.Contexts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

class WorkerProvider
{
    private readonly IReadOnlyList<IWorker> _workerList =
    [
        new ReplaceColorWorker(),
    ];

    public void Run(Image<Rgba32> image, IImageConvertTask task)
    {
        var taskType = task.GetType();
        foreach (var worker in _workerList)
        {
            if (worker.TaskType == taskType)
            {
                worker.Run(image, task);
            }
        }
    }
}