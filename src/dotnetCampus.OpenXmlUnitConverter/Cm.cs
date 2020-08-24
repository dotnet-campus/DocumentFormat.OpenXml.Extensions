namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 厘米。
    /// <list type="bullet">
    /// <item>1 <see cref="Cm"/> = 360000 <see cref="Emu"/></item>
    /// <item>1 <see cref="Inch"/> = 2.54 <see cref="Cm"/></item>
    /// </list>
    /// </summary>
    public readonly struct Cm
    {
        /// <summary>
        /// 创建 <see cref="Cm"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Cm"/> 单位数值。</param>
        public Cm(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="Cm"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}