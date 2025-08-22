using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DotNetCampus.MediaConverters.Contexts;

public class ImageConvertContext
{
    public int? MaxImageWidth { get; init; }
    public int? MaxImageHeight { get; init; }

    public bool? UseAreaSizeLimit { get; init; }

    /// <summary>
    /// 是否先行拷贝新的文件，再进行处理，避免图片占用
    /// </summary>
    public bool? ShouldCopyNewFile { get; init; }

    public List<IImageConvertTask>? ImageConvertTaskList { get; init; }

    public string ToJsonText()
    {
        return JsonSerializer.Serialize(this, typeof(ImageConvertContext), MediaConverterJsonSerializerSourceGenerationContext.Default);
    }

    public static ImageConvertContext? FromJsonText(string jsonText)
    {
        return JsonSerializer.Deserialize<ImageConvertContext>(jsonText, MediaConverterJsonSerializerSourceGenerationContext.Default.ImageConvertContext);
    }
}