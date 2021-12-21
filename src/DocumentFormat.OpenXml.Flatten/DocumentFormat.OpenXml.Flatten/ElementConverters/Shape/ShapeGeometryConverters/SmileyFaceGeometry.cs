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
    /// 笑脸
    /// </summary>
    public class SmileyFaceGeometry : ShapeGeometryBase
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
            //  <gd name="adj" fmla="val 4653" />
            //</avLst>
            var customAdj = adjusts?.GetAdjustValue("adj");
            var adj = customAdj ?? 4653d;

            // <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a" fmla="pin -4653 adj 4653" />
            //  <gd name="x1" fmla="*/ w 4969 21699" />
            //  <gd name="x2" fmla="*/ w 6215 21600" />
            //  <gd name="x3" fmla="*/ w 13135 21600" />
            //  <gd name="x4" fmla="*/ w 16640 21600" />
            //  <gd name="y1" fmla="*/ h 7570 21600" />
            //  <gd name="y3" fmla="*/ h 16515 21600" />
            //  <gd name="dy2" fmla="*/ h a 100000" />
            //  <gd name="y2" fmla="+- y3 0 dy2" />
            //  <gd name="y4" fmla="+- y3 dy2 0" />
            //  <gd name="dy3" fmla="*/ h a 50000" />
            //  <gd name="y5" fmla="+- y4 dy3 0" />
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //  <gd name="wR" fmla="*/ w 1125 21600" />
            //  <gd name="hR" fmla="*/ h 1125 21600" />
            //</gdLst>


            //  <gd name="a" fmla="pin -4653 adj 4653" />
            var a = Pin(-4653, adj, 4653);
            //  <gd name="x1" fmla="*/ w 4969 21699" />
            var x1 = w * 4969 / 21699;
            //  <gd name="x2" fmla="*/ w 6215 21600" />
            var x2 = w * 6215 / 21600;
            //  <gd name="x3" fmla="*/ w 13135 21600" />
            var x3 = w * 13135 / 21600;
            //  <gd name="x4" fmla="*/ w 16640 21600" />
            var x4 = w * 16640 / 21600;
            //  <gd name="y1" fmla="*/ h 7570 21600" />
            var y1 = h * 7570 / 21600;
            //  <gd name="y3" fmla="*/ h 16515 21600" />
            var y3 = h * 16515 / 21600;
            //  <gd name="dy2" fmla="*/ h a 100000" />
            var dy2 = h * a / 100000;
            //  <gd name="y2" fmla="+- y3 0 dy2" />
            var y2 = y3 - dy2;
            //  <gd name="y4" fmla="+- y3 dy2 0" />
            var y4 = y3 + dy2;
            //  <gd name="dy3" fmla="*/ h a 50000" />
            var dy3 = h * a / 50000;
            //  <gd name="y5" fmla="+- y4 dy3 0" />
            var y5 = y4 + dy3;
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
            //  <gd name="wR" fmla="*/ w 1125 21600" />
            var wR = w * 1125 / 21600;
            //  <gd name="hR" fmla="*/ h 1125 21600" />
            var hR = h * 1125 / 21600;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x2" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wR" hR="hR" stAng="cd2" swAng="21600000" />
            //    <moveTo>
            //      <pt x="x3" y="y1" />
            //    </moveTo>
            //    <arcTo wR="wR" hR="hR" stAng="cd2" swAng="21600000" />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="y2" />
            //    </moveTo>
            //    <quadBezTo>
            //      <pt x="hc" y="y5" />
            //      <pt x="x4" y="y2" />
            //    </quadBezTo>
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="21600000" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[4];
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="21600000" />
            var stAng = cd2;
            var swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            //  <path fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x2" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(x2, y1);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wR" hR="hR" stAng="cd2" swAng="21600000" />
            stAng = cd2;
            swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="x3" y="y1" />
            //    </moveTo>
            currentPoint = new EmuPoint(x3, y1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wR" hR="hR" stAng="cd2" swAng="21600000" />
            stAng = cd2;
            swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //  </path>
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess);

            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="x1" y="y2" />
            //    </moveTo>
            currentPoint = new EmuPoint(x1, y2);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <quadBezTo>
            //      <pt x="hc" y="y5" />
            //      <pt x="x4" y="y2" />
            //    </quadBezTo>
            currentPoint = QuadBezToString(stringPath, hc, y5, x4, y2);
            //  </path>
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            currentPoint = new EmuPoint(l, vc);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="21600000" />
            stAng = cd2;
            swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[3] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
