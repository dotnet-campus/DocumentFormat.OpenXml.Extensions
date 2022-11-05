using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;
using DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive;
using DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Packaging;

using dotnetCampus.OpenXmlUnitConverter;

using MauiPptxViewerCore.Utils;

using Microsoft.Maui.Graphics;

using Shape = DocumentFormat.OpenXml.Presentation.Shape;

namespace MauiPptxViewerCore;

public class PptxSlide
{
    internal PptxSlide(uint slideId, SlidePart slidePart, DocumentModel documentModel)
    {
        SlideId = slideId;
        SlidePart = slidePart;
        DocumentModel = documentModel;
    }

    public UInt32 SlideId { get; }
    public SlidePart SlidePart { get; }
    private DocumentModel DocumentModel { get; }

    public void Render(ICanvas canvas)
    {
        var slide = SlidePart.Slide;

        var slideContext = new SlideContext(slide, DocumentModel.PresentationDocument);

        foreach (var openXmlElement in slide.CommonSlideData!.ShapeTree!.ChildElements)
        {
            canvas.SaveState();

            var openXmlElementFlatten = new OpenXmlElementFlatten();
            var newElement = openXmlElementFlatten.GetFlattenElement(openXmlElement, slideContext, shouldCloneOriginElement: false);

            //var rootElement ??= slideContext.RootElement;

            //var currentPart = slideContext.GetCurrentPart();
            //var colorMap = slideContext.GetColorMap();
            //var colorScheme = slideContext.GetColorScheme();

            if (newElement is Shape shape)
            {
                RenderShape(canvas, newElement, slideContext, shape);
            }
            else
            {
                // 其他类型的元素
            }

            canvas.RestoreState();
        }
    }

    private void RenderShape(ICanvas canvas, OpenXmlElement newElement, SlideContext slideContext, Shape shape)
    {
        var transformData = newElement.GetOrCreateTransformData(slideContext);

        // 这里渲染采用像素单位，需要进行转换
        // 设置坐标
        canvas.Translate((float)transformData.OffsetX.ToPixel().Value, (float)transformData.OffsetY.ToPixel().Value);

        var shapeProperties = shape.ShapeProperties;
        if (shapeProperties == null)
        {
            return;
        }

        var solidFill = shapeProperties.GetFirstChild<SolidFill>();
        if (solidFill != null)
        {
            var aRgbColor = ColorHelper.BuildColor(solidFill, slideContext);
            if (aRgbColor != null)
            {
                canvas.FillColor = ARgbToColor(aRgbColor);
            }
        }

        // 获取线条
        var outline = shapeProperties.GetFirstChild<Outline>();
        if (outline != null)
        {
            if (outline.Width is not null)
            {
                var emu = new Emu(outline.Width);
                canvas.StrokeSize = (float)emu.ToPixel().Value;
            }

            var lineFill = outline.GetFirstChild<SolidFill>();
            if (lineFill != null)
            {
                var aRgbColor = ColorHelper.BuildColor(lineFill, slideContext);
                if (aRgbColor != null)
                {
                    canvas.StrokeColor = ARgbToColor(aRgbColor);
                }
            }
        }

        var svgPath = shape.ToSvgPath();
        var multiShapePaths = svgPath?.MultiShapePaths;

        if (multiShapePaths != null)
        {
            foreach (var multiShapePath in multiShapePaths)
            {
                PathF path = PathConverter.ToPath(multiShapePath.Path);

                if (multiShapePath.IsFilled)
                {
                    canvas.FillPath(path, WindingMode.NonZero);
                }

                if (multiShapePath.IsStroke)
                {
                    canvas.DrawPath(path);
                }
            }
        }
    }

    private static Color ARgbToColor(ARgbColor aRgbColor)
    {
        var color = new Color(aRgbColor.R, aRgbColor.G, aRgbColor.B, aRgbColor.A);
        return color;
    }
}