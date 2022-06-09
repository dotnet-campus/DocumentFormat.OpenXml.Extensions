using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 旋风箭头
    /// </summary>
    public class SwooshArrowGeometry : ShapeGeometryBase
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
            //  <gd name="adj2" fmla="val 16667" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 25000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 16667d;

            var ssd8 = ss / 8;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 1 adj1 75000" />
            //  <gd name="maxAdj2" fmla="*/ 70000 w ss" />
            //  <gd name="a2" fmla="pin 0 adj2 maxAdj2" />
            //  <gd name="ad1" fmla="*/ h a1 100000" />
            //  <gd name="ad2" fmla="*/ ss a2 100000" />
            //  <gd name="xB" fmla="+- r 0 ad2" />
            //  <gd name="yB" fmla="+- t ssd8 0" />
            //  <gd name="alfa" fmla="*/ cd4 1 14" />
            //  <gd name="dx0" fmla="tan ssd8 alfa" />
            //  <gd name="xC" fmla="+- xB 0 dx0" />
            //  <gd name="dx1" fmla="tan ad1 alfa" />
            //  <gd name="yF" fmla="+- yB ad1 0" />
            //  <gd name="xF" fmla="+- xB dx1 0" />
            //  <gd name="xE" fmla="+- xF dx0 0" />
            //  <gd name="yE" fmla="+- yF ssd8 0" />
            //  <gd name="dy2" fmla="+- yE 0 t" />
            //  <gd name="dy22" fmla="*/ dy2 1 2" />
            //  <gd name="dy3" fmla="*/ h 1 20" />
            //  <gd name="yD" fmla="+- t dy22 dy3" />
            //  <gd name="dy4" fmla="*/ hd6 1 1" />
            //  <gd name="yP1" fmla="+- hd6 dy4 0" />
            //  <gd name="xP1" fmla="val wd6" />
            //  <gd name="dy5" fmla="*/ hd6 1 2" />
            //  <gd name="yP2" fmla="+- yF dy5 0" />
            //  <gd name="xP2" fmla="val wd4" />
            //</gdLst>

            //<gd name="a1" fmla="pin 1 adj1 75000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(1, adj1, 75000);
            //<gd name="maxAdj2" fmla="*/ 70000 w ss" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj2 = 70000 * w / ss;
            //<gd name="a2" fmla="pin 0 adj2 maxAdj2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(0, adj2, maxAdj2);
            //<gd name="ad1" fmla="*/ h a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ad1 = h * a1 / 100000;
            //<gd name="ad2" fmla="*/ ss a2 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ad2 = ss * a2 / 100000;
            //<gd name="xB" fmla="+- r 0 ad2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB = r + 0 - ad2;
            //<gd name="yB" fmla="+- t ssd8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB = t + ssd8 - 0;
            //<gd name="alfa" fmla="*/ cd4 1 14" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var alfa = cd4 * 1 / 14;
            //<gd name="dx0" fmla="tan ssd8 alfa" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx0 = Tan(ssd8, (int) alfa);
            //<gd name="xC" fmla="+- xB 0 dx0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC = xB + 0 - dx0;
            //<gd name="dx1" fmla="tan ad1 alfa" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = Tan(ad1, (int) alfa);
            //<gd name="yF" fmla="+- yB ad1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yF = yB + ad1 - 0;
            //<gd name="xF" fmla="+- xB dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF = xB + dx1 - 0;
            //<gd name="xE" fmla="+- xF dx0 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE = xF + dx0 - 0;
            //<gd name="yE" fmla="+- yF ssd8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yE = yF + ssd8 - 0;
            //<gd name="dy2" fmla="+- yE 0 t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = yE + 0 - t;
            //<gd name="dy22" fmla="*/ dy2 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy22 = dy2 * 1 / 2;
            //<gd name="dy3" fmla="*/ h 1 20" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = h * 1 / 20;
            //<gd name="yD" fmla="+- t dy22 dy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD = t + dy22 - dy3;
            //<gd name="dy4" fmla="*/ hd6 1 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy4 = hd6 * 1 / 1;
            //<gd name="yP1" fmla="+- hd6 dy4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yP1 = hd6 + dy4 - 0;
            //<gd name="xP1" fmla="val wd6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xP1 = wd6;
            //<gd name="dy5" fmla="*/ hd6 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy5 = hd6 * 1 / 2;
            //<gd name="yP2" fmla="+- yF dy5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yP2 = yF + dy5 - 0;
            //<gd name="xP2" fmla="val wd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xP2 = wd4;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <quadBezTo>
            //      <pt x="xP1" y="yP1" />
            //      <pt x="xB" y="yB" />
            //    </quadBezTo>
            //    <lnTo>
            //      <pt x="xC" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="yD" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xE" y="yE" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xF" y="yF" />
            //    </lnTo>
            //    <quadBezTo>
            //      <pt x="xP2" y="yP2" />
            //      <pt x="l" y="b" />
            //    </quadBezTo>
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
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xP1" y="yP1" />
            //  <pt x="xB" y="yB" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, xP1, yP1, xB, yB);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="yD" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, yD);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE" y="yE" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xE, yE);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xF" y="yF" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xF, yF);
            //<quadBezTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xP2" y="yP2" />
            //  <pt x="l" y="b" />
            //</quadBezTo>
            currentPoint = QuadBezToString(stringPath, xP2, yP2, l, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }


}

