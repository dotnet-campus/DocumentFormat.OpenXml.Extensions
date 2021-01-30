using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class EmuPercentage : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.EmuPercentage, EmuPercentage>
    {
        public EmuPercentage(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static implicit operator EmuPercentage(dotnetCampus.OpenXmlUnitConverter.EmuPercentage newUnit)
        {
            return new EmuPercentage(newUnit.Value);
        }
    }
}
