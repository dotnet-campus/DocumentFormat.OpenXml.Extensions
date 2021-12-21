using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 卷形：水平
    /// </summary>
    public class HorizontalScrollGeometry : ShapeGeometryBase
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
            //  <gd name="y3" fmla="+- ch ch2 0" />
            //  <gd name="y4" fmla="+- ch ch 0" />
            //  <gd name="y6" fmla="+- b 0 ch" />
            //  <gd name="y7" fmla="+- b 0 ch2" />
            //  <gd name="y5" fmla="+- y6 0 ch2" />
            //  <gd name="x3" fmla="+- r 0 ch" />
            //  <gd name="x4" fmla="+- r 0 ch2" />
            //</gdLst>

            //<gd name="a" fmla="pin 0 adj 25000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = Pin(0, adj, 25000);
            //<gd name="ch" fmla="*/ ss a 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ch = ss * a / 100000;
            //<gd name="ch2" fmla="*/ ch 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ch2 = ch * 1 / 2;
            //<gd name="ch4" fmla="*/ ch 1 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ch4 = ch * 1 / 4;
            //<gd name="y3" fmla="+- ch ch2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = ch + ch2 - 0;
            //<gd name="y4" fmla="+- ch ch 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = ch + ch - 0;
            //<gd name="y6" fmla="+- b 0 ch" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = b + 0 - ch;
            //<gd name="y7" fmla="+- b 0 ch2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y7 = b + 0 - ch2;
            //<gd name="y5" fmla="+- y6 0 ch2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = y6 + 0 - ch2;
            //<gd name="x3" fmla="+- r 0 ch" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = r + 0 - ch;
            //<gd name="x4" fmla="+- r 0 ch2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = r + 0 - ch2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="ch2" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x4" y="ch2" />
            //    </lnTo>
            //    <arcTo wR="ch4" hR="ch4" stAng="0" swAng="cd2" />
            //    <lnTo>
            //      <pt x="x3" y="ch" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ch2" y="ch" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="l" y="y7" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="ch" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y6" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" />
            //    <close />
            //    <moveTo>
            //      <pt x="ch2" y="y4" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" />
            //    <arcTo wR="ch4" hR="ch4" stAng="0" swAng="-10800000" />
            //    <close />
            //  </path>
            //  <path fill="darkenLess" stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="ch2" y="y4" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" />
            //    <arcTo wR="ch4" hR="ch4" stAng="0" swAng="-10800000" />
            //    <close />
            //    <moveTo>
            //      <pt x="x4" y="ch" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-16200000" />
            //    <arcTo wR="ch4" hR="ch4" stAng="cd2" swAng="-10800000" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y3" />
            //    </moveTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x3" y="ch" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="ch2" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd2" />
            //    <lnTo>
            //      <pt x="r" y="y5" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="ch" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ch" y="y7" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd2" />
            //    <close />
            //    <moveTo>
            //      <pt x="x3" y="ch" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="ch" />
            //    </lnTo>
            //    <arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" />
            //    <moveTo>
            //      <pt x="x4" y="ch" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="ch2" />
            //    </lnTo>
            //    <arcTo wR="ch4" hR="ch4" stAng="0" swAng="cd2" />
            //    <moveTo>
            //      <pt x="ch2" y="y4" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="ch2" y="y3" />
            //    </lnTo>
            //    <arcTo wR="ch4" hR="ch4" stAng="cd2" swAng="cd2" />
            //    <arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd2" />
            //    <moveTo>
            //      <pt x="ch" y="y3" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="ch" y="y6" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="ch2" />
            //</moveTo>
            var currentPoint = new EmuPoint(r, ch2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ch2);
            //<arcTo wR="ch4" hR="ch4" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, 0d, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, ch);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch2, ch);
            //<arcTo wR="ch2" hR="ch2" stAng="3cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 3 * cd4, -5400000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, y7);
            //<arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd2, -10800000d);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y6);
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -5400000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="y4" />
            //</moveTo>
            currentPoint = new EmuPoint(ch2, y4);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -5400000d);
            //<arcTo wR="ch4" hR="ch4" stAng="0" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, 0d, -10800000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path fill="darkenLess"stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="y4" />
            //</moveTo>
            currentPoint = new EmuPoint(ch2, y4);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -5400000d);
            //<arcTo wR="ch4" hR="ch4" stAng="0" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, 0d, -10800000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch" />
            //</moveTo>
            currentPoint = new EmuPoint(x4, ch);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-16200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -16200000d);
            //<arcTo wR="ch4" hR="ch4" stAng="cd2" swAng="-10800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, cd2, -10800000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="y3" />
            //</moveTo>
            currentPoint = new EmuPoint(l, y3);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd2, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, ch);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ch2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, ch2);
            //<arcTo wR="ch2" hR="ch2" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd2, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, y5);
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y7);
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="ch" />
            //</moveTo>
            currentPoint = new EmuPoint(x3, ch);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ch);
            //<arcTo wR="ch2" hR="ch2" stAng="cd4" swAng="-5400000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, cd4, -5400000d);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch" />
            //</moveTo>
            currentPoint = new EmuPoint(x4, ch);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="ch2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, ch2);
            //<arcTo wR="ch4" hR="ch4" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, 0d, cd2);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="y4" />
            //</moveTo>
            currentPoint = new EmuPoint(ch2, y4);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch2" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch2, y3);
            //<arcTo wR="ch4" hR="ch4" stAng="cd2" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch4, ch4, cd2, cd2);
            //<arcTo wR="ch2" hR="ch2" stAng="0" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ch2, ch2, 0d, cd2);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y3" />
            //</moveTo>
            currentPoint = new EmuPoint(ch, y3);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ch" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ch, y6);
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="ch" t="ch" r="x4" b="y6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(ch, ch, x4, y6);

            return shapePaths;
        }
    }


}

