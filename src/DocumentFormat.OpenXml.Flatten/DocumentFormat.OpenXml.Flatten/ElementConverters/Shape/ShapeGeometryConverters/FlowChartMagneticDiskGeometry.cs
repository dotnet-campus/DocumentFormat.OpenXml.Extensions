using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图：磁盘
    /// </summary>
    public class FlowChartMagneticDiskGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="y3" fmla="*/ h 5 6" />
            //</gdLst>

            //<gd name="y3" fmla="*/ h 5 6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = h * 5 / 6;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false" w="6" h="6">
            //    <moveTo>
            //      <pt x="0" y="1" />
            //    </moveTo>
            //    <arcTo wR="3" hR="1" stAng="cd2" swAng="cd2" />
            //    <lnTo>
            //      <pt x="6" y="5" />
            //    </lnTo>
            //    <arcTo wR="3" hR="1" stAng="0" swAng="cd2" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false" w="6" h="6">
            //    <moveTo>
            //      <pt x="6" y="1" />
            //    </moveTo>
            //    <arcTo wR="3" hR="1" stAng="0" swAng="cd2" />
            //  </path>
            //  <path fill="none" w="6" h="6">
            //    <moveTo>
            //      <pt x="0" y="1" />
            //    </moveTo>
            //    <arcTo wR="3" hR="1" stAng="cd2" swAng="cd2" />
            //    <lnTo>
            //      <pt x="6" y="5" />
            //    </lnTo>
            //    <arcTo wR="3" hR="1" stAng="0" swAng="cd2" />
            //    <close />
            //  </path>
            //</pathLst>

            //设置Emu转Pixel的精度为小数点4位，防止精度不够被转为0
            UnitPrecision = 4;
            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false"w="6"h="6">
            var shapePathWidth = 6d;
            var shapePathHeight = 6d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="1" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="3" hR="1" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3d, 1d, cd2, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="6" y="5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 6, 5);
            //<arcTo wR="3" hR="1" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3d, 1d, 0d, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            // <path fill="none"extrusionOk="false"w="6"h="6">
            shapePathWidth = 6d;
            shapePathHeight = 6d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="6" y="1" />
            //</moveTo>
            currentPoint = new EmuPoint(6, 1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="3" hR="1" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3d, 1d, 0d, cd2);
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            // <path fill="none"w="6"h="6">
            shapePathWidth = 6d;
            shapePathHeight = 6d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="1" />
            //</moveTo>
            currentPoint = new EmuPoint(0, 1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="3" hR="1" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3d, 1d, cd2, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="6" y="5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 6, 5);
            //<arcTo wR="3" hR="1" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3d, 1d, 0d, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="l" t="hd3" r="r" b="y3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, h / 3, r, y3);

            return shapePaths;
        }
    }


}

