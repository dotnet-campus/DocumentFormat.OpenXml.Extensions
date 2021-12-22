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
    /// 不完整圆
    /// </summary>
    public class PieGeometry : ShapeGeometryBase
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
            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 0" />
            //  <gd name="adj2" fmla="val 16200000" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 0d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 16200000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="stAng" fmla="pin 0 adj1 21599999" />
            //  <gd name="enAng" fmla="pin 0 adj2 21599999" />
            //  <gd name="sw1" fmla="+- enAng 0 stAng" />
            //  <gd name="sw2" fmla="+- sw1 21600000 0" />
            //  <gd name="swAng" fmla="?: sw1 sw1 sw2" />
            //  <gd name="wt1" fmla="sin wd2 stAng" />
            //  <gd name="ht1" fmla="cos hd2 stAng" />
            //  <gd name="dx1" fmla="cat2 wd2 ht1 wt1" />
            //  <gd name="dy1" fmla="sat2 hd2 ht1 wt1" />
            //  <gd name="x1" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc dy1 0" />
            //  <gd name="wt2" fmla="sin wd2 enAng" />
            //  <gd name="ht2" fmla="cos hd2 enAng" />
            //  <gd name="dx2" fmla="cat2 wd2 ht2 wt2" />
            //  <gd name="dy2" fmla="sat2 hd2 ht2 wt2" />
            //  <gd name="x2" fmla="+- hc dx2 0" />
            //  <gd name="y2" fmla="+- vc dy2 0" />
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>

            //  <gd name="stAng" fmla="pin 0 adj1 21599999" />
            var stAng = Pin(0, adj1, 21599999);
            //  <gd name="enAng" fmla="pin 0 adj2 21599999" />
            var enAng = Pin(0, adj2, 21599999);
            //  <gd name="sw1" fmla="+- enAng 0 stAng" />
            var sw1 = enAng - stAng;
            //  <gd name="sw2" fmla="+- sw1 21600000 0" />
            var sw2 = sw1 + 21600000;
            //  <gd name="swAng" fmla="?: sw1 sw1 sw2" />
            var swAng = sw1 > 0 ? sw1 : sw2;
            //  <gd name="wt1" fmla="sin wd2 stAng" />
            var wt1 = Sin(wd2, (int) stAng);
            //  <gd name="ht1" fmla="cos hd2 stAng" />
            var ht1 = Cos(hd2, (int) stAng);
            //  <gd name="dx1" fmla="cat2 wd2 ht1 wt1" />
            var dx1 = Cat2(wd2, ht1, wt1);
            //  <gd name="dy1" fmla="sat2 hd2 ht1 wt1" />
            var dy1 = Sat2(hd2, ht1, wt1);
            //  <gd name="x1" fmla="+- hc dx1 0" />
            var x1 = hc + dx1;
            //  <gd name="y1" fmla="+- vc dy1 0" />
            var y1 = vc + dy1;
            //  <gd name="wt2" fmla="sin wd2 enAng" />
            var wt2 = Sin(wd2, (int) enAng);
            //  <gd name="ht2" fmla="cos hd2 enAng" />
            var ht2 = Cos(hd2, (int) enAng);
            //  <gd name="dx2" fmla="cat2 wd2 ht2 wt2" />
            var dx2 = Cat2(wd2, ht2, wt2);
            //  <gd name="dy2" fmla="sat2 hd2 ht2 wt2" />
            var dy2 = Sat2(hd2, ht2, wt2);
            //  <gd name="x2" fmla="+- hc dx2 0" />
            var x2 = hc + dx2;
            //  <gd name="y2" fmla="+- vc dy2 0" />
            var y2 = vc + dy2;
            //  <gd name="idx" fmla="cos wd2 2700000" />
            var idx = Cos(wd2, 2700000);
            //  <gd name="idy" fmla="sin hd2 2700000" />
            var idy = Sin(hd2, 2700000);
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
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng" swAng="swAng" />
            //    <lnTo>
            //      <pt x="hc" y="vc" />
            //    </lnTo>
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
            //      <pt x="hc" y="vc" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, hc, vc);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="il" t="ir" r="it" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, ir, it, ib);

            return shapePaths;
        }
    }
}
