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
    /// 左中括号
    /// </summary>
    public class LeftBracketGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 8333" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 8333d;

            //当adj为最低为0，导致一些值为0，参与公式乘除运算，导致路径有误
            adj = System.Math.Max(adj, 0.1);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 50000 h ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="y1" fmla="*/ ss a 100000" />
            //  <gd name="y2" fmla="+- b 0 y1" />
            //  <gd name="dx1" fmla="cos w 2700000" />
            //  <gd name="dy1" fmla="sin y1 2700000" />
            //  <gd name="il" fmla="+- r 0 dx1" />
            //  <gd name="it" fmla="+- y1 0 dy1" />
            //  <gd name="ib" fmla="+- b dy1 y1" />
            //</gdLst>

            //  <gd name="maxAdj" fmla="*/ 50000 h ss" />
            var maxAdj = 50000 * h / ss;
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //  <gd name="y1" fmla="*/ ss a 100000" />
            var y1 = ss * a / 100000;
            //  <gd name="y2" fmla="+- b 0 y1" />
            var y2 = b - y1;
            //  <gd name="dx1" fmla="cos w 2700000" />
            var dx1 = Cos(w, 2700000);
            //  <gd name="dy1" fmla="sin y1 2700000" />
            var dy1 = Sin(y1, 2700000);
            //  <gd name="il" fmla="+- r 0 dx1" />
            var il = r - dx1;
            //  <gd name="it" fmla="+- y1 0 dy1" />
            var it = y1 - dy1;
            //  <gd name="ib" fmla="+- b dy1 y1" />
            var ib = b + dy1 - y1;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            //    <arcTo wR="w" hR="y1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            //    <arcTo wR="w" hR="y1" stAng="cd2" swAng="cd4" />
            //    <close />
            //  </path>

            //  <path fill="none">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            //    <arcTo wR="w" hR="y1" stAng="cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            //    <arcTo wR="w" hR="y1" stAng="cd2" swAng="cd4" />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[2];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            var currentPoint = new EmuPoint(r, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="w" hR="y1" stAng="cd4" swAng="cd4" />
            var wR = w;
            var hR = y1;
            var stAng = cd4;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y1);
            //    <arcTo wR="w" hR="y1" stAng="cd2" swAng="cd4" />
            wR = w;
            hR = y1;
            stAng = cd2;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, isStroke: false);

            //  <path fill="none">
            //    <moveTo>
            //      <pt x="r" y="b" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(r, b);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="w" hR="y1" stAng="cd4" swAng="cd4" />
            wR = w;
            hR = y1;
            stAng = cd4;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y1);
            //    <arcTo wR="w" hR="y1" stAng="cd2" swAng="cd4" />
            wR = w;
            hR = y1;
            stAng = cd2;
            swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //  </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            return shapePaths;
        }


    }
}
