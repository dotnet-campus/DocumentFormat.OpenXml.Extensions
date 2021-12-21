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
    ///     矩形：剪去单角
    /// </summary>
    internal class Snip1RectangleGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 16667" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 16667d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="dx1" fmla="*/ ss a 100000" />
            //  <gd name="x1" fmla="+- r 0 dx1" />
            //  <gd name="it" fmla="*/ dx1 1 2" />
            //  <gd name="ir" fmla="+/ x1 r 2" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 50000" />
            var a = Pin(0, adj, 50000);
            //  <gd name="dx1" fmla="*/ ss a 100000" />
            var dx1 = ss * a / 100000;
            //  <gd name="x1" fmla="+- r 0 dx1" />
            var x1 = r - dx1;
            //  <gd name="it" fmla="*/ dx1 1 2" />
            var it = dx1 * 1 / 2;
            //  <gd name="ir" fmla="+/ x1 r 2" />
            var ir = (x1 + r) / 2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //   <path>
            //     <moveTo>
            //       <pt x="l" y="t" />
            //     </moveTo>
            //     <lnTo>
            //       <pt x="x1" y="t" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="r" y="dx1" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="r" y="b" />
            //     </lnTo>
            //     <lnTo>
            //       <pt x="l" y="b" />
            //     </lnTo>
            //     <close />
            //   </path>
            // </pathLst>

            //     <moveTo>
            //       <pt x="l" y="t" />
            //     </moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //     <lnTo>
            //       <pt x="x1" y="t" />
            //     </lnTo>
            _ = LineToToString(stringPath, x1, t);
            //     <lnTo>
            //       <pt x="r" y="dx1" />
            //     </lnTo>
            _ = LineToToString(stringPath, r, dx1);
            //     <lnTo>
            //       <pt x="r" y="b" />
            //     </lnTo>
            _ = LineToToString(stringPath, r, b);
            //     <lnTo>
            //       <pt x="l" y="b" />
            //     </lnTo>
            _ = LineToToString(stringPath, l, b);

            stringPath.Append("z");

            //<rect l="l" t="it" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, it, ir, b);

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
