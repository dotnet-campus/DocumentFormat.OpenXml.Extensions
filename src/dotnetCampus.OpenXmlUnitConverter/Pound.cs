namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 磅
    /// </summary>
    public readonly struct Pound
    {
        /// <summary>
        /// 磅
        /// </summary>
        /// <param name="value"></param>
        public Pound(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 磅
        /// </summary>
        public double Value { get; }
    }
}