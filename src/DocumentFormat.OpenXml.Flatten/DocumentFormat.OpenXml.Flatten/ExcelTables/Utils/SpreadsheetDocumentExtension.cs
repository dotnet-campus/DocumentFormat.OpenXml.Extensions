using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables.Utils
{
    static class SpreadsheetDocumentExtension
    {
        public static SpreadsheetDocument GetSpreadsheetDocument(this WorksheetPart worksheetPart) => (SpreadsheetDocument) worksheetPart.OpenXmlPackage;

        public static WorkbookPart GetWorkbookPart(this WorksheetPart worksheetPart) =>
            // 既然都能拿到 WorksheetPart 了，那么 WorkbookPart 一定不是空
            worksheetPart.GetSpreadsheetDocument().WorkbookPart!;
    }
}
