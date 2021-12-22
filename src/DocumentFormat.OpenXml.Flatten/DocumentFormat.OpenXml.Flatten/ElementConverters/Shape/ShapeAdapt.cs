using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    /// <summary>
    /// 存在 Shape 和 ConnectionShape 这两个不同的类型，需要使用适配器
    /// </summary>
    public class ShapeAdapt
    {
        /// <summary>
        /// 创建形状适配器
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="shapeProperties"></param>
        /// <param name="shapeStyle"></param>
        /// <param name="nonVisualShapeProperties"></param>
        /// <param name="useBackgroundFill"></param>
        /// <param name="textBody"></param>
        public ShapeAdapt(OpenXmlCompositeElement? shape, ShapeProperties? shapeProperties,
            ShapeStyle? shapeStyle,
            NonVisualShapeProperties? nonVisualShapeProperties = null,
            bool? useBackgroundFill = null,
            TextBody? textBody = null)
        {
            ShapeProperties = shapeProperties;
            ShapeStyle = shapeStyle;
            NonVisualShapeProperties = nonVisualShapeProperties;
            UseBackgroundFill = useBackgroundFill;
            TextBody = textBody;
            Shape = shape;
        }

        /// <summary>
        /// 形状元素，可能是 <see cref="ConnectionShape"/> 和 <see cref="DocumentFormat.OpenXml.Presentation.Shape"/>
        /// </summary>
        public OpenXmlCompositeElement? Shape { get; }

        /// <summary>
        /// 形状属性
        /// </summary>
        public ShapeProperties? ShapeProperties { get; }

        /// <summary>
        /// 形状样式
        /// </summary>
        public ShapeStyle? ShapeStyle { get; }

        /// <summary>
        /// 形状不可见属性
        /// </summary>
        public NonVisualShapeProperties? NonVisualShapeProperties { get; }

        /// <summary>
        /// 是否使用背景填充
        /// </summary>
        /// 此属性也许没有什么用
        public bool? UseBackgroundFill { get; }

        /// <summary>
        /// 形状里面的文本
        /// </summary>
        public TextBody? TextBody { get; }

        /// <summary>
        /// 转换为形状适配器
        /// </summary>
        public static implicit operator ShapeAdapt(ConnectionShape shape) => new ShapeAdapt
        (
            shape,
            shape.ShapeProperties,
            shape.ShapeStyle
        );

        /// <summary>
        /// 转换为形状适配器
        /// </summary>
        public static implicit operator ShapeAdapt(DocumentFormat.OpenXml.Presentation.Shape shape) =>
            new ShapeAdapt
            (
                shape,
                shape.ShapeProperties,
                shape.ShapeStyle,
                shape.NonVisualShapeProperties,
                shape.UseBackgroundFill?.Value,
                shape.TextBody
            );
    }
}
