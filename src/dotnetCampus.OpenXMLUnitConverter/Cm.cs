namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 厘米
    /// </summary>
    public readonly struct Cm
    {
        /// <summary>
        /// 厘米
        /// </summary>
        /// <param name="value"></param>
        public Cm(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 厘米
        /// </summary>
        public double Value { get; }
    }
}