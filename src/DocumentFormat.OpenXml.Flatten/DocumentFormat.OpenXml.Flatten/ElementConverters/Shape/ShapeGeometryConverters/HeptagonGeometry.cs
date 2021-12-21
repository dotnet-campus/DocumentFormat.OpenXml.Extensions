using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    ///     七边形
    /// </summary>
    internal class HeptagonGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gd name="hf" fmla="val 102572" />
            //<gd name="vf" fmla="val 105210" />

            var customHf = adjusts?.GetAdjustValue("hf");
            var customVf = adjusts?.GetAdjustValue("vf");
            var hf = customHf ?? 102572d;
            var vf = customVf ?? 105210d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="swd2" fmla="*/ wd2 hf 100000" />
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            //  <gd name="svc" fmla="*/ vc  vf 100000" />
            //  <gd name="dx1" fmla="*/ swd2 97493 100000" />
            //  <gd name="dx2" fmla="*/ swd2 78183 100000" />
            //  <gd name="dx3" fmla="*/ swd2 43388 100000" />
            //  <gd name="dy1" fmla="*/ shd2 62349 100000" />
            //  <gd name="dy2" fmla="*/ shd2 22252 100000" />
            //  <gd name="dy3" fmla="*/ shd2 90097 100000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc 0 dx3" />
            //  <gd name="x4" fmla="+- hc dx3 0" />
            //  <gd name="x5" fmla="+- hc dx2 0" />
            //  <gd name="x6" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- svc 0 dy1" />
            //  <gd name="y2" fmla="+- svc dy2 0" />
            //  <gd name="y3" fmla="+- svc dy3 0" />
            //  <gd name="ib" fmla="+- b 0 y1" />
            //</gdLst>

            //<gd name="swd2" fmla="*/ wd2 hf 100000" />
            var swd2 = wd2 * hf / 100000;
            //<gd name="shd2" fmla="*/ hd2 vf 100000" />
            var shd2 = hd2 * vf / 100000;
            //<gd name="svc" fmla="*/ vc  vf 100000" />
            var svc = vc * vf / 100000;
            //<gd name="dx1" fmla="*/ swd2 97493 100000" />
            var dx1 = swd2 * 97493 / 100000;
            //<gd name="dx2" fmla="*/ swd2 78183 100000" />
            var dx2 = swd2 * 78183 / 100000;
            //<gd name="dx3" fmla="*/ swd2 43388 100000" />
            var dx3 = swd2 * 43388 / 100000;
            //<gd name="dy1" fmla="*/ shd2 62349 100000" />
            var dy1 = shd2 * 62349 / 100000;
            //<gd name="dy2" fmla="*/ shd2 22252 100000" />
            var dy2 = shd2 * 22252 / 100000;
            //<gd name="dy3" fmla="*/ shd2 90097 100000" />
            var dy3 = shd2 * 90097 / 100000;
            //<gd name="x1" fmla="+- hc 0 dx1" />
            var x1 = hc - dx1;
            //<gd name="x2" fmla="+- hc 0 dx2" />
            var x2 = hc - dx2;
            //<gd name="x3" fmla="+- hc 0 dx3" />
            var x3 = hc - dx3;
            //<gd name="x4" fmla="+- hc dx3 0" />
            var x4 = hc + dx3;
            //<gd name="x5" fmla="+- hc dx2 0" />
            var x5 = hc + dx2;
            //<gd name="x6" fmla="+- hc dx1 0" />
            var x6 = hc + dx1;
            //<gd name="y1" fmla="+- svc 0 dy1" />
            var y1 = svc - dy1;
            //<gd name="y2" fmla="+- svc dy2 0" />
            var y2 = svc + dy2;
            //<gd name="y3" fmla="+- svc dy3 0" />
            var y3 = svc + dy3;
            //<gd name="ib" fmla="+- b 0 y1" />
            var ib = b - y1;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="x1" y="y2" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x1, y2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            _ = LineToToString(stringPath, hc, t);
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, x5, y1);
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            _ = LineToToString(stringPath, x6, y2);
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            _ = LineToToString(stringPath, x4, y3);
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            _ = LineToToString(stringPath, x3, y3);

            stringPath.Append("z");

            //<rect l="x2" t="y1" r="x5" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x2, y1, x5, ib);

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
