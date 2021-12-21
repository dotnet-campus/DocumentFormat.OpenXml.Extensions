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
    /// 矩形：折角
    /// </summary>
    public class FoldedCornerGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 16667" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 16667d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="dy2" fmla="*/ ss a 100000" />
            //  <gd name="dy1" fmla="*/ dy2 1 5" />
            //  <gd name="x1" fmla="+- r 0 dy2" />
            //  <gd name="x2" fmla="+- x1 dy1 0" />
            //  <gd name="y2" fmla="+- b 0 dy2" />
            //  <gd name="y1" fmla="+- y2 dy1 0" />
            //</gdLst>


            //  <gd name="a" fmla="pin 0 adj 50000" />
            var a = Pin(0, adj, 50000);
            //  <gd name="dy2" fmla="*/ ss a 100000" />
            var dy2 = ss * a / 100000;
            //  <gd name="dy1" fmla="*/ dy2 1 5" />
            var dy1 = dy2 * 1 / 5;
            //  <gd name="x1" fmla="+- r 0 dy2" />
            var x1 = r - dy2;
            //  <gd name="x2" fmla="+- x1 dy1 0" />
            var x2 = x1 + dy1;
            //  <gd name="y2" fmla="+- b 0 dy2" />
            var y2 = b - dy2;
            //  <gd name="y1" fmla="+- y2 dy1 0" />
            var y1 = y2 + dy1;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[3];
            //  <path stroke="false" extrusionOk="false">
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
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="b" />
            //    </moveTo>
            currentPoint = new EmuPoint(x1, b);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);

            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="b" />
            //    </moveTo>
            currentPoint = new EmuPoint(x1, b);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <lnTo>
            //      <pt x="l" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, t);
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="l" t="t" r="r" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, y2);

            return shapePaths;
        }
    }
}
