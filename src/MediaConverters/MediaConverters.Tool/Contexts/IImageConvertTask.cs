using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonDerivedType(typeof(ReplaceColorTask), typeDiscriminator: nameof(ReplaceColorTask))]
[JsonDerivedType(typeof(SetDuotoneEffectTask), typeDiscriminator: nameof(SetDuotoneEffectTask))]
public interface IImageConvertTask
{
}