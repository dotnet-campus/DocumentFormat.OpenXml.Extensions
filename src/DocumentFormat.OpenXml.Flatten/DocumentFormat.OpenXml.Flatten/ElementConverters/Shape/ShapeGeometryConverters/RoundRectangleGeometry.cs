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
    ///     圆角矩形
    /// </summary>
    internal class RoundRectangleGeometry : ShapeGeometryBase
    {
        /// <inheritdoc />
        public override string ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);

            /*
                <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
                  <gd name="adj" fmla="val 16667" />
                </avLst>
            */
            var adjustValue = adjusts?.GetAdjustValue("adj");
            var adj = adjustValue ?? 16667d;

            // <gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //    <gd name="a" fmla="pin 0 adj 50000" />
            //    <gd name="x1" fmla="*/ ss a 100000" />
            //    <gd name="x2" fmla="+- r 0 x1" />
            //    <gd name="y2" fmla="+- b 0 x1" />
            //    <gd name="il" fmla="*/ x1 29289 100000" />
            //    <gd name="ir" fmla="+- r 0 il" />
            //    <gd name="ib" fmla="+- b 0 il" />
            // </gdLst>
            var a = Pin(0, adj, 50000);
            var x1 = ss * a / 100000;
            var x2 = r + 0 - x1;
            var y2 = b + 0 - x1;
            var il = x1 * 29289 / 100000;
            var ir = r + il;
            var ib = b - il;
            /*
 <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
   <path>
     <moveTo>
       <pt x="l" y="x1" />
     </moveTo>
     <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
     <lnTo>
       <pt x="x2" y="t" />
     </lnTo>
     <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="cd4" />
     <lnTo>
       <pt x="r" y="y2" />
     </lnTo>
     <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
     <lnTo>
       <pt x="x1" y="b" />
     </lnTo>
     <arcTo wR="x1" hR="x1" stAng="cd4" swAng="cd4" />
     <close />
   </path>
 </pathLst>
          */
            var stringPath = new StringBuilder();
            /*
                 <moveTo>
                   <pt x="l" y="x1" />
                 </moveTo>
             */
            var currentPoint = new EmuPoint(l, x1);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");

            // <arcTo wR="x1" hR="x1" stAng="cd2" swAng="cd4" />
            var wR = x1;
            var hR = x1;
            var stAng = cd2;
            var swAng = cd4;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            /*
                 <lnTo>
                   <pt x="x2" y="t" />
                 </lnTo>
             */
            currentPoint = LineToToString(stringPath, x2, t);
            //     <arcTo wR="x1" hR="x1" stAng="3cd4" swAng="cd4" />
            currentPoint = ArcToToString(stringPath, currentPoint, x1, x1, 3 * cd4, cd4);
            //     <lnTo>
            //       <pt x="r" y="y2" />
            //     </lnTo>
            currentPoint = LineToToString(stringPath, r, y2);
            //     <arcTo wR="x1" hR="x1" stAng="0" swAng="cd4" />
            currentPoint = ArcToToString(stringPath, currentPoint, x1, x1, 0, cd4);
            //     <lnTo>
            //       <pt x="x1" y="b" />
            //     </lnTo>
            currentPoint = LineToToString(stringPath, x1, b);
            //     <arcTo wR="x1" hR="x1" stAng="cd4" swAng="cd4" />
            _ = ArcToToString(stringPath, currentPoint, x1, x1, cd4, cd4);

            stringPath.Append("z");

            //<rect l="il" t="il" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, il, ir, ib);

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
