namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 表示 DXA 单位，点的二十分之一。主要用于指定页面尺寸，边距，制表符等。
    /// <para>1 <see cref="Dxa"/> = 1/20 <see cref="Pt"/></para>
    /// </summary>
    public readonly struct Dxa
    {
        /// <summary>
        /// 创建 <see cref="Dxa"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Dxa"/> 单位数值。</param>
        public Dxa(double value) => Value = value;

        /// <summary>
        /// 获取 <see cref="Dxa"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}