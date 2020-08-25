using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class PtHundredfold : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.PtHundredfold, PtHundredfold>
    {
        public PtHundredfold(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static implicit operator PtHundredfold(dotnetCampus.OpenXmlUnitConverter.PtHundredfold newUnit)
        {
            return new PtHundredfold(newUnit.Value);
        }
    }
}