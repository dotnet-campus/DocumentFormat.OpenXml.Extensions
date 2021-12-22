using System.Text.RegularExpressions;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts
{
    /// <summary>
    /// 表示 Excel 单元格的坐标
    /// </summary>
    public readonly struct ExcelPosition
    {
        /// <summary>
        /// 创建 Excel 单元格的坐标
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public ExcelPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// 所在行序号，从 0 开始
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// 所在列序号，从 0 开始
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// 将如 B1 A3 字符串转换为 <see cref="ExcelPosition"/> 的值
        /// </summary>
        /// <remarks>
        /// 可采用 <see cref="ToString"/> 方法转换为如 B1 A3 字符串
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool TryParse(string? input, out ExcelPosition position)
        {
            if (input is null)
            {
                position = default;
                return false;
            }

            var match = Regex.Match(input, @"([A-Z]+)(\d+)");
            if (!match.Success)
            {
                position = default;
                return false;
            }

            var column = match.Groups[1].Value;
            var row = match.Groups[2].Value;

            int columnIndex;
            if (!TryParseExcelColumnIndex(column, out columnIndex))
            {
                position = default;
                return false;
            }

            position = new ExcelPosition(int.Parse(row) - 1, columnIndex - 1);
            return true;
        }

        /// <summary>
        /// 从字符串转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ExcelPosition? ConvertFromString(string? input)
        {
            if (TryParse(input, out var position))
            {
                return position;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将一个Excel表格的字符串列索引转换为数字索引，其中 A = 1, Z = 26 。
        /// 例如：AA 转换后的值为 27 。
        /// </summary>
        /// <param name="stringValue">一个全大写字母并且长度小于等于6的字符串参数。</param>
        /// <param name="columnIndex">如果解析成功，输出基于1的列索引值。</param>
        /// <returns>解析成功返回true，失败返回false。</returns>
        static bool TryParseExcelColumnIndex(string stringValue, out int columnIndex)
        {
            // 如果字符串长度大于6会导致数据运算时int类型数据溢出。
            if (string.IsNullOrWhiteSpace(stringValue) || stringValue.Length > 6)
            {
                columnIndex = default;
                return false;
            }

            int weight = 1;
            int value = 0;

            for (int i = stringValue.Length - 1; i >= 0; --i)
            {
                var charValue = stringValue[i];

                // 只要非大写字符，一个都不能容忍，直接返回false。
                if (charValue > 'Z' || charValue < 'A')
                {
                    columnIndex = default;
                    return false;
                }

                // 获取差值。
                var detla = charValue - 'A' + 1; // 为什么 + 1，意会就行了，我也解释不上来。
                // 差值乘以权。
                value += detla * weight;
                weight *= 26;
            }

            columnIndex = value;
            return true;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{(char) ('A' + Column)}{Row + 1}";
        }
    }
}
