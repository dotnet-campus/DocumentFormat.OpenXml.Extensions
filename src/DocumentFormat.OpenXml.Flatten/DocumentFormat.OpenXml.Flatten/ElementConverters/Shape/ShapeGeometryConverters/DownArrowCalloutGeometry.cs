using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 标注: 下箭头
    /// </summary>
    public class DownArrowCalloutGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 25000" />
            //  <gd name="adj3" fmla="val 25000" />
            //  <gd name="adj4" fmla="val 64977" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 25000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 25000d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? 64977d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 50000 w ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="maxAdj3" fmla="*/ 100000 h ss" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="q2" fmla="*/ a3 ss h" />
            //  <gd name="maxAdj4" fmla="+- 100000 0 q2" />
            //  <gd name="a4" fmla="pin 0 adj4 maxAdj4" />
            //  <gd name="dx1" fmla="*/ ss a2 100000" />
            //  <gd name="dx2" fmla="*/ ss a1 200000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc dx1 0" />
            //  <gd name="dy3" fmla="*/ ss a3 100000" />
            //  <gd name="y3" fmla="+- b 0 dy3" />
            //  <gd name="y2" fmla="*/ h a4 100000" />
            //  <gd name="y1" fmla="*/ y2 1 2" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 50000 w ss" />
            var maxAdj2 = 50000 * w / ss;
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            var maxAdj1 = a2 * 2 / 1;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="maxAdj3" fmla="*/ 100000 h ss" />
            var maxAdj3 = 100000 * h / ss;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="q2" fmla="*/ a3 ss h" />
            var q2 = a3 * ss / h;
            //  <gd name="maxAdj4" fmla="+- 100000 0 q2" />
            var maxAdj4 = 100000 - q2;
            //  <gd name="a4" fmla="pin 0 adj4 maxAdj4" />
            var a4 = Pin(0, adj4, maxAdj4);
            //  <gd name="dx1" fmla="*/ ss a2 100000" />
            var dx1 = ss * a2 / 100000;
            //  <gd name="dx2" fmla="*/ ss a1 200000" />
            var dx2 = ss * a1 / 200000;
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            var x1 = hc - dx1;
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            var x2 = hc - dx2;
            //  <gd name="x3" fmla="+- hc dx2 0" />
            var x3 = hc + dx2;
            //  <gd name="x4" fmla="+- hc dx1 0" />
            var x4 = hc + dx1;
            //  <gd name="dy3" fmla="*/ ss a3 100000" />
            var dy3 = ss * a3 / 100000;
            //  <gd name="y3" fmla="+- b 0 dy3" />
            var y3 = b - dy3;
            //  <gd name="y2" fmla="*/ h a4 100000" />
            var y2 = h * a4 / 100000;
            //  <gd name="y1" fmla="*/ y2 1 2" />
            var y1 = y2 * 1 / 2;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y2);
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y3);
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y3);
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //    <lnTo>
            //      <pt x="x2" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y3);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y2);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="l" t="t" r="r" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, y2);

            return shapePaths;

        }
    }
}
