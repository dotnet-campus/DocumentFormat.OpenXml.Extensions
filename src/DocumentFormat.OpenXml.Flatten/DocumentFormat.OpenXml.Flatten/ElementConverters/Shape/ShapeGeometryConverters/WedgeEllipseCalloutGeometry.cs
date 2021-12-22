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
    /// 对话气泡：椭圆形
    /// </summary>
    class WedgeEllipseCalloutGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //  <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val -20833" />
            //  <gd name="adj2" fmla="val 62500" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? -20833d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 62500d;


            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="dxPos" fmla="*/ w adj1 100000" />
            //  <gd name="dyPos" fmla="*/ h adj2 100000" />
            //  <gd name="xPos" fmla="+- hc dxPos 0" />
            //  <gd name="yPos" fmla="+- vc dyPos 0" />
            //  <gd name="sdx" fmla="*/ dxPos h 1" />
            //  <gd name="sdy" fmla="*/ dyPos w 1" />
            //  <gd name="pang" fmla="at2 sdx sdy" />
            //  <gd name="stAng" fmla="+- pang 660000 0" />
            //  <gd name="enAng" fmla="+- pang 0 660000" />
            //  <gd name="dx1" fmla="cos wd2 stAng" />
            //  <gd name="dy1" fmla="sin hd2 stAng" />
            //  <gd name="x1" fmla="+- hc dx1 0" />
            //  <gd name="y1" fmla="+- vc dy1 0" />
            //  <gd name="dx2" fmla="cos wd2 enAng" />
            //  <gd name="dy2" fmla="sin hd2 enAng" />
            //  <gd name="x2" fmla="+- hc dx2 0" />
            //  <gd name="y2" fmla="+- vc dy2 0" />
            //  <gd name="stAng1" fmla="at2 dx1 dy1" />
            //  <gd name="enAng1" fmla="at2 dx2 dy2" />
            //  <gd name="swAng1" fmla="+- enAng1 0 stAng1" />
            //  <gd name="swAng2" fmla="+- swAng1 21600000 0" />
            //  <gd name="swAng" fmla="?: swAng1 swAng1 swAng2" />
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>

            //  <gd name="dxPos" fmla="*/ w adj1 100000" />
            var dxPos = w * adj1 / 100000;
            //  <gd name="dyPos" fmla="*/ h adj2 100000" />
            var dyPos = h * adj2 / 100000;
            //  <gd name="xPos" fmla="+- hc dxPos 0" />
            var xPos = hc + dxPos;
            //  <gd name="yPos" fmla="+- vc dyPos 0" />
            var yPos = vc + dyPos;
            //  <gd name="sdx" fmla="*/ dxPos h 1" />
            var sdx = dxPos * h / 1;
            //  <gd name="sdy" fmla="*/ dyPos w 1" />
            var sdy = dyPos * w / 1;
            //  <gd name="pang" fmla="at2 sdx sdy" />
            var pang = ATan2(sdx, sdy);
            //  <gd name="stAng" fmla="+- pang 660000 0" />
            var stAng = pang + 660000;
            //  <gd name="enAng" fmla="+- pang 0 660000" />
            var enAng = pang - 660000;
            //  <gd name="dx1" fmla="cos wd2 stAng" />
            var dx1 = Cos(wd2, (int) stAng);
            //  <gd name="dy1" fmla="sin hd2 stAng" />
            var dy1 = Sin(hd2, (int) stAng);
            //  <gd name="x1" fmla="+- hc dx1 0" />
            var x1 = hc + dx1;
            //  <gd name="y1" fmla="+- vc dy1 0" />
            var y1 = vc + dy1;
            //  <gd name="dx2" fmla="cos wd2 enAng" />
            var dx2 = Cos(wd2, (int) enAng);
            //  <gd name="dy2" fmla="sin hd2 enAng" />
            var dy2 = Sin(hd2, (int) enAng);
            //  <gd name="x2" fmla="+- hc dx2 0" />
            var x2 = hc + dx2;
            //  <gd name="y2" fmla="+- vc dy2 0" />
            var y2 = vc + dy2;
            //  <gd name="stAng1" fmla="at2 dx1 dy1" />
            var stAng1 = ATan2(dx1, dy1);
            //  <gd name="enAng1" fmla="at2 dx2 dy2" />
            var enAng1 = ATan2(dx2, dy2);
            //  <gd name="swAng1" fmla="+- enAng1 0 stAng1" />
            var swAng1 = enAng1 - stAng1;
            //  <gd name="swAng2" fmla="+- swAng1 21600000 0" />
            var swAng2 = swAng1 + 21600000;
            //  <gd name="swAng" fmla="?: swAng1 swAng1 swAng2" />
            var swAng = swAng1 > 0 ? swAng1 : swAng2;
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
            //      <pt x="xPos" y="yPos" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng1" swAng="swAng" />
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="xPos" y="yPos" />
            //    </moveTo>
            var currentPoint = new EmuPoint(xPos, yPos);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="x1" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y1);
            //    <arcTo wR="wd2" hR="hd2" stAng="stAng1" swAng="swAng" />
            var wR = wd2;
            var hR = hd2;
            stAng = stAng1;
            _ = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);

            stringPath.Append("z");

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return stringPath.ToString();
        }

        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var shapePaths = new ShapePath[1];
            shapePaths[0] = new ShapePath(ToGeometryPathString(emuSize, adjusts));

            return shapePaths;
        }
    }
}
