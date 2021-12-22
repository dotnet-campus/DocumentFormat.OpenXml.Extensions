using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 卷形: 垂直
    /// </summary>
    public class VerticalScrollGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 12500" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 12500d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 25000" />
            //  <gd name="ch" fmla="*/ ss a 100000" />
            //  <gd name="ch2" fmla="*/ ch 1 2" />
            //  <gd name="ch4" fmla="*/ ch 1 4" />
            //  <gd name="x3" fmla="+- ch ch2 0" />
            //  <gd name="x4" fmla="+- ch ch 0" />
            //  <gd name="x6" fmla="+- r 0 ch" />
            //  <gd name="x7" fmla="+- r 0 ch2" />
            //  <gd name="x5" fmla="+- x6 0 ch2" />
            //  <gd name="y3" fmla="+- b 0 ch" />
            //  <gd name="y4" fmla="+- b 0 ch2" />
            //</gdLst>

            //<gd name="a" fmla="pin 0 adj 25000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = Pin(0, adj, 25000);
            //<gd name="ch" fmla="*/ ss a 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ch = ss * a / 100000;
            //<gd name="ch2" fmla="*/ ch 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ch2 = ch * 1 / 2;
            //<gd name="ch4" fmla="*/ ch 1 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ch4 = ch * 1 / 4;
            //<gd name="x3" fmla="+- ch ch2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = ch + ch2 - 0;
            //<gd name="x4" fmla="+- ch ch 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = ch + ch - 0;
            //<gd name="x6" fmla="+- r 0 ch" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = r + 0 - ch;
            //<gd name="x7" fmla="+- r 0 ch2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x7 = r + 0 - ch2;
            //<gd name="x5" fmla="+- x6 0 ch2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = x6 + 0 - ch2;
            //<gd name="y3" fmla="+- b 0 ch" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = b + 0 - ch;
            //<gd name="y4" fmla="+- b 0 ch2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = b + 0 - ch2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ch2" y="b" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="ch2" y="y4" />
            //    </lnTo>
            //    <arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="ch" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ch" y="ch2" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x7" y="t" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x6" y="ch" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y4" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" />
            //    <close />
            //    <moveTo>
            //      <pt x="x4" y="ch2" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" />
            //    <arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="cd2" />
            //    <close />
            //  </path>
            //  <path fill="darkenLess" stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x4" y="ch2" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" />
            //    <arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="cd2" />
            //    <close />
            //    <moveTo>
            //      <pt x="ch" y="y4" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="3cd4" />
            //    <arcTo wR="ch4" hR="ch4" stAng="3cd4" swAng="cd2" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ch" y="y3" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="ch" y="ch2" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x7" y="t" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x6" y="ch" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y4" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="ch2" y="b" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="cd2" />
            //    <close />
            //    <moveTo>
            //      <pt x="x3" y="t" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="cd2" />
            //    <arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x4" y="ch2" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x6" y="ch" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x3" y="ch" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="ch2" y="y3" />
            //    </moveTo>
            //    <arcTo wR="ch4" hR="ch4" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="ch" y="y4" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="ch2" y="b" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="ch" y="y3" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="b" />
            //</moveTo>
            var currentPoint = new EmuPoint(ch2, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch2, y4);
            //<arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, cd4, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="ch2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, ch2);
            //<arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd2, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, t);
            //<arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, ch);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y4);
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch2" />
            //</moveTo>
            currentPoint = new EmuPoint(x4, ch2);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd4);
            //<arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, cd4, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path fill="darkenLess"stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch2" />
            //</moveTo>
            currentPoint = new EmuPoint(x4, ch2);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd4);
            //<arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, cd4, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y4" />
            //</moveTo>
            currentPoint = new EmuPoint(ch, y4);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="3cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, 3 * cd4);
            //<arcTo wR="ch4" hR="ch4" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, 3 * cd4, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y3" />
            //</moveTo>
            currentPoint = new EmuPoint(ch, y3);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="ch2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, ch2);
            //<arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd2, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, t);
            //<arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, ch);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y4);
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch2, b);
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="t" />
            //</moveTo>
            currentPoint = new EmuPoint(x3, t);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 3 * cd4, cd2);
            //<arcTo wR="ch4" hR="ch4" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ch2);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="ch" />
            //</moveTo>
            currentPoint = new EmuPoint(x6, ch);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, ch);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="y3" />
            //</moveTo>
            currentPoint = new EmuPoint(ch2, y3);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch4" hR="ch4" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y4);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="b" />
            //</moveTo>
            currentPoint = new EmuPoint(ch2, b);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y3);
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="ch" t="ch" r="x6" b="y4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(ch, ch, x6, y4);

            return shapePaths;
        }
    }


}

