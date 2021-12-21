#nullable disable // 这是其他项目的源代码，不做更改，也不支持可空
#pragma warning disable CS1591 // 这是其他项目的源代码，不做更改
#pragma warning disable 618 // 这是其他项目的源代码，不做更改
#pragma warning disable 1572 // 这是其他项目的源代码，不做更改
#pragma warning disable 1573
// ReSharper disable All

using System;

namespace RedBlackTree
{
    public class RBTreeException : Exception
    {
        public RBTreeException(String msg)
            : base(msg)
        {
        }
    }
}
