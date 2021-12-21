using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 文档
    /// </summary>
    public class FlowChartDocumentGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="y1" fmla="*/ h 17322 21600" />
            //  <gd name="y2" fmla="*/ h 20172 21600" />
            //</gdLst>

            //<gd name="y1" fmla="*/ h 17322 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = h * 17322 / 21600;
            //<gd name="y2" fmla="*/ h 20172 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = h * 20172 / 21600;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="21600" h="21600">
            //    <moveTo>
            //      <pt x="0" y="0" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="21600" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="17322" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="10800" y="17322" />
            //      <pt x="10800" y="23922" />
            //      <pt x="0" y="20172" />
            //    </cubicBezTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path w="21600"h="21600">
            var shapePathWidth = 21600d;
            var shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="0" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 0);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="17322" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 17322);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="10800" y="17322" />
            //  <pt x="10800" y="23922" />
            //  <pt x="0" y="20172" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 10800, 17322, 10800, 23922, 0, 20172);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="l" t="t" r="r" b="y1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, y1);

            return shapePaths;
        }
    }


}

