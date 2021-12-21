using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 可选过程
    /// </summary>
    public class FlowChartAlternateProcessGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x2" fmla="+- r 0 ssd6" />
            //  <gd name="y2" fmla="+- b 0 ssd6" />
            //  <gd name="il" fmla="*/ ssd6 29289 100000" />
            //  <gd name="ir" fmla="+- r 0 il" />
            //  <gd name="ib" fmla="+- b 0 il" />
            //</gdLst>

            var ssd6 = ss / 6;

            //<gd name="x2" fmla="+- r 0 ssd6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = r + 0 - ssd6;
            //<gd name="y2" fmla="+- b 0 ssd6" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = b + 0 - ssd6;
            //<gd name="il" fmla="*/ ssd6 29289 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = ssd6 * 29289 / 100000;
            //<gd name="ir" fmla="+- r 0 il" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = r + 0 - il;
            //<gd name="ib" fmla="+- b 0 il" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = b + 0 - il;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="ssd6" />
            //    </moveTo>
            //    <arcTo wR="ssd6" hR="ssd6" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="x2" y="t" />
            //    </lnTo>
            //    <arcTo wR="ssd6" hR="ssd6" stAng="3cd4" swAng="cd4" />
            //    <lnTo>
            //      <pt x="r" y="y2" />
            //    </lnTo>
            //    <arcTo wR="ssd6" hR="ssd6" stAng="0" swAng="cd4" />
            //    <lnTo>
            //      <pt x="ssd6" y="b" />
            //    </lnTo>
            //    <arcTo wR="ssd6" hR="ssd6" stAng="cd4" swAng="cd4" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];
            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="ssd6" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, ssd6);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="ssd6" hR="ssd6" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ssd6, ssd6, cd2, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, t);
            //<arcTo wR="ssd6" hR="ssd6" stAng="3cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ssd6, ssd6, 3 * cd4, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //<arcTo wR="ssd6" hR="ssd6" stAng="0" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ssd6, ssd6, 0d, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="ssd6" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, ssd6, b);
            //<arcTo wR="ssd6" hR="ssd6" stAng="cd4" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, ssd6, ssd6, cd4, cd4);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="il" t="il" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, ib);

            return shapePaths;
        }
    }


}

