namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 毫米。
    /// <para>1 <see cref="Mm"/> = 1/10 <see cref="Cm"/></para>
    /// </summary>
    public readonly struct Mm
    {
        /// <summary>
        /// 创建 <see cref="Mm"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Mm"/> 单位数值。</param>
        public Mm(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="Mm"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}