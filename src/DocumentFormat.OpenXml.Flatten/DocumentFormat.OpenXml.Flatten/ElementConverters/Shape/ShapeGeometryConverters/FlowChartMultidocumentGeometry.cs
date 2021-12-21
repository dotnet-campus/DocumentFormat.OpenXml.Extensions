using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 多文档
    /// </summary>
    public class FlowChartMultidocumentGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="y2" fmla="*/ h 3675 21600" />
            //  <gd name="y8" fmla="*/ h 20782 21600" />
            //  <gd name="x3" fmla="*/ w 9298 21600" />
            //  <gd name="x4" fmla="*/ w 12286 21600" />
            //  <gd name="x5" fmla="*/ w 18595 21600" />
            //</gdLst>

            //<gd name="y2" fmla="*/ h 3675 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = h * 3675 / 21600;
            //<gd name="y8" fmla="*/ h 20782 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y8 = h * 20782 / 21600;
            //<gd name="x3" fmla="*/ w 9298 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = w * 9298 / 21600;
            //<gd name="x4" fmla="*/ w 12286 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = w * 12286 / 21600;
            //<gd name="x5" fmla="*/ w 18595 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = w * 18595 / 21600;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false" w="21600" h="21600">
            //    <moveTo>
            //      <pt x="0" y="20782" />
            //    </moveTo>
            //    <cubicBezTo>
            //      <pt x="9298" y="23542" />
            //      <pt x="9298" y="18022" />
            //      <pt x="18595" y="18022" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="18595" y="3675" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="3675" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="1532" y="3675" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1532" y="1815" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="20000" y="1815" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="20000" y="16252" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="19298" y="16252" />
            //      <pt x="18595" y="16352" />
            //      <pt x="18595" y="16352" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="18595" y="3675" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="2972" y="1815" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="2972" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="14392" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="20800" y="14392" />
            //      <pt x="20000" y="14467" />
            //      <pt x="20000" y="14467" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="20000" y="1815" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false" w="21600" h="21600">
            //    <moveTo>
            //      <pt x="0" y="3675" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="18595" y="3675" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="18595" y="18022" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="9298" y="18022" />
            //      <pt x="9298" y="23542" />
            //      <pt x="0" y="20782" />
            //    </cubicBezTo>
            //    <close />
            //    <moveTo>
            //      <pt x="1532" y="3675" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1532" y="1815" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="20000" y="1815" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="20000" y="16252" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="19298" y="16252" />
            //      <pt x="18595" y="16352" />
            //      <pt x="18595" y="16352" />
            //    </cubicBezTo>
            //    <moveTo>
            //      <pt x="2972" y="1815" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="2972" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="14392" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="20800" y="14392" />
            //      <pt x="20000" y="14467" />
            //      <pt x="20000" y="14467" />
            //    </cubicBezTo>
            //  </path>
            //  <path stroke="false" fill="none" w="21600" h="21600">
            //    <moveTo>
            //      <pt x="0" y="20782" />
            //    </moveTo>
            //    <cubicBezTo>
            //      <pt x="9298" y="23542" />
            //      <pt x="9298" y="18022" />
            //      <pt x="18595" y="18022" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="18595" y="16352" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="18595" y="16352" />
            //      <pt x="19298" y="16252" />
            //      <pt x="20000" y="16252" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="20000" y="14467" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="20000" y="14467" />
            //      <pt x="20800" y="14392" />
            //      <pt x="21600" y="14392" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="21600" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="2972" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="2972" y="1815" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1532" y="1815" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1532" y="3675" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="3675" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false"w="21600"h="21600">
            var shapePathWidth = 21600d;
            var shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="20782" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 20782);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="9298" y="23542" />
            //  <pt x="9298" y="18022" />
            //  <pt x="18595" y="18022" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 9298, 23542, 9298, 18022, 18595, 18022);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18595" y="3675" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18595, 3675);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="3675" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 0, 3675);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1532" y="3675" />
            //</moveTo>
            currentPoint = new EmuPoint(1532, 3675);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1532" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1532, 1815);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 20000, 1815);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="16252" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 20000, 16252);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="19298" y="16252" />
            //  <pt x="18595" y="16352" />
            //  <pt x="18595" y="16352" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 19298, 16252, 18595, 16352, 18595, 16352);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18595" y="3675" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18595, 3675);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2972" y="1815" />
            //</moveTo>
            currentPoint = new EmuPoint(2972, 1815);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2972" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2972, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="14392" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 14392);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20800" y="14392" />
            //  <pt x="20000" y="14467" />
            //  <pt x="20000" y="14467" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 20800, 14392, 20000, 14467, 20000, 14467);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 20000, 1815);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            // <path fill="none"extrusionOk="false"w="21600"h="21600">
            shapePathWidth = 21600d;
            shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="3675" />
            //</moveTo>
            currentPoint = new EmuPoint(0, 3675);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18595" y="3675" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18595, 3675);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18595" y="18022" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18595, 18022);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="9298" y="18022" />
            //  <pt x="9298" y="23542" />
            //  <pt x="0" y="20782" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 9298, 18022, 9298, 23542, 0, 20782);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1532" y="3675" />
            //</moveTo>
            currentPoint = new EmuPoint(1532, 3675);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1532" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1532, 1815);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 20000, 1815);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="16252" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 20000, 16252);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="19298" y="16252" />
            //  <pt x="18595" y="16352" />
            //  <pt x="18595" y="16352" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 19298, 16252, 18595, 16352, 18595, 16352);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2972" y="1815" />
            //</moveTo>
            currentPoint = new EmuPoint(2972, 1815);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2972" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2972, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="14392" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 14392);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20800" y="14392" />
            //  <pt x="20000" y="14467" />
            //  <pt x="20000" y="14467" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 20800, 14392, 20000, 14467, 20000, 14467);
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            // <path stroke="false"fill="none"w="21600"h="21600">
            shapePathWidth = 21600d;
            shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="20782" />
            //</moveTo>
            currentPoint = new EmuPoint(0, 20782);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="9298" y="23542" />
            //  <pt x="9298" y="18022" />
            //  <pt x="18595" y="18022" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 9298, 23542, 9298, 18022, 18595, 18022);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18595" y="16352" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18595, 16352);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18595" y="16352" />
            //  <pt x="19298" y="16252" />
            //  <pt x="20000" y="16252" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 18595, 16352, 19298, 16252, 20000, 16252);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="14467" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 20000, 14467);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="20000" y="14467" />
            //  <pt x="20800" y="14392" />
            //  <pt x="21600" y="14392" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, 20000, 14467, 20800, 14392, 21600, 14392);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2972" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2972, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2972" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2972, 1815);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1532" y="1815" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1532, 1815);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1532" y="3675" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1532, 3675);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="3675" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 0, 3675);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, isStroke: false, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="l" t="y2" r="x5" b="y8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, y2, x5, y8);

            return shapePaths;
        }
    }


}

