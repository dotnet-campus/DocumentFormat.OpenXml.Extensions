using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 闪电形
    /// </summary>
    public class LightningBoltGeometry : ShapeGeometryBase
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
            //  <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="x1" fmla="*/ w 5022 21600" />
            //  <gd name="x3" fmla="*/ w 8472 21600" />
            //  <gd name="x4" fmla="*/ w 8757 21600" />
            //  <gd name="x5" fmla="*/ w 10012 21600" />
            //  <gd name="x8" fmla="*/ w 12860 21600" />
            //  <gd name="x9" fmla="*/ w 13917 21600" />
            //  <gd name="x11" fmla="*/ w 16577 21600" />
            //  <gd name="y1" fmla="*/ h 3890 21600" />
            //  <gd name="y2" fmla="*/ h 6080 21600" />
            //  <gd name="y4" fmla="*/ h 7437 21600" />
            //  <gd name="y6" fmla="*/ h 9705 21600" />
            //  <gd name="y7" fmla="*/ h 12007 21600" />
            //  <gd name="y10" fmla="*/ h 14277 21600" />
            //  <gd name="y11" fmla="*/ h 14915 21600" />
            //</gdLst>

            //  <gd name="x1" fmla="*/ w 5022 21600" />
            var x1 = w * 5022 / 21600;
            //  <gd name="x3" fmla="*/ w 8472 21600" />
            var x3 = w * 8472 / 21600;
            //  <gd name="x4" fmla="*/ w 8757 21600" />
            var x4 = w * 8757 / 21600;
            //  <gd name="x5" fmla="*/ w 10012 21600" />
            var x5 = w * 10012 / 21600;
            //  <gd name="x8" fmla="*/ w 12860 21600" />
            var x8 = w * 12860 / 21600;
            //  <gd name="x9" fmla="*/ w 13917 21600" />
            var x9 = w * 13917 / 21600;
            //  <gd name="x11" fmla="*/ w 16577 21600" />
            var x11 = w * 16577 / 21600;
            //  <gd name="y1" fmla="*/ h 3890 21600" />
            var y1 = h * 3890 / 21600;
            //  <gd name="y2" fmla="*/ h 6080 21600" />
            var y2 = h * 6080 / 21600;
            //  <gd name="y4" fmla="*/ h 7437 21600" />
            var y4 = h * 7437 / 21600;
            //  <gd name="y6" fmla="*/ h 9705 21600" />
            var y6 = h * 9705 / 21600;
            //  <gd name="y7" fmla="*/ h 12007 21600" />
            var y7 = h * 12007 / 21600;
            //  <gd name="y10" fmla="*/ h 14277 21600" />
            var y10 = h * 14277 / 21600;
            //  <gd name="y11" fmla="*/ h 14915 21600" />
            var y11 = h * 14915 / 21600;


            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="21600" h="21600">
            //    <moveTo>
            //      <pt x="8472" y="0" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="12860" y="6080" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="11050" y="6797" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="16577" y="12007" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="14767" y="12877" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="21600" y="21600" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="10012" y="14915" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="12222" y="13987" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="5022" y="9705" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="7602" y="8382" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="0" y="3890" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>



            var shapePaths = new ShapePath[1];
            //  <path w="21600" h="21600">
            var widthFactor = w / 21600;
            var heightFactor = h / 21600;
            //    <moveTo>
            //      <pt x="8472" y="0" />
            //    </moveTo>
            var currentPoint = new EmuPoint(widthFactor * 8472, 0);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <lnTo>
            //      <pt x="12860" y="6080" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 12860, heightFactor * 6080);
            //    <lnTo>
            //      <pt x="11050" y="6797" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 11050, heightFactor * 6797);
            //    <lnTo>
            //      <pt x="16577" y="12007" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 16577, heightFactor * 12007);
            //    <lnTo>
            //      <pt x="14767" y="12877" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 14767, heightFactor * 12877);
            //    <lnTo>
            //      <pt x="21600" y="21600" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 21600, heightFactor * 21600);
            //    <lnTo>
            //      <pt x="10012" y="14915" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 10012, heightFactor * 14915);
            //    <lnTo>
            //      <pt x="12222" y="13987" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 12222, heightFactor * 13987);
            //    <lnTo>
            //      <pt x="5022" y="9705" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 5022, heightFactor * 9705);
            //    <lnTo>
            //      <pt x="7602" y="8382" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, widthFactor * 7602, heightFactor * 8382);
            //    <lnTo>
            //      <pt x="0" y="3890" />
            //    </lnTo>
            currentPoint = LineToToString(stringPath, 0, heightFactor * 3890);
            //    <close />
            //  </path>
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());

            //<rect l="x4" t="y4" r="x9" b="y10" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(x4, y4, x9, y10);

            return shapePaths;
        }
    }
}
