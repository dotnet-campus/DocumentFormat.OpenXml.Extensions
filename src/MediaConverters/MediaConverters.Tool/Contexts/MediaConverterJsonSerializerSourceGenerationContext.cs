using DotNetCampus.MediaConverters.Contexts.IpcContexts;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonSourceGenerationOptions(WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(ReplaceColorInfo))]
[JsonSerializable(typeof(SetDuotoneEffectTask))]
[JsonSerializable(typeof(SetBlackWhiteEffectTask))]
[JsonSerializable(typeof(SetBrightnessTask))]
[JsonSerializable(typeof(SetContrastTask))]
[JsonSerializable(typeof(SetGrayScaleEffectTask))]
[JsonSerializable(typeof(SetLuminanceEffectTask))]
[JsonSerializable(typeof(SetSoftEdgeEffectTask))]
[JsonSerializable(typeof(ImageConvertContext))]
[JsonSerializable(typeof(IImageConvertTask))]
// IPC 相关
[JsonSerializable(typeof(IpcExitRequest))]
[JsonSerializable(typeof(IpcExitResponse))]
[JsonSerializable(typeof(IpcConvertImageRequest))]
[JsonSerializable(typeof(IpcConvertImageResponse))]
public partial class MediaConverterJsonSerializerSourceGenerationContext : JsonSerializerContext
{
}