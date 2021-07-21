using System;
using System.Diagnostics;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Presentation;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 定义ppt中的时间单位（毫秒）
    /// </summary>
    public readonly struct MillisecondTime
    {
        /// <summary>
        /// 创建 PPT 中的时间单位
        /// </summary>
        /// <param name="millisecond"></param>
        public MillisecondTime(StringValue? millisecond)
        {
            if (millisecond is null)
            {
                Milliseconds = long.MaxValue;
                IsIndefinite = true;
            }
            else if (long.TryParse(millisecond, out var value))
            {
                Milliseconds = value;
                IsIndefinite = false;
            }
            else if (Enum.TryParse<IndefiniteTimeDeclarationValues>(millisecond, true, out var result)
                     && result == IndefiniteTimeDeclarationValues.Indefinite)
            {
                Milliseconds = long.MaxValue;
                IsIndefinite = true;
            }
            else
            {
                if (Debugger.IsAttached)
                {
                    throw new ArgumentException($"输入参数不合法 MillisecondString={millisecond}", nameof(millisecond));
                }
                else
                {
                    Milliseconds = long.MaxValue;
                    IsIndefinite = false;
                }
            }
        }

        private MillisecondTime(long milliseconds)
        {
            Milliseconds = milliseconds;
            IsIndefinite = false;
        }

        /// <summary>
        /// 是否未定义（相当于无穷，也相当于空值）具体按照业务决定
        /// </summary>
        public bool IsIndefinite { get; }

        /// <summary>
        /// 以毫秒形式表示
        /// </summary>
        public long Milliseconds { get; }

        /// <summary>
        /// 从 <paramref name="milliseconds"/> 毫秒创建
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static MillisecondTime FromMilliseconds(long milliseconds)
        {
            return new MillisecondTime(milliseconds);
        }

        /// <summary>
        /// 以ticks形式表示的时间 10000 Tick 是 1 毫秒
        /// </summary>
        public long ToTicks() => IsIndefinite
            ? throw new InvalidOperationException(
                $"The {nameof(MillisecondTime)} is indefinite. Can not convert to Tick.")
            : Milliseconds * TicksPerMillisecond;

        /// <summary>
        /// 返回使用 <see cref="TimeSpan"/> 表示的时间
        /// </summary>
        public TimeSpan ToTimeSpan() => IsIndefinite
            ? throw new InvalidOperationException(
                $"The {nameof(MillisecondTime)} is indefinite. Can not convert to {nameof(TimeSpan)}.")
            : TimeSpan.FromMilliseconds(Milliseconds);

        private const int TicksPerMillisecond = 10000;
    }
}