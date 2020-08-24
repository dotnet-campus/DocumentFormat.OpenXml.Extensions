namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 100倍的磅
    /// </summary>
    public readonly struct PoundHundredfold
    {
        /// <summary>
        /// 100倍的磅
        /// </summary>
        /// <param name="value"></param>
        public PoundHundredfold(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 100倍的磅
        /// </summary>
        public double Value { get; }
    }
}