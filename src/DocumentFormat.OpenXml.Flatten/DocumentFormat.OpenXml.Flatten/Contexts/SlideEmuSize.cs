using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.Contexts
{
    /// <summary>
    /// 页面的使用 EMU 单位表示的 Size 尺寸
    /// </summary>
    public readonly struct SlideEmuSize
    {
        /// <summary>
        /// 创建使用 EMU 单位表示的 Size 尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public SlideEmuSize(Emu width, Emu height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public Emu Width { get; }
        /// <summary>
        /// 高度
        /// </summary>
        public Emu Height { get; }
    }
}
