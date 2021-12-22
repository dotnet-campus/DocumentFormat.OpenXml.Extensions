using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Flatten.Framework.Context
{
    /// <summary>
    /// 给每个元素独立的上下文信息
    /// </summary>
    /// 为什么定义结构体？原因是信息少，拷贝结构体性能损耗不多。而元素数量较多，创建类相对损耗比较多
    public readonly struct ElementContext
    {
        /// <summary>
        /// 创建给每个元素独立的上下文信息
        /// </summary>
        public ElementContext(OpenXmlElement originOpenXmlElement, SlideContext slideContext)
        {
            SlideContext = slideContext;
            OriginOpenXmlElement = originOpenXmlElement;

            GroupShape = originOpenXmlElement.Parent as GroupShape;
        }

        /// <summary>
        /// 页面上下文信息
        /// </summary>
        public SlideContext SlideContext { get; }

        /// <summary>
        /// 原始的 OpenXML 元素的对象。因为实际上传入的对象是被 Clone 一次的
        /// </summary>
        public OpenXmlElement OriginOpenXmlElement { get; }

        /// <summary>
        /// 如果元素放在组合内，那么此属性有值。否则是空
        /// </summary>
        public GroupShape? GroupShape { get; }

        /// <summary>
        ///     对应的元素最上层元素，如页面，如页面模版等
        /// </summary>
        public OpenXmlPartRootElement RootElement => SlideContext.RootElement;

        /// <summary>
        ///     PPT文件
        /// </summary>
        public PresentationDocument Document => SlideContext.Document;

        /// <summary>
        ///     页面数据
        ///     这里不能直接认为元素就在slide上，还要考虑layout和master
        /// </summary>
        public OpenXmlPart GetCurrentPart(OpenXmlElement? _ = null) => SlideContext.GetCurrentPart();
    }
}
