using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CustomGeometryConverters;
using DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;
using Path = System.IO.Path;
using Shape = DocumentFormat.OpenXml.Presentation.Shape;
using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// 形状的几何转换器辅助
    /// </summary>
    public static class ShapeGeometryConverterHelper
    {
        /// <summary>
        ///     获取形状的SvgPath
        /// </summary>
        /// <param name="shape">OpenXml Shape</param>
        /// <returns></returns>
        public static SvgPath? ToSvgPath(this Shape shape)
        {
            return ToSvgPath((ShapeAdapt) shape);
        }

        /// <summary>
        ///     获取形状的SvgPath
        /// </summary>
        public static SvgPath? ToSvgPath(this ShapeAdapt shape)
        {
            var shapePropertiesAdapt = shape.ShapePropertiesAdapt;
            Debug.Assert(shapePropertiesAdapt != null, nameof(shapePropertiesAdapt) + " != null");
            var emuSize = shape.GetElementEmuSize();
            var presetGeometry = shapePropertiesAdapt?.ShapeProperties?.GetFirstChild<PresetGeometry>();
            if (presetGeometry?.Preset != null)
            {
                return GetPresetGeometrySvgPath(presetGeometry.Preset.Value, emuSize, presetGeometry.AdjustValueList);
            }

            return GetCustomGeometrySvgPath(shapePropertiesAdapt, emuSize);
        }

        /// <summary>
        /// 获取自定义形状的 SVG 路径
        /// </summary>
        /// <param name="shapePropertiesAdapt"></param>
        /// <param name="emuSize"></param>
        /// <returns></returns>
        public static SvgPath? GetCustomGeometrySvgPath(ShapePropertiesAdapt? shapePropertiesAdapt, ElementEmuSize emuSize)
        {
            var customGeometry = shapePropertiesAdapt?.ShapeProperties?.GetFirstChild<CustomGeometry>();
            if (customGeometry == null)
            {
                return null;
            }

            var customGeometryConverter = new CustomGeometryConverter(customGeometry, emuSize);
            return customGeometryConverter.Convert();
        }

        /// <summary>
        /// 获取预设形状的 SVG 路径
        /// </summary>
        /// <param name="geometryShapeTypeValues"></param>
        /// <param name="emuSize"></param>
        /// <param name="adjustList"></param>
        /// <returns></returns>
        public static SvgPath? GetPresetGeometrySvgPath(ShapeTypeValues geometryShapeTypeValues,
            ElementEmuSize emuSize,
            AdjustValueList? adjustList)
        {
            ShapeGeometryBase? shapeGeometryBase = geometryShapeTypeValues switch
            {
                ShapeTypeValues.Ellipse => CreateShapeGeometry<EllipseGeometry>(),
                ShapeTypeValues.RoundRectangle => CreateShapeGeometry<RoundRectangleGeometry>(),
                ShapeTypeValues.Round2SameRectangle => CreateShapeGeometry<Round2SameRectangleGeometry>(),
                ShapeTypeValues.Round1Rectangle => CreateShapeGeometry<Round1RectangleGeometry>(),
                ShapeTypeValues.Round2DiagonalRectangle => CreateShapeGeometry<Round2DiagonalRectangleGeometry>(),
                ShapeTypeValues.SnipRoundRectangle => CreateShapeGeometry<SnipRoundRectangleGeometry>(),
                ShapeTypeValues.Decagon => CreateShapeGeometry<DecagonGeometry>(),
                ShapeTypeValues.Diamond => CreateShapeGeometry<DiamondGeometry>(),
                ShapeTypeValues.Dodecagon => CreateShapeGeometry<DodecagonGeometry>(),
                ShapeTypeValues.Heptagon => CreateShapeGeometry<HeptagonGeometry>(),
                ShapeTypeValues.Hexagon => CreateShapeGeometry<HexagonGeometry>(),
                ShapeTypeValues.Octagon => CreateShapeGeometry<OctagonGeometry>(),
                ShapeTypeValues.Parallelogram => CreateShapeGeometry<ParallelogramGeometry>(),
                ShapeTypeValues.Pentagon => CreateShapeGeometry<PentagonGeometry>(),
                ShapeTypeValues.Rectangle => CreateShapeGeometry<RectangleGeometry>(),
                ShapeTypeValues.RightTriangle => CreateShapeGeometry<RightTriangleGeometry>(),
                ShapeTypeValues.Snip1Rectangle => CreateShapeGeometry<Snip1RectangleGeometry>(),
                ShapeTypeValues.Snip2DiagonalRectangle => CreateShapeGeometry<Snip2DiagonalRectangleGeometry>(),
                ShapeTypeValues.Snip2SameRectangle => CreateShapeGeometry<Snip2SameRectangleGeometry>(),
                ShapeTypeValues.Trapezoid => CreateShapeGeometry<TrapezoidGeometry>(),
                ShapeTypeValues.Triangle => CreateShapeGeometry<TriangleGeometry>(),
                ShapeTypeValues.WedgeRectangleCallout => CreateShapeGeometry<WedgeRectangleCalloutGeometry>(),
                ShapeTypeValues.BorderCallout1 => CreateShapeGeometry<BorderCallout1Geometry>(),
                ShapeTypeValues.WedgeRoundRectangleCallout => CreateShapeGeometry<WedgeRoundRectangleCalloutGeometry>(),
                ShapeTypeValues.WedgeEllipseCallout => CreateShapeGeometry<WedgeEllipseCalloutGeometry>(),
                ShapeTypeValues.BorderCallout2 => CreateShapeGeometry<BorderCallout2Geometry>(),
                ShapeTypeValues.BorderCallout3 => CreateShapeGeometry<BorderCallout3Geometry>(),
                ShapeTypeValues.AccentCallout1 => CreateShapeGeometry<AccentCallout1Geometry>(),
                ShapeTypeValues.AccentCallout2 => CreateShapeGeometry<AccentCallout2Geometry>(),
                ShapeTypeValues.AccentCallout3 => CreateShapeGeometry<AccentCallout3Geometry>(),
                ShapeTypeValues.AccentBorderCallout1 => CreateShapeGeometry<AccentBorderCallout1Geometry>(),
                ShapeTypeValues.AccentBorderCallout2 => CreateShapeGeometry<AccentBorderCallout2Geometry>(),
                ShapeTypeValues.AccentBorderCallout3 => CreateShapeGeometry<AccentBorderCallout3Geometry>(),
                ShapeTypeValues.Callout1 => CreateShapeGeometry<Callout1Geometry>(),
                ShapeTypeValues.Callout2 => CreateShapeGeometry<Callout2Geometry>(),
                ShapeTypeValues.Callout3 => CreateShapeGeometry<Callout3Geometry>(),
                ShapeTypeValues.Line => CreateShapeGeometry<LineGeometry>(),
                ShapeTypeValues.StraightConnector1 => CreateShapeGeometry<StraightConnector1Geometry>(),
                ShapeTypeValues.BentConnector2 => CreateShapeGeometry<BentConnector2Geometry>(),
                ShapeTypeValues.BentConnector3 => CreateShapeGeometry<BentConnector3Geometry>(),
                ShapeTypeValues.BentConnector4 => CreateShapeGeometry<BentConnector4Geometry>(),
                ShapeTypeValues.BentConnector5 => CreateShapeGeometry<BentConnector5Geometry>(),
                ShapeTypeValues.CurvedConnector2 => CreateShapeGeometry<CurvedConnector2Geometry>(),
                ShapeTypeValues.CurvedConnector3 => CreateShapeGeometry<CurvedConnector3Geometry>(),
                ShapeTypeValues.CurvedConnector4 => CreateShapeGeometry<CurvedConnector4Geometry>(),
                ShapeTypeValues.CurvedConnector5 => CreateShapeGeometry<CurvedConnector5Geometry>(),
                ShapeTypeValues.LeftBracket => CreateShapeGeometry<LeftBracketGeometry>(),
                ShapeTypeValues.RightBracket => CreateShapeGeometry<RightBracketGeometry>(),
                ShapeTypeValues.BracketPair => CreateShapeGeometry<BracketPairGeometry>(),
                ShapeTypeValues.LeftBrace => CreateShapeGeometry<LeftBraceGeometry>(),
                ShapeTypeValues.RightBrace => CreateShapeGeometry<RightBraceGeometry>(),
                ShapeTypeValues.BracePair => CreateShapeGeometry<BracePairGeometry>(),
                ShapeTypeValues.Donut => CreateShapeGeometry<DonutGeometry>(),
                ShapeTypeValues.BlockArc => CreateShapeGeometry<BlockArcGeometry>(),
                ShapeTypeValues.Cube => CreateShapeGeometry<CubeGeometry>(),
                ShapeTypeValues.FlowChartPunchedTape => CreateShapeGeometry<FlowChartPunchedTapeGeometry>(),
                ShapeTypeValues.Chord => CreateShapeGeometry<ChordGeometry>(),
                ShapeTypeValues.Wave => CreateShapeGeometry<WaveGeometry>(),
                ShapeTypeValues.FlowChartDelay => CreateShapeGeometry<FlowChartDelayGeometry>(),
                ShapeTypeValues.Chevron => CreateShapeGeometry<ChevronGeometry>(),
                ShapeTypeValues.Pie => CreateShapeGeometry<PieGeometry>(),
                ShapeTypeValues.Teardrop => CreateShapeGeometry<TeardropGeometry>(),
                ShapeTypeValues.Frame => CreateShapeGeometry<FrameGeometry>(),
                ShapeTypeValues.HalfFrame => CreateShapeGeometry<HalfFrameGeometry>(),
                ShapeTypeValues.Corner => CreateShapeGeometry<CornerGeometry>(),
                ShapeTypeValues.DiagonalStripe => CreateShapeGeometry<DiagonalStripeGeometry>(),
                ShapeTypeValues.Plus => CreateShapeGeometry<PlusGeometry>(),
                ShapeTypeValues.Plaque => CreateShapeGeometry<PlaqueGeometry>(),
                ShapeTypeValues.Can => CreateShapeGeometry<CanGeometry>(),
                ShapeTypeValues.Bevel => CreateShapeGeometry<BevelGeometry>(),
                ShapeTypeValues.NoSmoking => CreateShapeGeometry<NoSmokingGeometry>(),
                ShapeTypeValues.FoldedCorner => CreateShapeGeometry<FoldedCornerGeometry>(),
                ShapeTypeValues.Heart => CreateShapeGeometry<HeartGeometry>(),
                ShapeTypeValues.LightningBolt => CreateShapeGeometry<LightningBoltGeometry>(),
                ShapeTypeValues.SmileyFace => CreateShapeGeometry<SmileyFaceGeometry>(),
                ShapeTypeValues.Sun => CreateShapeGeometry<SunGeometry>(),
                ShapeTypeValues.Moon => CreateShapeGeometry<MoonGeometry>(),
                ShapeTypeValues.Cloud => CreateShapeGeometry<CloudGeometry>(),
                ShapeTypeValues.Arc => CreateShapeGeometry<ArcGeometry>(),
                ShapeTypeValues.LeftArrow => CreateShapeGeometry<LeftArrowGeometry>(),
                ShapeTypeValues.RightArrow => CreateShapeGeometry<RightArrowGeometry>(),
                ShapeTypeValues.DownArrow => CreateShapeGeometry<DownArrowGeometry>(),
                ShapeTypeValues.LeftRightArrow => CreateShapeGeometry<LeftRightArrowGeometry>(),
                ShapeTypeValues.UpDownArrow => CreateShapeGeometry<UpDownArrowGeometry>(),
                ShapeTypeValues.QuadArrow => CreateShapeGeometry<QuadArrowGeometry>(),
                ShapeTypeValues.LeftRightUpArrow => CreateShapeGeometry<LeftRightUpArrowGeometry>(),
                ShapeTypeValues.BentArrow => CreateShapeGeometry<BentArrowGeometry>(),
                ShapeTypeValues.UTurnArrow => CreateShapeGeometry<UTurnArrowGeometry>(),
                ShapeTypeValues.LeftUpArrow => CreateShapeGeometry<LeftUpArrowGeometry>(),
                ShapeTypeValues.BentUpArrow => CreateShapeGeometry<BentUpArrowGeometry>(),
                ShapeTypeValues.CurvedRightArrow => CreateShapeGeometry<CurvedRightArrowGeometry>(),
                ShapeTypeValues.CurvedLeftArrow => CreateShapeGeometry<CurvedLeftArrowGeometry>(),
                ShapeTypeValues.CurvedUpArrow => CreateShapeGeometry<CurvedUpArrowGeometry>(),
                ShapeTypeValues.CurvedDownArrow => CreateShapeGeometry<CurvedDownArrowGeometry>(),
                ShapeTypeValues.StripedRightArrow => CreateShapeGeometry<StripedRightArrowGeometry>(),
                ShapeTypeValues.NotchedRightArrow => CreateShapeGeometry<NotchedRightArrowGeometry>(),
                ShapeTypeValues.HomePlate => CreateShapeGeometry<HomePlateGeometry>(),
                ShapeTypeValues.LeftArrowCallout => CreateShapeGeometry<LeftArrowCalloutGeometry>(),
                ShapeTypeValues.RightArrowCallout => CreateShapeGeometry<RightArrowCalloutGeometry>(),
                ShapeTypeValues.LeftRightArrowCallout => CreateShapeGeometry<LeftRightArrowCalloutGeometry>(),
                ShapeTypeValues.DownArrowCallout => CreateShapeGeometry<DownArrowCalloutGeometry>(),
                ShapeTypeValues.UpArrowCallout => CreateShapeGeometry<UpArrowCalloutGeometry>(),
                ShapeTypeValues.QuadArrowCallout => CreateShapeGeometry<QuadArrowCalloutGeometry>(),
                ShapeTypeValues.CircularArrow => CreateShapeGeometry<CircularArrowGeometry>(),
                ShapeTypeValues.UpArrow => CreateShapeGeometry<UpArrowGeometry>(),
                ShapeTypeValues.CloudCallout => CreateShapeGeometry<CloudCalloutGeometry>(),
                ShapeTypeValues.FlowChartProcess => CreateShapeGeometry<FlowChartProcessGeometry>(),
                ShapeTypeValues.FlowChartAlternateProcess => CreateShapeGeometry<FlowChartAlternateProcessGeometry>(),
                ShapeTypeValues.FlowChartDecision => CreateShapeGeometry<FlowChartDecisionGeometry>(),
                ShapeTypeValues.FlowChartInputOutput => CreateShapeGeometry<FlowChartInputOutputGeometry>(),
                ShapeTypeValues.FlowChartPredefinedProcess => CreateShapeGeometry<FlowChartPredefinedProcessGeometry>(),
                ShapeTypeValues.FlowChartInternalStorage => CreateShapeGeometry<FlowChartInternalStorageGeometry>(),
                ShapeTypeValues.FlowChartDocument => CreateShapeGeometry<FlowChartDocumentGeometry>(),
                ShapeTypeValues.FlowChartMultidocument => CreateShapeGeometry<FlowChartMultidocumentGeometry>(),
                ShapeTypeValues.FlowChartTerminator => CreateShapeGeometry<FlowChartTerminatorGeometry>(),
                ShapeTypeValues.FlowChartMagneticTape => CreateShapeGeometry<FlowChartMagneticTapeGeometry>(),
                ShapeTypeValues.FlowChartMagneticDrum => CreateShapeGeometry<FlowChartMagneticDrumGeometry>(),
                ShapeTypeValues.FlowChartPreparation => CreateShapeGeometry<FlowChartPreparationGeometry>(),
                ShapeTypeValues.FlowChartManualInput => CreateShapeGeometry<FlowChartManualInputGeometry>(),
                ShapeTypeValues.FlowChartManualOperation => CreateShapeGeometry<FlowChartManualOperationGeometry>(),
                ShapeTypeValues.FlowChartConnector => CreateShapeGeometry<FlowChartConnectorGeometry>(),
                ShapeTypeValues.FlowChartOffpageConnector => CreateShapeGeometry<FlowChartOffpageConnectorGeometry>(),
                ShapeTypeValues.FlowChartPunchedCard => CreateShapeGeometry<FlowChartPunchedCardGeometry>(),
                ShapeTypeValues.FlowChartSummingJunction => CreateShapeGeometry<FlowChartSummingJunctionGeometry>(),
                ShapeTypeValues.FlowChartOr => CreateShapeGeometry<FlowChartOrGeometry>(),
                ShapeTypeValues.FlowChartCollate => CreateShapeGeometry<FlowChartCollateGeometry>(),
                ShapeTypeValues.FlowChartSort => CreateShapeGeometry<FlowChartSortGeometry>(),
                ShapeTypeValues.FlowChartExtract => CreateShapeGeometry<FlowChartExtractGeometry>(),
                ShapeTypeValues.FlowChartMerge => CreateShapeGeometry<FlowChartMergeGeometry>(),
                ShapeTypeValues.FlowChartOnlineStorage => CreateShapeGeometry<FlowChartOnlineStorageGeometry>(),
                ShapeTypeValues.FlowChartDisplay => CreateShapeGeometry<FlowChartDisplayGeometry>(),
                ShapeTypeValues.FlowChartMagneticDisk => CreateShapeGeometry<FlowChartMagneticDiskGeometry>(),
                ShapeTypeValues.IrregularSeal1 => CreateShapeGeometry<IrregularSeal1Geometry>(),
                ShapeTypeValues.IrregularSeal2 => CreateShapeGeometry<IrregularSeal2Geometry>(),
                ShapeTypeValues.Star4 => CreateShapeGeometry<Star4Geometry>(),
                ShapeTypeValues.Star5 => CreateShapeGeometry<Star5Geometry>(),
                ShapeTypeValues.Star6 => CreateShapeGeometry<Star6Geometry>(),
                ShapeTypeValues.Star7 => CreateShapeGeometry<Star7Geometry>(),
                ShapeTypeValues.Star8 => CreateShapeGeometry<Star8Geometry>(),
                ShapeTypeValues.Star10 => CreateShapeGeometry<Star10Geometry>(),
                ShapeTypeValues.Star12 => CreateShapeGeometry<Star12Geometry>(),
                ShapeTypeValues.Star16 => CreateShapeGeometry<Star16Geometry>(),
                ShapeTypeValues.Star24 => CreateShapeGeometry<Star24Geometry>(),
                ShapeTypeValues.Star32 => CreateShapeGeometry<Star32Geometry>(),
                ShapeTypeValues.Ribbon => CreateShapeGeometry<RibbonGeometry>(),
                ShapeTypeValues.Ribbon2 => CreateShapeGeometry<Ribbon2Geometry>(),
                ShapeTypeValues.EllipseRibbon => CreateShapeGeometry<EllipseRibbonGeometry>(),
                ShapeTypeValues.EllipseRibbon2 => CreateShapeGeometry<EllipseRibbon2Geometry>(),
                ShapeTypeValues.VerticalScroll => CreateShapeGeometry<VerticalScrollGeometry>(),
                ShapeTypeValues.HorizontalScroll => CreateShapeGeometry<HorizontalScrollGeometry>(),
                ShapeTypeValues.DoubleWave => CreateShapeGeometry<DoubleWaveGeometry>(),
                ShapeTypeValues.MathPlus => CreateShapeGeometry<MathPlusGeometry>(),
                ShapeTypeValues.MathMinus => CreateShapeGeometry<MathMinusGeometry>(),
                ShapeTypeValues.MathMultiply => CreateShapeGeometry<MathMultiplyGeometry>(),
                ShapeTypeValues.MathDivide => CreateShapeGeometry<MathDivideGeometry>(),
                ShapeTypeValues.MathEqual => CreateShapeGeometry<MathEqualGeometry>(),
                ShapeTypeValues.MathNotEqual => CreateShapeGeometry<MathNotEqualGeometry>(),
                ShapeTypeValues.ActionButtonBackPrevious => CreateShapeGeometry<ActionButtonBackPreviousGeometry>(),
                ShapeTypeValues.ActionButtonBlank => CreateShapeGeometry<ActionButtonBlankGeometry>(),
                ShapeTypeValues.ActionButtonDocument => CreateShapeGeometry<ActionButtonDocumentGeometry>(),
                ShapeTypeValues.ActionButtonBeginning => CreateShapeGeometry<ActionButtonBeginningGeometry>(),
                ShapeTypeValues.ActionButtonEnd => CreateShapeGeometry<ActionButtonEndGeometry>(),
                ShapeTypeValues.ActionButtonForwardNext => CreateShapeGeometry<ActionButtonForwardNextGeometry>(),
                ShapeTypeValues.ActionButtonHelp => CreateShapeGeometry<ActionButtonHelpGeometry>(),
                ShapeTypeValues.ActionButtonHome => CreateShapeGeometry<ActionButtonHomeGeometry>(),
                ShapeTypeValues.ActionButtonInformation => CreateShapeGeometry<ActionButtonInformationGeometry>(),
                ShapeTypeValues.ActionButtonMovie => CreateShapeGeometry<ActionButtonMovieGeometry>(),
                ShapeTypeValues.ActionButtonReturn => CreateShapeGeometry<ActionButtonReturnGeometry>(),
                ShapeTypeValues.ActionButtonSound => CreateShapeGeometry<ActionButtonSoundGeometry>(),
                ShapeTypeValues.Funnel => CreateShapeGeometry<FunnelGeometry>(),
                ShapeTypeValues.Gear6 => CreateShapeGeometry<Gear6Geometry>(),
                ShapeTypeValues.Gear9 => CreateShapeGeometry<Gear9Geometry>(),
                ShapeTypeValues.LeftRightRibbon => CreateShapeGeometry<LeftRightRibbonGeometry>(),
                ShapeTypeValues.SwooshArrow => CreateShapeGeometry<SwooshArrowGeometry>(),
                ShapeTypeValues.PieWedge => CreateShapeGeometry<PieWedgeGeometry>(),
                ShapeTypeValues.LeftCircularArrow => CreateShapeGeometry<LeftCircularArrowGeometry>(),
                ShapeTypeValues.NonIsoscelesTrapezoid => CreateShapeGeometry<NonIsoscelesTrapezoidGeometry>(),
                _ => null
            };

            if (shapeGeometryBase is null)
            {
                return default;
            }

            var multiGeometryPaths = shapeGeometryBase.GetMultiShapePaths(emuSize, adjustList);

            var pathString = shapeGeometryBase.ToGeometryPathString(emuSize, adjustList);
            var path = string.IsNullOrWhiteSpace(pathString) ? GetSvgPath(multiGeometryPaths) : pathString;
            return new SvgPath(geometryShapeTypeValues, path, shapeGeometryBase.ShapeTextRectangle, multiGeometryPaths);
        }

        private static string? GetSvgPath(ShapePath[]? shapePaths)
        {
            if (shapePaths is not null)
            {
                var sb = new StringBuilder();
                foreach (var shapePath in shapePaths)
                {
                    sb.Append(shapePath.Path);
                }
                return sb.ToString();
            }
            return default;
        }

        /// <summary>
        ///     根据调整点Name获取调整点的Value
        /// </summary>
        /// <param name="adjustList"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static double? GetAdjustValue(this AdjustValueList adjustList, string name)
        {
            var guide = adjustList.Elements<ShapeGuide>().FirstOrDefault(t => string.Equals(name, t.Name?.Value));
            if (guide != null)
            {
                var value = guide.Formula?.Value;
                const string val = "val ";
                if (value != null && value.StartsWith(val, StringComparison.OrdinalIgnoreCase))
                {
                    var numString = value.Substring(val.Length);
                    if (double.TryParse(numString, out var num))
                    {
                        return num;
                    }
                }
            }

            return default;
        }


        /// <summary>
        /// 创建ShapeGeometry
        /// </summary>
        /// <typeparam name="T">ShapeGeometryBase</typeparam>
        /// <returns></returns>
        public static T CreateShapeGeometry<T>() where T : ShapeGeometryBase, new()
        {
            return new();
        }
    }
}
