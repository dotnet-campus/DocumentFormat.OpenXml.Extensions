using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 动作按钮: 文档
    /// </summary>
    public class ActionButtonDocumentGeometry : ShapeGeometryBase
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
            //  <gd name="dx2" fmla="*/ ss 3 8" />
            //  <gd name="g9" fmla="+- vc 0 dx2" />
            //  <gd name="g10" fmla="+- vc dx2 0" />
            //  <gd name="dx1" fmla="*/ ss 9 32" />
            //  <gd name="g11" fmla="+- hc 0 dx1" />
            //  <gd name="g12" fmla="+- hc dx1 0" />
            //  <gd name="g13" fmla="*/ ss 3 16" />
            //  <gd name="g14" fmla="+- g12 0 g13" />
            //  <gd name="g15" fmla="+- g9 g13 0" />
            //</gdLst>

            //<gd name="dx2" fmla="*/ ss 3 8" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx2 = ss * 3 / 8;
            //<gd name="g9" fmla="+- vc 0 dx2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g9 = vc + 0 - dx2;
            //<gd name="g10" fmla="+- vc dx2 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g10 = vc + dx2 - 0;
            //<gd name="dx1" fmla="*/ ss 9 32" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dx1 = ss * 9 / 32;
            //<gd name="g11" fmla="+- hc 0 dx1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g11 = hc + 0 - dx1;
            //<gd name="g12" fmla="+- hc dx1 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g12 = hc + dx1 - 0;
            //<gd name="g13" fmla="*/ ss 3 16" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g13 = ss * 3 / 16;
            //<gd name="g14" fmla="+- g12 0 g13" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g14 = g12 + 0 - g13;
            //<gd name="g15" fmla="+- g9 g13 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var g15 = g9 + g13 - 0;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path stroke="false" extrusionOk="false">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="g11" y="g9" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g14" y="g9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g11" y="g10" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darkenLess" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g11" y="g9" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g14" y="g9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g14" y="g15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g11" y="g10" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path stroke="false" fill="darken" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g14" y="g9" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g14" y="g15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g15" />
            //    </lnTo>
            //    <close />
            //  </path>
            //  <path fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="g11" y="g9" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g14" y="g9" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g12" y="g10" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g11" y="g10" />
            //    </lnTo>
            //    <close />
            //    <moveTo>
            //      <pt x="g12" y="g15" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="g14" y="g15" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="g14" y="g9" />
            //    </lnTo>
            //  </path>
            //  <path fill="none">
            //    <moveTo>
            //      <pt x="l" y="t" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="r" y="t" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="r" y="b" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="l" y="b" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[5];

            // <path stroke="false"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="t" />
            //</moveTo>
            var currentPoint = new EmuPoint(l, t);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g9);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g10);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString(), isStroke: false);


            // <path stroke="false"fill="darkenLess"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g9);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g10);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[1] = new ShapePath(stringPath.ToString(), PathFillModeValues.DarkenLess, isStroke: false);


            // <path stroke="false"fill="darken"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(g14, g9);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g15);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[2] = new ShapePath(stringPath.ToString(), PathFillModeValues.Darken, isStroke: false);


            // <path fill="none"extrusionOk="false">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g9" />
            //</moveTo>
            currentPoint = new EmuPoint(g11, g9);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g9);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g12, g10);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g11" y="g10" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g11, g10);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g12" y="g15" />
            //</moveTo>
            currentPoint = new EmuPoint(g12, g15);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g15" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g15);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="g14" y="g9" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, g14, g9);
            shapePaths[3] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            // <path fill="none">
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="t" />
            //</moveTo>
            currentPoint = new EmuPoint(l, t);
            stringPath.Clear();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="t" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, t);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="r" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, r, b);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="l" y="b" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, l, b);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[4] = new ShapePath(stringPath.ToString(), PathFillModeValues.None);


            //<rect l="l" t="t" r="r" b="b" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(l, t, r, b);

            return shapePaths;
        }
    }


}

