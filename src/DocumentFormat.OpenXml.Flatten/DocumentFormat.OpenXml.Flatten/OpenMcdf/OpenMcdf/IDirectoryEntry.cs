#nullable disable // 这是其他项目的源代码，不做更改，也不支持可空
#pragma warning disable CS1591 // 这是其他项目的源代码，不做更改
#pragma warning disable 618 // 这是其他项目的源代码，不做更改
#pragma warning disable 1572 // 这是其他项目的源代码，不做更改
#pragma warning disable 1573
// ReSharper disable All

/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. 
 * 
 * The Original Code is OpenMCDF - Compound Document Format library.
 * 
 * The Initial Developer of the Original Code is Federico Blaseotto.*/


using RedBlackTree;
using System;


namespace OpenMcdf
{
    internal interface IDirectoryEntry : IComparable, IRBNode
    {
        int Child { get; set; }
        byte[] CreationDate { get; set; }
        byte[] EntryName { get; }
        string GetEntryName();
        int LeftSibling { get; set; }
        byte[] ModifyDate { get; set; }
        string Name { get; }
        ushort NameLength { get; set; }
        void Read(System.IO.Stream stream, CFSVersion ver = CFSVersion.Ver_3);
        int RightSibling { get; set; }
        void SetEntryName(string entryName);
        int SID { get; set; }
        long Size { get; set; }
        int StartSetc { get; set; }
        int StateBits { get; set; }
        StgColor StgColor { get; set; }
        StgType StgType { get; set; }
        Guid StorageCLSID { get; set; }
        void Write(System.IO.Stream stream);
    }
}
