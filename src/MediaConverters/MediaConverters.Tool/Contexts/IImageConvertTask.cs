using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(ReplaceColorTask), typeDiscriminator: nameof(ReplaceColorTask))]
[JsonDerivedType(typeof(SetDuotoneEffectTask), typeDiscriminator: nameof(SetDuotoneEffectTask))]
[JsonDerivedType(typeof(SetBlackWhiteEffectTask), typeDiscriminator: nameof(SetBlackWhiteEffectTask))]
[JsonDerivedType(typeof(SetBrightnessTask), typeDiscriminator: nameof(SetBrightnessTask))]
[JsonDerivedType(typeof(SetContrastTask), typeDiscriminator: nameof(SetContrastTask))]
[JsonDerivedType(typeof(SetGrayScaleEffectTask), typeDiscriminator: nameof(SetGrayScaleEffectTask))]
[JsonDerivedType(typeof(SetLuminanceEffectTask), typeDiscriminator: nameof(SetLuminanceEffectTask))]
[JsonDerivedType(typeof(SetSoftEdgeEffectTask), typeDiscriminator: nameof(SetSoftEdgeEffectTask))]
public interface IImageConvertTask
{
}