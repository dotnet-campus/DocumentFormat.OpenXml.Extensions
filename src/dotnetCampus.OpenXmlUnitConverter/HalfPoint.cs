namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 半点 用来表示字体大小的半点，一个点等于两个半点
    /// </summary>
    public readonly struct HalfPoint
    {
        public HalfPoint(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}