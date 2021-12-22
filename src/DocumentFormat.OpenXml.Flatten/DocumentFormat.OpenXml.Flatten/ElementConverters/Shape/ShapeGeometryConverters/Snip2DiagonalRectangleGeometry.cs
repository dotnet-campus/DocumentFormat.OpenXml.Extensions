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
    ///     矩形：剪去对角
    /// </summary>
    internal class Snip2DiagonalRectangleGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 0" />
            //  <gd name="adj2" fmla="val 16667" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj1 = customAdj1 ?? 0d;
            var adj2 = customAdj2 ?? 16667d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="lx1" fmla="*/ ss a1 100000" />
            //  <gd name="lx2" fmla="+- r 0 lx1" />
            //  <gd name="ly1" fmla="+- b 0 lx1" />
            //  <gd name="rx1" fmla="*/ ss a2 100000" />
            //  <gd name="rx2" fmla="+- r 0 rx1" />
            //  <gd name="ry1" fmla="+- b 0 rx1" />
            //  <gd name="d" fmla="+- lx1 0 rx1" />
            //  <gd name="dx" fmla="?: d lx1 rx1" />
            //  <gd name="il" fmla="*/ dx 1 2" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 il" />
            //</gdLst>


            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            var a1 = Pin(0, adj1, 50000);
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="lx1" fmla="*/ ss a1 100000" />
            var lx1 = ss * a1 / 100000;
            //  <gd name="lx2" fmla="+- r 0 lx1" />
            var lx2 = r - lx1;
            //  <gd name="ly1" fmla="+- b 0 lx1" />
            var ly1 = b - lx1;
            //  <gd name="rx1" fmla="*/ ss a2 100000" />
            var rx1 = ss * a2 / 100000;
            //  <gd name="rx2" fmla="+- r 0 rx1" />
            var rx2 = r - rx1;
            //  <gd name="ry1" fmla="+- b 0 rx1" />
            var ry1 = b - rx1;
            //  <gd name="d" fmla="+- lx1 0 rx1" />
            var d = lx1 - rx1;
            //  <gd name="dx" fmla="?: d lx1 rx1" />
            var dx = d > 0 ? lx1 : rx1;
            //  <gd name="il" fmla="*/ dx 1 2" />
            var il = dx * 1 / 2;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="ib" fmla="+- b 0 il" />
            var ib = b - il;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="lx1" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="rx2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="rx1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="ly1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="lx2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="rx1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="ry1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="lx1" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="lx1" y="t" />
            //    </moveTo>
            var currentPoint = new EmuPoint(lx1, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="rx2" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, rx2, t);
            //    <lnTo>
            //      <pt x="r" y="rx1" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, rx1);
            //    <lnTo>
            //      <pt x="r" y="ly1" />
            //    </lnTo>
            _ = LineToToString(stringPath, r, ly1);
            //    <lnTo>
            //      <pt x="lx2" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, lx2, b);
            //    <lnTo>
            //      <pt x="rx1" y="b" />
            //    </lnTo>
            _ = LineToToString(stringPath, rx1, b);
            //    <lnTo>
            //      <pt x="l" y="ry1" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, ry1);
            //    <lnTo>
            //      <pt x="l" y="lx1" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, lx1);

            stringPath.Append("z");

            //<rect l="il" t="il" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, ib);

            return stringPath.ToString();
        }

        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var shapePaths = new ShapePath[1];
            shapePaths[0] = new ShapePath(ToGeometryPathString(emuSize, adjusts));

            return shapePaths;
        }
    }
}
