using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("此类型仅供兼容代码使用，请勿直接调用。")]
    public abstract class LegacyUnit<T, TLegacy>
        where T : struct
        where TLegacy : LegacyUnit<T, TLegacy>
    {
        public static implicit operator T(LegacyUnit<T, TLegacy> legacyUnit)
        {
            var legacyValue = legacyUnit.GetType().GetProperty("Value", typeof(double))!.GetValue(legacyUnit);
            return (T) typeof(T).GetConstructor(new[] {typeof(double)})!.Invoke(null, new object[] {legacyValue});
        }
    }
}
