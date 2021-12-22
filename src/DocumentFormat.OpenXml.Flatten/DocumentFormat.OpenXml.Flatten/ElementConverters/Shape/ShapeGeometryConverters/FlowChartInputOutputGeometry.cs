using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 数据
    /// </summary>
    public class FlowChartInputOutputGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x3" fmla="*/ w 2 5" />
            //  <gd name="x4" fmla="*/ w 3 5" />
            //  <gd name="x5" fmla="*/ w 4 5" />
            //  <gd name="x6" fmla="*/ w 9 10" />
            //</gdLst>

            //<gd name="x3" fmla="*/ w 2 5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = w * 2 / 5;
            //<gd name="x4" fmla="*/ w 3 5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = w * 3 / 5;
            //<gd name="x5" fmla="*/ w 4 5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = w * 4 / 5;
            //<gd name="x6" fmla="*/ w 9 10" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = w * 9 / 10;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="5" h="5">
            //    <moveTo>
            //      <pt x="0" y="5" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="1" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="5" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="4" y="5" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            //设置Emu转Pixel的精度为小数点4位，防止精度不够被转为0
            UnitPrecision = 4;
            var shapePaths = new ShapePath[1];

            // <path w="5"h="5">
            var shapePathWidth = 5d;
            var shapePathHeight = 5d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="5" />
            //</moveTo>
            var currentPoint = new EmuPoint(0, 5);
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
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="4" y="5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 4, 5);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="wd5" t="t" r="x5" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(wd5, t, x5, b);

            return shapePaths;
        }
    }


}

