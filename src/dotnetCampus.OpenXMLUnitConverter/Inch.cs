namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 英寸
    /// </summary>
    public readonly struct Inch
    {
        /// <summary>
        /// 英寸
        /// </summary>
        /// <param name="value"></param>
        public Inch(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 英寸
        /// </summary>
        public double Value { get; }
    }
}