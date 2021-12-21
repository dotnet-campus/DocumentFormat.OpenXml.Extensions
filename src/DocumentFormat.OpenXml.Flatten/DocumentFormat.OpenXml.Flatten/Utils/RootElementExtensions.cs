using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

using ColorMap = DocumentFormat.OpenXml.Presentation.ColorMap;

namespace DocumentFormat.OpenXml.Flatten.Utils
{
    /// <summary>
    ///     <see cref="OpenXmlPartRootElement" />的扩展方法
    ///     可用于<see cref="Slide" />,<see cref="SlideLayout" />,<see cref="SlideMaster" />
    /// </summary>
    static class RootElementExtensions
    {
        /// <summary>
        ///     获取指定的FormatScheme
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static FormatScheme? GetFormatScheme(this OpenXmlPartRootElement root)
        {
            var (slidePart, slideLayoutPart, slideMasterPart) = GetParts(root);

            //从当前Slide获取theme
            if (slidePart?.ThemeOverridePart?.ThemeOverride?.FormatScheme != null)
                return slidePart.ThemeOverridePart.ThemeOverride.FormatScheme;

            //从SlideLayout获取theme
            if (slideLayoutPart?.ThemeOverridePart?.ThemeOverride?.FormatScheme != null)
                return slideLayoutPart.ThemeOverridePart.ThemeOverride.FormatScheme;

            //从SlideMaster获取theme
            return slideMasterPart?.ThemePart?.Theme?.ThemeElements?.FormatScheme;
        }

        /// <summary>
        ///     获取指定的FontScheme
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static FontScheme? GetFontScheme(
            this OpenXmlPartRootElement root)
        {
            var (slidePart, slideLayoutPart, slideMasterPart) = GetParts(root);

            //从当前Slide获取theme
            if (slidePart?.ThemeOverridePart?.ThemeOverride?.FontScheme != null)
                return slidePart.ThemeOverridePart.ThemeOverride.FontScheme;

            //从SlideLayout获取theme
            if (slideLayoutPart?.ThemeOverridePart?.ThemeOverride?.FontScheme != null)
                return slideLayoutPart.ThemeOverridePart.ThemeOverride.FontScheme;

            //从SlideMaster获取theme
            return slideMasterPart?.ThemePart?.Theme?.ThemeElements?.FontScheme;
        }

        /// <summary>
        ///     获取指定的ColorScheme
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static ColorScheme? GetColorScheme(
            this OpenXmlPartRootElement root)
        {
            var (slidePart, slideLayoutPart, slideMasterPart) = GetParts(root);

            //从当前Slide获取theme
            if (slidePart?.ThemeOverridePart?.ThemeOverride?.ColorScheme != null)
                return slidePart.ThemeOverridePart.ThemeOverride.ColorScheme;

            //从SlideLayout获取theme
            if (slideLayoutPart?.ThemeOverridePart?.ThemeOverride?.ColorScheme != null)
                return slideLayoutPart.ThemeOverridePart.ThemeOverride.ColorScheme;

            //从SlideMaster获取theme
            return slideMasterPart?.ThemePart?.Theme?.ThemeElements?.ColorScheme;
        }


        /// <summary>
        ///     从Slide获取ColorMap
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static ColorMap? GetColorMap(this OpenXmlPartRootElement root)
        {
            var (slidePart, slideLayoutPart, slideMasterPart) = GetParts(root);

            var masterColorMap = slideMasterPart?.SlideMaster.ColorMap;

            //从当前Slide获取ColorMap
            if (slidePart?.Slide.ColorMapOverride != null)
            {
                if (slidePart.Slide.ColorMapOverride.MasterColorMapping != null) return masterColorMap;

                if (slidePart.Slide.ColorMapOverride.OverrideColorMapping != null)
                    return slidePart.Slide.ColorMapOverride.OverrideColorMapping.ToColorMap();
            }

            //从SlideLayout获取ColorMap
            if (slideLayoutPart?.SlideLayout.ColorMapOverride != null)
            {
                if (slideLayoutPart.SlideLayout.ColorMapOverride.MasterColorMapping != null) return masterColorMap;

                if (slideLayoutPart.SlideLayout.ColorMapOverride.OverrideColorMapping != null)
                    return slideLayoutPart.SlideLayout.ColorMapOverride.OverrideColorMapping.ToColorMap();
            }

            //从SlideMaster获取ColorMap
            return masterColorMap;
        }

        private static (SlidePart? slidePart, SlideLayoutPart? slideLayoutPart, SlideMasterPart? slideMasterPart) GetParts(
            OpenXmlPartRootElement root)
        {
            SlidePart? slidePart = null;
            SlideLayoutPart? slideLayoutPart = null;
            SlideMasterPart? slideMasterPart = null;
            if (root is Slide slide) slidePart = slide.SlidePart;

            if (slidePart != null)
                slideLayoutPart = slidePart.SlideLayoutPart;
            else if (root is SlideLayout slideLayout) slideLayoutPart = slideLayout.SlideLayoutPart;

            if (slideLayoutPart != null)
                slideMasterPart = slideLayoutPart.SlideMasterPart;
            else if (root is SlideMaster slideMaster) slideMasterPart = slideMaster.SlideMasterPart;

            return (slidePart, slideLayoutPart, slideMasterPart);
        }


        /// <summary>
        ///     将<see cref="OverrideColorMapping" />转换为<see cref="ColorMap" />
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public static ColorMap ToColorMap(this OverrideColorMapping mapping)
        {
            return new ColorMap
            {
                Accent1 = mapping.Accent1,
                Accent2 = mapping.Accent2,
                Accent3 = mapping.Accent3,
                Accent4 = mapping.Accent4,
                Accent5 = mapping.Accent5,
                Accent6 = mapping.Accent6,
                Background1 = mapping.Background1,
                Background2 = mapping.Background2,
                FollowedHyperlink = mapping.FollowedHyperlink,
                Hyperlink = mapping.Hyperlink,
                Text1 = mapping.Text1,
                Text2 = mapping.Text2
            };
        }
    }
}
