using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{

    /// <summary>
    /// 左箭头：环形
    /// </summary>
    public class LeftCircularArrowGeometry : ShapeGeometryBase
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
            //  <gd name="adj1" fmla="val 12500" />
            //  <gd name="adj2" fmla="val -1142319" />
            //  <gd name="adj3" fmla="val 1142319" />
            //  <gd name="adj4" fmla="val 10800000" />
            //  <gd name="adj5" fmla="val 12500" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 12500d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? -1142319d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 1142319d;
            var customAdj4 = adjusts?.GetAdjustValue("adj4");
            var adj4 = customAdj4 ?? 10800000d;
            var customAdj5 = adjusts?.GetAdjustValue("adj5");
            var adj5 = customAdj5 ?? 12500d;

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
            //  <gd name="u22" fmla="abs u21" />
            //  <gd name="minAng" fmla="*/ u22 -1 1" />
            //  <gd name="u23" fmla="abs adj2" />
            //  <gd name="a2" fmla="*/ u23 -1 1" />
            //  <gd name="aAng" fmla="pin minAng a2 0" />
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
            //  <gd name="wtD" fmla="sin rw2 stAng" />
            //  <gd name="htD" fmla="cos rh2 stAng" />
            //  <gd name="dxD" fmla="cat2 rw2 htD wtD" />
            //  <gd name="dyD" fmla="sat2 rh2 htD wtD" />
            //  <gd name="xD" fmla="+- hc dxD 0" />
            //  <gd name="yD" fmla="+- vc dyD 0" />
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
            //  <gd name="istAng0" fmla="?: ist0 ist0 ist1" />
            //  <gd name="isw1" fmla="+- stAng 0 istAng0" />
            //  <gd name="isw2" fmla="+- isw1 21600000 0" />
            //  <gd name="iswAng0" fmla="?: isw1 isw1 isw2" />
            //  <gd name="istAng" fmla="+- istAng0 iswAng0 0" />
            //  <gd name="iswAng" fmla="+- 0 0 iswAng0" />
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
            //  <gd name="sw1" fmla="+- sw0 0 21600000" />
            //  <gd name="swAng" fmla="?: sw0 sw1 sw0" />
            //  <gd name="stAng0" fmla="+- stAng swAng 0" />
            //  <gd name="swAng0" fmla="+- 0 0 swAng" />
            //  <gd name="wtI" fmla="sin rw3 stAng" />
            //  <gd name="htI" fmla="cos rh3 stAng" />
            //  <gd name="dxI" fmla="cat2 rw3 htI wtI" />
            //  <gd name="dyI" fmla="sat2 rh3 htI wtI" />
            //  <gd name="xI" fmla="+- hc dxI 0" />
            //  <gd name="yI" fmla="+- vc dyI 0" />
            //  <gd name="aI" fmla="+- stAng cd4 0" />
            //  <gd name="aA" fmla="+- ptAng 0 cd4" />
            //  <gd name="aB" fmla="+- ptAng cd2 0" />
            //  <gd name="idx" fmla="cos rw1 2700000" />
            //  <gd name="idy" fmla="sin rh1 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>

            //<gd name="a5" fmla="pin 0 adj5 25000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a5 = Pin(0, adj5, 25000);
            //<gd name="maxAdj1" fmla="*/ a5 2 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var maxAdj1 = a5 * 2 / 1;
            //<gd name="a1" fmla="pin 0 adj1 maxAdj1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, maxAdj1);
            //<gd name="enAng" fmla="pin 1 adj3 21599999" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var enAng = Pin(1, adj3, 21599999);
            //<gd name="stAng" fmla="pin 0 adj4 21599999" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var stAng = Pin(0, adj4, 21599999);
            //<gd name="th" fmla="*/ ss a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var th = ss * a1 / 100000;
            //<gd name="thh" fmla="*/ ss a5 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var thh = ss * a5 / 100000;
            //<gd name="th2" fmla="*/ th 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var th2 = th * 1 / 2;
            //<gd name="rw1" fmla="+- wd2 th2 thh" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw1 = wd2 + th2 - thh;
            //<gd name="rh1" fmla="+- hd2 th2 thh" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh1 = hd2 + th2 - thh;
            //<gd name="rw2" fmla="+- rw1 0 th" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw2 = rw1 + 0 - th;
            //<gd name="rh2" fmla="+- rh1 0 th" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh2 = rh1 + 0 - th;
            //<gd name="rw3" fmla="+- rw2 th2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw3 = rw2 + th2 - 0;
            //<gd name="rh3" fmla="+- rh2 th2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rh3 = rh2 + th2 - 0;
            //<gd name="wtH" fmla="sin rw3 enAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var wtH = Sin(rw3, (int) enAng);
            //<gd name="htH" fmla="cos rh3 enAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var htH = Cos(rh3, (int) enAng);
            //<gd name="dxH" fmla="cat2 rw3 htH wtH" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxH = Cat2(rw3, htH, wtH);
            //<gd name="dyH" fmla="sat2 rh3 htH wtH" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyH = Sat2(rh3, htH, wtH);
            //<gd name="xH" fmla="+- hc dxH 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xH = hc + dxH - 0;
            //<gd name="yH" fmla="+- vc dyH 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yH = vc + dyH - 0;
            //<gd name="rI" fmla="min rw2 rh2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rI = System.Math.Min(rw2, rh2);
            //<gd name="u1" fmla="*/ dxH dxH 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u1 = dxH * dxH / 1;
            //<gd name="u2" fmla="*/ dyH dyH 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u2 = dyH * dyH / 1;
            //<gd name="u3" fmla="*/ rI rI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u3 = rI * rI / 1;
            //<gd name="u4" fmla="+- u1 0 u3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u4 = u1 + 0 - u3;
            //<gd name="u5" fmla="+- u2 0 u3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u5 = u2 + 0 - u3;
            //<gd name="u6" fmla="*/ u4 u5 u1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u6 = u4 * u5 / u1;
            //<gd name="u7" fmla="*/ u6 1 u2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u7 = u6 * 1 / u2;
            //<gd name="u8" fmla="+- 1 0 u7" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u8 = 1 + 0 - u7;
            //<gd name="u9" fmla="sqrt u8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u9 = System.Math.Sqrt(u8);
            //<gd name="u10" fmla="*/ u4 1 dxH" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u10 = u4 * 1 / dxH;
            //<gd name="u11" fmla="*/ u10 1 dyH" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u11 = u10 * 1 / dyH;
            //<gd name="u12" fmla="+/ 1 u9 u11" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u12 = (1 + u9) / u11;
            //<gd name="u13" fmla="at2 1 u12" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u13 = ATan2(1, u12);
            //<gd name="u14" fmla="+- u13 21600000 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u14 = u13 + 21600000 - 0;
            //<gd name="u15" fmla="?: u13 u13 u14" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u15 = u13 > 0 ? u13 : u14;
            //<gd name="u16" fmla="+- u15 0 enAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u16 = u15 + 0 - enAng;
            //<gd name="u17" fmla="+- u16 21600000 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u17 = u16 + 21600000 - 0;
            //<gd name="u18" fmla="?: u16 u16 u17" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u18 = u16 > 0 ? u16 : u17;
            //<gd name="u19" fmla="+- u18 0 cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u19 = u18 + 0 - cd2;
            //<gd name="u20" fmla="+- u18 0 21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u20 = u18 + 0 - 21600000;
            //<gd name="u21" fmla="?: u19 u20 u18" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u21 = u19 > 0 ? u20 : u18;
            //<gd name="u22" fmla="abs u21" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u22 = Abs(u21);
            //<gd name="minAng" fmla="*/ u22 -1 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var minAng = u22 * -1 / 1;
            //<gd name="u23" fmla="abs adj2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var u23 = Abs(adj2);
            //<gd name="a2" fmla="*/ u23 -1 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a2 = u23 * -1 / 1;
            //<gd name="aAng" fmla="pin minAng a2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aAng = Pin(minAng, a2, 0);
            //<gd name="ptAng" fmla="+- enAng aAng 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ptAng = enAng + aAng - 0;
            //<gd name="wtA" fmla="sin rw3 ptAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var wtA = Sin(rw3, (int) ptAng);
            //<gd name="htA" fmla="cos rh3 ptAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var htA = Cos(rh3, (int) ptAng);
            //<gd name="dxA" fmla="cat2 rw3 htA wtA" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxA = Cat2(rw3, htA, wtA);
            //<gd name="dyA" fmla="sat2 rh3 htA wtA" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyA = Sat2(rh3, htA, wtA);
            //<gd name="xA" fmla="+- hc dxA 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA = hc + dxA - 0;
            //<gd name="yA" fmla="+- vc dyA 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA = vc + dyA - 0;
            //<gd name="wtE" fmla="sin rw1 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var wtE = Sin(rw1, (int) stAng);
            //<gd name="htE" fmla="cos rh1 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var htE = Cos(rh1, (int) stAng);
            //<gd name="dxE" fmla="cat2 rw1 htE wtE" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxE = Cat2(rw1, htE, wtE);
            //<gd name="dyE" fmla="sat2 rh1 htE wtE" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyE = Sat2(rh1, htE, wtE);
            //<gd name="xE" fmla="+- hc dxE 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE = hc + dxE - 0;
            //<gd name="yE" fmla="+- vc dyE 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yE = vc + dyE - 0;
            //<gd name="wtD" fmla="sin rw2 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var wtD = Sin(rw2, (int) stAng);
            //<gd name="htD" fmla="cos rh2 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var htD = Cos(rh2, (int) stAng);
            //<gd name="dxD" fmla="cat2 rw2 htD wtD" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxD = Cat2(rw2, htD, wtD);
            //<gd name="dyD" fmla="sat2 rh2 htD wtD" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyD = Sat2(rh2, htD, wtD);
            //<gd name="xD" fmla="+- hc dxD 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD = hc + dxD - 0;
            //<gd name="yD" fmla="+- vc dyD 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yD = vc + dyD - 0;
            //<gd name="dxG" fmla="cos thh ptAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxG = Cos(thh, (int) ptAng);
            //<gd name="dyG" fmla="sin thh ptAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyG = Sin(thh, (int) ptAng);
            //<gd name="xG" fmla="+- xH dxG 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xG = xH + dxG - 0;
            //<gd name="yG" fmla="+- yH dyG 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yG = yH + dyG - 0;
            //<gd name="dxB" fmla="cos thh ptAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxB = Cos(thh, (int) ptAng);
            //<gd name="dyB" fmla="sin thh ptAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyB = Sin(thh, (int) ptAng);
            //<gd name="xB" fmla="+- xH 0 dxB 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB = xH + 0 - dxB;
            //<gd name="yB" fmla="+- yH 0 dyB 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB = yH + 0 - dyB;
            //<gd name="sx1" fmla="+- xB 0 hc" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx1 = xB + 0 - hc;
            //<gd name="sy1" fmla="+- yB 0 vc" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy1 = yB + 0 - vc;
            //<gd name="sx2" fmla="+- xG 0 hc" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sx2 = xG + 0 - hc;
            //<gd name="sy2" fmla="+- yG 0 vc" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sy2 = yG + 0 - vc;
            //<gd name="rO" fmla="min rw1 rh1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rO = System.Math.Min(rw1, rh1);
            //<gd name="x1O" fmla="*/ sx1 rO rw1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1O = sx1 * rO / rw1;
            //<gd name="y1O" fmla="*/ sy1 rO rh1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1O = sy1 * rO / rh1;
            //<gd name="x2O" fmla="*/ sx2 rO rw1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2O = sx2 * rO / rw1;
            //<gd name="y2O" fmla="*/ sy2 rO rh1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2O = sy2 * rO / rh1;
            //<gd name="dxO" fmla="+- x2O 0 x1O" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxO = x2O + 0 - x1O;
            //<gd name="dyO" fmla="+- y2O 0 y1O" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyO = y2O + 0 - y1O;
            //<gd name="dO" fmla="mod dxO dyO 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dO = Mod(dxO, dyO, 0);
            //<gd name="q1" fmla="*/ x1O y2O 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q1 = x1O * y2O / 1;
            //<gd name="q2" fmla="*/ x2O y1O 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q2 = x2O * y1O / 1;
            //<gd name="DO" fmla="+- q1 0 q2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var DO = q1 + 0 - q2;
            //<gd name="q3" fmla="*/ rO rO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q3 = rO * rO / 1;
            //<gd name="q4" fmla="*/ dO dO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q4 = dO * dO / 1;
            //<gd name="q5" fmla="*/ q3 q4 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q5 = q3 * q4 / 1;
            //<gd name="q6" fmla="*/ DO DO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q6 = DO * DO / 1;
            //<gd name="q7" fmla="+- q5 0 q6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q7 = q5 + 0 - q6;
            //<gd name="q8" fmla="max q7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q8 = System.Math.Max(q7, 0);
            //<gd name="sdelO" fmla="sqrt q8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdelO = System.Math.Sqrt(q8);
            //<gd name="ndyO" fmla="*/ dyO -1 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ndyO = dyO * -1 / 1;
            //<gd name="sdyO" fmla="?: ndyO -1 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdyO = ndyO > 0 ? -1 : 1;
            //<gd name="q9" fmla="*/ sdyO dxO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q9 = sdyO * dxO / 1;
            //<gd name="q10" fmla="*/ q9 sdelO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q10 = q9 * sdelO / 1;
            //<gd name="q11" fmla="*/ DO dyO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q11 = DO * dyO / 1;
            //<gd name="dxF1" fmla="+/ q11 q10 q4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF1 = (q11 + q10) / q4;
            //<gd name="q12" fmla="+- q11 0 q10" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q12 = q11 + 0 - q10;
            //<gd name="dxF2" fmla="*/ q12 1 q4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF2 = q12 * 1 / q4;
            //<gd name="adyO" fmla="abs dyO" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var adyO = Abs(dyO);
            //<gd name="q13" fmla="*/ adyO sdelO 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q13 = adyO * sdelO / 1;
            //<gd name="q14" fmla="*/ DO dxO -1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q14 = DO * dxO / -1;
            //<gd name="dyF1" fmla="+/ q14 q13 q4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF1 = (q14 + q13) / q4;
            //<gd name="q15" fmla="+- q14 0 q13" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q15 = q14 + 0 - q13;
            //<gd name="dyF2" fmla="*/ q15 1 q4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF2 = q15 * 1 / q4;
            //<gd name="q16" fmla="+- x2O 0 dxF1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q16 = x2O + 0 - dxF1;
            //<gd name="q17" fmla="+- x2O 0 dxF2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q17 = x2O + 0 - dxF2;
            //<gd name="q18" fmla="+- y2O 0 dyF1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q18 = y2O + 0 - dyF1;
            //<gd name="q19" fmla="+- y2O 0 dyF2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q19 = y2O + 0 - dyF2;
            //<gd name="q20" fmla="mod q16 q18 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q20 = Mod(q16, q18, 0);
            //<gd name="q21" fmla="mod q17 q19 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q21 = Mod(q17, q19, 0);
            //<gd name="q22" fmla="+- q21 0 q20" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var q22 = q21 + 0 - q20;
            //<gd name="dxF" fmla="?: q22 dxF1 dxF2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxF = q22 > 0 ? dxF1 : dxF2;
            //<gd name="dyF" fmla="?: q22 dyF1 dyF2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyF = q22 > 0 ? dyF1 : dyF2;
            //<gd name="sdxF" fmla="*/ dxF rw1 rO" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdxF = dxF * rw1 / rO;
            //<gd name="sdyF" fmla="*/ dyF rh1 rO" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdyF = dyF * rh1 / rO;
            //<gd name="xF" fmla="+- hc sdxF 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF = hc + sdxF - 0;
            //<gd name="yF" fmla="+- vc sdyF 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yF = vc + sdyF - 0;
            //<gd name="x1I" fmla="*/ sx1 rI rw2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1I = sx1 * rI / rw2;
            //<gd name="y1I" fmla="*/ sy1 rI rh2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1I = sy1 * rI / rh2;
            //<gd name="x2I" fmla="*/ sx2 rI rw2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2I = sx2 * rI / rw2;
            //<gd name="y2I" fmla="*/ sy2 rI rh2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2I = sy2 * rI / rh2;
            //<gd name="dxI" fmla="+- x2I 0 x1I" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxI = x2I + 0 - x1I;
            //<gd name="dyI" fmla="+- y2I 0 y1I" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyI = y2I + 0 - y1I;
            //<gd name="dI" fmla="mod dxI dyI 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dI = Mod(dxI, dyI, 0);
            //<gd name="v1" fmla="*/ x1I y2I 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v1 = x1I * y2I / 1;
            //<gd name="v2" fmla="*/ x2I y1I 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v2 = x2I * y1I / 1;
            //<gd name="DI" fmla="+- v1 0 v2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var DI = v1 + 0 - v2;
            //<gd name="v3" fmla="*/ rI rI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v3 = rI * rI / 1;
            //<gd name="v4" fmla="*/ dI dI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v4 = dI * dI / 1;
            //<gd name="v5" fmla="*/ v3 v4 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v5 = v3 * v4 / 1;
            //<gd name="v6" fmla="*/ DI DI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v6 = DI * DI / 1;
            //<gd name="v7" fmla="+- v5 0 v6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v7 = v5 + 0 - v6;
            //<gd name="v8" fmla="max v7 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v8 = System.Math.Max(v7, 0);
            //<gd name="sdelI" fmla="sqrt v8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdelI = System.Math.Sqrt(v8);
            //<gd name="v9" fmla="*/ sdyO dxI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v9 = sdyO * dxI / 1;
            //<gd name="v10" fmla="*/ v9 sdelI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v10 = v9 * sdelI / 1;
            //<gd name="v11" fmla="*/ DI dyI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v11 = DI * dyI / 1;
            //<gd name="dxC1" fmla="+/ v11 v10 v4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxC1 = (v11 + v10) / v4;
            //<gd name="v12" fmla="+- v11 0 v10" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v12 = v11 + 0 - v10;
            //<gd name="dxC2" fmla="*/ v12 1 v4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxC2 = v12 * 1 / v4;
            //<gd name="adyI" fmla="abs dyI" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var adyI = Abs(dyI);
            //<gd name="v13" fmla="*/ adyI sdelI 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v13 = adyI * sdelI / 1;
            //<gd name="v14" fmla="*/ DI dxI -1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v14 = DI * dxI / -1;
            //<gd name="dyC1" fmla="+/ v14 v13 v4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyC1 = (v14 + v13) / v4;
            //<gd name="v15" fmla="+- v14 0 v13" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v15 = v14 + 0 - v13;
            //<gd name="dyC2" fmla="*/ v15 1 v4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyC2 = v15 * 1 / v4;
            //<gd name="v16" fmla="+- x1I 0 dxC1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v16 = x1I + 0 - dxC1;
            //<gd name="v17" fmla="+- x1I 0 dxC2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v17 = x1I + 0 - dxC2;
            //<gd name="v18" fmla="+- y1I 0 dyC1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v18 = y1I + 0 - dyC1;
            //<gd name="v19" fmla="+- y1I 0 dyC2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v19 = y1I + 0 - dyC2;
            //<gd name="v20" fmla="mod v16 v18 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v20 = Mod(v16, v18, 0);
            //<gd name="v21" fmla="mod v17 v19 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v21 = Mod(v17, v19, 0);
            //<gd name="v22" fmla="+- v21 0 v20" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var v22 = v21 + 0 - v20;
            //<gd name="dxC" fmla="?: v22 dxC1 dxC2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxC = v22 > 0 ? dxC1 : dxC2;
            //<gd name="dyC" fmla="?: v22 dyC1 dyC2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyC = v22 > 0 ? dyC1 : dyC2;
            //<gd name="sdxC" fmla="*/ dxC rw2 rI" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdxC = dxC * rw2 / rI;
            //<gd name="sdyC" fmla="*/ dyC rh2 rI" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sdyC = dyC * rh2 / rI;
            //<gd name="xC" fmla="+- hc sdxC 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC = hc + sdxC - 0;
            //<gd name="yC" fmla="+- vc sdyC 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC = vc + sdyC - 0;
            //<gd name="ist0" fmla="at2 sdxC sdyC" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ist0 = ATan2(sdxC, sdyC);
            //<gd name="ist1" fmla="+- ist0 21600000 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ist1 = ist0 + 21600000 - 0;
            //<gd name="istAng0" fmla="?: ist0 ist0 ist1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var istAng0 = ist0 > 0 ? ist0 : ist1;
            //<gd name="isw1" fmla="+- stAng 0 istAng0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var isw1 = stAng + 0 - istAng0;
            //<gd name="isw2" fmla="+- isw1 21600000 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var isw2 = isw1 + 21600000 - 0;
            //<gd name="iswAng0" fmla="?: isw1 isw1 isw2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iswAng0 = isw1 > 0 ? isw1 : isw2;
            //<gd name="istAng" fmla="+- istAng0 iswAng0 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var istAng = istAng0 + iswAng0 - 0;
            //<gd name="iswAng" fmla="+- 0 0 iswAng0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var iswAng = 0 + 0 - iswAng0;
            //<gd name="p1" fmla="+- xF 0 xC" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var p1 = xF + 0 - xC;
            //<gd name="p2" fmla="+- yF 0 yC" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var p2 = yF + 0 - yC;
            //<gd name="p3" fmla="mod p1 p2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var p3 = Mod(p1, p2, 0);
            //<gd name="p4" fmla="*/ p3 1 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var p4 = p3 * 1 / 2;
            //<gd name="p5" fmla="+- p4 0 thh" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var p5 = p4 + 0 - thh;
            //<gd name="xGp" fmla="?: p5 xF xG" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xGp = p5 > 0 ? xF : xG;
            //<gd name="yGp" fmla="?: p5 yF yG" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yGp = p5 > 0 ? yF : yG;
            //<gd name="xBp" fmla="?: p5 xC xB" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xBp = p5 > 0 ? xC : xB;
            //<gd name="yBp" fmla="?: p5 yC yB" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yBp = p5 > 0 ? yC : yB;
            //<gd name="en0" fmla="at2 sdxF sdyF" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var en0 = ATan2(sdxF, sdyF);
            //<gd name="en1" fmla="+- en0 21600000 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var en1 = en0 + 21600000 - 0;
            //<gd name="en2" fmla="?: en0 en0 en1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var en2 = en0 > 0 ? en0 : en1;
            //<gd name="sw0" fmla="+- en2 0 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sw0 = en2 + 0 - stAng;
            //<gd name="sw1" fmla="+- sw0 0 21600000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sw1 = sw0 + 0 - 21600000;
            //<gd name="swAng" fmla="?: sw0 sw1 sw0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng = sw0 > 0 ? sw1 : sw0;
            //<gd name="stAng0" fmla="+- stAng swAng 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var stAng0 = stAng + swAng - 0;
            //<gd name="swAng0" fmla="+- 0 0 swAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var swAng0 = 0 + 0 - swAng;
            //<gd name="wtI" fmla="sin rw3 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var wtI = Sin(rw3, (int) stAng);
            //<gd name="htI" fmla="cos rh3 stAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var htI = Cos(rh3, (int) stAng);
            //<gd name="dxI" fmla="cat2 rw3 htI wtI" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            dxI = Cat2(rw3, htI, wtI);
            //<gd name="dyI" fmla="sat2 rh3 htI wtI" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            dyI = Sat2(rh3, htI, wtI);
            //<gd name="xI" fmla="+- hc dxI 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xI = hc + dxI - 0;
            //<gd name="yI" fmla="+- vc dyI 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yI = vc + dyI - 0;
            //<gd name="aI" fmla="+- stAng cd4 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aI = stAng + cd4 - 0;
            //<gd name="aA" fmla="+- ptAng 0 cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aA = ptAng + 0 - cd4;
            //<gd name="aB" fmla="+- ptAng cd2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var aB = ptAng + cd2 - 0;
            //<gd name="idx" fmla="cos rw1 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var idx = Cos(rw1, (int) 2700000);
            //<gd name="idy" fmla="sin rh1 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var idy = Sin(rh1, (int) 2700000);
            //<gd name="il" fmla="+- hc 0 idx" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = hc + 0 - idx;
            //<gd name="ir" fmla="+- hc idx 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = hc + idx - 0;
            //<gd name="it" fmla="+- vc 0 idy" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var it = vc + 0 - idy;
            //<gd name="ib" fmla="+- vc idy 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = vc + idy - 0;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="xE" y="yE" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="xD" y="yD" />
            //    </lnTo>
            //    <arcTo wR="rw2" hR="rh2" stAng="istAng" swAng="iswAng" />
            //    <lnTo>
            //      <pt x="xBp" y="yBp" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xA" y="yA" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xGp" y="yGp" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xF" y="yF" />
            //    </lnTo>
            //    <arcTo wR="rw1" hR="rh1" stAng="stAng0" swAng="swAng0" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE" y="yE" />
            //</moveTo>
            var currentPoint = new EmuPoint(xE, yE);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD" y="yD" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD, yD);
            //<arcTo wR="rw2" hR="rh2" stAng="istAng" swAng="iswAng" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw2, rh2, istAng, iswAng);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xBp" y="yBp" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xBp, yBp);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xA" y="yA" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xA, yA);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xGp" y="yGp" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xGp, yGp);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xF" y="yF" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xF, yF);
            //<arcTo wR="rw1" hR="rh1" stAng="stAng0" swAng="swAng0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, rw1, rh1, stAng0, swAng0);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

