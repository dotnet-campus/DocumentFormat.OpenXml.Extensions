using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 流程图: 终止
    /// </summary>
    public class FlowChartTerminatorGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="il" fmla="*/ w 1018 21600" />
            //  <gd name="ir" fmla="*/ w 20582 21600" />
            //  <gd name="it" fmla="*/ h 3163 21600" />
            //  <gd name="ib" fmla="*/ h 18437 21600" />
            //</gdLst>

            //<gd name="il" fmla="*/ w 1018 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var il = w * 1018 / 21600;
            //<gd name="ir" fmla="*/ w 20582 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ir = w * 20582 / 21600;
            //<gd name="it" fmla="*/ h 3163 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var it = h * 3163 / 21600;
            //<gd name="ib" fmla="*/ h 18437 21600" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ib = h * 18437 / 21600;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="21600" h="21600">
            //    <moveTo>
            //      <pt x="3475" y="0" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="18125" y="0" />
            //    </lnTo>
            //    <arcTo wR="3475" hR="10800" stAng="3cd4" swAng="cd2" />
            //    <lnTo>
            //      <pt x="3475" y="21600" />
            //    </lnTo>
            //    <arcTo wR="3475" hR="10800" stAng="cd4" swAng="cd2" />
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path w="21600"h="21600">
            var shapePathWidth = 21600d;
            var shapePathHeight = 21600d;
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="3475" y="0" />
            //</moveTo>
            var currentPoint = new EmuPoint(3475, 0);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="18125" y="0" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 18125, 0);
            //<arcTo wR="3475" hR="10800" stAng="3cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3475d, 10800d, 3 * cd4, cd2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="3475" y="21600" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, 3475, 21600);
            //<arcTo wR="3475" hR="10800" stAng="cd4" swAng="cd2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, 3475d, 10800d, cd4, cd2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }


}

