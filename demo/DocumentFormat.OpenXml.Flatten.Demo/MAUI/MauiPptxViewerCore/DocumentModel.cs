using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace MauiPptxViewerCore;

record DocumentModel(PresentationDocument PresentationDocument, Presentation Presentation)
{
    public PresentationPart PresentationPart
        // 这里能拿到 Presentation 那就一定能拿到 PresentationPart 对象，一定不是空
        => Presentation.PresentationPart!;
}