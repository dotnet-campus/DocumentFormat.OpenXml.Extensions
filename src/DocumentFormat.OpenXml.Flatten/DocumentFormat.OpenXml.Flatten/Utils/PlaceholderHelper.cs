using System.Diagnostics;
using System.Linq;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Presentation;

using NonVisualPictureProperties = DocumentFormat.OpenXml.Presentation.NonVisualPictureProperties;
using NonVisualShapeProperties = DocumentFormat.OpenXml.Presentation.NonVisualShapeProperties;
using Shape = DocumentFormat.OpenXml.Presentation.Shape;
using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;

namespace DocumentFormat.OpenXml.Flatten.Utils
{
    static class PlaceholderHelper
    {
        /// <summary>
        ///     获取元素的<see cref="Transform2D" />信息
        /// </summary>
        public static Transform2D? GetTransform2DFromPlaceholder(OpenXmlElement? element, SlideContext context)
        {
            if (element is null) return null;
            // 这里原本只是想处理占位符相关逻辑，但是也没有限制在不存在占位符调用
            // 先判断是否实际上有值，如果有值，就不继续后续逻辑
            var currentValue = element.GetFirstChild<ShapeProperties>()?.Transform2D;
            if (currentValue is not null) return currentValue;

            var placeholder = GetPlaceholder(element);
            if (placeholder is null) return null;

            var (layoutPlaceholderShape, masterPlaceholderShape) =
                   GetPlaceholderElementForStyle(placeholder, context.RootElement);

            var layoutValue = layoutPlaceholderShape?.GetFirstChild<ShapeProperties>()?.Transform2D;
            if (layoutValue is not null) return layoutValue;

            var masterValue = masterPlaceholderShape?.GetFirstChild<ShapeProperties>()?.Transform2D;
            if (masterValue is not null) return masterValue;

            return null;
        }

        /// <summary>
        ///    判断某元素是否是 Placeholder 元素
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsPlaceholder(OpenXmlElement? element) => GetPlaceholder(element) != null;

        /// <summary>
        ///     尝试获取元素的 <see cref="PlaceholderShape"/> 元素
        /// </summary>
        public static PlaceholderShape? GetPlaceholder(OpenXmlElement? element)
        {
            if (element is null) return null;

            var placeholder = GetShapePlaceholder(element);
            if (placeholder is not null) return placeholder;

            placeholder = GetPicturePlaceholder(element);
            return placeholder;

            static PlaceholderShape? GetPicturePlaceholder(OpenXmlElement element)
            {
                var nonVisualPictureProperties
                    = element
                    .GetFirstChild<NonVisualPictureProperties>();

                var placeholderShape = nonVisualPictureProperties?.ApplicationNonVisualDrawingProperties
                   ?.PlaceholderShape;
                return placeholderShape;
            }

            static PlaceholderShape? GetShapePlaceholder(OpenXmlElement element)
            {
                var nonVisualShapeProperties
                   = element
                   .GetFirstChild<NonVisualShapeProperties>();
                // [c# - Getting the placeholder values with Open XML SDK 2.0 - Stack Overflow](https://stackoverflow.com/questions/26264815/getting-the-placeholder-values-with-open-xml-sdk-2-0 )
                var placeholderShape = nonVisualShapeProperties?.ApplicationNonVisualDrawingProperties
                    ?.PlaceholderShape;
                return placeholderShape;
            }
        }

        /// <summary>
        ///     判断某元素是否是placeholder
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static bool IsPlaceholder(this Shape? shape)
        {
            return IsPlaceholder((OpenXmlElement?) shape);
        }

        /// <summary>
        ///     判断某元素是否是placeholder
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static PlaceholderShape? GetPlaceholder(this Shape? shape)
        {
            return GetPlaceholder((OpenXmlElement?) shape);
        }

        /// <summary>
        ///     获取元素的<see cref="Transform2D" />信息
        /// </summary>
        /// <returns></returns>
        public static Transform2D? GetTransform2D(this Shape? shape, SlideContext context)
        {
            return GetTransform2DFromPlaceholder(shape, context);
        }

        /// <summary>
        ///     判断获取元素的<see cref="PresetGeometry" />
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static PresetGeometry? GetPresetGeometry(this Shape shape)
        {
            var currentValue = shape?.ShapeProperties?.GetFirstChild<PresetGeometry>();
            if (currentValue != null) return currentValue;

            if (shape.IsPlaceholder())
            {
                var (layoutPlaceholderShape, masterPlaceholderShape) = shape!.GetPlaceholderShapes();
                var layoutValue = layoutPlaceholderShape?.ShapeProperties?.GetFirstChild<PresetGeometry>();
                if (layoutValue != null) return layoutValue;

                var masterValue = masterPlaceholderShape?.ShapeProperties?.GetFirstChild<PresetGeometry>();
                if (masterValue != null) return masterValue;
            }

            return null;
        }

        /// <summary>
        ///     判断获取元素的<see cref="BodyProperties" />
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static BodyProperties? GetBodyProperties(this Shape? shape)
        {
            var currentValue = shape?.TextBody?.BodyProperties;
            if (currentValue != null) return currentValue;

            if (shape.IsPlaceholder())
            {
                var (layoutPlaceholderShape, masterPlaceholderShape) = shape!.GetPlaceholderShapes();
                var layoutValue = layoutPlaceholderShape?.TextBody?.BodyProperties;
                if (layoutValue != null) return layoutValue;

                var masterValue = masterPlaceholderShape?.TextBody?.BodyProperties;
                if (masterValue != null) return masterValue;
            }

            return null;
        }

        /// <summary>
        ///     判断获取元素的<see cref="ListStyle" />
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static ListStyle? GetListStyle(this Shape shape)
        {
            var currentValue = shape?.TextBody?.ListStyle;
            if (currentValue != null) return currentValue;

            if (shape.IsPlaceholder())
            {
                var (layoutPlaceholderShape, masterPlaceholderShape) = shape!.GetPlaceholderShapes();
                var layoutValue = layoutPlaceholderShape?.TextBody?.ListStyle;
                if (layoutValue != null) return layoutValue;

                var masterValue = masterPlaceholderShape?.TextBody?.ListStyle;
                if (masterValue != null) return masterValue;
            }

            return null;
        }


        /// <summary>
        ///     判断获取元素的第一个后代元素<see cref="OpenXmlElement" />
        ///     <remarks>
        ///         使用该方法时需要注意指定类型在<see cref="Shape" />子树中无重复
        ///     </remarks>
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static T? GetFirstDescendant<T>(Shape shape) where T : OpenXmlElement
        {
            var currentValue = shape?.Descendants<T>().FirstOrDefault();
            if (currentValue != null) return currentValue;

            if (shape.IsPlaceholder())
            {
                var (layoutPlaceholderShape, masterPlaceholderShape) = shape!.GetPlaceholderShapes();
                var layoutValue = layoutPlaceholderShape?.Descendants<T>().FirstOrDefault();
                if (layoutValue != null) return layoutValue;

                var masterValue = masterPlaceholderShape?.Descendants<T>().FirstOrDefault();
                if (masterValue != null) return masterValue;
            }

            return null;
        }

        /// <summary>
        ///     获取形状的占位符，通过获取 Layout 的和 Master 的
        /// </summary>
        /// 获取算法是通过判断 Type 和 Index 的方法
        /// <param name="shape"></param>
        /// <returns></returns>
        private static (Shape? layoutPlaceholderShape, Shape? masterPlaceholderShape) GetPlaceholderShapes(
            this Shape shape)
        {
            if (shape.IsPlaceholder())
            {
                var placeholder = shape.GetPlaceholder();
                var rootElement = shape.Ancestors<OpenXmlPartRootElement>().FirstOrDefault();
                return GetPlaceholderShapes(placeholder, rootElement);
            }

            Shape? layoutPlaceholder = null;
            Shape? masterPlaceholder = null;
            return (layoutPlaceholder, masterPlaceholder);
        }

        /// <summary>
        ///     获取 SlideLayout 和 SlideMaster 占位符元素
        ///     <para></para>
        ///     获取算法是通过 PlaceholderShape 的 Type 和 Index 判断对应元素
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        public static (Shape? layoutPlaceholderShape, Shape? masterPlaceholderShape) GetPlaceholderShapes(
            PlaceholderShape? placeholder, OpenXmlPartRootElement? rootElement)
        {
            OpenXmlElement? layoutPlaceholder = null;
            OpenXmlElement? masterPlaceholder = null;
            if (rootElement is Slide slide)
            {
                var layout = slide.SlidePart?.SlideLayoutPart?.SlideLayout;
                var master = slide.SlidePart?.SlideLayoutPart?.SlideMasterPart?.SlideMaster;
                layoutPlaceholder = GetPlaceholderShape(placeholder, layout?.CommonSlideData?.ShapeTree);
                masterPlaceholder = GetPlaceholderShape(placeholder, master?.CommonSlideData?.ShapeTree);
            }
            else if (rootElement is SlideLayout slideLayout)
            {
                // 对 SlideLayout 只从 SlideMaster 获取继承
                var master = slideLayout.SlideLayoutPart?.SlideMasterPart?.SlideMaster;
                masterPlaceholder = GetPlaceholderShape(placeholder, master?.CommonSlideData?.ShapeTree);
            }

            return (layoutPlaceholder as Shape, masterPlaceholder as Shape);
        }

        /// <summary>
        ///     获取 SlideLayout 和 SlideMaster 占位符元素用于样式继承。和
        ///     <see cref="GetPlaceholderShapes(DocumentFormat.OpenXml.Presentation.PlaceholderShape, OpenXmlPartRootElement)" />
        ///     不同在于只要 PlaceholderShape 的 Type 相等即可获取 masterPlaceholderShape 元素
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        public static (Shape? layoutPlaceholderShape, Shape? masterPlaceholderShape) GetPlaceholderShapesForStyle(
            PlaceholderShape? placeholder, OpenXmlPartRootElement rootElement)
        {
            var (layoutPlaceholderElement, masterPlaceholderElement) = GetPlaceholderElementForStyle(placeholder, rootElement);
            return (layoutPlaceholderElement as Shape, masterPlaceholderElement as Shape);
        }

        private static (OpenXmlElement? layoutPlaceholderElement, OpenXmlElement? masterPlaceholderElement) GetPlaceholderElementForStyle
            (PlaceholderShape? placeholder, OpenXmlPartRootElement rootElement)
        {
            OpenXmlElement? layoutPlaceholder = null;
            OpenXmlElement? masterPlaceholder = null;
            if (rootElement is Slide slide)
            {
                var layout = slide.SlidePart?.SlideLayoutPart?.SlideLayout;
                var master = slide.SlidePart?.SlideLayoutPart?.SlideMasterPart?.SlideMaster;
                layoutPlaceholder = GetPlaceholderShape(placeholder, layout?.CommonSlideData?.ShapeTree);
                masterPlaceholder = GetMasterPlaceholderByPlaceHolderType(placeholder, master);
            }
            else if (rootElement is SlideLayout slideLayout)
            {
                // 对 SlideLayout 只从 SlideMaster 获取继承
                var master = slideLayout.SlideLayoutPart?.SlideMasterPart?.SlideMaster;
                masterPlaceholder = GetMasterPlaceholderByPlaceHolderType(placeholder, master);
            }

            return (layoutPlaceholder, masterPlaceholder);
        }

        /// <summary>
        ///     对 SlideMaster 的获取占位符的规则是假设 PlaceholderShape 存在 Id 的值，那么在 SlideMaster 所有元素尝试找到对应的 Id 的值的元素，如果能找到那么这个元素就是占位符元素。如果不存在
        ///     Id 或找不到对应的元素，那么进行 Type 的查找，如果传入的 PlaceholderType 没有设置值，那么将使用默认的 PlaceholderValues.Body 的值
        /// </summary>
        /// <returns></returns>
        private static OpenXmlElement? GetMasterPlaceholderByPlaceHolderType(PlaceholderShape? placeholder, SlideMaster? master)
        {
            var placeholderType = placeholder?.Type;
            const PlaceholderValues defaultPlaceholderValue = PlaceholderValues.Body;

            var type = placeholderType?.Value ?? defaultPlaceholderValue;
            var id = placeholder?.Index?.Value;

            var elementList = master?.CommonSlideData?.ShapeTree;
            if (elementList == null) return null;

            OpenXmlElement? typeMatchShape = null;

            foreach (var element in elementList)
            {
                var placeholderShape = GetPlaceholder(element);

                if (placeholderShape == null) continue;

                if (id != null)
                    // 优先找到 id 相同的占位符
                    // 如果 id 相同的找不到，那么找 Type 相同的
                    if (placeholderShape.Index?.Value == id.Value)
                        return element;

                if (placeholderShape.Type?.Value == type)
                {
                    // 基本只有一个 Type 相等，如果有多个，那么这个课件不是标准的
                    Debug.Assert(typeMatchShape == null);
                    typeMatchShape = element;
                }
            }

            return typeMatchShape;
        }

        /// <summary>
        ///     找到对应的placeholder
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        private static OpenXmlElement? GetPlaceholderShape(PlaceholderShape? placeholder, ShapeTree? tree)
        {
            if (placeholder is null || tree is null) return null;

            return tree.Elements().FirstOrDefault(element =>
            {
                var elementPlaceholder = GetPlaceholder(element);
                return elementPlaceholder != null && Equals(placeholder, elementPlaceholder);
            });
        }

        /// <summary>
        ///     比较<see cref="PlaceholderShape" />的type和id是否相同
        ///     <para></para>
        ///     如果 1 的 Type 或 Index 是空，那么跳过这个属性的判断
        ///     <para></para>
        ///     如果这个属性不是空，那么一定要求 2 存在这个属性
        /// </summary>
        /// 这个规则通过 文本占位符没有type和id的值，获取第一个占位符作为坐标 和 WPS 对比测试拿到
        /// 测试课件：文本占位符没有type和id的值.pptx
        /// <param name="placeholder1"></param>
        /// <param name="placeholder2"></param>
        /// <returns></returns>
        private static bool Equals(PlaceholderShape? placeholder1, PlaceholderShape? placeholder2)
        {
            // 如果 placeholder1.Type 存在值，要求 2 一定存在值
            if (placeholder1?.Type != null &&
                placeholder1.Type.Value != placeholder2?.Type?.Value)
                return false;

            if (placeholder1?.Index is not null && placeholder1.Index.Value !=
                placeholder2?.Index?.Value)
                return false;

            return true;
        }
    }
}
