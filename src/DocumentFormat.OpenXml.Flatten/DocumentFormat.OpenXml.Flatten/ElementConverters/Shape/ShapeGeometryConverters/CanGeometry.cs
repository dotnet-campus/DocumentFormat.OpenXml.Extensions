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
    /// 圆柱体
    /// </summary>
    public class CanGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 25000" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 25000d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="maxAdj" fmla="*/ 50000 h ss" />
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            //  <gd name="y1" fmla="*/ ss a 200000" />
            //  <gd name="y2" fmla="+- y1 y1 0" />
            //  <gd name="y3" fmla="+- b 0 y1" />
            //</gdLst>


            //  <gd name="maxAdj" fmla="*/ 50000 h ss" />
            var maxAdj = 50000 * h / ss;
            //  <gd name="a" fmla="pin 0 adj maxAdj" />
            var a = Pin(0, adj, maxAdj);
            //  <gd name="y1" fmla="*/ ss a 200000" />
            var y1 = ss * a / 200000;
            //  <gd name="y2" fmla="+- y1 y1 0" />
            var y2 = y1 + y1;
            //  <gd name="y3" fmla="+- b 0 y1" />
            var y3 = b - y1;

            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="-10800000" />
            //    <lnTo>
            //      <pt x="r" y="y3" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            //    <close />
            //  </path>
            //  <path stroke="false" fill="lighten" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd2" />
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd2" />
            //    <lnTo>
            //      <pt x="r" y="y3" />
            //    </lnTo>
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[3];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="-10800000" />
            var wR = wd2;
            var hR = y1;
            var stAng = cd2;
            var swAng = -10800000d;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="r" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y3);
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            wR = wd2;
            hR = y1;
            stAng = 0;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);



            //  <path stroke="false" fill="lighten" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(l, y1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd2" />
            wR = wd2;
            hR = y1;
            stAng = cd2;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            wR = wd2;
            hR = y1;
            stAng = 0;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.Lighten, isStroke: false);

            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="r" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(r, y1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            wR = wd2;
            hR = y1;
            stAng = 0;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="wd2" hR="y1" stAng="cd2" swAng="cd2" />
            wR = wd2;
            hR = y1;
            stAng = cd2;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="r" y="y3" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, r, y3);
            //    <arcTo wR="wd2" hR="y1" stAng="0" swAng="cd2" />
            wR = wd2;
            hR = y1;
            stAng = 0;
            swAng = cd2;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <lnTo>
            //      <pt x="l" y="y1" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, l, y1);
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="l" t="y2" r="r" b="y3" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, y2, r, y3);

            return shapePaths;
        }
    }
}
