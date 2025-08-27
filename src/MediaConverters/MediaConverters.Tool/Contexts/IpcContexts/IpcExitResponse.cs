namespace DotNetCampus.MediaConverters.Contexts.IpcContexts;

public class IpcExitResponse
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public int Code { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    public string? Message { get; set; }
}