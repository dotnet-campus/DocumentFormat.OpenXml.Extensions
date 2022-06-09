using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    /// <summary>
    /// 形状属性适配器
    /// </summary>
    public class ShapePropertiesAdapt
    {
        /// <summary>
        /// 形状属性
        /// </summary>
        public OpenXmlCompositeElement? ShapeProperties { get; }

        /// <summary>
        /// 创建形状属性适配器
        /// </summary>
        /// <param name="shapeProperties"></param>
        public ShapePropertiesAdapt(OpenXmlCompositeElement? shapeProperties)
        {
            ShapeProperties = shapeProperties;
        }

        /// <summary>
        /// 转换为形状属性适配器
        /// </summary>
        /// <param name="shapeProperties"></param>
        /// <returns></returns>
        public static implicit operator ShapePropertiesAdapt(DocumentFormat.OpenXml.Presentation.ShapeProperties? shapeProperties) =>
        new(shapeProperties);

        /// <summary>
        /// 转换为形状属性适配器
        /// </summary>
        /// <param name="shapeProperties"></param>
        /// <returns></returns>
        public static implicit operator ShapePropertiesAdapt(DocumentFormat.OpenXml.Office.Drawing.ShapeProperties? shapeProperties) =>
            new(shapeProperties);

        /// <summary>
        /// 获取Transform2D
        /// </summary>
        /// <returns></returns>
        public Transform2D? GetTransform2D()
        {
            return ShapeProperties?.GetFirstChild<Transform2D>();
        }

    }
}
