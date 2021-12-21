using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 双波形
    /// </summary>
    public class DoubleWaveGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 6250" />
            //  <gd name="adj2" fmla="val 0" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 6250d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 0d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 12500" />
            //  <gd name="a2" fmla="pin -10000 adj2 10000" />
            //  <gd name="y1" fmla="*/ h a1 100000" />
            //  <gd name="dy2" fmla="*/ y1 10 3" />
            //  <gd name="y2" fmla="+- y1 0 dy2" />
            //  <gd name="y3" fmla="+- y1 dy2 0" />
            //  <gd name="y4" fmla="+- b 0 y1" />
            //  <gd name="y5" fmla="+- y4 0 dy2" />
            //  <gd name="y6" fmla="+- y4 dy2 0" />
            //  <gd name="dx1" fmla="*/ w a2 100000" />
            //  <gd name="of2" fmla="*/ w a2 50000" />
            //  <gd name="x1" fmla="abs dx1" />
            //  <gd name="dx2" fmla="?: of2 0 of2" />
            //  <gd name="x2" fmla="+- l 0 dx2" />
            //  <gd name="dx8" fmla="?: of2 of2 0" />
            //  <gd name="x8" fmla="+- r 0 dx8" />
            //  <gd name="dx3" fmla="+/ dx2 x8 6" />
            //  <gd name="x3" fmla="+- x2 dx3 0" />
            //  <gd name="dx4" fmla="+/ dx2 x8 3" />
            //  <gd name="x4" fmla="+- x2 dx4 0" />
            //  <gd name="x5" fmla="+/ x2 x8 2" />
            //  <gd name="x6" fmla="+- x5 dx3 0" />
            //  <gd name="x7" fmla="+/ x6 x8 2" />
            //  <gd name="x9" fmla="+- l dx8 0" />
            //  <gd name="x15" fmla="+- r dx2 0" />
            //  <gd name="x10" fmla="+- x9 dx3 0" />
            //  <gd name="x11" fmla="+- x9 dx4 0" />
            //  <gd name="x12" fmla="+/ x9 x15 2" />
            //  <gd name="x13" fmla="+- x12 dx3 0" />
            //  <gd name="x14" fmla="+/ x13 x15 2" />
            //  <gd name="x16" fmla="+- r 0 x1" />
            //  <gd name="xAdj" fmla="+- hc dx1 0" />
            //  <gd name="il" fmla="max x2 x9" />
            //  <gd name="ir" fmla="min x8 x15" />
            //  <gd name="it" fmla="*/ h a1 50000" />
            //  <gd name="ib" fmla="+- b 0 it" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 12500" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 12500);
            //<gd name="a2" fmla="pin -10000 adj2 10000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(-10000, adj2, 10000);
            //<gd name="y1" fmla="*/ h a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = h * a1 / 100000;
            //<gd name="dy2" fmla="*/ y1 10 3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = y1 * 10 / 3;
            //<gd name="y2" fmla="+- y1 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = y1 + 0 - dy2;
            //<gd name="y3" fmla="+- y1 dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = y1 + dy2 - 0;
            //<gd name="y4" fmla="+- b 0 y1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = b + 0 - y1;
            //<gd name="y5" fmla="+- y4 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = y4 + 0 - dy2;
            //<gd name="y6" fmla="+- y4 dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = y4 + dy2 - 0;
            //<gd name="dx1" fmla="*/ w a2 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = w * a2 / 100000;
            //<gd name="of2" fmla="*/ w a2 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var of2 = w * a2 / 50000;
            //<gd name="x1" fmla="abs dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = Abs(dx1);
            //<gd name="dx2" fmla="?: of2 0 of2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = of2 > 0 ? 0 : of2;
            //<gd name="x2" fmla="+- l 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = l + 0 - dx2;
            //<gd name="dx8" fmla="?: of2 of2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx8 = of2 > 0 ? of2 : 0;
            //<gd name="x8" fmla="+- r 0 dx8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x8 = r + 0 - dx8;
            //<gd name="dx3" fmla="+/ dx2 x8 6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = (dx2 + x8) / 6;
            //<gd name="x3" fmla="+- x2 dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = x2 + dx3 - 0;
            //<gd name="dx4" fmla="+/ dx2 x8 3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx4 = (dx2 + x8) / 3;
            //<gd name="x4" fmla="+- x2 dx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = x2 + dx4 - 0;
            //<gd name="x5" fmla="+/ x2 x8 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = (x2 + x8) / 2;
            //<gd name="x6" fmla="+- x5 dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = x5 + dx3 - 0;
            //<gd name="x7" fmla="+/ x6 x8 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x7 = (x6 + x8) / 2;
            //<gd name="x9" fmla="+- l dx8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x9 = l + dx8 - 0;
            //<gd name="x15" fmla="+- r dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x15 = r + dx2 - 0;
            //<gd name="x10" fmla="+- x9 dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x10 = x9 + dx3 - 0;
            //<gd name="x11" fmla="+- x9 dx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x11 = x9 + dx4 - 0;
            //<gd name="x12" fmla="+/ x9 x15 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x12 = (x9 + x15) / 2;
            //<gd name="x13" fmla="+- x12 dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x13 = x12 + dx3 - 0;
            //<gd name="x14" fmla="+/ x13 x15 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x14 = (x13 + x15) / 2;
            //<gd name="x16" fmla="+- r 0 x1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x16 = r + 0 - x1;
            //<gd name="xAdj" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xAdj = hc + dx1 - 0;
            //<gd name="il" fmla="max x2 x9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = System.Math.Max(x2, x9);
            //<gd name="ir" fmla="min x8 x15" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = System.Math.Min(x8, x15);
            //<gd name="it" fmla="*/ h a1 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var it = h * a1 / 50000;
            //<gd name="ib" fmla="+- b 0 it" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = b + 0 - it;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x2" y="y1" />
            //    </moveTo>
            //    <cubicBezTo>
            //      <pt x="x3" y="y2" />
            //      <pt x="x4" y="y3" />
            //      <pt x="x5" y="y1" />
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x6" y="y2" />
            //      <pt x="x7" y="y3" />
            //      <pt x="x8" y="y1" />
            //    </cubicBezTo>
            //    <lnTo>
            //      <pt x="x15" y="y4" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="x14" y="y6" />
            //      <pt x="x13" y="y5" />
            //      <pt x="x12" y="y4" />
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x11" y="y6" />
            //      <pt x="x10" y="y5" />
            //      <pt x="x9" y="y4" />
            //    </cubicBezTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y1" />
            //</moveTo>
            var currentPoint = new EmuPoint(x2, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y2" />
            //  <pt x="x4" y="y3" />
            //  <pt x="x5" y="y1" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x3, y2, x4, y3, x5, y1);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //  <pt x="x7" y="y3" />
            //  <pt x="x8" y="y1" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x6, y2, x7, y3, x8, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x15" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x15, y4);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x14" y="y6" />
            //  <pt x="x13" y="y5" />
            //  <pt x="x12" y="y4" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x14, y6, x13, y5, x12, y4);
            //<cubicBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x11" y="y6" />
            //  <pt x="x10" y="y5" />
            //  <pt x="x9" y="y4" />
            //</cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x11, y6, x10, y5, x9, y4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

