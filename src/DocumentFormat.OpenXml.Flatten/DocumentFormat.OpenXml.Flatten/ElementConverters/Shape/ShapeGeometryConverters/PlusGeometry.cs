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
    /// 十字形
    /// </summary>
    public class PlusGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 25000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="x1" fmla="*/ ss a 100000" />
            //  <gd name="x2" fmla="+- r 0 x1" />
            //  <gd name="y2" fmla="+- b 0 x1" />
            //  <gd name="d" fmla="+- w 0 h" />
            //  <gd name="il" fmla="?: d l x1" />
            //  <gd name="ir" fmla="?: d r x2" />
            //  <gd name="it" fmla="?: d x1 t" />
            //  <gd name="ib" fmla="?: d y2 b" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 50000" />
            var a = Pin(0, adj, 50000);
            //  <gd name="x1" fmla="*/ ss a 100000" />
            var x1 = ss * a / 100000;
            //  <gd name="x2" fmla="+- r 0 x1" />
            var x2 = r - x1;
            //  <gd name="y2" fmla="+- b 0 x1" />
            var y2 = b - x1;
            //  <gd name="d" fmla="+- w 0 h" />
            var d = w - h;
            //  <gd name="il" fmla="?: d l x1" />
            var il = d > 0 ? l : x1;
            //  <gd name="ir" fmla="?: d r x2" />
            var ir = d > 0 ? r : x2;
            //  <gd name="it" fmla="?: d x1 t" />
            var it = d > 0 ? x1 : t;
            //  <gd name="ib" fmla="?: d y2 b" />
            var ib = d > 0 ? y2 : b;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="x1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="x1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, x1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, x1);
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, t);
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="x2" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, x1);
            //    <lnTo>
            //      <pt x="r" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, x1);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y2);
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
