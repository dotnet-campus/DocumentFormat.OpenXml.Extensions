using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace MauiPptxViewerCore;

class PptxDocument
{
    public PptxDocument(DocumentModel documentModel)
    {
        DocumentModel = documentModel;

        var slideIdList = documentModel.Presentation.SlideIdList;

        if (slideIdList is null)
        {
            // 这个文档连一页都没有…
            throw new ArgumentException($"The Empty document. The number of slide is 0.");
        }

        var presentationPart = documentModel.PresentationPart;

        var pptxSlideList = new List<PptxSlide>();

        // [dotnet OpenXML 幻灯片 PPTX 的 Slide Id 和页面序号的关系](https://blog.lindexi.com/post/dotnet-OpenXML-%E5%B9%BB%E7%81%AF%E7%89%87-PPTX-%E7%9A%84-Slide-Id-%E5%92%8C%E9%A1%B5%E9%9D%A2%E5%BA%8F%E5%8F%B7%E7%9A%84%E5%85%B3%E7%B3%BB.html)
        foreach (var slideId in slideIdList.ChildElements.OfType<SlideId>())
        {
            // 获取页面内容
            Debug.Assert(slideId.RelationshipId != null, "slideId.RelationshipId != null");
            SlidePart slidePart = (SlidePart) presentationPart.GetPartById(slideId.RelationshipId!);

            Debug.Assert(slideId.Id is not null, "slideId.Id != null");
            var pptxSlide = new PptxSlide(slideId.Id!.Value, slidePart, documentModel);

            pptxSlideList.Add(pptxSlide);
        }

        SlideList = pptxSlideList;
    }

    public DocumentModel DocumentModel { get; }

    public List<PptxSlide> SlideList { get; }
}