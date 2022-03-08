using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 动作按钮: 声音
    /// </summary>
    public class ActionButtonSoundGeometry : ShapeGeometryBase
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
            //  <gd name="g14" fmla="*/ g13 1 8" />
            //  <gd name="g15" fmla="*/ g13 5 16" />
            //  <gd name="g16" fmla="*/ g13 5 8" />
            //  <gd name="g17" fmla="*/ g13 11 16" />
            //  <gd name="g18" fmla="*/ g13 3 4" />
            //  <gd name="g19" fmla="*/ g13 7 8" />
            //  <gd name="g20" fmla="+- g9 g14 0" />
            //  <gd name="g21" fmla="+- g9 g15 0" />
            //  <gd name="g22" fmla="+- g9 g17 0" />
            //  <gd name="g23" fmla="+- g9 g19 0" />
            //  <gd name="g24" fmla="+- g11 g15 0" />
            //  <gd name="g25" fmla="+- g11 g16 0" />
            //  <gd name="g26" fmla="+- g11 g18 0" />
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
            //<gd name="g14" fmla="*/ g13 1 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g14 = g13 * 1 / 8;
            //<gd name="g15" fmla="*/ g13 5 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g15 = g13 * 5 / 16;
            //<gd name="g16" fmla="*/ g13 5 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g16 = g13 * 5 / 8;
            //<gd name="g17" fmla="*/ g13 11 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g17 = g13 * 11 / 16;
            //<gd name="g18" fmla="*/ g13 3 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g18 = g13 * 3 / 4;
            //<gd name="g19" fmla="*/ g13 7 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g19 = g13 * 7 / 8;
            //<gd name="g20" fmla="+- g9 g14 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g20 = g9 + g14 - 0;
            //<gd name="g21" fmla="+- g9 g15 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g21 = g9 + g15 - 0;
            //<gd name="g22" fmla="+- g9 g17 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g22 = g9 + g17 - 0;
            //<gd name="g23" fmla="+- g9 g19 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g23 = g9 + g19 - 0;
            //<gd name="g24" fmla="+- g11 g15 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g24 = g11 + g15 - 0;
            //<gd name="g25" fmla="+- g11 g16 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g25 = g11 + g16 - 0;
            //<gd name="g26" fmla="+- g11 g18 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g26 = g11 + g18 - 0;

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
            //      <pt x="g11" y="g21" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g11" y="g22" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g24" y="g22" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g25" y="g10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g25" y="g9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g24" y="g21" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darken" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g11" y="g21" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g11" y="g22" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g24" y="g22" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g25" y="g10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g25" y="g9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g24" y="g21" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g11" y="g21" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g24" y="g21" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g25" y="g9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g25" y="g10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g24" y="g22" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g11" y="g22" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="g26" y="g21" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g12" y="g20" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="g26" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g12" y="vc" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="g26" y="g22" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g12" y="g23" />
            //    </lnTo>
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
            //  <pt x="g11" y="g21" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g21);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g22" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g22);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g24" y="g22" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g24, g22);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g25" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g25, g10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g25" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g25, g9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g24" y="g21" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g24, g21);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darken"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g21" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g21);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g22" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g22);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g24" y="g22" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g24, g22);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g25" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g25, g10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g25" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g25, g9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g24" y="g21" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g24, g21);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.Darken, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g21" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g21);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g24" y="g21" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g24, g21);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g25" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g25, g9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g25" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g25, g10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g24" y="g22" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g24, g22);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g22" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g22);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g26" y="g21" />
            //</moveTo>
            currentPoint = new EmuPoint(g26, g21);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g20" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g20);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g26" y="vc" />
            //</moveTo>
            currentPoint = new EmuPoint(g26, vc);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, vc);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g26" y="g22" />
            //</moveTo>
            currentPoint = new EmuPoint(g26, g22);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g23" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g23);
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

