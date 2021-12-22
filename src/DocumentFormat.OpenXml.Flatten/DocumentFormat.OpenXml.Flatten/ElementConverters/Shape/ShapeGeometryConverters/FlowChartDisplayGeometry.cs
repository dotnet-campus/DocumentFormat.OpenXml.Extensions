using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 显示
    /// </summary>
    public class FlowChartDisplayGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x2" fmla="*/ w 5 6" />
            //</gdLst>

            //<gd name="x2" fmla="*/ w 5 6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = w * 5 / 6;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="6" h="6">
            //    <moveTo>
            //      <pt x="0" y="3" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="5" y="0" />
            //    </lnTo>
            //    <arcTo wR="1" hR="3" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="1" y="6" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            //设置Emu转Pixel的精度为小数点4位，防止精度不够被转为0
            UnitPrecision = 4;
            var shapePaths = new ShapePath[1];

            // <path w="6"h="6">
            var shapePathWidth = 6d;
            var shapePathHeight = 6d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="3" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 3);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="5" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 5, 0);
            //<arcTo wR="1" hR="3" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 1d, 3d, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 6);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="wd6" t="t" r="x2" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(wd6, t, x2, b);

            return shapePaths;
        }
    }


}

