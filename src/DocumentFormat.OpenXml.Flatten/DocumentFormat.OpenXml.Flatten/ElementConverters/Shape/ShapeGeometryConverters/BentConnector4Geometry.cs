using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 连接符：肘形4
    /// </summary>
    public class BentConnector4Geometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x1" fmla="*/ w adj1 100000" />
            //  <gd name="x2" fmla="+/ x1 r 2" />
            //  <gd name="y2" fmla="*/ h adj2 100000" />
            //  <gd name="y1" fmla="+/ t y2 2" />
            //</gdLst>

            //  <gd name="x1" fmla="*/ w adj1 100000" />
            var x1 = w * adj1 / 100000;
            //  <gd name="x2" fmla="+/ x1 r 2" />
            var x2 = (x1 + r) / 2;
            //  <gd name="y2" fmla="*/ h adj2 100000" />
            var y2 = h * adj2 / 100000;
            //  <gd name="y1" fmla="+/ t y2 2" />
            var y1 = (t + y2) / 2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path fill="none">
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
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            // <rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }
}
