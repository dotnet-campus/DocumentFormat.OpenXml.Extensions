namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 像素百分数
    /// </summary>
    public class PixelPercentage : Percentage
    {
        /// <inheritdoc />
        public PixelPercentage(int value) : base(value)
        {
        }

        public static implicit operator PixelPercentage(dotnetCampus.OpenXmlUnitConverter.PixelPercentage newUnit)
        {
            return new PixelPercentage(newUnit.IntValue);
        }
    }
}