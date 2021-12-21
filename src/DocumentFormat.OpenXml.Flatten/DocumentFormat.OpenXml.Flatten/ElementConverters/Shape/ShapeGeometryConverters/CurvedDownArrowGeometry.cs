using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头: 上弧形
    /// </summary>
    public class CurvedDownArrowGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 50000" />
            //  <gd name="adj3" fmla="val 25000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 50000 w ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="th" fmla="*/ ss a1 100000" />
            //  <gd name="aw" fmla="*/ ss a2 100000" />
            //  <gd name="q1" fmla="+/ th aw 4" />
            //  <gd name="wR" fmla="+- wd2 0 q1" />
            //  <gd name="q7" fmla="*/ wR 2 1" />
            //  <gd name="q8" fmla="*/ q7 q7 1" />
            //  <gd name="q9" fmla="*/ th th 1" />
            //  <gd name="q10" fmla="+- q8 0 q9" />
            //  <gd name="q11" fmla="sqrt q10" />
            //  <gd name="idy" fmla="*/ q11 h q7" />
            //  <gd name="maxAdj3" fmla="*/ 100000 idy ss" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="ah" fmla="*/ ss adj3 100000" />
            //  <gd name="x3" fmla="+- wR th 0" />
            //  <gd name="q2" fmla="*/ h h 1" />
            //  <gd name="q3" fmla="*/ ah ah 1" />
            //  <gd name="q4" fmla="+- q2 0 q3" />
            //  <gd name="q5" fmla="sqrt q4" />
            //  <gd name="dx" fmla="*/ q5 wR h" />
            //  <gd name="x5" fmla="+- wR dx 0" />
            //  <gd name="x7" fmla="+- x3 dx 0" />
            //  <gd name="q6" fmla="+- aw 0 th" />
            //  <gd name="dh" fmla="*/ q6 1 2" />
            //  <gd name="x4" fmla="+- x5 0 dh" />
            //  <gd name="x8" fmla="+- x7 dh 0" />
            //  <gd name="aw2" fmla="*/ aw 1 2" />
            //  <gd name="x6" fmla="+- r 0 aw2" />
            //  <gd name="y1" fmla="+- b 0 ah" />
            //  <gd name="swAng" fmla="at2 ah dx" />
            //  <gd name="mswAng" fmla="+- 0 0 swAng" />
            //  <gd name="iy" fmla="+- b 0 idy" />
            //  <gd name="ix" fmla="+/ wR x3 2" />
            //  <gd name="q12" fmla="*/ th 1 2" />
            //  <gd name="dang2" fmla="at2 idy q12" />
            //  <gd name="stAng" fmla="+- 3cd4 swAng 0" />
            //  <gd name="stAng2" fmla="+- 3cd4 0 dang2" />
            //  <gd name="swAng2" fmla="+- dang2 0 cd4" />
            //  <gd name="swAng3" fmla="+- cd4 dang2 0" />
            //</gdLst>


            //  <gd name="maxAdj2" fmla="*/ 50000 w ss" />
            var maxAdj2 = 50000 * w / ss;
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            var a1 = Pin(0, adj1, 100000);
            //  <gd name="th" fmla="*/ ss a1 100000" />
            var th = ss * a1 / 100000;
            //  <gd name="aw" fmla="*/ ss a2 100000" />
            var aw = ss * a2 / 100000;
            //  <gd name="q1" fmla="+/ th aw 4" />
            var q1 = (th + aw) / 4;
            //  <gd name="wR" fmla="+- wd2 0 q1" />
            var wR = wd2 - q1;
            //  <gd name="q7" fmla="*/ wR 2 1" />
            var q7 = wR * 2 / 1;
            //  <gd name="q8" fmla="*/ q7 q7 1" />
            var q8 = q7 * q7 / 1;
            //  <gd name="q9" fmla="*/ th th 1" />
            var q9 = th * th / 1;
            //  <gd name="q10" fmla="+- q8 0 q9" />
            var q10 = q8 - q9;
            //  <gd name="q11" fmla="sqrt q10" />
            var q11 = System.Math.Sqrt(q10);
            //  <gd name="idy" fmla="*/ q11 h q7" />
            var idy = q11 * h / q7;
            //  <gd name="maxAdj3" fmla="*/ 100000 idy ss" />
            var maxAdj3 = 100000 * idy / ss;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="ah" fmla="*/ ss adj3 100000" />
            var ah = ss * adj3 / 100000;
            //  <gd name="x3" fmla="+- wR th 0" />
            var x3 = wR + th;
            //  <gd name="q2" fmla="*/ h h 1" />
            var q2 = h * h / 1;
            //  <gd name="q3" fmla="*/ ah ah 1" />
            var q3 = ah * ah / 1;
            //  <gd name="q4" fmla="+- q2 0 q3" />
            var q4 = q2 - q3;
            //  <gd name="q5" fmla="sqrt q4" />
            var q5 = System.Math.Sqrt(q4);
            //  <gd name="dx" fmla="*/ q5 wR h" />
            var dx = q5 * wR / h;
            //  <gd name="x5" fmla="+- wR dx 0" />
            var x5 = wR + dx;
            //  <gd name="x7" fmla="+- x3 dx 0" />
            var x7 = x3 + dx;
            //  <gd name="q6" fmla="+- aw 0 th" />
            var q6 = aw - th;
            //  <gd name="dh" fmla="*/ q6 1 2" />
            var dh = q6 * 1 / 2;
            //  <gd name="x4" fmla="+- x5 0 dh" />
            var x4 = x5 - dh;
            //  <gd name="x8" fmla="+- x7 dh 0" />
            var x8 = x7 + dh;
            //  <gd name="aw2" fmla="*/ aw 1 2" />
            var aw2 = aw * 1 / 2;
            //  <gd name="x6" fmla="+- r 0 aw2" />
            var x6 = r - aw2;
            //  <gd name="y1" fmla="+- b 0 ah" />
            var y1 = b - ah;
            //  <gd name="swAng" fmla="at2 ah dx" />
            var swAng = ATan2(ah, dx);
            //  <gd name="mswAng" fmla="+- 0 0 swAng" />
            var mswAng = 0 - swAng;
            //  <gd name="iy" fmla="+- b 0 idy" />
            var iy = b - idy;
            //  <gd name="ix" fmla="+/ wR x3 2" />
            var ix = (wR + x3) / 2;
            //  <gd name="q12" fmla="*/ th 1 2" />
            var q12 = th * 1 / 2;
            //  <gd name="dang2" fmla="at2 idy q12" />
            var dang2 = ATan2(idy, q12);
            //  <gd name="stAng" fmla="+- 3cd4 swAng 0" />
            var stAng = 3 * cd4 + swAng;
            //  <gd name="stAng2" fmla="+- 3cd4 0 dang2" />
            var stAng2 = 3 * cd4 - dang2;
            //  <gd name="swAng2" fmla="+- dang2 0 cd4" />
            var swAng2 = dang2 - cd4;
            //  <gd name="swAng3" fmla="+- cd4 dang2 0" />
            var swAng3 = cd4 + dang2;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x6" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wR" hR="h" stAng="stAng" swAng="mswAng" />
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <arcTo wR="wR" hR="h" stAng="3cd4" swAng="swAng" />
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="darkenLess" stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ix" y="iy" />
            //    </moveTo>
            //    <arcTo wR="wR" hR="h" stAng="stAng2" swAng="swAng2" />
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <arcTo wR="wR" hR="h" stAng="cd2" swAng="swAng3" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ix" y="iy" />
            //    </moveTo>
            //    <arcTo wR="wR" hR="h" stAng="stAng2" swAng="swAng2" />
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <arcTo wR="wR" hR="h" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <arcTo wR="wR" hR="h" stAng="3cd4" swAng="swAng" />
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wR" hR="h" stAng="stAng" swAng="mswAng" />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[3];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x6" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x6, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y1);
            //    <arcTo wR="wR" hR="h" stAng="stAng" swAng="mswAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, stAng, mswAng);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //    <arcTo wR="wR" hR="h" stAng="3cd4" swAng="swAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, 3 * cd4, swAng);
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y1);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);



            //  <path fill="darkenLess" stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ix" y="iy" />
            //    </moveTo>
            currentPoint = new EmuPoint(ix, iy);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wR" hR="h" stAng="stAng2" swAng="swAng2" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, stAng2, swAng2);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <arcTo wR="wR" hR="h" stAng="cd2" swAng="swAng3" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, cd2, swAng3);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ix" y="iy" />
            //    </moveTo>
            currentPoint = new EmuPoint(ix, iy);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wR" hR="h" stAng="stAng2" swAng="swAng2" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, stAng2, swAng2);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <arcTo wR="wR" hR="h" stAng="cd2" swAng="cd4" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, cd2, cd4);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //    <arcTo wR="wR" hR="h" stAng="3cd4" swAng="swAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, 3 * cd4, swAng);
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y1);
            //    <lnTo>
            //      <pt x="x6" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x6, b);
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y1);
            //    <arcTo wR="wR" hR="h" stAng="stAng" swAng="mswAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, wR, h, stAng, mswAng);
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            // <rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;

        }
    }
}
