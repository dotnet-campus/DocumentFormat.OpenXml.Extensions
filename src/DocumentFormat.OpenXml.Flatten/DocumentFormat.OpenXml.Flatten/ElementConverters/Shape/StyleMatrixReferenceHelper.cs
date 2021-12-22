using System.Diagnostics;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Flatten.Utils;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;

using ColorMap = DocumentFormat.OpenXml.Presentation.ColorMap;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    /// <summary>
    ///     reference元素扩展
    /// </summary>
    static class StyleMatrixReferenceHelper
    {
        /// <summary>
        ///     从reference元素中获取brush
        ///     暂时仅支持<see cref="LineReference" />和<see cref="FillReference" />
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static OpenXmlElement? FlattenReferenceBrush(this StyleMatrixReferenceType? reference, SlideContext context)
        {
            if (reference == null)
            {
                // 既然没有引用，自然返回空啦
                return null;
            }

            // 只有在使用 SchemeColor 时，才会使用到 placeHolderColor 的值
            var placeHolderColor = ColorHelper.BuildColor(reference, context);

            var root = context.RootElement;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (root is null)
            {
                return null;
            }
            var currentPart = context.GetCurrentPart();
            var colorMap = root.GetColorMap();
            var colorScheme = root.GetColorScheme();
            var theme = reference.GetThemeElementFromReference(root.GetFormatScheme());

            // 如果是 HslColor 或 RgbColorModelPercentage 或 RgbColorModelHex 或 SystemColor
            // 那都能表示具体含义，不需要继续修改值
            if (theme != null)
            {
                //如果是fill元素，直接转换
                if (IsSupportedFill(theme))
                {
                    return FlattenBrushWithPlaceholder(theme, placeHolderColor, colorScheme,
                         colorMap, currentPart);
                }
                //否则，对子元素进行转换
                else
                {
                    foreach (var childElement in theme.ChildElements)
                    {
                        var brush = FlattenBrushWithPlaceholder(childElement, placeHolderColor, colorScheme,
                            colorMap, currentPart);
                        if (brush != null) return brush;
                    }
                }
            }

            return null;
        }

        private static OpenXmlElement? FlattenBrushWithPlaceholder(OpenXmlElement? fillElement, ARgbColor? placeHolderColor, ColorScheme? scheme, ColorMap? map, OpenXmlPart part)
        {
            if (fillElement is SolidFill solidFill)
            {
                return FlattenBrush(solidFill, scheme, map, placeHolderColor);
            }
            else if (fillElement is GradientFill gradientFill)
            {
                // 这里啥都不做
            }

#if DEBUG
            // 下面代码没有任何作用，只是用来辅助调试
            if (fillElement is BlipFill blipFill)
            {
                // 图片本身不需要做优化
            }
            else if (fillElement is NoFill noFill)
            {
                // 不要有任何填充
            }

            // 不支持 PatternFill EffectList EffectDag
#endif

            return fillElement;
        }

        private static SolidFill FlattenBrush(this SolidFill fill, ColorScheme? scheme,
            ColorMap? map, ARgbColor? placeholderColor)
        {
            // 可以忽略的颜色是 HslColor RgbColorModelPercentage RgbColorModelHex

            if (fill.RgbColorModelHex != null)
            {
                // 因为使用的是 RgbColorModelHex 属性，因此只判断此属性存在
                return fill;
            }

            if (fill.SystemColor != null)
            {
                // 如果有系统颜色，而且没有 RGB 颜色， 那么替换为 RGB 颜色
                ARgbColor? color = fill.SystemColor.ToColor();
                return CreateNewSolidFill(color);
            }

            if (fill.PresetColor != null)
            {
                // 预定义的颜色，也做一次转换
                var color = fill.PresetColor.ToColor();
                return CreateNewSolidFill(color);
            }

            // 其实这是核心的方法
            if (fill.SchemeColor != null)
            {
                var color = fill.SchemeColor.ToColor(scheme, map, placeholderColor);
                return CreateNewSolidFill(color);
            }

            return fill;

            SolidFill CreateNewSolidFill(ARgbColor? color)
            {
                if (color != null)
                {
                    var newBrush = (SolidFill) fill.Clone();
                    newBrush.RgbColorModelHex = color.ToRgbColorModelHex();
                    return newBrush;
                }

                return fill;
            }
        }

        private static bool IsSupportedFill(OpenXmlElement element)
        {
            if (element is SolidFill ||
                element is NoFill ||
                element is GradientFill ||
                element is BlipFill)
                return true;

            return false;
        }

        public static OpenXmlElement? GetThemeElementFromReference(this StyleMatrixReferenceType? reference,
            FormatScheme? themes)
        {
            if (reference == null)
            {
                return null;
            }

            if (reference is FillReference fill) return fill.GetThemeFill(themes);

            if (reference is LineReference line) return line.GetThemeLine(themes);

            // TODO @lindexi 2030年9月21日 其他类型暂未实现

            return null;
        }

        /// <summary>
        ///     获取<see cref="NoFill" />,<see cref="SolidFill" />,<see cref="GradientFill" />,<see cref="BlipFill" />
        ///     <see cref="BlipFill" />,<see cref="GroupFill" />,<see cref="PatternFill" />
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="formatScheme"></param>
        /// <returns></returns>
        public static OpenXmlElement? GetThemeFill(this FillReference reference, FormatScheme? formatScheme)
        {
            if (reference.Index is not null && formatScheme != null)
            {
                var index = (int) reference.Index.Value;

                var openXmlElementList = formatScheme.FillStyleList?.ChildElements;

                if (openXmlElementList != null) return GetThemeElement(index, openXmlElementList);
            }

            return null;
        }

        /// <summary>
        ///     获取主题线型
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="formatScheme"></param>
        /// <returns></returns>
        public static Outline? GetThemeLine(this LineReference reference, FormatScheme? formatScheme)
        {
            if (reference.Index is not null && formatScheme != null)
            {
                var index = (int) reference.Index.Value;

                if (GetThemeElement(index, formatScheme.LineStyleList?.ChildElements) is Outline outline) return outline;
            }

            return null;
        }

        private static OpenXmlElement? GetThemeElement(int index, OpenXmlElementList? elements)
        {
            if (index > 0 && elements != null && elements.Count >= index)
            {
                //GetItem是0 base的数组，所以需要减去1
                var xmlElement = elements.GetItem(index - 1);

                return xmlElement;
            }

            return null;
        }
    }
}
