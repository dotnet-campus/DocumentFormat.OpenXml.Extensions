using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 标注：线形(带边框和强调线)
    /// </summary>
    public class AccentBorderCallout1Geometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 18750" />
            //  <gd name="adj2" fmla="val -8333" />
            //  <gd name="adj3" fmla="val 112500" />
            //  <gd name="adj4" fmla="val -38333" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 18750d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? -8333d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 112500d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? -38333d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="y1" fmla="*/ h adj1 100000" />
            var y1 = h * adj1 / 100000;
            //  <gd name="x1" fmla="*/ w adj2 100000" />
            var x1 = w * adj2 / 100000;
            //  <gd name="y2" fmla="*/ h adj3 100000" />
            var y2 = h * adj3 / 100000;
            //  <gd name="x2" fmla="*/ w adj4 100000" />
            var x2 = w * adj4 / 100000;
            //</gdLst>


            //  <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path extrusionOk="false">
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
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="t" />
            //    </moveTo>
            //    <close />
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];
            //  <path extrusionOk="false">
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
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="t" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x1, t);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <close />
            stringPath.Append("z ");
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //  </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x1, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }
}
