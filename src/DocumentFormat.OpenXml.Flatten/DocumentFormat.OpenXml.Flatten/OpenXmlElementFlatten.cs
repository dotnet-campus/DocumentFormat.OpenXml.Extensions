using System.Collections.Generic;

using DocumentFormat.OpenXml.Flatten.ElementConverters;
using DocumentFormat.OpenXml.Flatten.Framework;
using DocumentFormat.OpenXml.Flatten.Framework.Context;

namespace DocumentFormat.OpenXml.Flatten
{
    /// <summary>
    /// 对 OpenXml 元素的属性拍平辅助类，属性拍平指的是从层层样式里面获取到属性的最终值
    /// </summary>
    public class OpenXmlElementFlatten
    {
        IReadOnlyList<IOpenXmlElementFlattenConverter> FlattenConverterList { get; } =
           new[] { new ShapeFlattenConverter() };

        /// <summary>
        /// 获取拍平属性后的 OpenXml 对象
        /// </summary>
        /// <param name="element"></param>
        /// <param name="context"></param>
        /// <param name="shouldCloneOriginElement">是否需要复制新的 OpenXml 元素，不在原有的元素基础上修改属性值。默认值是在原有元素上修改值</param>
        /// <returns></returns>
        public OpenXmlElement GetFlattenElement(OpenXmlElement element, SlideContext context, bool shouldCloneOriginElement = false)
        {
            foreach (var shapeFlattenConverter in FlattenConverterList)
            {
                if (shapeFlattenConverter.IsMatch(element))
                {
                    var elementContext = new ElementContext(element, context);

                    var convertingElement = element;
                    if (shouldCloneOriginElement)
                    {
                        convertingElement = (OpenXmlElement) convertingElement.Clone();
                    }

                    return shapeFlattenConverter.Convert(convertingElement, elementContext);
                }
            }

            return element;
        }

    }
}
