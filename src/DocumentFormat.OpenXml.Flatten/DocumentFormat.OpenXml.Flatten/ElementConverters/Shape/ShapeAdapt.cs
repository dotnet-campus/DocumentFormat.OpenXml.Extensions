using DocumentFormat.OpenXml.Flatten.ElementConverters.Text;
using DocumentFormat.OpenXml.Presentation;

using Shape = DocumentFormat.OpenXml.Presentation.Shape;
using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;
using ShapeStyle = DocumentFormat.OpenXml.Presentation.ShapeStyle;
using SmartArtShape = DocumentFormat.OpenXml.Office.Drawing.Shape;
using SmartArtShapeNonVisualProperties = DocumentFormat.OpenXml.Office.Drawing.ShapeNonVisualProperties;
using SmartArtShapeProperties = DocumentFormat.OpenXml.Office.Drawing.ShapeProperties;
using SmartArtShapeStyle = DocumentFormat.OpenXml.Office.Drawing.ShapeStyle;
using SmartArtTextBody = DocumentFormat.OpenXml.Office.Drawing.TextBody;
using TextBody = DocumentFormat.OpenXml.Presentation.TextBody;

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
        /// <param name="shapePropertiesAdapt"></param>
        /// <param name="shapeStyleAdapt"></param>
        /// <param name="nonVisualShapeProperties"></param>
        /// <param name="useBackgroundFill"></param>
        /// <param name="textBody"></param>
        public ShapeAdapt(OpenXmlCompositeElement? shape, ShapeProperties? shapePropertiesAdapt,
            ShapeStyle? shapeStyleAdapt,
            NonVisualShapeProperties? nonVisualShapeProperties = null,
            bool? useBackgroundFill = null,
            TextBody? textBody = null)
        {
            ShapePropertiesAdapt = shapePropertiesAdapt;
            ShapeStyleAdapt = shapeStyleAdapt;
            NonVisualShapeProperties = nonVisualShapeProperties;
            UseBackgroundFill = useBackgroundFill;
            TextBodyAdapt = textBody;
            Shape = shape;
        }

        /// <summary>
        /// 适配SmartArt的形状构造
        /// </summary>
        /// <param name="smartArtShape"></param>
        /// <param name="smartArtShapeProperties"></param>
        /// <param name="shapeStyle"></param>
        /// <param name="smartArtShapeNonVisualProperties"></param>
        /// <param name="smartArtTextBody"></param>
        public ShapeAdapt(SmartArtShape? smartArtShape,
            SmartArtShapeProperties? smartArtShapeProperties,
            SmartArtShapeStyle? shapeStyle,
            SmartArtShapeNonVisualProperties? smartArtShapeNonVisualProperties = null,
            SmartArtTextBody? smartArtTextBody = null)
        {
            ShapePropertiesAdapt = smartArtShapeProperties;
            ShapeStyleAdapt = shapeStyle;
            SmartArtShapeNonVisualProperties = smartArtShapeNonVisualProperties;
            TextBodyAdapt = smartArtTextBody;
            Shape = smartArtShape;
        }

        /// <summary>
        /// 形状元素，可能是 <see cref="ConnectionShape"/> 和 <see cref="DocumentFormat.OpenXml.Presentation.Shape"/>和<see cref="DocumentFormat.OpenXml.Office.Drawing.Shape"/>
        /// </summary>
        public OpenXmlCompositeElement? Shape { get; }

        /// <summary>
        /// 形状属性
        /// </summary>
        public ShapePropertiesAdapt? ShapePropertiesAdapt { get; }

        /// <summary>
        /// SmartArt形状不可见属性
        /// </summary>
        public SmartArtShapeNonVisualProperties? SmartArtShapeNonVisualProperties { get; }

        /// <summary>
        /// 形状样式
        /// </summary>
        public ShapeStyleAdapt? ShapeStyleAdapt { get; }


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
        public TextBodyAdapt? TextBodyAdapt { get; }

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
        public static implicit operator ShapeAdapt(Shape shape) =>
            new ShapeAdapt
            (
                shape,
                shape.ShapeProperties,
                shape.ShapeStyle,
                shape.NonVisualShapeProperties,
                shape.UseBackgroundFill?.Value,
                shape.TextBody
            );

        /// <summary>
        /// 转换为形状适配器
        /// </summary>
        public static implicit operator ShapeAdapt(DocumentFormat.OpenXml.Office.Drawing.Shape shape) =>
            new ShapeAdapt
            (
                shape,
                shape.ShapeProperties,
                shape.ShapeStyle,
                shape.ShapeNonVisualProperties,
                shape.TextBody
            );
    }
}
