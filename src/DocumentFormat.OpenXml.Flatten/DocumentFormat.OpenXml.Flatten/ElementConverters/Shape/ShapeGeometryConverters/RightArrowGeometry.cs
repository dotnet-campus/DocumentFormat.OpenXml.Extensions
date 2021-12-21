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
    /// 箭头：右
    /// </summary>
    public class RightArrowGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 100000 w ss" />
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="dx1" fmla="*/ ss a2 100000" />
            //  <gd name="x1" fmla="+- r 0 dx1" />
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc dy1 0" />
            //  <gd name="dx2" fmla="*/ y1 dx1 hd2" />
            //  <gd name="x2" fmla="+- x1 dx2 0" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 100000 w ss" />
            var maxAdj2 = 100000 * w / ss;
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            var a1 = Pin(0, adj1, 100000);
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="dx1" fmla="*/ ss a2 100000" />
            var dx1 = ss * a2 / 100000;
            //  <gd name="x1" fmla="+- r 0 dx1" />
            var x1 = r - dx1;
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            var dy1 = h * a1 / 200000;
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            var y1 = vc - dy1;
            //  <gd name="y2" fmla="+- vc dy1 0" />
            var y2 = vc + dy1;
            //  <gd name="dx2" fmla="*/ y1 dx1 hd2" />
            var dx2 = y1 * dx1 / hd2;
            //  <gd name="x2" fmla="+- x1 dx2 0" />
            var x2 = x1 + dx2;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
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
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, t);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, y2);
            //    <close />
            //  </path>
            stringPath.Append("z ");

            //<rect l="l" t="y1" r="x2" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, y1, x2, y2);

            return stringPath.ToString();
        }

        /// <inheritdoc />
        public override ShapePath[] GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var shapePaths = new ShapePath[1];
            shapePaths[0] = new ShapePath(ToGeometryPathString(emuSize, adjusts)!);

            return shapePaths;
        }
    }
}
