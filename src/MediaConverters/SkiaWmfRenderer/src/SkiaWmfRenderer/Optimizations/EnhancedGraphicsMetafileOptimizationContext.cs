namespace DotNetCampus.MediaConverter.SkiaWmfRenderer.Optimizations;

/// <summary>
/// 包含用于增强图元（EMF/WMF）优化操作的上下文信息。
/// </summary>
public readonly record struct EnhancedGraphicsMetafileOptimizationContext()
{
    /// <summary>
    /// 跟踪标识符，用于日志定位。
    /// </summary>
    public string TraceId { get; init; } = Guid.NewGuid().ToString("N");

    /// <summary>
    /// 要优化的图像文件信息。
    /// </summary>
    public required FileInfo ImageFile { get; init; }

    /// <summary>
    /// 工作目录，临时输出文件将写入此目录。
    /// </summary>
    public required DirectoryInfo WorkingFolder { get; init; }

    /// <summary>
    /// 请求的最大图像宽度（像素）。null 表示不限制。
    /// </summary>
    public required int? MaxImageWidth { get; init; }

    /// <summary>
    /// 请求的最大图像高度（像素）。null 表示不限制。
    /// </summary>
    public required int? MaxImageHeight { get; init; } = null;

    /// <summary>
    /// 是否将日志输出到控制台。
    /// </summary>
    public bool ShouldLogToConsole { get; init; } = false;

    /// <summary>
    /// 是否将日志写入文件。
    /// </summary>
    public bool ShouldLogToFile { get; init; } = false;

    /// <summary>
    /// 日志文件名（相对于 <see cref="WorkingFolder"/>）。
    /// </summary>
    public string LogFileName { get; init; } = "Log.txt";

    /// <summary>
    /// 将一条日志消息输出到控制台或工作目录中的日志文件（取决于配置）。
    /// </summary>
    /// <param name="message">要记录的消息内容。</param>
    public void LogMessage(string message)
    {
        if (!ShouldLogToConsole && !ShouldLogToFile)
        {
            return;
        }

        if (ShouldLogToConsole)
        {
            Console.WriteLine(message);
        }

        if (ShouldLogToFile)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}][{TraceId}] {message}";

            var logFile = Path.Join(WorkingFolder.FullName, LogFileName ?? "Log.txt");

            File.AppendAllLines(logFile, [logMessage]);
        }
    }
}