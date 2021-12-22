using System.Collections;
using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml.Flatten.ExcelTables.Contexts;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Flatten.ExcelTables
{
    /// <summary>
    /// 工作表集合
    /// </summary>
    public class WorksheetsFlatten : IReadOnlyList<WorksheetFlatten>
    {
        /// <summary>
        /// 工作表集合
        /// </summary>
        /// <param name="worksheetParts"></param>
        /// <param name="context"></param>
        internal WorksheetsFlatten(IEnumerable<WorksheetPart>? worksheetParts,
            ExcelTableConvertContext context)
        {
            Context = context;
            if (worksheetParts is null)
            {
                InnerList = new WorksheetFlatten[0];
            }
            else
            {
                InnerList = worksheetParts.Select(t => new WorksheetFlatten(t, Context)).ToList();
            }
        }

        private IReadOnlyList<WorksheetFlatten> InnerList { get; }
        private ExcelTableConvertContext Context { get; }

        /// <inheritdoc />
        public IEnumerator<WorksheetFlatten> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) InnerList).GetEnumerator();
        }

        /// <inheritdoc />
        public int Count => InnerList.Count;

        /// <inheritdoc />
        public WorksheetFlatten this[int index] => InnerList[index];
    }
}
