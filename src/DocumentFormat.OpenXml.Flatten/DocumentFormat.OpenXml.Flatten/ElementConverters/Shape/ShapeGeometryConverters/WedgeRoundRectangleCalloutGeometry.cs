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
    /// 对话气泡：圆角矩形
    /// </summary>
    class WedgeRoundRectangleCalloutGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            //< avLst xmlns = "http://schemas.openxmlformats.org/drawingml/2006/main" >
            //  <gd name="adj1" fmla="val -20833" />
            //  <gd name="adj2" fmla="val 62500" />
            //  <gd name="adj3" fmla="val 16667" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? (-20833d);
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 62500d;
            var customAdj3 = adjusts?.GetAdjustValue("adj3");
            var adj3 = customAdj3 ?? 16667d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="dxPos" fmla="*/ w adj1 100000" />
            //  <gd name="dyPos" fmla="*/ h adj2 100000" />
            //  <gd name="xPos" fmla="+- hc dxPos 0" />
            //  <gd name="yPos" fmla="+- vc dyPos 0" />
            //  <gd name="dq" fmla="*/ dxPos h w" />
            //  <gd name="ady" fmla="abs dyPos" />
            //  <gd name="adq" fmla="abs dq" />
            //  <gd name="dz" fmla="+- ady 0 adq" />
            //  <gd name="xg1" fmla="?: dxPos 7 2" />
            //  <gd name="xg2" fmla="?: dxPos 10 5" />
            //  <gd name="x1" fmla="*/ w xg1 12" />
            //  <gd name="x2" fmla="*/ w xg2 12" />
            //  <gd name="yg1" fmla="?: dyPos 7 2" />
            //  <gd name="yg2" fmla="?: dyPos 10 5" />
            //  <gd name="y1" fmla="*/ h yg1 12" />
            //  <gd name="y2" fmla="*/ h yg2 12" />
            //  <gd name="t1" fmla="?: dxPos l xPos" />
            //  <gd name="xl" fmla="?: dz l t1" />
            //  <gd name="t2" fmla="?: dyPos x1 xPos" />
            //  <gd name="xt" fmla="?: dz t2 x1" />
            //  <gd name="t3" fmla="?: dxPos xPos r" />
            //  <gd name="xr" fmla="?: dz r t3" />
            //  <gd name="t4" fmla="?: dyPos xPos x1" />
            //  <gd name="xb" fmla="?: dz t4 x1" />
            //  <gd name="t5" fmla="?: dxPos y1 yPos" />
            //  <gd name="yl" fmla="?: dz y1 t5" />
            //  <gd name="t6" fmla="?: dyPos t yPos" />
            //  <gd name="yt" fmla="?: dz t6 t" />
            //  <gd name="t7" fmla="?: dxPos yPos y1" />
            //  <gd name="yr" fmla="?: dz y1 t7" />
            //  <gd name="t8" fmla="?: dyPos yPos b" />
            //  <gd name="yb" fmla="?: dz t8 b" />
            //  <gd name="u1" fmla="*/ ss adj3 100000" />
            //  <gd name="u2" fmla="+- r 0 u1" />
            //  <gd name="v2" fmla="+- b 0 u1" />
            //  <gd name="il" fmla="*/ u1 29289 100000" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 il" />
            //</gdLst>

            //  <gd name="dxPos" fmla="*/ w adj1 100000" />
            var dxPos = w * adj1 / 100000;
            //  <gd name="dyPos" fmla="*/ h adj2 100000" />
            var dyPos = h * adj2 / 100000;
            //  <gd name="xPos" fmla="+- hc dxPos 0" />
            var xPos = hc + dxPos;
            //  <gd name="yPos" fmla="+- vc dyPos 0" />
            var yPos = vc + dyPos;
            //  <gd name="dq" fmla="*/ dxPos h w" />
            var dq = dxPos * h / w;
            //  <gd name="ady" fmla="abs dyPos" />
            var ady = Abs(dyPos);
            //  <gd name="adq" fmla="abs dq" />
            var adq = Abs(dq);
            //  <gd name="dz" fmla="+- ady 0 adq" />
            var dz = ady - adq;
            //  <gd name="xg1" fmla="?: dxPos 7 2" />
            var xg1 = dxPos > 0 ? 7 : 2;
            //  <gd name="xg2" fmla="?: dxPos 10 5" />
            var xg2 = dxPos > 0 ? 10 : 5;
            //  <gd name="x1" fmla="*/ w xg1 12" />
            var x1 = w * xg1 / 12;
            //  <gd name="x2" fmla="*/ w xg2 12" />
            var x2 = w * xg2 / 12;
            //  <gd name="yg1" fmla="?: dyPos 7 2" />
            var yg1 = dyPos > 0 ? 7 : 2;
            //  <gd name="yg2" fmla="?: dyPos 10 5" />
            var yg2 = dyPos > 0 ? 10 : 5;
            //  <gd name="y1" fmla="*/ h yg1 12" />
            var y1 = h * yg1 / 12;
            //  <gd name="y2" fmla="*/ h yg2 12" />
            var y2 = h * yg2 / 12;
            //  <gd name="t1" fmla="?: dxPos l xPos" />
            var t1 = dxPos > 0 ? l : xPos;
            //  <gd name="xl" fmla="?: dz l t1" />
            var xl = dz > 0 ? l : t1;
            //  <gd name="t2" fmla="?: dyPos x1 xPos" />
            var t2 = dyPos > 0 ? x1 : xPos;
            //  <gd name="xt" fmla="?: dz t2 x1" />
            var xt = dz > 0 ? t2 : x1;
            //  <gd name="t3" fmla="?: dxPos xPos r" />
            var t3 = dxPos > 0 ? xPos : r;
            //  <gd name="xr" fmla="?: dz r t3" />
            var xr = dz > 0 ? r : t3;
            //  <gd name="t4" fmla="?: dyPos xPos x1" />
            var t4 = dyPos > 0 ? xPos : x1;
            //  <gd name="xb" fmla="?: dz t4 x1" />
            var xb = dz > 0 ? t4 : x1;
            //  <gd name="t5" fmla="?: dxPos y1 yPos" />
            var t5 = dxPos > 0 ? y1 : yPos;
            //  <gd name="yl" fmla="?: dz y1 t5" />
            var yl = dz > 0 ? y1 : t5;
            //  <gd name="t6" fmla="?: dyPos t yPos" />
            var t6 = dyPos > 0 ? t : yPos;
            //  <gd name="yt" fmla="?: dz t6 t" />
            var yt = dz > 0 ? t6 : t;
            //  <gd name="t7" fmla="?: dxPos yPos y1" />
            var t7 = dxPos > 0 ? yPos : y1;
            //  <gd name="yr" fmla="?: dz y1 t7" />
            var yr = dz > 0 ? y1 : t7;
            //  <gd name="t8" fmla="?: dyPos yPos b" />
            var t8 = dyPos > 0 ? yPos : b;
            //  <gd name="yb" fmla="?: dz t8 b" />
            var yb = dz > 0 ? t8 : b;
            //  <gd name="u1" fmla="*/ ss adj3 100000" />
            var u1 = ss * adj3 / 100000;
            //  <gd name="u2" fmla="+- r 0 u1" />
            var u2 = r - u1;
            //  <gd name="v2" fmla="+- b 0 u1" />
            var v2 = b - u1;
            //  <gd name="il" fmla="*/ u1 29289 100000" />
            var il = u1 * 29289 / 100000;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="ib" fmla="+- b 0 il" />
            var ib = b - il;

            //  <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="u1" />
            //    </moveTo>
            //    <arcTo wR="u1" hR="u1" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xt" y="yt" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="u2" y="t" />
            //    </lnTo>
            //    <arcTo wR="u1" hR="u1" stAng="3cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xr" y="yr" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="v2" />
            //    </lnTo>
            //    <arcTo wR="u1" hR="u1" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xb" y="yb" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="u1" y="b" />
            //    </lnTo>
            //    <arcTo wR="u1" hR="u1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xl" y="yl" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            //    <moveTo>
            //      <pt x="l" y="u1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, u1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="u1" hR="u1" stAng="cd2" swAng="cd4" />
            var wR = u1;
            var hR = u1;
            var stAng = cd2;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x1" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, t);
            //    <lnTo>
            //      <pt x="xt" y="yt" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xt, yt);
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //    <lnTo>
            //      <pt x="u2" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, u2, t);
            //    <arcTo wR="u1" hR="u1" stAng="3cd4" swAng="cd4" />
            wR = u1;
            hR = u1;
            stAng = 3 * cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="r" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y1);
            //    <lnTo>
            //      <pt x="xr" y="yr" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xr, yr);
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //    <lnTo>
            //      <pt x="r" y="v2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, v2);
            //    <arcTo wR="u1" hR="u1" stAng="0" swAng="cd4" />
            wR = u1;
            hR = u1;
            stAng = 0;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x2" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x2, b);
            //    <lnTo>
            //      <pt x="xb" y="yb" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xb, yb);
            //    <lnTo>
            //      <pt x="x1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //    <lnTo>
            //      <pt x="u1" y="b" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, u1, b);
            //    <arcTo wR="u1" hR="u1" stAng="cd4" swAng="cd4" />
            wR = u1;
            hR = u1;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="l" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y2);
            //    <lnTo>
            //      <pt x="xl" y="yl" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, xl, yl);
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            _ = LineToToString(stringPath, l, y1);

            stringPath.Append("z");

            //<rect l="il" t="il" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, ib);
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
