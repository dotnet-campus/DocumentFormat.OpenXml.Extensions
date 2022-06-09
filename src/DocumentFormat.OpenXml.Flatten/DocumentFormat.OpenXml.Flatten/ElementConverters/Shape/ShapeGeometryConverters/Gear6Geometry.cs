using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 齿轮6
    /// </summary>
    public class Gear6Geometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 15000" />
            //  <gd name="adj2" fmla="val 3526" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 15000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 3526d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 20000" />
            //  <gd name="a2" fmla="pin 0 adj2 5358" />
            //  <gd name="th" fmla="*/ ss a1 100000" />
            //  <gd name="lFD" fmla="*/ ss a2 100000" />
            //  <gd name="th2" fmla="*/ th 1 2" />
            //  <gd name="l2" fmla="*/ lFD 1 2" />
            //  <gd name="l3" fmla="+- th2 l2 0" />
            //  <gd name="rh" fmla="+- hd2 0 th" />
            //  <gd name="rw" fmla="+- wd2 0 th" />
            //  <gd name="dr" fmla="+- rw 0 rh" />
            //  <gd name="maxr" fmla="?: dr rh rw" />
            //  <gd name="ha" fmla="at2 maxr l3" />
            //  <gd name="aA1" fmla="+- 19800000 0 ha" />
            //  <gd name="aD1" fmla="+- 19800000 ha 0" />
            //  <gd name="ta11" fmla="cos rw aA1" />
            //  <gd name="ta12" fmla="sin rh aA1" />
            //  <gd name="bA1" fmla="at2 ta11 ta12" />
            //  <gd name="cta1" fmla="cos rh bA1" />
            //  <gd name="sta1" fmla="sin rw bA1" />
            //  <gd name="ma1" fmla="mod cta1 sta1 0" />
            //  <gd name="na1" fmla="*/ rw rh ma1" />
            //  <gd name="dxa1" fmla="cos na1 bA1" />
            //  <gd name="dya1" fmla="sin na1 bA1" />
            //  <gd name="xA1" fmla="+- hc dxa1 0" />
            //  <gd name="yA1" fmla="+- vc dya1 0" />
            //  <gd name="td11" fmla="cos rw aD1" />
            //  <gd name="td12" fmla="sin rh aD1" />
            //  <gd name="bD1" fmla="at2 td11 td12" />
            //  <gd name="ctd1" fmla="cos rh bD1" />
            //  <gd name="std1" fmla="sin rw bD1" />
            //  <gd name="md1" fmla="mod ctd1 std1 0" />
            //  <gd name="nd1" fmla="*/ rw rh md1" />
            //  <gd name="dxd1" fmla="cos nd1 bD1" />
            //  <gd name="dyd1" fmla="sin nd1 bD1" />
            //  <gd name="xD1" fmla="+- hc dxd1 0" />
            //  <gd name="yD1" fmla="+- vc dyd1 0" />
            //  <gd name="xAD1" fmla="+- xA1 0 xD1" />
            //  <gd name="yAD1" fmla="+- yA1 0 yD1" />
            //  <gd name="lAD1" fmla="mod xAD1 yAD1 0" />
            //  <gd name="a1" fmla="at2 yAD1 xAD1" />
            //  <gd name="dxF1" fmla="sin lFD a1" />
            //  <gd name="dyF1" fmla="cos lFD a1" />
            //  <gd name="xF1" fmla="+- xD1 dxF1 0" />
            //  <gd name="yF1" fmla="+- yD1 dyF1 0" />
            //  <gd name="xE1" fmla="+- xA1 0 dxF1" />
            //  <gd name="yE1" fmla="+- yA1 0 dyF1" />
            //  <gd name="yC1t" fmla="sin th a1" />
            //  <gd name="xC1t" fmla="cos th a1" />
            //  <gd name="yC1" fmla="+- yF1 yC1t 0" />
            //  <gd name="xC1" fmla="+- xF1 0 xC1t" />
            //  <gd name="yB1" fmla="+- yE1 yC1t 0" />
            //  <gd name="xB1" fmla="+- xE1 0 xC1t" />
            //  <gd name="aD6" fmla="+- 3cd4 ha 0" />
            //  <gd name="td61" fmla="cos rw aD6" />
            //  <gd name="td62" fmla="sin rh aD6" />
            //  <gd name="bD6" fmla="at2 td61 td62" />
            //  <gd name="ctd6" fmla="cos rh bD6" />
            //  <gd name="std6" fmla="sin rw bD6" />
            //  <gd name="md6" fmla="mod ctd6 std6 0" />
            //  <gd name="nd6" fmla="*/ rw rh md6" />
            //  <gd name="dxd6" fmla="cos nd6 bD6" />
            //  <gd name="dyd6" fmla="sin nd6 bD6" />
            //  <gd name="xD6" fmla="+- hc dxd6 0" />
            //  <gd name="yD6" fmla="+- vc dyd6 0" />
            //  <gd name="xA6" fmla="+- hc 0 dxd6" />
            //  <gd name="xF6" fmla="+- xD6 0 lFD" />
            //  <gd name="xE6" fmla="+- xA6 lFD 0" />
            //  <gd name="yC6" fmla="+- yD6 0 th" />
            //  <gd name="swAng1" fmla="+- bA1 0 bD6" />
            //  <gd name="aA2" fmla="+- 1800000 0 ha" />
            //  <gd name="aD2" fmla="+- 1800000 ha 0" />
            //  <gd name="ta21" fmla="cos rw aA2" />
            //  <gd name="ta22" fmla="sin rh aA2" />
            //  <gd name="bA2" fmla="at2 ta21 ta22" />
            //  <gd name="yA2" fmla="+- h 0 yD1" />
            //  <gd name="td21" fmla="cos rw aD2" />
            //  <gd name="td22" fmla="sin rh aD2" />
            //  <gd name="bD2" fmla="at2 td21 td22" />
            //  <gd name="yD2" fmla="+- h 0 yA1" />
            //  <gd name="yC2" fmla="+- h 0 yB1" />
            //  <gd name="yB2" fmla="+- h 0 yC1" />
            //  <gd name="xB2" fmla="val xC1" />
            //  <gd name="swAng2" fmla="+- bA2 0 bD1" />
            //  <gd name="aD3" fmla="+- cd4 ha 0" />
            //  <gd name="td31" fmla="cos rw aD3" />
            //  <gd name="td32" fmla="sin rh aD3" />
            //  <gd name="bD3" fmla="at2 td31 td32" />
            //  <gd name="yD3" fmla="+- h 0 yD6" />
            //  <gd name="yB3" fmla="+- h 0 yC6" />
            //  <gd name="aD4" fmla="+- 9000000 ha 0" />
            //  <gd name="td41" fmla="cos rw aD4" />
            //  <gd name="td42" fmla="sin rh aD4" />
            //  <gd name="bD4" fmla="at2 td41 td42" />
            //  <gd name="xD4" fmla="+- w 0 xD1" />
            //  <gd name="xC4" fmla="+- w 0 xC1" />
            //  <gd name="xB4" fmla="+- w 0 xB1" />
            //  <gd name="aD5" fmla="+- 12600000 ha 0" />
            //  <gd name="td51" fmla="cos rw aD5" />
            //  <gd name="td52" fmla="sin rh aD5" />
            //  <gd name="bD5" fmla="at2 td51 td52" />
            //  <gd name="xD5" fmla="+- w 0 xA1" />
            //  <gd name="xC5" fmla="+- w 0 xB1" />
            //  <gd name="xB5" fmla="+- w 0 xC1" />
            //  <gd name="xCxn1" fmla="+/ xB1 xC1 2" />
            //  <gd name="yCxn1" fmla="+/ yB1 yC1 2" />
            //  <gd name="yCxn2" fmla="+- b 0 yCxn1" />
            //  <gd name="xCxn4" fmla="+/ r 0 xCxn1" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 20000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 20000);
            //<gd name="a2" fmla="pin 0 adj2 5358" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(0, adj2, 5358);
            //<gd name="th" fmla="*/ ss a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var th = ss * a1 / 100000;
            //<gd name="lFD" fmla="*/ ss a2 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lFD = ss * a2 / 100000;
            //<gd name="th2" fmla="*/ th 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var th2 = th * 1 / 2;
            //<gd name="l2" fmla="*/ lFD 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var l2 = lFD * 1 / 2;
            //<gd name="l3" fmla="+- th2 l2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var l3 = th2 + l2 - 0;
            //<gd name="rh" fmla="+- hd2 0 th" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh = hd2 + 0 - th;
            //<gd name="rw" fmla="+- wd2 0 th" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw = wd2 + 0 - th;
            //<gd name="dr" fmla="+- rw 0 rh" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dr = rw + 0 - rh;
            //<gd name="maxr" fmla="?: dr rh rw" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxr = dr > 0 ? rh : rw;
            //<gd name="ha" fmla="at2 maxr l3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ha = ATan2(maxr, l3);
            //<gd name="aA1" fmla="+- 19800000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA1 = 19800000 + 0 - ha;
            //<gd name="aD1" fmla="+- 19800000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD1 = 19800000 + ha - 0;
            //<gd name="ta11" fmla="cos rw aA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta11 = Cos(rw, (int) aA1);
            //<gd name="ta12" fmla="sin rh aA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta12 = Sin(rh, (int) aA1);
            //<gd name="bA1" fmla="at2 ta11 ta12" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA1 = ATan2(ta11, ta12);
            //<gd name="cta1" fmla="cos rh bA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cta1 = Cos(rh, (int) bA1);
            //<gd name="sta1" fmla="sin rw bA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sta1 = Sin(rw, (int) bA1);
            //<gd name="ma1" fmla="mod cta1 sta1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma1 = Mod(cta1, sta1, 0);
            //<gd name="na1" fmla="*/ rw rh ma1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var na1 = rw * rh / ma1;
            //<gd name="dxa1" fmla="cos na1 bA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxa1 = Cos(na1, (int) bA1);
            //<gd name="dya1" fmla="sin na1 bA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dya1 = Sin(na1, (int) bA1);
            //<gd name="xA1" fmla="+- hc dxa1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA1 = hc + dxa1 - 0;
            //<gd name="yA1" fmla="+- vc dya1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA1 = vc + dya1 - 0;
            //<gd name="td11" fmla="cos rw aD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td11 = Cos(rw, (int) aD1);
            //<gd name="td12" fmla="sin rh aD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td12 = Sin(rh, (int) aD1);
            //<gd name="bD1" fmla="at2 td11 td12" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD1 = ATan2(td11, td12);
            //<gd name="ctd1" fmla="cos rh bD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ctd1 = Cos(rh, (int) bD1);
            //<gd name="std1" fmla="sin rw bD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var std1 = Sin(rw, (int) bD1);
            //<gd name="md1" fmla="mod ctd1 std1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var md1 = Mod(ctd1, std1, 0);
            //<gd name="nd1" fmla="*/ rw rh md1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var nd1 = rw * rh / md1;
            //<gd name="dxd1" fmla="cos nd1 bD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxd1 = Cos(nd1, (int) bD1);
            //<gd name="dyd1" fmla="sin nd1 bD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyd1 = Sin(nd1, (int) bD1);
            //<gd name="xD1" fmla="+- hc dxd1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD1 = hc + dxd1 - 0;
            //<gd name="yD1" fmla="+- vc dyd1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD1 = vc + dyd1 - 0;
            //<gd name="xAD1" fmla="+- xA1 0 xD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xAD1 = xA1 + 0 - xD1;
            //<gd name="yAD1" fmla="+- yA1 0 yD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAD1 = yA1 + 0 - yD1;
            //<gd name="lAD1" fmla="mod xAD1 yAD1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lAD1 = Mod(xAD1, yAD1, 0);
            //<gd name="a1" fmla="at2 yAD1 xAD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            a1 = ATan2(yAD1, xAD1);
            //<gd name="dxF1" fmla="sin lFD a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF1 = Sin(lFD, (int) a1);
            //<gd name="dyF1" fmla="cos lFD a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF1 = Cos(lFD, (int) a1);
            //<gd name="xF1" fmla="+- xD1 dxF1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF1 = xD1 + dxF1 - 0;
            //<gd name="yF1" fmla="+- yD1 dyF1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yF1 = yD1 + dyF1 - 0;
            //<gd name="xE1" fmla="+- xA1 0 dxF1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE1 = xA1 + 0 - dxF1;
            //<gd name="yE1" fmla="+- yA1 0 dyF1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yE1 = yA1 + 0 - dyF1;
            //<gd name="yC1t" fmla="sin th a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC1t = Sin(th, (int) a1);
            //<gd name="xC1t" fmla="cos th a1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC1t = Cos(th, (int) a1);
            //<gd name="yC1" fmla="+- yF1 yC1t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC1 = yF1 + yC1t - 0;
            //<gd name="xC1" fmla="+- xF1 0 xC1t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC1 = xF1 + 0 - xC1t;
            //<gd name="yB1" fmla="+- yE1 yC1t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB1 = yE1 + yC1t - 0;
            //<gd name="xB1" fmla="+- xE1 0 xC1t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB1 = xE1 + 0 - xC1t;
            //<gd name="aD6" fmla="+- 3cd4 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD6 = 3 * cd4 + ha - 0;
            //<gd name="td61" fmla="cos rw aD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td61 = Cos(rw, (int) aD6);
            //<gd name="td62" fmla="sin rh aD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td62 = Sin(rh, (int) aD6);
            //<gd name="bD6" fmla="at2 td61 td62" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD6 = ATan2(td61, td62);
            //<gd name="ctd6" fmla="cos rh bD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ctd6 = Cos(rh, (int) bD6);
            //<gd name="std6" fmla="sin rw bD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var std6 = Sin(rw, (int) bD6);
            //<gd name="md6" fmla="mod ctd6 std6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var md6 = Mod(ctd6, std6, 0);
            //<gd name="nd6" fmla="*/ rw rh md6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var nd6 = rw * rh / md6;
            //<gd name="dxd6" fmla="cos nd6 bD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxd6 = Cos(nd6, (int) bD6);
            //<gd name="dyd6" fmla="sin nd6 bD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyd6 = Sin(nd6, (int) bD6);
            //<gd name="xD6" fmla="+- hc dxd6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD6 = hc + dxd6 - 0;
            //<gd name="yD6" fmla="+- vc dyd6 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD6 = vc + dyd6 - 0;
            //<gd name="xA6" fmla="+- hc 0 dxd6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA6 = hc + 0 - dxd6;
            //<gd name="xF6" fmla="+- xD6 0 lFD" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF6 = xD6 + 0 - lFD;
            //<gd name="xE6" fmla="+- xA6 lFD 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE6 = xA6 + lFD - 0;
            //<gd name="yC6" fmla="+- yD6 0 th" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC6 = yD6 + 0 - th;
            //<gd name="swAng1" fmla="+- bA1 0 bD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng1 = bA1 + 0 - bD6;
            //<gd name="aA2" fmla="+- 1800000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA2 = 1800000 + 0 - ha;
            //<gd name="aD2" fmla="+- 1800000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD2 = 1800000 + ha - 0;
            //<gd name="ta21" fmla="cos rw aA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta21 = Cos(rw, (int) aA2);
            //<gd name="ta22" fmla="sin rh aA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta22 = Sin(rh, (int) aA2);
            //<gd name="bA2" fmla="at2 ta21 ta22" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA2 = ATan2(ta21, ta22);
            //<gd name="yA2" fmla="+- h 0 yD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA2 = h + 0 - yD1;
            //<gd name="td21" fmla="cos rw aD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td21 = Cos(rw, (int) aD2);
            //<gd name="td22" fmla="sin rh aD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td22 = Sin(rh, (int) aD2);
            //<gd name="bD2" fmla="at2 td21 td22" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD2 = ATan2(td21, td22);
            //<gd name="yD2" fmla="+- h 0 yA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD2 = h + 0 - yA1;
            //<gd name="yC2" fmla="+- h 0 yB1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC2 = h + 0 - yB1;
            //<gd name="yB2" fmla="+- h 0 yC1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB2 = h + 0 - yC1;
            var xB2 = xC1;
            //<gd name="swAng2" fmla="+- bA2 0 bD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng2 = bA2 + 0 - bD1;
            //<gd name="aD3" fmla="+- cd4 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD3 = cd4 + ha - 0;
            //<gd name="td31" fmla="cos rw aD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td31 = Cos(rw, (int) aD3);
            //<gd name="td32" fmla="sin rh aD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td32 = Sin(rh, (int) aD3);
            //<gd name="bD3" fmla="at2 td31 td32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD3 = ATan2(td31, td32);
            //<gd name="yD3" fmla="+- h 0 yD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD3 = h + 0 - yD6;
            //<gd name="yB3" fmla="+- h 0 yC6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB3 = h + 0 - yC6;
            //<gd name="aD4" fmla="+- 9000000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD4 = 9000000 + ha - 0;
            //<gd name="td41" fmla="cos rw aD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td41 = Cos(rw, (int) aD4);
            //<gd name="td42" fmla="sin rh aD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td42 = Sin(rh, (int) aD4);
            //<gd name="bD4" fmla="at2 td41 td42" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD4 = ATan2(td41, td42);
            //<gd name="xD4" fmla="+- w 0 xD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD4 = w + 0 - xD1;
            //<gd name="xC4" fmla="+- w 0 xC1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC4 = w + 0 - xC1;
            //<gd name="xB4" fmla="+- w 0 xB1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB4 = w + 0 - xB1;
            //<gd name="aD5" fmla="+- 12600000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD5 = 12600000 + ha - 0;
            //<gd name="td51" fmla="cos rw aD5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td51 = Cos(rw, (int) aD5);
            //<gd name="td52" fmla="sin rh aD5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td52 = Sin(rh, (int) aD5);
            //<gd name="bD5" fmla="at2 td51 td52" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD5 = ATan2(td51, td52);
            //<gd name="xD5" fmla="+- w 0 xA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD5 = w + 0 - xA1;
            //<gd name="xC5" fmla="+- w 0 xB1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC5 = w + 0 - xB1;
            //<gd name="xB5" fmla="+- w 0 xC1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB5 = w + 0 - xC1;
            //<gd name="xCxn1" fmla="+/ xB1 xC1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn1 = (xB1 + xC1) / 2;
            //<gd name="yCxn1" fmla="+/ yB1 yC1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yCxn1 = (yB1 + yC1) / 2;
            //<gd name="yCxn2" fmla="+- b 0 yCxn1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yCxn2 = b + 0 - yCxn1;
            //<gd name="xCxn4" fmla="+/ r 0 xCxn1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn4 = (r + 0) / xCxn1;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="xA1" y="yA1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="xB1" y="yB1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC1" y="yC1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD1" y="yD1" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD1" swAng="swAng2" />
            //    <lnTo>
            //      <pt x="xC1" y="yB2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xB1" y="yC2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xA1" y="yD2" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD2" swAng="swAng1" />
            //    <lnTo>
            //      <pt x="xF6" y="yB3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xE6" y="yB3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xA6" y="yD3" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD3" swAng="swAng1" />
            //    <lnTo>
            //      <pt x="xB4" y="yC2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC4" y="yB2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD4" y="yA2" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD4" swAng="swAng2" />
            //    <lnTo>
            //      <pt x="xB5" y="yC1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC5" y="yB1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD5" y="yA1" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD5" swAng="swAng1" />
            //    <lnTo>
            //      <pt x="xE6" y="yC6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xF6" y="yC6" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD6" y="yD6" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD6" swAng="swAng1" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xA1" y="yA1" />
            //</moveTo>
            var currentPoint = new EmuPoint(xA1, yA1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB1" y="yB1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB1, yB1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC1" y="yC1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC1, yC1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD1" y="yD1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD1, yD1);
            //<arcTo hR="rh" wR="rw" stAng="bD1" swAng="swAng2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD1, swAng2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC1" y="yB2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC1, yB2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB1" y="yC2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB1, yC2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xA1" y="yD2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xA1, yD2);
            //<arcTo hR="rh" wR="rw" stAng="bD2" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD2, swAng1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xF6" y="yB3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xF6, yB3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE6" y="yB3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xE6, yB3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xA6" y="yD3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xA6, yD3);
            //<arcTo hR="rh" wR="rw" stAng="bD3" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD3, swAng1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB4" y="yC2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB4, yC2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC4" y="yB2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC4, yB2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD4" y="yA2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD4, yA2);
            //<arcTo hR="rh" wR="rw" stAng="bD4" swAng="swAng2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD4, swAng2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB5" y="yC1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB5, yC1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC5" y="yB1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC5, yB1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD5" y="yA1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD5, yA1);
            //<arcTo hR="rh" wR="rw" stAng="bD5" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD5, swAng1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE6" y="yC6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xE6, yC6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xF6" y="yC6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xF6, yC6);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD6" y="yD6" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD6, yD6);
            //<arcTo hR="rh" wR="rw" stAng="bD6" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD6, swAng1);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="xD5" t="yA1" r="xA1" b="yD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(xD5, yA1, xA1, yD2);

            return shapePaths;
        }
    }


}

