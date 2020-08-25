namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 半点。用来表示字体大小。
    /// <para>1 <see cref="HalfPoint"/> = 1/2 <see cref="Pt"/></para>
    /// </summary>
    public readonly struct HalfPoint
    {
        /// <summary>
        /// 创建 <see cref="HalfPoint"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="HalfPoint"/> 单位数值。</param>
        public HalfPoint(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="HalfPoint"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}