using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 漏斗形
    /// </summary>
    public class FunnelGeometry : ShapeGeometryBase
    {

        /// <inheritdoc />
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="d" fmla="*/ ss 1 20" />
            //  <gd name="rw2" fmla="+- wd2 0 d" />
            //  <gd name="rh2" fmla="+- hd4 0 d" />
            //  <gd name="t1" fmla="cos wd2 480000" />
            //  <gd name="t2" fmla="sin hd4 480000" />
            //  <gd name="da" fmla="at2 t1 t2" />
            //  <gd name="2da" fmla="*/ da 2 1" />
            //  <gd name="stAng1" fmla="+- cd2 0 da" />
            //  <gd name="swAng1" fmla="+- cd2 2da 0" />
            //  <gd name="swAng3" fmla="+- cd2 0 2da" />
            //  <gd name="rw3" fmla="*/ wd2 1 4" />
            //  <gd name="rh3" fmla="*/ hd4 1 4" />
            //  <gd name="ct1" fmla="cos hd4 stAng1" />
            //  <gd name="st1" fmla="sin wd2 stAng1" />
            //  <gd name="m1" fmla="mod ct1 st1 0" />
            //  <gd name="n1" fmla="*/ wd2 hd4 m1" />
            //  <gd name="dx1" fmla="cos n1 stAng1" />
            //  <gd name="dy1" fmla="sin n1 stAng1" />
            //  <gd name="x1" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- hd4 dy1 0" />
            //  <gd name="ct3" fmla="cos rh3 da" />
            //  <gd name="st3" fmla="sin rw3 da" />
            //  <gd name="m3" fmla="mod ct3 st3 0" />
            //  <gd name="n3" fmla="*/ rw3 rh3 m3" />
            //  <gd name="dx3" fmla="cos n3 da" />
            //  <gd name="dy3" fmla="sin n3 da" />
            //  <gd name="x3" fmla="+- hc dx3 0" />
            //  <gd name="vc3" fmla="+- b 0 rh3" />
            //  <gd name="y2" fmla="+- vc3 dy3 0" />
            //  <gd name="x2" fmla="+- wd2 0 rw2" />
            //  <gd name="cd" fmla="*/ cd2 2 1" />
            //</gdLst>

            //<gd name="d" fmla="*/ ss 1 20" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var d = ss * 1 / 20;
            //<gd name="rw2" fmla="+- wd2 0 d" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw2 = wd2 + 0 - d;
            //<gd name="rh2" fmla="+- hd4 0 d" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh2 = hd4 + 0 - d;
            //<gd name="t1" fmla="cos wd2 480000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var t1 = Cos(wd2, (int) 480000);
            //<gd name="t2" fmla="sin hd4 480000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var t2 = Sin(hd4, (int) 480000);
            //<gd name="da" fmla="at2 t1 t2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var da = ATan2(t1, t2);
            //<gd name="2da" fmla="*/ da 2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var _2da = da * 2 / 1;
            //<gd name="stAng1" fmla="+- cd2 0 da" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var stAng1 = cd2 + 0 - da;
            //<gd name="swAng1" fmla="+- cd2 2da 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng1 = cd2 + _2da - 0;
            //<gd name="swAng3" fmla="+- cd2 0 2da" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng3 = cd2 + 0 - _2da;
            //<gd name="rw3" fmla="*/ wd2 1 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw3 = wd2 * 1 / 4;
            //<gd name="rh3" fmla="*/ hd4 1 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh3 = hd4 * 1 / 4;
            //<gd name="ct1" fmla="cos hd4 stAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ct1 = Cos(hd4, (int) stAng1);
            //<gd name="st1" fmla="sin wd2 stAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var st1 = Sin(wd2, (int) stAng1);
            //<gd name="m1" fmla="mod ct1 st1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var m1 = Mod(ct1, st1, 0);
            //<gd name="n1" fmla="*/ wd2 hd4 m1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var n1 = wd2 * hd4 / m1;
            //<gd name="dx1" fmla="cos n1 stAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = Cos(n1, (int) stAng1);
            //<gd name="dy1" fmla="sin n1 stAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = Sin(n1, (int) stAng1);
            //<gd name="x1" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + dx1 - 0;
            //<gd name="y1" fmla="+- hd4 dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = hd4 + dy1 - 0;
            //<gd name="ct3" fmla="cos rh3 da" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ct3 = Cos(rh3, (int) da);
            //<gd name="st3" fmla="sin rw3 da" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var st3 = Sin(rw3, (int) da);
            //<gd name="m3" fmla="mod ct3 st3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var m3 = Mod(ct3, st3, 0);
            //<gd name="n3" fmla="*/ rw3 rh3 m3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var n3 = rw3 * rh3 / m3;
            //<gd name="dx3" fmla="cos n3 da" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = Cos(n3, (int) da);
            //<gd name="dy3" fmla="sin n3 da" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = Sin(n3, (int) da);
            //<gd name="x3" fmla="+- hc dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + dx3 - 0;
            //<gd name="vc3" fmla="+- b 0 rh3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var vc3 = b + 0 - rh3;
            //<gd name="y2" fmla="+- vc3 dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = vc3 + dy3 - 0;
            //<gd name="x2" fmla="+- wd2 0 rw2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = wd2 + 0 - rw2;
            //<gd name="cd" fmla="*/ cd2 2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cd = cd2 * 2 / 1;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <arcTo hR="hd4" wR="wd2" stAng="stAng1" swAng="swAng1" />
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            //    <arcTo hR="rh3" wR="rw3" stAng="da" swAng="swAng3" />
            //    <close />
            //    <moveTo>
            //      <pt x="x2" y="hd4" />
            //    </moveTo>
            //    <arcTo hR="rh2" wR="rw2" stAng="cd2" swAng="-21600000" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y1" />
            //</moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo hR="hd4" wR="wd2" stAng="stAng1" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd4, stAng1, swAng1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y2);
            //<arcTo hR="rh3" wR="rw3" stAng="da" swAng="swAng3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw3, rh3, da, swAng3);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="hd4" />
            //</moveTo>
            currentPoint = new EmuPoint(x2, hd4);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo hR="rh2" wR="rw2" stAng="cd2" swAng="-21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw2, rh2, cd2, -21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }


}

