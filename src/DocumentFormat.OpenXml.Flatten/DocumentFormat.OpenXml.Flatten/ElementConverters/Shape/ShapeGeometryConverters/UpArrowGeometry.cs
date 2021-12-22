using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 上箭头
    /// </summary>
    public class UpArrowGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            // <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            // </avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //       <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 100000 h ss" />
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="dy2" fmla="*/ ss a2 100000" />
            //  <gd name="y2" fmla="+- t dy2 0" />
            //  <gd name="dx1" fmla="*/ w a1 200000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc dx1 0" />
            //  <gd name="dy1" fmla="*/ x1 dy2 wd2" />
            //  <gd name="y1" fmla="+- y2  0 dy1" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 100000 h ss" />
            var maxAdj2 = 100000 * h / ss;
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            var a1 = Pin(0, adj1, 100000);
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="dy2" fmla="*/ ss a2 100000" />
            var dy2 = ss * a2 / 100000;
            //  <gd name="y2" fmla="+- t dy2 0" />
            var y2 = t + dy2;
            //  <gd name="dx1" fmla="*/ w a1 200000" />
            var dx1 = w * a1 / 200000;
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            var x1 = hc - dx1;
            //  <gd name="x2" fmla="+- hc dx1 0" />
            var x2 = hc + dx1;
            //  <gd name="dy1" fmla="*/ x1 dy2 wd2" />
            var dy1 = x1 * dy2 / wd2;
            //  <gd name="y1" fmla="+- y2  0 dy1" />
            var y1 = y2 - dy1;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <close/>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //    <close/>
            stringPath.Append("z ");
            //  </path>

            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="x1" t="y1" r="x2" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y1, x2, b);

            return shapePaths;
        }
    }
}
