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
    /// 箭头：V形
    /// </summary>
    public class ChevronGeometry : ShapeGeometryBase
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
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 50000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 100000 w ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="x1" fmla="*/ ss a 100000" />
            //  <gd name="x2" fmla="+- r 0 x1" />
            //  <gd name="x3" fmla="*/ x2 1 2" />
            //  <gd name="dx" fmla="+- x2 0 x1" />
            //  <gd name="il" fmla="?: dx x1 l" />
            //  <gd name="ir" fmla="?: dx x2 r" />
            //</gdLst>

            //  <gd name="maxAdj" fmla="*/ 100000 w ss" />
            var maxAdj = 100000 * w / ss;
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //  <gd name="x1" fmla="*/ ss a 100000" />
            var x1 = ss * a / 100000;
            //  <gd name="x2" fmla="+- r 0 x1" />
            var x2 = r - x1;
            //  <gd name="x3" fmla="*/ x2 1 2" />
            var x3 = x2 / 2;
            //  <gd name="dx" fmla="+- x2 0 x1" />
            var dx = x2 - x1;
            //  <gd name="il" fmla="?: dx x1 l" />
            var il = dx > 0 ? x1 : l;
            //  <gd name="ir" fmla="?: dx x2 r" />
            var ir = dx > 0 ? x2 : r;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="vc" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <lnTo>
            //      <pt x="x1" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, vc);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="il" t="t" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, t, ir, b);

            return shapePaths;
        }
    }
}
