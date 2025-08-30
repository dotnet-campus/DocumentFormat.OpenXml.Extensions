namespace DotNetCampus.MediaConverters.Contexts.IpcContexts;

public class IpcExitRequest
{
    public int ExitCode { get; set; }
    public string? Reason { get; set; }
}