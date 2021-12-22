using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    ///     菱形
    /// </summary>
    internal class DiamondGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="ir" fmla="*/ w 3 4" />
            //  <gd name="ib" fmla="*/ h 3 4" />
            //</gdLst>

            //  <gd name="ir" fmla="*/ w 3 4" />
            var ir = w * 3 / 4;
            //  <gd name="ib" fmla="*/ h 3 4" />
            var ib = h * 3 / 4;

            //  <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //     <path>
            //       <moveTo>
            //         <pt x="l" y="vc" />
            //       </moveTo>
            //       <lnTo>
            //         <pt x="hc" y="t" />
            //       </lnTo>
            //       <lnTo>
            //         <pt x="r" y="vc" />
            //       </lnTo>
            //       <lnTo>
            //         <pt x="hc" y="b" />
            //       </lnTo>
            //       <close />
            //     </path>
            //   </pathLst>

            var currentPoint = new EmuPoint(l, vc);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //       <lnTo>
            //         <pt x="hc" y="t" />
            //       </lnTo>
            _ = LineToToString(stringPath, hc, t);
            //       <lnTo>
            //         <pt x="r" y="vc" />
            //       </lnTo>
            _ = LineToToString(stringPath, r, vc);
            //       <lnTo>
            //         <pt x="hc" y="b" />
            //       </lnTo>
            _ = LineToToString(stringPath, hc, b);

            stringPath.Append("z");

            //<rect l="wd4" t="hd4" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(wd4, hd4, ir, ib);

            return stringPath.ToString();
        }

        public override ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var shapePaths = new ShapePath[1];
            shapePaths[0] = new ShapePath(ToGeometryPathString(emuSize, adjusts));

            return shapePaths;
        }
    }
}
