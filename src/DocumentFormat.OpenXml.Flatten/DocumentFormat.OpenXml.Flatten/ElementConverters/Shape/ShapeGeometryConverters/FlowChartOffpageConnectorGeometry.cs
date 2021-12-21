using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 离页连接符
    /// </summary>
    public class FlowChartOffpageConnectorGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="y1" fmla="*/ h 4 5" />
            //</gdLst>

            //<gd name="y1" fmla="*/ h 4 5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = h * 4 / 5;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="10" h="10">
            //    <moveTo>
            //      <pt x="0" y="0" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="10" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="10" y="8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="5" y="10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="8" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            //设置Emu转Pixel的精度为小数点4位，防止精度不够被转为0
            UnitPrecision = 4;
            var shapePaths = new ShapePath[1];

            // <path w="10"h="10">
            var shapePathWidth = 10d;
            var shapePathHeight = 10d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="0" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 0);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="10" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 10, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="10" y="8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 10, 8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="5" y="10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 5, 10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 0, 8);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="l" t="t" r="r" b="y1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, y1);

            return shapePaths;
        }
    }


}

