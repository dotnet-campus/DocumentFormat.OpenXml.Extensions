using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 带形: 前凸
    /// </summary>
    public class RibbonGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 16667" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 16667d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 33333" />
            //  <gd name="a2" fmla="pin 25000 adj2 75000" />
            //  <gd name="x10" fmla="+- r 0 wd8" />
            //  <gd name="dx2" fmla="*/ w a2 200000" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x9" fmla="+- hc dx2 0" />
            //  <gd name="x3" fmla="+- x2 wd32 0" />
            //  <gd name="x8" fmla="+- x9 0 wd32" />
            //  <gd name="x5" fmla="+- x2 wd8 0" />
            //  <gd name="x6" fmla="+- x9 0 wd8" />
            //  <gd name="x4" fmla="+- x5 0 wd32" />
            //  <gd name="x7" fmla="+- x6 wd32 0" />
            //  <gd name="y1" fmla="*/ h a1 200000" />
            //  <gd name="y2" fmla="*/ h a1 100000" />
            //  <gd name="y4" fmla="+- b 0 y2" />
            //  <gd name="y3" fmla="*/ y4 1 2" />
            //  <gd name="hR" fmla="*/ h a1 400000" />
            //  <gd name="y5" fmla="+- b 0 hR" />
            //  <gd name="y6" fmla="+- y2 0 hR" />
            //</gdLst>

            var wd32 = w / 32;
            //<gd name="a1" fmla="pin 0 adj1 33333" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 33333);
            //<gd name="a2" fmla="pin 25000 adj2 75000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(25000, adj2, 75000);
            //<gd name="x10" fmla="+- r 0 wd8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x10 = r + 0 - wd8;
            //<gd name="dx2" fmla="*/ w a2 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = w * a2 / 200000;
            //<gd name="x2" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - dx2;
            //<gd name="x9" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x9 = hc + dx2 - 0;
            //<gd name="x3" fmla="+- x2 wd32 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = x2 + wd32 - 0;
            //<gd name="x8" fmla="+- x9 0 wd32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x8 = x9 + 0 - wd32;
            //<gd name="x5" fmla="+- x2 wd8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = x2 + wd8 - 0;
            //<gd name="x6" fmla="+- x9 0 wd8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = x9 + 0 - wd8;
            //<gd name="x4" fmla="+- x5 0 wd32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = x5 + 0 - wd32;
            //<gd name="x7" fmla="+- x6 wd32 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x7 = x6 + wd32 - 0;
            //<gd name="y1" fmla="*/ h a1 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = h * a1 / 200000;
            //<gd name="y2" fmla="*/ h a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = h * a1 / 100000;
            //<gd name="y4" fmla="+- b 0 y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = b + 0 - y2;
            //<gd name="y3" fmla="*/ y4 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = y4 * 1 / 2;
            //<gd name="hR" fmla="*/ h a1 400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var hR = h * a1 / 400000;
            //<gd name="y5" fmla="+- b 0 hR" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = b + 0 - hR;
            //<gd name="y6" fmla="+- y2 0 hR" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = y2 + 0 - hR;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="t" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x8" y="y2" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x7" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y5" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="wd8" y="y3" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x5" y="hR" />
            //    </moveTo>
            //    <arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x6" y="hR" />
            //    </moveTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd2" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="t" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x8" y="y2" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x7" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y5" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="wd8" y="y3" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x5" y="hR" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x6" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x6" y="hR" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x2" y="y4" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y6" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x9" y="y6" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x9" y="y4" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="t" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, t);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y2);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, y1);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x10" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x10, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y5);
            //<arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, b);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="wd8" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, wd8, y3);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darkenLess"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="hR" />
            //</moveTo>
            currentPoint = new EmuPoint(x5, hR);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="hR" />
            //</moveTo>
            currentPoint = new EmuPoint(x6, hR);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="wd32" hR="hR" stAng="cd2" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd2, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y1);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="t" />
            //</moveTo>
            currentPoint = new EmuPoint(l, t);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, t);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y2);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, y1);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x10" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x10, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y5);
            //<arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, b);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="wd8" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, wd8, y3);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="hR" />
            //</moveTo>
            currentPoint = new EmuPoint(x5, hR);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y2);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //</moveTo>
            currentPoint = new EmuPoint(x6, y2);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="hR" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, hR);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y4" />
            //</moveTo>
            currentPoint = new EmuPoint(x2, y4);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y6);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y6" />
            //</moveTo>
            currentPoint = new EmuPoint(x9, y6);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y4);
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="x2" t="y2" r="x9" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x2, y2, x9, b);

            return shapePaths;
        }
    }


}

