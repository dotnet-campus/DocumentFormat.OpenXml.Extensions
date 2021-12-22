using System;

using DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables
{
    /// <summary>
    /// 单元格样式
    /// </summary>
    public class CellFlattenStyle : IReadonlyCellFlattenStyle
    {
        /// <summary>
        /// 创建单元格样式
        /// </summary>
        public CellFlattenStyle()
        {
        }

        /// <summary>
        /// 创建单元格样式
        /// </summary>
        /// <param name="reservedStyle"></param>
        public CellFlattenStyle(IReadonlyCellFlattenStyle? reservedStyle)
        {
            ReservedStyle = reservedStyle;
        }

        /// <inheritdoc />
        public PixelSize Size
        {
            set => _size = value;
            get => _size ?? ReservedStyle?.Size ?? DefaultCellSize;
        }

        private PixelSize? _size;

        /// <summary>
        /// 默认 Excel 行为是 <para/>
        /// - 列宽：72 pixel （8.38 Excel单位）<para/>
        /// - 行高：14.25 磅
        /// </summary>
        /// 列宽单位和像素计算方法：像素 =（列宽+0.62）* 8
        public static PixelSize DefaultCellSize =>
            new PixelSize(new ExcelCellColumnWidth(8.38).ToPixel(), new Pixel(19));


        /// <inheritdoc />
        public Pound FontSize
        {
            set => _fontSize = value;
            get => _fontSize ?? ReservedStyle?.FontSize ?? DefaultFontSize;
        }

        private Pound? _fontSize;

        /// <summary>
        /// 默认的字体是 11 磅的
        /// </summary>
        public static Pound DefaultFontSize => new Pound(11);


        /// <inheritdoc />
        public string FontName
        {
            set => _fontName = value;
            get => _fontName ?? ReservedStyle?.FontName ?? DefaultFontName;
        }

        private string? _fontName;

        /// <summary>
        /// 默认单元格字体名
        /// </summary>
        public const string DefaultFontName = "Arial";

        /// <inheritdoc />
        public IReadonlyCellFlattenStyle BuildNewProperty(Action<CellFlattenStyle> action)
        {
            // 这是参照文本库的设计，我也不知道这个设计是否在此是合理的
            var cellFlattenStyle = new CellFlattenStyle(this);
            action(cellFlattenStyle);
            return cellFlattenStyle;
        }

        private IReadonlyCellFlattenStyle? ReservedStyle { get; }
    }

    /// <summary>
    /// 单元格样式
    /// </summary>
    public interface IReadonlyCellFlattenStyle
    {
        /// <summary>
        /// 单元格的尺寸
        /// </summary>
        PixelSize Size { get; }

        /// <summary>
        /// 单元格默认的字号
        /// </summary>
        Pound FontSize { get; }

        /// <summary>
        /// 字体名
        /// </summary>
        string FontName { get; }

        /// <summary>
        /// 基于当前的构建出新的单元格样式
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IReadonlyCellFlattenStyle BuildNewProperty(Action<CellFlattenStyle> action);
    }
}
