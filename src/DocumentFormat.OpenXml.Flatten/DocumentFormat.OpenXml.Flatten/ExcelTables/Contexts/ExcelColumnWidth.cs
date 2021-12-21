using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts
{
    /// <summary>
    /// 表示单元格列宽单位
    /// </summary>
    /// 值和像素换算公式是 像素 =（列宽+0.62）* 8
    /// 此计算方法暂时没有找到文档
    public readonly struct ExcelCellColumnWidth
    {
        /// <summary>
        /// 表示单元格列宽单位
        /// </summary>
        /// <param name="value"></param>
        public ExcelCellColumnWidth(double value)
        {
            Value = value;
        }

        private double Value { get; }

        /// <summary>
        /// 转换为使用像素单位
        /// </summary>
        /// <returns></returns>
        public Pixel ToPixel() => new Pixel((Value + 0.62) * 8);

        /// <inheritdoc />
        public override string ToString() => ToPixel().ToString();
    }
}
