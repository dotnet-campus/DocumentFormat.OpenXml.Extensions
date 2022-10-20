using DocumentFormat.OpenXml.Drawing;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;
using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement
{
    /// <summary>
    /// 提供获取元素Extents真实的EmuSize的扩展类
    /// </summary>
    public static class ExtentsToElementEmuSizeExtensions
    {
        /// <summary>
        /// 获取元素大小
        /// </summary>
        public static ElementEmuSize GetElementEmuSize(this DocumentFormat.OpenXml.Presentation.Shape shape)
        {
            return GetElementEmuSize((ShapeAdapt) shape);
        }

        /// <summary>
        /// 获取元素大小
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static ElementEmuSize GetElementEmuSize(this ShapeAdapt shape)
        {
            var shapePropertiesAdapt = shape.ShapePropertiesAdapt;
            var extents = shapePropertiesAdapt?.GetTransform2D()?.Extents;
            if (extents == null)
            {
                return default;
            }

            if (shape.Shape?.Parent is DocumentFormat.OpenXml.Presentation.GroupShape groupShape)
            {
                return GetGroupExtentsEmuSize(groupShape, extents);
            }

            return GetElementEmuSize(extents);
        }

        /// <summary>
        /// 获取元素的大小
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ElementEmuSize GetElementEmuSize(this OpenXmlElement? element)
        {
            if (element is null)
            {
                return default;
            }

            var shapeProperties = element.GetFirstChild<ShapeProperties>();
            var extents = shapeProperties?.GetFirstChild<Transform2D>()?.Extents;
            if (extents == null)
            {
                return default;
            }

            if (element?.Parent is DocumentFormat.OpenXml.Presentation.GroupShape groupShape)
            {
                return GetGroupExtentsEmuSize(groupShape, extents);
            }

            return GetElementEmuSize(extents);
        }

        /// <summary>
        /// 获取该元素在组合形状中计算后的真实的EmuSize
        /// </summary>
        /// <param name="groupShape">形状组合</param>
        /// <param name="extents">该元素的原本Extents</param>
        /// <returns></returns>
        public static ElementEmuSize GetGroupExtentsEmuSize(DocumentFormat.OpenXml.Presentation.GroupShape? groupShape, Extents? extents)
        {
            if (extents == null) return new ElementEmuSize(new Emu(0), new Emu(0));
            if (groupShape == null) return GetElementEmuSize(extents);
            var elementEmuSize = GetElementEmuSize(extents);
            Extents? groupExtents = null;
            ChildExtents? childExtents = null;
            var cxFactor = 1d;
            var cyFactor = 1d;
            //单层组合的情况
            if (groupShape.Parent is not DocumentFormat.OpenXml.Presentation.GroupShape)
            {
                groupExtents = groupShape.GroupShapeProperties?.TransformGroup?.Extents;
                childExtents = groupShape.GroupShapeProperties?.TransformGroup?.ChildExtents;
                if (groupExtents != null && childExtents != null)
                {
                    (cxFactor, cyFactor) = GetGroupShapeExtentFactor(groupExtents, childExtents);
                    elementEmuSize = GetGroupExtentsEmuSize(cxFactor, cyFactor, extents);
                }
                return elementEmuSize;
            }



            //嵌套组合的情况
            //先算出第一层组合嵌套组合的Extents的Cx和Cy的变换因子
            if (groupShape.Parent is DocumentFormat.OpenXml.Presentation.GroupShape parentGroupShape)
            {
                var parentGroupExtents = parentGroupShape.GroupShapeProperties?.TransformGroup?.Extents;
                var parentChildExtents = parentGroupShape.GroupShapeProperties?.TransformGroup?.ChildExtents;
                groupExtents = groupShape.GroupShapeProperties?.TransformGroup?.Extents;
                childExtents = groupShape.GroupShapeProperties?.TransformGroup?.ChildExtents;
                if (groupExtents != null && childExtents != null)
                {
                    (cxFactor, cyFactor) = GetGroupShapeExtentFactor(parentGroupExtents, parentChildExtents,
                        groupExtents, childExtents, cxFactor, cyFactor);
                }

                var tempGroupShape = parentGroupShape;
                //假如groupShape的Parent的Parent再是组合，则只需要用变化因子分别乘与其自身的变化因子即可
                while (tempGroupShape.Parent is DocumentFormat.OpenXml.Presentation.GroupShape tempParentGroupShape)
                {
                    parentGroupExtents = tempParentGroupShape.GroupShapeProperties?.TransformGroup?.Extents;
                    parentChildExtents = tempParentGroupShape.GroupShapeProperties?.TransformGroup?.ChildExtents;
                    var (tmpCxFactor, tmpCyFactor) = GetGroupShapeExtentFactor(parentGroupExtents, parentChildExtents);
                    cxFactor = tmpCxFactor * cxFactor;
                    cyFactor = tmpCyFactor * cyFactor;
                    tempGroupShape = tempParentGroupShape;
                }
            }

            elementEmuSize = GetGroupExtentsEmuSize(cxFactor, cyFactor, extents);
            return elementEmuSize;
        }

        /// <summary>
        /// 获取组合形状的宽度告诉缩放比例
        /// </summary>
        /// <param name="groupExtents"></param>
        /// <param name="childExtents"></param>
        /// <returns></returns>
        public static (double cxFactor, double cyFactor) GetGroupShapeExtentFactor(Extents? groupExtents, ChildExtents? childExtents)
        {
            var cxFactor = 1d;
            var cyFactor = 1d;
            //当元素的父级是组形状时候，元素的真实Extents: 组形状的Extents / ChildExtents * 元素的Extents
            if (groupExtents is not null && childExtents is not null)
            {
                if (groupExtents.Cx is not null && childExtents.Cx is not null && groupExtents.Cx.Value != 0 && childExtents.Cx.Value != 0)
                {
                    cxFactor = groupExtents.Cx.Value / (double) childExtents.Cx.Value;
                }
                if (groupExtents.Cy is not null && childExtents.Cy is not null && groupExtents.Cy.Value != 0 && childExtents.Cy.Value != 0)
                {
                    cyFactor = groupExtents.Cy.Value / (double) childExtents.Cy.Value;
                }
            }
            return (cxFactor, cyFactor);
        }

        /// <summary>
        /// 获取嵌套的组合的Extents的Cx和Cy的变换因子
        /// </summary>
        /// <param name="parentGroupExtents">组合元素的父组合Extents</param>
        /// <param name="parentChildExtents">组合元素的父组合ChildExtents</param>
        /// <param name="groupExtents">组合元素的Extents</param>
        /// <param name="childExtents">组合元素的ChildExtents</param>
        /// <param name="cxFactor">Cx变换因子</param>
        /// <param name="cyFactor">Cy的变换因子</param>
        /// <returns></returns>
        public static (double cxFactor, double cyFactor) GetGroupShapeExtentFactor(Extents? parentGroupExtents, ChildExtents? parentChildExtents, Extents? groupExtents, ChildExtents? childExtents, double cxFactor, double cyFactor)
        {
            var tempCxFactor = 1d;
            var tempCyFactor = 1d;
            //当元素的父级是组形状时候，元素的真实Extents: 组形状的Extents / ChildExtents * 元素的Extents
            if (parentGroupExtents != null && parentChildExtents != null && groupExtents != null && childExtents != null)
            {
                if (parentGroupExtents.Cx is not null && parentChildExtents.Cx is not null && groupExtents.Cx is not null && childExtents.Cx is not null && groupExtents.Cx.Value != 0 && childExtents.Cx.Value != 0)
                {
                    tempCxFactor = (parentGroupExtents.Cx.Value / (double) parentChildExtents.Cx) * (groupExtents.Cx.Value / (double) childExtents.Cx.Value);
                }
                if (parentGroupExtents.Cy is not null && parentChildExtents.Cy is not null && groupExtents.Cy is not null && childExtents.Cy is not null && groupExtents.Cy.Value != 0 && childExtents.Cy.Value != 0)
                {
                    tempCyFactor = (parentGroupExtents.Cy.Value / (double) parentChildExtents.Cy) * (groupExtents.Cy.Value / (double) childExtents.Cy.Value);
                }
            }
            return (cxFactor * tempCxFactor, cyFactor * tempCyFactor);
        }


        /// <summary>
        /// 获取在组合内该形状的真实Emu Size
        /// </summary>
        /// <param name="cxFactor">组合的Extents的Cx变化因子</param>
        /// <param name="cyFactor">组合的Extents的Cy变化因子</param>
        /// <param name="extents">组合子形状Extents</param>
        /// <returns></returns>
        private static ElementEmuSize GetGroupExtentsEmuSize(double cxFactor, double cyFactor, Extents extents)
        {
            //当元素的父级是组形状时候，元素的真实Extents: 组形状的Extents / ChildExtents * 元素的Extents
            double cx = extents.Cx?.Value ?? 0;
            if (extents.Cx is not null)
            {
                cx = cxFactor * extents.Cx.Value;
            }
            double cy = extents.Cy?.Value ?? 0;
            if (extents.Cy is not null)
            {
                cy = cyFactor * extents.Cy.Value;
            }
            return new ElementEmuSize(new Emu(cx), new Emu(cy));
        }

        /// <summary>
        /// 获取元素大小
        /// </summary>
        /// <param name="extents"></param>
        /// <returns></returns>
        public static ElementEmuSize GetElementEmuSize(Extents extents)
        {
            if (!long.TryParse(extents.Cx?.InnerText, out var width))
            {
                width = 0;
            }
            if (!long.TryParse(extents.Cy?.InnerText, out var height))
            {
                height = 0;
            }

            return new ElementEmuSize(new Emu(width), new Emu(height));
        }
    }
}
