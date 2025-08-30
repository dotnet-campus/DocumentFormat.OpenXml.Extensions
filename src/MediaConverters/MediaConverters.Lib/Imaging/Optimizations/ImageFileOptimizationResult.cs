using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

/// <summary>
/// 图片文件优化结果
/// </summary>
public readonly record struct ImageFileOptimizationResult() : IDisposable
{
    /// <summary>
    /// 优化后的图片文件
    /// </summary>
    public required FileInfo? OptimizedImageFile { get; init; }

    public Exception? Exception { get; init; }
    public ImageFileOptimizationFailureReason FailureReason { get; init; } = ImageFileOptimizationFailureReason.Ok;

    [MemberNotNullWhen(true, nameof(OptimizedImageFile))]
    public bool IsSuccess => OptimizedImageFile is not null;

    public Image<Rgba32>? Image { get; init; }

    public void Dispose()
    {
        Image?.Dispose();
    }

    public static ImageFileOptimizationResult FailException(Exception e)
    {
        return new ImageFileOptimizationResult()
        {
            OptimizedImageFile = null,
            Exception = e,
            FailureReason = ImageFileOptimizationFailureReason.Exception
        };
    }
}