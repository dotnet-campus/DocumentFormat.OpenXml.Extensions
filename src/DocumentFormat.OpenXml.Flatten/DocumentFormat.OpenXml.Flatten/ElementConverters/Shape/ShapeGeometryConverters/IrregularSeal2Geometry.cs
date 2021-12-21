using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 爆炸形: 14 pt
    /// </summary>
    public class IrregularSeal2Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x2" fmla="*/ w 9722 21600" />
            //  <gd name="x5" fmla="*/ w 5372 21600" />
            //  <gd name="x16" fmla="*/ w 11612 21600" />
            //  <gd name="x19" fmla="*/ w 14640 21600" />
            //  <gd name="y2" fmla="*/ h 1887 21600" />
            //  <gd name="y3" fmla="*/ h 6382 21600" />
            //  <gd name="y8" fmla="*/ h 12877 21600" />
            //  <gd name="y14" fmla="*/ h 19712 21600" />
            //  <gd name="y16" fmla="*/ h 18842 21600" />
            //  <gd name="y17" fmla="*/ h 15935 21600" />
            //  <gd name="y24" fmla="*/ h 6645 21600" />
            //</gdLst>

            //<gd name="x2" fmla="*/ w 9722 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = w * 9722 / 21600;
            //<gd name="x5" fmla="*/ w 5372 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = w * 5372 / 21600;
            //<gd name="x16" fmla="*/ w 11612 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x16 = w * 11612 / 21600;
            //<gd name="x19" fmla="*/ w 14640 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x19 = w * 14640 / 21600;
            //<gd name="y2" fmla="*/ h 1887 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = h * 1887 / 21600;
            //<gd name="y3" fmla="*/ h 6382 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = h * 6382 / 21600;
            //<gd name="y8" fmla="*/ h 12877 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y8 = h * 12877 / 21600;
            //<gd name="y14" fmla="*/ h 19712 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y14 = h * 19712 / 21600;
            //<gd name="y16" fmla="*/ h 18842 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y16 = h * 18842 / 21600;
            //<gd name="y17" fmla="*/ h 15935 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y17 = h * 15935 / 21600;
            //<gd name="y24" fmla="*/ h 6645 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y24 = h * 6645 / 21600;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="21600" h="21600">
            //    <moveTo>
            //      <pt x="11462" y="4342" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="14790" y="0" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="14525" y="5777" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="18007" y="3172" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="16380" y="6532" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="6645" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="16985" y="9402" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="18270" y="11290" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="16380" y="12310" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="18877" y="15632" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="14640" y="14350" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="14942" y="17370" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="12180" y="15935" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="11612" y="18842" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="9872" y="17370" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="8700" y="19712" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="7527" y="18125" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="4917" y="21600" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="4805" y="18240" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1285" y="17825" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="3330" y="15370" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="12877" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="3935" y="11592" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="1172" y="8270" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="5372" y="7817" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="4502" y="3625" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="8550" y="6382" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="9722" y="1887" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path w="21600"h="21600">
            var shapePathWidth = 21600d;
            var shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="11462" y="4342" />
            //</moveTo>
            var currentPoint = new EmuPoint(11462, 4342);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14790" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14790, 0);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14525" y="5777" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14525, 5777);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18007" y="3172" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18007, 3172);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="16380" y="6532" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 16380, 6532);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="21600" y="6645" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 21600, 6645);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="16985" y="9402" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 16985, 9402);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18270" y="11290" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18270, 11290);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="16380" y="12310" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 16380, 12310);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18877" y="15632" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18877, 15632);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14640" y="14350" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14640, 14350);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="14942" y="17370" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 14942, 17370);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="12180" y="15935" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 12180, 15935);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="11612" y="18842" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 11612, 18842);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="9872" y="17370" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 9872, 17370);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="8700" y="19712" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 8700, 19712);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="7527" y="18125" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 7527, 18125);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="4917" y="21600" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 4917, 21600);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="4805" y="18240" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 4805, 18240);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1285" y="17825" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1285, 17825);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="3330" y="15370" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 3330, 15370);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="0" y="12877" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 0, 12877);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="3935" y="11592" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 3935, 11592);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="1172" y="8270" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 1172, 8270);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="5372" y="7817" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 5372, 7817);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="4502" y="3625" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 4502, 3625);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="8550" y="6382" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 8550, 6382);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="9722" y="1887" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 9722, 1887);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="x5" t="y3" r="x19" b="y17" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x5, y3, x19, y17);

            return shapePaths;
        }
    }


}

