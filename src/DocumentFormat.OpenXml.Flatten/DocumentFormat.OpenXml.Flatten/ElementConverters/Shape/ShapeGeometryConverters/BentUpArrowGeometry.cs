using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{

    /// <summary>
    /// 箭头: 直角上
    /// </summary>
    public class BentUpArrowGeometry : ShapeGeometryBase
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
            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            //  <gd name="a3" fmla="pin 0 adj3 50000" />
            //  <gd name="y1" fmla="*/ ss a3 100000" />
            //  <gd name="dx1" fmla="*/ ss a2 50000" />
            //  <gd name="x1" fmla="+- r 0 dx1" />
            //  <gd name="dx3" fmla="*/ ss a2 100000" />
            //  <gd name="x3" fmla="+- r 0 dx3" />
            //  <gd name="dx2" fmla="*/ ss a1 200000" />
            //  <gd name="x2" fmla="+- x3 0 dx2" />
            //  <gd name="x4" fmla="+- x3 dx2 0" />
            //  <gd name="dy2" fmla="*/ ss a1 100000" />
            //  <gd name="y2" fmla="+- b 0 dy2" />
            //  <gd name="x0" fmla="*/ x4 1 2" />
            //  <gd name="y3" fmla="+/ y2 b 2" />
            //  <gd name="y15" fmla="+/ y1 b 2" />
            //</gdLst>


            //  <gd name="a1" fmla="pin 0 adj1 50000" />
            var a1 = Pin(0, adj1, 50000);
            //  <gd name="a2" fmla="pin 0 adj2 50000" />
            var a2 = Pin(0, adj2, 50000);
            //  <gd name="a3" fmla="pin 0 adj3 50000" />
            var a3 = Pin(0, adj3, 50000);
            //  <gd name="y1" fmla="*/ ss a3 100000" />
            var y1 = ss * a3 / 100000;
            //  <gd name="dx1" fmla="*/ ss a2 50000" />
            var dx1 = ss * a2 / 50000;
            //  <gd name="x1" fmla="+- r 0 dx1" />
            var x1 = r - dx1;
            //  <gd name="dx3" fmla="*/ ss a2 100000" />
            var dx3 = ss * a2 / 100000;
            //  <gd name="x3" fmla="+- r 0 dx3" />
            var x3 = r - dx3;
            //  <gd name="dx2" fmla="*/ ss a1 200000" />
            var dx2 = ss * a1 / 200000;
            //  <gd name="x2" fmla="+- x3 0 dx2" />
            var x2 = x3 - dx2;
            //  <gd name="x4" fmla="+- x3 dx2 0" />
            var x4 = x3 + dx2;
            //  <gd name="dy2" fmla="*/ ss a1 100000" />
            var dy2 = ss * a1 / 100000d;
            //  <gd name="y2" fmla="+- b 0 dy2" />
            var y2 = b - dy2;
            //  <gd name="x0" fmla="*/ x4 1 2" />
            var x0 = x4 * 1 / 2;
            //  <gd name="y3" fmla="+/ y2 b 2" />
            var y3 = (y2 + b) / 2;
            //  <gd name="y15" fmla="+/ y1 b 2" />
            var y15 = (y1 + b) / 2;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y2" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y1);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y1);
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //    <lnTo>
            //      <pt x="x4" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, b);
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="l" t="y2" r="x4" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, y2, x4, b);

            return shapePaths;

        }
    }
}
