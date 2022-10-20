using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive;
using DocumentFormat.OpenXml.Flatten.Framework;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Flatten.Utils;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

using BlipFill = DocumentFormat.OpenXml.Drawing.BlipFill;
using Brush = DocumentFormat.OpenXml.Flatten.ElementConverters.BrushFill;
using Color = DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive.ARgbColor;
using ColorMap = DocumentFormat.OpenXml.Presentation.ColorMap;
using HslColor = DocumentFormat.OpenXml.Drawing.HslColor;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    /// <summary>
    /// 画刷的辅助类
    /// </summary>
    public static class BrushHelper
    {
        /// <summary>
        /// 替换形状里面的画刷
        /// </summary>
        /// <param name="element"></param>
        /// <param name="brush"></param>
        public static void ReplaceBrushFill(OpenXmlElement? element, BrushFill brush)
        {
            if (element == null) return;

            element.RemoveAllChildren<SolidFill>();
            element.RemoveAllChildren<NoFill>();
            element.RemoveAllChildren<GradientFill>();
            element.RemoveAllChildren<BlipFill>();
            element.RemoveAllChildren<PatternFill>();

            brush.AddToElement(element);
        }

        /// <summary>
        /// 尝试获取画刷，将会按照 SolidFill NoFill GradientFill BlipFill 顺序尝试获取
        /// </summary>
        public static Brush? GetFlattenFill(OpenXmlElement? element, SlideContext context, OpenXmlPartRootElement? rootElement = null,
            Color? placeholderColor = null)
        {
            if (element == null) return null;

            // 在文本的 TextStyleList 是文档级的，也就是 element 的 OpenXmlPartRootElement 是 presentation 但是颜色的定义是放在页面，或页面布局这些，此时就需要使用 RootElement 获取
            rootElement ??= context.RootElement;

            var currentPart = context.GetCurrentPart(rootElement);
            var colorMap = rootElement.GetColorMap();
            var colorScheme = rootElement.GetColorScheme();

            return BuildBrush(element.ChildElements, colorScheme, colorMap, currentPart, placeholderColor);
        }

        /// <summary>
        ///     尝试获取画刷，将会按照 SolidFill NoFill GradientFill BlipFill 顺序尝试获取
        /// </summary>
        /// <returns></returns>
        public static Brush? BuildBrush(OpenXmlElementList element,
            ColorScheme? scheme,
            ColorMap? map, OpenXmlPart? part, Color? placeholderColor = null)
        {
            var solidFill = element.First<SolidFill>();
            if (solidFill != null) return solidFill.ToBrush(scheme, map, placeholderColor);

            var noFill = element.First<NoFill>();
            if (noFill != null) return new BrushFill(noFill);

            var gradientFill = element.First<GradientFill>();
            if (gradientFill != null) return new BrushFill(gradientFill);

            var blipFill = element.First<BlipFill>();
            if (blipFill != null && part != null) return new BrushFill(blipFill);

            var patternFill = element.First<PatternFill>();
            if (patternFill != null)
            {
                // 这是当前不支持的
                return new BrushFill(patternFill);
            }
            // 不支持 EffectList EffectDag
            return null;
        }

        /// <summary>
        /// 转换 SolidFill 画刷
        /// </summary>
        /// <param name="fill"></param>
        /// <param name="scheme"></param>
        /// <param name="map"></param>
        /// <param name="placeholderColor"></param>
        /// <returns></returns>
        public static Brush? ToBrush(this SolidFill fill, ColorScheme? scheme,
            ColorMap? map, Color? placeholderColor)
        {
            var color = GetColor();
            if (color is not null)
            {
                var solidFill = new SolidFill();
                solidFill.AppendChild(color.ToRgbColorModelHex().ToRgbColorModelHex());
                return new BrushFill(solidFill);
            }
            else
            {
                return default;
            }

            Color? GetColor()
            {
                if (fill.HslColor is { } hslColor)
                {
                    return hslColor.ToColor();
                }

                if (fill.RgbColorModelPercentage is { } rgbColorModelPercentage)
                {
                    return rgbColorModelPercentage.ToColor();
                }

                if (fill.RgbColorModelHex is { } rgbColorModelHex)
                {
                    return rgbColorModelHex.ToColor();
                }

                if (fill.SystemColor is { } systemColor)
                {
                    return systemColor.ToColor();
                }

                if (fill.PresetColor is { } presetColor)
                {
                    return presetColor.ToColor();
                }

                if (fill.SchemeColor is { } schemeColor)
                {
                    return schemeColor.ToColor(scheme, map, placeholderColor);
                }

                return default;
            }
        }

        /// <summary>
        /// 合并画刷
        /// </summary>
        /// <param name="mainBrushElement"></param>
        /// <param name="reservedBrushElement"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static OpenXmlElement? MergeBrush(OpenXmlElement? mainBrushElement, OpenXmlElement? reservedBrushElement,
            ElementContext context)
        {
            // 这里的 reservedBrushElement 基本都是 Fill 类型

            if (mainBrushElement is null && reservedBrushElement is null)
            {
                return null;
            }

            if (mainBrushElement != null)
            {
                // 根据 ECMA 376 的 20.1.8.28 章
                // This element specifies a fill which is one of blipFill, gradFill, grpFill, noFill, pattFill or solidFill

                var mainBrushNoFill = mainBrushElement.GetFirstChild<NoFill>();
                if (mainBrushNoFill != null)
                {
                    // 传入带有 NoFill 的形状颜色，将不会执行合并颜色逻辑
                    return mainBrushNoFill;
                }

                var mainSolidFill = mainBrushElement.GetFirstChild<SolidFill>();
                if (mainSolidFill != null)
                {
                    var reservedSolidFill = reservedBrushElement as SolidFill;
                    var elementFlattenProvider = new ElementFlattenProvider<SolidFill>(mainSolidFill, reservedSolidFill);
                    // 至少有一个颜色，如果 MainSolidFill 不存在任何颜色，那么从 ReservedBrushElement 获取颜色
                    elementFlattenProvider.FlattenMainElementProperty<OpenXmlElement>(e => ElementFlattenExtension.ReturnWhenNotNull(e.HslColor)
                        .ReturnWhenNotNull(e.PresetColor)
                        .ReturnWhenNotNull(e.RgbColorModelHex)
                        .ReturnWhenNotNull(e.RgbColorModelPercentage)
                        .ReturnWhenNotNull(e.SchemeColor)
                        .ReturnWhenNotNull(e.SystemColor).Result!);
                    return mainSolidFill;
                }

                var gradientFill = mainBrushElement.GetFirstChild<GradientFill>();
                if (gradientFill != null)
                {
                    var reservedGradientFill = reservedBrushElement as GradientFill;
                    var elementFlattenProvider = new ElementFlattenProvider<GradientFill>(gradientFill, reservedGradientFill);

                    elementFlattenProvider
                        .FlattenMainElementProperty<GradientStopList>()
                        .FlattenMainElementProperty<LinearGradientFill>()
                        .FlattenMainElementProperty<TileRectangle>();

                    return gradientFill;
                }

                var mainBlipFill = mainBrushElement.GetFirstChild<BlipFill>();
                if (mainBlipFill != null)
                {
                    return mainBlipFill;
                }

                var mainPatternFill = mainBrushElement.GetFirstChild<PatternFill>();
                if (mainPatternFill != null)
                {
                    return mainPatternFill;
                }

                // 需要将 GroupFill 放在最后，这是优先级最低的
                // 此时将从 GroupFill 取出颜色填充过去
                var groupFill = mainBrushElement.GetFirstChild<GroupFill>();
                if (groupFill != null)
                {
                    if (context.GroupShape != null)
                    {
                        return FillFromGroupElement(mainBrushElement, context.GroupShape);
                    }
                    else
                    {
                        // 如果设置了 GroupFill 了，但是没有放在组合内，那相当于采用了 NoFill 没有颜色
                        mainBrushElement.RemoveChild(groupFill);

                        var fill = new NoFill();
                        mainBrushElement.AppendChild(fill);
                        return fill;
                    }
                }

                if (reservedBrushElement is BlipFill blipFill)
                {
                    return blipFill;
                }

                if (reservedBrushElement is NoFill noFill)
                {
                    return noFill;
                }
            }

            if (reservedBrushElement != null)
            {
                // 如果啥都没找到
                if (reservedBrushElement is SolidFill
                    || reservedBrushElement is GradientFill
                    || reservedBrushElement is BlipFill
                    || reservedBrushElement is NoFill)
                {
                    // 还有一个 GroupFill 需要被忽略，因为 GroupFill 是继承组合的
                    var result = (OpenXmlElement) reservedBrushElement.Clone();
                    mainBrushElement?.AppendChild(result);

                    return result;
                }

                // 不支持 PatternFill EffectList EffectDag
            }

            return null;
        }

        private static OpenXmlElement? FillFromGroupElement(OpenXmlElement mainBrushElement,
            DocumentFormat.OpenXml.Presentation.GroupShape? groupShape)
        {
            // 参阅 `dotnet OpenXML 继承组合颜色的 GrpFill 属性.md` 文档
            while (groupShape != null)
            {
                var fill = FindFromGroupElementInner(groupShape);
                if (fill != null)
                {
                    var openXmlElement = (OpenXmlElement) fill.Clone();
                    mainBrushElement.AppendChild(openXmlElement);
                    return openXmlElement;
                }
                else
                {
                    groupShape = groupShape.Parent as DocumentFormat.OpenXml.Presentation.GroupShape;
                }
            }

            return null;

            static OpenXmlElement? FindFromGroupElementInner(
                DocumentFormat.OpenXml.Presentation.GroupShape presentationGroupShape)
            {
                var shapeProperties = presentationGroupShape.GroupShapeProperties;
                if (shapeProperties is null)
                {
                    return null;
                }

                return ElementFlattenExtension.ReturnWhenNotNull(shapeProperties.GetFirstChild<NoFill>())
                    .ReturnWhenNotNull(shapeProperties.GetFirstChild<SolidFill>())
                    .ReturnWhenNotNull(shapeProperties.GetFirstChild<GradientFill>())
                    .ReturnWhenNotNull(shapeProperties.GetFirstChild<BlipFill>())
                    .Result;
            }
        }
    }
}
