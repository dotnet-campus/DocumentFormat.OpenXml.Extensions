using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables
{
    /// <summary>
    /// 工作表
    /// </summary>
    public class WorksheetFlatten
    {
        /// <summary>
        /// 创建工作表
        /// </summary>
        public WorksheetFlatten(WorksheetPart worksheetPart, ExcelTableConvertContext context)
        {
            WorksheetPart = worksheetPart;
            Context = context;

            var workbookPart = context.WorkbookPart;

            // todo 这里需要了解获取的是哪个才对
            SharedStringTablePart? sharedStringTablePart =
                workbookPart?.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
            SharedStringTable? sharedStringTable = sharedStringTablePart?.SharedStringTable;
            SharedStringTable = sharedStringTable;
            SetMergeRangeList();
        }

        /// <summary>
        /// 获取当前工作表是否被选中
        /// </summary>
        /// <returns></returns>
        public bool GetIsTabSelected()
        {
            return Worksheet.SheetViews?.GetFirstChild<SheetView>()?.TabSelected?.Value ?? false;
        }

        internal SheetData? SheetData => Worksheet.GetFirstChild<SheetData>();
        internal Worksheet Worksheet => WorksheetPart.Worksheet;
        internal WorksheetPart WorksheetPart { get; }
        internal ExcelTableConvertContext Context { get; }
        internal SharedStringTable? SharedStringTable { get; }

        /// <summary>
        /// 单元格合并范围信息集合。
        /// </summary>
        internal List<ExcelRange>? MergeRangeList { get; private set; }

        internal PixelSize GetDefaultCellSize()
        {
            var sheetFormatProperties = Worksheet.SheetFormatProperties;

            // 默认单位是磅，默认 Excel 行为是
            // 列宽：8.38 Excel 单位 约等于 1.83cm
            // 行高：14.25 磅
            Pixel width;
            var defaultColumnWidth = sheetFormatProperties?.DefaultColumnWidth?.Value;
            if (defaultColumnWidth != null)
            {
                width = new ExcelCellColumnWidth(defaultColumnWidth.Value).ToPixel();
            }
            else
            {
                width = CellFlattenStyle.DefaultCellSize.Width;
            }

            Pixel height = new Pound(sheetFormatProperties?.DefaultRowHeight?.Value ?? 14.25).ToPixel();
            // 虽然说的是行高列宽，但是为了保持先宽后高，修改了顺序
            return new PixelSize(width, height);
        }

        /// <summary>
        /// 获取当前工作表对应的稀疏单元格
        /// </summary>
        /// <returns></returns>
        public SparseCellTable GetSparseCellTable(ExcelRange? range = null)
        {
            // 工作表级单元格样式
            var sheetCellDefaultStyle = GetSheetCellDefaultStyle();

            ExcelRange excelRange = range ?? ExcelRange.WholeRange;

            var cellFlattenInfoList = new List<CellFlattenInfo>();

            var sheetData = SheetData;
            if (sheetData != null)
            {
                var sharedStringTable = SharedStringTable;

                // 格式如下
                /*
                  <sheetData>
                    <row r="1" spans="1:3" x14ac:dyDescent="0.2">
                      <c r="A1" t="s">
                        <v>0</v>
                      </c>
                      <c r="B1" t="s">
                        <v>1</v>
                      </c>
                    </row>
                  </sheetData>
                 */
                foreach (var row in sheetData.Elements<Row>())
                {
                    foreach (var cell in row.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                    {
                        var excelPositionValue = cell.CellReference?.Value;
                        var excelPosition = ExcelPosition.ConvertFromString(excelPositionValue);

                        if (excelPosition is null)
                        {
                            // 预期不是空的
                            continue;
                        }

                        if (excelRange.Contain(excelPosition.Value) is false)
                        {
                            // 如果不包含此单元格，那么忽略
                            continue;
                        }

                        var cellValue = cell.CellValue;
                        string displayText = string.Empty;

                        if (cellValue is not null)
                        {
                            switch (cell.DataType?.Value)
                            {
                                case CellValues.SharedString:
                                    {
                                        if (cellValue.TryGetInt(out var number))
                                        {
                                            if (sharedStringTable?.Count?.Value > number)
                                            {
                                                displayText = sharedStringTable.ElementAt(number).InnerText;
                                            }
                                        }

                                        break;
                                    }
                                default:
                                    {
                                        displayText = cellValue.InnerText ?? string.Empty;
                                        break;
                                    }
                            }
                        }

                        var style = sheetCellDefaultStyle.BuildNewProperty(s =>
                        {
                            // 仅仅只是做拷贝而已
                        });

                        var cellFlattenInfo =
                            new CellFlattenInfo(displayText, style, excelPosition.Value, this, row, cell);
                        cellFlattenInfoList.Add(cellFlattenInfo);
                    }
                }
            }

            return new SparseCellTable(this, cellFlattenInfoList);
        }

        /// <summary>
        /// 设置单元格合并信息范围列表。
        /// </summary>
        private void SetMergeRangeList()
        {
            var mergeCellList = WorksheetPart.Worksheet?.Elements<MergeCells>()?.FirstOrDefault()?.Elements<MergeCell>();

            if (mergeCellList is null)
            {
                return;
            }

            var mergeRangeList = new List<ExcelRange>();

            foreach (var mergeCell in mergeCellList)
            {
                if (ExcelRange.TryParse(mergeCell.Reference?.Value, out ExcelRange excelRange))
                {
                    mergeRangeList.Add(excelRange);
                }
            }

            MergeRangeList = mergeRangeList;
        }

        /// <summary>
        /// 工作表级单元格样式
        /// </summary>
        private IReadonlyCellFlattenStyle GetSheetCellDefaultStyle()
        {
            var sheetCellDefaultStyle = new CellFlattenStyle()
            {
                Size = GetDefaultCellSize(),
            };
            FillDefaultCellStyle(sheetCellDefaultStyle);

            return sheetCellDefaultStyle;
        }

        private void FillDefaultCellStyle(CellFlattenStyle sheetCellDefaultStyle)
        {
            /*
              在 styles.xml 里面，可以看到如下定义
              如字体大小，定义样式的单元格文字大小就是获取第一个值
              单位是磅
              <fonts count="2" x14ac:knownFonts="1">
                <font>
                  <sz val="16" />
                  <color theme="1" />
                  <name val="等线" />
                  <family val="2" /> 
                  <charset val="134" />
                  <scheme val="minor" />
                </font>
                <font>
                  <sz val="9" />
                  <name val="等线" />
                  <family val="2" />
                  <charset val="134" />
                  <scheme val="minor" />
                </font>
              </fonts>
             */
            var stylesheet = Context.Stylesheet;
            if (stylesheet == null)
            {
                return;
            }

            var font = stylesheet.Fonts?.Elements<Font>()?.FirstOrDefault();
            if (font != null)
            {
                var fontSize = font.FontSize?.Val?.Value;
                if (fontSize != null)
                {
                    sheetCellDefaultStyle.FontSize = new Pound(fontSize.Value);
                }

                var fontName = font.FontName?.Val?.Value;
                if (fontName != null)
                {
                    sheetCellDefaultStyle.FontName = fontName;
                }
            }
        }
    }
}
