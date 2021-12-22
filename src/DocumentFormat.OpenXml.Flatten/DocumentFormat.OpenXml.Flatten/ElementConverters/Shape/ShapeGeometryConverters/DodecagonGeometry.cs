using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    ///     十二边形形状
    /// </summary>
    internal class DodecagonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            // <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x1" fmla="*/ w 2894 21600" />
            //  <gd name="x2" fmla="*/ w 7906 21600" />
            //  <gd name="x3" fmla="*/ w 13694 21600" />
            //  <gd name="x4" fmla="*/ w 18706 21600" />
            //  <gd name="y1" fmla="*/ h 2894 21600" />
            //  <gd name="y2" fmla="*/ h 7906 21600" />
            //  <gd name="y3" fmla="*/ h 13694 21600" />
            //  <gd name="y4" fmla="*/ h 18706 21600" />
            //</gdLst>

            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <gd name="x1" fmla="*/ w 2894 21600" />
            var x1 = w * 2894 / 21600;
            //  <gd name="x2" fmla="*/ w 7906 21600" />
            var x2 = w * 7906 / 21600;
            //  <gd name="x3" fmla="*/ w 13694 21600" />
            var x3 = w * 13694 / 21600;
            //  <gd name="x4" fmla="*/ w 18706 21600" />
            var x4 = w * 18706 / 21600;
            //  <gd name="y1" fmla="*/ h 2894 21600" />
            var y1 = h * 2894 / 21600;
            //  <gd name="y2" fmla="*/ h 7906 21600" />
            var y2 = h * 7906 / 21600;
            //  <gd name="y3" fmla="*/ h 13694 21600" />
            var y3 = h * 13694 / 21600;
            //  <gd name="y4" fmla="*/ h 18706 21600" />
            var y4 = h * 18706 / 21600;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y3" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, t);
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="r" y="y3" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, y3);
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            _ = LineToToString(stringPath, x4, y4);
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, b);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y4);
            //    <lnTo>
            //      <pt x="l" y="y3" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, y3);

            stringPath.Append("z");

            //<rect l="x1" t="y1" r="x4" b="y4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y1, x4, y4);

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
