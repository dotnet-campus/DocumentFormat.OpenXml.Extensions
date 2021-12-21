using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement
{
    class TransformData :
        // 继承 OpenXmlLeafElement 是为了做缓存，可以放在 OpenXML 元素
        OpenXmlLeafElement,
        ITransformData
    {
        public Emu OffsetX { get; set; } = new Emu(0);
        public Emu OffsetY { get; set; } = new Emu(0);
        public Emu Width { get; set; } = new Emu(0);
        public Emu Height { get; set; } = new Emu(0);
        public Degree Rotation { get; set; } = new Degree(0);
        public bool HorizontalFlip { get; set; } = false;
        public bool VerticalFlip { get; set; } = false;

        // 以下是继承 OpenXmlLeafElement 需要实现的属性
        public override string InnerXml { get => string.Empty; set { } }
        public override string Prefix => string.Empty;
        public override string InnerText { get => string.Empty; protected set { } }
    }
}
