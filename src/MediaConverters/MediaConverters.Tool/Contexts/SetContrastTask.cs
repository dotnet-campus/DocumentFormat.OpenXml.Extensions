namespace DotNetCampus.MediaConverters.Contexts;

/// <summary>
///     更改当前图像的对比度。
/// </summary>
public class SetContrastTask : IImageConvertTask
{
    /// <summary>
    /// 转化比例，必须大于或等于 0。
    /// </summary>
    /// <remarks>
    /// 值为 0 将创建一个完全灰色的图像。值为 1 时输入保持不变。
    /// 其他值是效果的线性乘数。允许超过 1 的值，从而提供具有更高对比度的结果。
    /// </remarks>
    public float Percentage { get; init; }

}