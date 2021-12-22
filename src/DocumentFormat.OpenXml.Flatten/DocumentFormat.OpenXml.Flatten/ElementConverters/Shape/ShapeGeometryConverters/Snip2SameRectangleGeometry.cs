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
    ///     矩形：剪去左右顶角
    /// </summary>
    internal class Snip2SameRectangleGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 16667" />
            //  <gd name="adj2" fmla="val 0" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj1 = customAdj1 ?? 16667d;
            var adj2 = customAdj2 ?? 0d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="tx1" fmla="*/ ss a1 100000" />
            //  <gd name="tx2" fmla="+- r 0 tx1" />
            //  <gd name="bx1" fmla="*/ ss a2 100000" />
            //  <gd name="bx2" fmla="+- r 0 bx1" />
            //  <gd name="by1" fmla="+- b 0 bx1" />
            //  <gd name="d" fmla="+- tx1 0 bx1" />
            //  <gd name="dx" fmla="?: d tx1 bx1" />
            //  <gd name="il" fmla="*/ dx 1 2" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="it" fmla="*/ tx1 1 2" />
            //  <gd name="ib" fmla="+/ by1 b 2" />
            //</gdLst>


            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            var a1 = Pin(0, adj1, 50000);
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="tx1" fmla="*/ ss a1 100000" />
            var tx1 = ss * a1 / 100000;
            //  <gd name="tx2" fmla="+- r 0 tx1" />
            var tx2 = r - tx1;
            //  <gd name="bx1" fmla="*/ ss a2 100000" />
            var bx1 = ss * a2 / 100000;
            //  <gd name="bx2" fmla="+- r 0 bx1" />
            var bx2 = r - bx1;
            //  <gd name="by1" fmla="+- b 0 bx1" />
            var by1 = b - bx1;
            //  <gd name="d" fmla="+- tx1 0 bx1" />
            var d = tx1 - bx1;
            //  <gd name="dx" fmla="?: d tx1 bx1" />
            var dx = d > 0 ? tx1 : bx1;
            //  <gd name="il" fmla="*/ dx 1 2" />
            var il = dx * 1 / 2;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="it" fmla="*/ tx1 1 2" />
            var it = tx1 * 1 / 2;
            //  <gd name="ib" fmla="+/ by1 b 2" />
            var ib = (by1 + b) / 2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //   <path>
            //     <moveTo>
            //       <pt x="tx1" y="t" />
            //     </moveTo>
            //     <lnTo>
            //       <pt x="tx2" y="t" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="r" y="tx1" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="r" y="by1" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="bx2" y="b" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="bx1" y="b" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="l" y="by1" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="l" y="tx1" />
            //     </lnTo>
            //     <close />
            //   </path>
            // </pathLst>

            //     <moveTo>
            //       <pt x="tx1" y="t" />
            //     </moveTo>
            var currentPoint = new EmuPoint(tx1, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //     <lnTo>
            //       <pt x="tx2" y="t" />
            //     </lnTo>
            _ = LineToToString(stringPath, tx2, t);
            //     <lnTo>
            //       <pt x="r" y="tx1" />
            //     </lnTo>
            _ = LineToToString(stringPath, r, tx1);
            //     <lnTo>
            //       <pt x="r" y="by1" />
            //     </lnTo>
            _ = LineToToString(stringPath, r, by1);
            //     <lnTo>
            //       <pt x="bx2" y="b" />
            //     </lnTo>
            _ = LineToToString(stringPath, bx2, b);
            //     <lnTo>
            //       <pt x="bx1" y="b" />
            //     </lnTo>
            _ = LineToToString(stringPath, bx1, b);
            //     <lnTo>
            //       <pt x="l" y="by1" />
            //     </lnTo>
            _ = LineToToString(stringPath, l, by1);
            //     <lnTo>
            //       <pt x="l" y="tx1" />
            //     </lnTo>
            _ = LineToToString(stringPath, l, tx1);

            stringPath.Append("z");

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

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
