using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement
{
    /// <summary>
    /// 变换信息，包括坐标和尺寸旋转翻转等信息
    /// </summary>
    public interface ITransformData
    {
        /// <summary>
        /// X 坐标
        /// </summary>
        Emu OffsetX { get; }
        /// <summary>
        /// Y 坐标
        /// </summary>
        Emu OffsetY { get; }
        /// <summary>
        /// 宽度
        /// </summary>
        Emu Width { get; }
        /// <summary>
        /// 高度
        /// </summary>
        Emu Height { get; }
        /// <summary>
        /// 旋转
        /// </summary>
        Degree Rotation { get; }
        /// <summary>
        /// 是否水平翻转
        /// </summary>
        bool HorizontalFlip { get; }
        /// <summary>
        /// 是否垂直翻转
        /// </summary>
        bool VerticalFlip { get; }
    }
}
