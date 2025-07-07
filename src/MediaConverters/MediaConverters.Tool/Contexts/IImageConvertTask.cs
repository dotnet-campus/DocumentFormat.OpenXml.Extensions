using System.Text.Json.Serialization;

namespace DotNetCampus.MediaConverters.Contexts;

[JsonDerivedType(typeof(ReplaceColorTask))]
public interface IImageConvertTask
{
}