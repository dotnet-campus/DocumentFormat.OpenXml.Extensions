using System.Text.RegularExpressions;

using DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables.Utils
{
    /// <summary>
    /// 获取 Excel 文档信息
    /// </summary>
    public static class ExcelInfoHelper
    {
        /// <summary>
        /// 获取作为 Ole 的范围
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static ExcelRange? GetOleSize(this Workbook workbook)
        {
            var oleSize = workbook.GetFirstChild<OleSize>();
            var oleSizeValue = oleSize?.Reference?.Value;
            if (oleSizeValue is null)
            {
                return null;
            }

            return StringToExcelRange(oleSizeValue);
        }

        internal static ExcelRange? StringToExcelRange(string? excelRange)
        {
            if (ExcelRange.TryParse(excelRange, out var range))
            {
                return range;
            }

            return null;
        }
    }
}
