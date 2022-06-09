using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.Text
{

    /// <summary>
    /// 文本适配器
    /// </summary>
    /// 在 OpenXML 里面文本包含了 DocumentFormat.OpenXml.Presentation.TextBody 和 DocumentFormat.OpenXml.Drawing.TextBody 这两个类没有继承关系
    /// 但是有一些相同的属性，都可以作为文本
    public class TextBodyAdapt
    {
        /// <summary>
        /// 构建文本适配器
        /// </summary>
        /// <param name="textBody"></param>
        public TextBodyAdapt(DocumentFormat.OpenXml.Presentation.TextBody textBody)
        {
            TextBody = textBody;

            BodyProperties = textBody.BodyProperties;
            ListStyle = textBody.ListStyle;
        }

        /// <summary>
        /// 构建文本适配器
        /// </summary>
        /// <param name="textBody"></param>
        public TextBodyAdapt(DocumentFormat.OpenXml.Drawing.TextBody textBody)
        {
            TextBody = textBody;

            BodyProperties = textBody.BodyProperties;
            ListStyle = textBody.ListStyle;
        }

        /// <summary>
        /// 构建文本适配器
        /// </summary>
        /// <param name="textBody"></param>
        public TextBodyAdapt(DocumentFormat.OpenXml.Office.Drawing.TextBody textBody)
        {
            TextBody = textBody;

            BodyProperties = textBody?.BodyProperties;
            ListStyle = textBody?.ListStyle;
        }

        /// <summary>
        /// 枚举子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Descendants<T>() where T : OpenXmlElement => TextBody?.Descendants<T>() ?? Enumerable.Empty<T>();

        /// <summary>
        /// 正文属性
        /// </summary>
        public BodyProperties? BodyProperties { get; }

        /// <summary>
        /// 文本列表样式
        /// </summary>
        public ListStyle? ListStyle { get; }

        private OpenXmlElement? TextBody { get; }

        /// <summary>
        /// 转为文本适配器
        /// </summary>
        /// <param name="textBody"></param>
        public static implicit operator TextBodyAdapt?(DocumentFormat.OpenXml.Presentation.TextBody? textBody)
            => textBody is not null ? new TextBodyAdapt(textBody) : null;

        /// <summary>
        /// 转为文本适配器
        /// </summary>
        /// <param name="textBody"></param>
        public static implicit operator TextBodyAdapt?(DocumentFormat.OpenXml.Drawing.TextBody? textBody)
            => textBody is not null ? new TextBodyAdapt(textBody) : null;

        /// <summary>
        /// 转为文本适配器
        /// </summary>
        /// <param name="textBody"></param>
        public static implicit operator TextBodyAdapt?(DocumentFormat.OpenXml.Office.Drawing.TextBody? textBody)
        {
            return textBody is not null ? new TextBodyAdapt(textBody) : null;
        }
    }
}
