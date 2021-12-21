using DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables
{
    /// <summary>
    /// 单元格拍平信息，包括单元格展示最终信息
    /// </summary>
    public class CellFlattenInfo
    {
        /// <summary>
        /// 创建单元格拍平信息
        /// </summary>
        public CellFlattenInfo(string displayText, IReadonlyCellFlattenStyle style, ExcelPosition position,
            WorksheetFlatten worksheetFlatten,
            Row originRow, Cell originCell)
        {
            Position = position;
            WorksheetFlatten = worksheetFlatten;
            OriginRow = originRow;
            OriginCell = originCell;
            DisplayText = displayText;
            Style = style;
            SetSpanNumebrAndMergeCell();
        }

        /// <summary>
        /// 跨行数目。
        /// </summary>
        public int RowSpan { get; private set; }

        /// <summary>
        /// 跨列数目。
        /// </summary>
        public int ColumnSpan { get; private set; }

        /// <summary>
        /// 垂直合并。
        /// </summary>
        public bool VerticalMerged { get; private set; }

        /// <summary>
        /// 水平合并。
        /// </summary>
        public bool HorizontalMerged { get; private set; }

        /// <summary>
        /// 单元格显示的内容
        /// </summary>
        public string DisplayText { get; }

        /// <summary>
        /// 单元格样式
        /// </summary>
        public IReadonlyCellFlattenStyle Style { get; }

        /// <summary>
        /// 所在的工作表
        /// </summary>
        public WorksheetFlatten WorksheetFlatten { get; }

        /// <summary>
        /// 所在的坐标
        /// </summary>
        public ExcelPosition Position { get; }

        /// <summary>
        /// 所在行
        /// </summary>
        public Row OriginRow { get; }

        /// <summary>
        /// 所在单元格
        /// </summary>
        public DocumentFormat.OpenXml.Spreadsheet.Cell OriginCell { get; }

        /// <summary>
        /// 设置单元格跨行跨列数目以及合并单元格信息。
        /// </summary>
        void SetSpanNumebrAndMergeCell()
        {
            var mergeRangeList = WorksheetFlatten.MergeRangeList;

            VerticalMerged = false;
            HorizontalMerged = false;
            RowSpan = 1;
            ColumnSpan = 1;

            if (mergeRangeList is null || mergeRangeList.Count == 0)
            {
                return;
            }

            var rowIndex = Position.Row;
            var columnIndex = Position.Column;

            foreach (var excelRange in mergeRangeList)
            {
                if (excelRange.Contain(Position))
                {
                    RowSpan = excelRange.EndPosition.Row - rowIndex + 1;
                    ColumnSpan = excelRange.EndPosition.Column - columnIndex + 1;

                    // 如果单元格的行索引大于合并范围的起始行，小于等于合并范围的结束行，则该单元格被垂直合并。
                    if (rowIndex > excelRange.StartPosition.Row && rowIndex <= excelRange.EndPosition.Row)
                    {
                        VerticalMerged = true;
                    }

                    // 如果单元格的列索引大于合并范围的起始列，小于等于合并范围的结束列，则该单元格被水平合并。
                    if (columnIndex > excelRange.StartPosition.Column && columnIndex <= excelRange.EndPosition.Column)
                    {
                        HorizontalMerged = true;
                    }

                    return;
                }
            }
        }
    }
}
