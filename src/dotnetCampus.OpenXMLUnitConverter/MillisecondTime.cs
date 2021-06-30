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
                Millisecond = long.MaxValue;
                IsIndefinite = true;
            }
            else if (long.TryParse(millisecond, out var value))
            {
                Millisecond = value;
                IsIndefinite = false;
            }
            else if (Enum.TryParse<IndefiniteTimeDeclarationValues>(millisecond, true, out var result)
                     && result == IndefiniteTimeDeclarationValues.Indefinite)
            {
                Millisecond = long.MaxValue;
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
                    Millisecond = long.MaxValue;
                    IsIndefinite = false;
                }
            }
        }

        /// <summary>
        /// 是否未定义（相当于无穷，也相当于空值）具体按照业务决定
        /// </summary>
        public bool IsIndefinite { get; }

        /// <summary>
        /// 以毫秒形式表示
        /// </summary>
        public long Millisecond { get; }

        /// <summary>
        /// 以ticks形式表示的时间 10000 Tick 是 1 毫秒
        /// </summary>
        public long ToTicks() => IsIndefinite
            ? throw new InvalidOperationException(
                $"The {nameof(MillisecondTime)} is indefinite. Can not convert to Tick.")
            : Millisecond * TicksPerMillisecond;

        /// <summary>
        /// 返回使用 <see cref="TimeSpan"/> 表示的时间
        /// </summary>
        public TimeSpan ToTimeSpan() => IsIndefinite
            ? throw new InvalidOperationException(
                $"The {nameof(MillisecondTime)} is indefinite. Can not convert to {nameof(TimeSpan)}.")
            : TimeSpan.FromMilliseconds(Millisecond);

        private const int TicksPerMillisecond = 10000;
    }
}