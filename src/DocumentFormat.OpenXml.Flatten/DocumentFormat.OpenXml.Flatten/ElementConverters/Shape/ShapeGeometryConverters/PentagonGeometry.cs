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
    ///     五边形
    /// </summary>
    internal class PentagonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="hf" fmla="val 105146" />
            //  <gd name="vf" fmla="val 110557" />
            //</avLst>
            var customHf = adjusts?.GetAdjustValue("hf");
            var customVf = adjusts?.GetAdjustValue("vf");
            var hf = customHf ?? 105146d;
            var vf = customVf ?? 110557d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="swd2" fmla="*/ wd2 hf 100000" />
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            //  <gd name="svc" fmla="*/ vc  vf 100000" />
            //  <gd name="dx1" fmla="cos swd2 1080000" />
            //  <gd name="dx2" fmla="cos swd2 18360000" />
            //  <gd name="dy1" fmla="sin shd2 1080000" />
            //  <gd name="dy2" fmla="sin shd2 18360000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- svc 0 dy1" />
            //  <gd name="y2" fmla="+- svc 0 dy2" />
            //  <gd name="it" fmla="*/ y1 dx2 dx1" />
            //</gdLst>

            //  <gd name="swd2" fmla="*/ wd2 hf 100000" />
            var swd2 = wd2 * hf / 100000;
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            var shd2 = hd2 * vf / 100000;
            //  <gd name="svc" fmla="*/ vc  vf 100000" />
            var svc = vc * vf / 100000;
            //  <gd name="dx1" fmla="cos swd2 1080000" />
            var dx1 = Cos(swd2, 1080000);
            //  <gd name="dx2" fmla="cos swd2 18360000" />
            var dx2 = Cos(swd2, 18360000);
            //  <gd name="dy1" fmla="sin shd2 1080000" />
            var dy1 = Sin(shd2, 1080000);
            //  <gd name="dy2" fmla="sin shd2 18360000" />
            var dy2 = Sin(shd2, 18360000);
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            var x1 = hc - dx1;
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            var x2 = hc - dx2;
            //  <gd name="x3" fmla="+- hc dx2 0" />
            var x3 = hc + dx2;
            //  <gd name="x4" fmla="+- hc dx1 0" />
            var x4 = hc + dx1;
            //  <gd name="y1" fmla="+- svc 0 dy1" />
            var y1 = svc - dy1;
            //  <gd name="y2" fmla="+- svc 0 dy2" />
            var y2 = svc - dy2;
            //  <gd name="it" fmla="*/ y1 dx2 dx1" />
            var it = y1 * dx2 / dx1;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, hc, t);
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, y2);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, y2);

            stringPath.Append("z");

            //<rect l="x2" t="it" r="x3" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x2, it, x3, y2);

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
