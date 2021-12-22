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
    ///     梯形
    /// </summary>
    internal class TrapezoidGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 25000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 50000 w ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="x1" fmla="*/ ss a 200000" />
            //  <gd name="x2" fmla="*/ ss a 100000" />
            //  <gd name="x3" fmla="+- r 0 x2" />
            //  <gd name="x4" fmla="+- r 0 x1" />
            //  <gd name="il" fmla="*/ wd3 a maxAdj" />
            //  <gd name="it" fmla="*/ hd3 a maxAdj" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //</gdLst>

            //  <gd name="maxAdj" fmla="*/ 50000 w ss" />
            var maxAdj = 50000 * w / ss;
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //  <gd name="x1" fmla="*/ ss a 200000" />
            var x1 = ss * a / 200000;
            //  <gd name="x2" fmla="*/ ss a 100000" />
            var x2 = ss * a / 100000;
            //  <gd name="x3" fmla="+- r 0 x2" />
            var x3 = r - x2;
            //  <gd name="x4" fmla="+- r 0 x1" />
            var x4 = r - x1;
            //  <gd name="il" fmla="*/ wd3 a maxAdj" />
            var wd3 = w / 3;
            var il = wd3 * a / maxAdj;
            //  <gd name="it" fmla="*/ hd3 a maxAdj" />
            var hd3 = h / 3;
            var it = hd3 * a / maxAdj;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
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
            //      <pt x="x3" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, t);
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, b);

            stringPath.Append("z");

            //<rect l="il" t="it" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, b);

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
