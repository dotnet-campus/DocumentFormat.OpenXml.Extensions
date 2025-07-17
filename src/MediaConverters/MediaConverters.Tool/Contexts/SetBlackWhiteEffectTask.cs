namespace DotNetCampus.MediaConverters.Contexts;

/// <summary>
///     设置黑白图效果
/// </summary>
public class SetBlackWhiteEffectTask : IImageConvertTask
{
    /// <summary>
    /// 像素灰度大于该阈值设为白色，否则为黑色。范围 0-1
    /// </summary>
    public float Threshold { get; init; }
}