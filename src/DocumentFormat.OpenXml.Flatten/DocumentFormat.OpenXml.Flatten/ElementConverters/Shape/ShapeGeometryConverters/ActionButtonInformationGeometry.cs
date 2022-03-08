using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{

    /// <summary>
    /// 动作按钮: 获取信息
    /// </summary>
    public class ActionButtonInformationGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="dx2" fmla="*/ ss 3 8" />
            //  <gd name="g9" fmla="+- vc 0 dx2" />
            //  <gd name="g11" fmla="+- hc 0 dx2" />
            //  <gd name="g13" fmla="*/ ss 3 4" />
            //  <gd name="g14" fmla="*/ g13 1 32" />
            //  <gd name="g17" fmla="*/ g13 5 16" />
            //  <gd name="g18" fmla="*/ g13 3 8" />
            //  <gd name="g19" fmla="*/ g13 13 32" />
            //  <gd name="g20" fmla="*/ g13 19 32" />
            //  <gd name="g22" fmla="*/ g13 11 16" />
            //  <gd name="g23" fmla="*/ g13 13 16" />
            //  <gd name="g24" fmla="*/ g13 7 8" />
            //  <gd name="g25" fmla="+- g9 g14 0" />
            //  <gd name="g28" fmla="+- g9 g17 0" />
            //  <gd name="g29" fmla="+- g9 g18 0" />
            //  <gd name="g30" fmla="+- g9 g23 0" />
            //  <gd name="g31" fmla="+- g9 g24 0" />
            //  <gd name="g32" fmla="+- g11 g17 0" />
            //  <gd name="g34" fmla="+- g11 g19 0" />
            //  <gd name="g35" fmla="+- g11 g20 0" />
            //  <gd name="g37" fmla="+- g11 g22 0" />
            //  <gd name="g38" fmla="*/ g13 3 32" />
            //</gdLst>

            //<gd name="dx2" fmla="*/ ss 3 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = ss * 3 / 8;
            //<gd name="g9" fmla="+- vc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g9 = vc + 0 - dx2;
            //<gd name="g11" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g11 = hc + 0 - dx2;
            //<gd name="g13" fmla="*/ ss 3 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g13 = ss * 3 / 4;
            //<gd name="g14" fmla="*/ g13 1 32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g14 = g13 * 1 / 32;
            //<gd name="g17" fmla="*/ g13 5 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g17 = g13 * 5 / 16;
            //<gd name="g18" fmla="*/ g13 3 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g18 = g13 * 3 / 8;
            //<gd name="g19" fmla="*/ g13 13 32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g19 = g13 * 13 / 32;
            //<gd name="g20" fmla="*/ g13 19 32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g20 = g13 * 19 / 32;
            //<gd name="g22" fmla="*/ g13 11 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g22 = g13 * 11 / 16;
            //<gd name="g23" fmla="*/ g13 13 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g23 = g13 * 13 / 16;
            //<gd name="g24" fmla="*/ g13 7 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g24 = g13 * 7 / 8;
            //<gd name="g25" fmla="+- g9 g14 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g25 = g9 + g14 - 0;
            //<gd name="g28" fmla="+- g9 g17 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g28 = g9 + g17 - 0;
            //<gd name="g29" fmla="+- g9 g18 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g29 = g9 + g18 - 0;
            //<gd name="g30" fmla="+- g9 g23 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g30 = g9 + g23 - 0;
            //<gd name="g31" fmla="+- g9 g24 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g31 = g9 + g24 - 0;
            //<gd name="g32" fmla="+- g11 g17 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g32 = g11 + g17 - 0;
            //<gd name="g34" fmla="+- g11 g19 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g34 = g11 + g19 - 0;
            //<gd name="g35" fmla="+- g11 g20 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g35 = g11 + g20 - 0;
            //<gd name="g37" fmla="+- g11 g22 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g37 = g11 + g22 - 0;
            //<gd name="g38" fmla="*/ g13 3 32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g38 = g13 * 3 / 32;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="g9" />
            //    </moveTo>
            //    <arcTo wR="dx2" hR="dx2" stAng="3cd4" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darken" extrusionOk="false">
            //    <moveTo>
            //      <pt x="hc" y="g9" />
            //    </moveTo>
            //    <arcTo wR="dx2" hR="dx2" stAng="3cd4" swAng="21600000" />
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="g25" />
            //    </moveTo>
            //    <arcTo wR="g38" hR="g38" stAng="3cd4" swAng="21600000" />
            //    <moveTo>
            //      <pt x="g32" y="g28" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g32" y="g29" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g29" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g31" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g31" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g28" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="lighten" extrusionOk="false">
            //    <moveTo>
            //      <pt x="hc" y="g25" />
            //    </moveTo>
            //    <arcTo wR="g38" hR="g38" stAng="3cd4" swAng="21600000" />
            //    <moveTo>
            //      <pt x="g32" y="g28" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g35" y="g28" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g31" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g31" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g29" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g29" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="hc" y="g9" />
            //    </moveTo>
            //    <arcTo wR="dx2" hR="dx2" stAng="3cd4" swAng="21600000" />
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="g25" />
            //    </moveTo>
            //    <arcTo wR="g38" hR="g38" stAng="3cd4" swAng="21600000" />
            //    <moveTo>
            //      <pt x="g32" y="g28" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g35" y="g28" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g31" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g31" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g29" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g29" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[5];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="t" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g9);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="dx2" hR="dx2" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, dx2, dx2, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darken"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g9);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="dx2" hR="dx2" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, dx2, dx2, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g25" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g25);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g38" hR="g38" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g38, g38, 3 * cd4, 21600000d);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g28" />
            //</moveTo>
            currentPoint = new EmuPoint(g32, g28);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g29);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g29);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g31" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g31);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g31" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g31);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g28" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g28);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.Darken, isStroke: false);


            // <path stroke="false"fill="lighten"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g25" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g25);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g38" hR="g38" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g38, g38, 3 * cd4, 21600000d);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g28" />
            //</moveTo>
            currentPoint = new EmuPoint(g32, g28);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g28" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g28);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g31" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g31);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g31" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g31);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g29);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g29);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.Lighten, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g9);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="dx2" hR="dx2" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, dx2, dx2, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g25" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g25);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g38" hR="g38" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g38, g38, 3 * cd4, 21600000d);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g28" />
            //</moveTo>
            currentPoint = new EmuPoint(g32, g28);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g28" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g28);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g31" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g31);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g31" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g31);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g29);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g29);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[3] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            // <path fill="none">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="t" />
            //</moveTo>
            currentPoint = new EmuPoint(l, t);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[4] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }


}

