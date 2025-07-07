using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonSourceGenerationOptions(WriteIndented = true)]
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
internal partial class SourceGenerationContext : JsonSerializerContext
{
}