using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 星形：七角
    /// </summary>
    public class Star7Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 34601" />
            //  <gd name="hf" fmla="val 102572" />
            //  <gd name="vf" fmla="val 105210" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 34601d;
            var customHf = adjusts?.GetAdjustValue("hf");
            var hf = customHf ?? 102572d;
            var customVf = adjusts?.GetAdjustValue("vf");
            var vf = customVf ?? 105210d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="swd2" fmla="*/ wd2 hf 100000" />
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            //  <gd name="svc" fmla="*/ vc  vf 100000" />
            //  <gd name="dx1" fmla="*/ swd2 97493 100000" />
            //  <gd name="dx2" fmla="*/ swd2 78183 100000" />
            //  <gd name="dx3" fmla="*/ swd2 43388 100000" />
            //  <gd name="dy1" fmla="*/ shd2 62349 100000" />
            //  <gd name="dy2" fmla="*/ shd2 22252 100000" />
            //  <gd name="dy3" fmla="*/ shd2 90097 100000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc 0 dx3" />
            //  <gd name="x4" fmla="+- hc dx3 0" />
            //  <gd name="x5" fmla="+- hc dx2 0" />
            //  <gd name="x6" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- svc 0 dy1" />
            //  <gd name="y2" fmla="+- svc dy2 0" />
            //  <gd name="y3" fmla="+- svc dy3 0" />
            //  <gd name="iwd2" fmla="*/ swd2 a 50000" />
            //  <gd name="ihd2" fmla="*/ shd2 a 50000" />
            //  <gd name="sdx1" fmla="*/ iwd2 97493 100000" />
            //  <gd name="sdx2" fmla="*/ iwd2 78183 100000" />
            //  <gd name="sdx3" fmla="*/ iwd2 43388 100000" />
            //  <gd name="sx1" fmla="+- hc 0 sdx1" />
            //  <gd name="sx2" fmla="+- hc 0 sdx2" />
            //  <gd name="sx3" fmla="+- hc 0 sdx3" />
            //  <gd name="sx4" fmla="+- hc sdx3 0" />
            //  <gd name="sx5" fmla="+- hc sdx2 0" />
            //  <gd name="sx6" fmla="+- hc sdx1 0" />
            //  <gd name="sdy1" fmla="*/ ihd2 90097 100000" />
            //  <gd name="sdy2" fmla="*/ ihd2 22252 100000" />
            //  <gd name="sdy3" fmla="*/ ihd2 62349 100000" />
            //  <gd name="sy1" fmla="+- svc 0 sdy1" />
            //  <gd name="sy2" fmla="+- svc 0 sdy2" />
            //  <gd name="sy3" fmla="+- svc sdy3 0" />
            //  <gd name="sy4" fmla="+- svc ihd2 0" />
            //  <gd name="yAdj" fmla="+- svc 0 ihd2" />
            //</gdLst>

            //<gd name="a" fmla="pin 0 adj 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = Pin(0, adj, 50000);
            //<gd name="swd2" fmla="*/ wd2 hf 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swd2 = wd2 * hf / 100000;
            //<gd name="shd2" fmla="*/ hd2 vf 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var shd2 = hd2 * vf / 100000;
            //<gd name="svc" fmla="*/ vc  vf 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var svc = vc * vf / 100000;
            //<gd name="dx1" fmla="*/ swd2 97493 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = swd2 * 97493 / 100000;
            //<gd name="dx2" fmla="*/ swd2 78183 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = swd2 * 78183 / 100000;
            //<gd name="dx3" fmla="*/ swd2 43388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = swd2 * 43388 / 100000;
            //<gd name="dy1" fmla="*/ shd2 62349 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = shd2 * 62349 / 100000;
            //<gd name="dy2" fmla="*/ shd2 22252 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = shd2 * 22252 / 100000;
            //<gd name="dy3" fmla="*/ shd2 90097 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = shd2 * 90097 / 100000;
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x2" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - dx2;
            //<gd name="x3" fmla="+- hc 0 dx3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + 0 - dx3;
            //<gd name="x4" fmla="+- hc dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = hc + dx3 - 0;
            //<gd name="x5" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = hc + dx2 - 0;
            //<gd name="x6" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = hc + dx1 - 0;
            //<gd name="y1" fmla="+- svc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = svc + 0 - dy1;
            //<gd name="y2" fmla="+- svc dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = svc + dy2 - 0;
            //<gd name="y3" fmla="+- svc dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = svc + dy3 - 0;
            //<gd name="iwd2" fmla="*/ swd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iwd2 = swd2 * a / 50000;
            //<gd name="ihd2" fmla="*/ shd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ihd2 = shd2 * a / 50000;
            //<gd name="sdx1" fmla="*/ iwd2 97493 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx1 = iwd2 * 97493 / 100000;
            //<gd name="sdx2" fmla="*/ iwd2 78183 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx2 = iwd2 * 78183 / 100000;
            //<gd name="sdx3" fmla="*/ iwd2 43388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx3 = iwd2 * 43388 / 100000;
            //<gd name="sx1" fmla="+- hc 0 sdx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx1 = hc + 0 - sdx1;
            //<gd name="sx2" fmla="+- hc 0 sdx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx2 = hc + 0 - sdx2;
            //<gd name="sx3" fmla="+- hc 0 sdx3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx3 = hc + 0 - sdx3;
            //<gd name="sx4" fmla="+- hc sdx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx4 = hc + sdx3 - 0;
            //<gd name="sx5" fmla="+- hc sdx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx5 = hc + sdx2 - 0;
            //<gd name="sx6" fmla="+- hc sdx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx6 = hc + sdx1 - 0;
            //<gd name="sdy1" fmla="*/ ihd2 90097 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy1 = ihd2 * 90097 / 100000;
            //<gd name="sdy2" fmla="*/ ihd2 22252 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy2 = ihd2 * 22252 / 100000;
            //<gd name="sdy3" fmla="*/ ihd2 62349 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy3 = ihd2 * 62349 / 100000;
            //<gd name="sy1" fmla="+- svc 0 sdy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy1 = svc + 0 - sdy1;
            //<gd name="sy2" fmla="+- svc 0 sdy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy2 = svc + 0 - sdy2;
            //<gd name="sy3" fmla="+- svc sdy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy3 = svc + sdy3 - 0;
            //<gd name="sy4" fmla="+- svc ihd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy4 = svc + ihd2 - 0;
            //<gd name="yAdj" fmla="+- svc 0 ihd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAdj = svc + 0 - ihd2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y2" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="sx1" y="sy2" />
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
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy3" />
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
            //  <pt x="sx1" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy2);
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
            //  <pt x="x5" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy3);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="sx2" t="sy1" r="sx5" b="sy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(sx2, sy1, sx5, sy3);

            return shapePaths;
        }
    }


}

