using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 连接符：曲线4
    /// </summary>
    public class CurvedConnector4Geometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x2" fmla="*/ w adj1 100000" />
            //  <gd name="x1" fmla="+/ l x2 2" />
            //  <gd name="x3" fmla="+/ r x2 2" />
            //  <gd name="x4" fmla="+/ x2 x3 2" />
            //  <gd name="x5" fmla="+/ x3 r 2" />
            //  <gd name="y4" fmla="*/ h adj2 100000" />
            //  <gd name="y1" fmla="+/ t y4 2" />
            //  <gd name="y2" fmla="+/ t y1 2" />
            //  <gd name="y3" fmla="+/ y1 y4 2" />
            //  <gd name="y5" fmla="+/ b y4 2" />
            //</gdLst>

            //  <gd name="x2" fmla="*/ w adj1 100000" />
            var x2 = w * adj1 / 100000;
            //  <gd name="x1" fmla="+/ l x2 2" />
            var x1 = (l + x2) / 2;
            //  <gd name="x3" fmla="+/ r x2 2" />
            var x3 = (r + x2) / 2;
            //  <gd name="x4" fmla="+/ x2 x3 2" />
            var x4 = (x2 + x3) / 2;
            //  <gd name="x5" fmla="+/ x3 r 2" />
            var x5 = (x3 + r) / 2;
            //  <gd name="y4" fmla="*/ h adj2 100000" />
            var y4 = h * adj2 / 100000;
            //  <gd name="y1" fmla="+/ t y4 2" />
            var y1 = (t + y4) / 2;
            //  <gd name="y2" fmla="+/ t y1 2" />
            var y2 = (t + y1) / 2;
            //  <gd name="y3" fmla="+/ y1 y4 2" />
            var y3 = (y1 + y4) / 2;
            //  <gd name="y5" fmla="+/ b y4 2" />
            var y5 = (b + y4) / 2;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <cubicBezTo>
            //      <pt x="x1" y="t" />
            //      <pt x="x2" y="y2" />
            //      <pt x="x2" y="y1" />
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x2" y="y3" />
            //      <pt x="x4" y="y4" />
            //      <pt x="x3" y="y4" />
            //    </cubicBezTo>
            //    <cubicBezTo>
            //      <pt x="x5" y="y4" />
            //      <pt x="r" y="y5" />
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
            //      <pt x="x2" y="y2" />
            //      <pt x="x2" y="y1" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x1, t, x2, y2, x2, y1);
            //    <cubicBezTo>
            //      <pt x="x2" y="y3" />
            //      <pt x="x4" y="y4" />
            //      <pt x="x3" y="y4" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x2, y3, x4, y4, x3, y4);
            //    <cubicBezTo>
            //      <pt x="x5" y="y4" />
            //      <pt x="r" y="y5" />
            //      <pt x="r" y="b" />
            //    </cubicBezTo>
            currentPoint = CubicBezToString(stringPath, x5, y4, r, y5, r, b);
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            // <rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }

    }
}
