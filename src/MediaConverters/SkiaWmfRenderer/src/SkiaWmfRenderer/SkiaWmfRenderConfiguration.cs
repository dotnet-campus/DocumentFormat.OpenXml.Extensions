namespace SkiaWmfRenderer;

public class SkiaWmfRenderConfiguration
{
    /// <summary>
    /// 请求的宽度，0 表示不限制
    /// </summary>
    public int RequestWidth { get; set; } = 0;

    /// <summary>
    /// 请求的高度，0 表示不限制
    /// </summary>
    public int RequestHeight { get; set; } = 0;

    /// <summary>
    /// 是否启用调试模式，输出日志到控制台
    /// </summary>
    public bool? ShouldLogToConsole { get; init; } = false;

    /// <summary>
    /// 符号字体文件的路径
    /// </summary>
    public FileInfo? SymbolFontFile { get; init; }

    /// <summary>
    /// 字体文件夹
    /// </summary>
    public DirectoryInfo? FontFolder { get; init; }

    internal void LogMessage(string message)
    {
        if (ShouldLogToConsole is true)
        {
            Console.WriteLine(message);
        }
    }
}