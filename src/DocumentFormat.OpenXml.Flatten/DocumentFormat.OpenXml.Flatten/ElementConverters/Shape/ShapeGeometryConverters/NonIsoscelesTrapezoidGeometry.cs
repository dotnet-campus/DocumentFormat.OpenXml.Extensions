using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 不等腰梯形
    /// </summary>
    public class NonIsoscelesTrapezoidGeometry : ShapeGeometryBase
    {

        /// <inheritdoc />
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 25000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 25000d;

            //当adj1和adj2为最低为0，导致一些值为0，参与公式乘除运算，导致路径有误
            adj1 = System.Math.Max(adj1, 1);
            adj2 = System.Math.Max(adj2, 1);

            var wd3 = w / 3;
            var hd3 = h / 3;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 50000 w ss" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj" />
            //  <gd name="x1" fmla="*/ ss a1 200000" />
            //  <gd name="x2" fmla="*/ ss a1 100000" />
            //  <gd name="dx3" fmla="*/ ss a2 100000" />
            //  <gd name="x3" fmla="+- r 0 dx3" />
            //  <gd name="x4" fmla="+/ r x3 2" />
            //  <gd name="il" fmla="*/ wd3 a1 maxAdj" />
            //  <gd name="adjm" fmla="max a1 a2" />
            //  <gd name="it" fmla="*/ hd3 adjm maxAdj" />
            //  <gd name="irt" fmla="*/ wd3 a2 maxAdj" />
            //  <gd name="ir" fmla="+- r 0 irt" />
            //</gdLst>

            //<gd name="maxAdj" fmla="*/ 50000 w ss" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj = 50000 * w / ss;
            //<gd name="a1" fmla="pin 0 adj1 maxAdj" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, maxAdj);
            //<gd name="a2" fmla="pin 0 adj2 maxAdj" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(0, adj2, maxAdj);
            //<gd name="x1" fmla="*/ ss a1 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = ss * a1 / 200000;
            //<gd name="x2" fmla="*/ ss a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = ss * a1 / 100000;
            //<gd name="dx3" fmla="*/ ss a2 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = ss * a2 / 100000;
            //<gd name="x3" fmla="+- r 0 dx3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = r + 0 - dx3;
            //<gd name="x4" fmla="+/ r x3 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = (r + x3) / 2;
            //<gd name="il" fmla="*/ wd3 a1 maxAdj" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = wd3 * a1 / maxAdj;
            //<gd name="adjm" fmla="max a1 a2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var adjm = System.Math.Max(a1, a2);
            //<gd name="it" fmla="*/ hd3 adjm maxAdj" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var it = hd3 * adjm / maxAdj;
            //<gd name="irt" fmla="*/ wd3 a2 maxAdj" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var irt = wd3 * a2 / maxAdj;
            //<gd name="ir" fmla="+- r 0 irt" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = r + 0 - irt;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="it" r="ir" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, b);

            return shapePaths;
        }
    }


}

