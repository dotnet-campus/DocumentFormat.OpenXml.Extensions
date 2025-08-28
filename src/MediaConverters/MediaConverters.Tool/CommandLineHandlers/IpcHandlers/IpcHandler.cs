using System;
using dotnetCampus.Ipc.Context;
using dotnetCampus.Ipc.IpcRouteds.DirectRouteds;
using dotnetCampus.Ipc.Pipes;
using dotnetCampus.Ipc.Threading;
using dotnetCampus.Ipc.Utils.Buffers;

using DotNetCampus.Cli;
using DotNetCampus.Cli.Compiler;
using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Contexts.IpcContexts;

using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCampus.MediaConverters.CommandLineHandlers;

public class IpcHandler : ICommandHandler
{
    [Option]
    public required string IpcName { get; init; }

    /// <summary>
    /// 总的工作路径，可用在后续的转换过程中，存放日志或过程文件等
    /// </summary>
    [Option]
    public required string WorkingFolder { get; init; }

    [Option]
    public bool? ShouldLogToConsole { get; init; }

    [Option]
    public bool? ShouldLogToFile { get; init; }

    public async Task<int> RunAsync()
    {
        var ipcConfiguration = new IpcConfiguration()
        {
            AutoReconnectPeers = false,
            IpcLoggerProvider = name => new MediaConverterIpcLogger(name, this),

            // 以下为默认配置
            SharedArrayPool = new SharedArrayPool(ArrayPool<byte>.Shared),
            IpcTaskScheduling = IpcTaskScheduling.GlobalConcurrent,
        };
        ipcConfiguration.UseSystemJsonIpcObjectSerializer(MediaConverterJsonSerializerSourceGenerationContext.Default);

        var ipcProvider = new IpcProvider(IpcName, ipcConfiguration);

        var ipcHandlerLogger = new IpcHandlerLogger(this);
        var exitTaskCompletionSource = new TaskCompletionSource<int>();

        var jsonIpcDirectRoutedProvider = new JsonIpcDirectRoutedProvider(ipcProvider);

        jsonIpcDirectRoutedProvider.AddRequestHandler(IpcPaths.RequestConvertImage, async (IpcConvertImageRequest request) =>
        {
            if (request.InputFile is null || request.OutputFile is null || request.ConvertConfigurationFile is null)
            {
                return IpcConvertImageResponse.FromErrorCode(MediaConverterErrorCode.InvalidIpcRequestArgument);
            }

            var traceId = request.TraceId ?? Guid.NewGuid().ToString();

            var workingFolder = request.WorkingFolder;
            if (string.IsNullOrEmpty(workingFolder))
            {
                workingFolder = Path.Join(WorkingFolder, traceId);
            }

            Directory.CreateDirectory(workingFolder);

            ipcHandlerLogger.LogMessage($"[{traceId}] Receive RequestConvertImage. InputFile='{request.InputFile}' OutputFile='{request.OutputFile}' ConvertConfigurationFile='{request.ConvertConfigurationFile}' WorkingFolder='{workingFolder}'");

            var convertHandler = new ConvertHandler()
            {
                InputFile = request.InputFile,
                OutputFile = request.OutputFile,
                ConvertConfigurationFile = request.ConvertConfigurationFile,
                WorkingFolder = workingFolder,
                ShouldLogToConsole = ShouldLogToConsole,
                ShouldLogToFile = ShouldLogToFile,
            };

            var errorCode = await Program.RunAsync(convertHandler);
            ipcHandlerLogger.LogMessage($"[{traceId}] RequestConvertImage completed. ErrorCode={errorCode.Code} Message={errorCode.Message}");
            return IpcConvertImageResponse.FromErrorCode(errorCode);
        });

        jsonIpcDirectRoutedProvider.AddRequestHandler(IpcPaths.Exit, (IpcExitRequest request) =>
        {
            ipcHandlerLogger.LogMessage($"Request Exit. Code={request.ExitCode} Reason={request.Reason}");

            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                exitTaskCompletionSource.SetResult(request.ExitCode);
            });

            return new IpcExitResponse()
            {
                Code = MediaConverterErrorCode.Success.Code,
                Message = MediaConverterErrorCode.Success.Message
            };
        });

        jsonIpcDirectRoutedProvider.StartServer();
        return await exitTaskCompletionSource.Task;
    }
}

file class IpcHandlerLogger
{
    public IpcHandlerLogger(IpcHandler ipcHandler)
    {
        _ipcHandler = ipcHandler;

        CanLog = _ipcHandler.ShouldLogToConsole is true || _ipcHandler.ShouldLogToFile is true;

        if (_ipcHandler.ShouldLogToFile is true)
        {
            var logFile = Path.Join(_ipcHandler.WorkingFolder, "Log.txt");
            _logFile = new FileInfo(logFile);
        }
    }

    private readonly FileInfo? _logFile;
    private readonly IpcHandler _ipcHandler;
    private readonly Lock _locker = new Lock();

    private bool CanLog { get; }

    public void LogMessage(string message)
    {
        if (!CanLog)
        {
            return;
        }

        if (_ipcHandler.ShouldLogToConsole is true)
        {
            Console.WriteLine(message);
        }

        if (_logFile is { } logFile)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}] {message}";
            lock (_locker)
            {
                File.AppendAllLines(logFile.FullName, [logMessage]);
            }
        }
    }
}