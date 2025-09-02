using System;
using System.IO;

using dotnetCampus.Ipc.Utils.Logging;

namespace DotNetCampus.MediaConverters.CommandLineHandlers;

class MediaConverterIpcLogger : IpcLogger
{
    public MediaConverterIpcLogger(string name, IpcHandler ipcHandler) : base(name)
    {
        _ipcHandler = ipcHandler;
        if (_ipcHandler.ShouldLogToFile is true)
        {
            var ipcLogFile = Path.Join(_ipcHandler.WorkingFolder, "IpcLog.log");
            _logFile = new FileInfo(ipcLogFile);
        }
    }

    private readonly IpcHandler _ipcHandler;

    private bool ShouldLogToConsole => _ipcHandler.ShouldLogToConsole ?? false;
    private readonly FileInfo? _logFile;

    protected override bool IsEnabled(LogLevel logLevel)
    {
        if (!ShouldLogToConsole && _logFile is null)
        {
            // 没有任何日志输出
            return false;
        }

        return base.IsEnabled(logLevel);
    }

    protected override void Log<TState>(LogLevel logLevel, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = formatter(state, exception);
        if (ShouldLogToConsole)
        {
            Console.WriteLine($"[IPC] {message}");
        }
        if (_logFile is { } logFile)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}][{logLevel}] {message}";
            lock (logFile)
            {
                File.AppendAllLines(logFile.FullName, [logMessage]);
            }
        }
    }
}