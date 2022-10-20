using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;

namespace DocumentFormat.OpenXml.Flatten.Compatibilities.Packaging;

partial class CompatiblePackage
{
    /// <summary>
    /// This class represents a Part within a Zip container.
    /// This is a part of the Packaging Layer APIs.
    /// This implementation is specific to the Zip file format.
    /// </summary>
    public sealed class ZipPackagePart : PackagePart
    {
        #region Public Methods 定制逻辑

        /// <summary>
        /// Custom Implementation for the GetStream Method
        /// </summary>
        /// <param name="streamFileMode">Mode in which the stream should be opened</param>
        /// <param name="streamFileAccess">Access with which the stream should be opened</param>
        /// <returns>Stream Corresponding to this part</returns>
        protected override Stream? GetStreamCore(FileMode streamFileMode, FileAccess streamFileAccess)
        {
            if (_zipArchiveEntry != null)
            {
                // Reset the stream when FileMode.Create is specified.  Since ZipArchiveEntry only
                // ever supports opening once when the backing archive is in Create mode, we'll avoid
                // calling SetLength since the stream returned won't be seekable. You could still open
                // an archive in Update mode then call part.GetStream(FileMode.Create), in which case
                // we'll want this call to SetLength.
                if (streamFileMode == FileMode.Create && _zipArchiveEntry.Archive!.Mode != ZipArchiveMode.Create)
                {
                    using (var tempStream = _zipStreamManager.Open(_zipArchiveEntry, streamFileMode, streamFileAccess))
                    {
                        tempStream.SetLength(0);
                    }
                }

                var stream = _zipStreamManager.Open(_zipArchiveEntry, streamFileMode, streamFileAccess);

                return FixContent(stream);
            }
            return null;
        }

        private Stream FixContent(Stream stream)
        {
            if (Uri.OriginalString.EndsWith(".rels"))
            {
                var (needFix, newStream) = TryFixRelationship(stream);
                if (needFix && newStream is not null)
                {
                    stream.Dispose();
                    return newStream;
                }
                else
                {
                    Debug.Assert(stream.CanSeek);
                    stream.Position = 0;
                }
            }

            return stream;
        }

        /// <summary>
        /// 尝试修复 Relationship 内容
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// 先将代码写到这里，后续如果修复的条件更多，那再拆开
        /// 在 Office 里面，有大量的逻辑，用来处理诡异的课件
        private (bool needFix, Stream? newStream) TryFixRelationship(Stream stream)
        {
            // 尝试修复诡异的课件
            // - XmlException： Relationship tag requires attribute 'Type'
            var xDocument = XDocument.Load(stream);
            var root = xDocument.Root;
            if (root is null)
            {
                return (false, null);
            }

            bool needFix = false;
            if (string.Equals(root.Name.LocalName, "Relationships", StringComparison.Ordinal))
            {
                List<XElement> elementList = root.Elements().ToList();

                foreach (var xElement in elementList)
                {
                    if (string.Equals(xElement.Name.LocalName, "Relationship", StringComparison.Ordinal))
                    {
                        if (string.IsNullOrEmpty(xElement.Attribute("Type")?.Value))
                        {
                            // 处理 Type 为空的情况 XmlException： Relationship tag requires attribute 'Type'
                            needFix = true;

                            // 尝试找到其他的元素，其他的元素存在相同的 Target 内容，使用这个元素的 Type 替换。如果替换的内容是随意写的，那将会让相同的 Target 内容存在不同的定义，转换提示 DocumentFormat.OpenXml.Packaging.OpenXmlPackageException: 'A shared part is referenced by multiple source parts with a different relationship type.'
                            var otherSameTargetElement = FindOtherSameTargetElement(elementList, xElement);

                            var type = otherSameTargetElement?.Attribute("Type")?.Value;
                            if (string.IsNullOrEmpty(type))
                            {
                                // 如果没有找到相同的元素，先随意写一个
                                type = "http://schemas.microsoft.com/office/2007/relationships/unknow";
                            }

                            xElement.SetAttributeValue("Type", type);
                        }
                    }
                    else
                    {
                        // 理论上不会存在非 Relationship 元素
                    }
                }

                static XElement? FindOtherSameTargetElement(List<XElement> elementList, XElement currentElement)
                {
                    return elementList.FirstOrDefault(t =>
                        !ReferenceEquals(t, currentElement) &&
                        string.Equals(t.Attribute("Target")?.Value, currentElement.Attribute("Target")?.Value,
                            StringComparison.Ordinal));
                }
            }
            else
            {
                // 其他的？有对应的测试课件再来修
            }

            if (needFix)
            {
                var memoryStream = new MemoryStream();
                xDocument.Save(memoryStream);
                memoryStream.Position = 0;
                return (needFix, memoryStream);
            }

            return (needFix, null);
        }

        #endregion Public Methods

        #region Internal Constructors

        /// <summary>
        /// Constructs a ZipPackagePart for an atomic (i.e. non-interleaved) part.
        /// This is called from the ZipPackage class as a result of GetPartCore,
        /// GetPartsCore or CreatePartCore methods
        /// </summary>
        /// <param name="zipPackage"></param>
        /// <param name="zipArchive"></param>
        /// <param name="zipArchiveEntry"></param>
        /// <param name="zipStreamManager"></param>
        /// <param name="partUri"></param>
        /// <param name="compressionOption"></param>
        /// <param name="contentType"></param>
        internal ZipPackagePart(CompatiblePackage zipPackage,
            ZipArchive zipArchive,
            ZipArchiveEntry zipArchiveEntry,
            ZipStreamManager zipStreamManager,
            PackUriHelper.ValidatedPartUri partUri,
            string contentType,
            CompressionOption compressionOption)
            : base(zipPackage, partUri, contentType, compressionOption)
        {
            _zipPackage = zipPackage;
            _zipArchive = zipArchive;
            _zipStreamManager = zipStreamManager;
            _zipArchiveEntry = zipArchiveEntry;
        }

        #endregion Internal Constructors

        #region Internal Properties

        /// <summary>
        /// Obtain the ZipFileInfo descriptor of an atomic part.
        /// </summary>
        internal ZipArchiveEntry ZipArchiveEntry
        {
            get
            {
                return _zipArchiveEntry;
            }
        }

        #endregion Internal Properties

        #region Private Variables

        private readonly CompatiblePackage _zipPackage;
        private readonly ZipArchiveEntry _zipArchiveEntry;
        private readonly ZipArchive _zipArchive;
        private readonly ZipStreamManager _zipStreamManager;

        #endregion Private Variables
    }
}
