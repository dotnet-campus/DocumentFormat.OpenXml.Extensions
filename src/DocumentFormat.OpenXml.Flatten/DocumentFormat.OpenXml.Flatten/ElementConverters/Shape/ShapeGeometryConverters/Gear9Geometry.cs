using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{

    /// <summary>
    /// 齿轮9
    /// </summary>
    public class Gear9Geometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 10000" />
            //  <gd name="adj2" fmla="val 1763" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 10000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 1763d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 20000" />
            //  <gd name="a2" fmla="pin 0 adj2 2679" />
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
            //  <gd name="aA1" fmla="+- 18600000 0 ha" />
            //  <gd name="aD1" fmla="+- 18600000 ha 0" />
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
            //  <gd name="aA2" fmla="+- 21000000 0 ha" />
            //  <gd name="aD2" fmla="+- 21000000 ha 0" />
            //  <gd name="ta21" fmla="cos rw aA2" />
            //  <gd name="ta22" fmla="sin rh aA2" />
            //  <gd name="bA2" fmla="at2 ta21 ta22" />
            //  <gd name="cta2" fmla="cos rh bA2" />
            //  <gd name="sta2" fmla="sin rw bA2" />
            //  <gd name="ma2" fmla="mod cta2 sta2 0" />
            //  <gd name="na2" fmla="*/ rw rh ma2" />
            //  <gd name="dxa2" fmla="cos na2 bA2" />
            //  <gd name="dya2" fmla="sin na2 bA2" />
            //  <gd name="xA2" fmla="+- hc dxa2 0" />
            //  <gd name="yA2" fmla="+- vc dya2 0" />
            //  <gd name="td21" fmla="cos rw aD2" />
            //  <gd name="td22" fmla="sin rh aD2" />
            //  <gd name="bD2" fmla="at2 td21 td22" />
            //  <gd name="ctd2" fmla="cos rh bD2" />
            //  <gd name="std2" fmla="sin rw bD2" />
            //  <gd name="md2" fmla="mod ctd2 std2 0" />
            //  <gd name="nd2" fmla="*/ rw rh md2" />
            //  <gd name="dxd2" fmla="cos nd2 bD2" />
            //  <gd name="dyd2" fmla="sin nd2 bD2" />
            //  <gd name="xD2" fmla="+- hc dxd2 0" />
            //  <gd name="yD2" fmla="+- vc dyd2 0" />
            //  <gd name="xAD2" fmla="+- xA2 0 xD2" />
            //  <gd name="yAD2" fmla="+- yA2 0 yD2" />
            //  <gd name="lAD2" fmla="mod xAD2 yAD2 0" />
            //  <gd name="a2" fmla="at2 yAD2 xAD2" />
            //  <gd name="dxF2" fmla="sin lFD a2" />
            //  <gd name="dyF2" fmla="cos lFD a2" />
            //  <gd name="xF2" fmla="+- xD2 dxF2 0" />
            //  <gd name="yF2" fmla="+- yD2 dyF2 0" />
            //  <gd name="xE2" fmla="+- xA2 0 dxF2" />
            //  <gd name="yE2" fmla="+- yA2 0 dyF2" />
            //  <gd name="yC2t" fmla="sin th a2" />
            //  <gd name="xC2t" fmla="cos th a2" />
            //  <gd name="yC2" fmla="+- yF2 yC2t 0" />
            //  <gd name="xC2" fmla="+- xF2 0 xC2t" />
            //  <gd name="yB2" fmla="+- yE2 yC2t 0" />
            //  <gd name="xB2" fmla="+- xE2 0 xC2t" />
            //  <gd name="swAng1" fmla="+- bA2 0 bD1" />
            //  <gd name="aA3" fmla="+- 1800000 0 ha" />
            //  <gd name="aD3" fmla="+- 1800000 ha 0" />
            //  <gd name="ta31" fmla="cos rw aA3" />
            //  <gd name="ta32" fmla="sin rh aA3" />
            //  <gd name="bA3" fmla="at2 ta31 ta32" />
            //  <gd name="cta3" fmla="cos rh bA3" />
            //  <gd name="sta3" fmla="sin rw bA3" />
            //  <gd name="ma3" fmla="mod cta3 sta3 0" />
            //  <gd name="na3" fmla="*/ rw rh ma3" />
            //  <gd name="dxa3" fmla="cos na3 bA3" />
            //  <gd name="dya3" fmla="sin na3 bA3" />
            //  <gd name="xA3" fmla="+- hc dxa3 0" />
            //  <gd name="yA3" fmla="+- vc dya3 0" />
            //  <gd name="td31" fmla="cos rw aD3" />
            //  <gd name="td32" fmla="sin rh aD3" />
            //  <gd name="bD3" fmla="at2 td31 td32" />
            //  <gd name="ctd3" fmla="cos rh bD3" />
            //  <gd name="std3" fmla="sin rw bD3" />
            //  <gd name="md3" fmla="mod ctd3 std3 0" />
            //  <gd name="nd3" fmla="*/ rw rh md3" />
            //  <gd name="dxd3" fmla="cos nd3 bD3" />
            //  <gd name="dyd3" fmla="sin nd3 bD3" />
            //  <gd name="xD3" fmla="+- hc dxd3 0" />
            //  <gd name="yD3" fmla="+- vc dyd3 0" />
            //  <gd name="xAD3" fmla="+- xA3 0 xD3" />
            //  <gd name="yAD3" fmla="+- yA3 0 yD3" />
            //  <gd name="lAD3" fmla="mod xAD3 yAD3 0" />
            //  <gd name="a3" fmla="at2 yAD3 xAD3" />
            //  <gd name="dxF3" fmla="sin lFD a3" />
            //  <gd name="dyF3" fmla="cos lFD a3" />
            //  <gd name="xF3" fmla="+- xD3 dxF3 0" />
            //  <gd name="yF3" fmla="+- yD3 dyF3 0" />
            //  <gd name="xE3" fmla="+- xA3 0 dxF3" />
            //  <gd name="yE3" fmla="+- yA3 0 dyF3" />
            //  <gd name="yC3t" fmla="sin th a3" />
            //  <gd name="xC3t" fmla="cos th a3" />
            //  <gd name="yC3" fmla="+- yF3 yC3t 0" />
            //  <gd name="xC3" fmla="+- xF3 0 xC3t" />
            //  <gd name="yB3" fmla="+- yE3 yC3t 0" />
            //  <gd name="xB3" fmla="+- xE3 0 xC3t" />
            //  <gd name="swAng2" fmla="+- bA3 0 bD2" />
            //  <gd name="aA4" fmla="+- 4200000 0 ha" />
            //  <gd name="aD4" fmla="+- 4200000 ha 0" />
            //  <gd name="ta41" fmla="cos rw aA4" />
            //  <gd name="ta42" fmla="sin rh aA4" />
            //  <gd name="bA4" fmla="at2 ta41 ta42" />
            //  <gd name="cta4" fmla="cos rh bA4" />
            //  <gd name="sta4" fmla="sin rw bA4" />
            //  <gd name="ma4" fmla="mod cta4 sta4 0" />
            //  <gd name="na4" fmla="*/ rw rh ma4" />
            //  <gd name="dxa4" fmla="cos na4 bA4" />
            //  <gd name="dya4" fmla="sin na4 bA4" />
            //  <gd name="xA4" fmla="+- hc dxa4 0" />
            //  <gd name="yA4" fmla="+- vc dya4 0" />
            //  <gd name="td41" fmla="cos rw aD4" />
            //  <gd name="td42" fmla="sin rh aD4" />
            //  <gd name="bD4" fmla="at2 td41 td42" />
            //  <gd name="ctd4" fmla="cos rh bD4" />
            //  <gd name="std4" fmla="sin rw bD4" />
            //  <gd name="md4" fmla="mod ctd4 std4 0" />
            //  <gd name="nd4" fmla="*/ rw rh md4" />
            //  <gd name="dxd4" fmla="cos nd4 bD4" />
            //  <gd name="dyd4" fmla="sin nd4 bD4" />
            //  <gd name="xD4" fmla="+- hc dxd4 0" />
            //  <gd name="yD4" fmla="+- vc dyd4 0" />
            //  <gd name="xAD4" fmla="+- xA4 0 xD4" />
            //  <gd name="yAD4" fmla="+- yA4 0 yD4" />
            //  <gd name="lAD4" fmla="mod xAD4 yAD4 0" />
            //  <gd name="a4" fmla="at2 yAD4 xAD4" />
            //  <gd name="dxF4" fmla="sin lFD a4" />
            //  <gd name="dyF4" fmla="cos lFD a4" />
            //  <gd name="xF4" fmla="+- xD4 dxF4 0" />
            //  <gd name="yF4" fmla="+- yD4 dyF4 0" />
            //  <gd name="xE4" fmla="+- xA4 0 dxF4" />
            //  <gd name="yE4" fmla="+- yA4 0 dyF4" />
            //  <gd name="yC4t" fmla="sin th a4" />
            //  <gd name="xC4t" fmla="cos th a4" />
            //  <gd name="yC4" fmla="+- yF4 yC4t 0" />
            //  <gd name="xC4" fmla="+- xF4 0 xC4t" />
            //  <gd name="yB4" fmla="+- yE4 yC4t 0" />
            //  <gd name="xB4" fmla="+- xE4 0 xC4t" />
            //  <gd name="swAng3" fmla="+- bA4 0 bD3" />
            //  <gd name="aA5" fmla="+- 6600000 0 ha" />
            //  <gd name="aD5" fmla="+- 6600000 ha 0" />
            //  <gd name="ta51" fmla="cos rw aA5" />
            //  <gd name="ta52" fmla="sin rh aA5" />
            //  <gd name="bA5" fmla="at2 ta51 ta52" />
            //  <gd name="td51" fmla="cos rw aD5" />
            //  <gd name="td52" fmla="sin rh aD5" />
            //  <gd name="bD5" fmla="at2 td51 td52" />
            //  <gd name="xD5" fmla="+- w 0 xA4" />
            //  <gd name="xC5" fmla="+- w 0 xB4" />
            //  <gd name="xB5" fmla="+- w 0 xC4" />
            //  <gd name="swAng4" fmla="+- bA5 0 bD4" />
            //  <gd name="aD6" fmla="+- 9000000 ha 0" />
            //  <gd name="td61" fmla="cos rw aD6" />
            //  <gd name="td62" fmla="sin rh aD6" />
            //  <gd name="bD6" fmla="at2 td61 td62" />
            //  <gd name="xD6" fmla="+- w 0 xA3" />
            //  <gd name="xC6" fmla="+- w 0 xB3" />
            //  <gd name="xB6" fmla="+- w 0 xC3" />
            //  <gd name="aD7" fmla="+- 11400000 ha 0" />
            //  <gd name="td71" fmla="cos rw aD7" />
            //  <gd name="td72" fmla="sin rh aD7" />
            //  <gd name="bD7" fmla="at2 td71 td72" />
            //  <gd name="xD7" fmla="+- w 0 xA2" />
            //  <gd name="xC7" fmla="+- w 0 xB2" />
            //  <gd name="xB7" fmla="+- w 0 xC2" />
            //  <gd name="aD8" fmla="+- 13800000 ha 0" />
            //  <gd name="td81" fmla="cos rw aD8" />
            //  <gd name="td82" fmla="sin rh aD8" />
            //  <gd name="bD8" fmla="at2 td81 td82" />
            //  <gd name="xA8" fmla="+- w 0 xD1" />
            //  <gd name="xD8" fmla="+- w 0 xA1" />
            //  <gd name="xC8" fmla="+- w 0 xB1" />
            //  <gd name="xB8" fmla="+- w 0 xC1" />
            //  <gd name="aA9" fmla="+- 3cd4 0 ha" />
            //  <gd name="aD9" fmla="+- 3cd4 ha 0" />
            //  <gd name="td91" fmla="cos rw aD9" />
            //  <gd name="td92" fmla="sin rh aD9" />
            //  <gd name="bD9" fmla="at2 td91 td92" />
            //  <gd name="ctd9" fmla="cos rh bD9" />
            //  <gd name="std9" fmla="sin rw bD9" />
            //  <gd name="md9" fmla="mod ctd9 std9 0" />
            //  <gd name="nd9" fmla="*/ rw rh md9" />
            //  <gd name="dxd9" fmla="cos nd9 bD9" />
            //  <gd name="dyd9" fmla="sin nd9 bD9" />
            //  <gd name="xD9" fmla="+- hc dxd9 0" />
            //  <gd name="yD9" fmla="+- vc dyd9 0" />
            //  <gd name="ta91" fmla="cos rw aA9" />
            //  <gd name="ta92" fmla="sin rh aA9" />
            //  <gd name="bA9" fmla="at2 ta91 ta92" />
            //  <gd name="xA9" fmla="+- hc 0 dxd9" />
            //  <gd name="xF9" fmla="+- xD9 0 lFD" />
            //  <gd name="xE9" fmla="+- xA9 lFD 0" />
            //  <gd name="yC9" fmla="+- yD9 0 th" />
            //  <gd name="swAng5" fmla="+- bA9 0 bD8" />
            //  <gd name="xCxn1" fmla="+/ xB1 xC1 2" />
            //  <gd name="yCxn1" fmla="+/ yB1 yC1 2" />
            //  <gd name="xCxn2" fmla="+/ xB2 xC2 2" />
            //  <gd name="yCxn2" fmla="+/ yB2 yC2 2" />
            //  <gd name="xCxn3" fmla="+/ xB3 xC3 2" />
            //  <gd name="yCxn3" fmla="+/ yB3 yC3 2" />
            //  <gd name="xCxn4" fmla="+/ xB4 xC4 2" />
            //  <gd name="yCxn4" fmla="+/ yB4 yC4 2" />
            //  <gd name="xCxn5" fmla="+/ r 0 xCxn4" />
            //  <gd name="xCxn6" fmla="+/ r 0 xCxn3" />
            //  <gd name="xCxn7" fmla="+/ r 0 xCxn2" />
            //  <gd name="xCxn8" fmla="+/ r 0 xCxn1" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 20000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 20000);
            //<gd name="a2" fmla="pin 0 adj2 2679" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = Pin(0, adj2, 2679);
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
            //<gd name="aA1" fmla="+- 18600000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA1 = 18600000 + 0 - ha;
            //<gd name="aD1" fmla="+- 18600000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD1 = 18600000 + ha - 0;
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
            //<gd name="aA2" fmla="+- 21000000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA2 = 21000000 + 0 - ha;
            //<gd name="aD2" fmla="+- 21000000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD2 = 21000000 + ha - 0;
            //<gd name="ta21" fmla="cos rw aA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta21 = Cos(rw, (int) aA2);
            //<gd name="ta22" fmla="sin rh aA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta22 = Sin(rh, (int) aA2);
            //<gd name="bA2" fmla="at2 ta21 ta22" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA2 = ATan2(ta21, ta22);
            //<gd name="cta2" fmla="cos rh bA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cta2 = Cos(rh, (int) bA2);
            //<gd name="sta2" fmla="sin rw bA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sta2 = Sin(rw, (int) bA2);
            //<gd name="ma2" fmla="mod cta2 sta2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma2 = Mod(cta2, sta2, 0);
            //<gd name="na2" fmla="*/ rw rh ma2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var na2 = rw * rh / ma2;
            //<gd name="dxa2" fmla="cos na2 bA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxa2 = Cos(na2, (int) bA2);
            //<gd name="dya2" fmla="sin na2 bA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dya2 = Sin(na2, (int) bA2);
            //<gd name="xA2" fmla="+- hc dxa2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA2 = hc + dxa2 - 0;
            //<gd name="yA2" fmla="+- vc dya2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA2 = vc + dya2 - 0;
            //<gd name="td21" fmla="cos rw aD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td21 = Cos(rw, (int) aD2);
            //<gd name="td22" fmla="sin rh aD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td22 = Sin(rh, (int) aD2);
            //<gd name="bD2" fmla="at2 td21 td22" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD2 = ATan2(td21, td22);
            //<gd name="ctd2" fmla="cos rh bD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ctd2 = Cos(rh, (int) bD2);
            //<gd name="std2" fmla="sin rw bD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var std2 = Sin(rw, (int) bD2);
            //<gd name="md2" fmla="mod ctd2 std2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var md2 = Mod(ctd2, std2, 0);
            //<gd name="nd2" fmla="*/ rw rh md2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var nd2 = rw * rh / md2;
            //<gd name="dxd2" fmla="cos nd2 bD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxd2 = Cos(nd2, (int) bD2);
            //<gd name="dyd2" fmla="sin nd2 bD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyd2 = Sin(nd2, (int) bD2);
            //<gd name="xD2" fmla="+- hc dxd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD2 = hc + dxd2 - 0;
            //<gd name="yD2" fmla="+- vc dyd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD2 = vc + dyd2 - 0;
            //<gd name="xAD2" fmla="+- xA2 0 xD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xAD2 = xA2 + 0 - xD2;
            //<gd name="yAD2" fmla="+- yA2 0 yD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAD2 = yA2 + 0 - yD2;
            //<gd name="lAD2" fmla="mod xAD2 yAD2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lAD2 = Mod(xAD2, yAD2, 0);
            //<gd name="a2" fmla="at2 yAD2 xAD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            a2 = ATan2(yAD2, xAD2);
            //<gd name="dxF2" fmla="sin lFD a2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF2 = Sin(lFD, (int) a2);
            //<gd name="dyF2" fmla="cos lFD a2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF2 = Cos(lFD, (int) a2);
            //<gd name="xF2" fmla="+- xD2 dxF2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF2 = xD2 + dxF2 - 0;
            //<gd name="yF2" fmla="+- yD2 dyF2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yF2 = yD2 + dyF2 - 0;
            //<gd name="xE2" fmla="+- xA2 0 dxF2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE2 = xA2 + 0 - dxF2;
            //<gd name="yE2" fmla="+- yA2 0 dyF2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yE2 = yA2 + 0 - dyF2;
            //<gd name="yC2t" fmla="sin th a2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC2t = Sin(th, (int) a2);
            //<gd name="xC2t" fmla="cos th a2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC2t = Cos(th, (int) a2);
            //<gd name="yC2" fmla="+- yF2 yC2t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC2 = yF2 + yC2t - 0;
            //<gd name="xC2" fmla="+- xF2 0 xC2t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC2 = xF2 + 0 - xC2t;
            //<gd name="yB2" fmla="+- yE2 yC2t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB2 = yE2 + yC2t - 0;
            //<gd name="xB2" fmla="+- xE2 0 xC2t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB2 = xE2 + 0 - xC2t;
            //<gd name="swAng1" fmla="+- bA2 0 bD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng1 = bA2 + 0 - bD1;
            //<gd name="aA3" fmla="+- 1800000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA3 = 1800000 + 0 - ha;
            //<gd name="aD3" fmla="+- 1800000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD3 = 1800000 + ha - 0;
            //<gd name="ta31" fmla="cos rw aA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta31 = Cos(rw, (int) aA3);
            //<gd name="ta32" fmla="sin rh aA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta32 = Sin(rh, (int) aA3);
            //<gd name="bA3" fmla="at2 ta31 ta32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA3 = ATan2(ta31, ta32);
            //<gd name="cta3" fmla="cos rh bA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cta3 = Cos(rh, (int) bA3);
            //<gd name="sta3" fmla="sin rw bA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sta3 = Sin(rw, (int) bA3);
            //<gd name="ma3" fmla="mod cta3 sta3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma3 = Mod(cta3, sta3, 0);
            //<gd name="na3" fmla="*/ rw rh ma3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var na3 = rw * rh / ma3;
            //<gd name="dxa3" fmla="cos na3 bA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxa3 = Cos(na3, (int) bA3);
            //<gd name="dya3" fmla="sin na3 bA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dya3 = Sin(na3, (int) bA3);
            //<gd name="xA3" fmla="+- hc dxa3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA3 = hc + dxa3 - 0;
            //<gd name="yA3" fmla="+- vc dya3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA3 = vc + dya3 - 0;
            //<gd name="td31" fmla="cos rw aD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td31 = Cos(rw, (int) aD3);
            //<gd name="td32" fmla="sin rh aD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td32 = Sin(rh, (int) aD3);
            //<gd name="bD3" fmla="at2 td31 td32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD3 = ATan2(td31, td32);
            //<gd name="ctd3" fmla="cos rh bD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ctd3 = Cos(rh, (int) bD3);
            //<gd name="std3" fmla="sin rw bD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var std3 = Sin(rw, (int) bD3);
            //<gd name="md3" fmla="mod ctd3 std3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var md3 = Mod(ctd3, std3, 0);
            //<gd name="nd3" fmla="*/ rw rh md3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var nd3 = rw * rh / md3;
            //<gd name="dxd3" fmla="cos nd3 bD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxd3 = Cos(nd3, (int) bD3);
            //<gd name="dyd3" fmla="sin nd3 bD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyd3 = Sin(nd3, (int) bD3);
            //<gd name="xD3" fmla="+- hc dxd3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD3 = hc + dxd3 - 0;
            //<gd name="yD3" fmla="+- vc dyd3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD3 = vc + dyd3 - 0;
            //<gd name="xAD3" fmla="+- xA3 0 xD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xAD3 = xA3 + 0 - xD3;
            //<gd name="yAD3" fmla="+- yA3 0 yD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAD3 = yA3 + 0 - yD3;
            //<gd name="lAD3" fmla="mod xAD3 yAD3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lAD3 = Mod(xAD3, yAD3, 0);
            //<gd name="a3" fmla="at2 yAD3 xAD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a3 = ATan2(yAD3, xAD3);
            //<gd name="dxF3" fmla="sin lFD a3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF3 = Sin(lFD, (int) a3);
            //<gd name="dyF3" fmla="cos lFD a3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF3 = Cos(lFD, (int) a3);
            //<gd name="xF3" fmla="+- xD3 dxF3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF3 = xD3 + dxF3 - 0;
            //<gd name="yF3" fmla="+- yD3 dyF3 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yF3 = yD3 + dyF3 - 0;
            //<gd name="xE3" fmla="+- xA3 0 dxF3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE3 = xA3 + 0 - dxF3;
            //<gd name="yE3" fmla="+- yA3 0 dyF3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yE3 = yA3 + 0 - dyF3;
            //<gd name="yC3t" fmla="sin th a3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC3t = Sin(th, (int) a3);
            //<gd name="xC3t" fmla="cos th a3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC3t = Cos(th, (int) a3);
            //<gd name="yC3" fmla="+- yF3 yC3t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC3 = yF3 + yC3t - 0;
            //<gd name="xC3" fmla="+- xF3 0 xC3t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC3 = xF3 + 0 - xC3t;
            //<gd name="yB3" fmla="+- yE3 yC3t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB3 = yE3 + yC3t - 0;
            //<gd name="xB3" fmla="+- xE3 0 xC3t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB3 = xE3 + 0 - xC3t;
            //<gd name="swAng2" fmla="+- bA3 0 bD2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng2 = bA3 + 0 - bD2;
            //<gd name="aA4" fmla="+- 4200000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA4 = 4200000 + 0 - ha;
            //<gd name="aD4" fmla="+- 4200000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD4 = 4200000 + ha - 0;
            //<gd name="ta41" fmla="cos rw aA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta41 = Cos(rw, (int) aA4);
            //<gd name="ta42" fmla="sin rh aA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta42 = Sin(rh, (int) aA4);
            //<gd name="bA4" fmla="at2 ta41 ta42" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA4 = ATan2(ta41, ta42);
            //<gd name="cta4" fmla="cos rh bA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var cta4 = Cos(rh, (int) bA4);
            //<gd name="sta4" fmla="sin rw bA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sta4 = Sin(rw, (int) bA4);
            //<gd name="ma4" fmla="mod cta4 sta4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ma4 = Mod(cta4, sta4, 0);
            //<gd name="na4" fmla="*/ rw rh ma4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var na4 = rw * rh / ma4;
            //<gd name="dxa4" fmla="cos na4 bA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxa4 = Cos(na4, (int) bA4);
            //<gd name="dya4" fmla="sin na4 bA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dya4 = Sin(na4, (int) bA4);
            //<gd name="xA4" fmla="+- hc dxa4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA4 = hc + dxa4 - 0;
            //<gd name="yA4" fmla="+- vc dya4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA4 = vc + dya4 - 0;
            //<gd name="td41" fmla="cos rw aD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td41 = Cos(rw, (int) aD4);
            //<gd name="td42" fmla="sin rh aD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td42 = Sin(rh, (int) aD4);
            //<gd name="bD4" fmla="at2 td41 td42" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD4 = ATan2(td41, td42);
            //<gd name="ctd4" fmla="cos rh bD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ctd4 = Cos(rh, (int) bD4);
            //<gd name="std4" fmla="sin rw bD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var std4 = Sin(rw, (int) bD4);
            //<gd name="md4" fmla="mod ctd4 std4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var md4 = Mod(ctd4, std4, 0);
            //<gd name="nd4" fmla="*/ rw rh md4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var nd4 = rw * rh / md4;
            //<gd name="dxd4" fmla="cos nd4 bD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxd4 = Cos(nd4, (int) bD4);
            //<gd name="dyd4" fmla="sin nd4 bD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyd4 = Sin(nd4, (int) bD4);
            //<gd name="xD4" fmla="+- hc dxd4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD4 = hc + dxd4 - 0;
            //<gd name="yD4" fmla="+- vc dyd4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD4 = vc + dyd4 - 0;
            //<gd name="xAD4" fmla="+- xA4 0 xD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xAD4 = xA4 + 0 - xD4;
            //<gd name="yAD4" fmla="+- yA4 0 yD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yAD4 = yA4 + 0 - yD4;
            //<gd name="lAD4" fmla="mod xAD4 yAD4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lAD4 = Mod(xAD4, yAD4, 0);
            //<gd name="a4" fmla="at2 yAD4 xAD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a4 = ATan2(yAD4, xAD4);
            //<gd name="dxF4" fmla="sin lFD a4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF4 = Sin(lFD, (int) a4);
            //<gd name="dyF4" fmla="cos lFD a4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF4 = Cos(lFD, (int) a4);
            //<gd name="xF4" fmla="+- xD4 dxF4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF4 = xD4 + dxF4 - 0;
            //<gd name="yF4" fmla="+- yD4 dyF4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yF4 = yD4 + dyF4 - 0;
            //<gd name="xE4" fmla="+- xA4 0 dxF4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE4 = xA4 + 0 - dxF4;
            //<gd name="yE4" fmla="+- yA4 0 dyF4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yE4 = yA4 + 0 - dyF4;
            //<gd name="yC4t" fmla="sin th a4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC4t = Sin(th, (int) a4);
            //<gd name="xC4t" fmla="cos th a4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC4t = Cos(th, (int) a4);
            //<gd name="yC4" fmla="+- yF4 yC4t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC4 = yF4 + yC4t - 0;
            //<gd name="xC4" fmla="+- xF4 0 xC4t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC4 = xF4 + 0 - xC4t;
            //<gd name="yB4" fmla="+- yE4 yC4t 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB4 = yE4 + yC4t - 0;
            //<gd name="xB4" fmla="+- xE4 0 xC4t" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB4 = xE4 + 0 - xC4t;
            //<gd name="swAng3" fmla="+- bA4 0 bD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng3 = bA4 + 0 - bD3;
            //<gd name="aA5" fmla="+- 6600000 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA5 = 6600000 + 0 - ha;
            //<gd name="aD5" fmla="+- 6600000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD5 = 6600000 + ha - 0;
            //<gd name="ta51" fmla="cos rw aA5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta51 = Cos(rw, (int) aA5);
            //<gd name="ta52" fmla="sin rh aA5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta52 = Sin(rh, (int) aA5);
            //<gd name="bA5" fmla="at2 ta51 ta52" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA5 = ATan2(ta51, ta52);
            //<gd name="td51" fmla="cos rw aD5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td51 = Cos(rw, (int) aD5);
            //<gd name="td52" fmla="sin rh aD5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td52 = Sin(rh, (int) aD5);
            //<gd name="bD5" fmla="at2 td51 td52" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD5 = ATan2(td51, td52);
            //<gd name="xD5" fmla="+- w 0 xA4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD5 = w + 0 - xA4;
            //<gd name="xC5" fmla="+- w 0 xB4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC5 = w + 0 - xB4;
            //<gd name="xB5" fmla="+- w 0 xC4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB5 = w + 0 - xC4;
            //<gd name="swAng4" fmla="+- bA5 0 bD4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng4 = bA5 + 0 - bD4;
            //<gd name="aD6" fmla="+- 9000000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD6 = 9000000 + ha - 0;
            //<gd name="td61" fmla="cos rw aD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td61 = Cos(rw, (int) aD6);
            //<gd name="td62" fmla="sin rh aD6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td62 = Sin(rh, (int) aD6);
            //<gd name="bD6" fmla="at2 td61 td62" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD6 = ATan2(td61, td62);
            //<gd name="xD6" fmla="+- w 0 xA3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD6 = w + 0 - xA3;
            //<gd name="xC6" fmla="+- w 0 xB3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC6 = w + 0 - xB3;
            //<gd name="xB6" fmla="+- w 0 xC3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB6 = w + 0 - xC3;
            //<gd name="aD7" fmla="+- 11400000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD7 = 11400000 + ha - 0;
            //<gd name="td71" fmla="cos rw aD7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td71 = Cos(rw, (int) aD7);
            //<gd name="td72" fmla="sin rh aD7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td72 = Sin(rh, (int) aD7);
            //<gd name="bD7" fmla="at2 td71 td72" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD7 = ATan2(td71, td72);
            //<gd name="xD7" fmla="+- w 0 xA2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD7 = w + 0 - xA2;
            //<gd name="xC7" fmla="+- w 0 xB2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC7 = w + 0 - xB2;
            //<gd name="xB7" fmla="+- w 0 xC2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB7 = w + 0 - xC2;
            //<gd name="aD8" fmla="+- 13800000 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD8 = 13800000 + ha - 0;
            //<gd name="td81" fmla="cos rw aD8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td81 = Cos(rw, (int) aD8);
            //<gd name="td82" fmla="sin rh aD8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td82 = Sin(rh, (int) aD8);
            //<gd name="bD8" fmla="at2 td81 td82" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD8 = ATan2(td81, td82);
            //<gd name="xA8" fmla="+- w 0 xD1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA8 = w + 0 - xD1;
            //<gd name="xD8" fmla="+- w 0 xA1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD8 = w + 0 - xA1;
            //<gd name="xC8" fmla="+- w 0 xB1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC8 = w + 0 - xB1;
            //<gd name="xB8" fmla="+- w 0 xC1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB8 = w + 0 - xC1;
            //<gd name="aA9" fmla="+- 3cd4 0 ha" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA9 = 3 * cd4 + 0 - ha;
            //<gd name="aD9" fmla="+- 3cd4 ha 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aD9 = 3 * cd4 + ha - 0;
            //<gd name="td91" fmla="cos rw aD9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td91 = Cos(rw, (int) aD9);
            //<gd name="td92" fmla="sin rh aD9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var td92 = Sin(rh, (int) aD9);
            //<gd name="bD9" fmla="at2 td91 td92" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bD9 = ATan2(td91, td92);
            //<gd name="ctd9" fmla="cos rh bD9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ctd9 = Cos(rh, (int) bD9);
            //<gd name="std9" fmla="sin rw bD9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var std9 = Sin(rw, (int) bD9);
            //<gd name="md9" fmla="mod ctd9 std9 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var md9 = Mod(ctd9, std9, 0);
            //<gd name="nd9" fmla="*/ rw rh md9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var nd9 = rw * rh / md9;
            //<gd name="dxd9" fmla="cos nd9 bD9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxd9 = Cos(nd9, (int) bD9);
            //<gd name="dyd9" fmla="sin nd9 bD9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyd9 = Sin(nd9, (int) bD9);
            //<gd name="xD9" fmla="+- hc dxd9 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD9 = hc + dxd9 - 0;
            //<gd name="yD9" fmla="+- vc dyd9 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD9 = vc + dyd9 - 0;
            //<gd name="ta91" fmla="cos rw aA9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta91 = Cos(rw, (int) aA9);
            //<gd name="ta92" fmla="sin rh aA9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta92 = Sin(rh, (int) aA9);
            //<gd name="bA9" fmla="at2 ta91 ta92" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var bA9 = ATan2(ta91, ta92);
            //<gd name="xA9" fmla="+- hc 0 dxd9" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA9 = hc + 0 - dxd9;
            //<gd name="xF9" fmla="+- xD9 0 lFD" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF9 = xD9 + 0 - lFD;
            //<gd name="xE9" fmla="+- xA9 lFD 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE9 = xA9 + lFD - 0;
            //<gd name="yC9" fmla="+- yD9 0 th" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC9 = yD9 + 0 - th;
            //<gd name="swAng5" fmla="+- bA9 0 bD8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng5 = bA9 + 0 - bD8;
            //<gd name="xCxn1" fmla="+/ xB1 xC1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn1 = (xB1 + xC1) / 2;
            //<gd name="yCxn1" fmla="+/ yB1 yC1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yCxn1 = (yB1 + yC1) / 2;
            //<gd name="xCxn2" fmla="+/ xB2 xC2 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn2 = (xB2 + xC2) / 2;
            //<gd name="yCxn2" fmla="+/ yB2 yC2 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yCxn2 = (yB2 + yC2) / 2;
            //<gd name="xCxn3" fmla="+/ xB3 xC3 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn3 = (xB3 + xC3) / 2;
            //<gd name="yCxn3" fmla="+/ yB3 yC3 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yCxn3 = (yB3 + yC3) / 2;
            //<gd name="xCxn4" fmla="+/ xB4 xC4 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn4 = (xB4 + xC4) / 2;
            //<gd name="yCxn4" fmla="+/ yB4 yC4 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yCxn4 = (yB4 + yC4) / 2;
            //<gd name="xCxn5" fmla="+/ r 0 xCxn4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn5 = (r + 0) / xCxn4;
            //<gd name="xCxn6" fmla="+/ r 0 xCxn3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn6 = (r + 0) / xCxn3;
            //<gd name="xCxn7" fmla="+/ r 0 xCxn2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn7 = (r + 0) / xCxn2;
            //<gd name="xCxn8" fmla="+/ r 0 xCxn1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xCxn8 = (r + 0) / xCxn1;

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
            //    <arcTo hR="rh" wR="rw" stAng="bD1" swAng="swAng1" />
            //    <lnTo>
            //      <pt x="xB2" y="yB2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC2" y="yC2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD2" y="yD2" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD2" swAng="swAng2" />
            //    <lnTo>
            //      <pt x="xB3" y="yB3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC3" y="yC3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD3" y="yD3" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD3" swAng="swAng3" />
            //    <lnTo>
            //      <pt x="xB4" y="yB4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC4" y="yC4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD4" y="yD4" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD4" swAng="swAng4" />
            //    <lnTo>
            //      <pt x="xB5" y="yC4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC5" y="yB4" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD5" y="yA4" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD5" swAng="swAng3" />
            //    <lnTo>
            //      <pt x="xB6" y="yC3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC6" y="yB3" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD6" y="yA3" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD6" swAng="swAng2" />
            //    <lnTo>
            //      <pt x="xB7" y="yC2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC7" y="yB2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD7" y="yA2" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD7" swAng="swAng1" />
            //    <lnTo>
            //      <pt x="xB8" y="yC1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC8" y="yB1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD8" y="yA1" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD8" swAng="swAng5" />
            //    <lnTo>
            //      <pt x="xE9" y="yC9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xF9" y="yC9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD9" y="yD9" />
            //    </lnTo>
            //    <arcTo hR="rh" wR="rw" stAng="bD9" swAng="swAng5" />
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
            //<arcTo hR="rh" wR="rw" stAng="bD1" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD1, swAng1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB2" y="yB2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB2, yB2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC2" y="yC2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC2, yC2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD2" y="yD2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD2, yD2);
            //<arcTo hR="rh" wR="rw" stAng="bD2" swAng="swAng2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD2, swAng2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB3" y="yB3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB3, yB3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC3" y="yC3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC3, yC3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD3" y="yD3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD3, yD3);
            //<arcTo hR="rh" wR="rw" stAng="bD3" swAng="swAng3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD3, swAng3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB4" y="yB4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB4, yB4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC4" y="yC4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC4, yC4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD4" y="yD4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD4, yD4);
            //<arcTo hR="rh" wR="rw" stAng="bD4" swAng="swAng4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD4, swAng4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB5" y="yC4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB5, yC4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC5" y="yB4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC5, yB4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD5" y="yA4" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD5, yA4);
            //<arcTo hR="rh" wR="rw" stAng="bD5" swAng="swAng3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD5, swAng3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB6" y="yC3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB6, yC3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC6" y="yB3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC6, yB3);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD6" y="yA3" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD6, yA3);
            //<arcTo hR="rh" wR="rw" stAng="bD6" swAng="swAng2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD6, swAng2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB7" y="yC2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB7, yC2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC7" y="yB2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC7, yB2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD7" y="yA2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD7, yA2);
            //<arcTo hR="rh" wR="rw" stAng="bD7" swAng="swAng1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD7, swAng1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB8" y="yC1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB8, yC1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xC8" y="yB1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xC8, yB1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD8" y="yA1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD8, yA1);
            //<arcTo hR="rh" wR="rw" stAng="bD8" swAng="swAng5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD8, swAng5);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE9" y="yC9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xE9, yC9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xF9" y="yC9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xF9, yC9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD9" y="yD9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD9, yD9);
            //<arcTo hR="rh" wR="rw" stAng="bD9" swAng="swAng5" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw, rh, bD9, swAng5);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="xA8" t="yD1" r="xD1" b="yD3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(xA8, yD1, xD1, yD3);

            return shapePaths;
        }
    }


}

