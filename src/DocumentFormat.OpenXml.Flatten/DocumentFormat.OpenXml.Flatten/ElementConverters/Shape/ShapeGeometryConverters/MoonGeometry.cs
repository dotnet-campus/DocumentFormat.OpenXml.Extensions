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
    /// 月亮形
    /// </summary>
    public class MoonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string? ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 50000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 87500" />
            //  <gd name="g0" fmla="*/ ss a 100000" />
            //  <gd name="g0w" fmla="*/ g0 w ss" />
            //  <gd name="g1" fmla="+- ss 0 g0" />
            //  <gd name="g2" fmla="*/ g0 g0 g1" />
            //  <gd name="g3" fmla="*/ ss ss g1" />
            //  <gd name="g4" fmla="*/ g3 2 1" />
            //  <gd name="g5" fmla="+- g4 0 g2" />
            //  <gd name="g6" fmla="+- g5 0 g0" />
            //  <gd name="g6w" fmla="*/ g6 w ss" />
            //  <gd name="g7" fmla="*/ g5 1 2" />
            //  <gd name="g8" fmla="+- g7 0 g0" />
            //  <gd name="dy1" fmla="*/ g8 hd2 ss" />
            //  <gd name="g10h" fmla="+- vc 0 dy1" />
            //  <gd name="g11h" fmla="+- vc dy1 0" />
            //  <gd name="g12" fmla="*/ g0 9598 32768" />
            //  <gd name="g12w" fmla="*/ g12 w ss" />
            //  <gd name="g13" fmla="+- ss 0 g12" />
            //  <gd name="q1" fmla="*/ ss ss 1" />
            //  <gd name="q2" fmla="*/ g13 g13 1" />
            //  <gd name="q3" fmla="+- q1 0 q2" />
            //  <gd name="q4" fmla="sqrt q3" />
            //  <gd name="dy4" fmla="*/ q4 hd2 ss" />
            //  <gd name="g15h" fmla="+- vc 0 dy4" />
            //  <gd name="g16h" fmla="+- vc dy4 0" />
            //  <gd name="g17w" fmla="+- g6w 0 g0w" />
            //  <gd name="g18w" fmla="*/ g17w 1 2" />
            //  <gd name="dx2p" fmla="+- g0w g18w w" />
            //  <gd name="dx2" fmla="*/ dx2p -1 1" />
            //  <gd name="dy2" fmla="*/ hd2 -1 1" />
            //  <gd name="stAng1" fmla="at2 dx2 dy2" />
            //  <gd name="enAngp1" fmla="at2 dx2 hd2" />
            //  <gd name="enAng1" fmla="+- enAngp1 0 21600000" />
            //  <gd name="swAng1" fmla="+- enAng1 0 stAng1" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 87500" />
            var a = Pin(0, adj, 87500);
            //  <gd name="g0" fmla="*/ ss a 100000" />
            var g0 = ss * a / 100000;
            //  <gd name="g0w" fmla="*/ g0 w ss" />
            var g0w = g0 * w / ss;
            //  <gd name="g1" fmla="+- ss 0 g0" />
            var g1 = ss - g0;
            //  <gd name="g2" fmla="*/ g0 g0 g1" />
            var g2 = g0 * g0 / g1;
            //  <gd name="g3" fmla="*/ ss ss g1" />
            var g3 = ss * ss / g1;
            //  <gd name="g4" fmla="*/ g3 2 1" />
            var g4 = g3 * 2 / 1;
            //  <gd name="g5" fmla="+- g4 0 g2" />
            var g5 = g4 - g2;
            //  <gd name="g6" fmla="+- g5 0 g0" />
            var g6 = g5 - g0;
            //  <gd name="g6w" fmla="*/ g6 w ss" />
            var g6w = g6 * w / ss;
            //  <gd name="g7" fmla="*/ g5 1 2" />
            var g7 = g5 * 1 / 2;
            //  <gd name="g8" fmla="+- g7 0 g0" />
            var g8 = g7 - g0;
            //  <gd name="dy1" fmla="*/ g8 hd2 ss" />
            var dy1 = g8 * hd2 / ss;
            //  <gd name="g10h" fmla="+- vc 0 dy1" />
            var g10h = vc - dy1;
            //  <gd name="g11h" fmla="+- vc dy1 0" />
            var g11h = vc + dy1;
            //  <gd name="g12" fmla="*/ g0 9598 32768" />
            var g12 = g0 * 9598 / 32768;
            //  <gd name="g12w" fmla="*/ g12 w ss" />
            var g12w = g12 * w / ss;
            //  <gd name="g13" fmla="+- ss 0 g12" />
            var g13 = ss - g12;
            //  <gd name="q1" fmla="*/ ss ss 1" />
            var q1 = ss * ss / 1;
            //  <gd name="q2" fmla="*/ g13 g13 1" />
            var q2 = g13 * g13 / 1;
            //  <gd name="q3" fmla="+- q1 0 q2" />
            var q3 = q1 - q2;
            //  <gd name="q4" fmla="sqrt q3" />
            var q4 = System.Math.Sqrt(q3);
            //  <gd name="dy4" fmla="*/ q4 hd2 ss" />
            var dy4 = q4 * hd2 / ss;
            //  <gd name="g15h" fmla="+- vc 0 dy4" />
            var g15h = vc - dy4;
            //  <gd name="g16h" fmla="+- vc dy4 0" />
            var g16h = vc + dy4;
            //  <gd name="g17w" fmla="+- g6w 0 g0w" />
            var g17w = g6w - g0w;
            //  <gd name="g18w" fmla="*/ g17w 1 2" />
            var g18w = g17w * 1 / 2;
            //  <gd name="dx2p" fmla="+- g0w g18w w" />
            var dx2p = g0w + g18w - w;
            //  <gd name="dx2" fmla="*/ dx2p -1 1" />
            var dx2 = dx2p * -1 / 1;
            //  <gd name="dy2" fmla="*/ hd2 -1 1" />
            var dy2 = hd2 * -1 / 1;
            //  <gd name="stAng1" fmla="at2 dx2 dy2" />
            var stAng1 = ATan2(dx2, dy2);
            //  <gd name="enAngp1" fmla="at2 dx2 hd2" />
            var enAngp1 = ATan2(dx2, hd2);
            //  <gd name="enAng1" fmla="+- enAngp1 0 21600000" />
            var enAng1 = enAngp1 - 21600000;
            //  <gd name="swAng1" fmla="+- enAng1 0 stAng1" />
            var swAng1 = enAng1 - stAng1;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            //    <arcTo wR="w" hR="hd2" stAng="cd4" swAng="cd2" />
            //    <arcTo wR="g18w" hR="dy1" stAng="stAng1" swAng="swAng1" />
            //    <close />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(r, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="w" hR="hd2" stAng="cd4" swAng="cd2" />
            var wR = w;
            var hR = hd2;
            var stAng = cd4;
            var swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="g18w" hR="dy1" stAng="stAng1" swAng="swAng1" />
            wR = g18w;
            hR = dy1;
            stAng = stAng1;
            swAng = swAng1;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");

            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="g12w" t="g15h" r="g0w" b="g16h" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(g12w, g15h, g0w, g16h);

            return shapePaths;
        }
    }
}
