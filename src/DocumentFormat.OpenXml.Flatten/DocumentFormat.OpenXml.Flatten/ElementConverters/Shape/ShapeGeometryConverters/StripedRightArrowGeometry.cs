using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头: 虚尾
    /// </summary>
    public class StripedRightArrowGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 50000" />
            //  <gd name="adj2" fmla="val 50000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 50000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 50000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj2" fmla="*/ 84375 w ss" />
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="x4" fmla="*/ ss 5 32" />
            //  <gd name="dx5" fmla="*/ ss a2 100000" />
            //  <gd name="x5" fmla="+- r 0 dx5" />
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc dy1 0" />
            //  <gd name="dx6" fmla="*/ dy1 dx5 hd2" />
            //  <gd name="x6" fmla="+- r 0 dx6" />
            //</gdLst>

            //  <gd name="maxAdj2" fmla="*/ 84375 w ss" />
            var maxAdj2 = 84375 * w / ss;
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            var a1 = Pin(0, adj1, 100000);
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            var a2 = Pin(0, adj2, maxAdj2);
            //  <gd name="x4" fmla="*/ ss 5 32" />
            var x4 = ss * 5 / 32;
            //  <gd name="dx5" fmla="*/ ss a2 100000" />
            var dx5 = ss * a2 / 100000;
            //  <gd name="x5" fmla="+- r 0 dx5" />
            var x5 = r - dx5;
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            var dy1 = h * a1 / 200000;
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            var y1 = vc - dy1;
            //  <gd name="y2" fmla="+- vc dy1 0" />
            var y2 = vc + dy1;
            //  <gd name="dx6" fmla="*/ dy1 dx5 hd2" />
            var dx6 = dy1 * dx5 / hd2;
            //  <gd name="x6" fmla="+- r 0 dx6" />
            var x6 = r - dx6;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="ssd32" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ssd32" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="ssd16" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="ssd8" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ssd8" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="ssd16" y="y2" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>



            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");

            var ssd32 = ss / 32;
            var ssd16 = ss / 16;
            var ssd8 = ss / 8;
            //    <lnTo>
            //      <pt x="ssd32" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ssd32, y1);
            //    <lnTo>
            //      <pt x="ssd32" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ssd32, y2);
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y2);
            //    <close />
            stringPath.Append(" z ");
            //    <moveTo>
            //      <pt x="ssd16" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(ssd16, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="ssd8" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ssd8, y1);
            //    <lnTo>
            //      <pt x="ssd8" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ssd8, y2);
            //    <lnTo>
            //      <pt x="ssd16" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, ssd16, y2);
            //    <close />
            stringPath.Append(" z ");
            //    <moveTo>
            //      <pt x="x4" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(x4, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y1);
            //    <lnTo>
            //      <pt x="x5" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, t);
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //    <lnTo>
            //      <pt x="x5" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, b);
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x5, y2);
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="x4" t="y1" r="x6" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x4, y1, x6, y2);

            return shapePaths;
        }
    }
}
