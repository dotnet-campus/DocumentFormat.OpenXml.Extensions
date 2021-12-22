using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 太阳形
    /// </summary>
    public class SunGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string? ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 25000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 12500 adj 46875" />
            //  <gd name="g0" fmla="+- 50000 0 a" />
            //  <gd name="g1" fmla="*/ g0 30274 32768" />
            //  <gd name="g2" fmla="*/ g0 12540 32768" />
            //  <gd name="g3" fmla="+- g1 50000 0" />
            //  <gd name="g4" fmla="+- g2 50000 0" />
            //  <gd name="g5" fmla="+- 50000 0 g1" />
            //  <gd name="g6" fmla="+- 50000 0 g2" />
            //  <gd name="g7" fmla="*/ g0 23170 32768" />
            //  <gd name="g8" fmla="+- 50000 g7 0" />
            //  <gd name="g9" fmla="+- 50000 0 g7" />
            //  <gd name="g10" fmla="*/ g5 3 4" />
            //  <gd name="g11" fmla="*/ g6 3 4" />
            //  <gd name="g12" fmla="+- g10 3662 0" />
            //  <gd name="g13" fmla="+- g11 3662 0" />
            //  <gd name="g14" fmla="+- g11 12500 0" />
            //  <gd name="g15" fmla="+- 100000 0 g10" />
            //  <gd name="g16" fmla="+- 100000 0 g12" />
            //  <gd name="g17" fmla="+- 100000 0 g13" />
            //  <gd name="g18" fmla="+- 100000 0 g14" />
            //  <gd name="ox1" fmla="*/ w 18436 21600" />
            //  <gd name="oy1" fmla="*/ h 3163 21600" />
            //  <gd name="ox2" fmla="*/ w 3163 21600" />
            //  <gd name="oy2" fmla="*/ h 18436 21600" />
            //  <gd name="x8" fmla="*/ w g8 100000" />
            //  <gd name="x9" fmla="*/ w g9 100000" />
            //  <gd name="x10" fmla="*/ w g10 100000" />
            //  <gd name="x12" fmla="*/ w g12 100000" />
            //  <gd name="x13" fmla="*/ w g13 100000" />
            //  <gd name="x14" fmla="*/ w g14 100000" />
            //  <gd name="x15" fmla="*/ w g15 100000" />
            //  <gd name="x16" fmla="*/ w g16 100000" />
            //  <gd name="x17" fmla="*/ w g17 100000" />
            //  <gd name="x18" fmla="*/ w g18 100000" />
            //  <gd name="x19" fmla="*/ w a 100000" />
            //  <gd name="wR" fmla="*/ w g0 100000" />
            //  <gd name="hR" fmla="*/ h g0 100000" />
            //  <gd name="y8" fmla="*/ h g8 100000" />
            //  <gd name="y9" fmla="*/ h g9 100000" />
            //  <gd name="y10" fmla="*/ h g10 100000" />
            //  <gd name="y12" fmla="*/ h g12 100000" />
            //  <gd name="y13" fmla="*/ h g13 100000" />
            //  <gd name="y14" fmla="*/ h g14 100000" />
            //  <gd name="y15" fmla="*/ h g15 100000" />
            //  <gd name="y16" fmla="*/ h g16 100000" />
            //  <gd name="y17" fmla="*/ h g17 100000" />
            //  <gd name="y18" fmla="*/ h g18 100000" />
            //</gdLst>


            //  <gd name="a" fmla="pin 12500 adj 46875" />
            var a = Pin(12500, adj, 46875);
            //  <gd name="g0" fmla="+- 50000 0 a" />
            var g0 = 50000 - a;
            //  <gd name="g1" fmla="*/ g0 30274 32768" />
            var g1 = g0 * 30274 / 32768;
            //  <gd name="g2" fmla="*/ g0 12540 32768" />
            var g2 = g0 * 12540 / 32768;
            //  <gd name="g3" fmla="+- g1 50000 0" />
            var g3 = g1 + 50000;
            //  <gd name="g4" fmla="+- g2 50000 0" />
            var g4 = g2 + 50000;
            //  <gd name="g5" fmla="+- 50000 0 g1" />
            var g5 = 50000 - g1;
            //  <gd name="g6" fmla="+- 50000 0 g2" />
            var g6 = 50000 - g2;
            //  <gd name="g7" fmla="*/ g0 23170 32768" />
            var g7 = g0 * 23170 / 32768;
            //  <gd name="g8" fmla="+- 50000 g7 0" />
            var g8 = 50000 + g7;
            //  <gd name="g9" fmla="+- 50000 0 g7" />
            var g9 = 50000 - g7;
            //  <gd name="g10" fmla="*/ g5 3 4" />
            var g10 = g5 * 3 / 4;
            //  <gd name="g11" fmla="*/ g6 3 4" />
            var g11 = g6 * 3 / 4;
            //  <gd name="g12" fmla="+- g10 3662 0" />
            var g12 = g10 + 3662;
            //  <gd name="g13" fmla="+- g11 3662 0" />
            var g13 = g11 + 3662;
            //  <gd name="g14" fmla="+- g11 12500 0" />
            var g14 = g11 + 12500;
            //  <gd name="g15" fmla="+- 100000 0 g10" />
            var g15 = 100000 - g10;
            //  <gd name="g16" fmla="+- 100000 0 g12" />
            var g16 = 100000 - g12;
            //  <gd name="g17" fmla="+- 100000 0 g13" />
            var g17 = 100000 - g13;
            //  <gd name="g18" fmla="+- 100000 0 g14" />
            var g18 = 100000 - g14;
            //  <gd name="ox1" fmla="*/ w 18436 21600" />
            var ox1 = w * 18436 / 21600;
            //  <gd name="oy1" fmla="*/ h 3163 21600" />
            var oy1 = h * 3163 / 21600;
            //  <gd name="ox2" fmla="*/ w 3163 21600" />
            var ox2 = w * 3163 / 21600;
            //  <gd name="oy2" fmla="*/ h 18436 21600" />
            var oy2 = h * 18436 / 21600;
            //  <gd name="x8" fmla="*/ w g8 100000" />
            var x8 = w * g8 / 100000;
            //  <gd name="x9" fmla="*/ w g9 100000" />
            var x9 = w * g9 / 100000;
            //  <gd name="x10" fmla="*/ w g10 100000" />
            var x10 = w * g10 / 100000;
            //  <gd name="x12" fmla="*/ w g12 100000" />
            var x12 = w * g12 / 100000;
            //  <gd name="x13" fmla="*/ w g13 100000" />
            var x13 = w * g13 / 100000;
            //  <gd name="x14" fmla="*/ w g14 100000" />
            var x14 = w * g14 / 100000;
            //  <gd name="x15" fmla="*/ w g15 100000" />
            var x15 = w * g15 / 100000;
            //  <gd name="x16" fmla="*/ w g16 100000" />
            var x16 = w * g16 / 100000;
            //  <gd name="x17" fmla="*/ w g17 100000" />
            var x17 = w * g17 / 100000;
            //  <gd name="x18" fmla="*/ w g18 100000" />
            var x18 = w * g18 / 100000;
            //  <gd name="x19" fmla="*/ w a 100000" />
            var x19 = w * a / 100000;
            //  <gd name="wR" fmla="*/ w g0 100000" />
            var wR = w * g0 / 100000;
            //  <gd name="hR" fmla="*/ h g0 100000" />
            var hR = h * g0 / 100000;
            //  <gd name="y8" fmla="*/ h g8 100000" />
            var y8 = h * g8 / 100000;
            //  <gd name="y9" fmla="*/ h g9 100000" />
            var y9 = h * g9 / 100000;
            //  <gd name="y10" fmla="*/ h g10 100000" />
            var y10 = h * g10 / 100000;
            //  <gd name="y12" fmla="*/ h g12 100000" />
            var y12 = h * g12 / 100000;
            //  <gd name="y13" fmla="*/ h g13 100000" />
            var y13 = h * g13 / 100000;
            //  <gd name="y14" fmla="*/ h g14 100000" />
            var y14 = h * g14 / 100000;
            //  <gd name="y15" fmla="*/ h g15 100000" />
            var y15 = h * g15 / 100000;
            //  <gd name="y16" fmla="*/ h g16 100000" />
            var y16 = h * g16 / 100000;
            //  <gd name="y17" fmla="*/ h g17 100000" />
            var y17 = h * g17 / 100000;
            //  <gd name="y18" fmla="*/ h g18 100000" />
            var y18 = h * g18 / 100000;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="r" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x15" y="y18" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x15" y="y14" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="ox1" y="oy1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x16" y="y13" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x17" y="y12" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x18" y="y10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x14" y="y10" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="ox2" y="oy1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x13" y="y12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x12" y="y13" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x10" y="y14" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y18" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="ox2" y="oy2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x12" y="y17" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x13" y="y16" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x14" y="y15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x18" y="y15" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="ox1" y="oy2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x17" y="y16" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x16" y="y17" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x19" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wR" hR="hR" stAng="cd2" swAng="21600000" />
            //    <close />
            //  </path>
            //</pathLst>



            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="r" y="vc" />
            //    </moveTo>
            var currentPoint = new EmuPoint(r, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x15" y="y18" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x15, y18);
            //    <lnTo>
            //      <pt x="x15" y="y14" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x15, y14);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="ox1" y="oy1" />
            //    </moveTo>
            currentPoint = new EmuPoint(ox1, oy1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x16" y="y13" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x16, y13);
            //    <lnTo>
            //      <pt x="x17" y="y12" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x17, y12);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="hc" y="t" />
            //    </moveTo>
            currentPoint = new EmuPoint(hc, t);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x18" y="y10" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x18, y10);
            //    <lnTo>
            //      <pt x="x14" y="y10" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x14, y10);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="ox2" y="oy1" />
            //    </moveTo>
            currentPoint = new EmuPoint(ox2, oy1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x13" y="y12" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x13, y12);
            //    <lnTo>
            //      <pt x="x12" y="y13" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x12, y13);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            currentPoint = new EmuPoint(l, vc);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x10" y="y14" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x10, y14);
            //    <lnTo>
            //      <pt x="x10" y="y18" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x10, y18);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="ox2" y="oy2" />
            //    </moveTo>
            currentPoint = new EmuPoint(ox2, oy2);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x12" y="y17" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x12, y17);
            //    <lnTo>
            //      <pt x="x13" y="y16" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x13, y16);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="hc" y="b" />
            //    </moveTo>
            currentPoint = new EmuPoint(hc, b);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x14" y="y15" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x14, y15);
            //    <lnTo>
            //      <pt x="x18" y="y15" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x18, y15);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="ox1" y="oy2" />
            //    </moveTo>
            currentPoint = new EmuPoint(ox1, oy2);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x17" y="y16" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x17, y16);
            //    <lnTo>
            //      <pt x="x16" y="y17" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x16, y17);
            //    <close />
            stringPath.Append("z ");
            //    <moveTo>
            //      <pt x="x19" y="vc" />
            //    </moveTo>
            currentPoint = new EmuPoint(x19, vc);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wR" hR="hR" stAng="cd2" swAng="21600000" />;
            var stAng = cd2;
            var swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="x9" t="y9" r="x8" b="y8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x9, y9, x8, y8);

            return shapePaths;
        }
    }
}
