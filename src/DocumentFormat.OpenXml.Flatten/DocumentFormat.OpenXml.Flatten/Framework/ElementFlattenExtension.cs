using System;
using System.Diagnostics.CodeAnalysis;

namespace DocumentFormat.OpenXml.Flatten.Framework
{
    /// <summary>
    /// 元素属性拍平的扩展
    /// </summary>
    public static class ElementFlattenExtension
    {
        /// <summary>
        /// 返回值如果不是空的结果
        /// </summary>
        public readonly struct ReturnWhenNotNullResult
        {
            /// <summary>
            /// 返回值如果不是空的结果
            /// </summary>
            /// <param name="result"></param>
            public ReturnWhenNotNullResult(OpenXmlElement? result)
            {
                Result = result;
            }

            /// <summary>
            /// 结果
            /// </summary>
            public OpenXmlElement? Result { get; }
        }

        /// <summary>
        /// 如果此属性不是空，那么返回不空结果
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ReturnWhenNotNullResult ReturnWhenNotNull(OpenXmlElement? element)
        {
            return new ReturnWhenNotNullResult(element);
        }

        /// <summary>
        /// 如果当前结果是空，那么采用当前属性，直到不是空
        /// </summary>
        /// <param name="result"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ReturnWhenNotNullResult ReturnWhenNotNull(this in ReturnWhenNotNullResult result,
            OpenXmlElement? element)
        {
            if (result.Result != null)
            {
                return result;
            }

            return new ReturnWhenNotNullResult(element);
        }

        /// <summary>
        /// 获取或创建对象
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static TElement GetOrCreateElement<TElement>(this OpenXmlCompositeElement element)
            where TElement : OpenXmlElement, new()
        {
            var result = element.GetFirstChild<TElement>();

            if (result == null)
            {
                result = new TElement();
                element.AddChild(result);
            }

            return result;
        }

        /// <summary>
        /// 获取拍平的属性
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="func"></param>
        /// <param name="elementList"></param>
        /// <returns></returns>
        [return: MaybeNull]
        public static TProperty GetFlattenProperty<TElement, TProperty>(
            Func<TElement, (bool success, TProperty result)> func,
            params TElement[] elementList)
        {
            foreach (var element in elementList)
            {
                if (element is null) continue;

                var (success, result) = func(element);
                if (success)
                {
                    return result;
                }
            }

            return default;
        }

        /// <summary>
        /// 获取拍平的属性
        /// </summary>
        [return: MaybeNull]
        public static TProperty GetFlattenProperty<TElement, TProperty>(Func<TElement, TProperty> func,
            params TElement[] elementList)
        {
            return ElementFlattenExtension.GetFlattenProperty<TElement, TProperty>(e =>
            {
                var result = func(e);
                if (result is null)
                {
                    return (false, default)!;
                }

                return (true, result);
            }, elementList);
        }
    }
}
