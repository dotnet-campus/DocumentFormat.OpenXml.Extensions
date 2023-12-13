using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Flatten.Utils;
using DocumentFormat.OpenXml.Presentation;

using dotnetCampus.OpenXmlUnitConverter;

using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement
{
    /// <summary>
    /// 通用元素的变换信息扩展
    /// </summary>
    public static class CommonElementTransformDataExtensions
    {
        /// <summary>
        /// 获取或创建变换信息
        /// </summary>
        /// <param name="element"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ITransformData GetOrCreateTransformData(this OpenXmlElement element, SlideContext context)
        {
            var transformData = element.GetFirstChild<TransformData>();
            if (transformData != null)
            {
                return transformData;
            }

            transformData = (TransformData) element.CreateTransformData(context);
            element.AppendChild(transformData);
            return transformData;
        }

        /// <summary>
        /// 创建变换信息
        /// </summary>
        /// <param name="element"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static ITransformData CreateTransformData(this OpenXmlElement element, SlideContext context)
        {
            var transformData = new TransformData();

            Transform2D? transform2D = null;
            var shapeTransform = element.GetFirstChild<ShapeProperties>()?.GetFirstChild<Transform2D>();
            if (shapeTransform is not null)
            {
                transform2D = shapeTransform;
            }
            if (transform2D != null)
            {
                FillOffset(transform2D.Offset, transformData);
                //如果是组合形状得子元素，需要走组合形状算法计算元素大小
                if (element.Parent is DocumentFormat.OpenXml.Presentation.GroupShape groupShape)
                {
                    var emuSize = ExtentsToElementEmuSizeExtensions.GetGroupExtentsEmuSize(groupShape, transform2D.Extents);
                    transformData.Width = emuSize.Width;
                    transformData.Height = emuSize.Height;
                }
                else
                {
                    FillExtents(transform2D.Extents, transformData);
                }
                FillRotation(transform2D.Rotation, transformData);
                FillFlip(transform2D.HorizontalFlip, transform2D.VerticalFlip, transformData);
                return transformData;
            }

            // grpsppr 是 ShapeProperties
            var groupShapeProperties = element.GetFirstChild<GroupShapeProperties>();
            if (groupShapeProperties?.TransformGroup != null)
            {
                // xfrm 是 Transform2D
                var transformGroup = groupShapeProperties.TransformGroup;
                FillOffset(transformGroup.Offset, transformData);

                FillExtents(transformGroup.Extents, transformData);

                FillRotation(transformGroup.Rotation, transformData);

                FillFlip(transformGroup.HorizontalFlip, transformGroup.VerticalFlip, transformData);

                return transformData;
            }

            //对于表格直接获取xfrm
            var transform = element.GetFirstChild<Transform>();
            if (transform != null)
            {
                FillOffset(transform.Offset, transformData);
                FillExtents(transform.Extents, transformData);
                FillRotation(transform.Rotation, transformData);
                FillFlip(transform.HorizontalFlip, transform.VerticalFlip, transformData);
                return transformData;
            }

            //这是SmartArt获取Transform2D逻辑
            var officeShapeTransform = element.GetFirstChild<DocumentFormat.OpenXml.Office.Drawing.ShapeProperties>()?.GetFirstChild<Transform2D>();
            if (officeShapeTransform is not null)
            {
                FillOffset(officeShapeTransform.Offset, transformData);
                FillExtents(officeShapeTransform.Extents, transformData);
                FillRotation(officeShapeTransform.Rotation, transformData);
                FillFlip(officeShapeTransform.HorizontalFlip, officeShapeTransform.VerticalFlip, transformData);
                return transformData;
            }

            // 需要考虑从占位符获取坐标等
            // 对于形状、图片，需要尝试获取占位符坐标
            var transform2DFromPlaceholder = PlaceholderHelper.GetTransform2DFromPlaceholder(element, context);
            if (transform2DFromPlaceholder != null)
            {
                FillOffset(transform2DFromPlaceholder.Offset, transformData);
                FillExtents(transform2DFromPlaceholder.Extents, transformData);
                FillRotation(transform2DFromPlaceholder.Rotation, transformData);
                FillFlip(transform2DFromPlaceholder.HorizontalFlip, transform2DFromPlaceholder.VerticalFlip, transformData);
                return transformData;
            }

            var alternateContentTransform2D = element.GetFirstChild<ContentPart>()?.GetFirstChild<DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D>();
            if (alternateContentTransform2D is not null)
            {
                FillOffset(alternateContentTransform2D.Offset, transformData);
                FillExtents(alternateContentTransform2D.Extents, transformData);
                FillRotation(alternateContentTransform2D.Rotation, transformData);
                FillFlip(alternateContentTransform2D.HorizontalFlip, alternateContentTransform2D.VerticalFlip, transformData);
                return transformData;
            }


            return transformData;

            void FillOffset(Offset? offset, TransformData transformData2)
            {
                if (!string.IsNullOrEmpty(offset?.X?.InnerText))
                {
                    transformData2.OffsetX = new Emu(offset!.X!);
                }

                if (!string.IsNullOrEmpty(offset?.Y?.InnerText))
                {
                    transformData2.OffsetY = new Emu(offset!.Y!);
                }
            }

            void FillExtents(Extents? extents, TransformData transformData3)
            {
                if (!string.IsNullOrEmpty(extents?.Cx?.InnerText))
                {
                    transformData3.Width = new Emu(extents!.Cx!);
                }

                if (!string.IsNullOrEmpty(extents?.Cy?.InnerText))
                {
                    transformData3.Height = new Emu(extents!.Cy!);
                }
            }

            void FillRotation(Int32Value? rotation, TransformData transformData4)
            {
                if (rotation is not null)
                {
                    transformData4.Rotation = new Degree(rotation);
                }
            }

            // 关于翻转请看 [dotnet OpenXML SDK 形状的翻转与旋转](https://blog.lindexi.com/post/dotnet-OpenXML-SDK-%E5%BD%A2%E7%8A%B6%E7%9A%84%E7%BF%BB%E8%BD%AC%E4%B8%8E%E6%97%8B%E8%BD%AC.html )
            void FillFlip(BooleanValue? horizontalFlip, BooleanValue? verticalFlip, TransformData transformData5)
            {
                if (horizontalFlip is not null)
                {
                    transformData5.HorizontalFlip = horizontalFlip.Value;
                }

                if (verticalFlip is not null)
                {
                    transformData5.VerticalFlip = verticalFlip.Value;
                }
            }
        }
    }
}
