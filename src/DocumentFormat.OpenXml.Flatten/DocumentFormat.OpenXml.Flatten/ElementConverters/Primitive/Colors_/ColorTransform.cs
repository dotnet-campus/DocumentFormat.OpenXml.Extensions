using DocumentFormat.OpenXml.Drawing;

using dotnetCampus.OpenXmlUnitConverter;

using Color = DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive.ARgbColor;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive
{
    /// <summary>
    ///     处理颜色之间的变换，调整，格式转换
    /// </summary>
    public static class ColorTransform
    {
        /// <summary>
        ///     将Hsl的数据转换为<see cref="Color" />
        /// </summary>
        /// <param name="hue">色相</param>
        /// <param name="saturation">饱和度</param>
        /// <param name="lightness">亮度</param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color HslToColor(Degree hue, Percentage saturation, Percentage lightness, byte a = 0xFF)
        {
            var color = new Color { A = a };
            double max;
            double min;
            if (saturation.IntValue == 0)
            {
                color.R =
                    color.G =
                        color.B = (byte) (0xFF * lightness.DoubleValue);

                return color;
            }

            if (lightness.DoubleValue < 0.5)
            {
                max = lightness.DoubleValue * (1 + saturation.DoubleValue);
            }
            else
            {
                max = lightness.DoubleValue * (1 - saturation.DoubleValue) + saturation.DoubleValue;
            }

            min = 2 * lightness.DoubleValue - max;

            color.R = (byte) (0xFF * GetRatio(min, max, hue + Degree.FromDouble(120)));
            color.G = (byte) (0xFF * GetRatio(min, max, hue));
            color.B = (byte) (0xFF * GetRatio(min, max, hue - Degree.FromDouble(120)));

            return color;

            static double GetRatio(double min, double max, Degree hue)
            {
                if (hue < Degree.FromDouble(60)) return min + (max - min) * hue.DoubleValue / 60;

                if (hue < Degree.FromDouble(180)) return max;

                if (hue < Degree.FromDouble(240))
                {
                    return min + (max - min) * (Degree.FromDouble(240) - hue).DoubleValue / 60;
                }

                return min;
            }
        }

        /// <summary>
        ///     将<see cref="Color" />的数据转换为Hsl
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static (Degree hue, Percentage sat, Percentage lum, byte alpha) ColorToHsl(Color color)
        {
            var max = System.Math.Max(color.R, System.Math.Max(color.G, color.B));
            var min = System.Math.Min(color.R, System.Math.Min(color.G, color.B));
            var delta = max - min;
            var l = Percentage.FromDouble((max + min) / 2.0 / 255.0);
            var h = Degree.FromDouble(0);
            var s = Percentage.Zero;

            if (delta > 0)
            {
                s = l < Percentage.FromDouble(0.5)
                    ? Percentage.FromDouble((max - min) * 1.0 / (max + min))
                    : Percentage.FromDouble((max - min) * 1.0 / (2 * 255 - max - min));

                if (max == color.R)
                {
                    h = Degree.FromDouble((0 + (color.G - color.B) * 1.0 / delta) * 60);
                }
                else if (max == color.G)
                {
                    h = Degree.FromDouble((2 + (color.B - color.R) * 1.0 / delta) * 60);
                }
                else
                {
                    h = Degree.FromDouble((4 + (color.R - color.G) * 1.0 / delta) * 60);
                }
            }

            return (h, s, l, color.A);
        }

        /// <summary>
        ///     给颜色叠加转换
        ///     <remarks>
        ///         详细请看 [dotnet OpenXML
        ///         颜色变换](https://blog.lindexi.com/post/dotnet-OpenXML-%E9%A2%9C%E8%89%B2%E5%8F%98%E6%8D%A2.html )
        ///     </remarks>
        /// </summary>
        /// <param name="color"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Color AppendColorModify(ARgbColor color, OpenXmlElementList list)
        {
            var updatedColor = color;
            foreach (var element in list)
            {
                if (element is LuminanceModulation luminanceModulation)
                {
                    updatedColor = HandleLuminance(updatedColor, luminanceModulation, null);
                    continue;
                }

                if (element is LuminanceOffset luminanceOffset)
                {
                    updatedColor = HandleLuminance(updatedColor, null, luminanceOffset);
                    continue;
                }

                if (element is SaturationModulation saturationModulation)
                {
                    updatedColor = HandleSaturation(updatedColor, saturationModulation, null);
                    continue;
                }

                if (element is SaturationOffset saturationOffset)
                {
                    updatedColor = HandleSaturation(updatedColor, null, saturationOffset);
                    continue;
                }

                if (element is Tint tint)
                {
                    updatedColor = HandleTint(updatedColor, tint);
                    continue;
                }

                if (element is Shade shade)
                {
                    updatedColor = HandleShade(updatedColor, shade);
                    continue;
                }

                if (element is Alpha alpha)
                {
                    updatedColor = HandleAlphaModify(updatedColor, alpha);
                }
            }

            return updatedColor;
        }

        private static Color HandleShade(Color color, Shade? shadeElement)
        {
            var updatedColor = color;
            if (shadeElement != null)
            {
                var shadeVal = shadeElement.Val;
                var shade = shadeVal is not null ? new Percentage(shadeVal) : Percentage.FromDouble(1);
                var linearR = SRgbToLinearRgb(updatedColor.R / 255.0);
                var linearG = SRgbToLinearRgb(updatedColor.G / 255.0);
                var linearB = SRgbToLinearRgb(updatedColor.B / 255.0);
                var r = linearR * shade.DoubleValue;
                var g = linearG * shade.DoubleValue;
                var b = linearB * shade.DoubleValue;
                updatedColor.R = (byte) System.Math.Round(255 * LinearRgbToSRgb(r));
                updatedColor.G = (byte) System.Math.Round(255 * LinearRgbToSRgb(g));
                updatedColor.B = (byte) System.Math.Round(255 * LinearRgbToSRgb(b));
            }

            return updatedColor;
        }

        private static Color HandleTint(Color color, Tint? tintElement)
        {
            var updatedColor = color;
            if (tintElement != null)
            {
                var tintVal = tintElement.Val;
                var tint = tintVal is not null ? new Percentage(tintVal) : Percentage.FromDouble(1);
                var linearR = SRgbToLinearRgb(updatedColor.R / 255.0);
                var linearG = SRgbToLinearRgb(updatedColor.G / 255.0);
                var linearB = SRgbToLinearRgb(updatedColor.B / 255.0);
                var r = linearR + (1 - linearR) * (1 - tint.DoubleValue);
                var g = linearG + (1 - linearG) * (1 - tint.DoubleValue);
                var b = linearB + (1 - linearB) * (1 - tint.DoubleValue);
                updatedColor.R = (byte) System.Math.Round(255 * LinearRgbToSRgb(r));
                updatedColor.G = (byte) System.Math.Round(255 * LinearRgbToSRgb(g));
                updatedColor.B = (byte) System.Math.Round(255 * LinearRgbToSRgb(b));
            }

            return updatedColor;
        }

        private static Color HandleLuminance(Color color, LuminanceModulation? luminanceModulation,
            LuminanceOffset? luminanceOffset)
        {
            var (hue, sat, lum, alpha) = ColorToHsl(color);
            if (luminanceModulation != null || luminanceOffset != null)
            {
                var modulationVal = luminanceModulation?.Val;
                var offsetVal = luminanceOffset?.Val;
                var mod = modulationVal is not null && modulationVal.HasValue
                    ? new Percentage(modulationVal)
                    : Percentage.FromDouble(1);
                var off = offsetVal is not null && offsetVal.HasValue
                    ? new Percentage(offsetVal)
                    : Percentage.Zero;

                var value = lum.DoubleValue * mod.DoubleValue + off.DoubleValue;
                lum = Percentage.FromDouble(value);
                lum = lum > Percentage.FromDouble(1) ? Percentage.FromDouble(1) : lum;
                lum = lum < Percentage.Zero ? Percentage.Zero : lum;
            }

            return HslToColor(hue, sat, lum, alpha);
        }

        private static Color HandleSaturation(Color color, SaturationModulation? saturationModulation,
            SaturationOffset? saturationOffset)
        {
            var (hue, sat, lum, alpha) = ColorToHsl(color);
            if (saturationModulation != null || saturationOffset != null)
            {
                var modulationVal = saturationModulation?.Val;
                var offsetVal = saturationOffset?.Val;
                var mod = modulationVal is not null && modulationVal.HasValue
                    ? new Percentage(modulationVal)
                    : Percentage.FromDouble(1);
                var off = offsetVal is not null && offsetVal.HasValue
                    ? new Percentage(offsetVal)
                    : Percentage.Zero;

                var value = sat.DoubleValue * mod.DoubleValue + off.DoubleValue;
                sat = Percentage.FromDouble(value);
                sat = sat > Percentage.FromDouble(1) ? Percentage.FromDouble(1) : sat;
                sat = sat < Percentage.Zero ? Percentage.Zero : sat;
            }

            return HslToColor(hue, sat, lum, alpha);
        }

        private static Color HandleAlphaModify(Color color, Alpha? alpha)
        {
            if (alpha?.Val is { } value)
            {
                var alphaValue = new Percentage(value);
                color.A = (byte) (color.A * alphaValue.DoubleValue);
            }

            return color;
        }

        /// <summary>
        ///     https://en.wikipedia.org/wiki/SRGB#The_forward_transformation_.28CIE_xyY_or_CIE_XYZ_to_sRGB.29
        /// </summary>
        /// <param name="sRgb"></param>
        /// <returns></returns>
        private static double SRgbToLinearRgb(double sRgb)
        {
            if (sRgb <= 0.04045) return sRgb / 12.92;

            return System.Math.Pow((sRgb + 0.055) / 1.055, 2.4);
        }

        /// <summary>
        ///     https://en.wikipedia.org/wiki/SRGB#The_forward_transformation_.28CIE_xyY_or_CIE_XYZ_to_sRGB.29
        /// </summary>
        /// <param name="linearRgb"></param>
        /// <returns></returns>
        private static double LinearRgbToSRgb(double linearRgb)
        {
            if (linearRgb < 0.0031308) return 12.92 * linearRgb;

            //var linearR=3.24096994*sR-1.53738318*sg-0.49861076*sb
            return System.Math.Pow(linearRgb, 1.0 / 2.4) * 1.055 - 0.055;
        }
    }
}
