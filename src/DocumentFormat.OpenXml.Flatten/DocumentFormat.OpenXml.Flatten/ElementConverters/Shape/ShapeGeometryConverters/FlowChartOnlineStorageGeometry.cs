using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 存储数据
    /// </summary>
    public class FlowChartOnlineStorageGeometry : ShapeGeometryBase
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
            //      <pt x="1" y="0" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="6" y="0" />
            //    </lnTo>
            //    <arcTo wR="1" hR="3" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="1" y="6" />
            //    </lnTo>
            //    <arcTo wR="1" hR="3" stAng="cd4" swAng="cd2" />
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
            //  <pt x="1" y="0" />
            //</moveTo>
            var currentPoint = new EmuPoint(1, 0);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="6" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 6, 0);
            //<arcTo wR="1" hR="3" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 1d, 3d, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 6);
            //<arcTo wR="1" hR="3" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 1d, 3d, cd4, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="wd6" t="t" r="x2" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(wd6, t, x2, b);

            return shapePaths;
        }
    }


}

