using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 楔饼形
    /// </summary>
    public class PieWedgeGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        /// <inheritdoc />
        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="g1" fmla="cos w 13500000" />
            //  <gd name="g2" fmla="sin h 13500000" />
            //  <gd name="x1" fmla="+- r g1 0" />
            //  <gd name="y1" fmla="+- b g2 0" />
            //</gdLst>

            //<gd name="g1" fmla="cos w 13500000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g1 = Cos(w, (int) 13500000);
            //<gd name="g2" fmla="sin h 13500000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g2 = Sin(h, (int) 13500000);
            //<gd name="x1" fmla="+- r g1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = r + g1 - 0;
            //<gd name="y1" fmla="+- b g2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = b + g2 - 0;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="l" y="b" />
            //    </moveTo>
            //    <arcTo wR="w" hR="h" stAng="cd2" swAng="cd4" />
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, b);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<arcTo wR="w" hR="h" stAng="cd2" swAng="cd4" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            currentPoint = ArcToToString(stringPath, currentPoint, w, h, cd2, cd4);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="x1" t="y1" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y1, r, b);

            return shapePaths;
        }
    }


}

