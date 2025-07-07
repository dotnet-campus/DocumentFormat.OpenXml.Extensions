using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonDerivedType(typeof(ReplaceColorTask), typeDiscriminator: nameof(ReplaceColorTask))]
public interface IImageConvertTask
{
}