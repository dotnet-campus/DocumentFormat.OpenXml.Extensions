using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    ///     直角三角形
    /// </summary>
    internal class RightTriangleGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //  <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="it" fmla="*/ h 7 12" />
            //  <gd name="ir" fmla="*/ w 7 12" />
            //  <gd name="ib" fmla="*/ h 11 12" />
            //</gdLst>

            //  <gd name="it" fmla="*/ h 7 12" />
            var it = h * 7 / 12;
            //  <gd name="ir" fmla="*/ w 7 12" />
            var ir = w * 7 / 12;
            //  <gd name="ib" fmla="*/ h 11 12" />
            var ib = h * 11 / 12;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="l" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="l" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, t);
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, b);

            stringPath.Append("z");

            //<rect l="wd12" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var wd12 = w / 12;
            InitializeShapeTextRectangle(wd12, it, ir, ib);

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
