using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class PoundHundredfold : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.PoundHundredfold, PoundHundredfold>
    {
        public PoundHundredfold(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}