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
    /// 泪滴形
    /// </summary>
    public class TeardropGeometry : ShapeGeometryBase
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
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 100000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 100000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 200000" />
            //  <gd name="r2" fmla="sqrt 2" />
            //  <gd name="tw" fmla="*/ wd2 r2 1" />
            //  <gd name="th" fmla="*/ hd2 r2 1" />
            //  <gd name="sw" fmla="*/ tw a 100000" />
            //  <gd name="sh" fmla="*/ th a 100000" />
            //  <gd name="dx1" fmla="cos sw 2700000" />
            //  <gd name="dy1" fmla="sin sh 2700000" />
            //  <gd name="x1" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="x2" fmla="+/ hc x1 2" />
            //  <gd name="y2" fmla="+/ vc y1 2" />
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>


            //  <gd name="a" fmla="pin 0 adj 200000" />
            var a = Pin(0, adj, 200000);
            //  <gd name="r2" fmla="sqrt 2" />
            var r2 = System.Math.Sqrt(2);
            //  <gd name="tw" fmla="*/ wd2 r2 1" />
            var tw = wd2 * r2 / 1;
            //  <gd name="th" fmla="*/ hd2 r2 1" />
            var th = hd2 * r2 / 1;
            //  <gd name="sw" fmla="*/ tw a 100000" />
            var sw = tw * a / 100000;
            //  <gd name="sh" fmla="*/ th a 100000" />
            var sh = th * a / 100000;
            //  <gd name="dx1" fmla="cos sw 2700000" />
            var dx1 = Cos(sw, 2700000);
            //  <gd name="dy1" fmla="sin sh 2700000" />
            var dy1 = Sin(sh, 2700000);
            //  <gd name="x1" fmla="+- hc dx1 0" />
            var x1 = hc + dx1;
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            var y1 = vc - dy1;
            //  <gd name="x2" fmla="+/ hc x1 2" />
            var x2 = (hc + x1) / 2;
            //  <gd name="y2" fmla="+/ vc y1 2" />
            var y2 = (vc + y1) / 2;
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


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            //    <quadBezTo>
            //      <pt x="x2" y="t" />
            //      <pt x="x1" y="y1" />
            //    </quadBezTo>
            //    <quadBezTo>
            //      <pt x="r" y="y2" />
            //      <pt x="r" y="vc" />
            //    </quadBezTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
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
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <quadBezTo>
            //      <pt x="x2" y="t" />
            //      <pt x="x1" y="y1" />
            //    </quadBezTo>
            currentPoint = QuadBezToString(stringPath, x2, t, x1, y1);
            //    <quadBezTo>
            //      <pt x="r" y="y2" />
            //      <pt x="r" y="vc" />
            //    </quadBezTo>
            currentPoint = QuadBezToString(stringPath, r, y2, r, vc);
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = 0;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //< rect l = "il" t = "it" r = "ir" b = "ib" xmlns = "http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
