using DocumentFormat.OpenXml.Flatten.Compatibilities.Packaging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.Maui.Graphics;

namespace MauiPptxViewerCore;

public class PptxViewer
{
    public PptxViewer(FileInfo pptxFile, ICanvas canvas)
    {
        PptxFile = pptxFile;
        Canvas = canvas;
    }

    public FileInfo PptxFile { get; }
    public ICanvas Canvas { get; }

    private Stream? _stream; // do not care about dispose it... This is a demo

    public void Open()
    {
        _stream = PptxFile.Open(FileMode.Open, FileAccess.ReadWrite);

        // 解析细节请参阅 [Office 使用 OpenXML SDK 解析文档博客目录](https://blog.lindexi.com/post/Office-%E4%BD%BF%E7%94%A8-OpenXML-SDK-%E8%A7%A3%E6%9E%90%E6%96%87%E6%A1%A3%E5%8D%9A%E5%AE%A2%E7%9B%AE%E5%BD%95.html )
        var package = CompatiblePackageProvider.OpenPackage(_stream, FileMode.Open, FileAccess.ReadWrite);

        PresentationDocument presentationDocument = PresentationDocument.Open(package);

        Presentation presentation = presentationDocument.PresentationPart!.Presentation;

        var documentModel = new DocumentModel(presentationDocument, presentation);
        _pptxDocument = new PptxDocument(documentModel);

        var pptxSlide = _pptxDocument.SlideList.FirstOrDefault();
        if (pptxSlide != null)
        {
            pptxSlide.Render(Canvas);
        }
    }

    private PptxDocument? _pptxDocument;
}


//public record PptxViewerBuilder(FileInfo PptxFile, ICanvas Canvas)
//{
//}
