using System;
using System.Diagnostics;
using System.Linq;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Flatten.Framework.Context
{
    /// <summary>
    ///     页面上下文
    /// </summary>
    public class SlideContext
    {
        /// <summary>
        /// 创建页面上下文
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="document"></param>
        public SlideContext(OpenXmlPartRootElement rootElement, PresentationDocument document)
        {
            RootElement = rootElement;
            Document = document;
        }

        /// <summary>
        ///     对应的元素最上层元素，如页面，如页面模版等
        /// </summary>
        public OpenXmlPartRootElement RootElement { get; }

        /// <summary>
        ///     PPT文件
        /// </summary>
        public PresentationDocument Document { get; }

        /// <summary>
        ///     页面数据
        ///     这里不能直接认为元素就在slide上，还要考虑layout和master以及是否在主题上
        /// </summary>
        public OpenXmlPart GetCurrentPart(OpenXmlElement? element = null)
        {
            var openXmlPartRootElement = element?.Ancestors<OpenXmlPartRootElement>().FirstOrDefault();
            if (openXmlPartRootElement is DocumentFormat.OpenXml.Drawing.Theme theme)
            {
                var themePart = theme.ThemePart;
                if (themePart != null)
                {
                    return themePart;
                }
            }

            if (_currentPart != null)
            {
                return _currentPart;
            }

            if (RootElement is Slide slide)
            {
                _currentPart = slide.SlidePart;
            }
            else if (RootElement is SlideLayout slideLayout)
            {
                _currentPart = slideLayout.SlideLayoutPart;
            }
            else if (RootElement is SlideMaster slideMaster)
            {
                _currentPart = slideMaster.SlideMasterPart;
            }
            else
            {
                throw new ArgumentException($"{nameof(RootElement)} must be Slide or SlideLayout or SlideMaster");
            }

            if (_currentPart is null)
            {
#if DEBUG
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
#endif
                throw new ArgumentException($"Can not find any part.");
            }

            return _currentPart;
        }

        private OpenXmlPart? _currentPart;
    }
}
