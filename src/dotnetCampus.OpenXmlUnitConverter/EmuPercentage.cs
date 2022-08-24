using System;
using DocumentFormat.OpenXml;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 用 Emu 表示的百分数
    /// </summary>
    public readonly struct EmuPercentage
    {
        /// <summary>
        /// 用 Emu 表示的百分数
        /// </summary>
        /// <param name="value"></param>
        public EmuPercentage(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 用 Emu 表示的百分数
        /// </summary>
        /// <param name="int32Value">支持传入带百分号的内容</param>
        public EmuPercentage(Int32Value int32Value) : this(int32Value.InnerText)
        {
        }

        internal EmuPercentage(string? percentageText)
        {
            if (!string.IsNullOrEmpty(percentageText))
            {
                if (int.TryParse(percentageText, out var intValue))
                {
                    Value = intValue;
                    return;
                }
                else
                {
                    // 如果是带了百分比的
                    if (percentageText!.Length > 1 && percentageText.EndsWith("%"))
                    {
#if NETCOREAPP3_1_OR_GREATER
                        var percentageSpan = percentageText.AsSpan().Slice(0, percentageText.Length - 1);
                        if (double.TryParse(percentageSpan, out var doubleValue))
#else
                        var newPercentageText = percentageText.Substring(0, percentageText.Length - 1);
                        if (double.TryParse(newPercentageText, out var doubleValue))
#endif
                        {
                            // 根据 OpenXml 规则，这里和像素百分比转换是 1000 倍
                            var pixelPercentageValue = doubleValue * 1000;

                            // 这里的百分比一定是 Pixel 的百分比级的，需要经过转换才能计算为 Emu 的百分比
                            // 为了将 PixelPercentageValue 转换为 EmuPercentage 类型，需要经过换算逻辑
                            var pixel = new Pixel(pixelPercentageValue);
                            var emuPercentageValue = pixel.ToEmu();

                            Value = emuPercentageValue.Value;
                            return;
                        }
                    }
                }
            }

            throw new ArgumentException($"Can not convert PercentageText={percentageText} to {nameof(EmuPercentage)} value.");
        }

        /// <summary>
        /// 用 Emu 表示的百分数
        /// </summary>
        public double Value { get; }
    }
}