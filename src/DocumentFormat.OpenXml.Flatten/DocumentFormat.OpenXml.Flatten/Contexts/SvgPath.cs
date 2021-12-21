using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.Contexts
{
    /// <summary>
    /// 提供包含Svg Path信息的类
    /// </summary>
    public class SvgPath
    {
        /// <summary>
        /// 创建 <see cref="T:DocumentFormat.OpenXml.Flatten.Contexts.SvgPath" />路径信息
        /// </summary>
        /// <param name="geometryShapeType">几何形状类型</param>
        /// <param name="svgPathString">Svg Path字符串</param>
        /// <param name="shapeTextRectangle">形状文本框</param>
        /// <param name="multiShapePaths">Svg 多路径</param>
        public SvgPath(ShapeTypeValues? geometryShapeType, string? svgPathString, EmuShapeTextRectangle shapeTextRectangle, ShapePath[]? multiShapePaths)
        {
            ShapeType = geometryShapeType;
            SvgPathString = svgPathString;
            ShapeTextRectangle = shapeTextRectangle;
            MultiShapePaths = multiShapePaths;
        }

        /// <summary>
        /// 形状类型
        /// </summary>
        public ShapeTypeValues? ShapeType { get; }

        /// <summary>
        /// Svg Path字符串
        /// </summary>
        public string? SvgPathString { get; }

        /// <summary>
        /// 形状的文本框
        /// </summary>
        public EmuShapeTextRectangle ShapeTextRectangle { get; }

        /// <summary>
        ///Shape Path 多路径
        /// </summary>
        public ShapePath[]? MultiShapePaths { get; }
    }
}
