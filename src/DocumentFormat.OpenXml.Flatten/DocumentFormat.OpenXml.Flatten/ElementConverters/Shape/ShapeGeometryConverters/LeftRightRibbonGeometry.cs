using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <inheritdoc />
    public class LeftRightRibbonGeometry : ShapeGeometryBase
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

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //  <gd name="adj3" fmla="val 16667" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 16667d;

            var wd32 = w / 32;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a3" fmla="pin 0 adj3 33333" />
            //  <gd name="maxAdj1" fmla="+- 100000 0 a3" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="w1" fmla="+- wd2 0 wd32" />
            //  <gd name="maxAdj2" fmla="*/ 100000 w1 ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="x1" fmla="*/ ss a2 100000" />
            //  <gd name="x4" fmla="+- r 0 x1" />
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            //  <gd name="dy2" fmla="*/ h a3 -200000" />
            //  <gd name="ly1" fmla="+- vc dy2 dy1" />
            //  <gd name="ry4" fmla="+- vc dy1 dy2" />
            //  <gd name="ly2" fmla="+- ly1 dy1 0" />
            //  <gd name="ry3" fmla="+- b 0 ly2" />
            //  <gd name="ly4" fmla="*/ ly2 2 1" />
            //  <gd name="ry1" fmla="+- b 0 ly4" />
            //  <gd name="ly3" fmla="+- ly4 0 ly1" />
            //  <gd name="ry2" fmla="+- b 0 ly3" />
            //  <gd name="hR" fmla="*/ a3 ss 400000" />
            //  <gd name="x2" fmla="+- hc 0 wd32" />
            //  <gd name="x3" fmla="+- hc wd32 0" />
            //  <gd name="y1" fmla="+- ly1 hR 0" />
            //  <gd name="y2" fmla="+- ry2 0 hR" />
            //</gdLst>

            //<gd name="a3" fmla="pin 0 adj3 33333" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a3 = Pin(0, adj3, 33333);
            //<gd name="maxAdj1" fmla="+- 100000 0 a3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj1 = 100000 + 0 - a3;
            //<gd name="a1" fmla="pin 0 adj1 maxAdj1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, maxAdj1);
            //<gd name="w1" fmla="+- wd2 0 wd32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var w1 = wd2 + 0 - wd32;
            //<gd name="maxAdj2" fmla="*/ 100000 w1 ss" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj2 = 100000 * w1 / ss;
            //<gd name="a2" fmla="pin 0 adj2 maxAdj2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(0, adj2, maxAdj2);
            //<gd name="x1" fmla="*/ ss a2 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = ss * a2 / 100000;
            //<gd name="x4" fmla="+- r 0 x1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = r + 0 - x1;
            //<gd name="dy1" fmla="*/ h a1 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = h * a1 / 200000;
            //<gd name="dy2" fmla="*/ h a3 -200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = h * a3 / -200000;
            //<gd name="ly1" fmla="+- vc dy2 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ly1 = vc + dy2 - dy1;
            //<gd name="ry4" fmla="+- vc dy1 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ry4 = vc + dy1 - dy2;
            //<gd name="ly2" fmla="+- ly1 dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ly2 = ly1 + dy1 - 0;
            //<gd name="ry3" fmla="+- b 0 ly2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ry3 = b + 0 - ly2;
            //<gd name="ly4" fmla="*/ ly2 2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ly4 = ly2 * 2 / 1;
            //<gd name="ry1" fmla="+- b 0 ly4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ry1 = b + 0 - ly4;
            //<gd name="ly3" fmla="+- ly4 0 ly1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ly3 = ly4 + 0 - ly1;
            //<gd name="ry2" fmla="+- b 0 ly3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ry2 = b + 0 - ly3;
            //<gd name="hR" fmla="*/ a3 ss 400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var hR = a3 * ss / 400000;
            //<gd name="x2" fmla="+- hc 0 wd32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - wd32;
            //<gd name="x3" fmla="+- hc wd32 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + wd32 - 0;
            //<gd name="y1" fmla="+- ly1 hR 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = ly1 + hR - 0;
            //<gd name="y2" fmla="+- ry2 0 hR" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = ry2 + 0 - hR;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="ly2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="ly1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="ly1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" />
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x4" y="ry2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="ry1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="ry3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="ry4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="ry4" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x2" y="ly3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="ly3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="ly4" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x3" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" />
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x3" y="ry2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="ly2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="ly1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="ly1" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" />
            //    <arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="x4" y="ry2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="ry1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="ry3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="ry4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="ry4" />
            //    </lnTo>
            //    <arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x2" y="ly3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="ly3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="ly4" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x3" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x3" y="ry2" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x2" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="ly3" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="ly2" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, ly2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="ly1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, ly1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="ly1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, ly1);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, cd2);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ry2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ry2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ry1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ry1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="ry3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, ry3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ry4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ry4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="ry4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, ry4);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="ly3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, ly3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="ly3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, ly3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="ly4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, ly4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darkenLess"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</moveTo>
            currentPoint = new EmuPoint(x3, y1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="wd32" hR="hR" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 0d, cd4);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ry2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, ry2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="ly2" />
            //</moveTo>
            currentPoint = new EmuPoint(l, ly2);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="ly1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, ly1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="ly1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, ly1);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, cd2);
            //<arcTo wR="wd32" hR="hR" stAng="3cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, 3 * cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ry2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ry2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ry1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ry1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="ry3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, ry3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ry4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ry4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="ry4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, ry4);
            //<arcTo wR="wd32" hR="hR" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd32, hR, cd4, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="ly3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, ly3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="ly3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, ly3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="ly4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, ly4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</moveTo>
            currentPoint = new EmuPoint(x3, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ry2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, ry2);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y2" />
            //</moveTo>
            currentPoint = new EmuPoint(x2, y2);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="ly3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, ly3);
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="x1" t="ly1" r="x4" b="ry4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, ly1, x4, ry4);

            return shapePaths;
        }
    }


}

