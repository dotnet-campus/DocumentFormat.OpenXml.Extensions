using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Emu : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.Emu, Emu>
    {
        public Emu(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static readonly Emu Zero = new Emu(0);

        public static implicit operator Emu(dotnetCampus.OpenXmlUnitConverter.Emu newUnit)
        {
            return new Emu(newUnit.Value);
        }
    }
}
