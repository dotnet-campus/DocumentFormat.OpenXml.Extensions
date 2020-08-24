using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class PixelPercentage : Percentage
    {
        public PixelPercentage(int value) : base(value)
        {
        }

        public static implicit operator PixelPercentage(dotnetCampus.OpenXmlUnitConverter.PixelPercentage newUnit)
        {
            return new PixelPercentage(newUnit.IntValue);
        }
    }
}
