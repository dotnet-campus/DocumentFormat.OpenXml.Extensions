using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头：左弧形
    /// </summary>
    public class CurvedRightArrowGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
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
            //  <gd name="maxAdj2" fmla="*/ 50000 h ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="a1" fmla="pin 0 adj1 a2" />
            //  <gd name="th" fmla="*/ ss a1 100000" />
            //  <gd name="aw" fmla="*/ ss a2 100000" />
            //  <gd name="q1" fmla="+/ th aw 4" />
            //  <gd name="hR" fmla="+- hd2 0 q1" />
            //  <gd name="q7" fmla="*/ hR 2 1" />
            //  <gd name="q8" fmla="*/ q7 q7 1" />
            //  <gd name="q9" fmla="*/ th th 1" />
            //  <gd name="q10" fmla="+- q8 0 q9" />
            //  <gd name="q11" fmla="sqrt q10" />
            //  <gd name="idx" fmla="*/ q11 w q7" />
            //  <gd name="maxAdj3" fmla="*/ 100000 idx ss" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="ah" fmla="*/ ss a3 100000" />
            //  <gd name="y3" fmla="+- hR th 0" />
            //  <gd name="q2" fmla="*/ w w 1" />
            //  <gd name="q3" fmla="*/ ah ah 1" />
            //  <gd name="q4" fmla="+- q2 0 q3" />
            //  <gd name="q5" fmla="sqrt q4" />
            //  <gd name="dy" fmla="*/ q5 hR w" />
            //  <gd name="y5" fmla="+- hR dy 0" />
            //  <gd name="y7" fmla="+- y3 dy 0" />
            //  <gd name="q6" fmla="+- aw 0 th" />
            //  <gd name="dh" fmla="*/ q6 1 2" />
            //  <gd name="y4" fmla="+- y5 0 dh" />
            //  <gd name="y8" fmla="+- y7 dh 0" />
            //  <gd name="aw2" fmla="*/ aw 1 2" />
            //  <gd name="y6" fmla="+- b 0 aw2" />
            //  <gd name="x1" fmla="+- r 0 ah" />
            //  <gd name="swAng" fmla="at2 ah dy" />
            //  <gd name="stAng" fmla="+- cd2 0 swAng" />
            //  <gd name="mswAng" fmla="+- 0 0 swAng" />
            //  <gd name="ix" fmla="+- r 0 idx" />
            //  <gd name="iy" fmla="+/ hR y3 2" />
            //  <gd name="q12" fmla="*/ th 1 2" />
            //  <gd name="dang2" fmla="at2 idx q12" />
            //  <gd name="swAng2" fmla="+- dang2 0 cd4" />
            //  <gd name="swAng3" fmla="+- cd4 dang2 0" />
            //  <gd name="stAng3" fmla="+- cd2 0 dang2" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 50000 h ss" />
            var maxAdj2 = 50000 * h / ss;
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="a1" fmla="pin 0 adj1 a2" />
            var a1 = Pin(0, adj1, a2);
            //  <gd name="th" fmla="*/ ss a1 100000" />
            var th = ss * a1 / 100000;
            //  <gd name="aw" fmla="*/ ss a2 100000" />
            var aw = ss * a2 / 100000;
            //  <gd name="q1" fmla="+/ th aw 4" />
            var q1 = (th + aw) / 4;
            //  <gd name="hR" fmla="+- hd2 0 q1" />
            var hR = hd2 - q1;
            //  <gd name="q7" fmla="*/ hR 2 1" />
            var q7 = hR * 2 / 1;
            //  <gd name="q8" fmla="*/ q7 q7 1" />
            var q8 = q7 * q7 / 1;
            //  <gd name="q9" fmla="*/ th th 1" />
            var q9 = th * th / 1;
            //  <gd name="q10" fmla="+- q8 0 q9" />
            var q10 = q8 - q9;
            //  <gd name="q11" fmla="sqrt q10" />
            var q11 = System.Math.Sqrt(q10);
            //  <gd name="idx" fmla="*/ q11 w q7" />
            var idx = q11 * w / q7;
            //  <gd name="maxAdj3" fmla="*/ 100000 idx ss" />
            var maxAdj3 = 100000 * idx / ss;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="ah" fmla="*/ ss a3 100000" />
            var ah = ss * a3 / 100000;
            //  <gd name="y3" fmla="+- hR th 0" />
            var y3 = hR + th;
            //  <gd name="q2" fmla="*/ w w 1" />
            var q2 = w * w / 1;
            //  <gd name="q3" fmla="*/ ah ah 1" />
            var q3 = ah * ah / 1;
            //  <gd name="q4" fmla="+- q2 0 q3" />
            var q4 = q2 - q3;
            //  <gd name="q5" fmla="sqrt q4" />
            var q5 = System.Math.Sqrt(q4);
            //  <gd name="dy" fmla="*/ q5 hR w" />
            var dy = q5 * hR / w;
            //  <gd name="y5" fmla="+- hR dy 0" />
            var y5 = hR + dy;
            //  <gd name="y7" fmla="+- y3 dy 0" />
            var y7 = y3 + dy;
            //  <gd name="q6" fmla="+- aw 0 th" />
            var q6 = aw - th;
            //  <gd name="dh" fmla="*/ q6 1 2" />
            var dh = q6 * 1 / 2;
            //  <gd name="y4" fmla="+- y5 0 dh" />
            var y4 = y5 - dh;
            //  <gd name="y8" fmla="+- y7 dh 0" />
            var y8 = y7 + dh;
            //  <gd name="aw2" fmla="*/ aw 1 2" />
            var aw2 = aw * 1 / 2;
            //  <gd name="y6" fmla="+- b 0 aw2" />
            var y6 = b - aw2;
            //  <gd name="x1" fmla="+- r 0 ah" />
            var x1 = r - ah;
            //  <gd name="swAng" fmla="at2 ah dy" />
            var swAng = ATan2(ah, dy);
            //  <gd name="stAng" fmla="+- cd2 0 swAng" />
            var stAng = cd2 - swAng;
            //  <gd name="mswAng" fmla="+- 0 0 swAng" />
            var mswAng = 0 - swAng;
            //  <gd name="ix" fmla="+- r 0 idx" />
            var ix = r - idx;
            //  <gd name="iy" fmla="+/ hR y3 2" />
            var iy = (hR + y3) / 2;
            //  <gd name="q12" fmla="*/ th 1 2" />
            var q12 = th * 1 / 2;
            //  <gd name="dang2" fmla="at2 idx q12" />
            var dang2 = ATan2(idx, q12);
            //  <gd name="swAng2" fmla="+- dang2 0 cd4" />
            var swAng2 = dang2 - cd4;
            //  <gd name="swAng3" fmla="+- cd4 dang2 0" />
            var swAng3 = cd4 + dang2;
            //  <gd name="stAng3" fmla="+- cd2 0 dang2" />
            var stAng3 = cd2 - dang2;




            //  <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //    <path stroke="false" extrusionOk="false">
            //      <moveTo>
            //        <pt x="l" y="hR" />
            //      </moveTo>
            //      <arcTo wR="w" hR="hR" stAng="cd2" swAng="mswAng" />
            //      <lnTo>
            //        <pt x="x1" y="y4" />
            //      </lnTo>
            //      <lnTo>
            //        <pt x="r" y="y6" />
            //      </lnTo>
            //      <lnTo>
            //        <pt x="x1" y="y8" />
            //      </lnTo>
            //      <lnTo>
            //        <pt x="x1" y="y7" />
            //      </lnTo>
            //      <arcTo wR="w" hR="hR" stAng="stAng" swAng="swAng" />
            //      <close />
            //    </path>
            //    <path fill="darkenLess" stroke="false" extrusionOk="false">
            //      <moveTo>
            //        <pt x="r" y="th" />
            //      </moveTo>
            //      <arcTo wR="w" hR="hR" stAng="3cd4" swAng="swAng2" />
            //      <arcTo wR="w" hR="hR" stAng="stAng3" swAng="swAng3" />
            //      <close />
            //    </path>
            //    <path fill="none" extrusionOk="false">
            //      <moveTo>
            //        <pt x="l" y="hR" />
            //      </moveTo>
            //      <arcTo wR="w" hR="hR" stAng="cd2" swAng="mswAng" />
            //      <lnTo>
            //        <pt x="x1" y="y4" />
            //      </lnTo>
            //      <lnTo>
            //        <pt x="r" y="y6" />
            //      </lnTo>
            //      <lnTo>
            //        <pt x="x1" y="y8" />
            //      </lnTo>
            //      <lnTo>
            //        <pt x="x1" y="y7" />
            //      </lnTo>
            //      <arcTo wR="w" hR="hR" stAng="stAng" swAng="swAng" />
            //      <lnTo>
            //        <pt x="l" y="hR" />
            //      </lnTo>
            //      <arcTo wR="w" hR="hR" stAng="cd2" swAng="cd4" />
            //      <lnTo>
            //        <pt x="r" y="th" />
            //      </lnTo>
            //      <arcTo wR="w" hR="hR" stAng="3cd4" swAng="swAng2" />
            //    </path>
            //  </pathLst>
            //</curvedRightArrow>


            var shapePaths = new ShapePath[3];
            //    <path stroke="false" extrusionOk="false">
            //      <moveTo>
            //        <pt x="l" y="hR" />
            //      </moveTo>
            var currentPoint = new EmuPoint(l, hR);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //      <arcTo wR="w" hR="hR" stAng="cd2" swAng="mswAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, cd2, mswAng);
            //      <lnTo>
            //        <pt x="x1" y="y4" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //      <lnTo>
            //        <pt x="r" y="y6" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, r, y6);
            //      <lnTo>
            //        <pt x="x1" y="y8" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, x1, y8);
            //      <lnTo>
            //        <pt x="x1" y="y7" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, x1, y7);
            //      <arcTo wR="w" hR="hR" stAng="stAng" swAng="swAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, stAng, swAng);
            //      <close />
            stringPath.Append(" z");
            //    </path>
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            //    <path fill="darkenLess" stroke="false" extrusionOk="false">
            //      <moveTo>
            //        <pt x="r" y="th" />
            //      </moveTo>
            currentPoint = new EmuPoint(r, th);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //      <arcTo wR="w" hR="hR" stAng="3cd4" swAng="swAng2" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, 3 * cd4, swAng2);
            //      <arcTo wR="w" hR="hR" stAng="stAng3" swAng="swAng3" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, stAng3, swAng3);
            //      <close />
            stringPath.Append(" z");
            //    </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            //    <path fill="none" extrusionOk="false">
            //      <moveTo>
            //        <pt x="l" y="hR" />
            //      </moveTo>
            currentPoint = new EmuPoint(l, hR);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //      <arcTo wR="w" hR="hR" stAng="cd2" swAng="mswAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, cd2, mswAng);
            //      <lnTo>
            //        <pt x="x1" y="y4" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //      <lnTo>
            //        <pt x="r" y="y6" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, r, y6);
            //      <lnTo>
            //        <pt x="x1" y="y8" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, x1, y8);
            //      <lnTo>
            //        <pt x="x1" y="y7" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, x1, y7);
            //      <arcTo wR="w" hR="hR" stAng="stAng" swAng="swAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, stAng, swAng);
            //      <lnTo>
            //        <pt x="l" y="hR" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, l, hR);
            //      <arcTo wR="w" hR="hR" stAng="cd2" swAng="cd4" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, cd2, cd4);
            //      <lnTo>
            //        <pt x="r" y="th" />
            //      </lnTo>
            currentPoint = LineToToString(stringPath, r, th);
            //      <arcTo wR="w" hR="hR" stAng="3cd4" swAng="swAng2" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, hR, 3 * cd4, swAng2);
            //    </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }
}
