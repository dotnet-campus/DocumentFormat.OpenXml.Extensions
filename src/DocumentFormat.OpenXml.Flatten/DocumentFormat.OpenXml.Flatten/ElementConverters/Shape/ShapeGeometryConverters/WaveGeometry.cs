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
    /// 波形
    /// </summary>
    public class WaveGeometry : ShapeGeometryBase
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
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 12500" />
            //  <gd name="adj2" fmla="val 0" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 12500d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 0d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 20000" />
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
            //  <gd name="dx5" fmla="?: of2 of2 0" />
            //  <gd name="x5" fmla="+- r 0 dx5" />
            //  <gd name="dx3" fmla="+/ dx2 x5 3" />
            //  <gd name="x3" fmla="+- x2 dx3 0" />
            //  <gd name="x4" fmla="+/ x3 x5 2" />
            //  <gd name="x6" fmla="+- l dx5 0" />
            //  <gd name="x10" fmla="+- r dx2 0" />
            //  <gd name="x7" fmla="+- x6 dx3 0" />
            //  <gd name="x8" fmla="+/ x7 x10 2" />
            //  <gd name="x9" fmla="+- r 0 x1" />
            //  <gd name="xAdj" fmla="+- hc dx1 0" />
            //  <gd name="xAdj2" fmla="+- hc 0 dx1" />
            //  <gd name="il" fmla="max x2 x6" />
            //  <gd name="ir" fmla="min x5 x10" />
            //  <gd name="it" fmla="*/ h a1 50000" />
            //  <gd name="ib" fmla="+- b 0 it" />
            //</gdLst>


            //  <gd name="a1" fmla="pin 0 adj1 20000" />
            var a1 = Pin(0, adj1, 20000);
            //  <gd name="a2" fmla="pin -10000 adj2 10000" />
            var a2 = Pin(-10000, adj2, 10000);
            //  <gd name="y1" fmla="*/ h a1 100000" />
            var y1 = h * a1 / 100000;
            //  <gd name="dy2" fmla="*/ y1 10 3" />
            var dy2 = y1 * 10 / 3;
            //  <gd name="y2" fmla="+- y1 0 dy2" />
            var y2 = y1 - dy2;
            //  <gd name="y3" fmla="+- y1 dy2 0" />
            var y3 = y1 + dy2;
            //  <gd name="y4" fmla="+- b 0 y1" />
            var y4 = b - y1;
            //  <gd name="y5" fmla="+- y4 0 dy2" />
            var y5 = y4 - dy2;
            //  <gd name="y6" fmla="+- y4 dy2 0" />
            var y6 = y4 + dy2;
            //  <gd name="dx1" fmla="*/ w a2 100000" />
            var dx1 = w * a2 / 100000;
            //  <gd name="of2" fmla="*/ w a2 50000" />
            var of2 = w * a2 / 50000;
            //  <gd name="x1" fmla="abs dx1" />
            var x1 = System.Math.Abs(dx1);
            //  <gd name="dx2" fmla="?: of2 0 of2" />
            var dx2 = of2 > 0 ? 0 : of2;
            //  <gd name="x2" fmla="+- l 0 dx2" />
            var x2 = l - dx2;
            //  <gd name="dx5" fmla="?: of2 of2 0" />
            var dx5 = of2 > 0 ? of2 : 0;
            //  <gd name="x5" fmla="+- r 0 dx5" />
            var x5 = r - dx5;
            //  <gd name="dx3" fmla="+/ dx2 x5 3" />
            var dx3 = (dx2 + x5) / 3;
            //  <gd name="x3" fmla="+- x2 dx3 0" />
            var x3 = x2 + dx3;
            //  <gd name="x4" fmla="+/ x3 x5 2" />
            var x4 = (x3 + x5) / 2;
            //  <gd name="x6" fmla="+- l dx5 0" />
            var x6 = l + dx5;
            //  <gd name="x10" fmla="+- r dx2 0" />
            var x10 = r + dx2;
            //  <gd name="x7" fmla="+- x6 dx3 0" />
            var x7 = x6 + dx3;
            //  <gd name="x8" fmla="+/ x7 x10 2" />
            var x8 = (x7 + x10) / 2;
            //  <gd name="x9" fmla="+- r 0 x1" />
            var x9 = r - x1;
            //  <gd name="xAdj" fmla="+- hc dx1 0" />
            var xAdj = hc + dx1;
            //  <gd name="xAdj2" fmla="+- hc 0 dx1" />
            var xAdj2 = hc - dx1;
            //  <gd name="il" fmla="max x2 x6" />
            var il = System.Math.Max(x2, x6);
            //  <gd name="ir" fmla="min x5 x10" />
            var ir = System.Math.Min(x5, x10);
            //  <gd name="it" fmla="*/ h a1 50000" />
            var it = h * a1 / 50000;
            //  <gd name="ib" fmla="+- b 0 it" />
            var ib = b - it;


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
            //    <lnTo>
            //      <pt x="x10" y="y4" />
            //    </lnTo>
            //    <cubicBezTo>
            //      <pt x="x8" y="y6" />
            //      <pt x="x7" y="y5" />
            //      <pt x="x6" y="y4" />
            //    </cubicBezTo>
            //    <close />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="x2" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x2, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <cubicBezTo>
            //      <pt x="x3" y="y2" />
            //      <pt x="x4" y="y3" />
            //      <pt x="x5" y="y1" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x3, y2, x4, y3, x5, y1);
            //    <lnTo>
            //      <pt x="x10" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x10, y4);
            //    <cubicBezTo>
            //      <pt x="x8" y="y6" />
            //      <pt x="x7" y="y5" />
            //      <pt x="x6" y="y4" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x8, y6, x7, y5, x6, y4);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //< rect l = "il" t = "it" r = "ir" b = "ib" xmlns = "http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
