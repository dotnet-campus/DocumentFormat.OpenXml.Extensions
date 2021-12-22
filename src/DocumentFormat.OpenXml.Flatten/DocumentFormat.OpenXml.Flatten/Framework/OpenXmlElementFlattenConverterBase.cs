using System;
using System.Diagnostics.CodeAnalysis;

using DocumentFormat.OpenXml.Flatten.Framework.Context;

namespace DocumentFormat.OpenXml.Flatten.Framework
{
    abstract class OpenXmlElementFlattenConverterBase<T> : IOpenXmlElementFlattenConverter
        where T : OpenXmlElement
    {
        public bool IsMatch(OpenXmlElement element) => element is T;

        public OpenXmlElement Convert(OpenXmlElement element, ElementContext context)
        {
            return Convert((T) element, context);
        }

        protected abstract T Convert(T element, ElementContext context);
    }

    interface IOpenXmlElementFlattenConverter
    {
        bool IsMatch(OpenXmlElement element);
        OpenXmlElement Convert(OpenXmlElement element, ElementContext context);
    }
}
