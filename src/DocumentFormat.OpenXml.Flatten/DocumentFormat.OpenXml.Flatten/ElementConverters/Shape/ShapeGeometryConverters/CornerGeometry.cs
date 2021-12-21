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
    /// L形
    /// </summary>
    public class CornerGeometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj1" fmla="*/ 100000 h ss" />
            //  <gd name="maxAdj2" fmla="*/ 100000 w ss" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="x1" fmla="*/ ss a2 100000" />
            //  <gd name="dy1" fmla="*/ ss a1 100000" />
            //  <gd name="y1" fmla="+- b 0 dy1" />
            //  <gd name="cx1" fmla="*/ x1 1 2" />
            //  <gd name="cy1" fmla="+/ y1 b 2" />
            //  <gd name="d" fmla="+- w 0 h" />
            //  <gd name="it" fmla="?: d y1 t" />
            //  <gd name="ir" fmla="?: d r x1" />
            //</gdLst>

            //  <gd name="maxAdj1" fmla="*/ 100000 h ss" />
            var maxAdj1 = 100000 * h / ss;
            //  <gd name="maxAdj2" fmla="*/ 100000 w ss" />
            var maxAdj2 = 100000 * w / ss;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="x1" fmla="*/ ss a2 100000" />
            var x1 = ss * a2 / 100000;
            //  <gd name="dy1" fmla="*/ ss a1 100000" />
            var dy1 = ss * a1 / 100000;
            //  <gd name="y1" fmla="+- b 0 dy1" />
            var y1 = b - dy1;
            //  <gd name="cx1" fmla="*/ x1 1 2" />
            var cx1 = x1 * 1 / 2;
            //  <gd name="cy1" fmla="+/ y1 b 2" />
            var cy1 = y1 * b / 2;
            //  <gd name="d" fmla="+- w 0 h" />
            var d = w - h;
            //  <gd name="it" fmla="?: d y1 t" />
            var it = d > 0 ? y1 : t;
            //  <gd name="ir" fmla="?: d r x1" />
            var ir = d > 0 ? r : x1;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
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
            //      <pt x="x1" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, t);
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y1);
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="l" t="it" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, it, ir, b);

            return shapePaths;
        }
    }
}
