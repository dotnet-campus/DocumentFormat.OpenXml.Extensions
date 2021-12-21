using System.Linq;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Framework;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Flatten.Utils;
using DocumentFormat.OpenXml.Presentation;

using NonVisualShapeDrawingProperties = DocumentFormat.OpenXml.Presentation.NonVisualShapeDrawingProperties;
using NonVisualShapeProperties = DocumentFormat.OpenXml.Presentation.NonVisualShapeProperties;
using Shape = DocumentFormat.OpenXml.Presentation.Shape;
using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    class ShapeFlattenConverter : OpenXmlElementFlattenConverterBase<DocumentFormat.OpenXml.Presentation.Shape>
    {
        protected override Shape Convert(Shape element, ElementContext context)
        {
            var presentation = context.Document.PresentationPart!.Presentation;
            var defaultTextStyle = presentation.DefaultTextStyle;
            var defaultTextStyleTextParagraphPropertiesList = defaultTextStyle?.ToTextParagraphPropertiesTypeList();
            var shapeProperties = element.GetOrCreateElement<ShapeProperties>();

            // <p:nvSpPr> 占位符的样式
            NonVisualShapeProperties? nonVisualShapeProperties = element.GetOrCreateElement<NonVisualShapeProperties>();

            // cNvSpPr
            NonVisualShapeDrawingProperties? nonVisualShapeDrawingProperties = nonVisualShapeProperties.GetOrCreateElement<NonVisualShapeDrawingProperties>();

            //var IsTextBox = nonVisualShapeDrawingProperties.TextBox?.Value is true;

            // nvpr
            var applicationNonVisualDrawingProperties = nonVisualShapeProperties?.ApplicationNonVisualDrawingProperties;

            // [dotnet OpenXML SDK 文本占位符解析](https://blog.lindexi.com/post/dotnet-OpenXML-SDK-%E6%96%87%E6%9C%AC%E5%8D%A0%E4%BD%8D%E7%AC%A6%E8%A7%A3%E6%9E%90.html)
            var placeholderShape = applicationNonVisualDrawingProperties?.PlaceholderShape;
            if (placeholderShape != null)
            {
                // 这里仅获取用来做样式的
                var (layoutPlaceholderShape, masterPlaceholderShape) =
                    PlaceholderHelper.GetPlaceholderShapesForStyle(placeholderShape, context.RootElement);

                var placeholderShapeType = placeholderShape.Type;

                // 占位符类型有很多，这里强行分为标题和非标题
                if (IsMainTitle(placeholderShapeType))
                {
                    //GeMasterTextStyles()
                }
                else
                {

                }

                // 如果占位符元素存在，那么需要从占位符元素读取文本的样式
                if (layoutPlaceholderShape != null || masterPlaceholderShape != null)
                {
                    // 待读取文本属性
                    // var textBody = placeholderShapeElement.TextBody;

                    nonVisualShapeDrawingProperties.TextBox = ElementFlattenExtension
                        .GetFlattenProperty<NonVisualShapeDrawingProperties, BooleanValue>(properties => properties.TextBox!, nonVisualShapeDrawingProperties,
                            layoutPlaceholderShape?.NonVisualShapeProperties?.NonVisualShapeDrawingProperties!,
                            masterPlaceholderShape?.NonVisualShapeProperties?.NonVisualShapeDrawingProperties!);

                    FlattenTransformData(shapeProperties, layoutPlaceholderShape, masterPlaceholderShape, context);
                }
            }

            FillOutline(element, context);
            FillShapeBackgroundBrush(element, context);

            return element;
        }

        private void FlattenTransformData(ShapeProperties shapeProperties, Shape? layoutPlaceholderShape, Shape? masterPlaceholderShape, ElementContext context)
        {
            var layoutTransform2D = layoutPlaceholderShape?.ShapeProperties?.Transform2D;
            var masterTransform2D = masterPlaceholderShape?.ShapeProperties?.Transform2D;
            var provider = new OpenXmlCompositeElementFlattenProvider<Transform2D>
            (
                shapeProperties.GetOrCreateElement<Transform2D>(),
                layoutTransform2D,
                masterTransform2D
            );

            provider.FlattenMainElementProperty(transform2D => transform2D.Offset!)
                .FlattenMainElementProperty(transform2D => transform2D.Extents!);

            var elementTransform2D = provider.Main;
            elementTransform2D.Rotation = provider.GetFlattenProperty(transform2D => transform2D.Rotation);
            elementTransform2D.HorizontalFlip = provider.GetFlattenProperty(transform2D => transform2D.HorizontalFlip);
            elementTransform2D.VerticalFlip = provider.GetFlattenProperty(transform2D => transform2D.VerticalFlip);
        }

        private void FillShapeBackgroundBrush(Shape element, ElementContext context)
        {
            var shapeProperties = element.ShapeProperties;
            var fillReference = element.ShapeStyle?.FillReference;

            var themeBrush = fillReference.FlattenReferenceBrush(context.SlideContext);

            // 合并颜色，通过 BrushHelper.MergeBrush 可以将 themeBrush 的颜色合入到 shapeProperties 里面
            // 合并颜色解决的问题是 issues/26 形状渐变丢失
            // 没有显示的原因是在 ShapeProperties 虽然也定义了颜色是渐变，但是 GradientStops 没有值，因为这个属性的值需要在 ShapeStyle 里面通过 FillReference 拿到主题的颜色，这里主题的颜色也是渐变色，拿到渐变色的 GradientStops 放在相同的类
            var _ = BrushHelper.MergeBrush(shapeProperties, themeBrush, context);
        }

        /// <summary>
        /// 填充轮廓
        /// </summary>
        /// <param name="element"></param>
        /// <param name="context"></param>
        private void FillOutline(Shape element, ElementContext context)
        {
            var shapeOutlineFlatten = new ShapeOutlineFlatten(element, context);
            shapeOutlineFlatten.Convert();
        }

        private bool IsMainTitle(EnumValue<PlaceholderValues>? placeholderShapeType)
        {
            if (placeholderShapeType != null)
            {
                var type = placeholderShapeType.Value;

                return type == PlaceholderValues.Title || type == PlaceholderValues.CenteredTitle;
            }

            return false;
        }
    }
}
