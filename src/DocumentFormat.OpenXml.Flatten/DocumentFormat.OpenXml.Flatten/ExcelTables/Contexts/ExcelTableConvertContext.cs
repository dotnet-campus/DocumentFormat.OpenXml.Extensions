using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts
{
    /// <summary>
    /// 转换的上下文
    /// </summary>
    public class ExcelTableConvertContext
    {
        /// <summary>
        /// 创建转换的上下文
        /// </summary>
        /// <param name="spreadsheetDocument"></param>
        public ExcelTableConvertContext(SpreadsheetDocument spreadsheetDocument)
        {
            SpreadsheetDocument = spreadsheetDocument;
        }

        /// <summary>
        /// 文档
        /// </summary>
        public SpreadsheetDocument SpreadsheetDocument { get; }

        internal WorkbookPart? WorkbookPart => SpreadsheetDocument.WorkbookPart;
        internal Workbook? Workbook => WorkbookPart?.Workbook;
        internal Stylesheet? Stylesheet => WorkbookPart?.WorkbookStylesPart?.Stylesheet;
    }
}
