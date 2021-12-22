using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头: 五边形
    /// </summary>
    public class HomePlateGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 50000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 50000d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 100000 w ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="dx1" fmla="*/ ss a 100000" />
            //  <gd name="x1" fmla="+- r 0 dx1" />
            //  <gd name="ir" fmla="+/ x1 r 2" />
            //  <gd name="x2" fmla="*/ x1 1 2" />
            //</gdLst>

            //  <gd name="maxAdj" fmla="*/ 100000 w ss" />
            var maxAdj = 100000 * w / ss;
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //  <gd name="dx1" fmla="*/ ss a 100000" />
            var dx1 = ss * a / 100000;
            //  <gd name="x1" fmla="+- r 0 dx1" />
            var x1 = r - dx1;
            //  <gd name="ir" fmla="+/ x1 r 2" />
            var ir = (x1 + r) / 2;
            //  <gd name="x2" fmla="*/ x1 1 2" />
            var x2 = x1 * 1 / 2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
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
            //      <pt x="x1" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, t);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            // <rect l="l" t="t" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, ir, b);

            return shapePaths;

        }
    }
}
