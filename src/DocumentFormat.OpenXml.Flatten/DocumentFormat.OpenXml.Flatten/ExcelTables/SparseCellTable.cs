using System.Collections;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables
{
    /// <summary>
    /// 稀疏的单元格表格
    /// </summary>
    public class SparseCellTable : IEnumerable<CellFlattenInfo>
    {
        internal SparseCellTable(WorksheetFlatten currentWorksheet, List<CellFlattenInfo> cellList)
        {
            CurrentWorksheet = currentWorksheet;
            _cellList = cellList;
        }

        /// <summary>
        /// 当前所在的工作表
        /// </summary>
        public WorksheetFlatten CurrentWorksheet { get; }

        private readonly List<CellFlattenInfo> _cellList;

        /// <inheritdoc />
        public IEnumerator<CellFlattenInfo> GetEnumerator()
        {
            return _cellList.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _cellList).GetEnumerator();
        }
    }
}
