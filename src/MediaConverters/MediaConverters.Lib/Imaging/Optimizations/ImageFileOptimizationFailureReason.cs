namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

public enum ImageFileOptimizationFailureReason
{
    /// <summary>
    /// 没有错误
    /// </summary>
    Ok,

    /// <summary>
    /// 图片文件格式不支持
    /// </summary>
    UnknownImageFormat,

    /// <summary>
    /// 图片内容无效
    /// </summary>
    InvalidImageContent,

    /// <summary>
    /// 图片文件未找到
    /// </summary>
    FileNotFound,

    /// <summary>
    /// 不支持的图片格式
    /// </summary>
    NotSupported,
}