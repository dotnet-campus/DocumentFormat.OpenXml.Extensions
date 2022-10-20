namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    /// <summary>
    /// 颜色填充
    /// </summary>
    /// 这是因为在 OpenXML 里面，各个颜色填充没有共同的基类，为了转换之间有单位，因此特别定义此类型
    public class BrushFill
    {
        /// <summary>
        /// 创建颜色填充
        /// </summary>
        /// <param name="fill"></param>
        public BrushFill(OpenXmlElement fill)
        {
            _fill = fill;
        }

        /// <summary>
        /// 获取填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetFill<T>() where T : OpenXmlElement
        {
            var value = _fill as T;
            return value;
        }

        /// <summary>
        /// 添加到 OpenXML 元素里面
        /// </summary>
        /// <param name="element"></param>
        public void AddToElement(OpenXmlElement element)
        {
            if (_fill.Clone() is OpenXmlElement openXmlElement)
            {
                element.Append(new[] { openXmlElement });
            }
        }

        private readonly OpenXmlElement _fill;
    }
}
