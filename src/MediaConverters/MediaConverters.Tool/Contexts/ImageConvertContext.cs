using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

public class ImageConvertContext
{
    public int? MaxImageWidth { get; init; }
    public int? MaxImageHeight { get; init; }

    public bool? UseAreaSizeLimit { get; init; }

    public List<IImageConvertTask>? ImageConvertTaskList { get; init; }
}