using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.Contexts
{
    /// <summary>
    /// 记录自定义形状信息
    /// </summary>
    public readonly struct CustomGeometryInfo
    {
        /// <summary>
        /// 创建记录自定义形状信息
        /// </summary>
        /// <param name="shapeGuideList">形状导航集合</param>
        /// <param name="adjustValueList">调整点集合</param>
        /// <param name="pathList">路径集合</param>
        /// <param name="rectangle">文本框范围</param>
        public CustomGeometryInfo(ShapeGuideList? shapeGuideList, AdjustValueList? adjustValueList, PathList? pathList, Rectangle? rectangle)
        {
            ShapeGuideList = shapeGuideList;
            AdjustValueList = adjustValueList;
            PathList = pathList;
            Rectangle = rectangle;
        }

        /// <summary>
        /// 形状导航集合
        /// </summary>
        public ShapeGuideList? ShapeGuideList { get; }

        /// <summary>
        /// 调整点集合
        /// </summary>
        public AdjustValueList? AdjustValueList { get; }

        /// <summary>
        /// 路径集合
        /// </summary>
        public PathList? PathList { get; }

        /// <summary>
        /// 文本框范围
        /// </summary>
        public Rectangle? Rectangle { get; }
    }
}
