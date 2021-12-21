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
    /// 半闭框
    /// </summary>
    public class HalfFrameGeometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 33333" />
            //  <gd name="adj2" fmla="val 33333" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 33333d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 33333d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 100000 w ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="x1" fmla="*/ ss a2 100000" />
            //  <gd name="g1" fmla="*/ h x1 w" />
            //  <gd name="g2" fmla="+- h 0 g1" />
            //  <gd name="maxAdj1" fmla="*/ 100000 g2 ss" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="y1" fmla="*/ ss a1 100000" />
            //  <gd name="dx2" fmla="*/ y1 w h" />
            //  <gd name="x2" fmla="+- r 0 dx2" />
            //  <gd name="dy2" fmla="*/ x1 h w" />
            //  <gd name="y2" fmla="+- b 0 dy2" />
            //  <gd name="cx1" fmla="*/ x1 1 2" />
            //  <gd name="cy1" fmla="+/ y2 b 2" />
            //  <gd name="cx2" fmla="+/ x2 r 2" />
            //  <gd name="cy2" fmla="*/ y1 1 2" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 100000 w ss" />
            var maxAdj2 = 100000 * w / ss;
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="x1" fmla="*/ ss a2 100000" />
            var x1 = ss * a2 / 100000;
            //  <gd name="g1" fmla="*/ h x1 w" />
            var g1 = h * x1 / w;
            //  <gd name="g2" fmla="+- h 0 g1" />
            var g2 = h - g1;
            //  <gd name="maxAdj1" fmla="*/ 100000 g2 ss" />
            var maxAdj1 = 100000 * g2 / ss;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="y1" fmla="*/ ss a1 100000" />
            var y1 = ss * a1 / 100000;
            //  <gd name="dx2" fmla="*/ y1 w h" />
            var dx2 = y1 * w / h;
            //  <gd name="x2" fmla="+- r 0 dx2" />
            var x2 = r - dx2;
            //  <gd name="dy2" fmla="*/ x1 h w" />
            var dy2 = x1 * h / w;
            //  <gd name="y2" fmla="+- b 0 dy2" />
            var y2 = b - dy2;
            //  <gd name="cx1" fmla="*/ x1 1 2" />
            var cx1 = x1 * 1 / 2;
            //  <gd name="cy1" fmla="+/ y2 b 2" />
            var cy1 = (y2 + b) / 2;
            //  <gd name="cx2" fmla="+/ x2 r 2" />
            var cx2 = (x2 + r) / 2;
            //  <gd name="cy2" fmla="*/ y1 1 2" />
            var cy2 = y1 * 1 / 2;


            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
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
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }
}
