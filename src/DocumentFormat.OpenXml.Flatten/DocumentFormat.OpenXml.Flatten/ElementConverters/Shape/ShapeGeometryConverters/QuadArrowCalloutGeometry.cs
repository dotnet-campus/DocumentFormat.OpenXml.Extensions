using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 标注: 十字箭头
    /// </summary>
    public class QuadArrowCalloutGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 18515" />
            //  <gd name="adj2" fmla="val 18515" />
            //  <gd name="adj3" fmla="val 18515" />
            //  <gd name="adj4" fmla="val 48123" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 18515d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 18515d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 18515d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? 48123d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="maxAdj3" fmla="+- 50000 0 a2" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="q2" fmla="*/ a3 2 1" />
            //  <gd name="maxAdj4" fmla="+- 100000 0 q2" />
            //  <gd name="a4" fmla="pin a1 adj4 maxAdj4" />
            //  <gd name="dx2" fmla="*/ ss a2 100000" />
            //  <gd name="dx3" fmla="*/ ss a1 200000" />
            //  <gd name="ah" fmla="*/ ss a3 100000" />
            //  <gd name="dx1" fmla="*/ w a4 200000" />
            //  <gd name="dy1" fmla="*/ h a4 200000" />
            //  <gd name="x8" fmla="+- r 0 ah" />
            //  <gd name="x2" fmla="+- hc 0 dx1" />
            //  <gd name="x7" fmla="+- hc dx1 0" />
            //  <gd name="x3" fmla="+- hc 0 dx2" />
            //  <gd name="x6" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc 0 dx3" />
            //  <gd name="x5" fmla="+- hc dx3 0" />
            //  <gd name="y8" fmla="+- b 0 ah" />
            //  <gd name="y2" fmla="+- vc 0 dy1" />
            //  <gd name="y7" fmla="+- vc dy1 0" />
            //  <gd name="y3" fmla="+- vc 0 dx2" />
            //  <gd name="y6" fmla="+- vc dx2 0" />
            //  <gd name="y4" fmla="+- vc 0 dx3" />
            //  <gd name="y5" fmla="+- vc dx3 0" />
            //</gdLst>



            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            var maxAdj1 = a2 * 2 / 1;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="maxAdj3" fmla="+- 50000 0 a2" />
            var maxAdj3 = 50000 - a2;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="q2" fmla="*/ a3 2 1" />
            var q2 = a3 * 2 / 1;
            //  <gd name="maxAdj4" fmla="+- 100000 0 q2" />
            var maxAdj4 = 100000 - q2;
            //  <gd name="a4" fmla="pin a1 adj4 maxAdj4" />
            var a4 = Pin(a1, adj4, maxAdj4);
            //  <gd name="dx2" fmla="*/ ss a2 100000" />
            var dx2 = ss * a2 / 100000;
            //  <gd name="dx3" fmla="*/ ss a1 200000" />
            var dx3 = ss * a1 / 200000;
            //  <gd name="ah" fmla="*/ ss a3 100000" />
            var ah = ss * a3 / 100000;
            //  <gd name="dx1" fmla="*/ w a4 200000" />
            var dx1 = w * a4 / 200000;
            //  <gd name="dy1" fmla="*/ h a4 200000" />
            var dy1 = h * a4 / 200000;
            //  <gd name="x8" fmla="+- r 0 ah" />
            var x8 = r - ah;
            //  <gd name="x2" fmla="+- hc 0 dx1" />
            var x2 = hc - dx1;
            //  <gd name="x7" fmla="+- hc dx1 0" />
            var x7 = hc + dx1;
            //  <gd name="x3" fmla="+- hc 0 dx2" />
            var x3 = hc - dx2;
            //  <gd name="x6" fmla="+- hc dx2 0" />
            var x6 = hc + dx2;
            //  <gd name="x4" fmla="+- hc 0 dx3" />
            var x4 = hc - dx3;
            //  <gd name="x5" fmla="+- hc dx3 0" />
            var x5 = hc + dx3;
            //  <gd name="y8" fmla="+- b 0 ah" />
            var y8 = b - ah;
            //  <gd name="y2" fmla="+- vc 0 dy1" />
            var y2 = vc - dy1;
            //  <gd name="y7" fmla="+- vc dy1 0" />
            var y7 = vc + dy1;
            //  <gd name="y3" fmla="+- vc 0 dx2" />
            var y3 = vc - dx2;
            //  <gd name="y6" fmla="+- vc dx2 0" />
            var y6 = vc + dx2;
            //  <gd name="y4" fmla="+- vc 0 dx3" />
            var y4 = vc - dx3;
            //  <gd name="y5" fmla="+- vc dx3 0" />
            var y5 = vc + dx3;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="ah" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ah" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="ah" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="ah" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="ah" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="ah" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ah" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ah" y="y6" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="ah" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ah, y3);
            //    <lnTo>
            //      <pt x="ah" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ah, y4);
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y4);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //    <lnTo>
            //      <pt x="x4" y="ah" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, ah);
            //    <lnTo>
            //      <pt x="x3" y="ah" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, ah);
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //    <lnTo>
            //      <pt x="x6" y="ah" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x6, ah);
            //    <lnTo>
            //      <pt x="x5" y="ah" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, ah);
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y2);
            //    <lnTo>
            //      <pt x="x7" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x7, y2);
            //    <lnTo>
            //      <pt x="x7" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x7, y4);
            //    <lnTo>
            //      <pt x="x8" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y4);
            //    <lnTo>
            //      <pt x="x8" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y3);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x8" y="y6" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y6);
            //    <lnTo>
            //      <pt x="x8" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x8, y5);
            //    <lnTo>
            //      <pt x="x7" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x7, y5);
            //    <lnTo>
            //      <pt x="x7" y="y7" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x7, y7);
            //    <lnTo>
            //      <pt x="x5" y="y7" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y7);
            //    <lnTo>
            //      <pt x="x5" y="y8" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y8);
            //    <lnTo>
            //      <pt x="x6" y="y8" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x6, y8);
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //    <lnTo>
            //      <pt x="x3" y="y8" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y8);
            //    <lnTo>
            //      <pt x="x4" y="y8" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y8);
            //    <lnTo>
            //      <pt x="x4" y="y7" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y7);
            //    <lnTo>
            //      <pt x="x2" y="y7" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y7);
            //    <lnTo>
            //      <pt x="x2" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y5);
            //    <lnTo>
            //      <pt x="ah" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ah, y5);
            //    <lnTo>
            //      <pt x="ah" y="y6" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ah, y6);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="x2" t="y2" r="x7" b="y7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x2, y2, x7, y7);

            return shapePaths;

        }
    }
}
