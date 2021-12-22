using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 心形
    /// </summary>
    public class HeartGeometry : ShapeGeometryBase
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
            //  <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="dx1" fmla="*/ w 49 48" />
            //  <gd name="dx2" fmla="*/ w 10 48" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- t 0 hd3" />
            //  <gd name="il" fmla="*/ w 1 6" />
            //  <gd name="ir" fmla="*/ w 5 6" />
            //  <gd name="ib" fmla="*/ h 2 3" />
            //</gdLst>

            //  <gd name="dx1" fmla="*/ w 49 48" />
            var dx1 = w * 49 / 48;
            //  <gd name="dx2" fmla="*/ w 10 48" />
            var dx2 = w * 10 / 48;
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            var x1 = hc - dx1;
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            var x2 = hc - dx2;
            //  <gd name="x3" fmla="+- hc dx2 0" />
            var x3 = hc + dx2;
            //  <gd name="x4" fmla="+- hc dx1 0" />
            var x4 = hc + dx1;
            //  <gd name="y1" fmla="+- t 0 hd3" />
            var y1 = t - (h / 3);
            //  <gd name="il" fmla="*/ w 1 6" />
            var il = w * 1 / 6;
            //  <gd name="ir" fmla="*/ w 5 6" />
            var ir = w * 5 / 6;
            //  <gd name="ib" fmla="*/ h 2 3" />
            var ib = h * 2 / 3;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="hc" y="hd4" />
            //    </moveTo>
            //    <cubicBezTo>
            //      <pt x="x3" y="y1" />
            //      <pt x="x4" y="hd4" />
            //      <pt x="hc" y="b" />
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x1" y="hd4" />
            //      <pt x="x2" y="y1" />
            //      <pt x="hc" y="hd4" />
            //    </cubicBezTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="hc" y="hd4" />
            //    </moveTo>
            var currentPoint = new EmuPoint(hc, hd4);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <cubicBezTo>
            //      <pt x="x3" y="y1" />
            //      <pt x="x4" y="hd4" />
            //      <pt x="hc" y="b" />
            CubicBezToString(stringPath, x3, y1, x4, hd4, hc, b);
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x1" y="hd4" />
            //      <pt x="x2" y="y1" />
            //      <pt x="hc" y="hd4" />
            //    </cubicBezTo>
            CubicBezToString(stringPath, x1, hd4, x2, y1, hc, hd4);
            //    <close />
            stringPath.Append("z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="hd4" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, hd4, ir, ib);

            return shapePaths;
        }
    }
}
