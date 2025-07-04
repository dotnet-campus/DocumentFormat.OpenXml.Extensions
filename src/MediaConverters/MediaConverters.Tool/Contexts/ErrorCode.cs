using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCampus.MediaConverter.Contexts;

internal readonly record struct ErrorCode
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
    /// 错误代码
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; init; }

    public ErrorCode(int code, string message)
    {
        Code = code;
        Message = message;

        CodeList ??= [];
        CodeList.Add(this);
    }

    private static List<ErrorCode>? CodeList { get; set; }
}
