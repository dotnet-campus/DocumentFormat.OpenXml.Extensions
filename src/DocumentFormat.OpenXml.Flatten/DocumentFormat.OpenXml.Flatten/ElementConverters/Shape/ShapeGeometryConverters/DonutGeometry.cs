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
    /// 同心圆
    /// </summary>
    public class DonutGeometry : ShapeGeometryBase
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
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 25000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="dr" fmla="*/ ss a 100000" />
            //  <gd name="iwd2" fmla="+- wd2 0 dr" />
            //  <gd name="ihd2" fmla="+- hd2 0 dr" />
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 50000" />
            var a = Pin(0, adj, 50000);
            //  <gd name="dr" fmla="*/ ss a 100000" />
            var dr = ss * a / 100000;
            //  <gd name="iwd2" fmla="+- wd2 0 dr" />
            var iwd2 = wd2 - dr;
            //  <gd name="ihd2" fmla="+- hd2 0 dr" />
            var ihd2 = hd2 - dr;
            //  <gd name="idx" fmla="cos wd2 2700000" />
            var idx = Cos(wd2, 2700000);
            //  <gd name="idy" fmla="sin hd2 2700000" />
            var idy = Sin(hd2, 2700000);
            //  <gd name="il" fmla="+- hc 0 idx" />
            var il = hc - idx;
            //  <gd name="ir" fmla="+- hc idx 0" />
            var ir = hc + idx;
            //  <gd name="it" fmla="+- vc 0 idy" />
            var it = vc - idy;
            //  <gd name="ib" fmla="+- vc idy 0" />
            var ib = vc + idy;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            //    <close />
            //    <moveTo>
            //      <pt x="dr" y="vc" />
            //    </moveTo>
            //    <arcTo wR="iwd2" hR="ihd2" stAng="cd2" swAng="-5400000" />
            //    <arcTo wR="iwd2" hR="ihd2" stAng="cd4" swAng="-5400000" />
            //    <arcTo wR="iwd2" hR="ihd2" stAng="0" swAng="-5400000" />
            //    <arcTo wR="iwd2" hR="ihd2" stAng="3cd4" swAng="-5400000" />
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
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            var wR = wd2;
            var hR = hd2;
            var stAng = cd2;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = 3 * cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = 0;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            wR = wd2;
            hR = hd2;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            stringPath.Append("z ");


            //    <moveTo>
            //      <pt x="dr" y="vc" />
            //    </moveTo>
            currentPoint = new EmuPoint(dr, vc);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="iwd2" hR="ihd2" stAng="cd2" swAng="-5400000" />
            wR = iwd2;
            hR = ihd2;
            stAng = cd2;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="iwd2" hR="ihd2" stAng="cd4" swAng="-5400000" />
            wR = iwd2;
            hR = ihd2;
            stAng = cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="iwd2" hR="ihd2" stAng="0" swAng="-5400000" />
            wR = iwd2;
            hR = ihd2;
            stAng = 0;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="iwd2" hR="ihd2" stAng="3cd4" swAng="-5400000" />
            wR = iwd2;
            hR = ihd2;
            stAng = 3 * cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");

            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
