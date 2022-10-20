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
        ///     将Hsl的数据转换为<see cref="Color" />
        /// </summary>
        /// <param name="hue">色相</param>
        /// <param name="saturation">饱和度</param>
        /// <param name="lightness">亮度</param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color HslToColorNew(Degree hue, Percentage saturation, Percentage lightness, byte a = 0xFF)
        {
            var color = new Color { A = a };

            var hueValue = hue.DoubleValue;
            var saturationValue = saturation.DoubleValue;
            var lightnessValue = lightness.DoubleValue;

            var c = (1 - System.Math.Abs(2 * lightnessValue - 1)) * saturationValue;
            var x = c * (1 - System.Math.Abs((hueValue / 60) % 2 - 1));
            var m = lightnessValue - c / 2;

            var r = 0d;
            var g = 0d;
            var b = 0d;

            if (hueValue is >= 0 and < 60)
            {
                r = c;
                g = x;
                b = 0;
            }

            if (hueValue is >= 60 and < 120)
            {
                r = x;
                g = c;
                b = 0;
            }

            if (hueValue is >= 120 and < 180)
            {
                r = 0;
                g = c;
                b = x;
            }

            if (hueValue is >= 180 and < 240)
            {
                r = 0;
                g = x;
                b = c;
            }

            if (hueValue is >= 240 and < 300)
            {
                r = x;
                g = 0;
                b = c;
            }

            if (hueValue is >= 300 and < 360)
            {
                r = c;
                g = 0;
                b = x;
            }

            color.R = (byte) ((r + m) * 255);
            color.G = (byte) ((g + m) * 255);
            color.B = (byte) ((b + m) * 255);

            return color;
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

                if (element is Hue hue)
                {
                    updatedColor = HandleHue(updatedColor, hue, null, null);
                    continue;
                }

                if (element is HueModulation hueModulation)
                {
                    updatedColor = HandleHue(updatedColor, null, hueModulation, null);
                    continue;
                }

                if (element is HueOffset hueOffset)
                {
                    updatedColor = HandleHue(updatedColor, null, null, hueOffset);
                    continue;
                }

                if (element is Saturation saturation)
                {
                    updatedColor = HandleSaturation(updatedColor, saturation, null, null);
                    continue;
                }

                if (element is SaturationModulation saturationModulation)
                {
                    updatedColor = HandleSaturation(updatedColor, null, saturationModulation, null);
                    continue;
                }

                if (element is SaturationOffset saturationOffset)
                {
                    updatedColor = HandleSaturation(updatedColor, null, null, saturationOffset);
                    continue;
                }

                if (element is Luminance luminance)
                {
                    updatedColor = HandleLuminance(updatedColor, luminance, null, null);
                    continue;
                }

                if (element is LuminanceModulation luminanceModulation)
                {
                    updatedColor = HandleLuminance(updatedColor, null, luminanceModulation, null);
                    continue;
                }

                if (element is LuminanceOffset luminanceOffset)
                {
                    updatedColor = HandleLuminance(updatedColor, null, null, luminanceOffset);
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
                    updatedColor = HandleAlphaModify(updatedColor, alpha, null, null);
                    continue;
                }

                if (element is AlphaModulation alphaModulation)
                {
                    updatedColor = HandleAlphaModify(updatedColor, null, alphaModulation, null);
                    continue;
                }

                if (element is AlphaOffset alphaOffset)
                {
                    updatedColor = HandleAlphaModify(updatedColor, null, null, alphaOffset);
                    continue;
                }

                if (element is Red red)
                {
                    updatedColor = HandleRgb(updatedColor, redElement: red, greenElement: null, blueElement: null);
                    continue;
                }

                if (element is RedModulation redModulation)
                {
                    updatedColor = HandleRgbModulation(updatedColor, redModulationElement: redModulation, greenModulationElement: null, blueModulationElement: null);
                    continue;
                }

                if (element is RedOffset redOffset)
                {
                    HandleRgbOffset(updatedColor, redOffsetElement: redOffset, greenOffsetElement: null, blueOffsetElement: null);
                    continue;
                }

                if (element is Green green)
                {
                    updatedColor = HandleRgb(updatedColor, redElement: null, greenElement: green, blueElement: null);
                    continue;
                }

                if (element is GreenModulation greenModulation)
                {
                    updatedColor = HandleRgbModulation(updatedColor, redModulationElement: null, greenModulationElement: greenModulation, blueModulationElement: null);
                    continue;
                }

                if (element is GreenOffset greenOffset)
                {
                    HandleRgbOffset(updatedColor, redOffsetElement: null, greenOffsetElement: greenOffset, blueOffsetElement: null);
                    continue;
                }

                if (element is Blue blue)
                {
                    updatedColor = HandleRgb(updatedColor, redElement: null, greenElement: null, blueElement: blue);
                    continue;
                }

                if (element is BlueModulation blueModulation)
                {
                    updatedColor = HandleRgbModulation(updatedColor, redModulationElement: null, greenModulationElement: null, blueModulationElement: blueModulation);
                    continue;
                }

                if (element is BlueOffset blueOffset)
                {
                    updatedColor = HandleRgbOffset(updatedColor, redOffsetElement: null, greenOffsetElement: null, blueOffsetElement: blueOffset);
                    continue;
                }

                if (element is Complement complement)
                {
                    updatedColor = HandleComplement(updatedColor, complement);
                    continue;
                }

                if (element is Gamma gamma)
                {
                    updatedColor = HandleGamma(updatedColor, gamma);
                    continue;
                }

                if (element is InverseGamma inverseGamma)
                {
                    updatedColor = HandleInverseGamma(updatedColor, inverseGamma);
                    continue;
                }

                if (element is Inverse inverse)
                {
                    updatedColor = HandleInverse(updatedColor, inverse);
                    continue;
                }

                if (element is Gray gray)
                {
                    updatedColor = HandleGray(updatedColor, gray);
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

        private static Color HandleInverse(Color color, Inverse? inverseElement)
        {
            var updatedColor = color;
            if (inverseElement != null)
            {
                var linearR = SRgbToLinearRgb(updatedColor.R / 255.0);
                var linearG = SRgbToLinearRgb(updatedColor.G / 255.0);
                var linearB = SRgbToLinearRgb(updatedColor.B / 255.0);
                var r = System.Math.Abs(1.0 - linearR);
                var g = System.Math.Abs(1.0 - linearG);
                var b = System.Math.Abs(1.0 - linearB);
                updatedColor.R = (byte) System.Math.Round(255 * LinearRgbToSRgb(r));
                updatedColor.G = (byte) System.Math.Round(255 * LinearRgbToSRgb(g));
                updatedColor.B = (byte) System.Math.Round(255 * LinearRgbToSRgb(b));
            }

            return updatedColor;
        }

        private static Color HandleComplement(Color color, Complement? complementElement)
        {
            var updatedColor = color;
            if (complementElement != null)
            {
                var r = updatedColor.B;
                var g = updatedColor.R + updatedColor.B - updatedColor.G;
                var b = updatedColor.R;
                updatedColor.R = r;
                updatedColor.G = (byte) g;
                updatedColor.B = b;
            }

            return updatedColor;
        }

        /// <summary>
        /// 对于sRGB的反伽玛校正，也就是2.2的幂运算
        /// </summary>
        /// <param name="color"></param>
        /// <param name="inverseGammaElement"></param>
        /// <returns></returns>
        private static Color HandleInverseGamma(Color color, InverseGamma? inverseGammaElement)
        {
            var updatedColor = color;
            if (inverseGammaElement != null)
            {
                var r = System.Math.Pow(updatedColor.R / 255.0, 2.2);
                var g = System.Math.Pow(updatedColor.G / 255.0, 2.2);
                var b = System.Math.Pow(updatedColor.B / 255.0, 2.2);
                updatedColor.R = (byte) System.Math.Round(255 * r);
                updatedColor.G = (byte) System.Math.Round(255 * g);
                updatedColor.B = (byte) System.Math.Round(255 * b);
            }

            return updatedColor;
        }

        /// <summary>
        /// 对于sRGB的伽玛校正，也就是 1/2.2的幂运算
        /// </summary>
        /// <param name="color"></param>
        /// <param name="gammaElement"></param>
        /// <returns></returns>
        private static Color HandleGamma(Color color, Gamma? gammaElement)
        {
            var updatedColor = color;
            if (gammaElement != null)
            {
                var r = System.Math.Pow(updatedColor.R / 255.0, 1 / 2.2);
                var g = System.Math.Pow(updatedColor.G / 255.0, 1 / 2.2);
                var b = System.Math.Pow(updatedColor.B / 255.0, 1 / 2.2);
                updatedColor.R = (byte) System.Math.Round(255 * r);
                updatedColor.G = (byte) System.Math.Round(255 * g);
                updatedColor.B = (byte) System.Math.Round(255 * b);
            }

            return updatedColor;
        }


        /// <summary>
        /// 对于sRGB的灰阶计算
        /// </summary>
        /// <param name="color"></param>
        /// <param name="grayElement"></param>
        /// <returns></returns>
        /// sRGB IEC61966-2.1 [gamma=2.20]:sRGB计算灰阶：Gray = (R^2.2 * 0.2126  + G^2.2  * 0.7152  + B^2.2  * 0.0722)^(1/2.2)
        private static Color HandleGray(Color color, Gray? grayElement)
        {
            var updatedColor = color;
            if (grayElement != null)
            {
                var gray = System.Math.Pow(
                          System.Math.Pow(updatedColor.R, 2.2) * 0.2126 +
                          System.Math.Pow(updatedColor.G, 2.2) * 0.7152 +
                          System.Math.Pow(updatedColor.B, 2.2) * 0.0722,
                          1 / 2.2);

                var grayResult = (byte) System.Math.Round(gray);

                updatedColor.R = grayResult;
                updatedColor.G = grayResult;
                updatedColor.B = grayResult;
            }

            return updatedColor;
        }




        private static Color HandleRgb(Color color, Red? redElement, Green? greenElement, Blue? blueElement)
        {
            if (redElement is null && greenElement is null && blueElement is null)
            {
                return color;
            }

            var updatedColor = HandleRgbCore(color, redElement: redElement, greenElement: greenElement,
                blueElement: blueElement);


            return updatedColor;
        }
        private static Color HandleRgbModulation(Color color, RedModulation? redModulationElement, GreenModulation? greenModulationElement, BlueModulation? blueModulationElement)
        {
            if (redModulationElement is null && greenModulationElement is null && blueModulationElement is null)
            {
                return color;
            }

            var updatedColor = HandleRgbCore(color, redModulationElement: redModulationElement,
                greenModulationElement: greenModulationElement, blueModulationElement: blueModulationElement);


            return updatedColor;
        }


        private static Color HandleRgbOffset(Color color, RedOffset? redOffsetElement, GreenOffset? greenOffsetElement, BlueOffset? blueOffsetElement)
        {
            if (redOffsetElement is null && blueOffsetElement is null && greenOffsetElement is null)
            {
                return color;
            }

            var updatedColor = HandleRgbCore(color, redOffsetElement: redOffsetElement,
                greenOffsetElement: greenOffsetElement, blueOffsetElement: blueOffsetElement);


            return updatedColor;
        }

        private static Color HandleRgbCore(Color color,
            Red? redElement = null, Green? greenElement = null, Blue? blueElement = null,
            RedModulation? redModulationElement = null, GreenModulation? greenModulationElement = null, BlueModulation? blueModulationElement = null,
            RedOffset? redOffsetElement = null, GreenOffset? greenOffsetElement = null, BlueOffset? blueOffsetElement = null)
        {
            if (redElement is null && greenElement is null && blueElement is null
                && redModulationElement is null && greenModulationElement is null && blueModulationElement is null
                && redOffsetElement is null && greenOffsetElement is null && blueOffsetElement is null)
            {
                return color;
            }

            var updatedColor = color;

            var redModulationValue = redModulationElement?.Val;
            var redMod = redModulationValue is not null ? new Percentage(redModulationValue) : Percentage.FromDouble(1);

            var greenModulationValue = greenModulationElement?.Val;
            var greenMod = greenModulationValue is not null ? new Percentage(greenModulationValue) : Percentage.FromDouble(1);

            var blueModulationValue = blueModulationElement?.Val;
            var blueMod = blueModulationValue is not null ? new Percentage(blueModulationValue) : Percentage.FromDouble(1);

            var redOffsetValue = redOffsetElement?.Val;
            var redOffset = redOffsetValue is not null ? new Percentage(redOffsetValue) : Percentage.FromDouble(0);

            var greenOffsetValue = greenOffsetElement?.Val;
            var greenOffset = greenOffsetValue is not null ? new Percentage(greenOffsetValue) : Percentage.FromDouble(0);

            var blueOffsetValue = blueOffsetElement?.Val;
            var blueOffset = blueOffsetValue is not null ? new Percentage(blueOffsetValue) : Percentage.FromDouble(0);


            var linearR = SRgbToLinearRgb(updatedColor.R / 255.0);
            var linearG = SRgbToLinearRgb(updatedColor.G / 255.0);
            var linearB = SRgbToLinearRgb(updatedColor.B / 255.0);

            var redValue = redElement?.Val;
            var red = redValue is not null ? new Percentage(redValue).DoubleValue : linearR;

            var greenValue = greenElement?.Val;
            var green = greenValue is not null ? new Percentage(greenValue).DoubleValue : linearG;

            var blueValue = blueElement?.Val;
            var blue = blueValue is not null ? new Percentage(blueValue).DoubleValue : linearB;

            var redResult = red * redMod.DoubleValue + redOffset.DoubleValue;
            var greenResult = green * greenMod.DoubleValue + greenOffset.DoubleValue;
            var blueResult = blue * blueMod.DoubleValue + blueOffset.DoubleValue;


            var r = redResult < 0 ? 0 : redResult > 1 ? 1 : redResult;
            var g = greenResult < 0 ? 0 : greenResult > 1 ? 1 : greenResult;
            var b = blueResult < 0 ? 0 : blueResult > 1 ? 1 : blueResult;
            updatedColor.R = (byte) System.Math.Round(255 * LinearRgbToSRgb(r));
            updatedColor.G = (byte) System.Math.Round(255 * LinearRgbToSRgb(g));
            updatedColor.B = (byte) System.Math.Round(255 * LinearRgbToSRgb(b));

            return updatedColor;
        }


        private static Color HandleHslCore(Color color,
            Hue? hueElement = null, HueModulation? hueModElement = null, HueOffset? hueOffsetElement = null,
            Saturation? satElement = null, SaturationModulation? satModElement = null, SaturationOffset? satOffsetElement = null,
            Luminance? lumElement = null, LuminanceModulation? lumModElement = null, LuminanceOffset? lumOffsetElement = null)
        {
            if (hueElement is null && hueModElement is null && hueOffsetElement is null
                && satElement is null && satModElement is null && satOffsetElement is null
                && lumElement is null && lumModElement is null && lumOffsetElement is null)
            {
                return color;
            }

            var (hue, sat, lum, alpha) = ColorToHsl(color);

            var hueElementVal = hueElement?.Val;
            var hueValue = hueElementVal is not null ? new Angle(hueElementVal).ToDegreeValue() : hue.DoubleValue;
            var satElementVal = satElement?.Val;
            var satValue = satElementVal is not null ? new Percentage(satElementVal).DoubleValue : sat.DoubleValue;
            var lumElementVal = lumElement?.Val;
            var lumValue = lumElementVal is not null ? new Percentage(lumElementVal).DoubleValue : lum.DoubleValue;

            var hueModElementVal = hueModElement?.Val;
            var hueModValue = hueModElementVal is not null ? new Percentage(hueModElementVal) : Percentage.FromDouble(1);
            var satModElementVal = satModElement?.Val;
            var satModValue = satModElementVal is not null ? new Percentage(satModElementVal) : Percentage.FromDouble(1);
            var lumModElementVal = lumModElement?.Val;
            var lumModValue = lumModElementVal is not null ? new Percentage(lumModElementVal) : Percentage.FromDouble(1);

            var hueOffsetVal = hueOffsetElement?.Val;
            var hueOffset = hueOffsetVal is not null ? new Angle(hueOffsetVal).ToDegreeValue() : new Angle(0).ToDegreeValue();
            var saturationOffsetVal = satOffsetElement?.Val;
            var saturationOffset = saturationOffsetVal is not null ? new Percentage(saturationOffsetVal) : Percentage.Zero;
            var lumOffsetElementVal = lumOffsetElement?.Val;
            var lumOffset = lumOffsetElementVal is not null ? new Percentage(lumOffsetElementVal) : Percentage.Zero;

            var hueResult = hueValue * hueModValue.DoubleValue + hueOffset;
            hue = Degree.FromDouble(hueResult);

            var satResult = satValue * satModValue.DoubleValue + saturationOffset.DoubleValue;
            sat = Percentage.FromDouble(satResult);
            sat = sat > Percentage.FromDouble(1) ? Percentage.FromDouble(1) : sat;
            sat = sat < Percentage.Zero ? Percentage.Zero : sat;

            var lumResult = lumValue * lumModValue.DoubleValue + lumOffset.DoubleValue;
            lum = Percentage.FromDouble(lumResult);
            lum = lum > Percentage.FromDouble(1) ? Percentage.FromDouble(1) : lum;
            lum = lum < Percentage.Zero ? Percentage.Zero : lum;

            return HslToColor(hue, sat, lum, alpha);

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

        private static Color HandleLuminance(Color color, Luminance? lumElement, LuminanceModulation? lumModElement,
            LuminanceOffset? lumOffsetElement)
        {
            if (lumElement is null && lumModElement is null && lumOffsetElement is null)
            {
                return color;
            }

            var updatedColor = HandleHslCore(color, lumElement: lumElement, lumModElement: lumModElement, lumOffsetElement: lumOffsetElement);

            return updatedColor;
        }

        private static Color HandleHue(Color color, Hue? hueElement, HueModulation? hueModElement,
            HueOffset? hueOffsetElement)
        {
            if (hueElement is null && hueModElement is null && hueOffsetElement is null)
            {
                return color;
            }

            var updatedColor = HandleHslCore(color, hueElement: hueElement, hueModElement: hueModElement, hueOffsetElement: hueOffsetElement);

            return updatedColor;
        }

        private static Color HandleSaturation(Color color, Saturation? satElement, SaturationModulation? satModElement,
            SaturationOffset? satOffsetElement)
        {
            if (satElement is null && satModElement is null && satOffsetElement is null)
            {
                return color;
            }

            var updatedColor = HandleHslCore(color, satElement: satElement, satModElement: satModElement, satOffsetElement: satOffsetElement);

            return updatedColor;
        }

        private static Color HandleAlphaModify(Color color, Alpha? alphaElement, AlphaModulation? alphaModulation, AlphaOffset? alphaOffset)
        {
            if (alphaElement is null && alphaModulation is null && alphaOffset is null)
            {
                return color;
            }

            var alphaValue = alphaElement?.Val;
            var modulationVal = alphaModulation?.Val;
            var offsetVal = alphaOffset?.Val;

            var alpha = alphaValue is not null ? new Percentage(alphaValue) : Percentage.FromDouble(1);

            var mod = modulationVal is not null ? new Percentage(modulationVal) : Percentage.FromDouble(1);

            var off = offsetVal is not null ? new Percentage(offsetVal) : Percentage.Zero;


            var alphaResult = alpha.DoubleValue * mod.DoubleValue + off.DoubleValue;
            color.A = (byte) (color.A * alphaResult);


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
