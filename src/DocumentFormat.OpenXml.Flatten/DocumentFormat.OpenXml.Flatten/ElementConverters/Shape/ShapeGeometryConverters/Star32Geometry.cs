using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 星形: 三十二角
    /// </summary>
    public class Star32Geometry : ShapeGeometryBase
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
            //  <gd name="dx1" fmla="*/ wd2 98079 100000" />
            //  <gd name="dx2" fmla="*/ wd2 92388 100000" />
            //  <gd name="dx3" fmla="*/ wd2 83147 100000" />
            //  <gd name="dx4" fmla="cos wd2 2700000" />
            //  <gd name="dx5" fmla="*/ wd2 55557 100000" />
            //  <gd name="dx6" fmla="*/ wd2 38268 100000" />
            //  <gd name="dx7" fmla="*/ wd2 19509 100000" />
            //  <gd name="dy1" fmla="*/ hd2 98079 100000" />
            //  <gd name="dy2" fmla="*/ hd2 92388 100000" />
            //  <gd name="dy3" fmla="*/ hd2 83147 100000" />
            //  <gd name="dy4" fmla="sin hd2 2700000" />
            //  <gd name="dy5" fmla="*/ hd2 55557 100000" />
            //  <gd name="dy6" fmla="*/ hd2 38268 100000" />
            //  <gd name="dy7" fmla="*/ hd2 19509 100000" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc 0 dx2" />
            //  <gd name="x3" fmla="+- hc 0 dx3" />
            //  <gd name="x4" fmla="+- hc 0 dx4" />
            //  <gd name="x5" fmla="+- hc 0 dx5" />
            //  <gd name="x6" fmla="+- hc 0 dx6" />
            //  <gd name="x7" fmla="+- hc 0 dx7" />
            //  <gd name="x8" fmla="+- hc dx7 0" />
            //  <gd name="x9" fmla="+- hc dx6 0" />
            //  <gd name="x10" fmla="+- hc dx5 0" />
            //  <gd name="x11" fmla="+- hc dx4 0" />
            //  <gd name="x12" fmla="+- hc dx3 0" />
            //  <gd name="x13" fmla="+- hc dx2 0" />
            //  <gd name="x14" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc 0 dy2" />
            //  <gd name="y3" fmla="+- vc 0 dy3" />
            //  <gd name="y4" fmla="+- vc 0 dy4" />
            //  <gd name="y5" fmla="+- vc 0 dy5" />
            //  <gd name="y6" fmla="+- vc 0 dy6" />
            //  <gd name="y7" fmla="+- vc 0 dy7" />
            //  <gd name="y8" fmla="+- vc dy7 0" />
            //  <gd name="y9" fmla="+- vc dy6 0" />
            //  <gd name="y10" fmla="+- vc dy5 0" />
            //  <gd name="y11" fmla="+- vc dy4 0" />
            //  <gd name="y12" fmla="+- vc dy3 0" />
            //  <gd name="y13" fmla="+- vc dy2 0" />
            //  <gd name="y14" fmla="+- vc dy1 0" />
            //  <gd name="iwd2" fmla="*/ wd2 a 50000" />
            //  <gd name="ihd2" fmla="*/ hd2 a 50000" />
            //  <gd name="sdx1" fmla="*/ iwd2 99518 100000" />
            //  <gd name="sdx2" fmla="*/ iwd2 95694 100000" />
            //  <gd name="sdx3" fmla="*/ iwd2 88192 100000" />
            //  <gd name="sdx4" fmla="*/ iwd2 77301 100000" />
            //  <gd name="sdx5" fmla="*/ iwd2 63439 100000" />
            //  <gd name="sdx6" fmla="*/ iwd2 47140 100000" />
            //  <gd name="sdx7" fmla="*/ iwd2 29028 100000" />
            //  <gd name="sdx8" fmla="*/ iwd2 9802 100000" />
            //  <gd name="sdy1" fmla="*/ ihd2 99518 100000" />
            //  <gd name="sdy2" fmla="*/ ihd2 95694 100000" />
            //  <gd name="sdy3" fmla="*/ ihd2 88192 100000" />
            //  <gd name="sdy4" fmla="*/ ihd2 77301 100000" />
            //  <gd name="sdy5" fmla="*/ ihd2 63439 100000" />
            //  <gd name="sdy6" fmla="*/ ihd2 47140 100000" />
            //  <gd name="sdy7" fmla="*/ ihd2 29028 100000" />
            //  <gd name="sdy8" fmla="*/ ihd2 9802 100000" />
            //  <gd name="sx1" fmla="+- hc 0 sdx1" />
            //  <gd name="sx2" fmla="+- hc 0 sdx2" />
            //  <gd name="sx3" fmla="+- hc 0 sdx3" />
            //  <gd name="sx4" fmla="+- hc 0 sdx4" />
            //  <gd name="sx5" fmla="+- hc 0 sdx5" />
            //  <gd name="sx6" fmla="+- hc 0 sdx6" />
            //  <gd name="sx7" fmla="+- hc 0 sdx7" />
            //  <gd name="sx8" fmla="+- hc 0 sdx8" />
            //  <gd name="sx9" fmla="+- hc sdx8 0" />
            //  <gd name="sx10" fmla="+- hc sdx7 0" />
            //  <gd name="sx11" fmla="+- hc sdx6 0" />
            //  <gd name="sx12" fmla="+- hc sdx5 0" />
            //  <gd name="sx13" fmla="+- hc sdx4 0" />
            //  <gd name="sx14" fmla="+- hc sdx3 0" />
            //  <gd name="sx15" fmla="+- hc sdx2 0" />
            //  <gd name="sx16" fmla="+- hc sdx1 0" />
            //  <gd name="sy1" fmla="+- vc 0 sdy1" />
            //  <gd name="sy2" fmla="+- vc 0 sdy2" />
            //  <gd name="sy3" fmla="+- vc 0 sdy3" />
            //  <gd name="sy4" fmla="+- vc 0 sdy4" />
            //  <gd name="sy5" fmla="+- vc 0 sdy5" />
            //  <gd name="sy6" fmla="+- vc 0 sdy6" />
            //  <gd name="sy7" fmla="+- vc 0 sdy7" />
            //  <gd name="sy8" fmla="+- vc 0 sdy8" />
            //  <gd name="sy9" fmla="+- vc sdy8 0" />
            //  <gd name="sy10" fmla="+- vc sdy7 0" />
            //  <gd name="sy11" fmla="+- vc sdy6 0" />
            //  <gd name="sy12" fmla="+- vc sdy5 0" />
            //  <gd name="sy13" fmla="+- vc sdy4 0" />
            //  <gd name="sy14" fmla="+- vc sdy3 0" />
            //  <gd name="sy15" fmla="+- vc sdy2 0" />
            //  <gd name="sy16" fmla="+- vc sdy1 0" />
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
            //<gd name="dx1" fmla="*/ wd2 98079 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = wd2 * 98079 / 100000;
            //<gd name="dx2" fmla="*/ wd2 92388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = wd2 * 92388 / 100000;
            //<gd name="dx3" fmla="*/ wd2 83147 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx3 = wd2 * 83147 / 100000;
            //<gd name="dx4" fmla="cos wd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx4 = Cos(wd2, (int) 2700000);
            //<gd name="dx5" fmla="*/ wd2 55557 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx5 = wd2 * 55557 / 100000;
            //<gd name="dx6" fmla="*/ wd2 38268 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx6 = wd2 * 38268 / 100000;
            //<gd name="dx7" fmla="*/ wd2 19509 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx7 = wd2 * 19509 / 100000;
            //<gd name="dy1" fmla="*/ hd2 98079 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = hd2 * 98079 / 100000;
            //<gd name="dy2" fmla="*/ hd2 92388 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy2 = hd2 * 92388 / 100000;
            //<gd name="dy3" fmla="*/ hd2 83147 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy3 = hd2 * 83147 / 100000;
            //<gd name="dy4" fmla="sin hd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy4 = Sin(hd2, (int) 2700000);
            //<gd name="dy5" fmla="*/ hd2 55557 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy5 = hd2 * 55557 / 100000;
            //<gd name="dy6" fmla="*/ hd2 38268 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy6 = hd2 * 38268 / 100000;
            //<gd name="dy7" fmla="*/ hd2 19509 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy7 = hd2 * 19509 / 100000;
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
            //<gd name="x6" fmla="+- hc 0 dx6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x6 = hc + 0 - dx6;
            //<gd name="x7" fmla="+- hc 0 dx7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x7 = hc + 0 - dx7;
            //<gd name="x8" fmla="+- hc dx7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x8 = hc + dx7 - 0;
            //<gd name="x9" fmla="+- hc dx6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x9 = hc + dx6 - 0;
            //<gd name="x10" fmla="+- hc dx5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x10 = hc + dx5 - 0;
            //<gd name="x11" fmla="+- hc dx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x11 = hc + dx4 - 0;
            //<gd name="x12" fmla="+- hc dx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x12 = hc + dx3 - 0;
            //<gd name="x13" fmla="+- hc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x13 = hc + dx2 - 0;
            //<gd name="x14" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x14 = hc + dx1 - 0;
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
            //<gd name="y6" fmla="+- vc 0 dy6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y6 = vc + 0 - dy6;
            //<gd name="y7" fmla="+- vc 0 dy7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y7 = vc + 0 - dy7;
            //<gd name="y8" fmla="+- vc dy7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y8 = vc + dy7 - 0;
            //<gd name="y9" fmla="+- vc dy6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y9 = vc + dy6 - 0;
            //<gd name="y10" fmla="+- vc dy5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y10 = vc + dy5 - 0;
            //<gd name="y11" fmla="+- vc dy4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y11 = vc + dy4 - 0;
            //<gd name="y12" fmla="+- vc dy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y12 = vc + dy3 - 0;
            //<gd name="y13" fmla="+- vc dy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y13 = vc + dy2 - 0;
            //<gd name="y14" fmla="+- vc dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y14 = vc + dy1 - 0;
            //<gd name="iwd2" fmla="*/ wd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iwd2 = wd2 * a / 50000;
            //<gd name="ihd2" fmla="*/ hd2 a 50000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ihd2 = hd2 * a / 50000;
            //<gd name="sdx1" fmla="*/ iwd2 99518 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx1 = iwd2 * 99518 / 100000;
            //<gd name="sdx2" fmla="*/ iwd2 95694 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx2 = iwd2 * 95694 / 100000;
            //<gd name="sdx3" fmla="*/ iwd2 88192 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx3 = iwd2 * 88192 / 100000;
            //<gd name="sdx4" fmla="*/ iwd2 77301 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx4 = iwd2 * 77301 / 100000;
            //<gd name="sdx5" fmla="*/ iwd2 63439 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx5 = iwd2 * 63439 / 100000;
            //<gd name="sdx6" fmla="*/ iwd2 47140 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx6 = iwd2 * 47140 / 100000;
            //<gd name="sdx7" fmla="*/ iwd2 29028 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx7 = iwd2 * 29028 / 100000;
            //<gd name="sdx8" fmla="*/ iwd2 9802 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdx8 = iwd2 * 9802 / 100000;
            //<gd name="sdy1" fmla="*/ ihd2 99518 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy1 = ihd2 * 99518 / 100000;
            //<gd name="sdy2" fmla="*/ ihd2 95694 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy2 = ihd2 * 95694 / 100000;
            //<gd name="sdy3" fmla="*/ ihd2 88192 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy3 = ihd2 * 88192 / 100000;
            //<gd name="sdy4" fmla="*/ ihd2 77301 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy4 = ihd2 * 77301 / 100000;
            //<gd name="sdy5" fmla="*/ ihd2 63439 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy5 = ihd2 * 63439 / 100000;
            //<gd name="sdy6" fmla="*/ ihd2 47140 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy6 = ihd2 * 47140 / 100000;
            //<gd name="sdy7" fmla="*/ ihd2 29028 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy7 = ihd2 * 29028 / 100000;
            //<gd name="sdy8" fmla="*/ ihd2 9802 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdy8 = ihd2 * 9802 / 100000;
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
            //<gd name="sx7" fmla="+- hc 0 sdx7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx7 = hc + 0 - sdx7;
            //<gd name="sx8" fmla="+- hc 0 sdx8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx8 = hc + 0 - sdx8;
            //<gd name="sx9" fmla="+- hc sdx8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx9 = hc + sdx8 - 0;
            //<gd name="sx10" fmla="+- hc sdx7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx10 = hc + sdx7 - 0;
            //<gd name="sx11" fmla="+- hc sdx6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx11 = hc + sdx6 - 0;
            //<gd name="sx12" fmla="+- hc sdx5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx12 = hc + sdx5 - 0;
            //<gd name="sx13" fmla="+- hc sdx4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx13 = hc + sdx4 - 0;
            //<gd name="sx14" fmla="+- hc sdx3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx14 = hc + sdx3 - 0;
            //<gd name="sx15" fmla="+- hc sdx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx15 = hc + sdx2 - 0;
            //<gd name="sx16" fmla="+- hc sdx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx16 = hc + sdx1 - 0;
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
            //<gd name="sy7" fmla="+- vc 0 sdy7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy7 = vc + 0 - sdy7;
            //<gd name="sy8" fmla="+- vc 0 sdy8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy8 = vc + 0 - sdy8;
            //<gd name="sy9" fmla="+- vc sdy8 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy9 = vc + sdy8 - 0;
            //<gd name="sy10" fmla="+- vc sdy7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy10 = vc + sdy7 - 0;
            //<gd name="sy11" fmla="+- vc sdy6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy11 = vc + sdy6 - 0;
            //<gd name="sy12" fmla="+- vc sdy5 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy12 = vc + sdy5 - 0;
            //<gd name="sy13" fmla="+- vc sdy4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy13 = vc + sdy4 - 0;
            //<gd name="sy14" fmla="+- vc sdy3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy14 = vc + sdy3 - 0;
            //<gd name="sy15" fmla="+- vc sdy2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy15 = vc + sdy2 - 0;
            //<gd name="sy16" fmla="+- vc sdy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy16 = vc + sdy1 - 0;
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
            //      <pt x="sx1" y="sy8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx7" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx8" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx9" y="sy1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx10" y="sy2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx11" y="sy3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx12" y="sy4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x11" y="y4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx13" y="sy5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x12" y="y5" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx14" y="sy6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x13" y="y6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx15" y="sy7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x14" y="y7" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx16" y="sy8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx16" y="sy9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x14" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx15" y="sy10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x13" y="y9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx14" y="sy11" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x12" y="y10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx13" y="sy12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x11" y="y11" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx12" y="sy13" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x10" y="y12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx11" y="sy14" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x9" y="y13" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx10" y="sy15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x8" y="y14" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx9" y="sy16" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx8" y="sy16" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x7" y="y14" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx7" y="sy15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x6" y="y13" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx6" y="sy14" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x5" y="y12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx5" y="sy13" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x4" y="y11" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx4" y="sy12" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x3" y="y10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx3" y="sy11" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx2" y="sy10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y8" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="sx1" y="sy9" />
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
            //  <pt x="sx1" y="sy8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx7" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx7, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx8" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx8, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx9" y="sy1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx9, sy1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx10" y="sy2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx10, sy2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx11" y="sy3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx11, sy3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x10" y="y3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x10, y3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx12" y="sy4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx12, sy4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x11" y="y4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x11, y4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx13" y="sy5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx13, sy5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x12" y="y5" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x12, y5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx14" y="sy6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx14, sy6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x13" y="y6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x13, y6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx15" y="sy7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx15, sy7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x14" y="y7" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x14, y7);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx16" y="sy8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx16, sy8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx16" y="sy9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx16, sy9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x14" y="y8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x14, y8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx15" y="sy10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx15, sy10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x13" y="y9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x13, y9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx14" y="sy11" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx14, sy11);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x12" y="y10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x12, y10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx13" y="sy12" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx13, sy12);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x11" y="y11" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x11, y11);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx12" y="sy13" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx12, sy13);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x10" y="y12" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x10, y12);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx11" y="sy14" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx11, sy14);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x9" y="y13" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x9, y13);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx10" y="sy15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx10, sy15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x8" y="y14" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x8, y14);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx9" y="sy16" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx9, sy16);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx8" y="sy16" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx8, sy16);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x7" y="y14" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x7, y14);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx7" y="sy15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx7, sy15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x6" y="y13" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x6, y13);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx6" y="sy14" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx6, sy14);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x5" y="y12" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x5, y12);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx5" y="sy13" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx5, sy13);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x4" y="y11" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x4, y11);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx4" y="sy12" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx4, sy12);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x3" y="y10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x3, y10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx3" y="sy11" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx3, sy11);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx2" y="sy10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx2, sy10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y8" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y8);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="sx1" y="sy9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, sx1, sy9);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

