using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 星形: 二十四角
    /// </summary>
    public class Star24Geometry : ShapeGeometryBase
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
            //  <gd name="dx1" fmla="cos wd2 900000" />
            //  <gd name="dx2" fmla="cos wd2 1800000" />
            //  <gd name="dx3" fmla="cos wd2 2700000" />
            //  <gd name="dx4" fmla="val wd4" />
            //  <gd name="dx5" fmla="cos wd2 4500000" />
            //  <gd name="dy1" fmla="sin hd2 4500000" />
            //  <gd name="dy2" fmla="sin hd2 3600000" />
            //  <gd name="dy3" fmla="sin hd2 2700000" />
            //  <gd name="dy4" fmla="val hd4" />
            //  <gd name="dy5" fmla="sin hd2 900000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc 0 dx3" />
            //  <gd name="x4" fmla="+- hc 0 dx4" />
            //  <gd name="x5" fmla="+- hc 0 dx5" />
            //  <gd name="x6" fmla="+- hc dx5 0" />
            //  <gd name="x7" fmla="+- hc dx4 0" />
            //  <gd name="x8" fmla="+- hc dx3 0" />
            //  <gd name="x9" fmla="+- hc dx2 0" />
            //  <gd name="x10" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc 0 dy3" />
            //  <gd name="y4" fmla="+- vc 0 dy4" />
            //  <gd name="y5" fmla="+- vc 0 dy5" />
            //  <gd name="y6" fmla="+- vc dy5 0" />
            //  <gd name="y7" fmla="+- vc dy4 0" />
            //  <gd name="y8" fmla="+- vc dy3 0" />
            //  <gd name="y9" fmla="+- vc dy2 0" />
            //  <gd name="y10" fmla="+- vc dy1 0" />
            //  <gd name="iwd2" fmla="*/ wd2 a 50000" />
            //  <gd name="ihd2" fmla="*/ hd2 a 50000" />
            //  <gd name="sdx1" fmla="*/ iwd2 99144 100000" />
            //  <gd name="sdx2" fmla="*/ iwd2 92388 100000" />
            //  <gd name="sdx3" fmla="*/ iwd2 79335 100000" />
            //  <gd name="sdx4" fmla="*/ iwd2 60876 100000" />
            //  <gd name="sdx5" fmla="*/ iwd2 38268 100000" />
            //  <gd name="sdx6" fmla="*/ iwd2 13053 100000" />
            //  <gd name="sdy1" fmla="*/ ihd2 99144 100000" />
            //  <gd name="sdy2" fmla="*/ ihd2 92388 100000" />
            //  <gd name="sdy3" fmla="*/ ihd2 79335 100000" />
            //  <gd name="sdy4" fmla="*/ ihd2 60876 100000" />
            //  <gd name="sdy5" fmla="*/ ihd2 38268 100000" />
            //  <gd name="sdy6" fmla="*/ ihd2 13053 100000" />
            //  <gd name="sx1" fmla="+- hc 0 sdx1" />
            //  <gd name="sx2" fmla="+- hc 0 sdx2" />
            //  <gd name="sx3" fmla="+- hc 0 sdx3" />
            //  <gd name="sx4" fmla="+- hc 0 sdx4" />
            //  <gd name="sx5" fmla="+- hc 0 sdx5" />
            //  <gd name="sx6" fmla="+- hc 0 sdx6" />
            //  <gd name="sx7" fmla="+- hc sdx6 0" />
            //  <gd name="sx8" fmla="+- hc sdx5 0" />
            //  <gd name="sx9" fmla="+- hc sdx4 0" />
            //  <gd name="sx10" fmla="+- hc sdx3 0" />
            //  <gd name="sx11" fmla="+- hc sdx2 0" />
            //  <gd name="sx12" fmla="+- hc sdx1 0" />
            //  <gd name="sy1" fmla="+- vc 0 sdy1" />
            //  <gd name="sy2" fmla="+- vc 0 sdy2" />
            //  <gd name="sy3" fmla="+- vc 0 sdy3" />
            //  <gd name="sy4" fmla="+- vc 0 sdy4" />
            //  <gd name="sy5" fmla="+- vc 0 sdy5" />
            //  <gd name="sy6" fmla="+- vc 0 sdy6" />
            //  <gd name="sy7" fmla="+- vc sdy6 0" />
            //  <gd name="sy8" fmla="+- vc sdy5 0" />
            //  <gd name="sy9" fmla="+- vc sdy4 0" />
            //  <gd name="sy10" fmla="+- vc sdy3 0" />
            //  <gd name="sy11" fmla="+- vc sdy2 0" />
            //  <gd name="sy12" fmla="+- vc sdy1 0" />
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
            //<gd name="dx1" fmla="cos wd2 900000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = Cos(wd2, (int) 900000);
            //<gd name="dx2" fmla="cos wd2 1800000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = Cos(wd2, (int) 1800000);
            //<gd name="dx3" fmla="cos wd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = Cos(wd2, (int) 2700000);
            //  <gd name="dx4" fmla="val wd4" />
            var dx4 = wd4;
            //<gd name="dx5" fmla="cos wd2 4500000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx5 = Cos(wd2, (int) 4500000);
            //<gd name="dy1" fmla="sin hd2 4500000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = Sin(hd2, (int) 4500000);
            //<gd name="dy2" fmla="sin hd2 3600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = Sin(hd2, (int) 3600000);
            //<gd name="dy3" fmla="sin hd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = Sin(hd2, (int) 2700000);
            //  <gd name="dy4" fmla="val hd4" />
            var dy4 = hd4;
            //<gd name="dy5" fmla="sin hd2 900000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy5 = Sin(hd2, (int) 900000);
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x2" fmla="+- hc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + 0 - dx2;
            //<gd name="x3" fmla="+- hc 0 dx3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x3 = hc + 0 - dx3;
            //<gd name="x4" fmla="+- hc 0 dx4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x4 = hc + 0 - dx4;
            //<gd name="x5" fmla="+- hc 0 dx5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x5 = hc + 0 - dx5;
            //<gd name="x6" fmla="+- hc dx5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = hc + dx5 - 0;
            //<gd name="x7" fmla="+- hc dx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x7 = hc + dx4 - 0;
            //<gd name="x8" fmla="+- hc dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x8 = hc + dx3 - 0;
            //<gd name="x9" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x9 = hc + dx2 - 0;
            //<gd name="x10" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x10 = hc + dx1 - 0;
            //<gd name="y1" fmla="+- vc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = vc + 0 - dy1;
            //<gd name="y2" fmla="+- vc 0 dy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = vc + 0 - dy2;
            //<gd name="y3" fmla="+- vc 0 dy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y3 = vc + 0 - dy3;
            //<gd name="y4" fmla="+- vc 0 dy4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y4 = vc + 0 - dy4;
            //<gd name="y5" fmla="+- vc 0 dy5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y5 = vc + 0 - dy5;
            //<gd name="y6" fmla="+- vc dy5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = vc + dy5 - 0;
            //<gd name="y7" fmla="+- vc dy4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y7 = vc + dy4 - 0;
            //<gd name="y8" fmla="+- vc dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y8 = vc + dy3 - 0;
            //<gd name="y9" fmla="+- vc dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y9 = vc + dy2 - 0;
            //<gd name="y10" fmla="+- vc dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y10 = vc + dy1 - 0;
            //<gd name="iwd2" fmla="*/ wd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iwd2 = wd2 * a / 50000;
            //<gd name="ihd2" fmla="*/ hd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ihd2 = hd2 * a / 50000;
            //<gd name="sdx1" fmla="*/ iwd2 99144 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx1 = iwd2 * 99144 / 100000;
            //<gd name="sdx2" fmla="*/ iwd2 92388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx2 = iwd2 * 92388 / 100000;
            //<gd name="sdx3" fmla="*/ iwd2 79335 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx3 = iwd2 * 79335 / 100000;
            //<gd name="sdx4" fmla="*/ iwd2 60876 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx4 = iwd2 * 60876 / 100000;
            //<gd name="sdx5" fmla="*/ iwd2 38268 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx5 = iwd2 * 38268 / 100000;
            //<gd name="sdx6" fmla="*/ iwd2 13053 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx6 = iwd2 * 13053 / 100000;
            //<gd name="sdy1" fmla="*/ ihd2 99144 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy1 = ihd2 * 99144 / 100000;
            //<gd name="sdy2" fmla="*/ ihd2 92388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy2 = ihd2 * 92388 / 100000;
            //<gd name="sdy3" fmla="*/ ihd2 79335 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy3 = ihd2 * 79335 / 100000;
            //<gd name="sdy4" fmla="*/ ihd2 60876 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy4 = ihd2 * 60876 / 100000;
            //<gd name="sdy5" fmla="*/ ihd2 38268 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy5 = ihd2 * 38268 / 100000;
            //<gd name="sdy6" fmla="*/ ihd2 13053 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy6 = ihd2 * 13053 / 100000;
            //<gd name="sx1" fmla="+- hc 0 sdx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx1 = hc + 0 - sdx1;
            //<gd name="sx2" fmla="+- hc 0 sdx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx2 = hc + 0 - sdx2;
            //<gd name="sx3" fmla="+- hc 0 sdx3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx3 = hc + 0 - sdx3;
            //<gd name="sx4" fmla="+- hc 0 sdx4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx4 = hc + 0 - sdx4;
            //<gd name="sx5" fmla="+- hc 0 sdx5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx5 = hc + 0 - sdx5;
            //<gd name="sx6" fmla="+- hc 0 sdx6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx6 = hc + 0 - sdx6;
            //<gd name="sx7" fmla="+- hc sdx6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx7 = hc + sdx6 - 0;
            //<gd name="sx8" fmla="+- hc sdx5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx8 = hc + sdx5 - 0;
            //<gd name="sx9" fmla="+- hc sdx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx9 = hc + sdx4 - 0;
            //<gd name="sx10" fmla="+- hc sdx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx10 = hc + sdx3 - 0;
            //<gd name="sx11" fmla="+- hc sdx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx11 = hc + sdx2 - 0;
            //<gd name="sx12" fmla="+- hc sdx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx12 = hc + sdx1 - 0;
            //<gd name="sy1" fmla="+- vc 0 sdy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy1 = vc + 0 - sdy1;
            //<gd name="sy2" fmla="+- vc 0 sdy2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy2 = vc + 0 - sdy2;
            //<gd name="sy3" fmla="+- vc 0 sdy3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy3 = vc + 0 - sdy3;
            //<gd name="sy4" fmla="+- vc 0 sdy4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy4 = vc + 0 - sdy4;
            //<gd name="sy5" fmla="+- vc 0 sdy5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy5 = vc + 0 - sdy5;
            //<gd name="sy6" fmla="+- vc 0 sdy6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy6 = vc + 0 - sdy6;
            //<gd name="sy7" fmla="+- vc sdy6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy7 = vc + sdy6 - 0;
            //<gd name="sy8" fmla="+- vc sdy5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy8 = vc + sdy5 - 0;
            //<gd name="sy9" fmla="+- vc sdy4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy9 = vc + sdy4 - 0;
            //<gd name="sy10" fmla="+- vc sdy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy10 = vc + sdy3 - 0;
            //<gd name="sy11" fmla="+- vc sdy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy11 = vc + sdy2 - 0;
            //<gd name="sy12" fmla="+- vc sdy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy12 = vc + sdy1 - 0;
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
            //      <pt x="sx1" y="sy6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx7" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx8" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx9" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx10" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx11" y="sy5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx12" y="sy6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx12" y="sy7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx11" y="sy8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx10" y="sy9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx9" y="sy10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx8" y="sy11" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx7" y="sy12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy11" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx1" y="sy7" />
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
            //  <pt x="sx1" y="sy6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx7" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx7, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx8" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx8, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx9" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx9, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx10" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx10, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx11" y="sy5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx11, sy5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x10" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x10, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx12" y="sy6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx12, sy6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx12" y="sy7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx12, sy7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x10" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x10, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx11" y="sy8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx11, sy8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx10" y="sy9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx10, sy9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx9" y="sy10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx9, sy10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="y9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, y9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx8" y="sy11" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx8, sy11);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx7" y="sy12" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx7, sy12);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy12" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy12);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy11" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy11);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx1" y="sy7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy7);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

