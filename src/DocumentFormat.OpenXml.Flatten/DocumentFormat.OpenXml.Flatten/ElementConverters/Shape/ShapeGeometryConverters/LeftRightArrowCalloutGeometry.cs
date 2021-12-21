using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 标注: 左右箭头
    /// </summary>
    public class LeftRightArrowCalloutGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }


        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 25000" />
            //  <gd name="adj3" fmla="val 25000" />
            //  <gd name="adj4" fmla="val 48123" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 25000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 25000d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? 48123d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 50000 h ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="maxAdj3" fmla="*/ 50000 w ss" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="q2" fmla="*/ a3 ss wd2" />
            //  <gd name="maxAdj4" fmla="+- 100000 0 q2" />
            //  <gd name="a4" fmla="pin 0 adj4 maxAdj4" />
            //  <gd name="dy1" fmla="*/ ss a2 100000" />
            //  <gd name="dy2" fmla="*/ ss a1 200000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc dy2 0" />
            //  <gd name="y4" fmla="+- vc dy1 0" />
            //  <gd name="x1" fmla="*/ ss a3 100000" />
            //  <gd name="x4" fmla="+- r 0 x1" />
            //  <gd name="dx2" fmla="*/ w a4 200000" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //</gdLst>


            //  <gd name="maxAdj2" fmla="*/ 50000 h ss" />
            var maxAdj2 = 50000 * h / ss;
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            var maxAdj1 = a2 * 2 / 1;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="maxAdj3" fmla="*/ 50000 w ss" />
            var maxAdj3 = 50000 * w / ss;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="q2" fmla="*/ a3 ss wd2" />
            var q2 = a3 * ss / wd2;
            //  <gd name="maxAdj4" fmla="+- 100000 0 q2" />
            var maxAdj4 = 100000 - q2;
            //  <gd name="a4" fmla="pin 0 adj4 maxAdj4" />
            var a4 = Pin(0, adj4, maxAdj4);
            //  <gd name="dy1" fmla="*/ ss a2 100000" />
            var dy1 = ss * a2 / 100000;
            //  <gd name="dy2" fmla="*/ ss a1 200000" />
            var dy2 = ss * a1 / 200000;
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            var y1 = vc - dy1;
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            var y2 = vc - dy2;
            //  <gd name="y3" fmla="+- vc dy2 0" />
            var y3 = vc + dy2;
            //  <gd name="y4" fmla="+- vc dy1 0" />
            var y4 = vc + dy1;
            //  <gd name="x1" fmla="*/ ss a3 100000" />
            var x1 = ss * a3 / 100000;
            //  <gd name="x4" fmla="+- r 0 x1" />
            var x4 = r - x1;
            //  <gd name="dx2" fmla="*/ w a4 200000" />
            var dx2 = w * a4 / 200000;
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            var x2 = hc - dx2;
            //  <gd name="x3" fmla="+- hc dx2 0" />
            var x3 = hc + dx2;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y4" />
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
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y2);
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y4);
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y3);
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y3);
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, b);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="x2" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y3);
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="x2" t="t" r="x3" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x2, t, x3, b);

            return shapePaths;
        }
    }
}
