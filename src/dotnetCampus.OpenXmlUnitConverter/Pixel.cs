namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 像素
    /// </summary>
    public readonly struct Pixel
    {
        /// <summary>
        /// 像素
        /// </summary>
        /// <param name="value"></param>
        public Pixel(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 像素
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// 表示值是 0 的像素
        /// </summary>
        public static readonly Pixel ZeroPixel = new Pixel(0);
    }
}