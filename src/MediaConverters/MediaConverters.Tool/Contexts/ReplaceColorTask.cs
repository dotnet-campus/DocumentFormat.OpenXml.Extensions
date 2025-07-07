namespace DotNetCampus.MediaConverters.Contexts;

public class ReplaceColorTask : IImageConvertTask
{
    public List<ReplaceColorInfo>? ReplaceColorInfoList { get; init; }
}