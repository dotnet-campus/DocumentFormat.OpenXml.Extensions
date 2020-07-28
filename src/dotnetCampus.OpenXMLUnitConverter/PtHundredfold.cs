namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 100 倍的点
    /// </summary>
    public readonly struct PtHundredfold
    {
        /// <summary>
        /// 100 倍的点
        /// </summary>
        /// <param name="value"></param>
        public PtHundredfold(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 100 倍的点
        /// </summary>
        public double Value { get; }
    }
}