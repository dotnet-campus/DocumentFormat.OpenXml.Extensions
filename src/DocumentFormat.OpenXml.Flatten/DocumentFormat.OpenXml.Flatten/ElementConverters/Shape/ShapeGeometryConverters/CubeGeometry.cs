using System.Collections.Generic;
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
    /// 立方体
    /// </summary>
    public class CubeGeometry : ShapeGeometryBase
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
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 25000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj");
            var adj = customAdj1 ?? 25000d;

            //  <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 100000" />
            //  <gd name="y1" fmla="*/ ss a 100000" />
            //  <gd name="y4" fmla="+- b 0 y1" />
            //  <gd name="y2" fmla="*/ y4 1 2" />
            //  <gd name="y3" fmla="+/ y1 b 2" />
            //  <gd name="x4" fmla="+- r 0 y1" />
            //  <gd name="x2" fmla="*/ x4 1 2" />
            //  <gd name="x3" fmla="+/ y1 r 2" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 100000" />
            var a = Pin(0, adj, 100000);
            //  <gd name="y1" fmla="*/ ss a 100000" />
            var y1 = ss * a / 100000;
            //  <gd name="y4" fmla="+- b 0 y1" />
            var y4 = b - y1;
            //  <gd name="y2" fmla="*/ y4 1 2" />
            var y2 = y4 * 1 / 2;
            //  <gd name="y3" fmla="+/ y1 b 2" />
            var y3 = y1 + b - 2;
            //  <gd name="x4" fmla="+- r 0 y1" />
            var x4 = r - y1;
            //  <gd name="x2" fmla="*/ x4 1 2" />
            var x2 = x4 * 1 / 2;
            //  <gd name="x3" fmla="+/ y1 r 2" />
            var x3 = y1 + r / 2;

            //   <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="lightenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="y1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="y1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[4];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            stringPath.Append("z ");

            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);

            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x4, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y4);
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            //    <close />
            stringPath.Append("z ");

            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, false);

            //  </path>
            //  <path stroke="false" fill="lightenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(l, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="y1" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, y1, t);
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <close />
            stringPath.Append("z ");
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.LightenLess, false);

            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(l, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="y1" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, y1, t);
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y4);
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //stringPath.Clear();
            currentPoint = new EmuPoint(l, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            //stringPath.Clear();
            currentPoint = new EmuPoint(x4, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            shapePaths[3] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="l" t="y1" r="x4" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, y1, x4, b);

            return shapePaths;
        }
    }
}
