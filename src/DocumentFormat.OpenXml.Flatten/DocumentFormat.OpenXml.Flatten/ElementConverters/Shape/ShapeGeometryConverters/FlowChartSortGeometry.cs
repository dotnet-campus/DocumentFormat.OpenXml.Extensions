﻿using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 排序
    /// </summary>
    public class FlowChartSortGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="ir" fmla="*/ w 3 4" />
            //  <gd name="ib" fmla="*/ h 3 4" />
            //</gdLst>

            //<gd name="ir" fmla="*/ w 3 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = w * 3 / 4;
            //<gd name="ib" fmla="*/ h 3 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = h * 3 / 4;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false" w="2" h="2">
            //    <moveTo>
            //      <pt x="0" y="1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="2" y="1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1" y="2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false" w="2" h="2">
            //    <moveTo>
            //      <pt x="0" y="1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="2" y="1" />
            //    </lnTo>
            //  </path>
            //  <path fill="none" w="2" h="2">
            //    <moveTo>
            //      <pt x="0" y="1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="2" y="1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1" y="2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            //设置Emu转Pixel的精度为小数点4位，防止精度不够被转为0
            UnitPrecision = 4;
            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false"w="2"h="2">
            var shapePathWidth = 2d;
            var shapePathHeight = 2d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="1" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2" y="1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2, 1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            // <path fill="none"extrusionOk="false"w="2"h="2">
            shapePathWidth = 2d;
            shapePathHeight = 2d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="1" />
            //</moveTo>
            currentPoint = new EmuPoint(0, 1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2" y="1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2, 1);
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            // <path fill="none"w="2"h="2">
            shapePathWidth = 2d;
            shapePathHeight = 2d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="1" />
            //</moveTo>
            currentPoint = new EmuPoint(0, 1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="2" y="1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 2, 1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="wd4" t="hd4" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(wd4, hd4, ir, ib);

            return shapePaths;
        }
    }


}

