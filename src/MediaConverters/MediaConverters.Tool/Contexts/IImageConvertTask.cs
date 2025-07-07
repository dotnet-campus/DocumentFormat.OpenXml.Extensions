using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonDerivedType(typeof(ReplaceColorTask))]
public interface IImageConvertTask
{
    void Run(Image<Rgba32> image);
}