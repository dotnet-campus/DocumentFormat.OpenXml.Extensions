#nullable disable // 这是迁移旧代码，不敢开可空
// ReSharper disable CheckNamespace 特别的命名空间

using System.Collections.Generic;

namespace OpenMcdf
{
    internal class ReadonlyCompoundFileItemComparer : IComparer<ReadonlyCompoundFileItem>
    {
        public int Compare(ReadonlyCompoundFileItem x, ReadonlyCompoundFileItem y)
        {
            // X CompareTo Y : X > Y --> 1 ; X < Y  --> -1
            return (x.DirEntry.CompareTo(y.DirEntry));

            //Compare X < Y --> -1
        }
    }
}
