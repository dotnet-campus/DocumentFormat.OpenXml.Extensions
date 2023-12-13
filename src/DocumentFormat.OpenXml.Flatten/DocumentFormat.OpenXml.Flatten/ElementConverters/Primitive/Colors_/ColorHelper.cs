using System.Diagnostics;
using System.Globalization;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Flatten.Utils;

using dotnetCampus.OpenXmlUnitConverter;

using Color = DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive.ARgbColor;
using ColorMap = DocumentFormat.OpenXml.Presentation.ColorMap;
using HslColor = DocumentFormat.OpenXml.Drawing.HslColor;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive
{
    /// <summary>
    ///     各种颜色类型的转换辅助类
    /// </summary>
    public static class ColorHelper
    {
        ///// <summary>
        /////     尝试获取画刷，将会从子元素中尝试获取
        ///// </summary>
        ///// <returns></returns>
        //public static Brush? BuildBrush(OpenXmlElement element,
        //    OpenXmlPartRootElement rootElement)
        //{
        //    var color = BuildColorInner(element, rootElement);

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     尝试获取画刷，将会从子元素中尝试获取
        /// </summary>
        /// <returns></returns>
        private static Color? BuildColorInner(OpenXmlElement? element,
            OpenXmlPartRootElement? rootElement, Color? placeholderColor = null)
        {
            if (rootElement == null) return null;
            if (element == null) return null;

            var colorMap = rootElement.GetColorMap();
            var colorScheme = rootElement.GetColorScheme();
            return BuildColor(element.ChildElements, colorScheme, colorMap, placeholderColor);
        }

        /// <summary>
        ///     尝试获取画刷，将会 从子元素中尝试获取
        /// </summary>
        /// <returns></returns>
        public static Color? BuildColor(OpenXmlElement? element,
            SlideContext context)
        {
            var root = context.RootElement;
            // 从 context 里面获取，等同使用下面代码获取。使用如下代码也只是多访问几次而已，基本没有性能差，只是使用 context 的属性，可以让 context 有引用，减少警告
            //var root = element?.Ancestors<OpenXmlPartRootElement>().FirstOrDefault();

            return BuildColorInner(element, root);
        }

        /// <summary>
        ///     创建笔刷
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scheme"></param>
        /// <param name="map"></param>
        /// <param name="placeholderColor"></param>
        /// <returns></returns>
        public static Color? BuildColor(OpenXmlElementList element,
            ColorScheme? scheme = null,
            ColorMap? map = null, Color? placeholderColor = null)
        {
            var hslColor = element.First<HslColor>();
            if (hslColor != null) return hslColor.ToColor();

            var rgbColorModelPercentage = element.First<RgbColorModelPercentage>();
            if (rgbColorModelPercentage != null) return rgbColorModelPercentage.ToColor();

            var rgbColorModelHex = element.First<RgbColorModelHex>();
            if (rgbColorModelHex != null) return rgbColorModelHex.ToColor();

            var systemColor = element.First<SystemColor>();
            if (systemColor != null) return systemColor.ToColor();

            var presetColor = element.First<PresetColor>();
            if (presetColor != null) return presetColor.ToColor();

            var schemeColor = element.First<SchemeColor>();
            if (schemeColor != null) return schemeColor.ToColor(scheme, map, placeholderColor);

            return null;
        }

        ///// <summary>
        /////     将<see cref="SchemeColor" />转换为<see cref="Brush" />
        ///// </summary>
        ///// <param name="schemeColor"></param>
        ///// <param name="scheme"></param>
        ///// <param name="map"></param>
        ///// <param name="placeholderColor"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this SchemeColor schemeColor, ColorScheme scheme, ColorMap map,
        //    Color? placeholderColor)
        //{
        //    var color = schemeColor.ToColor(scheme, map, placeholderColor);

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     将<see cref="SchemeColor" />转换为<see cref="Color" />
        /// </summary>
        /// <param name="color"></param>
        /// <param name="scheme"></param>
        /// <param name="map"></param>
        /// <param name="placeholderColor"></param>
        /// <returns></returns>
        public static Color? ToColor(this SchemeColor color, ColorScheme? scheme, ColorMap? map, Color? placeholderColor)
        {
            if (color.Val != null)
            {
                var value = color.Val.Value;
                //对于占位符颜色，使用输入的占位符颜色，加下面的调整
                if (value == SchemeColorValues.PhColor && placeholderColor != null)
                    return ColorTransform.AppendColorModify(placeholderColor, color.ChildElements);

                if (map != null) value = SchemeColorMap(value, map);

                if (scheme != null)
                {
                    var schemeColor = FindSchemeColor(value, scheme);
                    if (schemeColor != null)
                    {
                        Color? buildColor = null;
                        if (schemeColor.HslColor != null) buildColor = schemeColor.HslColor.ToColor();

                        if (schemeColor.PresetColor != null) buildColor = schemeColor.PresetColor.ToColor();

                        if (schemeColor.RgbColorModelHex != null) buildColor = schemeColor.RgbColorModelHex.ToColor();

                        if (schemeColor.RgbColorModelPercentage != null)
                            buildColor = schemeColor.RgbColorModelPercentage.ToColor();

                        if (schemeColor.SystemColor != null) buildColor = schemeColor.SystemColor.ToColor();

                        if (buildColor != null)
                            return ColorTransform.AppendColorModify(buildColor, color.ChildElements);
                    }
                }
            }

            return null;
        }

        ///// <summary>
        /////     将<see cref="SystemColor" />转换为<see cref="Storage.Standard.Brush" />
        ///// </summary>
        ///// <param name="presetColor"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this PresetColor presetColor)
        //{
        //    var color = presetColor.ToColor();

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     将<see cref="SystemColor" />转换为<see cref="Color" />
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color? ToColor(this PresetColor color)
        {
            if (color.Val != null)
            {
                var solidColor = PresetColorMap.BuildPreSetColor(color.Val.Value);
                var modifiedColor = ColorTransform.AppendColorModify(solidColor, color.ChildElements);
                return modifiedColor;
            }

            return null;
        }

        ///// <summary>
        /////     将<see cref="SystemColor" />转换为<see cref="Brush" />
        ///// </summary>
        ///// <param name="systemColor"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this SystemColor systemColor)
        //{
        //    var color = systemColor.ToColor();

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     将<see cref="SystemColor" />转换为<see cref="Color" />
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color? ToColor(this SystemColor color)
        {
            if (color.LastColor is not null)
            {
                if (uint.TryParse(color.LastColor.Value, NumberStyles.HexNumber, null, out var result))
                {
                    var solidColor = result.HexToColor();
                    var modifiedColor = ColorTransform.AppendColorModify(solidColor, color.ChildElements);
                    return modifiedColor;
                }
            }

            if (color.Val != null)
            {
                //这个值是依赖于系统环境的颜色设置，我们这边直接忽略
            }

            return null;
        }

        ///// <summary>
        /////     将<see cref="DocumentFormat.OpenXml.Drawing.HslColor" />转换为<see cref="Brush" />
        ///// </summary>
        ///// <param name="hslColor"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this HslColor hslColor)
        //{
        //    var color = hslColor.ToColor();

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     将<see cref="DocumentFormat.OpenXml.Drawing.HslColor" />转换为<see cref="Color" />
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color? ToColor(this HslColor color)
        {
            if (color.HueValue is not null && color.LumValue is not null && color.SatValue is not null)
            {
                var h = new Degree(color.HueValue);
                var s = new Percentage(color.SatValue);
                var l = new Percentage(color.LumValue);

                var solidColor = ColorTransform.HslToColor(h, s, l);

                var modifiedColor = ColorTransform.AppendColorModify(solidColor, color.ChildElements);
                return modifiedColor;
            }

            return null;
        }

        ///// <summary>
        /////     将<see cref="RgbColor" />转换为<see cref="Brush" />
        ///// </summary>
        ///// <param name="color"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this RgbColor color)
        //{
        //    if (color.Blue != null && color.Red != null && color.Green != null)
        //    {
        //        var solidColor = new Color
        //        {
        //            A = 0xFF,
        //            B = Convert.ToByte(color.Blue.Value),
        //            G = Convert.ToByte(color.Green.Value),
        //            R = Convert.ToByte(color.Red.Value)
        //        };
        //        var modifiedColor = ColorTransform.AppendColorModify(solidColor, color.ChildElements);

        //        return modifiedColor.ToColorBrush();
        //    }

        //    return null;
        //}

        ///// <summary>
        /////     将<see cref="RgbColorModelHex" />转换为<see cref="Brush" />
        ///// </summary>
        ///// <param name="rgbColorModelHex"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this RgbColorModelHex rgbColorModelHex)
        //{
        //    var color = rgbColorModelHex.ToColor();

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     将<see cref="RgbColorModelHex" />转换为<see cref="Color" />
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color? ToColor(this RgbColorModelHex color)
        {
            if (color.Val is null)
            {
                return null;
            }

            var solidColor = ToColor(color.Val.Value);
            if (solidColor is null)
            {
                return null;
            }
            var modifiedColor = ColorTransform.AppendColorModify(solidColor, color.ChildElements);
            return modifiedColor;

        }

        /// <summary>
        /// 将<see cref="string" />颜色值转换为<see cref="Color" />
        /// </summary>
        /// <param name="colorValue">颜色值：例如#E71224</param>
        /// <returns></returns>
        public static Color? ToColor(this string? colorValue)
        {
            if (string.IsNullOrEmpty(colorValue))
            {
                return null;
            }

            var (success, a, r, g, b) = ConvertToColor(colorValue!);
            if (success)
            {
                return new(a, r, g, b);
            }

            return null;
        }

        ///// <summary>
        /////     将<see cref="RgbColorModelPercentage" />转换为<see cref="Brush" />
        ///// </summary>
        ///// <param name="rgbColorModelPercentage"></param>
        ///// <returns></returns>
        //public static Brush? ToBrush(this RgbColorModelPercentage rgbColorModelPercentage)
        //{
        //    var color = rgbColorModelPercentage.ToColor();

        //    return color.ToColorBrush();
        //}

        /// <summary>
        ///     将<see cref="RgbColorModelPercentage" />转换为<see cref="Color" />
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color? ToColor(this RgbColorModelPercentage color)
        {
            if (color.BluePortion is not null && color.RedPortion is not null && color.GreenPortion is not null)
            {
                const byte maxByte = 0xFF;
                var solidColor = new Color
                {
                    A = 0xFF,
                    B = (byte) (new Percentage(color.BluePortion).DoubleValue * maxByte),
                    G = (byte) (new Percentage(color.RedPortion).DoubleValue * maxByte),
                    R = (byte) (new Percentage(color.GreenPortion).DoubleValue * maxByte)
                };
                var modifiedColor = ColorTransform.AppendColorModify(solidColor, color.ChildElements);
                return modifiedColor;
            }

            return null;
        }


        private static Color HexToColor(this uint rgb)
        {
            var color = new Color();
            const int maxByte = 0xff;

            color.B = (byte) (rgb & maxByte);
            color.G = (byte) ((rgb >> 8) & maxByte);
            color.R = (byte) ((rgb >> 16) & maxByte);
            color.A = 0xFF;

            return color;
        }

        /// <summary>
        /// 将传入的颜色字符串转换为颜色输出
        /// </summary>
        /// <param name="hexColorText">颜色字符串，格式如 “#FFDFD991” 或 “#DFD991”等，规则和 WPF 的 XAML 颜色相同，其中 “#” 是可选的</param>
        /// <returns></returns>
        public static (bool success, byte a, byte r, byte g, byte b) ConvertToColor(string hexColorText)
        {
#if NET6_0_OR_GREATER
            bool startWithPoundSign = hexColorText.StartsWith('#');
#else
            bool startWithPoundSign = hexColorText.StartsWith("#");
#endif
            var colorStringLength = hexColorText.Length;
            if (startWithPoundSign) colorStringLength -= 1;
            int currentOffset = startWithPoundSign ? 1 : 0;
            // 可以采用的格式如下
            // #FFDFD991   8 个字符 存在 Alpha 通道
            // #DFD991     6 个字符
            // #FD92       4 个字符 存在 Alpha 通道
            // #DAC        3 个字符
            if (colorStringLength == 8
                || colorStringLength == 6
                || colorStringLength == 4
                || colorStringLength == 3)
            {
                bool success;
                byte result;
                byte a;

                int readCount;
                // #DFD991     6 个字符
                // #FFDFD991   8 个字符 存在 Alpha 通道
                //if (colorStringLength == 8 || colorStringLength == 6)
                if (colorStringLength > 5)
                {
                    readCount = 2;
                }
                else
                {
                    readCount = 1;
                }

                bool includeAlphaChannel = colorStringLength == 8 || colorStringLength == 4;

                if (includeAlphaChannel)
                {
                    (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
                    if (!success) return default;
                    a = result;
                    currentOffset += readCount;
                }
                else
                {
                    a = 0xFF;
                }

                (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
                if (!success) return default;
                byte r = result;
                currentOffset += readCount;

                (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
                if (!success) return default;
                byte g = result;
                currentOffset += readCount;

                (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
                if (!success) return default;
                byte b = result;

                return (true, a, r, g, b);
            }

            return default;
        }

        static (bool success, byte result) HexCharToNumber(string input, int offset, int readCount)
        {
            Debug.Assert(readCount == 1 || readCount == 2, "要求 readCount 只能是 1 或者 2 的值，这是框架限制，因此不做判断");

            byte result = 0;

            for (int i = 0; i < readCount; i++, offset++)
            {
                var c = input[offset];
                byte n;
                if (c >= '0' && c <= '9')
                {
                    n = (byte) (c - '0');
                }
                else if (c >= 'a' && c <= 'f')
                {
                    n = (byte) (c - 'a' + 10);
                }
                else if (c >= 'A' && c <= 'F')
                {
                    n = (byte) (c - 'A' + 10);
                }
                else
                {
                    return default;
                }

                result *= 16;
                result += n;
            }

            if (readCount == 1)
            {
                result = (byte) (result * 16 + result);
            }

            return (true, result);
        }

        //private static ColorBrush? ToColorBrush([CanBeNull] this Color? color)
        //{
        //    if (color == null) return null;

        //    return new ColorBrush
        //    {
        //        Color = color
        //    };
        //}

        private static Color2Type? FindSchemeColor(SchemeColorValues value, ColorScheme scheme)
        {
            return value switch
            {
                SchemeColorValues.Accent1 => scheme.Accent1Color,
                SchemeColorValues.Accent2 => scheme.Accent2Color,
                SchemeColorValues.Accent3 => scheme.Accent3Color,
                SchemeColorValues.Accent4 => scheme.Accent4Color,
                SchemeColorValues.Accent5 => scheme.Accent5Color,
                SchemeColorValues.Accent6 => scheme.Accent6Color,
                SchemeColorValues.Dark1 => scheme.Dark1Color,
                SchemeColorValues.Dark2 => scheme.Dark2Color,
                SchemeColorValues.FollowedHyperlink => scheme.FollowedHyperlinkColor,
                SchemeColorValues.Hyperlink => scheme.Hyperlink,
                SchemeColorValues.Light1 => scheme.Light1Color,
                SchemeColorValues.Light2 => scheme.Light2Color,
                _ => null
            };
        }

        private static SchemeColorValues SchemeColorMap(SchemeColorValues value, ColorMap map)
        {
            return value switch
            {
                SchemeColorValues.Accent1 => IndexToSchemeColor(map.Accent1),
                SchemeColorValues.Accent2 => IndexToSchemeColor(map.Accent2),
                SchemeColorValues.Accent3 => IndexToSchemeColor(map.Accent3),
                SchemeColorValues.Accent4 => IndexToSchemeColor(map.Accent4),
                SchemeColorValues.Accent5 => IndexToSchemeColor(map.Accent5),
                SchemeColorValues.Accent6 => IndexToSchemeColor(map.Accent6),
                SchemeColorValues.Dark1 => SchemeColorValues.Dark1,
                SchemeColorValues.Dark2 => SchemeColorValues.Dark2,
                SchemeColorValues.FollowedHyperlink => IndexToSchemeColor(map.FollowedHyperlink),
                SchemeColorValues.Hyperlink => IndexToSchemeColor(map.Hyperlink),
                SchemeColorValues.Light1 => SchemeColorValues.Light1,
                SchemeColorValues.Light2 => SchemeColorValues.Light2,
                SchemeColorValues.Background1 => IndexToSchemeColor(map.Background1),
                SchemeColorValues.Background2 => IndexToSchemeColor(map.Background2),
                SchemeColorValues.Text1 => IndexToSchemeColor(map.Text1),
                SchemeColorValues.Text2 => IndexToSchemeColor(map.Text2),
                _ => SchemeColorValues.Accent1
            };
        }


        private static SchemeColorValues IndexToSchemeColor(EnumValue<ColorSchemeIndexValues>? value)
        {
            var colorSchemeIndexValues = value?.Value;
            return colorSchemeIndexValues switch
            {
                ColorSchemeIndexValues.Accent1 => SchemeColorValues.Accent1,
                ColorSchemeIndexValues.Accent2 => SchemeColorValues.Accent2,
                ColorSchemeIndexValues.Accent3 => SchemeColorValues.Accent3,
                ColorSchemeIndexValues.Accent4 => SchemeColorValues.Accent4,
                ColorSchemeIndexValues.Accent5 => SchemeColorValues.Accent5,
                ColorSchemeIndexValues.Accent6 => SchemeColorValues.Accent6,
                ColorSchemeIndexValues.Dark1 => SchemeColorValues.Dark1,
                ColorSchemeIndexValues.Dark2 => SchemeColorValues.Dark2,
                ColorSchemeIndexValues.FollowedHyperlink => SchemeColorValues.FollowedHyperlink,
                ColorSchemeIndexValues.Hyperlink => SchemeColorValues.Hyperlink,
                ColorSchemeIndexValues.Light1 => SchemeColorValues.Light1,
                ColorSchemeIndexValues.Light2 => SchemeColorValues.Light2,
                _ => SchemeColorValues.Accent1
            };
        }
    }
}
