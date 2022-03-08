using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 动作按钮: 视频
    /// </summary>
    public class ActionButtonMovieGeometry : ShapeGeometryBase
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
            //  <gd name="g10" fmla="+- vc dx2 0" />
            //  <gd name="g11" fmla="+- hc 0 dx2" />
            //  <gd name="g12" fmla="+- hc dx2 0" />
            //  <gd name="g13" fmla="*/ ss 3 4" />
            //  <gd name="g14" fmla="*/ g13 1455 21600" />
            //  <gd name="g15" fmla="*/ g13 1905 21600" />
            //  <gd name="g16" fmla="*/ g13 2325 21600" />
            //  <gd name="g17" fmla="*/ g13 16155 21600" />
            //  <gd name="g18" fmla="*/ g13 17010 21600" />
            //  <gd name="g19" fmla="*/ g13 19335 21600" />
            //  <gd name="g20" fmla="*/ g13 19725 21600" />
            //  <gd name="g21" fmla="*/ g13 20595 21600" />
            //  <gd name="g22" fmla="*/ g13 5280 21600" />
            //  <gd name="g23" fmla="*/ g13 5730 21600" />
            //  <gd name="g24" fmla="*/ g13 6630 21600" />
            //  <gd name="g25" fmla="*/ g13 7492 21600" />
            //  <gd name="g26" fmla="*/ g13 9067 21600" />
            //  <gd name="g27" fmla="*/ g13 9555 21600" />
            //  <gd name="g28" fmla="*/ g13 13342 21600" />
            //  <gd name="g29" fmla="*/ g13 14580 21600" />
            //  <gd name="g30" fmla="*/ g13 15592 21600" />
            //  <gd name="g31" fmla="+- g11 g14 0" />
            //  <gd name="g32" fmla="+- g11 g15 0" />
            //  <gd name="g33" fmla="+- g11 g16 0" />
            //  <gd name="g34" fmla="+- g11 g17 0" />
            //  <gd name="g35" fmla="+- g11 g18 0" />
            //  <gd name="g36" fmla="+- g11 g19 0" />
            //  <gd name="g37" fmla="+- g11 g20 0" />
            //  <gd name="g38" fmla="+- g11 g21 0" />
            //  <gd name="g39" fmla="+- g9 g22 0" />
            //  <gd name="g40" fmla="+- g9 g23 0" />
            //  <gd name="g41" fmla="+- g9 g24 0" />
            //  <gd name="g42" fmla="+- g9 g25 0" />
            //  <gd name="g43" fmla="+- g9 g26 0" />
            //  <gd name="g44" fmla="+- g9 g27 0" />
            //  <gd name="g45" fmla="+- g9 g28 0" />
            //  <gd name="g46" fmla="+- g9 g29 0" />
            //  <gd name="g47" fmla="+- g9 g30 0" />
            //  <gd name="g48" fmla="+- g9 g31 0" />
            //</gdLst>

            //<gd name="dx2" fmla="*/ ss 3 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = ss * 3 / 8;
            //<gd name="g9" fmla="+- vc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g9 = vc + 0 - dx2;
            //<gd name="g10" fmla="+- vc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g10 = vc + dx2 - 0;
            //<gd name="g11" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g11 = hc + 0 - dx2;
            //<gd name="g12" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g12 = hc + dx2 - 0;
            //<gd name="g13" fmla="*/ ss 3 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g13 = ss * 3 / 4;
            //<gd name="g14" fmla="*/ g13 1455 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g14 = g13 * 1455 / 21600;
            //<gd name="g15" fmla="*/ g13 1905 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g15 = g13 * 1905 / 21600;
            //<gd name="g16" fmla="*/ g13 2325 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g16 = g13 * 2325 / 21600;
            //<gd name="g17" fmla="*/ g13 16155 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g17 = g13 * 16155 / 21600;
            //<gd name="g18" fmla="*/ g13 17010 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g18 = g13 * 17010 / 21600;
            //<gd name="g19" fmla="*/ g13 19335 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g19 = g13 * 19335 / 21600;
            //<gd name="g20" fmla="*/ g13 19725 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g20 = g13 * 19725 / 21600;
            //<gd name="g21" fmla="*/ g13 20595 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g21 = g13 * 20595 / 21600;
            //<gd name="g22" fmla="*/ g13 5280 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g22 = g13 * 5280 / 21600;
            //<gd name="g23" fmla="*/ g13 5730 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g23 = g13 * 5730 / 21600;
            //<gd name="g24" fmla="*/ g13 6630 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g24 = g13 * 6630 / 21600;
            //<gd name="g25" fmla="*/ g13 7492 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g25 = g13 * 7492 / 21600;
            //<gd name="g26" fmla="*/ g13 9067 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g26 = g13 * 9067 / 21600;
            //<gd name="g27" fmla="*/ g13 9555 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g27 = g13 * 9555 / 21600;
            //<gd name="g28" fmla="*/ g13 13342 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g28 = g13 * 13342 / 21600;
            //<gd name="g29" fmla="*/ g13 14580 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g29 = g13 * 14580 / 21600;
            //<gd name="g30" fmla="*/ g13 15592 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g30 = g13 * 15592 / 21600;
            //<gd name="g31" fmla="+- g11 g14 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g31 = g11 + g14 - 0;
            //<gd name="g32" fmla="+- g11 g15 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g32 = g11 + g15 - 0;
            //<gd name="g33" fmla="+- g11 g16 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g33 = g11 + g16 - 0;
            //<gd name="g34" fmla="+- g11 g17 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g34 = g11 + g17 - 0;
            //<gd name="g35" fmla="+- g11 g18 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g35 = g11 + g18 - 0;
            //<gd name="g36" fmla="+- g11 g19 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g36 = g11 + g19 - 0;
            //<gd name="g37" fmla="+- g11 g20 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g37 = g11 + g20 - 0;
            //<gd name="g38" fmla="+- g11 g21 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g38 = g11 + g21 - 0;
            //<gd name="g39" fmla="+- g9 g22 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g39 = g9 + g22 - 0;
            //<gd name="g40" fmla="+- g9 g23 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g40 = g9 + g23 - 0;
            //<gd name="g41" fmla="+- g9 g24 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g41 = g9 + g24 - 0;
            //<gd name="g42" fmla="+- g9 g25 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g42 = g9 + g25 - 0;
            //<gd name="g43" fmla="+- g9 g26 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g43 = g9 + g26 - 0;
            //<gd name="g44" fmla="+- g9 g27 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g44 = g9 + g27 - 0;
            //<gd name="g45" fmla="+- g9 g28 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g45 = g9 + g28 - 0;
            //<gd name="g46" fmla="+- g9 g29 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g46 = g9 + g29 - 0;
            //<gd name="g47" fmla="+- g9 g30 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g47 = g9 + g30 - 0;
            //<gd name="g48" fmla="+- g9 g31 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g48 = g9 + g31 - 0;

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
            //      <pt x="g11" y="g39" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g11" y="g44" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g31" y="g44" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g43" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g33" y="g43" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g33" y="g47" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g47" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g45" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g45" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g38" y="g46" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g46" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g38" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g42" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g42" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g40" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g40" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g31" y="g39" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darken" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g11" y="g39" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g11" y="g44" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g31" y="g44" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g43" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g33" y="g43" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g33" y="g47" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g47" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g45" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g45" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g38" y="g46" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g46" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g38" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g42" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g42" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g40" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g40" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g31" y="g39" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g11" y="g39" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g31" y="g39" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g40" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g34" y="g40" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g42" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g37" y="g42" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g38" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g41" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g46" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g38" y="g46" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g36" y="g45" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g45" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g35" y="g47" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g33" y="g47" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g33" y="g43" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g32" y="g43" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g31" y="g44" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g11" y="g44" />
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
            //  <pt x="g11" y="g39" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g39);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g44" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g44);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g31" y="g44" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g31, g44);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g43" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g43);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g43" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g33, g43);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g47" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g33, g47);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g47" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g47);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g45" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g45);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g45" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g45);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g38" y="g46" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g38, g46);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g46" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g46);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g38" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g38, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g42" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g42);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g42" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g42);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g40" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g40);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g40" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g40);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g31" y="g39" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g31, g39);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darken"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g39" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g39);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g44" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g44);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g31" y="g44" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g31, g44);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g43" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g43);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g43" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g33, g43);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g47" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g33, g47);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g47" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g47);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g45" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g45);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g45" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g45);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g38" y="g46" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g38, g46);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g46" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g46);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g38" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g38, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g42" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g42);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g42" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g42);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g40" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g40);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g40" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g40);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g31" y="g39" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g31, g39);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.Darken, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g39" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g39);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g31" y="g39" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g31, g39);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g40" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g40);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g34" y="g40" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g34, g40);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g42" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g42);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g37" y="g42" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g37, g42);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g38" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g38, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g41" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g41);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g46" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g46);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g38" y="g46" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g38, g46);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g36" y="g45" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g36, g45);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g45" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g45);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g35" y="g47" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g35, g47);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g47" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g33, g47);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g33" y="g43" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g33, g43);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g32" y="g43" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g32, g43);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g31" y="g44" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g31, g44);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g44" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g44);
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

