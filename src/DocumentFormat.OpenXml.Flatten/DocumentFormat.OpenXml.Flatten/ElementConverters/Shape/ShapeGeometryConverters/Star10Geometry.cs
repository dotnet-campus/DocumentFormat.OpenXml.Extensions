using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 星形: 十角
    /// </summary>
    public class Star10Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 42533" />
            //  <gd name="hf" fmla="val 105146" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 42533d;
            var customHf = adjusts?.GetAdjustValue("hf");
            var hf = customHf ?? 105146d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="swd2" fmla="*/ wd2 hf 100000" />
            //  <gd name="dx1" fmla="*/ swd2 95106 100000" />
            //  <gd name="dx2" fmla="*/ swd2 58779 100000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc dx1 0" />
            //  <gd name="dy1" fmla="*/ hd2 80902 100000" />
            //  <gd name="dy2" fmla="*/ hd2 30902 100000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc dy2 0" />
            //  <gd name="y4" fmla="+- vc dy1 0" />
            //  <gd name="iwd2" fmla="*/ swd2 a 50000" />
            //  <gd name="ihd2" fmla="*/ hd2 a 50000" />
            //  <gd name="sdx1" fmla="*/ iwd2 80902 100000" />
            //  <gd name="sdx2" fmla="*/ iwd2 30902 100000" />
            //  <gd name="sdy1" fmla="*/ ihd2 95106 100000" />
            //  <gd name="sdy2" fmla="*/ ihd2 58779 100000" />
            //  <gd name="sx1" fmla="+- hc 0 iwd2" />
            //  <gd name="sx2" fmla="+- hc 0 sdx1" />
            //  <gd name="sx3" fmla="+- hc 0 sdx2" />
            //  <gd name="sx4" fmla="+- hc sdx2 0" />
            //  <gd name="sx5" fmla="+- hc sdx1 0" />
            //  <gd name="sx6" fmla="+- hc iwd2 0" />
            //  <gd name="sy1" fmla="+- vc 0 sdy1" />
            //  <gd name="sy2" fmla="+- vc 0 sdy2" />
            //  <gd name="sy3" fmla="+- vc sdy2 0" />
            //  <gd name="sy4" fmla="+- vc sdy1 0" />
            //  <gd name="yAdj" fmla="+- vc 0 ihd2" />
            //</gdLst>

            //<gd name="a" fmla="pin 0 adj 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = Pin(0, adj, 50000);
            //<gd name="swd2" fmla="*/ wd2 hf 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swd2 = wd2 * hf / 100000;
            //<gd name="dx1" fmla="*/ swd2 95106 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = swd2 * 95106 / 100000;
            //<gd name="dx2" fmla="*/ swd2 58779 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = swd2 * 58779 / 100000;
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x2" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - dx2;
            //<gd name="x3" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + dx2 - 0;
            //<gd name="x4" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = hc + dx1 - 0;
            //<gd name="dy1" fmla="*/ hd2 80902 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = hd2 * 80902 / 100000;
            //<gd name="dy2" fmla="*/ hd2 30902 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = hd2 * 30902 / 100000;
            //<gd name="y1" fmla="+- vc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = vc + 0 - dy1;
            //<gd name="y2" fmla="+- vc 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = vc + 0 - dy2;
            //<gd name="y3" fmla="+- vc dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = vc + dy2 - 0;
            //<gd name="y4" fmla="+- vc dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = vc + dy1 - 0;
            //<gd name="iwd2" fmla="*/ swd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iwd2 = swd2 * a / 50000;
            //<gd name="ihd2" fmla="*/ hd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ihd2 = hd2 * a / 50000;
            //<gd name="sdx1" fmla="*/ iwd2 80902 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx1 = iwd2 * 80902 / 100000;
            //<gd name="sdx2" fmla="*/ iwd2 30902 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx2 = iwd2 * 30902 / 100000;
            //<gd name="sdy1" fmla="*/ ihd2 95106 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy1 = ihd2 * 95106 / 100000;
            //<gd name="sdy2" fmla="*/ ihd2 58779 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy2 = ihd2 * 58779 / 100000;
            //<gd name="sx1" fmla="+- hc 0 iwd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx1 = hc + 0 - iwd2;
            //<gd name="sx2" fmla="+- hc 0 sdx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx2 = hc + 0 - sdx1;
            //<gd name="sx3" fmla="+- hc 0 sdx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx3 = hc + 0 - sdx2;
            //<gd name="sx4" fmla="+- hc sdx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx4 = hc + sdx2 - 0;
            //<gd name="sx5" fmla="+- hc sdx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx5 = hc + sdx1 - 0;
            //<gd name="sx6" fmla="+- hc iwd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx6 = hc + iwd2 - 0;
            //<gd name="sy1" fmla="+- vc 0 sdy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy1 = vc + 0 - sdy1;
            //<gd name="sy2" fmla="+- vc 0 sdy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy2 = vc + 0 - sdy2;
            //<gd name="sy3" fmla="+- vc sdy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy3 = vc + sdy2 - 0;
            //<gd name="sy4" fmla="+- vc sdy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy4 = vc + sdy1 - 0;
            //<gd name="yAdj" fmla="+- vc 0 ihd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAdj = vc + 0 - ihd2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx1" y="vc" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y2" />
            //</moveTo>
            var currentPoint = new EmuPoint(x1, y2);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, vc);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx1" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, vc);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="sx2" t="sy2" r="sx5" b="sy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(sx2, sy2, sx5, sy3);

            return shapePaths;
        }
    }


}

