using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Mm : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.Mm, Mm>
    {
        /// <summary>
        /// 创建 <see cref="Mm"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Mm"/> 单位数值。</param>
        public Mm(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="Mm"/> 单位数值。
        /// </summary>
        public double Value { get; }
    }
}