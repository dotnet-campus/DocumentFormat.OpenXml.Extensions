#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

using System;
using DocumentFormat.OpenXml;

namespace dotnetCampus.OpenXmlUnitConverter
{
    public static class OpenXmlUnitConverter
    {
        public static Int64Value ToOpenXmlInt64Value(this Emu emu)
        {
            return new Int64Value((long)Math.Round(emu.Value));
        }

        public static Int32Value ToOpenXmlInt32Value(this Emu emu)
        {
            return new Int32Value((int)Math.Round(emu.Value));
        }
    }
}