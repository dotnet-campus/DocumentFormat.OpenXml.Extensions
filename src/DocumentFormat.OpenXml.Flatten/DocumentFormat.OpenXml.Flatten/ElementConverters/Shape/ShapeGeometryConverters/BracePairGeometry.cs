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
    /// 双大括号
    /// </summary>
    public class BracePairGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 8333" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 8333d;

            //当adj为最低为0，导致一些值为0，参与公式乘除运算，导致路径有误
            adj = System.Math.Max(adj, 0.1);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin 0 adj 25000" />
            //  <gd name="x1" fmla="*/ ss a 100000" />
            //  <gd name="x2" fmla="*/ ss a 50000" />
            //  <gd name="x3" fmla="+- r 0 x2" />
            //  <gd name="x4" fmla="+- r 0 x1" />
            //  <gd name="y2" fmla="+- vc 0 x1" />
            //  <gd name="y3" fmla="+- vc x1 0" />
            //  <gd name="y4" fmla="+- b 0 x1" />
            //  <gd name="it" fmla="*/ x1 29289 100000" />
            //  <gd name="il" fmla="+- x1 it 0" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 it" />
            //</gdLst>

            //  <gd name="a" fmla="pin 0 adj 25000" />
            var a = Pin(0, adj, 25000);
            //  <gd name="x1" fmla="*/ ss a 100000" />
            var x1 = ss * a / 100000;
            //  <gd name="x2" fmla="*/ ss a 50000" />
            var x2 = ss * a / 50000;
            //  <gd name="x3" fmla="+- r 0 x2" />
            var x3 = r - x2;
            //  <gd name="x4" fmla="+- r 0 x1" />
            var x4 = r - x1;
            //  <gd name="y2" fmla="+- vc 0 x1" />
            var y2 = vc - x1;
            //  <gd name="y3" fmla="+- vc x1 0" />
            var y3 = vc + x1;
            //  <gd name="y4" fmla="+- b 0 x1" />
            var y4 = b - x1;
            //  <gd name="it" fmla="*/ x1 29289 100000" />
            var it = x1 * 29289 / 100000;
            //  <gd name="il" fmla="+- x1 it 0" />
            var il = x1 + it;
            //  <gd name="ir" fmla="+- r 0 il" />
            var ir = r - il;
            //  <gd name="ib" fmla="+- b 0 it" />
            var ib = b - it;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x2" y="b" />
            //    </moveTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="-5400000" />
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="x1" y="x1" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="-5400000" />
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            //    <close />
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="x2" y="b" />
            //    </moveTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="-5400000" />
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="x1" y="x1" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            //    <moveTo>
            //      <pt x="x3" y="t" />
            //    </moveTo>
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="-5400000" />
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="-5400000" />
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x2" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(x2, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="cd4" />
            var wR = x1;
            var hR = x1;
            var stAng = cd4;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = 0;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x1" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, x1);
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = cd2;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x3" y="t" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x3, t);
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = 3 * cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = cd2;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = 3 * cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y4);
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = 0;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, isStroke: false);

            //  <path fill="none">
            //    <moveTo>
            //      <pt x="x2" y="b" />
            //    </moveTo>
            currentPoint = new EmuPoint(x2, b);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x1" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, y3);
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = 0;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="x1" hR="x1" stAng="cd4" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x1" y="x1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x1, x1);
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = cd2;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            // 这是特别再切成两段 - 政道
            //    <moveTo>
            //      <pt x="x3" y="t" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x3, t);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = 3 * cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x4" y="y2" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y2);
            //    <arcTo wR="x1" hR="x1" stAng="cd2" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = cd2;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="-5400000" />
            wR = x1;
            hR = x1;
            stAng = 3 * cd4;
            swAng = -5400000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="x4" y="y4" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, x4, y4);
            //    <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            wR = x1;
            hR = x1;
            stAng = 0;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="il" t="il" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, ib);

            return shapePaths;
        }
    }
}
