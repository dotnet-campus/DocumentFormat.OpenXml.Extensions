using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 除号
    /// </summary>
    public class MathDivideGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 23520" />
            //  <gd name="adj2" fmla="val 5880" />
            //  <gd name="adj3" fmla="val 11760" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 23520d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 5880d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 11760d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 1000 adj1 36745" />
            //  <gd name="ma1" fmla="+- 0 0 a1" />
            //  <gd name="ma3h" fmla="+/ 73490 ma1 4" />
            //  <gd name="ma3w" fmla="*/ 36745 w h" />
            //  <gd name="maxAdj3" fmla="min ma3h ma3w" />
            //  <gd name="a3" fmla="pin 1000 adj3 maxAdj3" />
            //  <gd name="m4a3" fmla="*/ -4 a3 1" />
            //  <gd name="maxAdj2" fmla="+- 73490 m4a3 a1" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            //  <gd name="yg" fmla="*/ h a2 100000" />
            //  <gd name="rad" fmla="*/ h a3 100000" />
            //  <gd name="dx1" fmla="*/ w 73490 200000" />
            //  <gd name="y3" fmla="+- vc 0 dy1" />
            //  <gd name="y4" fmla="+- vc dy1 0" />
            //  <gd name="a" fmla="+- yg rad 0" />
            //  <gd name="y2" fmla="+- y3 0 a" />
            //  <gd name="y1" fmla="+- y2 0 rad" />
            //  <gd name="y5" fmla="+- b 0 y1" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x3" fmla="+- hc dx1 0" />
            //  <gd name="x2" fmla="+- hc 0 rad" />
            //</gdLst>

            //<gd name="a1" fmla="pin 1000 adj1 36745" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(1000, adj1, 36745);
            //<gd name="ma1" fmla="+- 0 0 a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma1 = 0 + 0 - a1;
            //<gd name="ma3h" fmla="+/ 73490 ma1 4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma3h = (73490 + ma1) / 4;
            //<gd name="ma3w" fmla="*/ 36745 w h" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma3w = 36745 * w / h;
            //<gd name="maxAdj3" fmla="min ma3h ma3w" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj3 = System.Math.Min(ma3h, ma3w);
            //<gd name="a3" fmla="pin 1000 adj3 maxAdj3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a3 = Pin(1000, adj3, maxAdj3);
            //<gd name="m4a3" fmla="*/ -4 a3 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var m4a3 = -4 * a3 / 1;
            //<gd name="maxAdj2" fmla="+- 73490 m4a3 a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj2 = 73490 + m4a3 - a1;
            //<gd name="a2" fmla="pin 0 adj2 maxAdj2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(0, adj2, maxAdj2);
            //<gd name="dy1" fmla="*/ h a1 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = h * a1 / 200000;
            //<gd name="yg" fmla="*/ h a2 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yg = h * a2 / 100000;
            //<gd name="rad" fmla="*/ h a3 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rad = h * a3 / 100000;
            //<gd name="dx1" fmla="*/ w 73490 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = w * 73490 / 200000;
            //<gd name="y3" fmla="+- vc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = vc + 0 - dy1;
            //<gd name="y4" fmla="+- vc dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = vc + dy1 - 0;
            //<gd name="a" fmla="+- yg rad 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = yg + rad - 0;
            //<gd name="y2" fmla="+- y3 0 a" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = y3 + 0 - a;
            //<gd name="y1" fmla="+- y2 0 rad" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = y2 + 0 - rad;
            //<gd name="y5" fmla="+- b 0 y1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = b + 0 - y1;
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x3" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + dx1 - 0;
            //<gd name="x2" fmla="+- hc 0 rad" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - rad;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="hc" y="y1" />
            //    </moveTo>
            //    <arcTo hR="rad" wR="rad" stAng="3cd4" swAng="21600000" />
            //    <close />
            //    <moveTo>
            //      <pt x="hc" y="y5" />
            //    </moveTo>
            //    <arcTo hR="rad" wR="rad" stAng="cd4" swAng="21600000" />
            //    <close />
            //    <moveTo>
            //      <pt x="x1" y="y3" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="y1" />
            //</moveTo>
            var currentPoint = new EmuPoint(hc, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo hR="rad" wR="rad" stAng="3cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rad, rad, 3 * cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="y5" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, y5);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo hR="rad" wR="rad" stAng="cd4" swAng="21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rad, rad, cd4, 21600000d);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y3" />
            //</moveTo>
            currentPoint = new EmuPoint(x1, y3);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="x1" t="y3" r="x3" b="y4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y3, x3, y4);

            return shapePaths;
        }
    }


}

