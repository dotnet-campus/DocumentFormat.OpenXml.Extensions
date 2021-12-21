using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 爆炸形: 8 pt
    /// </summary>
    public class IrregularSeal1Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x5" fmla="*/ w 4627 21600" />
            //  <gd name="x12" fmla="*/ w 8485 21600" />
            //  <gd name="x21" fmla="*/ w 16702 21600" />
            //  <gd name="x24" fmla="*/ w 14522 21600" />
            //  <gd name="y3" fmla="*/ h 6320 21600" />
            //  <gd name="y6" fmla="*/ h 8615 21600" />
            //  <gd name="y9" fmla="*/ h 13937 21600" />
            //  <gd name="y18" fmla="*/ h 13290 21600" />
            //</gdLst>

            //<gd name="x5" fmla="*/ w 4627 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = w * 4627 / 21600;
            //<gd name="x12" fmla="*/ w 8485 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x12 = w * 8485 / 21600;
            //<gd name="x21" fmla="*/ w 16702 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x21 = w * 16702 / 21600;
            //<gd name="x24" fmla="*/ w 14522 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x24 = w * 14522 / 21600;
            //<gd name="y3" fmla="*/ h 6320 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = h * 6320 / 21600;
            //<gd name="y6" fmla="*/ h 8615 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = h * 8615 / 21600;
            //<gd name="y9" fmla="*/ h 13937 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y9 = h * 13937 / 21600;
            //<gd name="y18" fmla="*/ h 13290 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y18 = h * 13290 / 21600;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="21600" h="21600">
            //    <moveTo>
            //      <pt x="10800" y="5800" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="14522" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="14155" y="5325" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="18380" y="4457" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="16702" y="7315" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21097" y="8137" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="17607" y="10475" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="13290" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="16837" y="12942" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="18145" y="18095" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="14020" y="14457" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="13247" y="19737" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="10532" y="14935" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="8485" y="21600" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="7715" y="15627" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="4762" y="17617" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="5667" y="13937" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="135" y="14587" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="3722" y="11775" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="8615" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="4627" y="7617" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="370" y="2295" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="7312" y="6320" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="8352" y="2295" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path w="21600"h="21600">
            var shapePathWidth = 21600d;
            var shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="10800" y="5800" />
            //</moveTo>
            var currentPoint = new EmuPoint(10800, 5800);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14522" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14522, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14155" y="5325" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14155, 5325);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18380" y="4457" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18380, 4457);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="16702" y="7315" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 16702, 7315);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21097" y="8137" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21097, 8137);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="17607" y="10475" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 17607, 10475);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="13290" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 13290);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="16837" y="12942" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 16837, 12942);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18145" y="18095" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18145, 18095);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14020" y="14457" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14020, 14457);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="13247" y="19737" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 13247, 19737);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="10532" y="14935" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 10532, 14935);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="8485" y="21600" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 8485, 21600);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="7715" y="15627" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 7715, 15627);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="4762" y="17617" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 4762, 17617);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="5667" y="13937" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 5667, 13937);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="135" y="14587" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 135, 14587);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="3722" y="11775" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 3722, 11775);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="8615" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 0, 8615);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="4627" y="7617" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 4627, 7617);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="370" y="2295" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 370, 2295);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="7312" y="6320" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 7312, 6320);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="8352" y="2295" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 8352, 2295);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="x5" t="y3" r="x21" b="y9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x5, y3, x21, y9);

            return shapePaths;
        }
    }


}

