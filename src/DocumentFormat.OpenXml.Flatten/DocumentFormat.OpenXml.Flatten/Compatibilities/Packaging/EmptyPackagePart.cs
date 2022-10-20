using System;
using System.IO;
using System.IO.Packaging;
using System.Text.RegularExpressions;

namespace DocumentFormat.OpenXml.Flatten.Compatibilities.Packaging;

partial class CompatiblePackage
{
    private class EmptyPackagePart : PackagePart
    {
        private Package _package;
        private readonly ContentTypeHelper _contentTypeHelper;
        public EmptyPackagePart(Package package, Uri partUri, ContentTypeHelper contentTypeHelper) : base(package, partUri)
        {
            _package = package;
            _contentTypeHelper = contentTypeHelper;
        }

        protected override string GetContentTypeCore()
        {
            const string UnknownType = "application/vnd.openxmlformats-officedocument.miss-content-unknown-type";

            var validatedPartUri = PackUriHelper.GetRelationshipPartUri(Uri) as PackUriHelper.ValidatedPartUri;
            if (validatedPartUri == null)
            {
                return UnknownType;
            }

            var sourcePartUriFromRelationshipPartUri = PackUriHelper.GetSourcePartUriFromRelationshipPartUri(validatedPartUri) as PackUriHelper.ValidatedPartUri;
            if (sourcePartUriFromRelationshipPartUri is not null)
            {
                var contentType = GetContentType(sourcePartUriFromRelationshipPartUri);
                if (!string.IsNullOrEmpty(contentType?.ToString()))
                {
                    return contentType!.ToString();
                }
            }
            return UnknownType;
        }

        internal CompatiblePackage.ContentType? GetContentType(CompatiblePackage.PackUriHelper.ValidatedPartUri partUri)
        {
            //Step 1: Check if there is an override entry present corresponding to the
            //partUri provided. Override takes precedence over the default entries
            var overrideDictionary = _contentTypeHelper.GetOverrideDictionary();
            if (overrideDictionary != null)
            {
                if (overrideDictionary.ContainsKey(partUri))
                    return overrideDictionary[partUri];
            }

            //Step 2: 通过路径进行特殊判断
            // 例如 Slide 几的页面路径
            var uri = partUri.OriginalString;
            if (Regex.IsMatch(uri, @"/ppt/slides/slide\d+.xml"))
            {
                // "/ppt/slides/slide0.xml"
                return new CompatiblePackage.ContentType(
                    "application/vnd.openxmlformats-officedocument.presentationml.slide+xml");
            }

            if (Regex.IsMatch(uri, @"/ppt/slideLayouts/slideLayout\d+\.xml"))
            {
                // "/ppt/slideLayouts/slideLayout0.xml"
                return new CompatiblePackage.ContentType("application/vnd.openxmlformats-officedocument.presentationml.slideLayout+xml");
            }

            if (Regex.IsMatch(uri, @"/ppt/slideMasters/slideMaster\d\.xml"))
            {
                // "/ppt/slideMasters/slideMaster0.xml"
                return new CompatiblePackage.ContentType("application/vnd.openxmlformats-officedocument.presentationml.slideMaster+xml");
            }

            if (Regex.IsMatch(uri, @"/ppt/theme/theme\d+\.xml"))
            {
                // "/ppt/theme/theme0.xml"
                return new CompatiblePackage.ContentType("application/vnd.openxmlformats-officedocument.theme+xml");
            }

            if (Regex.IsMatch(uri, @"/ppt/notesMasters/notesMaster\d+\.xml"))
            {
                // "/ppt/notesMasters/notesMaster0.xml"
                return new CompatiblePackage.ContentType("application/vnd.openxmlformats-officedocument.presentationml.notesMaster+xml");
            }

            //Step 3: Check if there is a default entry corresponding to the
            //extension of the partUri provided.
            string extension = partUri.PartUriExtension;

            var defaultDictionary = _contentTypeHelper.GetDefaultDictionary();
            if (defaultDictionary.ContainsKey(extension))
                return defaultDictionary[extension];

            //Step 4: 使用后缀名默认
            extension = extension.ToLowerInvariant();
            var extensionToContentType = ExtensionToContentType(extension);
            if (extensionToContentType != null)
            {
                return new CompatiblePackage.ContentType(extensionToContentType);
            }

            //Step 5: If we did not find an entry in the override and the default
            //dictionaries, this is an error condition
            return null;
        }

        protected override Stream GetStreamCore(FileMode mode, FileAccess access)
        {
            var memoryStream = new MemoryStream();
            return memoryStream;
        }

        private static string? ExtensionToContentType(string extensionWithLowerInvariant)
            => MimeUtility.LookupType(extensionWithLowerInvariant);
    }
}
