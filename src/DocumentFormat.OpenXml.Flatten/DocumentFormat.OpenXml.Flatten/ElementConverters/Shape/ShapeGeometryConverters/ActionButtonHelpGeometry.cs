using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 动作按钮: 帮助
    /// </summary>
    public class ActionButtonHelpGeometry : ShapeGeometryBase
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
            //  <gd name="g14" fmla="*/ g13 1 7" />
            //  <gd name="g15" fmla="*/ g13 3 14" />
            //  <gd name="g16" fmla="*/ g13 2 7" />
            //  <gd name="g19" fmla="*/ g13 3 7" />
            //  <gd name="g20" fmla="*/ g13 4 7" />
            //  <gd name="g21" fmla="*/ g13 17 28" />
            //  <gd name="g23" fmla="*/ g13 21 28" />
            //  <gd name="g24" fmla="*/ g13 11 14" />
            //  <gd name="g27" fmla="+- g9 g16 0" />
            //  <gd name="g29" fmla="+- g9 g21 0" />
            //  <gd name="g30" fmla="+- g9 g23 0" />
            //  <gd name="g31" fmla="+- g9 g24 0" />
            //  <gd name="g33" fmla="+- g11 g15 0" />
            //  <gd name="g36" fmla="+- g11 g19 0" />
            //  <gd name="g37" fmla="+- g11 g20 0" />
            //  <gd name="g41" fmla="*/ g13 1 14" />
            //  <gd name="g42" fmla="*/ g13 3 28" />
            //</gdLst>

            //<gd name="dx2" fmla="*/ ss 3 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = ss * 3 / 8;
            //<gd name="g9" fmla="+- vc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g9 = vc + 0 - dx2;
            //<gd name="g11" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g11 = hc + 0 - dx2;
            //<gd name="g13" fmla="*/ ss 3 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g13 = ss * 3 / 4;
            //<gd name="g14" fmla="*/ g13 1 7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g14 = g13 * 1 / 7;
            //<gd name="g15" fmla="*/ g13 3 14" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g15 = g13 * 3 / 14;
            //<gd name="g16" fmla="*/ g13 2 7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g16 = g13 * 2 / 7;
            //<gd name="g19" fmla="*/ g13 3 7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g19 = g13 * 3 / 7;
            //<gd name="g20" fmla="*/ g13 4 7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g20 = g13 * 4 / 7;
            //<gd name="g21" fmla="*/ g13 17 28" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g21 = g13 * 17 / 28;
            //<gd name="g23" fmla="*/ g13 21 28" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g23 = g13 * 21 / 28;
            //<gd name="g24" fmla="*/ g13 11 14" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g24 = g13 * 11 / 14;
            //<gd name="g27" fmla="+- g9 g16 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g27 = g9 + g16 - 0;
            //<gd name="g29" fmla="+- g9 g21 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g29 = g9 + g21 - 0;
            //<gd name="g30" fmla="+- g9 g23 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g30 = g9 + g23 - 0;
            //<gd name="g31" fmla="+- g9 g24 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g31 = g9 + g24 - 0;
            //<gd name="g33" fmla="+- g11 g15 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g33 = g11 + g15 - 0;
            //<gd name="g36" fmla="+- g11 g19 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g36 = g11 + g19 - 0;
            //<gd name="g37" fmla="+- g11 g20 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g37 = g11 + g20 - 0;
            //<gd name="g41" fmla="*/ g13 1 14" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g41 = g13 * 1 / 14;
            //<gd name="g42" fmla="*/ g13 3 28" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g42 = g13 * 3 / 28;

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
            //      <pt x="g33" y="g27" />
            //    </moveTo>
            //    <arcTo wR="g16" hR="g16" stAng="cd2" swAng="cd2" />
            //    <arcTo wR="g14" hR="g15" stAng="0" swAng="cd4" />
            //    <arcTo wR="g41" hR="g42" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="g37" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g29" />
            //    </lnTo>
            //    <arcTo wR="g14" hR="g15" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="g41" hR="g42" stAng="cd4" swAng="-5400000" />
            //    <arcTo wR="g14" hR="g14" stAng="0" swAng="-10800000" />
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="g31" />
            //    </moveTo>
            //    <arcTo wR="g42" hR="g42" stAng="3cd4" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darken" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g33" y="g27" />
            //    </moveTo>
            //    <arcTo wR="g16" hR="g16" stAng="cd2" swAng="cd2" />
            //    <arcTo wR="g14" hR="g15" stAng="0" swAng="cd4" />
            //    <arcTo wR="g41" hR="g42" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="g37" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g29" />
            //    </lnTo>
            //    <arcTo wR="g14" hR="g15" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="g41" hR="g42" stAng="cd4" swAng="-5400000" />
            //    <arcTo wR="g14" hR="g14" stAng="0" swAng="-10800000" />
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="g31" />
            //    </moveTo>
            //    <arcTo wR="g42" hR="g42" stAng="3cd4" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g33" y="g27" />
            //    </moveTo>
            //    <arcTo wR="g16" hR="g16" stAng="cd2" swAng="cd2" />
            //    <arcTo wR="g14" hR="g15" stAng="0" swAng="cd4" />
            //    <arcTo wR="g41" hR="g42" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="g37" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g30" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g29" />
            //    </lnTo>
            //    <arcTo wR="g14" hR="g15" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="g41" hR="g42" stAng="cd4" swAng="-5400000" />
            //    <arcTo wR="g14" hR="g14" stAng="0" swAng="-10800000" />
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="g31" />
            //    </moveTo>
            //    <arcTo wR="g42" hR="g42" stAng="3cd4" swAng="21600000" />
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

            var shapePaths = new ShapePath[4];

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
            //  <pt x="g33" y="g27" />
            //</moveTo>
            currentPoint = new EmuPoint(g33, g27);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g16" hR="g16" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g16, g16, cd2, cd2);
            //<arcTo wR="g14" hR="g15" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g15, 0d, cd4);
            //<arcTo wR="g41" hR="g42" stAng="3cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g41, g42, 3 * cd4, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g29);
            //<arcTo wR="g14" hR="g15" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g15, cd2, cd4);
            //<arcTo wR="g41" hR="g42" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g41, g42, cd4, -5400000d);
            //<arcTo wR="g14" hR="g14" stAng="0" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g14, 0d, -10800000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g31" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g31);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g42" hR="g42" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g42, g42, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darken"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g27" />
            //</moveTo>
            currentPoint = new EmuPoint(g33, g27);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g16" hR="g16" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g16, g16, cd2, cd2);
            //<arcTo wR="g14" hR="g15" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g15, 0d, cd4);
            //<arcTo wR="g41" hR="g42" stAng="3cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g41, g42, 3 * cd4, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g29);
            //<arcTo wR="g14" hR="g15" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g15, cd2, cd4);
            //<arcTo wR="g41" hR="g42" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g41, g42, cd4, -5400000d);
            //<arcTo wR="g14" hR="g14" stAng="0" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g14, 0d, -10800000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g31" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g31);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g42" hR="g42" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g42, g42, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.Darken, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g27" />
            //</moveTo>
            currentPoint = new EmuPoint(g33, g27);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g16" hR="g16" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g16, g16, cd2, cd2);
            //<arcTo wR="g14" hR="g15" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g15, 0d, cd4);
            //<arcTo wR="g41" hR="g42" stAng="3cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g41, g42, 3 * cd4, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g30" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g30);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g29" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g29);
            //<arcTo wR="g14" hR="g15" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g15, cd2, cd4);
            //<arcTo wR="g41" hR="g42" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g41, g42, cd4, -5400000d);
            //<arcTo wR="g14" hR="g14" stAng="0" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g14, g14, 0d, -10800000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="g31" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, g31);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="g42" hR="g42" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, g42, g42, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


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
            shapePaths[3] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }


}

