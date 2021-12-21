using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 带形: 上凸弯
    /// </summary>
    public class EllipseRibbon2Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 50000" />
            //  <gd name="adj3" fmla="val 12500" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 12500d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="a2" fmla="pin 25000 adj2 75000" />
            //  <gd name="q10" fmla="+- 100000 0 a1" />
            //  <gd name="q11" fmla="*/ q10 1 2" />
            //  <gd name="q12" fmla="+- a1 0 q11" />
            //  <gd name="minAdj3" fmla="max 0 q12" />
            //  <gd name="a3" fmla="pin minAdj3 adj3 a1" />
            //  <gd name="dx2" fmla="*/ w a2 200000" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- x2 wd8 0" />
            //  <gd name="x4" fmla="+- r 0 x3" />
            //  <gd name="x5" fmla="+- r 0 x2" />
            //  <gd name="x6" fmla="+- r 0 wd8" />
            //  <gd name="dy1" fmla="*/ h a3 100000" />
            //  <gd name="f1" fmla="*/ 4 dy1 w" />
            //  <gd name="q1" fmla="*/ x3 x3 w" />
            //  <gd name="q2" fmla="+- x3 0 q1" />
            //  <gd name="u1" fmla="*/ f1 q2 1" />
            //  <gd name="y1" fmla="+- b 0 u1" />
            //  <gd name="cx1" fmla="*/ x3 1 2" />
            //  <gd name="cu1" fmla="*/ f1 cx1 1" />
            //  <gd name="cy1" fmla="+- b 0 cu1" />
            //  <gd name="cx2" fmla="+- r 0 cx1" />
            //  <gd name="q1" fmla="*/ h a1 100000" />
            //  <gd name="dy3" fmla="+- q1 0 dy1" />
            //  <gd name="q3" fmla="*/ x2 x2 w" />
            //  <gd name="q4" fmla="+- x2 0 q3" />
            //  <gd name="q5" fmla="*/ f1 q4 1" />
            //  <gd name="u3" fmla="+- q5 dy3 0" />
            //  <gd name="y3" fmla="+- b 0 u3" />
            //  <gd name="q6" fmla="+- dy1 dy3 u3" />
            //  <gd name="q7" fmla="+- q6 dy1 0" />
            //  <gd name="cu3" fmla="+- q7 dy3 0" />
            //  <gd name="cy3" fmla="+- b 0 cu3" />
            //  <gd name="rh" fmla="+- b 0 q1" />
            //  <gd name="q8" fmla="*/ dy1 14 16" />
            //  <gd name="u2" fmla="+/ q8 rh 2" />
            //  <gd name="y2" fmla="+- b 0 u2" />
            //  <gd name="u5" fmla="+- q5 rh 0" />
            //  <gd name="y5" fmla="+- b 0 u5" />
            //  <gd name="u6" fmla="+- u3 rh 0" />
            //  <gd name="y6" fmla="+- b 0 u6" />
            //  <gd name="cx4" fmla="*/ x2 1 2" />
            //  <gd name="q9" fmla="*/ f1 cx4 1" />
            //  <gd name="cu4" fmla="+- q9 rh 0" />
            //  <gd name="cy4" fmla="+- b 0 cu4" />
            //  <gd name="cx5" fmla="+- r 0 cx4" />
            //  <gd name="cu6" fmla="+- cu3 rh 0" />
            //  <gd name="cy6" fmla="+- b 0 cu6" />
            //  <gd name="u7" fmla="+- u1 dy3 0" />
            //  <gd name="y7" fmla="+- b 0 u7" />
            //  <gd name="cu7" fmla="+- q1 q1 u7" />
            //  <gd name="cy7" fmla="+- b 0 cu7" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 100000);
            //<gd name="a2" fmla="pin 25000 adj2 75000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(25000, adj2, 75000);
            //<gd name="q10" fmla="+- 100000 0 a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q10 = 100000 + 0 - a1;
            //<gd name="q11" fmla="*/ q10 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q11 = q10 * 1 / 2;
            //<gd name="q12" fmla="+- a1 0 q11" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q12 = a1 + 0 - q11;
            //<gd name="minAdj3" fmla="max 0 q12" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var minAdj3 = System.Math.Max(0, q12);
            //<gd name="a3" fmla="pin minAdj3 adj3 a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a3 = Pin(minAdj3, adj3, a1);
            //<gd name="dx2" fmla="*/ w a2 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = w * a2 / 200000;
            //<gd name="x2" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - dx2;
            //<gd name="x3" fmla="+- x2 wd8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = x2 + wd8 - 0;
            //<gd name="x4" fmla="+- r 0 x3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = r + 0 - x3;
            //<gd name="x5" fmla="+- r 0 x2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = r + 0 - x2;
            //<gd name="x6" fmla="+- r 0 wd8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = r + 0 - wd8;
            //<gd name="dy1" fmla="*/ h a3 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = h * a3 / 100000;
            //<gd name="f1" fmla="*/ 4 dy1 w" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var f1 = 4 * dy1 / w;
            //<gd name="q1" fmla="*/ x3 x3 w" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q1 = x3 * x3 / w;
            //<gd name="q2" fmla="+- x3 0 q1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q2 = x3 + 0 - q1;
            //<gd name="u1" fmla="*/ f1 q2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u1 = f1 * q2 / 1;
            //<gd name="y1" fmla="+- b 0 u1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = b + 0 - u1;
            //<gd name="cx1" fmla="*/ x3 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cx1 = x3 * 1 / 2;
            //<gd name="cu1" fmla="*/ f1 cx1 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cu1 = f1 * cx1 / 1;
            //<gd name="cy1" fmla="+- b 0 cu1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cy1 = b + 0 - cu1;
            //<gd name="cx2" fmla="+- r 0 cx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cx2 = r + 0 - cx1;
            //<gd name="q1" fmla="*/ h a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            q1 = h * a1 / 100000;
            //<gd name="dy3" fmla="+- q1 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = q1 + 0 - dy1;
            //<gd name="q3" fmla="*/ x2 x2 w" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q3 = x2 * x2 / w;
            //<gd name="q4" fmla="+- x2 0 q3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q4 = x2 + 0 - q3;
            //<gd name="q5" fmla="*/ f1 q4 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q5 = f1 * q4 / 1;
            //<gd name="u3" fmla="+- q5 dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u3 = q5 + dy3 - 0;
            //<gd name="y3" fmla="+- b 0 u3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = b + 0 - u3;
            //<gd name="q6" fmla="+- dy1 dy3 u3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q6 = dy1 + dy3 - u3;
            //<gd name="q7" fmla="+- q6 dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q7 = q6 + dy1 - 0;
            //<gd name="cu3" fmla="+- q7 dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cu3 = q7 + dy3 - 0;
            //<gd name="cy3" fmla="+- b 0 cu3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cy3 = b + 0 - cu3;
            //<gd name="rh" fmla="+- b 0 q1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh = b + 0 - q1;
            //<gd name="q8" fmla="*/ dy1 14 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q8 = dy1 * 14 / 16;
            //<gd name="u2" fmla="+/ q8 rh 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u2 = (q8 + rh) / 2;
            //<gd name="y2" fmla="+- b 0 u2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = b + 0 - u2;
            //<gd name="u5" fmla="+- q5 rh 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u5 = q5 + rh - 0;
            //<gd name="y5" fmla="+- b 0 u5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = b + 0 - u5;
            //<gd name="u6" fmla="+- u3 rh 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u6 = u3 + rh - 0;
            //<gd name="y6" fmla="+- b 0 u6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = b + 0 - u6;
            //<gd name="cx4" fmla="*/ x2 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cx4 = x2 * 1 / 2;
            //<gd name="q9" fmla="*/ f1 cx4 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q9 = f1 * cx4 / 1;
            //<gd name="cu4" fmla="+- q9 rh 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cu4 = q9 + rh - 0;
            //<gd name="cy4" fmla="+- b 0 cu4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cy4 = b + 0 - cu4;
            //<gd name="cx5" fmla="+- r 0 cx4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cx5 = r + 0 - cx4;
            //<gd name="cu6" fmla="+- cu3 rh 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cu6 = cu3 + rh - 0;
            //<gd name="cy6" fmla="+- b 0 cu6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cy6 = b + 0 - cu6;
            //<gd name="u7" fmla="+- u1 dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u7 = u1 + dy3 - 0;
            //<gd name="y7" fmla="+- b 0 u7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y7 = b + 0 - u7;
            //<gd name="cu7" fmla="+- q1 q1 u7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cu7 = q1 + q1 - u7;
            //<gd name="cy7" fmla="+- b 0 cu7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cy7 = b + 0 - cu7;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <quadBezTo>
            //      <pt x="cx1" y="cy1" />
            //      <pt x="x3" y="y1" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x2" y="y3" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="hc" y="cy3" />
            //      <pt x="x5" y="y3" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx2" y="cy1" />
            //      <pt x="r" y="b" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="q1" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx5" y="cy4" />
            //      <pt x="x5" y="y5" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x5" y="y6" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="hc" y="cy6" />
            //      <pt x="x2" y="y6" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x2" y="y5" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx4" y="cy4" />
            //      <pt x="l" y="q1" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="wd8" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="darkenLess" stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x3" y="y7" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y3" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="hc" y="cy3" />
            //      <pt x="x5" y="y3" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y7" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="hc" y="cy7" />
            //      <pt x="x3" y="y7" />
            //    </quadBezTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="wd8" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="q1" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx4" y="cy4" />
            //      <pt x="x2" y="y5" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x2" y="y6" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="hc" y="cy6" />
            //      <pt x="x5" y="y6" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x5" y="y5" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx5" y="cy4" />
            //      <pt x="r" y="q1" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx2" y="cy1" />
            //      <pt x="x4" y="y1" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x5" y="y3" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="hc" y="cy3" />
            //      <pt x="x2" y="y3" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="cx1" y="cy1" />
            //      <pt x="l" y="b" />
            //    </quadBezTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x2" y="y3" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y5" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x5" y="y5" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x5" y="y3" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x3" y="y7" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x4" y="y7" />
            //    </lnTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx1" y="cy1" />
            //  <pt x="x3" y="y1" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx1, cy1, x3, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y3);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="cy3" />
            //  <pt x="x5" y="y3" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, cy3, x5, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx2" y="cy1" />
            //  <pt x="r" y="b" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx2, cy1, r, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="q1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, q1);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx5" y="cy4" />
            //  <pt x="x5" y="y5" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx5, cy4, x5, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y6);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="cy6" />
            //  <pt x="x2" y="y6" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, cy6, x2, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y5);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx4" y="cy4" />
            //  <pt x="l" y="q1" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx4, cy4, l, q1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="wd8" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, wd8, y2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path fill="darkenLess"stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y7" />
            //</moveTo>
            currentPoint = new EmuPoint(x3, y7);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y3);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="cy3" />
            //  <pt x="x5" y="y3" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, cy3, x5, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y7);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="cy7" />
            //  <pt x="x3" y="y7" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, cy7, x3, y7);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</moveTo>
            currentPoint = new EmuPoint(l, b);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="wd8" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, wd8, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="q1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, q1);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx4" y="cy4" />
            //  <pt x="x2" y="y5" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx4, cy4, x2, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y6);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="cy6" />
            //  <pt x="x5" y="y6" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, cy6, x5, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y5);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx5" y="cy4" />
            //  <pt x="r" y="q1" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx5, cy4, r, q1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx2" y="cy1" />
            //  <pt x="x4" y="y1" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx2, cy1, x4, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y3);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="cy3" />
            //  <pt x="x2" y="y3" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, cy3, x2, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="cx1" y="cy1" />
            //  <pt x="l" y="b" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, cx1, cy1, l, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y3" />
            //</moveTo>
            currentPoint = new EmuPoint(x2, y3);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y5);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y5" />
            //</moveTo>
            currentPoint = new EmuPoint(x5, y5);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y3);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y7" />
            //</moveTo>
            currentPoint = new EmuPoint(x3, y7);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y1" />
            //</moveTo>
            currentPoint = new EmuPoint(x4, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y7);
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="x2" t="y6" r="x5" b="rh" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x2, y6, x5, rh);

            return shapePaths;
        }
    }


}

