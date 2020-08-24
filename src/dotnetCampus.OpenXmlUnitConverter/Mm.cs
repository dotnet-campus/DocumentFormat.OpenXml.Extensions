namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 毫米
    /// </summary>
    public readonly struct Mm
    {
        public Mm(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}