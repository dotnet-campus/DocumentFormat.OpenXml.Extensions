using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 或者
    /// </summary>
    public class FlowChartOrGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="idx" fmla="cos wd2 2700000" />
            //  <gd name="idy" fmla="sin hd2 2700000" />
            //  <gd name="il" fmla="+- hc 0 idx" />
            //  <gd name="ir" fmla="+- hc idx 0" />
            //  <gd name="it" fmla="+- vc 0 idy" />
            //  <gd name="ib" fmla="+- vc idy 0" />
            //</gdLst>

            //<gd name="idx" fmla="cos wd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var idx = Cos(wd2, (int) 2700000);
            //<gd name="idy" fmla="sin hd2 2700000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var idy = Sin(hd2, (int) 2700000);
            //<gd name="il" fmla="+- hc 0 idx" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = hc + 0 - idx;
            //<gd name="ir" fmla="+- hc idx 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = hc + idx - 0;
            //<gd name="it" fmla="+- vc 0 idy" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var it = vc + 0 - idy;
            //<gd name="ib" fmla="+- vc idy 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = vc + idy - 0;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="hc" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="hc" y="b" />
            //    </lnTo>
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="vc" />
            //    </lnTo>
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="vc" />
            //    </moveTo>
            //    <arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" />
            //    <arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[3];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="vc" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, cd2, cd4);
            //<arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, 3 * cd4, cd4);
            //<arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, 0d, cd4);
            //<arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, cd4, cd4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="t" />
            //</moveTo>
            currentPoint = new EmuPoint(hc, t);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, b);
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="vc" />
            //</moveTo>
            currentPoint = new EmuPoint(l, vc);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, vc);
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            // <path fill="none">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="vc" />
            //</moveTo>
            currentPoint = new EmuPoint(l, vc);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="wd2" hR="hd2" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, cd2, cd4);
            //<arcTo wR="wd2" hR="hd2" stAng="3cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, 3 * cd4, cd4);
            //<arcTo wR="wd2" hR="hd2" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, 0d, cd4);
            //<arcTo wR="wd2" hR="hd2" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, wd2, hd2, cd4, cd4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

