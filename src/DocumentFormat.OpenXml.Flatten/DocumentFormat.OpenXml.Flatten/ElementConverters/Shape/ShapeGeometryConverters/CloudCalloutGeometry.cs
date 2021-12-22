using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 思想云
    /// </summary>
    public class CloudCalloutGeometry : ShapeGeometryBase
    {
        public override string? ToGeometryPathString(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return null;
        }

        public override ShapePath[]? GetMultiShapePaths(EmuSize emuSize, AdjustValueList? adjusts = null)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8, wd10, cd2, cd4, cd6, cd8) = GetFormulaProperties(emuSize);
            // <avLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            // <gd name="adj1" fmla="val -20833" />
            // <gd name="adj2" fmla="val 62500" />
            //</avLst>
            var customAdj1 = adjusts?.GetAdjustValue("adj1");
            var adj1 = customAdj1 ?? -20833d;
            var customAdj2 = adjusts?.GetAdjustValue("adj2");
            var adj2 = customAdj2 ?? 62500d;

            //<gdLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <gd name="dxPos" fmla="*/ w adj1 100000" />
            //  <gd name="dyPos" fmla="*/ h adj2 100000" />
            //  <gd name="xPos" fmla="+- hc dxPos 0" />
            //  <gd name="yPos" fmla="+- vc dyPos 0" />
            //  <gd name="ht" fmla="cat2 hd2 dxPos dyPos" />
            //  <gd name="wt" fmla="sat2 wd2 dxPos dyPos" />
            //  <gd name="g2" fmla="cat2 wd2 ht wt" />
            //  <gd name="g3" fmla="sat2 hd2 ht wt" />
            //  <gd name="g4" fmla="+- hc g2 0" />
            //  <gd name="g5" fmla="+- vc g3 0" />
            //  <gd name="g6" fmla="+- g4 0 xPos" />
            //  <gd name="g7" fmla="+- g5 0 yPos" />
            //  <gd name="g8" fmla="mod g6 g7 0" />
            //  <gd name="g9" fmla="*/ ss 6600 21600" />
            //  <gd name="g10" fmla="+- g8 0 g9" />
            //  <gd name="g11" fmla="*/ g10 1 3" />
            //  <gd name="g12" fmla="*/ ss 1800 21600" />
            //  <gd name="g13" fmla="+- g11 g12 0" />
            //  <gd name="g14" fmla="*/ g13 g6 g8" />
            //  <gd name="g15" fmla="*/ g13 g7 g8" />
            //  <gd name="g16" fmla="+- g14 xPos 0" />
            //  <gd name="g17" fmla="+- g15 yPos 0" />
            //  <gd name="g18" fmla="*/ ss 4800 21600" />
            //  <gd name="g19" fmla="*/ g11 2 1" />
            //  <gd name="g20" fmla="+- g18 g19 0" />
            //  <gd name="g21" fmla="*/ g20 g6 g8" />
            //  <gd name="g22" fmla="*/ g20 g7 g8" />
            //  <gd name="g23" fmla="+- g21 xPos 0" />
            //  <gd name="g24" fmla="+- g22 yPos 0" />
            //  <gd name="g25" fmla="*/ ss 1200 21600" />
            //  <gd name="g26" fmla="*/ ss 600 21600" />
            //  <gd name="x23" fmla="+- xPos g26 0" />
            //  <gd name="x24" fmla="+- g16 g25 0" />
            //  <gd name="x25" fmla="+- g23 g12 0" />
            //  <gd name="il" fmla="*/ w 2977 21600" />
            //  <gd name="it" fmla="*/ h 3262 21600" />
            //  <gd name="ir" fmla="*/ w 17087 21600" />
            //  <gd name="ib" fmla="*/ h 17337 21600" />
            //  <gd name="g27" fmla="*/ w 67 21600" />
            //  <gd name="g28" fmla="*/ h 21577 21600" />
            //  <gd name="g29" fmla="*/ w 21582 21600" />
            //  <gd name="g30" fmla="*/ h 1235 21600" />
            //  <gd name="pang" fmla="at2 dxPos dyPos" />
            //</gdLst>

            //  <gd name="dxPos" fmla="*/ w adj1 100000" />
            var dxPos = w * adj1 / 100000;
            //  <gd name="dyPos" fmla="*/ h adj2 100000" />
            var dyPos = h * adj2 / 100000;
            //  <gd name="xPos" fmla="+- hc dxPos 0" />
            var xPos = hc + dxPos;
            //  <gd name="yPos" fmla="+- vc dyPos 0" />
            var yPos = vc + dyPos;
            //  <gd name="ht" fmla="cat2 hd2 dxPos dyPos" />
            var ht = Cat2(hd2, dxPos, dyPos);
            //  <gd name="wt" fmla="sat2 wd2 dxPos dyPos" />
            var wt = Sat2(wd2, dxPos, dyPos);
            //  <gd name="g2" fmla="cat2 wd2 ht wt" />
            var g2 = Cat2(wd2, ht, wt);
            //  <gd name="g3" fmla="sat2 hd2 ht wt" />
            var g3 = Sat2(hd2, ht, wt);
            //  <gd name="g4" fmla="+- hc g2 0" />
            var g4 = hc + g2;
            //  <gd name="g5" fmla="+- vc g3 0" />
            var g5 = vc + g3;
            //  <gd name="g6" fmla="+- g4 0 xPos" />
            var g6 = g4 - xPos;
            //  <gd name="g7" fmla="+- g5 0 yPos" />
            var g7 = g5 - yPos;
            //  <gd name="g8" fmla="mod g6 g7 0" />
            var g8 = Mod(g6, g7, 0);
            //  <gd name="g9" fmla="*/ ss 6600 21600" />
            var g9 = ss * 6600 / 21600;
            //  <gd name="g10" fmla="+- g8 0 g9" />
            var g10 = g8 - g9;
            //  <gd name="g11" fmla="*/ g10 1 3" />
            var g11 = g10 * 1 / 3;
            //  <gd name="g12" fmla="*/ ss 1800 21600" />
            var g12 = ss * 1800 / 21600;
            //  <gd name="g13" fmla="+- g11 g12 0" />
            var g13 = g11 + g12;
            //  <gd name="g14" fmla="*/ g13 g6 g8" />
            var g14 = g13 * g6 / g8;
            //  <gd name="g15" fmla="*/ g13 g7 g8" />
            var g15 = g13 * g7 / g8;
            //  <gd name="g16" fmla="+- g14 xPos 0" />
            var g16 = g14 + xPos;
            //  <gd name="g17" fmla="+- g15 yPos 0" />
            var g17 = g15 + yPos;
            //  <gd name="g18" fmla="*/ ss 4800 21600" />
            var g18 = ss * 4800 / 21600;
            //  <gd name="g19" fmla="*/ g11 2 1" />
            var g19 = g11 * 2 / 1;
            //  <gd name="g20" fmla="+- g18 g19 0" />
            var g20 = g18 + g19;
            //  <gd name="g21" fmla="*/ g20 g6 g8" />
            var g21 = g20 * g6 / g8;
            //  <gd name="g22" fmla="*/ g20 g7 g8" />
            var g22 = g20 * g7 / g8;
            //  <gd name="g23" fmla="+- g21 xPos 0" />
            var g23 = g21 + xPos;
            //  <gd name="g24" fmla="+- g22 yPos 0" />
            var g24 = g22 + yPos;
            //  <gd name="g25" fmla="*/ ss 1200 21600" />
            var g25 = ss * 1200 / 21600;
            //  <gd name="g26" fmla="*/ ss 600 21600" />
            var g26 = ss * 600 / 21600;
            //  <gd name="x23" fmla="+- xPos g26 0" />
            var x23 = xPos + g26;
            //  <gd name="x24" fmla="+- g16 g25 0" />
            var x24 = g16 + g25;
            //  <gd name="x25" fmla="+- g23 g12 0" />
            var x25 = g23 + g12;
            //  <gd name="il" fmla="*/ w 2977 21600" />
            var il = w * 2977 / 21600;
            //  <gd name="it" fmla="*/ h 3262 21600" />
            var it = h * 3262 / 21600;
            //  <gd name="ir" fmla="*/ w 17087 21600" />
            var ir = w * 17087 / 21600;
            //  <gd name="ib" fmla="*/ h 17337 21600" />
            var ib = h * 17337 / 21600;
            //  <gd name="g27" fmla="*/ w 67 21600" />
            var g27 = w * 67 / 21600;
            //  <gd name="g28" fmla="*/ h 21577 21600" />
            var g28 = h * 21577 / 21600;
            //  <gd name="g29" fmla="*/ w 21582 21600" />
            var g29 = w * 21582 / 21600;
            //  <gd name="g30" fmla="*/ h 1235 21600" />
            var g30 = h * 1235 / 21600;
            //  <gd name="pang" fmla="at2 dxPos dyPos" />
            var pang = ATan2(dxPos, dyPos);


            // <pathLst xmlns="http://schemas.openxmlformats.org/drawingml/2006/main">
            //  <path w="43200" h="43200">
            //    <moveTo>
            //      <pt x="3900" y="14370" />
            //    </moveTo>
            //    <arcTo wR="6753" hR="9190" stAng="-11429249" swAng="7426832" />
            //    <arcTo wR="5333" hR="7267" stAng="-8646143" swAng="5396714" />
            //    <arcTo wR="4365" hR="5945" stAng="-8748475" swAng="5983381" />
            //    <arcTo wR="4857" hR="6595" stAng="-7859164" swAng="7034504" />
            //    <arcTo wR="5333" hR="7273" stAng="-4722533" swAng="6541615" />
            //    <arcTo wR="6775" hR="9220" stAng="-2776035" swAng="7816140" />
            //    <arcTo wR="5785" hR="7867" stAng="37501" swAng="6842000" />
            //    <arcTo wR="6752" hR="9215" stAng="1347096" swAng="6910353" />
            //    <arcTo wR="7720" hR="10543" stAng="3974558" swAng="4542661" />
            //    <arcTo wR="4360" hR="5918" stAng="-16496525" swAng="8804134" />
            //    <arcTo wR="4345" hR="5945" stAng="-14809710" swAng="9151131" />
            //    <close />
            //  </path>
            //  <path>
            //    <moveTo>
            //      <pt x="x23" y="yPos" />
            //    </moveTo>
            //    <arcTo wR="g26" hR="g26" stAng="0" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path>
            //    <moveTo>
            //      <pt x="x24" y="g17" />
            //    </moveTo>
            //    <arcTo wR="g25" hR="g25" stAng="0" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path>
            //    <moveTo>
            //      <pt x="x25" y="g24" />
            //    </moveTo>
            //    <arcTo wR="g12" hR="g12" stAng="0" swAng="21600000" />
            //    <close />
            //  </path>
            //  <path w="43200" h="43200" fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="4693" y="26177" />
            //    </moveTo>
            //    <arcTo wR="4345" hR="5945" stAng="5204520" swAng="1585770" />
            //    <moveTo>
            //      <pt x="6928" y="34899" />
            //    </moveTo>
            //    <arcTo wR="4360" hR="5918" stAng="4416628" swAng="686848" />
            //    <moveTo>
            //      <pt x="16478" y="39090" />
            //    </moveTo>
            //    <arcTo wR="6752" hR="9215" stAng="8257449" swAng="844866" />
            //    <moveTo>
            //      <pt x="28827" y="34751" />
            //    </moveTo>
            //    <arcTo wR="6752" hR="9215" stAng="387196" swAng="959901" />
            //    <moveTo>
            //      <pt x="34129" y="22954" />
            //    </moveTo>
            //    <arcTo wR="5785" hR="7867" stAng="-4217541" swAng="4255042" />
            //    <moveTo>
            //      <pt x="41798" y="15354" />
            //    </moveTo>
            //    <arcTo wR="5333" hR="7273" stAng="1819082" swAng="1665090" />
            //    <moveTo>
            //      <pt x="38324" y="5426" />
            //    </moveTo>
            //    <arcTo wR="4857" hR="6595" stAng="-824660" swAng="891534" />
            //    <moveTo>
            //      <pt x="29078" y="3952" />
            //    </moveTo>
            //    <arcTo wR="4857" hR="6595" stAng="-8950887" swAng="1091722" />
            //    <moveTo>
            //      <pt x="22141" y="4720" />
            //    </moveTo>
            //    <arcTo wR="4365" hR="5945" stAng="-9809656" swAng="1061181" />
            //    <moveTo>
            //      <pt x="14000" y="5192" />
            //    </moveTo>
            //    <arcTo wR="6753" hR="9190" stAng="-4002417" swAng="739161" />
            //    <moveTo>
            //      <pt x="4127" y="15789" />
            //    </moveTo>
            //    <arcTo wR="6753" hR="9190" stAng="9459261" swAng="711490" />
            //  </path>
            //</pathLst>


            var shapePaths = new ShapePath[5];

            //  <path w="43200" h="43200">
            var shapePathWidth = 43200d;
            var shapePathHeight = 43200d;
            //    <moveTo>
            //      <pt x="3900" y="14370" />
            //    </moveTo>
            var currentPoint = new EmuPoint(3900, 14370);
            var stringPath = new StringBuilder();
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="6753" hR="9190" stAng="-11429249" swAng="7426832" />
            var wR = 6753d;
            var hR = 9190d;
            var stAng = -11429249d;
            var swAng = 7426832d;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="5333" hR="7267" stAng="-8646143" swAng="5396714" />
            wR = 5333;
            hR = 7267;
            stAng = -8646143d;
            swAng = 5396714;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="4365" hR="5945" stAng="-8748475" swAng="5983381" />
            wR = 4365;
            hR = 5945;
            stAng = -8748475d;
            swAng = 5983381;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="4857" hR="6595" stAng="-7859164" swAng="7034504" />
            wR = 4857;
            hR = 6595;
            stAng = -7859164;
            swAng = 7034504;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="5333" hR="7273" stAng="-4722533" swAng="6541615" />
            wR = 5333;
            hR = 7273;
            stAng = -4722533;
            swAng = 6541615;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="6775" hR="9220" stAng="-2776035" swAng="7816140" />
            wR = 6775;
            hR = 9220;
            stAng = -2776035;
            swAng = 7816140;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="5785" hR="7867" stAng="37501" swAng="6842000" />
            wR = 5785;
            hR = 7867;
            stAng = 37501;
            swAng = 6842000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="6752" hR="9215" stAng="1347096" swAng="6910353" />
            wR = 6752;
            hR = 9215;
            stAng = 1347096;
            swAng = 6910353;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="7720" hR="10543" stAng="3974558" swAng="4542661" />
            wR = 7720;
            hR = 10543;
            stAng = 3974558;
            swAng = 4542661;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="4360" hR="5918" stAng="-16496525" swAng="8804134" />
            wR = 4360;
            hR = 5918;
            stAng = -16496525;
            swAng = 8804134;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <arcTo wR="4345" hR="5945" stAng="-14809710" swAng="9151131" />
            wR = 4345;
            hR = 5945;
            stAng = -14809710;
            swAng = 9151131;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z");
            shapePaths[0] = new ShapePath(stringPath.ToString(), emuWidth: shapePathWidth, emuHeight: shapePathHeight);


            //  <path>
            //    <moveTo>
            //      <pt x="x23" y="yPos" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x23, yPos);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="g26" hR="g26" stAng="0" swAng="21600000" />
            wR = g26;
            hR = g26;
            stAng = 0;
            swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z");
            shapePaths[1] = new ShapePath(stringPath.ToString());


            //  <path>
            //    <moveTo>
            //      <pt x="x24" y="g17" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x24, g17);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="g25" hR="g25" stAng="0" swAng="21600000" />
            wR = g25;
            hR = g25;
            stAng = 0;
            swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z");
            shapePaths[2] = new ShapePath(stringPath.ToString());

            //  <path>
            //    <moveTo>
            //      <pt x="x25" y="g24" />
            //    </moveTo>
            stringPath.Clear();
            currentPoint = new EmuPoint(x25, g24);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="g12" hR="g12" stAng="0" swAng="21600000" />
            wR = g12;
            hR = g12;
            stAng = 0;
            swAng = 21600000;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <close />
            //  </path>
            stringPath.Append("z");
            shapePaths[3] = new ShapePath(stringPath.ToString());


            //  <path w="43200" h="43200" fill="none" extrusionOk="false">
            //    <moveTo>
            //      <pt x="4693" y="26177" />
            //    </moveTo>
            shapePathWidth = 43200;
            shapePathHeight = 43200;
            stringPath.Clear();
            currentPoint = new EmuPoint(4693, 26177);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="4345" hR="5945" stAng="5204520" swAng="1585770" />
            wR = 4345;
            hR = 5945;
            stAng = 5204520;
            swAng = 1585770;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="6928" y="34899" />
            //    </moveTo>
            currentPoint = new EmuPoint(6928, 34899);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="4360" hR="5918" stAng="4416628" swAng="686848" />
            wR = 4360;
            hR = 5918;
            stAng = 4416628;
            swAng = 686848;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="16478" y="39090" />
            //    </moveTo>
            currentPoint = new EmuPoint(16478, 39090);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="6752" hR="9215" stAng="8257449" swAng="844866" />
            wR = 6752;
            hR = 9215;
            stAng = 8257449;
            swAng = 844866;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="28827" y="34751" />
            //    </moveTo>
            currentPoint = new EmuPoint(28827, 34751);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="6752" hR="9215" stAng="387196" swAng="959901" />
            wR = 6752;
            hR = 9215;
            stAng = 387196;
            swAng = 959901;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="34129" y="22954" />
            //    </moveTo>
            currentPoint = new EmuPoint(34129, 22954);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="5785" hR="7867" stAng="-4217541" swAng="4255042" />
            wR = 5785;
            hR = 7867;
            stAng = -4217541;
            swAng = 4255042;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="41798" y="15354" />
            //    </moveTo>
            currentPoint = new EmuPoint(41798, 15354);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="5333" hR="7273" stAng="1819082" swAng="1665090" />
            wR = 5333;
            hR = 7273;
            stAng = 1819082;
            swAng = 1665090;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="38324" y="5426" />
            //    </moveTo>
            currentPoint = new EmuPoint(38324, 5426);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="4857" hR="6595" stAng="-824660" swAng="891534" />
            wR = 4857;
            hR = 6595;
            stAng = -824660;
            swAng = 891534;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="29078" y="3952" />
            //    </moveTo>
            currentPoint = new EmuPoint(29078, 3952);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="4857" hR="6595" stAng="-8950887" swAng="1091722" />
            wR = 4857;
            hR = 6595;
            stAng = -8950887;
            swAng = 1091722;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="22141" y="4720" />
            //    </moveTo>
            currentPoint = new EmuPoint(22141, 4720);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="4365" hR="5945" stAng="-9809656" swAng="1061181" />
            wR = 4365;
            hR = 5945;
            stAng = -9809656;
            swAng = 1061181;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="14000" y="5192" />
            //    </moveTo>
            currentPoint = new EmuPoint(14000, 5192);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="6753" hR="9190" stAng="-4002417" swAng="739161" />
            wR = 6753;
            hR = 9190;
            stAng = -4002417;
            swAng = 739161;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //    <moveTo>
            //      <pt x="4127" y="15789" />
            //    </moveTo>
            currentPoint = new EmuPoint(4127, 15789);
            stringPath.Append($"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");
            //    <arcTo wR="6753" hR="9190" stAng="9459261" swAng="711490" />
            wR = 6753;
            hR = 9190;
            stAng = 9459261;
            swAng = 711490;
            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR, stAng, swAng);
            //  </path>
            shapePaths[4] = new ShapePath(stringPath.ToString(), PathFillModeValues.None, emuWidth: shapePathWidth, emuHeight: shapePathHeight);

            //<rect l="il" t="it" r="ir" b="ib" xmlns="http://schemas.openxmlformats.org/drawingml/2006/main" />
            InitializeShapeTextRectangle(il, it, ir, ib);

            return shapePaths;
        }
    }
}
