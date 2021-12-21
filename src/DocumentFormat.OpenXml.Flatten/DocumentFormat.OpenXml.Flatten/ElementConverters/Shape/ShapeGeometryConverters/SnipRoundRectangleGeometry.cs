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
    ///     矩形：一个圆顶角，剪去另一个顶角
    /// </summary>
    internal class SnipRoundRectangleGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 16667" />
            //  <gd name="adj2" fmla="val 16667" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 16667d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 16667d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="x1" fmla="*/ ss a1 100000" />
            //  <gd name="dx2" fmla="*/ ss a2 100000" />
            //  <gd name="x2" fmla="+- r 0 dx2" />
            //  <gd name="il" fmla="*/ x1 29289 100000" />

            //  <gd name="ir" fmla="+/ x2 r 2" />
            //</gdLst>

            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            var a1 = Pin(0, adj1, 50000);
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="x1" fmla="*/ ss a1 100000" />
            var x1 = ss * a1 / 100000;
            //  <gd name="dx2" fmla="*/ ss a2 100000" />
            var dx2 = ss * a2 / 100000;
            //  <gd name="x2" fmla="+- r 0 dx2" />
            var x2 = r - dx2;
            //  <gd name="il" fmla="*/ x1 29289 100000" />
            var il = x1 * 29289 / 100000;
            //  <gd name="ir" fmla="+/ x2 r 2" />
            var ir = (x2 + r) / 2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="dx2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="x1" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            //    <close />
            //  </path>

            //    <moveTo>
            //      <pt x="x1" y="t" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x1, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="r" y="dx2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, dx2);
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <lnTo>
            //      <pt x="l" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, x1);
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            var wR = x1;
            var hR = x1;
            var stAng = cd2;
            var swAng = cd4;
            _ = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);

            stringPath.Append("z");

            //<rect l="il" t="il" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, b);

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
