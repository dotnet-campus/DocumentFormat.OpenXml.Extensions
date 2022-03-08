using DocumentFormat.OpenXml.Drawing;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.Contexts
{
    /// <summary>
    /// 对应PPT的Shape Path
    /// </summary>
    public readonly struct ShapePath
    {
        /// <summary>
        /// 创建PPT的Geometry Path
        /// </summary>
        /// <param name="path">OpenXml  Path字符串</param>
        /// <param name="fillMode">OpenXml的Path Fill Mode  </param>
        /// <param name="isStroke">是否有轮廓</param>
        /// <param name="isExtrusionOk">指定使用 3D 拉伸可能在此路径</param>
        /// <param name="emuWidth">指定的宽度或在路径坐标系统中应在使用的最大的 x 坐标</param>
        /// <param name="emuHeight">指定框架的高度或在路径坐标系统中应在使用的最大的 y 坐标</param>
        public ShapePath(string path, PathFillModeValues fillMode = PathFillModeValues.Norm, bool isStroke = true, bool isExtrusionOk = false, double? emuWidth = null, double? emuHeight = null)
        {
            Path = path;
            IsStroke = isStroke;
            FillMode = fillMode;
            IsFilled = fillMode is not PathFillModeValues.None;
            IsExtrusionOk = isExtrusionOk;
            Width = emuWidth.HasValue ? new Emu(emuWidth.Value) : null;
            Height = emuHeight.HasValue ? new Emu(emuHeight.Value) : null;
        }

        /// <summary>
        /// 创建PPT的Geometry Path
        /// </summary>
        /// <param name="path">OpenXml  Path字符串</param>
        /// <param name="eumWidth">指定的宽度或在路径坐标系统中应在使用的最大的 x 坐标</param>
        /// <param name="eumHeight">指定框架的高度或在路径坐标系统中应在使用的最大的 y 坐标</param>
        public ShapePath(string path, double eumWidth, double eumHeight) : this(path, PathFillModeValues.Norm, emuWidth: eumWidth, emuHeight: eumHeight)
        {

        }

        /// <summary>
        /// 是否填充
        /// </summary>
        public bool IsFilled { get; }

        /// <summary>
        /// OpenXml 的 Path Stroke, 默认true
        /// </summary>
        public bool IsStroke { get; }

        /// <summary>
        /// OpenXml的Path Fill Mode  
        /// </summary>
        public PathFillModeValues FillMode { get; }

        /// <summary>
        ///OpenXml  Path字符串
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 指定使用 3D 拉伸可能在此路径，默认false或0
        /// </summary>
        public bool IsExtrusionOk { get; }

        /// <summary>
        /// 指定的宽度或在路径坐标系统中应在使用的最大的 x 坐标。默认是 0 的值
        /// </summary>
        public Emu? Width { get; }

        /// <summary>
        /// 指定框架的高度或在路径坐标系统中应在使用的最大的 y 坐标。默认是 0 的值
        /// </summary>
        public Emu? Height { get; }
    }
}
