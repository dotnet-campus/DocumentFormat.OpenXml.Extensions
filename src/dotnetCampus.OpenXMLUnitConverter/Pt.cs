namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 点
    /// </summary>
    public readonly struct Pt
    {
        public Pt(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 像素点的值
        /// </summary>
        public double Value { get; }
    }
}