using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 禁止符
    /// </summary>
    public class NoSmokingGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 18750" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 18750d;

            // <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="dr" fmla="*/ ss a 100000" />
            //  <gd name="iwd2" fmla="+- wd2 0 dr" />
            //  <gd name="ihd2" fmla="+- hd2 0 dr" />
            //  <gd name="ang" fmla="at2 w h" />
            //  <gd name="ct" fmla="cos ihd2 ang" />
            //  <gd name="st" fmla="sin iwd2 ang" />
            //  <gd name="m" fmla="mod ct st 0" />
            //  <gd name="n" fmla="*/ iwd2 ihd2 m" />
            //  <gd name="drd2" fmla="*/ dr 1 2" />
            //  <gd name="dang" fmla="at2 n drd2" />
            //  <gd name="2dang" fmla="*/ dang 2 1" />
            //  <gd name="swAng" fmla="+- -10800000 2dang 0" />
            //  <gd name="t3" fmla="at2 w h" />
            //  <gd name="stAng1" fmla="+- t3 0 dang" />
            //  <gd name="stAng2" fmla="+- stAng1 0 cd2" />
            //  <gd name="ct1" fmla="cos ihd2 stAng1" />
            //  <gd name="st1" fmla="sin iwd2 stAng1" />
            //  <gd name="m1" fmla="mod ct1 st1 0" />
            //  <gd name="n1" fmla="*/ iwd2 ihd2 m1" />
            //  <gd name="dx1" fmla="cos n1 stAng1" />
            //  <gd name="dy1" fmla="sin n1 stAng1" />
            //  <gd name="x1" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc dy1 0" />
            //  <gd name="x2" fmla="+- hc 0 dx1" />
            //  <gd name="y2" fmla="+- vc 0 dy1" />
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>


            //  <gd name="a" fmla="pin 0 adj 50000" />
            var a = Pin(0, adj, 50000);
            //  <gd name="dr" fmla="*/ ss a 100000" />
            var dr = ss * a / 100000;
            //  <gd name="iwd2" fmla="+- wd2 0 dr" />
            var iwd2 = wd2 - dr;
            //  <gd name="ihd2" fmla="+- hd2 0 dr" />
            var ihd2 = hd2 - dr;
            //  <gd name="ang" fmla="at2 w h" />
            var ang = ATan2(w, h);
            //  <gd name="ct" fmla="cos ihd2 ang" />
            var ct = Cos(ihd2, (int) ang);
            //  <gd name="st" fmla="sin iwd2 ang" />
            var st = Sin(iwd2, (int) ang);
            //  <gd name="m" fmla="mod ct st 0" />
            var m = Mod(ct, st, 0);
            //  <gd name="n" fmla="*/ iwd2 ihd2 m" />
            var n = iwd2 * ihd2 / m;
            //  <gd name="drd2" fmla="*/ dr 1 2" />
            var drd2 = dr * 1 / 2;
            //  <gd name="dang" fmla="at2 n drd2" />
            var dang = ATan2(n, drd2);
            //  <gd name="2dang" fmla="*/ dang 2 1" />
            var _2dang = dang * 2 / 1;
            //  <gd name="swAng" fmla="+- -10800000 2dang 0" />
            var swAng = -10800000 + _2dang;
            //  <gd name="t3" fmla="at2 w h" />
            var t3 = ATan2(w, h);
            //  <gd name="stAng1" fmla="+- t3 0 dang" />
            var stAng1 = t3 - dang;
            //  <gd name="stAng2" fmla="+- stAng1 0 cd2" />
            var stAng2 = stAng1 - cd2;
            //  <gd name="ct1" fmla="cos ihd2 stAng1" />
            var ct1 = Cos(ihd2, (int) stAng1);
            //  <gd name="st1" fmla="sin iwd2 stAng1" />
            var st1 = Sin(iwd2, (int) stAng1);
            //  <gd name="m1" fmla="mod ct1 st1 0" />
            var m1 = Mod(ct1, st1, 0);
            //  <gd name="n1" fmla="*/ iwd2 ihd2 m1" />
            var n1 = iwd2 * ihd2 / m1;
            //  <gd name="dx1" fmla="cos n1 stAng1" />
            var dx1 = Cos(n1, (int) stAng1);
            //  <gd name="dy1" fmla="sin n1 stAng1" />
            var dy1 = Sin(n1, (int) stAng1);
            //  <gd name="x1" fmla="+- hc dx1 0" />
            var x1 = hc + dx1;
            //  <gd name="y1" fmla="+- vc dy1 0" />
            var y1 = vc + dy1;
            //  <gd name="x2" fmla="+- hc 0 dx1" />
            var x2 = hc - dx1;
            //  <gd name="y2" fmla="+- vc 0 dy1" />
            var y2 = vc - dy1;
            //  <gd name="idx" fmla="cos wd2 2700000" />
            var idx = Cos(wd2, 2700000);
            //  <gd name="idy" fmla="sin hd2 2700000" />
            var idy = Sin(hd2, 2700000);
            //  <gd name="il" fmla="+- hc 0 idx" />
            var il = hc - idx;
            //  <gd name="ir" fmla="+- hc idx 0" />
            var ir = hc + idx;
            //  <gd name="it" fmla="+- vc 0 idy" />
            var it = vc - idy;
            //  <gd name="ib" fmla="+- vc idy 0" />
            var ib = vc + idy;


            //  <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            //    <close />
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <arcTo wR="iwd2" hR="ihd2" stAng="stAng1" swAng="swAng" />
            //    <close />
            //    <moveTo>
            //      <pt x="x2" y="y2" />
            //    </moveTo>
            //    <arcTo wR="iwd2" hR="ihd2" stAng="stAng2" swAng="swAng" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            var wR = wd2;
            var hR = hd2;
            var stAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, cd4);
            //    <arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = 3 * cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, cd4);
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = 0;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, cd4);
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, cd4);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(x1, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="iwd2" hR="ihd2" stAng="stAng1" swAng="swAng" />
            wR = iwd2;
            hR = ihd2;
            stAng = stAng1;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="x2" y="y2" />
            //    </moveTo>
            currentPoint = new EmuPoint(x2, y2);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="iwd2" hR="ihd2" stAng="stAng2" swAng="swAng" />
            wR = iwd2;
            hR = ihd2;
            stAng = stAng2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
