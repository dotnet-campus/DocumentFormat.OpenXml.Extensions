using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.Contexts
{
    /// <summary>
    /// 使用 Emu 单位的形状里的文本范围,对应 OpenXml Shape 的 a:rect 标签
    /// </summary>
    public readonly struct EmuShapeTextRectangle
    {
        /// <summary>
        /// 创建形状里的文本范围
        /// </summary>
        public EmuShapeTextRectangle(Emu left, Emu top, Emu right, Emu bottom)
        {
            var emuWidth = new Emu(right.Value - left.Value);
            var emuHeight = new Emu(bottom.Value - top.Value);

            OriginPoint = new EmuPoint(left, top);
            Size = new EmuSize(emuWidth, emuHeight);
        }

        /// <summary>
        /// 创建形状里的文本范围
        /// </summary>
        /// <param name="size">形状文本框的大小</param>
        /// <param name="originPoint">形状文本框的原点坐标</param>
        public EmuShapeTextRectangle(EmuSize size, EmuPoint originPoint)
        {
            Size = size;
            OriginPoint = originPoint;
        }

        /// <summary>
        /// 形状文本框的坐标原点
        /// </summary>
        public EmuPoint OriginPoint { get; }

        /// <summary>
        /// Rectangle的大小
        /// </summary>
        public EmuSize Size { get; }
    }

    /// <summary>
    /// Emu单位的形状文本框的Margin
    /// </summary>
    public readonly struct EmuTextMargin
    {
        /// <summary>
        /// 创建EmuTextMargin
        /// </summary>
        /// <param name="emuLeftInset">emu单位的LeftInset</param>
        /// <param name="emuTopInset">emu单位的TopInset</param>
        /// <param name="emuRightInset">emu单位的RightInset</param>
        /// <param name="emuBottomInset">emu单位的BottomInset</param>
        public EmuTextMargin(double emuLeftInset, double emuTopInset, double emuRightInset, double emuBottomInset)
        {
            LeftInset = new Emu(emuLeftInset);
            TopInset = new Emu(emuTopInset);
            RightInset = new Emu(emuRightInset);
            BottomInset = new Emu(emuBottomInset);
        }

        /// <summary>
        /// 对应 a:bodyPr lIns
        /// </summary>
        public Emu LeftInset { get; }

        /// <summary>
        /// 对应 a:bodyPr rIns
        /// </summary>
        public Emu RightInset { get; }

        /// <summary>
        /// 对应 a:bodyPr bIns
        /// </summary>
        public Emu BottomInset { get; }

        /// <summary>
        /// 对应 a:bodyPr tIns
        /// </summary>
        public Emu TopInset { get; }
    }
}
