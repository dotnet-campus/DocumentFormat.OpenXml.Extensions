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
    /// 弧形
    /// </summary>
    public class ArcGeometry : ShapeGeometryBase
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
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 16200000" />
            //  <gd name="adj2" fmla="val 0" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 16200000d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 0d;

            // <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="stAng" fmla="pin 0 adj1 21599999" />
            //  <gd name="enAng" fmla="pin 0 adj2 21599999" />
            //  <gd name="sw11" fmla="+- enAng 0 stAng" />
            //  <gd name="sw12" fmla="+- sw11 21600000 0" />
            //  <gd name="swAng" fmla="?: sw11 sw11 sw12" />
            //  <gd name="wt1" fmla="sin wd2 stAng" />
            //  <gd name="ht1" fmla="cos hd2 stAng" />
            //  <gd name="dx1" fmla="cat2 wd2 ht1 wt1" />
            //  <gd name="dy1" fmla="sat2 hd2 ht1 wt1" />
            //  <gd name="wt2" fmla="sin wd2 enAng" />
            //  <gd name="ht2" fmla="cos hd2 enAng" />
            //  <gd name="dx2" fmla="cat2 wd2 ht2 wt2" />
            //  <gd name="dy2" fmla="sat2 hd2 ht2 wt2" />
            //  <gd name="x1" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc dy1 0" />
            //  <gd name="x2" fmla="+- hc dx2 0" />
            //  <gd name="y2" fmla="+- vc dy2 0" />
            //  <gd name="sw0" fmla="+- 21600000 0 stAng" />
            //  <gd name="da1" fmla="+- swAng 0 sw0" />
            //  <gd name="g1" fmla="max x1 x2" />
            //  <gd name="ir" fmla="?: da1 r g1" />
            //  <gd name="sw1" fmla="+- cd4 0 stAng" />
            //  <gd name="sw2" fmla="+- 27000000 0 stAng" />
            //  <gd name="sw3" fmla="?: sw1 sw1 sw2" />
            //  <gd name="da2" fmla="+- swAng 0 sw3" />
            //  <gd name="g5" fmla="max y1 y2" />
            //  <gd name="ib" fmla="?: da2 b g5" />
            //  <gd name="sw4" fmla="+- cd2 0 stAng" />
            //  <gd name="sw5" fmla="+- 32400000 0 stAng" />
            //  <gd name="sw6" fmla="?: sw4 sw4 sw5" />
            //  <gd name="da3" fmla="+- swAng 0 sw6" />
            //  <gd name="g9" fmla="min x1 x2" />
            //  <gd name="il" fmla="?: da3 l g9" />
            //  <gd name="sw7" fmla="+- 3cd4 0 stAng" />
            //  <gd name="sw8" fmla="+- 37800000 0 stAng" />
            //  <gd name="sw9" fmla="?: sw7 sw7 sw8" />
            //  <gd name="da4" fmla="+- swAng 0 sw9" />
            //  <gd name="g13" fmla="min y1 y2" />
            //  <gd name="it" fmla="?: da4 t g13" />
            //  <gd name="cang1" fmla="+- stAng 0 cd4" />
            //  <gd name="cang2" fmla="+- enAng cd4 0" />
            //  <gd name="cang3" fmla="+/ cang1 cang2 2" />
            //</gdLst>

            //  <gd name="stAng" fmla="pin 0 adj1 21599999" />
            var stAng = Pin(0, adj1, 21599999);
            //  <gd name="enAng" fmla="pin 0 adj2 21599999" />
            var enAng = Pin(0, adj2, 21599999);
            //  <gd name="sw11" fmla="+- enAng 0 stAng" />
            var sw11 = enAng - stAng;
            //  <gd name="sw12" fmla="+- sw11 21600000 0" />
            var sw12 = sw11 + 21600000;
            //  <gd name="swAng" fmla="?: sw11 sw11 sw12" />
            var swAng = sw11 > 0 ? sw11 : sw12;
            //  <gd name="wt1" fmla="sin wd2 stAng" />
            var wt1 = Sin(wd2, (int) stAng);
            //  <gd name="ht1" fmla="cos hd2 stAng" />
            var ht1 = Cos(hd2, (int) stAng);
            //  <gd name="dx1" fmla="cat2 wd2 ht1 wt1" />
            var dx1 = Cat2(wd2, ht1, wt1);
            //  <gd name="dy1" fmla="sat2 hd2 ht1 wt1" />
            var dy1 = Sat2(hd2, ht1, wt1);
            //  <gd name="wt2" fmla="sin wd2 enAng" />
            var wt2 = Sin(wd2, (int) enAng);
            //  <gd name="ht2" fmla="cos hd2 enAng" />
            var ht2 = Cos(hd2, (int) enAng);
            //  <gd name="dx2" fmla="cat2 wd2 ht2 wt2" />
            var dx2 = Cat2(wd2, ht2, wt2);
            //  <gd name="dy2" fmla="sat2 hd2 ht2 wt2" />
            var dy2 = Sat2(hd2, ht2, wt2);
            //  <gd name="x1" fmla="+- hc dx1 0" />
            var x1 = hc + dx1;
            //  <gd name="y1" fmla="+- vc dy1 0" />
            var y1 = vc + dy1;
            //  <gd name="x2" fmla="+- hc dx2 0" />
            var x2 = hc + dx2;
            //  <gd name="y2" fmla="+- vc dy2 0" />
            var y2 = vc + dy2;
            //  <gd name="sw0" fmla="+- 21600000 0 stAng" />
            var sw0 = 21600000 - stAng;
            //  <gd name="da1" fmla="+- swAng 0 sw0" />
            var da1 = swAng - sw0;
            //  <gd name="g1" fmla="max x1 x2" />
            var g1 = System.Math.Max(x1, x2);
            //  <gd name="ir" fmla="?: da1 r g1" />
            var ir = da1 > 0 ? r : g1;
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
            //  <gd name="ib" fmla="?: da2 b g5" />
            var ib = da2 > 0 ? b : g5;
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
            //  <gd name="il" fmla="?: da3 l g9" />
            var il = da3 > 0 ? l : g9;
            //  <gd name="sw7" fmla="+- 3cd4 0 stAng" />
            var sw7 = 3 * cd4 - stAng;
            //  <gd name="sw8" fmla="+- 37800000 0 stAng" />
            var sw8 = 37800000 - stAng;
            //  <gd name="sw9" fmla="?: sw7 sw7 sw8" />
            var sw9 = sw7 > 0 ? sw7 : sw8;
            //  <gd name="da4" fmla="+- swAng 0 sw9" />
            var da4 = swAng - sw9;
            //  <gd name="g13" fmla="min y1 y2" />
            var g13 = System.Math.Min(y1, y2);
            //  <gd name="it" fmla="?: da4 t g13" />
            var it = da4 > 0 ? t : g13;
            //  <gd name="cang1" fmla="+- stAng 0 cd4" />
            var cang1 = stAng - cd4;
            //  <gd name="cang2" fmla="+- enAng cd4 0" />
            var cang2 = enAng + cd4;
            //  <gd name="cang3" fmla="+/ cang1 cang2 2" />
            var cang3 = (cang1 + cang2) / 2;



            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            //    <lnTo>
            //      <pt x="hc" y="vc" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[2];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            var wR = wd2;
            var hR = hd2;
            ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="hc" y="vc" />
            //    </lnTo>
            LineToToString(stringPath, hc, vc);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            // 这是特别设置 PathFillModeValues 的值，用于给图片裁剪使用的。图片裁剪为形状需要此优化
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, isStroke: false);

            //  <path fill="none">
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(x1, y1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            wR = wd2;
            hR = hd2;
            ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //  </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            // <rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
