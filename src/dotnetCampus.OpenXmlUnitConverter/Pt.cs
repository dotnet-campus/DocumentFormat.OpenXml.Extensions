namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 点（Point）。
    /// <para>1 <see cref="Pt"/> = 1/72 <see cref="Inch"/></para>
    /// </summary>
    public readonly struct Pt
    {
        /// <summary>
        /// 创建 <see cref="Pt"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Pt"/> 单位数值。</param>
        public Pt(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="Pt"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}