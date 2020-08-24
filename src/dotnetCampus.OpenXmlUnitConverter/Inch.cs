namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 英寸。
    /// <list type="bullet">
    /// <item>1 <see cref="Inch"/> = 914400 <see cref="Emu"/></item>
    /// <item>1 <see cref="Inch"/> = 2.54 <see cref="Cm"/></item>
    /// </list>
    /// </summary>
    public readonly struct Inch
    {
        /// <summary>
        /// 创建 <see cref="Inch"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Inch"/> 单位数值。</param>
        public Inch(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="Inch"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}