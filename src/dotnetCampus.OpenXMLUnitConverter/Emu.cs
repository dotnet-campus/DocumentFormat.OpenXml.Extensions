namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 最常用的单位
    /// </summary>
    public readonly struct Emu
    {
        /// <summary>
        /// Office 特有单位
        /// </summary>
        /// <param name="value"></param>
        public Emu(double value)
        {
            Value = value;
        }

        /// <summary>
        /// EMU 的值
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// 表示值为 0 的 EMU 的值
        /// </summary>
        public static readonly Emu Zero = new Emu(0);
    }
}