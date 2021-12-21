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
    ///     十边形形状
    /// </summary>
    internal class DecagonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="vf" fmla="val 105146" />
            //</avLst>
            var customVf = adjusts?.GetAdjustValue("vf");
            var vf = customVf ?? 105146d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            //  <gd name="dx1" fmla="cos wd2 2160000" />
            //  <gd name="dx2" fmla="cos wd2 4320000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc dx1 0" />
            //  <gd name="dy1" fmla="sin shd2 4320000" />
            //  <gd name="dy2" fmla="sin shd2 2160000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc dy2 0" />
            //  <gd name="y4" fmla="+- vc dy1 0" />
            //</gdLst>


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            var shd2 = hd2 * vf / 100000;
            //  <gd name="dx1" fmla="cos wd2 2160000" />
            var dx1 = Cos(wd2, 2160000);
            //  <gd name="dx2" fmla="cos wd2 4320000" />
            var dx2 = Cos(wd2, 4320000);
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            var x1 = hc - dx1;
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            var x2 = hc - dx2;
            //  <gd name="x3" fmla="+- hc dx2 0" />
            var x3 = hc + dx2;
            //  <gd name="x4" fmla="+- hc dx1 0" />
            var x4 = hc + dx1;
            //  <gd name="dy1" fmla="sin shd2 4320000" />
            var dy1 = Sin(shd2, 4320000);
            //  <gd name="dy2" fmla="sin shd2 2160000" />
            var dy2 = Sin(shd2, 2160000);
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            var y1 = vc - dy1;
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            var y2 = vc - dy2;
            //  <gd name="y3" fmla="+- vc dy2 0" />
            var y3 = vc + dy2;
            //  <gd name="y4" fmla="+- vc dy1 0" />
            var y4 = vc + dy1;
            //</gdLst>

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, y1);
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x4, y2);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            _ = LineToToString(stringPath, x4, y3);
            //    <lnTo>
            //      <pt x="x3" y="y4" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, y4);
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, y4);
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y3);

            stringPath.Append("z");

            //<rect l="x1" t="y2" r="x4" b="y3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y2, x4, y3);

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
