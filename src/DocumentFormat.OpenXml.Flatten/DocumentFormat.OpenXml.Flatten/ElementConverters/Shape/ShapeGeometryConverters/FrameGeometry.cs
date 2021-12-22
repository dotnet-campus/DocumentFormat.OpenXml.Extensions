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
    /// 图文框
    /// </summary>
    public class FrameGeometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 12500" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj ?? 12500d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="x1" fmla="*/ ss a1 100000" />
            //  <gd name="x4" fmla="+- r 0 x1" />
            //  <gd name="y4" fmla="+- b 0 x1" />
            //</gdLst>

            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            var a1 = Pin(0, adj1, 50000);
            //  <gd name="x1" fmla="*/ ss a1 100000" />
            var x1 = ss * a1 / 100000;
            //  <gd name="x4" fmla="+- r 0 x1" />
            var x4 = r - x1;
            //  <gd name="y4" fmla="+- b 0 x1" />
            var y4 = b - x1;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x1" y="x1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="x1" />
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
            //      <pt x="r" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="x1" y="x1" />
            //    </moveTo>
            currentPoint = new EmuPoint(x1, x1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y4);
            //    <lnTo>
            //      <pt x="x4" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, x1);
            //    <close />
            stringPath.Append("z ");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="x1" t="x1" r="x4" b="y4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, x1, x4, y4);

            return shapePaths;
        }
    }
}
