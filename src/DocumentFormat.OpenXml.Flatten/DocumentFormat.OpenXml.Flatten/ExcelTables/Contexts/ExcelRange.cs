using System.Text.RegularExpressions;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts
{
    /// <summary>
    /// 表示一定范围的 Excel 单元格，采用包含端点关系
    /// </summary>
    public readonly struct ExcelRange
    {
        /// <summary>
        /// 表示一定范围的 Excel 单元格
        /// </summary>
        public ExcelRange(ExcelPosition startPosition) : this(startPosition, startPosition)
        {
        }

        /// <summary>
        /// 表示一定范围的 Excel 单元格
        /// </summary>
        public ExcelRange(ExcelPosition startPosition, ExcelPosition endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        /// <summary>
        /// 起始的点，此点也被包含在范围内
        /// </summary>
        public ExcelPosition StartPosition { get; }

        /// <summary>
        /// 结束的点，此点也被包含在范围内
        /// </summary>
        public ExcelPosition EndPosition { get; }

        /// <summary>
        /// 表示整个表格的范围
        /// </summary>
        public static ExcelRange WholeRange =>
            new ExcelRange(new ExcelPosition(0, 0), new ExcelPosition(int.MaxValue, int.MaxValue));

        /// <summary>
        /// 包含的行数量
        /// </summary>
        public int RowCount => EndPosition.Row - StartPosition.Row + 1;

        /// <summary>
        /// 包含的列数量
        /// </summary>
        public int ColumnCount => EndPosition.Column - StartPosition.Column + 1;

        /// <summary>
        /// 判断给定的 <paramref name="position"/> 是否在范围内
        /// <para>
        /// [B1:F7] 包含 B2 也包含 B1 的值
        /// </para>
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Contain(ExcelPosition position)
        {
            // [B1:F7] 包含 B2
            return position.Row >= StartPosition.Row
                   && position.Column >= StartPosition.Column
                   && position.Row <= EndPosition.Row
                   && position.Column <= EndPosition.Column;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{StartPosition}:{EndPosition}";
        }

        /// <summary>
        /// 尝试转换为表示一定范围的 Excel 单元格
        /// </summary>
        /// <returns></returns>
        public static bool TryParse(string? excelRange, out ExcelRange range)
        {
            if (excelRange is null)
            {
                range = default;
                return false;
            }

            var match = Regex.Match(excelRange, @"([A-Z]+\d+):([A-Z]+\d+)");

            if (match.Success)
            {
                // 如以下字符串
                // B1:F7
                // 在 Excel 的行是从 1 开始
                ExcelPosition? startPosition = ExcelPosition.ConvertFromString(match.Groups[1].Value);
                ExcelPosition? endPosition = ExcelPosition.ConvertFromString(match.Groups[2].Value);

                if (startPosition != null && endPosition != null)
                {
                    range = new ExcelRange(startPosition.Value, endPosition.Value);
                    return true;
                }
            }

            range = default;
            return false;
        }
    }
}
