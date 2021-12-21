using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 星形: 五角
    /// </summary>
    public class Star5Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 19098" />
            //  <gd name="hf" fmla="val 105146" />
            //  <gd name="vf" fmla="val 110557" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 19098d;
            var customHf = adjusts?.GetAdjustValue("hf");
            var hf = customHf ?? 105146d;
            var customVf = adjusts?.GetAdjustValue("vf");
            var vf = customVf ?? 110557d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="swd2" fmla="*/ wd2 hf 100000" />
            //  <gd name="shd2" fmla="*/ hd2 vf 100000" />
            //  <gd name="svc" fmla="*/ vc  vf 100000" />
            //  <gd name="dx1" fmla="cos swd2 1080000" />
            //  <gd name="dx2" fmla="cos swd2 18360000" />
            //  <gd name="dy1" fmla="sin shd2 1080000" />
            //  <gd name="dy2" fmla="sin shd2 18360000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc dx2 0" />
            //  <gd name="x4" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- svc 0 dy1" />
            //  <gd name="y2" fmla="+- svc 0 dy2" />
            //  <gd name="iwd2" fmla="*/ swd2 a 50000" />
            //  <gd name="ihd2" fmla="*/ shd2 a 50000" />
            //  <gd name="sdx1" fmla="cos iwd2 20520000" />
            //  <gd name="sdx2" fmla="cos iwd2 3240000" />
            //  <gd name="sdy1" fmla="sin ihd2 3240000" />
            //  <gd name="sdy2" fmla="sin ihd2 20520000" />
            //  <gd name="sx1" fmla="+- hc 0 sdx1" />
            //  <gd name="sx2" fmla="+- hc 0 sdx2" />
            //  <gd name="sx3" fmla="+- hc sdx2 0" />
            //  <gd name="sx4" fmla="+- hc sdx1 0" />
            //  <gd name="sy1" fmla="+- svc 0 sdy1" />
            //  <gd name="sy2" fmla="+- svc 0 sdy2" />
            //  <gd name="sy3" fmla="+- svc ihd2 0" />
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
            //<gd name="dx1" fmla="cos swd2 1080000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = Cos(swd2, (int) 1080000);
            //<gd name="dx2" fmla="cos swd2 18360000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = Cos(swd2, (int) 18360000);
            //<gd name="dy1" fmla="sin shd2 1080000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = Sin(shd2, (int) 1080000);
            //<gd name="dy2" fmla="sin shd2 18360000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = Sin(shd2, (int) 18360000);
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x2" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - dx2;
            //<gd name="x3" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + dx2 - 0;
            //<gd name="x4" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = hc + dx1 - 0;
            //<gd name="y1" fmla="+- svc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = svc + 0 - dy1;
            //<gd name="y2" fmla="+- svc 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = svc + 0 - dy2;
            //<gd name="iwd2" fmla="*/ swd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iwd2 = swd2 * a / 50000;
            //<gd name="ihd2" fmla="*/ shd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ihd2 = shd2 * a / 50000;
            //<gd name="sdx1" fmla="cos iwd2 20520000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx1 = Cos(iwd2, (int) 20520000);
            //<gd name="sdx2" fmla="cos iwd2 3240000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx2 = Cos(iwd2, (int) 3240000);
            //<gd name="sdy1" fmla="sin ihd2 3240000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy1 = Sin(ihd2, (int) 3240000);
            //<gd name="sdy2" fmla="sin ihd2 20520000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy2 = Sin(ihd2, (int) 20520000);
            //<gd name="sx1" fmla="+- hc 0 sdx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx1 = hc + 0 - sdx1;
            //<gd name="sx2" fmla="+- hc 0 sdx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx2 = hc + 0 - sdx2;
            //<gd name="sx3" fmla="+- hc sdx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx3 = hc + sdx2 - 0;
            //<gd name="sx4" fmla="+- hc sdx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx4 = hc + sdx1 - 0;
            //<gd name="sy1" fmla="+- svc 0 sdy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy1 = svc + 0 - sdy1;
            //<gd name="sy2" fmla="+- svc 0 sdy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy2 = svc + 0 - sdy2;
            //<gd name="sy3" fmla="+- svc ihd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy3 = svc + ihd2 - 0;
            //<gd name="yAdj" fmla="+- svc 0 ihd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAdj = svc + 0 - ihd2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx1" y="sy2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y1" />
            //</moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx1" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="sx1" t="sy1" r="sx4" b="sy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(sx1, sy1, sx4, sy3);

            return shapePaths;
        }
    }


}

