using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Mm : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.Mm, Mm>
    {
        public Mm(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static implicit operator Mm(dotnetCampus.OpenXmlUnitConverter.Mm newUnit)
        {
            return new Mm(newUnit.Value);
        }
    }
}
