using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 减号
    /// </summary>
    public class MathMinusGeometry : ShapeGeometryBase
    {

        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="adj1" fmla="val 23520" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? 23520d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="a1" fmla="pin 0 adj1 100000" />
            //  <gd name="dy1" fmla="*/ h a1 200000" />
            //  <gd name="dx1" fmla="*/ w 73490 200000" />
            //  <gd name="y1" fmla="+- vc 0 dy1" />
            //  <gd name="y2" fmla="+- vc dy1 0" />
            //  <gd name="x1" fmla="+- hc 0 dx1" />
            //  <gd name="x2" fmla="+- hc dx1 0" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 100000);
            //<gd name="dy1" fmla="*/ h a1 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dy1 = h * a1 / 200000;
            //<gd name="dx1" fmla="*/ w 73490 200000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = w * 73490 / 200000;
            //<gd name="y1" fmla="+- vc 0 dy1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y1 = vc + 0 - dy1;
            //<gd name="y2" fmla="+- vc dy1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var y2 = vc + dy1 - 0;
            //<gd name="x1" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x1 = hc + 0 - dx1;
            //<gd name="x2" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var x2 = hc + dx1 - 0;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="x1" y="y1" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="x2" y="y1" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x2" y="y2" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="x1" y="y2" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y1" />
            //</moveTo>
            var currentPoint = new EmuPoint(x1, y1);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y1" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y1);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x2" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x2, y2);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="x1" y="y2" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, x1, y2);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="x1" t="y1" r="x2" b="y2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x1, y1, x2, y2);

            return shapePaths;
        }
    }


}

