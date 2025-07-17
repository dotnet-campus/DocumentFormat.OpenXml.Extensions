namespace DotNetCampus.MediaConverters.Contexts;

/// <summary>
///     设置柔化边缘效果
/// </summary>
public class SetSoftEdgeEffectTask : IImageConvertTask
{
    public float Radius { get; init; }

    /// <summary>
    /// 要求的图片宽度，如果不指定则使用原图宽度
    /// </summary>
    public int? ImageWidth { get; init; }

    /// <summary>
    /// 要求的图片高度，如果不指定则使用原图高度
    /// </summary>
    public int? ImageHeight { get; init; }
}