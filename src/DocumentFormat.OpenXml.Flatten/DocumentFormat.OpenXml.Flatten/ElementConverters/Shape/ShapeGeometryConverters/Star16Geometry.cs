﻿using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 星形：十六角
    /// </summary>
    public class Star16Geometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj" fmla="val 37500" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 37500d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 50000" />
            //  <gd name="dx1" fmla="*/ wd2 92388 100000" />
            //  <gd name="dx2" fmla="*/ wd2 70711 100000" />
            //  <gd name="dx3" fmla="*/ wd2 38268 100000" />
            //  <gd name="dy1" fmla="*/ hd2 92388 100000" />
            //  <gd name="dy2" fmla="*/ hd2 70711 100000" />
            //  <gd name="dy3" fmla="*/ hd2 38268 100000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc 0 dx3" />
            //  <gd name="x4" fmla="+- hc dx3 0" />
            //  <gd name="x5" fmla="+- hc dx2 0" />
            //  <gd name="x6" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc 0 dy3" />
            //  <gd name="y4" fmla="+- vc dy3 0" />
            //  <gd name="y5" fmla="+- vc dy2 0" />
            //  <gd name="y6" fmla="+- vc dy1 0" />
            //  <gd name="iwd2" fmla="*/ wd2 a 50000" />
            //  <gd name="ihd2" fmla="*/ hd2 a 50000" />
            //  <gd name="sdx1" fmla="*/ iwd2 98079 100000" />
            //  <gd name="sdx2" fmla="*/ iwd2 83147 100000" />
            //  <gd name="sdx3" fmla="*/ iwd2 55557 100000" />
            //  <gd name="sdx4" fmla="*/ iwd2 19509 100000" />
            //  <gd name="sdy1" fmla="*/ ihd2 98079 100000" />
            //  <gd name="sdy2" fmla="*/ ihd2 83147 100000" />
            //  <gd name="sdy3" fmla="*/ ihd2 55557 100000" />
            //  <gd name="sdy4" fmla="*/ ihd2 19509 100000" />
            //  <gd name="sx1" fmla="+- hc 0 sdx1" />
            //  <gd name="sx2" fmla="+- hc 0 sdx2" />
            //  <gd name="sx3" fmla="+- hc 0 sdx3" />
            //  <gd name="sx4" fmla="+- hc 0 sdx4" />
            //  <gd name="sx5" fmla="+- hc sdx4 0" />
            //  <gd name="sx6" fmla="+- hc sdx3 0" />
            //  <gd name="sx7" fmla="+- hc sdx2 0" />
            //  <gd name="sx8" fmla="+- hc sdx1 0" />
            //  <gd name="sy1" fmla="+- vc 0 sdy1" />
            //  <gd name="sy2" fmla="+- vc 0 sdy2" />
            //  <gd name="sy3" fmla="+- vc 0 sdy3" />
            //  <gd name="sy4" fmla="+- vc 0 sdy4" />
            //  <gd name="sy5" fmla="+- vc sdy4 0" />
            //  <gd name="sy6" fmla="+- vc sdy3 0" />
            //  <gd name="sy7" fmla="+- vc sdy2 0" />
            //  <gd name="sy8" fmla="+- vc sdy1 0" />
            //  <gd name="idx" fmla="cos iwd2 2700000" />
            //  <gd name="idy" fmla="sin ihd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //  <gd name="yAdj" fmla="+- vc 0 ihd2" />
            //</gdLst>

            //<gd name="a" fmla="pin 0 adj 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = Pin(0, adj, 50000);
            //<gd name="dx1" fmla="*/ wd2 92388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = wd2 * 92388 / 100000;
            //<gd name="dx2" fmla="*/ wd2 70711 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = wd2 * 70711 / 100000;
            //<gd name="dx3" fmla="*/ wd2 38268 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = wd2 * 38268 / 100000;
            //<gd name="dy1" fmla="*/ hd2 92388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = hd2 * 92388 / 100000;
            //<gd name="dy2" fmla="*/ hd2 70711 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = hd2 * 70711 / 100000;
            //<gd name="dy3" fmla="*/ hd2 38268 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = hd2 * 38268 / 100000;
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
            //<gd name="y1" fmla="+- vc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = vc + 0 - dy1;
            //<gd name="y2" fmla="+- vc 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = vc + 0 - dy2;
            //<gd name="y3" fmla="+- vc 0 dy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = vc + 0 - dy3;
            //<gd name="y4" fmla="+- vc dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = vc + dy3 - 0;
            //<gd name="y5" fmla="+- vc dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = vc + dy2 - 0;
            //<gd name="y6" fmla="+- vc dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = vc + dy1 - 0;
            //<gd name="iwd2" fmla="*/ wd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iwd2 = wd2 * a / 50000;
            //<gd name="ihd2" fmla="*/ hd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ihd2 = hd2 * a / 50000;
            //<gd name="sdx1" fmla="*/ iwd2 98079 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx1 = iwd2 * 98079 / 100000;
            //<gd name="sdx2" fmla="*/ iwd2 83147 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx2 = iwd2 * 83147 / 100000;
            //<gd name="sdx3" fmla="*/ iwd2 55557 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx3 = iwd2 * 55557 / 100000;
            //<gd name="sdx4" fmla="*/ iwd2 19509 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx4 = iwd2 * 19509 / 100000;
            //<gd name="sdy1" fmla="*/ ihd2 98079 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy1 = ihd2 * 98079 / 100000;
            //<gd name="sdy2" fmla="*/ ihd2 83147 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy2 = ihd2 * 83147 / 100000;
            //<gd name="sdy3" fmla="*/ ihd2 55557 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy3 = ihd2 * 55557 / 100000;
            //<gd name="sdy4" fmla="*/ ihd2 19509 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy4 = ihd2 * 19509 / 100000;
            //<gd name="sx1" fmla="+- hc 0 sdx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx1 = hc + 0 - sdx1;
            //<gd name="sx2" fmla="+- hc 0 sdx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx2 = hc + 0 - sdx2;
            //<gd name="sx3" fmla="+- hc 0 sdx3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx3 = hc + 0 - sdx3;
            //<gd name="sx4" fmla="+- hc 0 sdx4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx4 = hc + 0 - sdx4;
            //<gd name="sx5" fmla="+- hc sdx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx5 = hc + sdx4 - 0;
            //<gd name="sx6" fmla="+- hc sdx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx6 = hc + sdx3 - 0;
            //<gd name="sx7" fmla="+- hc sdx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx7 = hc + sdx2 - 0;
            //<gd name="sx8" fmla="+- hc sdx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx8 = hc + sdx1 - 0;
            //<gd name="sy1" fmla="+- vc 0 sdy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy1 = vc + 0 - sdy1;
            //<gd name="sy2" fmla="+- vc 0 sdy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy2 = vc + 0 - sdy2;
            //<gd name="sy3" fmla="+- vc 0 sdy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy3 = vc + 0 - sdy3;
            //<gd name="sy4" fmla="+- vc 0 sdy4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy4 = vc + 0 - sdy4;
            //<gd name="sy5" fmla="+- vc sdy4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy5 = vc + sdy4 - 0;
            //<gd name="sy6" fmla="+- vc sdy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy6 = vc + sdy3 - 0;
            //<gd name="sy7" fmla="+- vc sdy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy7 = vc + sdy2 - 0;
            //<gd name="sy8" fmla="+- vc sdy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy8 = vc + sdy1 - 0;
            //<gd name="idx" fmla="cos iwd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var idx = Cos(iwd2, (int) 2700000);
            //<gd name="idy" fmla="sin ihd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var idy = Sin(ihd2, (int) 2700000);
            //<gd name="il" fmla="+- hc 0 idx" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = hc + 0 - idx;
            //<gd name="it" fmla="+- vc 0 idy" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var it = vc + 0 - idy;
            //<gd name="ir" fmla="+- hc idx 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = hc + idx - 0;
            //<gd name="ib" fmla="+- vc idy 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = vc + idy - 0;
            //<gd name="yAdj" fmla="+- vc 0 ihd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAdj = vc + 0 - ihd2;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="sx1" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx7" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx8" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx8" y="sy5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx7" y="sy6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx1" y="sy5" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="vc" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx1" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx7" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx7, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx8" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx8, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx8" y="sy5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx8, sy5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx7" y="sy6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx7, sy6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx1" y="sy5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy5);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

