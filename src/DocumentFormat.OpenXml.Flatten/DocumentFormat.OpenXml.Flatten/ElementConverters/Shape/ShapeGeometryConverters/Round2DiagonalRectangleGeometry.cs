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
    ///     矩形：对角圆角
    /// </summary>
    internal class Round2DiagonalRectangleGeometry : ShapeGeometryBase
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
            var adj1 = customAdj1 ?? 16667d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 0d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="x1" fmla="*/ ss a1 100000" />
            //  <gd name="y1" fmla="+- b 0 x1" />
            //  <gd name="a" fmla="*/ ss a2 100000" />
            //  <gd name="x2" fmla="+- r 0 a" />
            //  <gd name="y2" fmla="+- b 0 a" />
            //  <gd name="dx1" fmla="*/ x1 29289 100000" />
            //  <gd name="dx2" fmla="*/ a 29289 100000" />
            //  <gd name="d" fmla="+- dx1 0 dx2" />
            //  <gd name="dx" fmla="?: d dx1 dx2" />
            //  <gd name="ir" fmla="+- r 0 dx" />
            //  <gd name="ib" fmla="+- b 0 dx" />
            //</gdLst>

            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            var a1 = Pin(0, adj1, 50000);
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="x1" fmla="*/ ss a1 100000" />
            var x1 = ss * a1 / 100000;
            //  <gd name="y1" fmla="+- b 0 x1" />
            var y1 = b - x1;
            //  <gd name="a" fmla="*/ ss a2 100000" />
            var a = ss * a2 / 100000;
            //  <gd name="x2" fmla="+- r 0 a" />
            var x2 = r - a;
            //  <gd name="y2" fmla="+- b 0 a" />
            var y2 = b - a;
            //  <gd name="dx1" fmla="*/ x1 29289 100000" />
            var dx1 = x1 * 29289 / 100000;
            //  <gd name="dx2" fmla="*/ a 29289 100000" />
            var dx2 = a * 29289 / 100000;
            //  <gd name="d" fmla="+- dx1 0 dx2" />
            var d = dx1 - dx2;
            //  <gd name="dx" fmla="?: d dx1 dx2" />
            var dx = d > 0 ? dx1 : dx2;
            //  <gd name="ir" fmla="+- r 0 dx" />
            var ir = r - dx;
            //  <gd name="ib" fmla="+- b 0 dx" />
            var ib = b - dx;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <arcTo wR="a" hR="a" stAng="3cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="a" y="b" />
            //    </lnTo>
            //    <arcTo wR="a" hR="a" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="l" y="x1" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            //    <close />
            //  </path>
            //</pathLst>


            var stringPath = new StringBuilder();
            //    <moveTo>
            //      <pt x="x1" y="t" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x1, t);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <arcTo wR="a" hR="a" stAng="3cd4" swAng="cd4" />
            var wR = a;
            var hR = a;
            var stAng = 3 * cd4;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y1);
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = 0;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="a" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, a, b);
            //    <arcTo wR="a" hR="a" stAng="cd4" swAng="cd4" />
            wR = a;
            hR = a;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="l" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, x1);
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = cd2;
            swAng = cd4;
            _ = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);

            stringPath.Append("z");

            //<rect l="dx" t="dx" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(dx, dx, ir, ib);

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
