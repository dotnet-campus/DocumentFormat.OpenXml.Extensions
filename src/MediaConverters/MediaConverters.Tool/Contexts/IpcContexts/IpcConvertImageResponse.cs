namespace DotNetCampus.MediaConverters.Contexts.IpcContexts;

public class IpcConvertImageResponse
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? Message { get; set; }

    internal static IpcConvertImageResponse FromErrorCode(MediaConverterErrorCode errorCode)
    {
        return new IpcConvertImageResponse()
        {
            Code = errorCode.Code,
            Message = errorCode.Message
        };
    }
}