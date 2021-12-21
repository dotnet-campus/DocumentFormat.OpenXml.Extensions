using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图：资料带
    /// </summary>
    public class FlowChartPunchedTapeGeometry : ShapeGeometryBase
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
            //   <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="y2" fmla="*/ h 9 10" />
            //  <gd name="ib" fmla="*/ h 4 5" />
            //</gdLst>

            //  <gd name="y2" fmla="*/ h 9 10" />
            var y2 = h * 9 / 10;
            //  <gd name="ib" fmla="*/ h 4 5" />
            var ib = h * 4 / 5;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="20" h="20">
            //    <moveTo>
            //      <pt x="0" y="2" />
            //    </moveTo>
            //    <arcTo wR="5" hR="2" stAng="cd2" swAng="-10800000" />
            //    <arcTo wR="5" hR="2" stAng="cd2" swAng="cd2" />
            //    <lnTo>
            //      <pt x="20" y="18" />
            //    </lnTo>
            //    <arcTo wR="5" hR="2" stAng="0" swAng="-10800000" />
            //    <arcTo wR="5" hR="2" stAng="0" swAng="cd2" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path w="20" h="20">
            //    <moveTo>
            //      <pt x="0" y="2" />
            //    </moveTo>
            var widthFactor = w / 20;
            var heightFactor = h / 20;
            var currentPoint = new EmuPoint(0, heightFactor * 2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="5" hR="2" stAng="cd2" swAng="-10800000" />
            var wR = widthFactor * 5;
            var hR = heightFactor * 2;
            var stAng = cd2;
            var swAng = -10800000d;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="5" hR="2" stAng="cd2" swAng="cd2" />
            wR = widthFactor * 5;
            hR = heightFactor * 2;
            stAng = cd2;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="20" y="18" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 20, heightFactor * 18);
            //    <arcTo wR="5" hR="2" stAng="0" swAng="-10800000" />
            wR = widthFactor * 5;
            hR = heightFactor * 2;
            stAng = 0;
            swAng = -10800000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="5" hR="2" stAng="0" swAng="cd2" />
            wR = widthFactor * 5;
            hR = heightFactor * 2;
            stAng = 0;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");

            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="l" t="hd5" r="r" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, hd5, r, ib);

            return shapePaths;
        }
    }
}
