using System;
using DotNetCampus.MediaConverters.Contexts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Workers;

interface IWorker
{
    Type TaskType { get; }
    void Run(Image<Rgba32> image, IImageConvertTask task);
}