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
    ///     平行四边形
    /// </summary>
    internal class ParallelogramGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //   <gd name="adj" fmla="val 25000" />
            // </avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 25000d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 100000 w ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="x1" fmla="*/ ss a 200000" />
            //  <gd name="x2" fmla="*/ ss a 100000" />
            //  <gd name="x6" fmla="+- r 0 x1" />
            //  <gd name="x5" fmla="+- r 0 x2" />
            //  <gd name="x3" fmla="*/ x5 1 2" />
            //  <gd name="x4" fmla="+- r 0 x3" />
            //  <gd name="il" fmla="*/ wd2 a maxAdj" />
            //  <gd name="q1" fmla="*/ 5 a maxAdj" />
            //  <gd name="q2" fmla="+/ 1 q1 12" />
            //  <gd name="il" fmla="*/ q2 w 1" />
            //  <gd name="it" fmla="*/ q2 h 1" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 it" />
            //  <gd name="q3" fmla="*/ h hc x2" />
            //  <gd name="y1" fmla="pin 0 q3 h" />
            //  <gd name="y2" fmla="+- b 0 y1" />
            //</gdLst>

            //   <gd name="maxAdj" fmla="*/ 100000 w ss" />
            var maxAdj = 100000 * w / ss;
            //   <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //   <gd name="x1" fmla="*/ ss a 200000" />
            var x2 = ss * a / 100000;
            //   <gd name="x5" fmla="+- r 0 x2" />
            var x5 = r - x2;
            //  <gd name="x3" fmla="*/ x5 1 2" />
            var x3 = x5 * 1 / 2;
            //  <gd name="x4" fmla="+- r 0 x3" />
            var x4 = r - x3;
            //  <gd name="il" fmla="*/ wd2 a maxAdj" />
            var il = wd2 * a / maxAdj;
            //  <gd name="q1" fmla="*/ 5 a maxAdj" />
            var q1 = 5 * a / maxAdj;
            //  <gd name="q2" fmla="+/ 1 q1 12" />
            var q2 = (1 + q1) / 12;
            //  <gd name="il" fmla="*/ q2 w 1" />
            il = q2 * w / 1;
            //  <gd name="it" fmla="*/ q2 h 1" />
            var it = q2 * h / 1;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="ib" fmla="+- b 0 it" />
            var ib = b - it;
            //  <gd name="q3" fmla="*/ h hc x2" />
            var q3 = h * hc / x2;
            //  <gd name="y1" fmla="pin 0 q3 h" />
            var y1 = Pin(0, q3, h);
            //  <gd name="y2" fmla="+- b 0 y1" />
            var y2 = b - y1;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="x5" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, x5, b);

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
