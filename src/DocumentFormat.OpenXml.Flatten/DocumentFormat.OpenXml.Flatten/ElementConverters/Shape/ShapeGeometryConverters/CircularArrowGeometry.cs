using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 箭头: 环形
    /// </summary>
    public class CircularArrowGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //        <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 12500" />
            //  <gd name="adj2" fmla="val 1142319" />
            //  <gd name="adj3" fmla="val 20457681" />
            //  <gd name="adj4" fmla="val 10800000" />
            //  <gd name="adj5" fmla="val 12500" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 12500d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 1142319d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 20457681d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? 10800000d;
            var customAdj5 = adjusts?.GetAdjustValue("adj5");
            var adj5 = customAdj5 ?? 12500d;


            //当adj1和adj5<=0时，会导致公式除法分母为0，导致路径绘制失效
            if (adj1 <= 0)
            {
                adj1 = 0.1;
            }
            if (adj5 <= 0)
            {
                adj5 = 0.1;
            }

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a5" fmla="pin 0 adj5 25000" />
            //  <gd name="maxAdj1" fmla="*/ a5 2 1" />
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            //  <gd name="enAng" fmla="pin 1 adj3 21599999" />
            //  <gd name="stAng" fmla="pin 0 adj4 21599999" />
            //  <gd name="th" fmla="*/ ss a1 100000" />
            //  <gd name="thh" fmla="*/ ss a5 100000" />
            //  <gd name="th2" fmla="*/ th 1 2" />
            //  <gd name="rw1" fmla="+- wd2 th2 thh" />
            //  <gd name="rh1" fmla="+- hd2 th2 thh" />
            //  <gd name="rw2" fmla="+- rw1 0 th" />
            //  <gd name="rh2" fmla="+- rh1 0 th" />
            //  <gd name="rw3" fmla="+- rw2 th2 0" />
            //  <gd name="rh3" fmla="+- rh2 th2 0" />
            //  <gd name="wtH" fmla="sin rw3 enAng" />
            //  <gd name="htH" fmla="cos rh3 enAng" />
            //  <gd name="dxH" fmla="cat2 rw3 htH wtH" />
            //  <gd name="dyH" fmla="sat2 rh3 htH wtH" />
            //  <gd name="xH" fmla="+- hc dxH 0" />
            //  <gd name="yH" fmla="+- vc dyH 0" />
            //  <gd name="rI" fmla="min rw2 rh2" />
            //  <gd name="u1" fmla="*/ dxH dxH 1" />
            //  <gd name="u2" fmla="*/ dyH dyH 1" />
            //  <gd name="u3" fmla="*/ rI rI 1" />
            //  <gd name="u4" fmla="+- u1 0 u3" />
            //  <gd name="u5" fmla="+- u2 0 u3" />
            //  <gd name="u6" fmla="*/ u4 u5 u1" />
            //  <gd name="u7" fmla="*/ u6 1 u2" />
            //  <gd name="u8" fmla="+- 1 0 u7" />
            //  <gd name="u9" fmla="sqrt u8" />
            //  <gd name="u10" fmla="*/ u4 1 dxH" />
            //  <gd name="u11" fmla="*/ u10 1 dyH" />
            //  <gd name="u12" fmla="+/ 1 u9 u11" />
            //  <gd name="u13" fmla="at2 1 u12" />
            //  <gd name="u14" fmla="+- u13 21600000 0" />
            //  <gd name="u15" fmla="?: u13 u13 u14" />
            //  <gd name="u16" fmla="+- u15 0 enAng" />
            //  <gd name="u17" fmla="+- u16 21600000 0" />
            //  <gd name="u18" fmla="?: u16 u16 u17" />
            //  <gd name="u19" fmla="+- u18 0 cd2" />
            //  <gd name="u20" fmla="+- u18 0 21600000" />
            //  <gd name="u21" fmla="?: u19 u20 u18" />
            //  <gd name="maxAng" fmla="abs u21" />
            //  <gd name="aAng" fmla="pin 0 adj2 maxAng" />
            //  <gd name="ptAng" fmla="+- enAng aAng 0" />
            //  <gd name="wtA" fmla="sin rw3 ptAng" />
            //  <gd name="htA" fmla="cos rh3 ptAng" />
            //  <gd name="dxA" fmla="cat2 rw3 htA wtA" />
            //  <gd name="dyA" fmla="sat2 rh3 htA wtA" />
            //  <gd name="xA" fmla="+- hc dxA 0" />
            //  <gd name="yA" fmla="+- vc dyA 0" />
            //  <gd name="wtE" fmla="sin rw1 stAng" />
            //  <gd name="htE" fmla="cos rh1 stAng" />
            //  <gd name="dxE" fmla="cat2 rw1 htE wtE" />
            //  <gd name="dyE" fmla="sat2 rh1 htE wtE" />
            //  <gd name="xE" fmla="+- hc dxE 0" />
            //  <gd name="yE" fmla="+- vc dyE 0" />
            //  <gd name="dxG" fmla="cos thh ptAng" />
            //  <gd name="dyG" fmla="sin thh ptAng" />
            //  <gd name="xG" fmla="+- xH dxG 0" />
            //  <gd name="yG" fmla="+- yH dyG 0" />
            //  <gd name="dxB" fmla="cos thh ptAng" />
            //  <gd name="dyB" fmla="sin thh ptAng" />
            //  <gd name="xB" fmla="+- xH 0 dxB 0" />
            //  <gd name="yB" fmla="+- yH 0 dyB 0" />
            //  <gd name="sx1" fmla="+- xB 0 hc" />
            //  <gd name="sy1" fmla="+- yB 0 vc" />
            //  <gd name="sx2" fmla="+- xG 0 hc" />
            //  <gd name="sy2" fmla="+- yG 0 vc" />
            //  <gd name="rO" fmla="min rw1 rh1" />
            //  <gd name="x1O" fmla="*/ sx1 rO rw1" />
            //  <gd name="y1O" fmla="*/ sy1 rO rh1" />
            //  <gd name="x2O" fmla="*/ sx2 rO rw1" />
            //  <gd name="y2O" fmla="*/ sy2 rO rh1" />
            //  <gd name="dxO" fmla="+- x2O 0 x1O" />
            //  <gd name="dyO" fmla="+- y2O 0 y1O" />
            //  <gd name="dO" fmla="mod dxO dyO 0" />
            //  <gd name="q1" fmla="*/ x1O y2O 1" />
            //  <gd name="q2" fmla="*/ x2O y1O 1" />
            //  <gd name="DO" fmla="+- q1 0 q2" />
            //  <gd name="q3" fmla="*/ rO rO 1" />
            //  <gd name="q4" fmla="*/ dO dO 1" />
            //  <gd name="q5" fmla="*/ q3 q4 1" />
            //  <gd name="q6" fmla="*/ DO DO 1" />
            //  <gd name="q7" fmla="+- q5 0 q6" />
            //  <gd name="q8" fmla="max q7 0" />
            //  <gd name="sdelO" fmla="sqrt q8" />
            //  <gd name="ndyO" fmla="*/ dyO -1 1" />
            //  <gd name="sdyO" fmla="?: ndyO -1 1" />
            //  <gd name="q9" fmla="*/ sdyO dxO 1" />
            //  <gd name="q10" fmla="*/ q9 sdelO 1" />
            //  <gd name="q11" fmla="*/ DO dyO 1" />
            //  <gd name="dxF1" fmla="+/ q11 q10 q4" />
            //  <gd name="q12" fmla="+- q11 0 q10" />
            //  <gd name="dxF2" fmla="*/ q12 1 q4" />
            //  <gd name="adyO" fmla="abs dyO" />
            //  <gd name="q13" fmla="*/ adyO sdelO 1" />
            //  <gd name="q14" fmla="*/ DO dxO -1" />
            //  <gd name="dyF1" fmla="+/ q14 q13 q4" />
            //  <gd name="q15" fmla="+- q14 0 q13" />
            //  <gd name="dyF2" fmla="*/ q15 1 q4" />
            //  <gd name="q16" fmla="+- x2O 0 dxF1" />
            //  <gd name="q17" fmla="+- x2O 0 dxF2" />
            //  <gd name="q18" fmla="+- y2O 0 dyF1" />
            //  <gd name="q19" fmla="+- y2O 0 dyF2" />
            //  <gd name="q20" fmla="mod q16 q18 0" />
            //  <gd name="q21" fmla="mod q17 q19 0" />
            //  <gd name="q22" fmla="+- q21 0 q20" />
            //  <gd name="dxF" fmla="?: q22 dxF1 dxF2" />
            //  <gd name="dyF" fmla="?: q22 dyF1 dyF2" />
            //  <gd name="sdxF" fmla="*/ dxF rw1 rO" />
            //  <gd name="sdyF" fmla="*/ dyF rh1 rO" />
            //  <gd name="xF" fmla="+- hc sdxF 0" />
            //  <gd name="yF" fmla="+- vc sdyF 0" />
            //  <gd name="x1I" fmla="*/ sx1 rI rw2" />
            //  <gd name="y1I" fmla="*/ sy1 rI rh2" />
            //  <gd name="x2I" fmla="*/ sx2 rI rw2" />
            //  <gd name="y2I" fmla="*/ sy2 rI rh2" />
            //  <gd name="dxI" fmla="+- x2I 0 x1I" />
            //  <gd name="dyI" fmla="+- y2I 0 y1I" />
            //  <gd name="dI" fmla="mod dxI dyI 0" />
            //  <gd name="v1" fmla="*/ x1I y2I 1" />
            //  <gd name="v2" fmla="*/ x2I y1I 1" />
            //  <gd name="DI" fmla="+- v1 0 v2" />
            //  <gd name="v3" fmla="*/ rI rI 1" />
            //  <gd name="v4" fmla="*/ dI dI 1" />
            //  <gd name="v5" fmla="*/ v3 v4 1" />
            //  <gd name="v6" fmla="*/ DI DI 1" />
            //  <gd name="v7" fmla="+- v5 0 v6" />
            //  <gd name="v8" fmla="max v7 0" />
            //  <gd name="sdelI" fmla="sqrt v8" />
            //  <gd name="v9" fmla="*/ sdyO dxI 1" />
            //  <gd name="v10" fmla="*/ v9 sdelI 1" />
            //  <gd name="v11" fmla="*/ DI dyI 1" />
            //  <gd name="dxC1" fmla="+/ v11 v10 v4" />
            //  <gd name="v12" fmla="+- v11 0 v10" />
            //  <gd name="dxC2" fmla="*/ v12 1 v4" />
            //  <gd name="adyI" fmla="abs dyI" />
            //  <gd name="v13" fmla="*/ adyI sdelI 1" />
            //  <gd name="v14" fmla="*/ DI dxI -1" />
            //  <gd name="dyC1" fmla="+/ v14 v13 v4" />
            //  <gd name="v15" fmla="+- v14 0 v13" />
            //  <gd name="dyC2" fmla="*/ v15 1 v4" />
            //  <gd name="v16" fmla="+- x1I 0 dxC1" />
            //  <gd name="v17" fmla="+- x1I 0 dxC2" />
            //  <gd name="v18" fmla="+- y1I 0 dyC1" />
            //  <gd name="v19" fmla="+- y1I 0 dyC2" />
            //  <gd name="v20" fmla="mod v16 v18 0" />
            //  <gd name="v21" fmla="mod v17 v19 0" />
            //  <gd name="v22" fmla="+- v21 0 v20" />
            //  <gd name="dxC" fmla="?: v22 dxC1 dxC2" />
            //  <gd name="dyC" fmla="?: v22 dyC1 dyC2" />
            //  <gd name="sdxC" fmla="*/ dxC rw2 rI" />
            //  <gd name="sdyC" fmla="*/ dyC rh2 rI" />
            //  <gd name="xC" fmla="+- hc sdxC 0" />
            //  <gd name="yC" fmla="+- vc sdyC 0" />
            //  <gd name="ist0" fmla="at2 sdxC sdyC" />
            //  <gd name="ist1" fmla="+- ist0 21600000 0" />
            //  <gd name="istAng" fmla="?: ist0 ist0 ist1" />
            //  <gd name="isw1" fmla="+- stAng 0 istAng" />
            //  <gd name="isw2" fmla="+- isw1 0 21600000" />
            //  <gd name="iswAng" fmla="?: isw1 isw2 isw1" />
            //  <gd name="p1" fmla="+- xF 0 xC" />
            //  <gd name="p2" fmla="+- yF 0 yC" />
            //  <gd name="p3" fmla="mod p1 p2 0" />
            //  <gd name="p4" fmla="*/ p3 1 2" />
            //  <gd name="p5" fmla="+- p4 0 thh" />
            //  <gd name="xGp" fmla="?: p5 xF xG" />
            //  <gd name="yGp" fmla="?: p5 yF yG" />
            //  <gd name="xBp" fmla="?: p5 xC xB" />
            //  <gd name="yBp" fmla="?: p5 yC yB" />
            //  <gd name="en0" fmla="at2 sdxF sdyF" />
            //  <gd name="en1" fmla="+- en0 21600000 0" />
            //  <gd name="en2" fmla="?: en0 en0 en1" />
            //  <gd name="sw0" fmla="+- en2 0 stAng" />
            //  <gd name="sw1" fmla="+- sw0 21600000 0" />
            //  <gd name="swAng" fmla="?: sw0 sw0 sw1" />
            //  <gd name="wtI" fmla="sin rw3 stAng" />
            //  <gd name="htI" fmla="cos rh3 stAng" />
            //  <gd name="dxI" fmla="cat2 rw3 htI wtI" />
            //  <gd name="dyI" fmla="sat2 rh3 htI wtI" />
            //  <gd name="xI" fmla="+- hc dxI 0" />
            //  <gd name="yI" fmla="+- vc dyI 0" />
            //  <gd name="aI" fmla="+- stAng 0 cd4" />
            //  <gd name="aA" fmla="+- ptAng cd4 0" />
            //  <gd name="aB" fmla="+- ptAng cd2 0" />
            //  <gd name="idx" fmla="cos rw1 2700000" />
            //  <gd name="idy" fmla="sin rh1 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>



            //  <gd name="a5" fmla="pin 0 adj5 25000" />
            var a5 = Pin(0, adj5, 25000);
            //  <gd name="maxAdj1" fmla="*/ a5 2 1" />
            var maxAdj1 = a5 * 2 / 1;
            //  <gd name="a1" fmla="pin 0 adj1 maxAdj1" />
            var a1 = Pin(0, adj1, maxAdj1);
            //  <gd name="enAng" fmla="pin 1 adj3 21599999" />
            var enAng = Pin(1, adj3, 21599999);
            //  <gd name="stAng" fmla="pin 0 adj4 21599999" />
            var stAng = Pin(0, adj4, 21599999);
            //  <gd name="th" fmla="*/ ss a1 100000" />
            var th = ss * a1 / 100000;
            //  <gd name="thh" fmla="*/ ss a5 100000" />
            var thh = ss * a5 / 100000;
            //  <gd name="th2" fmla="*/ th 1 2" />
            var th2 = th * 1 / 2;
            //  <gd name="rw1" fmla="+- wd2 th2 thh" />
            var rw1 = wd2 + th2 - thh;
            //  <gd name="rh1" fmla="+- hd2 th2 thh" />
            var rh1 = hd2 + th2 - thh;
            //  <gd name="rw2" fmla="+- rw1 0 th" />
            var rw2 = rw1 - th;
            //  <gd name="rh2" fmla="+- rh1 0 th" />
            var rh2 = rh1 - th;
            //  <gd name="rw3" fmla="+- rw2 th2 0" />
            var rw3 = rw2 + th2;
            //  <gd name="rh3" fmla="+- rh2 th2 0" />
            var rh3 = rh2 + th2;
            //  <gd name="wtH" fmla="sin rw3 enAng" />
            var wtH = Sin(rw3, (int) enAng);
            //  <gd name="htH" fmla="cos rh3 enAng" />
            var htH = Cos(rh3, (int) enAng);
            //  <gd name="dxH" fmla="cat2 rw3 htH wtH" />
            var dxH = Cat2(rw3, htH, wtH);
            //  <gd name="dyH" fmla="sat2 rh3 htH wtH" />
            var dyH = Sat2(rh3, htH, wtH);
            //  <gd name="xH" fmla="+- hc dxH 0" />
            var xH = hc + dxH;
            //  <gd name="yH" fmla="+- vc dyH 0" />
            var yH = vc + dyH;
            //  <gd name="rI" fmla="min rw2 rh2" />
            var rI = System.Math.Min(rw2, rh2);
            //  <gd name="u1" fmla="*/ dxH dxH 1" />
            var u1 = dxH * dxH / 1;
            //  <gd name="u2" fmla="*/ dyH dyH 1" />
            var u2 = dyH * dyH / 1;
            //  <gd name="u3" fmla="*/ rI rI 1" />
            var u3 = rI * rI / 1;
            //  <gd name="u4" fmla="+- u1 0 u3" />
            var u4 = u1 - u3;
            //  <gd name="u5" fmla="+- u2 0 u3" />
            var u5 = u2 - u3;
            //  <gd name="u6" fmla="*/ u4 u5 u1" />
            var u6 = u4 * u5 / u1;
            //  <gd name="u7" fmla="*/ u6 1 u2" />
            var u7 = u6 * 1 / u2;
            //  <gd name="u8" fmla="+- 1 0 u7" />
            var u8 = 1 - u7;
            //  <gd name="u9" fmla="sqrt u8" />
            var u9 = System.Math.Sqrt(u8);
            //  <gd name="u10" fmla="*/ u4 1 dxH" />
            var u10 = u4 * 1 / dxH;
            //  <gd name="u11" fmla="*/ u10 1 dyH" />
            var u11 = u10 * 1 / dyH;
            //  <gd name="u12" fmla="+/ 1 u9 u11" />
            var u12 = (1 + u9) / u11;
            //  <gd name="u13" fmla="at2 1 u12" />
            var u13 = ATan2(1, u12);
            //  <gd name="u14" fmla="+- u13 21600000 0" />
            var u14 = u13 + 21600000;
            //  <gd name="u15" fmla="?: u13 u13 u14" />
            var u15 = u13 > 0 ? u13 : u14;
            //  <gd name="u16" fmla="+- u15 0 enAng" />
            var u16 = u15 - enAng;
            //  <gd name="u17" fmla="+- u16 21600000 0" />
            var u17 = u16 + 21600000;
            //  <gd name="u18" fmla="?: u16 u16 u17" />
            var u18 = u16 > 0 ? u16 : u17;
            //  <gd name="u19" fmla="+- u18 0 cd2" />
            var u19 = u18 - cd2;
            //  <gd name="u20" fmla="+- u18 0 21600000" />
            var u20 = u18 - 21600000;
            //  <gd name="u21" fmla="?: u19 u20 u18" />
            var u21 = u19 > 0 ? u20 : u18;
            //  <gd name="maxAng" fmla="abs u21" />
            var maxAng = System.Math.Abs(u21);
            //  <gd name="aAng" fmla="pin 0 adj2 maxAng" />
            var aAng = Pin(0, adj2, maxAng);
            //  <gd name="ptAng" fmla="+- enAng aAng 0" />
            var ptAng = enAng + aAng;
            //  <gd name="wtA" fmla="sin rw3 ptAng" />
            var wtA = Sin(rw3, (int) ptAng);
            //  <gd name="htA" fmla="cos rh3 ptAng" />
            var htA = Cos(rh3, (int) ptAng);
            //  <gd name="dxA" fmla="cat2 rw3 htA wtA" />
            var dxA = Cat2(rw3, htA, wtA);
            //  <gd name="dyA" fmla="sat2 rh3 htA wtA" />
            var dyA = Sat2(rh3, htA, wtA);
            //  <gd name="xA" fmla="+- hc dxA 0" />
            var xA = hc + dxA;
            //  <gd name="yA" fmla="+- vc dyA 0" />
            var yA = vc + dyA;
            //  <gd name="wtE" fmla="sin rw1 stAng" />
            var wtE = Sin(rw1, (int) stAng);
            //  <gd name="htE" fmla="cos rh1 stAng" />
            var htE = Cos(rh1, (int) stAng);
            //  <gd name="dxE" fmla="cat2 rw1 htE wtE" />
            var dxE = Cat2(rw1, htE, wtE);
            //  <gd name="dyE" fmla="sat2 rh1 htE wtE" />
            var dyE = Sat2(rh1, htE, wtE);
            //  <gd name="xE" fmla="+- hc dxE 0" />
            var xE = hc + dxE;
            //  <gd name="yE" fmla="+- vc dyE 0" />
            var yE = vc + dyE;
            //  <gd name="dxG" fmla="cos thh ptAng" />
            var dxG = Cos(thh, (int) ptAng);
            //  <gd name="dyG" fmla="sin thh ptAng" />
            var dyG = Sin(thh, (int) ptAng);
            //  <gd name="xG" fmla="+- xH dxG 0" />
            var xG = xH + dxG;
            //  <gd name="yG" fmla="+- yH dyG 0" />
            var yG = yH + dyG;
            //  <gd name="dxB" fmla="cos thh ptAng" />
            var dxB = Cos(thh, (int) ptAng);
            //  <gd name="dyB" fmla="sin thh ptAng" />
            var dyB = Sin(thh, (int) ptAng);
            //  <gd name="xB" fmla="+- xH 0 dxB 0" />
            var xB = xH - dxB;
            //  <gd name="yB" fmla="+- yH 0 dyB 0" />
            var yB = yH - dyB;
            //  <gd name="sx1" fmla="+- xB 0 hc" />
            var sx1 = xB - hc;
            //  <gd name="sy1" fmla="+- yB 0 vc" />
            var sy1 = yB - vc;
            //  <gd name="sx2" fmla="+- xG 0 hc" />
            var sx2 = xG - hc;
            //  <gd name="sy2" fmla="+- yG 0 vc" />
            var sy2 = yG - vc;
            //  <gd name="rO" fmla="min rw1 rh1" />
            var rO = System.Math.Min(rw1, rh1);
            //  <gd name="x1O" fmla="*/ sx1 rO rw1" />
            var x1O = sx1 * rO / rw1;
            //  <gd name="y1O" fmla="*/ sy1 rO rh1" />
            var y1O = sy1 * rO / rh1;
            //  <gd name="x2O" fmla="*/ sx2 rO rw1" />
            var x2O = sx2 * rO / rw1;
            //  <gd name="y2O" fmla="*/ sy2 rO rh1" />
            var y2O = sy2 * rO / rh1;
            //  <gd name="dxO" fmla="+- x2O 0 x1O" />
            var dxO = x2O - x1O;
            //  <gd name="dyO" fmla="+- y2O 0 y1O" />
            var dyO = y2O - y1O;
            //  <gd name="dO" fmla="mod dxO dyO 0" />
            var dO = Mod(dxO, dyO, 0);
            //  <gd name="q1" fmla="*/ x1O y2O 1" />
            var q1 = x1O * y2O / 1;
            //  <gd name="q2" fmla="*/ x2O y1O 1" />
            var q2 = x2O * y1O / 1;
            //  <gd name="DO" fmla="+- q1 0 q2" />
            var DO = q1 - q2;
            //  <gd name="q3" fmla="*/ rO rO 1" />
            var q3 = rO * rO / 1;
            //  <gd name="q4" fmla="*/ dO dO 1" />
            var q4 = dO * dO / 1;
            //  <gd name="q5" fmla="*/ q3 q4 1" />
            var q5 = q3 * q4 / 1;
            //  <gd name="q6" fmla="*/ DO DO 1" />
            var q6 = DO * DO / 1;
            //  <gd name="q7" fmla="+- q5 0 q6" />
            var q7 = q5 - q6;
            //  <gd name="q8" fmla="max q7 0" />
            var q8 = System.Math.Max(q7, 0);
            //  <gd name="sdelO" fmla="sqrt q8" />
            var sdelO = System.Math.Sqrt(q8);
            //  <gd name="ndyO" fmla="*/ dyO -1 1" />
            var ndyO = dyO * (-1) / 1;
            //  <gd name="sdyO" fmla="?: ndyO -1 1" />
            var sdyO = ndyO > 0 ? -1 : 1;
            //  <gd name="q9" fmla="*/ sdyO dxO 1" />
            var q9 = sdyO * dxO / 1;
            //  <gd name="q10" fmla="*/ q9 sdelO 1" />
            var q10 = q9 * sdelO / 1;
            //  <gd name="q11" fmla="*/ DO dyO 1" />
            var q11 = DO * dyO / 1;
            //  <gd name="dxF1" fmla="+/ q11 q10 q4" />
            var dxF1 = (q11 + q10) / q4;
            //  <gd name="q12" fmla="+- q11 0 q10" />
            var q12 = q11 - q10;
            //  <gd name="dxF2" fmla="*/ q12 1 q4" />
            var dxF2 = q12 * 1 / q4;
            //  <gd name="adyO" fmla="abs dyO" />
            var adyO = Abs(dyO);
            //  <gd name="q13" fmla="*/ adyO sdelO 1" />
            var q13 = adyO * sdelO / 1;
            //  <gd name="q14" fmla="*/ DO dxO -1" />
            var q14 = DO * dxO / (-1);
            //  <gd name="dyF1" fmla="+/ q14 q13 q4" />
            var dyF1 = (q14 + q13) / q4;
            //  <gd name="q15" fmla="+- q14 0 q13" />
            var q15 = q14 - q13;
            //  <gd name="dyF2" fmla="*/ q15 1 q4" />
            var dyF2 = q15 * 1 / q4;
            //  <gd name="q16" fmla="+- x2O 0 dxF1" />
            var q16 = x2O - dxF1;
            //  <gd name="q17" fmla="+- x2O 0 dxF2" />
            var q17 = x2O - dxF2;
            //  <gd name="q18" fmla="+- y2O 0 dyF1" />
            var q18 = y2O - dyF1;
            //  <gd name="q19" fmla="+- y2O 0 dyF2" />
            var q19 = y2O - dyF2;
            //  <gd name="q20" fmla="mod q16 q18 0" />
            var q20 = Mod(q16, q18, 0);
            //  <gd name="q21" fmla="mod q17 q19 0" />
            var q21 = Mod(q17, q19, 0);
            //  <gd name="q22" fmla="+- q21 0 q20" />
            var q22 = q21 - q20;
            //  <gd name="dxF" fmla="?: q22 dxF1 dxF2" />
            var dxF = q22 > 0 ? dxF1 : dxF2;
            //  <gd name="dyF" fmla="?: q22 dyF1 dyF2" />
            var dyF = q22 > 0 ? dyF1 : dyF2;
            //  <gd name="sdxF" fmla="*/ dxF rw1 rO" />
            var sdxF = dxF * rw1 / rO;
            //  <gd name="sdyF" fmla="*/ dyF rh1 rO" />
            var sdyF = dyF * rh1 / rO;
            //  <gd name="xF" fmla="+- hc sdxF 0" />
            var xF = hc + sdxF;
            //  <gd name="yF" fmla="+- vc sdyF 0" />
            var yF = vc + sdyF;
            //  <gd name="x1I" fmla="*/ sx1 rI rw2" />
            var x1I = sx1 * rI / rw2;
            //  <gd name="y1I" fmla="*/ sy1 rI rh2" />
            var y1I = sy1 * rI / rh2;
            //  <gd name="x2I" fmla="*/ sx2 rI rw2" />
            var x2I = sx2 * rI / rw2;
            //  <gd name="y2I" fmla="*/ sy2 rI rh2" />
            var y2I = sy2 * rI / rh2;
            //  <gd name="dxI" fmla="+- x2I 0 x1I" />
            var dxI = x2I - x1I;
            //  <gd name="dyI" fmla="+- y2I 0 y1I" />
            var dyI = y2I - y1I;
            //  <gd name="dI" fmla="mod dxI dyI 0" />
            var dI = Mod(dxI, dyI, 0);
            //  <gd name="v1" fmla="*/ x1I y2I 1" />
            var v1 = x1I * y2I / 1;
            //  <gd name="v2" fmla="*/ x2I y1I 1" />
            var v2 = x2I * y1I / 1;
            //  <gd name="DI" fmla="+- v1 0 v2" />
            var DI = v1 - v2;
            //  <gd name="v3" fmla="*/ rI rI 1" />
            var v3 = rI * rI / 1;
            //  <gd name="v4" fmla="*/ dI dI 1" />
            var v4 = dI * dI / 1;
            //  <gd name="v5" fmla="*/ v3 v4 1" />
            var v5 = v3 * v4 / 1;
            //  <gd name="v6" fmla="*/ DI DI 1" />
            var v6 = DI * DI / 1;
            //  <gd name="v7" fmla="+- v5 0 v6" />
            var v7 = v5 - v6;
            //  <gd name="v8" fmla="max v7 0" />
            var v8 = System.Math.Max(v7, 0);
            //  <gd name="sdelI" fmla="sqrt v8" />
            var sdelI = System.Math.Sqrt(v8);
            //  <gd name="v9" fmla="*/ sdyO dxI 1" />
            var v9 = sdyO * dxI / 1;
            //  <gd name="v10" fmla="*/ v9 sdelI 1" />
            var v10 = v9 * sdelI / 1;
            //  <gd name="v11" fmla="*/ DI dyI 1" />
            var v11 = DI * dyI / 1;
            //  <gd name="dxC1" fmla="+/ v11 v10 v4" />
            var dxC1 = (v11 + v10) / v4;
            //  <gd name="v12" fmla="+- v11 0 v10" />
            var v12 = v11 - v10;
            //  <gd name="dxC2" fmla="*/ v12 1 v4" />
            var dxC2 = v12 * 1 / v4;
            //  <gd name="adyI" fmla="abs dyI" />
            var adyI = Abs(dyI);
            //  <gd name="v13" fmla="*/ adyI sdelI 1" />
            var v13 = adyI * sdelI / 1;
            //  <gd name="v14" fmla="*/ DI dxI -1" />
            var v14 = DI * dxI / (-1);
            //  <gd name="dyC1" fmla="+/ v14 v13 v4" />
            var dyC1 = (v14 + v13) / v4;
            //  <gd name="v15" fmla="+- v14 0 v13" />
            var v15 = v14 - v13;
            //  <gd name="dyC2" fmla="*/ v15 1 v4" />
            var dyC2 = v15 * 1 / v4;
            //  <gd name="v16" fmla="+- x1I 0 dxC1" />
            var v16 = x1I - dxC1;
            //  <gd name="v17" fmla="+- x1I 0 dxC2" />
            var v17 = x1I - dxC2;
            //  <gd name="v18" fmla="+- y1I 0 dyC1" />
            var v18 = y1I - dyC1;
            //  <gd name="v19" fmla="+- y1I 0 dyC2" />
            var v19 = y1I - dyC2;
            //  <gd name="v20" fmla="mod v16 v18 0" />
            var v20 = Mod(v16, v18, 0);
            //  <gd name="v21" fmla="mod v17 v19 0" />
            var v21 = Mod(v17, v19, 0);
            //  <gd name="v22" fmla="+- v21 0 v20" />
            var v22 = v21 - v20;
            //  <gd name="dxC" fmla="?: v22 dxC1 dxC2" />
            var dxC = v22 > 0 ? dxC1 : dxC2;
            //  <gd name="dyC" fmla="?: v22 dyC1 dyC2" />
            var dyC = v22 > 0 ? dyC1 : dyC2;
            //  <gd name="sdxC" fmla="*/ dxC rw2 rI" />
            var sdxC = dxC * rw2 / rI;
            //  <gd name="sdyC" fmla="*/ dyC rh2 rI" />
            var sdyC = dyC * rh2 / rI;
            //  <gd name="xC" fmla="+- hc sdxC 0" />
            var xC = hc + sdxC;
            //  <gd name="yC" fmla="+- vc sdyC 0" />
            var yC = vc + sdyC;
            //  <gd name="ist0" fmla="at2 sdxC sdyC" />
            var ist0 = ATan2(sdxC, sdyC);
            //  <gd name="ist1" fmla="+- ist0 21600000 0" />
            var ist1 = ist0 + 21600000;
            //  <gd name="istAng" fmla="?: ist0 ist0 ist1" />
            var istAng = ist0 > 0 ? ist0 : ist1;
            //  <gd name="isw1" fmla="+- stAng 0 istAng" />
            var isw1 = stAng - istAng;
            //  <gd name="isw2" fmla="+- isw1 0 21600000" />
            var isw2 = isw1 - 21600000;
            //  <gd name="iswAng" fmla="?: isw1 isw2 isw1" />
            var iswAng = isw1 > 0 ? isw2 : isw1;
            //  <gd name="p1" fmla="+- xF 0 xC" />
            var p1 = xF - xC;
            //  <gd name="p2" fmla="+- yF 0 yC" />
            var p2 = yF - yC;
            //  <gd name="p3" fmla="mod p1 p2 0" />
            var p3 = Mod(p1, p2, 0);
            //  <gd name="p4" fmla="*/ p3 1 2" />
            var p4 = p3 * 1 / 2;
            //  <gd name="p5" fmla="+- p4 0 thh" />
            var p5 = p4 - thh;
            //  <gd name="xGp" fmla="?: p5 xF xG" />
            var xGp = p5 > 0 ? xF : xG;
            //  <gd name="yGp" fmla="?: p5 yF yG" />
            var yGp = p5 > 0 ? yF : yG;
            //  <gd name="xBp" fmla="?: p5 xC xB" />
            var xBp = p5 > 0 ? xC : xB;
            //  <gd name="yBp" fmla="?: p5 yC yB" />
            var yBp = p5 > 0 ? yC : yB;
            //  <gd name="en0" fmla="at2 sdxF sdyF" />
            var en0 = ATan2(sdxF, sdyF);
            //  <gd name="en1" fmla="+- en0 21600000 0" />
            var en1 = en0 + 21600000;
            //  <gd name="en2" fmla="?: en0 en0 en1" />
            var en2 = en0 > 0 ? en0 : en1;
            //  <gd name="sw0" fmla="+- en2 0 stAng" />
            var sw0 = en2 - stAng;
            //  <gd name="sw1" fmla="+- sw0 21600000 0" />
            var sw1 = sw0 + 21600000;
            //  <gd name="swAng" fmla="?: sw0 sw0 sw1" />
            var swAng = sw0 > 0 ? sw0 : sw1;
            //  <gd name="wtI" fmla="sin rw3 stAng" />
            var wtI = Sin(rw3, (int) stAng);
            //  <gd name="htI" fmla="cos rh3 stAng" />
            var htI = Cos(rh3, (int) stAng);
            //  <gd name="dxI" fmla="cat2 rw3 htI wtI" />
            dxI = Cat2(rw3, htI, wtI);
            //  <gd name="dyI" fmla="sat2 rh3 htI wtI" />
            dyI = Sat2(rh3, htI, wtI);
            //  <gd name="xI" fmla="+- hc dxI 0" />
            var xI = hc + dxI;
            //  <gd name="yI" fmla="+- vc dyI 0" />
            var yI = vc + dyI;
            //  <gd name="aI" fmla="+- stAng 0 cd4" />
            var aI = stAng - cd4;
            //  <gd name="aA" fmla="+- ptAng cd4 0" />
            var aA = ptAng + cd4;
            //  <gd name="aB" fmla="+- ptAng cd2 0" />
            var aB = ptAng + cd2;
            //  <gd name="idx" fmla="cos rw1 2700000" />
            var idx = Cos(rw1, 2700000);
            //  <gd name="idy" fmla="sin rh1 2700000" />
            var idy = Sin(rh1, 2700000);
            //  <gd name="il" fmla="+- hc 0 idx" />
            var il = hc - idx;
            //  <gd name="ir" fmla="+- hc idx 0" />
            var ir = hc + idx;
            //  <gd name="it" fmla="+- vc 0 idy" />
            var it = vc - idy;
            //  <gd name="ib" fmla="+- vc idy 0" />
            var ib = vc + idy;




            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="xE" y="yE" />
            //    </moveTo>
            //    <arcTo wR="rw1" hR="rh1" stAng="stAng" swAng="swAng" />
            //    <lnTo>
            //      <pt x="xGp" y="yGp" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xA" y="yA" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xBp" y="yBp" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xC" y="yC" />
            //    </lnTo>
            //    <arcTo wR="rw2" hR="rh2" stAng="istAng" swAng="iswAng" />
            //    <close />
            //  </path>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="xE" y="yE" />
            //    </moveTo>
            var currentPoint = new EmuPoint(xE, yE);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="rw1" hR="rh1" stAng="stAng" swAng="swAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw1, rh1, stAng, swAng);
            //    <lnTo>
            //      <pt x="xGp" y="yGp" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xGp, yGp);
            //    <lnTo>
            //      <pt x="xA" y="yA" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xA, yA);
            //    <lnTo>
            //      <pt x="xBp" y="yBp" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xBp, yBp);
            //    <lnTo>
            //      <pt x="xC" y="yC" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xC, yC);
            //    <arcTo wR="rw2" hR="rh2" stAng="istAng" swAng="iswAng" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw2, rh2, istAng, iswAng);
            //    <close />
            stringPath.Append(" z");
            //  </path>
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }

    }
}
