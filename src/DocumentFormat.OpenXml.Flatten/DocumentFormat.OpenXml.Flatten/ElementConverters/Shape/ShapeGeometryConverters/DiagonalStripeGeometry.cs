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
    /// 斜纹
    /// </summary>
    public class DiagonalStripeGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 50000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 100000" />
            //  <gd name="x2" fmla="*/ w a 100000" />
            //  <gd name="x1" fmla="*/ x2 1 2" />
            //  <gd name="x3" fmla="+/ x2 r 2" />
            //  <gd name="y2" fmla="*/ h a 100000" />
            //  <gd name="y1" fmla="*/ y2 1 2" />
            //  <gd name="y3" fmla="+/ y2 b 2" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 100000" />
            var a = Pin(0, adj, 100000);
            //  <gd name="x2" fmla="*/ w a 100000" />
            var x2 = w * a / 100000;
            //  <gd name="x1" fmla="*/ x2 1 2" />
            var x1 = x2 * 1 / 2;
            //  <gd name="x3" fmla="+/ x2 r 2" />
            var x3 = (x2 + r) / 2;
            //  <gd name="y2" fmla="*/ h a 100000" />
            var y2 = h * a / 100000;
            //  <gd name="y1" fmla="*/ y2 1 2" />
            var y1 = y2 * 1 / 2;
            //  <gd name="y3" fmla="+/ y2 b 2" />
            var y3 = (y2 + b) / 2;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
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
            //      <pt x="l" y="y2" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="l" t="t" r="x3" b="y3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, x3, y3);

            return shapePaths;
        }
    }
}
