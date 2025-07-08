namespace DotNetCampus.MediaConverters.Contexts;

/// <summary>
///     设置冲蚀效果。
/// </summary>
public class SetLuminanceEffectTask : IImageConvertTask
{
    /// <summary>
    ///     亮度
    /// </summary>
    public float? Brightness { get; set; }

    /// <summary>
    ///     对比度
    /// </summary>
    public float? Contrast { get; set; }
}