using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DocumentFormat.OpenXml.Flatten.Framework
{
    class ElementFlattenProvider<TElement> where TElement : OpenXmlElement
    {
        public ElementFlattenProvider(params TElement?[] elementList)
        {
            if (elementList[0] is null)
            {
                throw new ArgumentException($"The main element should not be null. elementList[0] should not be null");
            }

            ElementList = elementList;
        }

        public TElement Main => ElementList[0]!;

        public ElementFlattenProvider<TElement> FlattenMainElementProperty<TProperty>()
            where TProperty : OpenXmlElement
        {
            return FlattenMainElementProperty(e => e.GetFirstChild<TProperty>());
        }

        public ElementFlattenProvider<TElement> FlattenMainElementProperty<TProperty>(Func<TElement, TProperty?> func) where TProperty : OpenXmlElement
        {
            return FlattenMainElementProperty(e =>
            {
                var openXmlElement = func(e);
                if (openXmlElement != null)
                {
                    return (true, openXmlElement);
                }

                return (false, default!);
            });
        }

        public ElementFlattenProvider<TElement> FlattenMainElementProperty<TProperty>(Func<TElement, (bool success, TProperty result)> func) where TProperty : OpenXmlElement
        {
            var mainResult = func(Main);
            if (mainResult.success)
            {
                return this;
            }

            for (var i = 1; i < ElementList.Length; i++)
            {
                var element = ElementList[i];
                if (element is null)
                {
                    continue;
                }

                var (success, result) = func(element);
                if (success)
                {
                    result = (TProperty) result.Clone();

                    Main.AppendChild(result);

                    return this;
                }
            }

            return this;
        }

        [return: MaybeNull]
        public TProperty GetFlattenProperty<TProperty>(Func<TElement, (bool success, TProperty result)> func)
        {
            return ElementFlattenExtension.GetFlattenProperty<TElement, TProperty>(func, ElementList!);
        }

        public ElementFlattenProvider<TProperty>? GetSubFlattenProperty<TProperty>(
            Func<TElement, TProperty> func) where TProperty : OpenXmlElement
            => GetSubFlattenProperty(e =>
            {
                var openXmlElement = func(e);
                if (openXmlElement == null)
                {
                    return (false, default!);
                }
                else
                {
                    return (true, openXmlElement);
                }
            });

        public ElementFlattenProvider<TProperty>? GetSubFlattenProperty<TProperty>(
            Func<TElement, (bool success, TProperty result)> func) where TProperty : OpenXmlElement
        {
            var elements = GetPropertyList().ToArray();
            if (elements.Length == 0)
            {
                return null;
            }

            return new ElementFlattenProvider<TProperty>(elements);

            IEnumerable<TProperty> GetPropertyList()
            {
                foreach (var element in ElementList)
                {
                    if (element is null) continue;

                    var (success, result) = func(element);
                    if (success)
                    {
                        yield return result;
                    }
                }
            }
        }

        [return: MaybeNull]
        public TProperty GetFlattenProperty<TProperty>(Func<TElement, TProperty> func)
        {
            return ElementFlattenExtension.GetFlattenProperty<TElement, TProperty>(func, ElementList!);
        }

        protected TElement?[] ElementList { get; }
    }
}
