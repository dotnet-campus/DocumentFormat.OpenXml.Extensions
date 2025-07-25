using System;
using System.Diagnostics;
using System.IO;

namespace DotNetCampus.MediaConverters.Imaging.Optimizations;

/// <summary>
/// 图片优化的上下文信息
/// </summary>
/// <param name="ImageFile">图片文件</param>
/// <param name="WorkingFolder"></param>
/// <param name="MaxImageWidth">限制图片的最大宽度。为空则表示不限制</param>
/// <param name="MaxImageHeight">限制图片的最大高度。为空则表示不限制</param>
public readonly record struct ImageFileOptimizationContext(FileInfo ImageFile,
    DirectoryInfo WorkingFolder, int? MaxImageWidth = null, int? MaxImageHeight = null)
{
    public Stopwatch TotalStopwatch { get; } = Stopwatch.StartNew();

    public string TraceId { get; init; } = Guid.NewGuid().ToString("N");

    public bool ShouldLogToConsole { get; init; } = false;

    public bool ShouldLogToFile { get; init; } = false;

    public string? LogFileName { get; init; }

    public void LogMessage(string message)
    {
        if (!ShouldLogToConsole && !ShouldLogToFile)
        {
            return;
        }

        if (ShouldLogToConsole)
        {
            Console.WriteLine($"{message} Total Cost {TotalStopwatch.ElapsedMilliseconds}ms");
        }

        if (ShouldLogToFile)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}][{TraceId}] {message}";

            var logFile = Path.Join(WorkingFolder.FullName, LogFileName ?? "Log.txt");

            File.AppendAllLines(logFile, [logMessage]);
        }
    }
}