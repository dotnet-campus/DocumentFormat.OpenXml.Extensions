using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 左右箭头
    /// </summary>
    public class LeftRightArrowGeometry : ShapeGeometryBase
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
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 50000 w ss" />
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="x2" fmla="*/ ss a2 100000" />
            //  <gd name="x3" fmla="+- r 0 x2" />
            //  <gd name="dy" fmla="*/ h a1 200000" />
            //  <gd name="y1" fmla="+- vc 0 dy" />
            //  <gd name="y2" fmla="+- vc dy 0" />
            //  <gd name="dx1" fmla="*/ y1 x2 hd2" />
            //  <gd name="x1" fmla="+- x2 0 dx1" />
            //  <gd name="x4" fmla="+- x3 dx1 0" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 50000 w ss" />
            var maxAdj2 = 50000 * w / ss;
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            var a1 = Pin(0, adj1, 100000);
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="x2" fmla="*/ ss a2 100000" />
            var x2 = ss * a2 / 100000;
            //  <gd name="x3" fmla="+- r 0 x2" />
            var x3 = r - x2;
            //  <gd name="dy" fmla="*/ h a1 200000" />
            var dy = h * a1 / 200000;
            //  <gd name="y1" fmla="+- vc 0 dy" />
            var y1 = vc - dy;
            //  <gd name="y2" fmla="+- vc dy 0" />
            var y2 = vc + dy;
            //  <gd name="dx1" fmla="*/ y1 x2 hd2" />
            var dx1 = y1 * x2 / hd2;
            //  <gd name="x1" fmla="+- x2 0 dx1" />
            var x1 = x2 - dx1;
            //  <gd name="x4" fmla="+- x3 dx1 0" />
            var x4 = x3 + dx1;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
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
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x3" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, b);
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y2);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, b);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="x1" t="y1" r="x4" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y1, x4, y2);

            return shapePaths;
        }
    }
}
