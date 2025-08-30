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
    /// 压缩 PNG 图片时使用的压缩等级，1-9，数值越大压缩率越高但速度越慢，默认值为 6。在 1-9 之外的值会被视为默认值。
    /// </summary>
    public int PngCompressionLevel { get; init; }

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