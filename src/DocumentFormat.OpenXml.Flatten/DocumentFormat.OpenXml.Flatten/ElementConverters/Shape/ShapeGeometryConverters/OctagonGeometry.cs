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
    ///     八边形
    /// </summary>
    internal class OctagonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 29289" />
            //</avLst>
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, d, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 29289d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="x1" fmla="*/ ss a 100000" />
            //  <gd name="x2" fmla="+- r 0 x1" />
            //  <gd name="y2" fmla="+- b 0 x1" />
            //  <gd name="il" fmla="*/ x1 1 2" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 il" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 50000" />
            var a = Pin(0, adj, 50000);
            //  <gd name="x1" fmla="*/ ss a 100000" />
            var x1 = ss * a / 100000;
            //  <gd name="x2" fmla="+- r 0 x1" />
            var x2 = r - x1;
            //  <gd name="y2" fmla="+- b 0 x1" />
            var y2 = b - x1;
            //  <gd name="il" fmla="*/ x1 1 2" />
            var il = x1 * 1 / 2;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="ib" fmla="+- b 0 il" />
            var ib = b - il;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="x1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="l" y="x1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, x1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, t);
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="r" y="x1" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, x1);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, y2);

            stringPath.Append("z");

            //<rect l="il" t="il" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, ib);

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
