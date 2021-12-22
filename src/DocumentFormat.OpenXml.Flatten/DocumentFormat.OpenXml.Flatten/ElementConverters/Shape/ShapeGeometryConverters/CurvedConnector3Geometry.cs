using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 连接符：曲线3
    /// </summary>
    public class CurvedConnector3Geometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x2" fmla="*/ w adj1 100000" />
            //  <gd name="x1" fmla="+/ l x2 2" />
            //  <gd name="x3" fmla="+/ r x2 2" />
            //  <gd name="y3" fmla="*/ h 3 4" />
            //</gdLst>

            //  <gd name="x2" fmla="*/ w adj1 100000" />
            var x2 = w * adj1 / 100000;
            //  <gd name="x1" fmla="+/ l x2 2" />
            var x1 = (l + x2) / 2;
            //  <gd name="x3" fmla="+/ r x2 2" />
            var x3 = (r + x2) / 2;
            //  <gd name="y3" fmla="*/ h 3 4" />
            var y3 = h * 3 / 4;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <cubicBezTo>
            //      <pt x="x1" y="t" />
            //      <pt x="x2" y="hd4" />
            //      <pt x="x2" y="vc" />
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x2" y="y3" />
            //      <pt x="x3" y="b" />
            //      <pt x="r" y="b" />
            //    </cubicBezTo>
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <cubicBezTo>
            //      <pt x="x1" y="t" />
            //      <pt x="x2" y="hd4" />
            //      <pt x="x2" y="vc" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x1, t, x2, hd4, x2, vc);
            //    <cubicBezTo>
            //      <pt x="x2" y="y3" />
            //      <pt x="x3" y="b" />
            //      <pt x="r" y="b" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x2, y3, x3, b, r, b);
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }
}
