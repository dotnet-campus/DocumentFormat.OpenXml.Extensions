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
        public double Value { get; }
    }
}