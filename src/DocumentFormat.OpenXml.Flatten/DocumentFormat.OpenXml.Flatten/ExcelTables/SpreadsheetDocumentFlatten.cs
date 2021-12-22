using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts;
using DocumentFormat.OpenXml.Flatten.ExcelTables.Utils;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables
{
    /// <summary>
    /// 对 Excel 文档的处理，可以获取 Excel 内容
    /// </summary>
    public class SpreadsheetDocumentFlatten : IDisposable
    {
        /// <summary>
        /// 创建对 Excel 文档的处理
        /// </summary>
        public SpreadsheetDocumentFlatten(Stream excelStream)
        {
            SpreadsheetDocument = Packaging.SpreadsheetDocument.Open(excelStream, false);
            Context = new ExcelTableConvertContext(SpreadsheetDocument);
        }

        /// <summary>
        /// 创建对 Excel 文档的处理
        /// </summary>
        /// <param name="spreadsheetDocument"></param>
        public SpreadsheetDocumentFlatten(SpreadsheetDocument spreadsheetDocument)
        {
            SpreadsheetDocument = spreadsheetDocument;
            Context = new ExcelTableConvertContext(SpreadsheetDocument);
        }

        private SpreadsheetDocument SpreadsheetDocument { get; }
        private ExcelTableConvertContext Context { get; }

        /// <summary>
        /// 获取作为 Ole 的范围
        /// </summary>
        /// <returns></returns>
        public ExcelRange? GetOleSize()
        {
            return Workbook?.GetOleSize();
        }

        /// <summary>
        /// 获取当前激活的工作表
        /// </summary>
        /// <returns></returns>
        public WorksheetFlatten GetActiveWorksheet()
        {
            var index = GetActiveWorksheetTabIndex();

            // 此方法获取是不对的，因为实际上的顺序是依靠 workbook.xml 的 sheets 列表决定的
            var sheet = Workbook?.Sheets?.Elements<Sheet>().ElementAtOrDefault(index);
            if (sheet is null)
            {
                throw new ArgumentException($"找不到对应的激活工作表");
            }

            var sheetId = sheet.Id?.Value;
            if (sheetId is null)
            {
                throw new ArgumentException($"Sheet Id 是空");
            }

            Debug.Assert(WorkbookPart != null, "因为能找到 sheetId 因此 WorkbookPart 一定存在");
            var worksheetPart = (WorksheetPart) WorkbookPart!.GetPartById(sheetId);

            return new WorksheetFlatten(worksheetPart, Context);
        }

        /// <summary>
        /// 获取当前激活的工作表，也就是 WorkbookView 的 ActiveTab 属性
        /// </summary>
        /// <returns></returns>
        public int GetActiveWorksheetTabIndex()
        {
            var bookViews = Workbook?.BookViews;
            var workbookView = bookViews?.GetFirstChild<WorkbookView>();
            return (int) (workbookView?.ActiveTab?.Value ?? 0);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            SpreadsheetDocument.Dispose();
        }

        private WorkbookPart? WorkbookPart => SpreadsheetDocument.WorkbookPart;
        private Workbook? Workbook => WorkbookPart?.Workbook;

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <returns></returns>
        public WorksheetsFlatten GetWorksheets()
        {
            var worksheetsFlatten = new WorksheetsFlatten(WorkbookPart?.WorksheetParts, Context);
            return worksheetsFlatten;
        }
    }
}
