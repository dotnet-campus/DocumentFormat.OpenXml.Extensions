namespace DotNetCampus.MediaConverters.Contexts.IpcContexts;

public class IpcConvertImageRequest
{
    public string? TraceId { get; set; }
    public string? InputFile { get; set; }
    public string? OutputFile { get; set; }
    public string? ConvertConfigurationFile { get; set; }
    public string? WorkingFolder { get; set; }
}