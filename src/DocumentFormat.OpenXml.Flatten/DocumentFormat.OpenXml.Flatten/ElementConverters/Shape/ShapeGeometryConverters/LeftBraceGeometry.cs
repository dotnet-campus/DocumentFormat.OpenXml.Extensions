﻿using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 左大括号
    /// </summary>
    public class LeftBraceGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string? ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 8333" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 8333d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //当adj2为最低0和最高值100000或者adj1为0，导致一些值为0，参与公式乘除运算，导致路径有误
            if (adj1 <= 0)
            {
                adj1 = System.Math.Max(adj1, 0.1);
            }
            if (adj2 <= 0)
                adj2 = System.Math.Max(adj2, 0.1);
            else if (adj2 >= 100000)
                adj2 = System.Math.Min(adj2, 99999);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a2" fmla="pin 0 adj2 100000" />
            //  <gd name="q1" fmla="+- 100000 0 a2" />
            //  <gd name="q2" fmla="min q1 a2" />
            //  <gd name="q3" fmla="*/ q2 1 2" />
            //  <gd name="maxAdj1" fmla="*/ q3 h ss" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="y1" fmla="*/ ss a1 100000" />
            //  <gd name="y3" fmla="*/ h a2 100000" />
            //  <gd name="y4" fmla="+- y3 y1 0" />
            //  <gd name="dx1" fmla="cos wd2 2700000" />
            //  <gd name="dy1" fmla="sin y1 2700000" />
            //  <gd name="il" fmla="+- r 0 dx1" />
            //  <gd name="it" fmla="+- y1 0 dy1" />
            //  <gd name="ib" fmla="+- b dy1 y1" />
            //</gdLst>

            //  <gd name="a2" fmla="pin 0 adj2 100000" />
            var a2 = Pin(0, adj2, 100000);
            //  <gd name="q1" fmla="+- 100000 0 a2" />
            var q1 = 100000 - a2;
            //  <gd name="q2" fmla="min q1 a2" />
            var q2 = System.Math.Min(q1, a2);
            //  <gd name="q3" fmla="*/ q2 1 2" />
            var q3 = q2 * 1 / 2;
            //  <gd name="maxAdj1" fmla="*/ q3 h ss" />
            var maxAdj1 = q3 * h / ss;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="y1" fmla="*/ ss a1 100000" />
            var y1 = ss * a1 / 100000;
            //  <gd name="y3" fmla="*/ h a2 100000" />
            var y3 = h * a2 / 100000;
            //  <gd name="y4" fmla="+- y3 y1 0" />
            var y4 = y3 + y1;
            //  <gd name="dx1" fmla="cos wd2 2700000" />
            var dx1 = Cos(wd2, 2700000);
            //  <gd name="dy1" fmla="sin y1 2700000" />
            var dy1 = Sin(y1, 2700000);
            //  <gd name="il" fmla="+- r 0 dx1" />
            var il = r - dx1;
            //  <gd name="it" fmla="+- y1 0 dy1" />
            var it = y1 - dy1;
            //  <gd name="ib" fmla="+- b dy1 y1" />
            var ib = b + dy1 - y1;

            //  <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="hc" y="y4" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="-5400000" />
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="hc" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd4" />
            //    <close />
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="hc" y="y4" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="-5400000" />
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="hc" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd4" />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[2];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(r, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="cd4" />
            var wR = wd2;
            var hR = y1;
            var stAng = cd4;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="hc" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, y4);
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="-5400000" />
            wR = wd2;
            hR = y1;
            stAng = 0;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="-5400000" />
            wR = wd2;
            hR = y1;
            stAng = cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="hc" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, y1);
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd4" />
            wR = wd2;
            hR = y1;
            stAng = cd2;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            // 这是特别设置 PathFillModeValues 的值，用于给图片裁剪使用的。图片裁剪为形状需要此优化
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, isStroke: false);

            //  <path fill="none">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(r, b);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="cd4" />
            wR = wd2;
            hR = y1;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="hc" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, y4);
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="-5400000" />
            wR = wd2;
            hR = y1;
            stAng = 0;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="y1" stAng="cd4" swAng="-5400000" />
            wR = wd2;
            hR = y1;
            stAng = cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="hc" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, y1);
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd4" />
            wR = wd2;
            hR = y1;
            stAng = cd2;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //  </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="il" t="it" r="r" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, r, ib);

            return shapePaths;
        }
    }
}
