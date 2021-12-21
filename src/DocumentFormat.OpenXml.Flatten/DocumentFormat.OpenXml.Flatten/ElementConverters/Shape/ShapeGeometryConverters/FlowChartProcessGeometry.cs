﻿using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 过程
    /// </summary>
    public class FlowChartProcessGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="1" h="1">
            //    <moveTo>
            //      <pt x="0" y="0" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1" y="1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="1" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //设置Emu转Pixel的精度为小数点4位，防止精度不够被转为0
            UnitPrecision = 4;

            var shapePaths = new ShapePath[1];

            // <path w="1"h="1">
            var shapePathWidth = 1d;
            var shapePathHeight = 1d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="0" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 0);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1" y="1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1, 1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 0, 1);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }


}

