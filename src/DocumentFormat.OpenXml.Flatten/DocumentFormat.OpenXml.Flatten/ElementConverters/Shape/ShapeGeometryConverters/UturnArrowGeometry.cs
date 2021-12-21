using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头: 手杖形
    /// </summary>
    public class UTurnArrowGeometry : ShapeGeometryBase
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
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 25000" />
            //  <gd name="adj3" fmla="val 25000" />
            //  <gd name="adj4" fmla="val 43750" />
            //  <gd name="adj5" fmla="val 75000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 25000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 25000d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? 43750;
            var customAdj5 = adjusts?.GetAdjustValue("adj5");
            var adj5 = customAdj5 ?? 75000;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a2" fmla="pin 0 adj2 25000" />
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="q2" fmla="*/ a1 ss h" />
            //  <gd name="q3" fmla="+- 100000 0 q2" />
            //  <gd name="maxAdj3" fmla="*/ q3 h ss" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="q1" fmla="+- a3 a1 0" />
            //  <gd name="minAdj5" fmla="*/ q1 ss h" />
            //  <gd name="a5" fmla="pin minAdj5 adj5 100000" />
            //  <gd name="th" fmla="*/ ss a1 100000" />
            //  <gd name="aw2" fmla="*/ ss a2 100000" />
            //  <gd name="th2" fmla="*/ th 1 2" />
            //  <gd name="dh2" fmla="+- aw2 0 th2" />
            //  <gd name="y5" fmla="*/ h a5 100000" />
            //  <gd name="ah" fmla="*/ ss a3 100000" />
            //  <gd name="y4" fmla="+- y5 0 ah" />
            //  <gd name="x9" fmla="+- r 0 dh2" />
            //  <gd name="bw" fmla="*/ x9 1 2" />
            //  <gd name="bs" fmla="min bw y4" />
            //  <gd name="maxAdj4" fmla="*/ bs 100000 ss" />
            //  <gd name="a4" fmla="pin 0 adj4 maxAdj4" />
            //  <gd name="bd" fmla="*/ ss a4 100000" />
            //  <gd name="bd3" fmla="+- bd 0 th" />
            //  <gd name="bd2" fmla="max bd3 0" />
            //  <gd name="x3" fmla="+- th bd2 0" />
            //  <gd name="x8" fmla="+- r 0 aw2" />
            //  <gd name="x6" fmla="+- x8 0 aw2" />
            //  <gd name="x7" fmla="+- x6 dh2 0" />
            //  <gd name="x4" fmla="+- x9 0 bd" />
            //  <gd name="x5" fmla="+- x7 0 bd2" />
            //  <gd name="cx" fmla="+/ th x7 2" />
            //</gdLst>


            //  <gd name="a2" fmla="pin 0 adj2 25000" />
            var a2 = Pin(0, adj2, 25000);
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            var maxAdj1 = a2 * 2 / 1;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="q2" fmla="*/ a1 ss h" />
            var q2 = a1 * ss / h;
            //  <gd name="q3" fmla="+- 100000 0 q2" />
            var q3 = 100000 - q2;
            //  <gd name="maxAdj3" fmla="*/ q3 h ss" />
            var maxAdj3 = q3 * h / ss;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="q1" fmla="+- a3 a1 0" />
            var q1 = a3 + a1;
            //  <gd name="minAdj5" fmla="*/ q1 ss h" />
            var minAdj5 = q1 * ss / h;
            //  <gd name="a5" fmla="pin minAdj5 adj5 100000" />
            var a5 = Pin(minAdj5, adj5, 100000);
            //  <gd name="th" fmla="*/ ss a1 100000" />
            var th = ss * a1 / 100000;
            //  <gd name="aw2" fmla="*/ ss a2 100000" />
            var aw2 = ss * a2 / 100000;
            //  <gd name="th2" fmla="*/ th 1 2" />
            var th2 = th * 1 / 2;
            //  <gd name="dh2" fmla="+- aw2 0 th2" />
            var dh2 = aw2 - th2;
            //  <gd name="y5" fmla="*/ h a5 100000" />
            var y5 = h * a5 / 100000;
            //  <gd name="ah" fmla="*/ ss a3 100000" />
            var ah = ss * a3 / 100000;
            //  <gd name="y4" fmla="+- y5 0 ah" />
            var y4 = y5 - ah;
            //  <gd name="x9" fmla="+- r 0 dh2" />
            var x9 = r - dh2;
            //  <gd name="bw" fmla="*/ x9 1 2" />
            var bw = x9 * 1 / 2;
            //  <gd name="bs" fmla="min bw y4" />
            var bs = System.Math.Min(bw, y4);
            //  <gd name="maxAdj4" fmla="*/ bs 100000 ss" />
            var maxAdj4 = bs * 100000 / ss;
            //  <gd name="a4" fmla="pin 0 adj4 maxAdj4" />
            var a4 = Pin(0, adj4, maxAdj4);
            //  <gd name="bd" fmla="*/ ss a4 100000" />
            var bd = ss * a4 / 100000;
            //  <gd name="bd3" fmla="+- bd 0 th" />
            var bd3 = bd - th;
            //  <gd name="bd2" fmla="max bd3 0" />
            var bd2 = System.Math.Max(bd3, 0);
            //  <gd name="x3" fmla="+- th bd2 0" />
            var x3 = th + bd2;
            //  <gd name="x8" fmla="+- r 0 aw2" />
            var x8 = r - aw2;
            //  <gd name="x6" fmla="+- x8 0 aw2" />
            var x6 = x8 - aw2;
            //  <gd name="x7" fmla="+- x6 dh2 0" />
            var x7 = x6 + dh2;
            //  <gd name="x4" fmla="+- x9 0 bd" />
            var x4 = x9 - bd;
            //  <gd name="x5" fmla="+- x7 0 bd2" />
            var x5 = x7 - bd2;
            //  <gd name="cx" fmla="+/ th x7 2" />
            var cx = (th + x7) / 2;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="l" y="bd" />
            //    </lnTo>
            //    <arcTo wR="bd" hR="bd" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x4" y="t" />
            //    </lnTo>
            //    <arcTo wR="bd" hR="bd" stAng="3cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x9" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="x3" />
            //    </lnTo>
            //    <arcTo wR="bd2" hR="bd2" stAng="0" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="x3" y="th" />
            //    </lnTo>
            //    <arcTo wR="bd2" hR="bd2" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="th" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="l" y="bd" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, bd);
            //    <arcTo wR="bd" hR="bd" stAng="cd2" swAng="cd4" />
            var wR = bd;
            var hR = bd;
            var stAng = cd2;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x4" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, t);
            //    <arcTo wR="bd" hR="bd" stAng="3cd4" swAng="cd4" />
            wR = bd;
            hR = bd;
            stAng = 3 * cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x9" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x9, y4);
            //    <lnTo>
            //      <pt x="r" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y4);
            //    <lnTo>
            //      <pt x="x8" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y5);
            //    <lnTo>
            //      <pt x="x6" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x6, y4);
            //    <lnTo>
            //      <pt x="x7" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x7, y4);
            //    <lnTo>
            //      <pt x="x7" y="x3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x7, x3);
            //    <arcTo wR="bd2" hR="bd2" stAng="0" swAng="-5400000" />
            wR = bd2;
            hR = bd2;
            stAng = 0;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x3" y="th" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, th);
            //    <arcTo wR="bd2" hR="bd2" stAng="3cd4" swAng="-5400000" />
            wR = bd2;
            hR = bd2;
            stAng = 3 * cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="th" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, th, b);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }
}
