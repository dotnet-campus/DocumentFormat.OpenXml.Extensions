using System.Diagnostics.CodeAnalysis;

namespace DotNetCampus.MediaConverter.SkiaWmfRenderer.Optimizations;

/// <summary>
/// 表示增强图元优化操作的结果。
/// </summary>
public readonly record struct EnhancedGraphicsMetafileOptimizationResult()
{
    /// <summary>
    /// 如果优化过程成功并产生了输出文件，则为 true。
    /// </summary>
    [MemberNotNullWhen(returnValue: true)]
    public bool IsSuccess => OptimizedImageFile is not null;

    /// <summary>
    /// 优化后生成的图像文件（如果成功）。
    /// </summary>
    public FileInfo? OptimizedImageFile { get; init; }

    /// <summary>
    /// 表示该操作不被支持（例如平台或格式不支持）。
    /// </summary>
    public bool IsNotSupport { get; init; }

    /// <summary>
    /// 如果发生异常则包含异常对象，否则为 null。
    /// </summary>
    public Exception? Exception { get; init; }

    /// <summary>
    /// 创建一个表示不支持的结果实例。
    /// </summary>
    /// <returns>标识操作不被支持的结果。</returns>
    public static EnhancedGraphicsMetafileOptimizationResult NotSupported()
    {
        return new EnhancedGraphicsMetafileOptimizationResult()
        {
            IsNotSupport = true,
        };
    }

    /// <summary>
    /// 创建一个包含异常信息的失败结果实例。
    /// </summary>
    /// <param name="exception">导致失败的异常。</param>
    /// <returns>包含异常的失败结果。</returns>
    public static EnhancedGraphicsMetafileOptimizationResult FailException(Exception exception)
    {
        return new EnhancedGraphicsMetafileOptimizationResult()
        {
            Exception = exception,
        };
    }
}