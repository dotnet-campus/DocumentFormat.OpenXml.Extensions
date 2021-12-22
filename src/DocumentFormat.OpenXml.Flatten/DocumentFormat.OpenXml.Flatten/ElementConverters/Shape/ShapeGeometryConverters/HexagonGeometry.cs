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
    ///     六边形
    /// </summary>
    internal class HexagonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 25000" />
            //  <gd name="vf" fmla="val 115470" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var customVf = adjusts?.GetAdjustValue("vf");
            var adj = customAdj ?? 25000d;
            var vf = customVf ?? 115470d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 50000 w ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            //  <gd name="x1" fmla="*/ ss a 100000" />
            //  <gd name="x2" fmla="+- r 0 x1" />
            //  <gd name="dy1" fmla="sin shd2 3600000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc dy1 0" />
            //  <gd name="q1" fmla="*/ maxAdj -1 2" />
            //  <gd name="q2" fmla="+- a q1 0" />
            //  <gd name="q3" fmla="?: q2 4 2" />
            //  <gd name="q4" fmla="?: q2 3 2" />
            //  <gd name="q5" fmla="?: q2 q1 0" />
            //  <gd name="q6" fmla="+/ a q5 q1" />
            //  <gd name="q7" fmla="*/ q6 q4 -1" />
            //  <gd name="q8" fmla="+- q3 q7 0" />
            //  <gd name="il" fmla="*/ w q8 24" />
            //  <gd name="it" fmla="*/ h q8 24" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 it" />
            //</gdLst>

            //  <gd name="maxAdj" fmla="*/ 50000 w ss" />
            var maxAdj = w * 50000 / ss;
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            var shd2 = hd2 * vf / 100000;
            //  <gd name="x1" fmla="*/ ss a 100000" />
            var x1 = ss * a / 100000;
            //  <gd name="x2" fmla="+- r 0 x1" />
            var x2 = r - x1;
            //  <gd name="dy1" fmla="sin shd2 3600000" />
            var dy1 = Sin(shd2, 3600000);
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            var y1 = vc - dy1;
            //  <gd name="y2" fmla="+- vc dy1 0" />
            var y2 = vc + dy1;
            //  <gd name="q1" fmla="*/ maxAdj -1 2" />
            var q1 = maxAdj * (-1) / 2;
            //  <gd name="q2" fmla="+- a q1 0" />
            var q2 = a + q1;
            //  <gd name="q3" fmla="?: q2 4 2" />
            var q3 = q2 > 0 ? 4 : 2;
            //  <gd name="q4" fmla="?: q2 3 2" />
            var q4 = q2 > 0 ? 3 : 2;
            //  <gd name="q5" fmla="?: q2 q1 0" />
            var q5 = q2 > 0 ? q1 : 0;
            //  <gd name="q6" fmla="+/ a q5 q1" />
            var q6 = (a + q5) / q1;
            //  <gd name="q7" fmla="*/ q6 q4 -1" />
            var q7 = q6 * q4 / (-1);
            //  <gd name="q8" fmla="+- q3 q7 0" />
            var q8 = q3 + q7;
            //  <gd name="il" fmla="*/ w q8 24" />
            var il = w * q8 / 24;
            //  <gd name="it" fmla="*/ h q8 24" />
            var it = h * q8 / 24;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="ib" fmla="+- b 0 it" />
            var ib = b - it;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //   <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y2);

            stringPath.Append("z");

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return stringPath.ToString();
        }

        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var shapePaths = new ShapePath[1];
            shapePaths[0] = new ShapePath(ToGeometryPathString(emuSize, adjusts));

            return shapePaths;
        }
    }
}
