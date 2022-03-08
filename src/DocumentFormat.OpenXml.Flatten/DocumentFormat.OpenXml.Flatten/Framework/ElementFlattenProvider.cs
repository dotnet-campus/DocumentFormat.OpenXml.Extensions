using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DocumentFormat.OpenXml.Flatten.Framework
{
    /// <summary>
    /// 提供 OpenXML 元素拍平的辅助功能
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    class ElementFlattenProvider<TElement> where TElement : OpenXmlElement
    {
        /// <summary>
        /// 创建 OpenXML 元素拍平的辅助功能类
        /// </summary>
        /// <param name="elementList">要求首个元素为主元素，主元素是待继承的元素</param>
        public ElementFlattenProvider(params TElement?[] elementList)
        {
            if (elementList[0] is null)
            {
                throw new ArgumentException($"The main element should not be null. elementList[0] should not be null");
            }

            ElementList = elementList;
        }

        /// <summary>
        /// 获取主元素，主元素是待继承的元素，主元素是传入列表的首个元素
        /// </summary>
        public TElement Main => ElementList[0]!;

        /// <summary>
        /// 获取继承后的主元素的属性，此属性可以继续进行多级继承
        /// </summary>
        public ElementFlattenProvider<TElement> FlattenMainElementProperty<TProperty>()
            where TProperty : OpenXmlElement
        {
            return FlattenMainElementProperty(e => e.GetFirstChild<TProperty>());
        }

        /// <summary>
        /// 获取继承后的主元素的属性，此属性可以继续进行多级继承
        /// </summary>
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

        /// <summary>
        /// 获取继承后的主元素的属性，此属性可以继续进行多级继承
        /// </summary>
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

        /// <summary>
        /// 获取基础的继承属性值
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [return: MaybeNull]
        public TProperty GetFlattenProperty<TProperty>(Func<TElement, (bool success, TProperty result)> func)
        {
            return ElementFlattenExtension.GetFlattenProperty<TElement, TProperty>(func, ElementList!);
        }

        /// <summary>
        /// 获取继承后的主元素的属性，此属性可以继续进行多级继承
        /// </summary>
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

        /// <summary>
        /// 获取继承后的主元素的属性，此属性可以继续进行多级继承
        /// </summary>
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

        /// <summary>
        /// 获取基础的继承属性值
        /// </summary>
        [return: MaybeNull]
        public TProperty GetFlattenProperty<TProperty>(Func<TElement, TProperty> func)
        {
            return ElementFlattenExtension.GetFlattenProperty<TElement, TProperty>(func, ElementList!);
        }

        /// <summary>
        /// 继承的元素列表
        /// </summary>
        protected TElement?[] ElementList { get; }
    }
}
