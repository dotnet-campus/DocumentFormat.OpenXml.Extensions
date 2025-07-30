using System.Collections.Generic;
using ErrorCode = DotNetCampus.MediaConverters.Contexts.MediaConverterErrorCode;

namespace DotNetCampus.MediaConverters.Contexts;

/// <summary>
/// 多媒体转换过程的错误代码
/// </summary>
public readonly record struct MediaConverterErrorCode
{
    /// <summary>
    /// 成功
    /// </summary>
    public static readonly ErrorCode Success = new(0, "Success");

    /// <summary>
    /// 未知错误
    /// </summary>
    public static readonly ErrorCode UnknownError = new(-1, "Unknown error");

    /// <summary>
    /// 未知的图片格式
    /// </summary>
    public static readonly ErrorCode UnknownImageFormat = new(1001, "Unknown image format");

    /// <summary>
    /// 图片内容错误
    /// </summary>
    public static readonly ErrorCode InvalidImageContent = new(1002, "Invalid image content");

    /// <summary>
    /// 找不到图片文件
    /// </summary>
    public static readonly ErrorCode ImageFileNotFound = new(1003, "Image file not found");

    /// <summary>
    /// 不支持的转换
    /// </summary>
    public static readonly ErrorCode NotSupported = new(1004, "Not supported operation");

    public static readonly ErrorCode GdiException = new(1005, "Gdi exception");

    public static readonly ErrorCode UnknownException = new(1006, "Unknown exception");

    /// <summary>
    /// 错误代码
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; init; }


    public MediaConverterErrorCode(int code, string message)
    {
        Code = code;
        Message = message;

        CodeList ??= [];
        CodeList.Add(this);
    }

    private static List<ErrorCode>? CodeList { get; set; }

    public static bool TryGetErrorCode(int code, out ErrorCode errorCode)
    {
        if (CodeList is null)
        {
            errorCode = UnknownError;
            return false;
        }

        var index = CodeList.FindIndex(t => t.Code == code);
        if (index < 0)
        {
            errorCode = UnknownError;
            return false;
        }

        errorCode = CodeList[index];
        return true;
    }

    public static implicit operator int(ErrorCode errorCode)
    {
        return errorCode.Code;
    }
}