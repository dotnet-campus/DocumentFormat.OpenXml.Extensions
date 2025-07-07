namespace DotNetCampus.MediaConverters.Contexts;

/// <summary>
///     设置柔化边缘效果
/// </summary>
public class SetSoftEdgeEffectTask : IImageConvertTask
{
    public float Radius { get; init; }
}