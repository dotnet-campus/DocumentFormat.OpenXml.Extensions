using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 乘号
    /// </summary>
    public class MathMultiplyGeometry : ShapeGeometryBase
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
            //  <gd name="a1" fmla="pin 0 adj1 51965" />
            //  <gd name="th" fmla="*/ ss a1 100000" />
            //  <gd name="a" fmla="at2 w h" />
            //  <gd name="sa" fmla="sin 1 a" />
            //  <gd name="ca" fmla="cos 1 a" />
            //  <gd name="ta" fmla="tan 1 a" />
            //  <gd name="dl" fmla="mod w h 0" />
            //  <gd name="rw" fmla="*/ dl 51965 100000" />
            //  <gd name="lM" fmla="+- dl 0 rw" />
            //  <gd name="xM" fmla="*/ ca lM 2" />
            //  <gd name="yM" fmla="*/ sa lM 2" />
            //  <gd name="dxAM" fmla="*/ sa th 2" />
            //  <gd name="dyAM" fmla="*/ ca th 2" />
            //  <gd name="xA" fmla="+- xM 0 dxAM" />
            //  <gd name="yA" fmla="+- yM dyAM 0" />
            //  <gd name="xB" fmla="+- xM dxAM 0" />
            //  <gd name="yB" fmla="+- yM 0 dyAM" />
            //  <gd name="xBC" fmla="+- hc 0 xB" />
            //  <gd name="yBC" fmla="*/ xBC ta 1" />
            //  <gd name="yC" fmla="+- yBC yB 0" />
            //  <gd name="xD" fmla="+- r 0 xB" />
            //  <gd name="xE" fmla="+- r 0 xA" />
            //  <gd name="yFE" fmla="+- vc 0 yA" />
            //  <gd name="xFE" fmla="*/ yFE 1 ta" />
            //  <gd name="xF" fmla="+- xE 0 xFE" />
            //  <gd name="xL" fmla="+- xA xFE 0" />
            //  <gd name="yG" fmla="+- b 0 yA" />
            //  <gd name="yH" fmla="+- b 0 yB" />
            //  <gd name="yI" fmla="+- b 0 yC" />
            //  <gd name="xC2" fmla="+- r 0 xM" />
            //  <gd name="yC3" fmla="+- b 0 yM" />
            //</gdLst>

            //<gd name="a1" fmla="pin 0 adj1 51965" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a1 = Pin(0, adj1, 51965);
            //<gd name="th" fmla="*/ ss a1 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var th = ss * a1 / 100000;
            //<gd name="a" fmla="at2 w h" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var a = ATan2(w, h);
            //<gd name="sa" fmla="sin 1 a" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var sa = Sin(1, (int) a);
            //<gd name="ca" fmla="cos 1 a" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ca = Cos(1, (int) a);
            //<gd name="ta" fmla="tan 1 a" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var ta = Tan(1, (int) a);
            //<gd name="dl" fmla="mod w h 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dl = Mod(w, h, 0);
            //<gd name="rw" fmla="*/ dl 51965 100000" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var rw = dl * 51965 / 100000;
            //<gd name="lM" fmla="+- dl 0 rw" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var lM = dl + 0 - rw;
            //<gd name="xM" fmla="*/ ca lM 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xM = ca * lM / 2;
            //<gd name="yM" fmla="*/ sa lM 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yM = sa * lM / 2;
            //<gd name="dxAM" fmla="*/ sa th 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dxAM = sa * th / 2;
            //<gd name="dyAM" fmla="*/ ca th 2" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var dyAM = ca * th / 2;
            //<gd name="xA" fmla="+- xM 0 dxAM" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xA = xM + 0 - dxAM;
            //<gd name="yA" fmla="+- yM dyAM 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yA = yM + dyAM - 0;
            //<gd name="xB" fmla="+- xM dxAM 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xB = xM + dxAM - 0;
            //<gd name="yB" fmla="+- yM 0 dyAM" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yB = yM + 0 - dyAM;
            //<gd name="xBC" fmla="+- hc 0 xB" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xBC = hc + 0 - xB;
            //<gd name="yBC" fmla="*/ xBC ta 1" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yBC = xBC * ta / 1;
            //<gd name="yC" fmla="+- yBC yB 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC = yBC + yB - 0;
            //<gd name="xD" fmla="+- r 0 xB" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xD = r + 0 - xB;
            //<gd name="xE" fmla="+- r 0 xA" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xE = r + 0 - xA;
            //<gd name="yFE" fmla="+- vc 0 yA" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yFE = vc + 0 - yA;
            //<gd name="xFE" fmla="*/ yFE 1 ta" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xFE = yFE * 1 / ta;
            //<gd name="xF" fmla="+- xE 0 xFE" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xF = xE + 0 - xFE;
            //<gd name="xL" fmla="+- xA xFE 0" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xL = xA + xFE - 0;
            //<gd name="yG" fmla="+- b 0 yA" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yG = b + 0 - yA;
            //<gd name="yH" fmla="+- b 0 yB" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yH = b + 0 - yB;
            //<gd name="yI" fmla="+- b 0 yC" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yI = b + 0 - yC;
            //<gd name="xC2" fmla="+- r 0 xM" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var xC2 = r + 0 - xM;
            //<gd name="yC3" fmla="+- b 0 yM" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            var yC3 = b + 0 - yM;

            //<pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path>
            //    <moveTo>
            //      <pt x="xA" y="yA" />
            //    </moveTo>
            //    <lnTo>
            //      <pt x="xB" y="yB" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="yC" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD" y="yB" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xE" y="yA" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xF" y="vc" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xE" y="yG" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xD" y="yH" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="hc" y="yI" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xB" y="yH" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xA" y="yG" />
            //    </lnTo>
            //    <lnTo>
            //      <pt x="xL" y="vc" />
            //    </lnTo>
            //    <close />
            //  </path>
            //</pathLst>

            var shapePaths = new ShapePath[1];

            // <path >
            //<moveTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xA" y="yA" />
            //</moveTo>
            var currentPoint = new EmuPoint(xA, yA);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB" y="yB" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB, yB);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="yC" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, yC);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD" y="yB" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD, yB);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE" y="yA" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xE, yA);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xF" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xF, vc);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xE" y="yG" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xE, yG);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xD" y="yH" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xD, yH);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="hc" y="yI" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, hc, yI);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xB" y="yH" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xB, yH);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xA" y="yG" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xA, yG);
            //<lnTo xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <pt x="xL" y="vc" />
            //</lnTo>
            currentPoint = LineToToString(stringPath, xL, vc);
            //<close xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            stringPath.Append("z ");
            shapePaths[0] = new ShapePath(stringPath.ToString());


            //<rect l="xA" t="yB" r="xE" b="yH" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(xA, yB, xE, yH);

            return shapePaths;
        }
    }


}

