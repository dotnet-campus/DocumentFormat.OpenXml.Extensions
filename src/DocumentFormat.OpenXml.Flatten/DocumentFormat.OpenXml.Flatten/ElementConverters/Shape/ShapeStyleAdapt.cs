using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    /// <summary>
    /// 形状样式适配器
    /// </summary>
    public class ShapeStyleAdapt
    {
        /// <summary>
        /// 形状样式
        /// </summary>
        public OpenXmlCompositeElement? ShapeStyle { get; }

        /// <summary>
        /// 构建形状样式适配器
        /// </summary>
        /// <param name="shapeStyle"></param>
        public ShapeStyleAdapt(OpenXmlCompositeElement? shapeStyle)
        {
            ShapeStyle = shapeStyle;
        }

        /// <summary>
        /// 转换为形状样式适配器
        /// </summary>
        /// <param name="shapeStyle"></param>
        /// <returns></returns>
        public static implicit operator ShapeStyleAdapt(DocumentFormat.OpenXml.Presentation.ShapeStyle? shapeStyle) =>
            new(shapeStyle);

        /// <summary>
        /// 转换为形状样式适配器
        /// </summary>
        /// <param name="shapeStyle"></param>
        /// <returns></returns>
        public static implicit operator ShapeStyleAdapt(DocumentFormat.OpenXml.Office.Drawing.ShapeStyle? shapeStyle) =>
            new(shapeStyle);

        /// <summary>
        /// 获取FontReference
        /// </summary>
        /// <returns></returns>
        public FontReference? GetFontReference()
        {
            return ShapeStyle?.GetFirstChild<FontReference>();
        }

        /// <summary>
        /// 获取FontReference
        /// </summary>
        /// <returns></returns>
        public LineReference? GetLineReference()
        {
            return ShapeStyle?.GetFirstChild<LineReference>();
        }
    }
}
