namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 表示dxa单位 二十分之一点 ，这是默认单位，是像素的20倍
    /// </summary>
    public readonly struct Dxa
    {
        public Dxa(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}