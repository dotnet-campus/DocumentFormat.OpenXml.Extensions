using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ReplaceColorInfo))]
[JsonSerializable(typeof(ImageConvertContext))]
[JsonSerializable(typeof(IImageConvertTask))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}