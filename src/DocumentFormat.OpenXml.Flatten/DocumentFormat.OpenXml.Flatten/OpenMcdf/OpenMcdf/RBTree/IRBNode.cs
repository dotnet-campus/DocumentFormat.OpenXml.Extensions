#nullable disable // 这是其他项目的源代码，不做更改，也不支持可空
#pragma warning disable CS1591 // 这是其他项目的源代码，不做更改
#pragma warning disable 618 // 这是其他项目的源代码，不做更改
#pragma warning disable 1572 // 这是其他项目的源代码，不做更改
#pragma warning disable 1573
// ReSharper disable All

using System;

namespace RedBlackTree
{
    /// <summary>
    /// Red Black Node interface
    /// </summary>
    public interface IRBNode : IComparable
    {

        IRBNode Left
        {
            get;
            set;
        }

        IRBNode Right
        {
            get;
            set;
        }


        Color Color

        { get; set; }



        IRBNode Parent { get; set; }


        IRBNode Grandparent();


        IRBNode Sibling();
        //        {
        //#if ASSERT
        //            Debug.Assert(Parent != null); // Root node has no sibling
        //#endif
        //            if (this == Parent.Left)
        //                return Parent.Right;
        //            else
        //                return Parent.Left;
        //        }

        IRBNode Uncle();
        //        {
        //#if ASSERT
        //            Debug.Assert(Parent != null); // Root node has no uncle
        //            Debug.Assert(Parent.Parent != null); // Children of root have no uncle
        //#endif
        //            return Parent.Sibling();
        //        }
        //    }

        void AssignValueTo(IRBNode other);
    }
}
