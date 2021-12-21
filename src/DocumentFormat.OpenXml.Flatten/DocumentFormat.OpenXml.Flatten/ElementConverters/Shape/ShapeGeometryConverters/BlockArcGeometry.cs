using System.Collections.Generic;
using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 空心弧
    /// </summary>
    public class BlockArcGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string? ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 10800000" />
            //  <gd name="adj2" fmla="val 0" />
            //  <gd name="adj3" fmla="val 25000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 10800000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 0d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="stAng" fmla="pin 0 adj1 21599999" />
            var stAng = Pin(0, adj1, 21599999);
            //  <gd name="istAng" fmla="pin 0 adj2 21599999" />
            var istAng = Pin(0, adj2, 21599999);
            //  <gd name="a3" fmla="pin 0 adj3 50000" />
            var a3 = Pin(0, adj3, 50000);
            //  <gd name="sw11" fmla="+- istAng 0 stAng" />
            var sw11 = istAng - stAng;
            //  <gd name="sw12" fmla="+- sw11 21600000 0" />
            var sw12 = sw11 + 21600000;
            //  <gd name="swAng" fmla="?: sw11 sw11 sw12" />
            var swAng = sw11 > 0 ? sw11 : sw12;
            //  <gd name="iswAng" fmla="+- 0 0 swAng" />
            var iswAng = 0 + 0 - swAng;
            //  <gd name="wt1" fmla="sin wd2 stAng" />
            var wt1 = Sin(wd2, (int) stAng);
            //  <gd name="ht1" fmla="cos hd2 stAng" />
            var ht1 = Cos(hd2, (int) stAng);
            //  <gd name="wt3" fmla="sin wd2 istAng" />
            var wt3 = Sin(wd2, (int) istAng);
            //  <gd name="ht3" fmla="cos hd2 istAng" />
            var ht3 = Cos(hd2, (int) istAng);
            //  <gd name="dx1" fmla="cat2 wd2 ht1 wt1" />
            var dx1 = Cat2(wd2, ht1, wt1);
            //  <gd name="dy1" fmla="sat2 hd2 ht1 wt1" />
            var dy1 = Sat2(hd2, ht1, wt1);
            //  <gd name="dx3" fmla="cat2 wd2 ht3 wt3" />
            var dx3 = Cat2(wd2, ht3, wt3);
            //  <gd name="dy3" fmla="sat2 hd2 ht3 wt3" />
            var dy3 = Sat2(hd2, ht3, wt3);
            //  <gd name="x1" fmla="+- hc dx1 0" />
            var x1 = hc + dx1;
            //  <gd name="y1" fmla="+- vc dy1 0" />
            var y1 = vc + dy1;
            //  <gd name="x3" fmla="+- hc dx3 0" />
            var x3 = hc + dx3;
            //  <gd name="y3" fmla="+- vc dy3 0" />
            var y3 = vc + dy3;
            //  <gd name="dr" fmla="*/ ss a3 100000" />
            var dr = ss * a3 / 100000;
            //  <gd name="iwd2" fmla="+- wd2 0 dr" />
            var iwd2 = wd2 - dr;
            //  <gd name="ihd2" fmla="+- hd2 0 dr" />
            var ihd2 = hd2 - dr;
            //  <gd name="wt2" fmla="sin iwd2 istAng" />
            var wt2 = Sin(iwd2, (int) istAng);
            //  <gd name="ht2" fmla="cos ihd2 istAng" />
            var ht2 = Cos(ihd2, (int) istAng);
            //  <gd name="wt4" fmla="sin iwd2 stAng" />
            var wt4 = Sin(iwd2, (int) stAng);
            //  <gd name="ht4" fmla="cos ihd2 stAng" />
            var ht4 = Cos(ihd2, (int) stAng);
            //  <gd name="dx2" fmla="cat2 iwd2 ht2 wt2" />
            var dx2 = Cat2(iwd2, ht2, wt2);
            //  <gd name="dy2" fmla="sat2 ihd2 ht2 wt2" />
            var dy2 = Sat2(ihd2, ht2, wt2);
            //  <gd name="dx4" fmla="cat2 iwd2 ht4 wt4" />
            var dx4 = Cat2(iwd2, ht4, wt4);
            //  <gd name="dy4" fmla="sat2 ihd2 ht4 wt4" />
            var dy4 = Sat2(ihd2, ht4, wt4);
            //  <gd name="x2" fmla="+- hc dx2 0" />
            var x2 = hc + dx2;
            //  <gd name="y2" fmla="+- vc dy2 0" />
            var y2 = vc + dy2;
            //  <gd name="x4" fmla="+- hc dx4 0" />
            var x4 = hc + dx4;
            //  <gd name="y4" fmla="+- vc dy4 0" />
            var y4 = vc + dy4;
            //  <gd name="sw0" fmla="+- 21600000 0 stAng" />
            var sw0 = 21600000 - stAng;
            //  <gd name="da1" fmla="+- swAng 0 sw0" />
            var da1 = swAng - sw0;
            //  <gd name="g1" fmla="max x1 x2" />
            var g1 = System.Math.Max(x1, x2);
            //  <gd name="g2" fmla="max x3 x4" />
            var g2 = System.Math.Max(x3, x4);
            //  <gd name="g3" fmla="max g1 g2" />
            var g3 = System.Math.Max(g1, g2);
            //  <gd name="ir" fmla="?: da1 r g3" />
            var ir = da1 > 0 ? r : g3;
            //  <gd name="sw1" fmla="+- cd4 0 stAng" />
            var sw1 = cd4 - stAng;
            //  <gd name="sw2" fmla="+- 27000000 0 stAng" />
            var sw2 = 27000000 - stAng;
            //  <gd name="sw3" fmla="?: sw1 sw1 sw2" />
            var sw3 = sw1 > 0 ? sw1 : sw2;
            //  <gd name="da2" fmla="+- swAng 0 sw3" />
            var da2 = swAng - sw3;
            //  <gd name="g5" fmla="max y1 y2" />
            var g5 = System.Math.Max(y1, y2);
            //  <gd name="g6" fmla="max y3 y4" />
            var g6 = System.Math.Max(y3, y4);
            //  <gd name="g7" fmla="max g5 g6" />
            var g7 = System.Math.Max(g5, g6);
            //  <gd name="ib" fmla="?: da2 b g7" />
            var ib = da2 > 0 ? b : g7;
            //  <gd name="sw4" fmla="+- cd2 0 stAng" />
            var sw4 = cd2 - stAng;
            //  <gd name="sw5" fmla="+- 32400000 0 stAng" />
            var sw5 = 32400000 - stAng;
            //  <gd name="sw6" fmla="?: sw4 sw4 sw5" />
            var sw6 = sw4 > 0 ? sw4 : sw5;
            //  <gd name="da3" fmla="+- swAng 0 sw6" />
            var da3 = swAng - sw6;
            //  <gd name="g9" fmla="min x1 x2" />
            var g9 = System.Math.Min(x1, x2);
            //  <gd name="g10" fmla="min x3 x4" />
            var g10 = System.Math.Min(x3, x4);
            //  <gd name="g11" fmla="min g9 g10" />
            var g11 = System.Math.Min(g9, g10);
            //  <gd name="il" fmla="?: da3 l g11" />
            var il = da3 > 0 ? l : g11;
            //  <gd name="sw7" fmla="+- 3cd4 0 stAng" />
            var sw7 = (3 * cd4) - stAng;
            //  <gd name="sw8" fmla="+- 37800000 0 stAng" />
            var sw8 = 37800000 - stAng;
            //  <gd name="sw9" fmla="?: sw7 sw7 sw8" />
            var sw9 = sw7 > 0 ? sw7 : sw8;
            //  <gd name="da4" fmla="+- swAng 0 sw9" />
            var da4 = swAng - sw9;
            //  <gd name="g13" fmla="min y1 y2" />
            var g13 = System.Math.Min(y1, y2);
            //  <gd name="g14" fmla="min y3 y4" />
            var g14 = System.Math.Min(y3, y4);
            //  <gd name="g15" fmla="min g13 g14" />
            var g15 = System.Math.Min(g13, g14);
            //  <gd name="it" fmla="?: da4 t g15" />
            var it = da4 > 0 ? t : g15;
            //  <gd name="x5" fmla="+/ x1 x4 2" />
            //  <gd name="y5" fmla="+/ y1 y4 2" />
            //  <gd name="x6" fmla="+/ x3 x2 2" />
            //  <gd name="y6" fmla="+/ y3 y2 2" />
            //  <gd name="cang1" fmla="+- stAng 0 cd4" />
            //  <gd name="cang2" fmla="+- istAng cd4 0" />
            //  <gd name="cang3" fmla="+/ cang1 cang2 2" />
            //</gdLst>

            //   <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <arcTo wR="iwd2" hR="ihd2" stAng="istAng" swAng="iswAng" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            var wR = wd2;
            var hR = hd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //    <arcTo wR="iwd2" hR="ihd2" stAng="istAng" swAng="iswAng" />
            wR = iwd2;
            hR = ihd2;
            stAng = istAng;
            swAng = iswAng;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
