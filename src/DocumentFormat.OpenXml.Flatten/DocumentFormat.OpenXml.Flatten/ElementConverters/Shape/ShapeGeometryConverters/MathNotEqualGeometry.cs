using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 不等号
    /// </summary>
    public class MathNotEqualGeometry : ShapeGeometryBase
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
            //  <gd name="adj2" fmla="val 6600000" />
            //  <gd name="adj3" fmla="val 11760" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 23520d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 6600000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 11760d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="crAng" fmla="pin 4200000 adj2 6600000" />
            //  <gd name="2a1" fmla="*/ a1 2 1" />
            //  <gd name="maxAdj3" fmla="+- 100000 0 2a1" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="dy1" fmla="*/ h a1 100000" />
            //  <gd name="dy2" fmla="*/ h a3 200000" />
            //  <gd name="dx1" fmla="*/ w 73490 200000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x8" fmla="+- hc dx1 0" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc dy2 0" />
            //  <gd name="y1" fmla="+- y2 0 dy1" />
            //  <gd name="y4" fmla="+- y3 dy1 0" />
            //  <gd name="cadj2" fmla="+- crAng 0 cd4" />
            //  <gd name="xadj2" fmla="tan hd2 cadj2" />
            //  <gd name="len" fmla="mod xadj2 hd2 0" />
            //  <gd name="bhw" fmla="*/ len dy1 hd2" />
            //  <gd name="bhw2" fmla="*/ bhw 1 2" />
            //  <gd name="x7" fmla="+- hc xadj2 bhw2" />
            //  <gd name="dx67" fmla="*/ xadj2 y1 hd2" />
            //  <gd name="x6" fmla="+- x7 0 dx67" />
            //  <gd name="dx57" fmla="*/ xadj2 y2 hd2" />
            //  <gd name="x5" fmla="+- x7 0 dx57" />
            //  <gd name="dx47" fmla="*/ xadj2 y3 hd2" />
            //  <gd name="x4" fmla="+- x7 0 dx47" />
            //  <gd name="dx37" fmla="*/ xadj2 y4 hd2" />
            //  <gd name="x3" fmla="+- x7 0 dx37" />
            //  <gd name="dx27" fmla="*/ xadj2 2 1" />
            //  <gd name="x2" fmla="+- x7 0 dx27" />
            //  <gd name="rx7" fmla="+- x7 bhw 0" />
            //  <gd name="rx6" fmla="+- x6 bhw 0" />
            //  <gd name="rx5" fmla="+- x5 bhw 0" />
            //  <gd name="rx4" fmla="+- x4 bhw 0" />
            //  <gd name="rx3" fmla="+- x3 bhw 0" />
            //  <gd name="rx2" fmla="+- x2 bhw 0" />
            //  <gd name="dx7" fmla="*/ dy1 hd2 len" />
            //  <gd name="rxt" fmla="+- x7 dx7 0" />
            //  <gd name="lxt" fmla="+- rx7 0 dx7" />
            //  <gd name="rx" fmla="?: cadj2 rxt rx7" />
            //  <gd name="lx" fmla="?: cadj2 x7 lxt" />
            //  <gd name="dy3" fmla="*/ dy1 xadj2 len" />
            //  <gd name="dy4" fmla="+- 0 0 dy3" />
            //  <gd name="ry" fmla="?: cadj2 dy3 t" />
            //  <gd name="ly" fmla="?: cadj2 t dy4" />
            //  <gd name="dlx" fmla="+- w 0 rx" />
            //  <gd name="drx" fmla="+- w 0 lx" />
            //  <gd name="dly" fmla="+- h 0 ry" />
            //  <gd name="dry" fmla="+- h 0 ly" />
            //  <gd name="xC1" fmla="+/ rx lx 2" />
            //  <gd name="xC2" fmla="+/ drx dlx 2" />
            //  <gd name="yC1" fmla="+/ ry ly 2" />
            //  <gd name="yC2" fmla="+/ y1 y2 2" />
            //  <gd name="yC3" fmla="+/ y3 y4 2" />
            //  <gd name="yC4" fmla="+/ dry dly 2" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 50000);
            //<gd name="crAng" fmla="pin 4200000 adj2 6600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var crAng = Pin(4200000, adj2, 6600000);
            //<gd name="2a1" fmla="*/ a1 2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var _2a1 = a1 * 2 / 1;
            //<gd name="maxAdj3" fmla="+- 100000 0 2a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj3 = 100000 + 0 - _2a1;
            //<gd name="a3" fmla="pin 0 adj3 maxAdj3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a3 = Pin(0, adj3, maxAdj3);
            //<gd name="dy1" fmla="*/ h a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = h * a1 / 100000;
            //<gd name="dy2" fmla="*/ h a3 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = h * a3 / 200000;
            //<gd name="dx1" fmla="*/ w 73490 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = w * 73490 / 200000;
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x8" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x8 = hc + dx1 - 0;
            //<gd name="y2" fmla="+- vc 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = vc + 0 - dy2;
            //<gd name="y3" fmla="+- vc dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = vc + dy2 - 0;
            //<gd name="y1" fmla="+- y2 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = y2 + 0 - dy1;
            //<gd name="y4" fmla="+- y3 dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = y3 + dy1 - 0;
            //<gd name="cadj2" fmla="+- crAng 0 cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cadj2 = crAng + 0 - cd4;
            //<gd name="xadj2" fmla="tan hd2 cadj2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xadj2 = Tan(hd2, (int) cadj2);
            //<gd name="len" fmla="mod xadj2 hd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var len = Mod(xadj2, hd2, 0);
            //<gd name="bhw" fmla="*/ len dy1 hd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bhw = len * dy1 / hd2;
            //<gd name="bhw2" fmla="*/ bhw 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bhw2 = bhw * 1 / 2;
            //<gd name="x7" fmla="+- hc xadj2 bhw2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x7 = hc + xadj2 - bhw2;
            //<gd name="dx67" fmla="*/ xadj2 y1 hd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx67 = xadj2 * y1 / hd2;
            //<gd name="x6" fmla="+- x7 0 dx67" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = x7 + 0 - dx67;
            //<gd name="dx57" fmla="*/ xadj2 y2 hd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx57 = xadj2 * y2 / hd2;
            //<gd name="x5" fmla="+- x7 0 dx57" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = x7 + 0 - dx57;
            //<gd name="dx47" fmla="*/ xadj2 y3 hd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx47 = xadj2 * y3 / hd2;
            //<gd name="x4" fmla="+- x7 0 dx47" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = x7 + 0 - dx47;
            //<gd name="dx37" fmla="*/ xadj2 y4 hd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx37 = xadj2 * y4 / hd2;
            //<gd name="x3" fmla="+- x7 0 dx37" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = x7 + 0 - dx37;
            //<gd name="dx27" fmla="*/ xadj2 2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx27 = xadj2 * 2 / 1;
            //<gd name="x2" fmla="+- x7 0 dx27" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = x7 + 0 - dx27;
            //<gd name="rx7" fmla="+- x7 bhw 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx7 = x7 + bhw - 0;
            //<gd name="rx6" fmla="+- x6 bhw 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx6 = x6 + bhw - 0;
            //<gd name="rx5" fmla="+- x5 bhw 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx5 = x5 + bhw - 0;
            //<gd name="rx4" fmla="+- x4 bhw 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx4 = x4 + bhw - 0;
            //<gd name="rx3" fmla="+- x3 bhw 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx3 = x3 + bhw - 0;
            //<gd name="rx2" fmla="+- x2 bhw 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx2 = x2 + bhw - 0;
            //<gd name="dx7" fmla="*/ dy1 hd2 len" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx7 = dy1 * hd2 / len;
            //<gd name="rxt" fmla="+- x7 dx7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rxt = x7 + dx7 - 0;
            //<gd name="lxt" fmla="+- rx7 0 dx7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lxt = rx7 + 0 - dx7;
            //<gd name="rx" fmla="?: cadj2 rxt rx7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rx = cadj2 > 0 ? rxt : rx7;
            //<gd name="lx" fmla="?: cadj2 x7 lxt" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lx = cadj2 > 0 ? x7 : lxt;
            //<gd name="dy3" fmla="*/ dy1 xadj2 len" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = dy1 * xadj2 / len;
            //<gd name="dy4" fmla="+- 0 0 dy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy4 = 0 + 0 - dy3;
            //<gd name="ry" fmla="?: cadj2 dy3 t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ry = cadj2 > 0 ? dy3 : t;
            //<gd name="ly" fmla="?: cadj2 t dy4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ly = cadj2 > 0 ? t : dy4;
            //<gd name="dlx" fmla="+- w 0 rx" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dlx = w + 0 - rx;
            //<gd name="drx" fmla="+- w 0 lx" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var drx = w + 0 - lx;
            //<gd name="dly" fmla="+- h 0 ry" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dly = h + 0 - ry;
            //<gd name="dry" fmla="+- h 0 ly" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dry = h + 0 - ly;
            //<gd name="xC1" fmla="+/ rx lx 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC1 = (rx + lx) / 2;
            //<gd name="xC2" fmla="+/ drx dlx 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC2 = (drx + dlx) / 2;
            //<gd name="yC1" fmla="+/ ry ly 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC1 = (ry + ly) / 2;
            //<gd name="yC2" fmla="+/ y1 y2 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC2 = (y1 + y2) / 2;
            //<gd name="yC3" fmla="+/ y3 y4 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC3 = (y3 + y4) / 2;
            //<gd name="yC4" fmla="+/ dry dly 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC4 = (dry + dly) / 2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x6" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="lx" y="ly" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="rx" y="ry" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="rx6" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="rx5" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="rx4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="rx3" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="drx" y="dry" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="dlx" y="dly" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y1" />
            //</moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="lx" y="ly" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, lx, ly);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="rx" y="ry" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, rx, ry);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="rx6" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, rx6, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="rx5" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, rx5, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="rx4" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, rx4, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="rx3" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, rx3, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="drx" y="dry" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, drx, dry);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="dlx" y="dly" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, dlx, dly);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="x1" t="y1" r="x8" b="y4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y1, x8, y4);

            return shapePaths;
        }
    }


}

