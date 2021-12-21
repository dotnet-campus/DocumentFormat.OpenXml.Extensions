using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;


namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头: 直角双向
    /// </summary>
    public class LeftUpArrowGeometry : ShapeGeometryBase
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
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 25000" />
            //  <gd name="adj2" fmla="val 25000" />
            //  <gd name="adj3" fmla="val 25000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 25000d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="maxAdj3" fmla="+- 100000 0 maxAdj1" />
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            //  <gd name="x1" fmla="*/ ss a3 100000" />
            //  <gd name="dx2" fmla="*/ ss a2 50000" />
            //  <gd name="x2" fmla="+- r 0 dx2" />
            //  <gd name="y2" fmla="+- b 0 dx2" />
            //  <gd name="dx4" fmla="*/ ss a2 100000" />
            //  <gd name="x4" fmla="+- r 0 dx4" />
            //  <gd name="y4" fmla="+- b 0 dx4" />
            //  <gd name="dx3" fmla="*/ ss a1 200000" />
            //  <gd name="x3" fmla="+- x4 0 dx3" />
            //  <gd name="x5" fmla="+- x4 dx3 0" />
            //  <gd name="y3" fmla="+- y4 0 dx3" />
            //  <gd name="y5" fmla="+- y4 dx3 0" />
            //  <gd name="il" fmla="*/ dx3 x1 dx4" />
            //  <gd name="cx1" fmla="+/ x1 x5 2" />
            //  <gd name="cy1" fmla="+/ x1 y5 2" />
            //</gdLst>


            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="maxAdj1" fmla="*/ a2 2 1" />
            var maxAdj1 = a2 * 2 / 1;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="maxAdj3" fmla="+- 100000 0 maxAdj1" />
            var maxAdj3 = 100000 - maxAdj1;
            //  <gd name="a3" fmla="pin 0 adj3 maxAdj3" />
            var a3 = Pin(0, adj3, maxAdj3);
            //  <gd name="x1" fmla="*/ ss a3 100000" />
            var x1 = ss * a3 / 100000;
            //  <gd name="dx2" fmla="*/ ss a2 50000" />
            var dx2 = ss * a2 / 50000;
            //  <gd name="x2" fmla="+- r 0 dx2" />
            var x2 = r - dx2;
            //  <gd name="y2" fmla="+- b 0 dx2" />
            var y2 = b - dx2;
            //  <gd name="dx4" fmla="*/ ss a2 100000" />
            var dx4 = ss * a2 / 100000;
            //  <gd name="x4" fmla="+- r 0 dx4" />
            var x4 = r - dx4;
            //  <gd name="y4" fmla="+- b 0 dx4" />
            var y4 = b - dx4;
            //  <gd name="dx3" fmla="*/ ss a1 200000" />
            var dx3 = ss * a1 / 200000;
            //  <gd name="x3" fmla="+- x4 0 dx3" />
            var x3 = x4 - dx3;
            //  <gd name="x5" fmla="+- x4 dx3 0" />
            var x5 = x4 + dx3;
            //  <gd name="y3" fmla="+- y4 0 dx3" />
            var y3 = y4 - dx3;
            //  <gd name="y5" fmla="+- y4 dx3 0" />
            var y5 = y4 + dx3;
            //  <gd name="il" fmla="*/ dx3 x1 dx4" />
            var il = dx3 * x1 / dx4;
            //  <gd name="cx1" fmla="+/ x1 x5 2" />
            var cx1 = (x1 + x5) / 2;
            //  <gd name="cy1" fmla="+/ x1 y5 2" />
            var cy1 = (x1 + y5) / 2;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y4" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="x1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y4" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y4);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, y3);
            //    <lnTo>
            //      <pt x="x3" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, x1);
            //    <lnTo>
            //      <pt x="x2" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, x1);
            //    <lnTo>
            //      <pt x="x4" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, t);
            //    <lnTo>
            //      <pt x="r" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, x1);
            //    <lnTo>
            //      <pt x="x5" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, x1);
            //    <lnTo>
            //      <pt x="x5" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y5);
            //    <lnTo>
            //      <pt x="x1" y="y5" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y5);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="y3" r="x4" b="y5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, y3, x4, y5);

            return shapePaths;
        }
    }
}
