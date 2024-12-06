using System;
using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Flatten.Framework;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Office.Drawing;
using DocumentFormat.OpenXml.Packaging;

using GraphicFrame = DocumentFormat.OpenXml.Presentation.GraphicFrame;
using Point = DocumentFormat.OpenXml.Drawing.Diagrams.Point;
using ShapeProperties = DocumentFormat.OpenXml.Office.Drawing.ShapeProperties;
using SmartArtShape = DocumentFormat.OpenXml.Office.Drawing.Shape;
using TextBody = DocumentFormat.OpenXml.Office.Drawing.TextBody;
using Transform2D = DocumentFormat.OpenXml.Drawing.Transform2D;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{

    class SmartArtElementFlattenConverter : OpenXmlElementFlattenConverterBase<GraphicFrame>
    {
        protected override GraphicFrame Convert(GraphicFrame element, ElementContext context)
        {
            /* SmartArt元素OpenXml文件结构对应关系
             * slide#.xml下的一个SmartArt元素graphicFrame#一一对应:呈现层drawing#.xml、数据层data#.xml、快速样式层quickStyle#.xml、布局层layout#.xml、颜色层colors#.xml
             * 呈现层drawing#.xml的样式通过对应数据层data#.xml，再找到颜色层colors#.xml，再通过其节点名称从theme.xml找到真正的样式数据
             */
            var relationshipIds = element.Graphic?.GraphicData?.GetFirstChild<RelationshipIds>();
            if (relationshipIds is null)
            {
                return element;
            }

            //通过关系关联出数据层data#.xml和颜色层colors#.xml
            DiagramDataPart? dataPart = null;
            DiagramColorsPart? colorsPart = null;
            if (relationshipIds.DataPart?.Value is not null)
            {
                dataPart = context.GetCurrentPart(element).GetPartById(relationshipIds.DataPart.Value) as DiagramDataPart;
            }
            //假如dataPart为空，则不需要进行继承样式
            if (dataPart is null)
            {
                return element;
            }

            //colorsPart为空，也不需要直接返回，因为以dataPart为主
            if (relationshipIds.ColorPart?.Value is not null)
            {
                colorsPart = context.GetCurrentPart(element).GetPartById(relationshipIds.ColorPart.Value) as DiagramColorsPart;
            }

            //真正呈现层元素继承数据层逻辑
            FlattenDrawingElementFromData(element, dataPart, colorsPart, context);

            return element;
        }


        /// <summary>
        /// SmartArt元素通过继承关系真正继承其样式方法
        /// </summary>
        /// <param name="xmlElement">SmartArt元素</param>
        /// <param name="diagramDataPart">数据层</param>
        /// <param name="colorsPart">颜色层</param>
        /// <param name="context">元素上下文</param>
        private void FlattenDrawingElementFromData(GraphicFrame xmlElement, DiagramDataPart diagramDataPart, DiagramColorsPart? colorsPart, in ElementContext context)
        {
            var dataModelRoot = diagramDataPart.DataModelRoot;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (dataModelRoot is null)
            {
                // 这个 dataModelRoot 是可空的，请看内部 issues/627
                return;
            }

            var connectionList = dataModelRoot.ConnectionList;
            var pointList = dataModelRoot.PointList;
            if (connectionList is null || pointList is null)
            {
                return;
            }

            var dataModelExtensionList = dataModelRoot.GetFirstChild<DataModelExtensionList>();
            var dataModelExtension = dataModelExtensionList?.GetFirstChild<DataModelExtension>();
            var dataModelExtensionBlock = dataModelExtension?.GetFirstChild<DataModelExtensionBlock>();
            var relIdValue = dataModelExtensionBlock?.RelId?.Value;
            if (relIdValue is null)
            {
                return;
            }

            //通过relIdValue找出呈现层
            var drawingPart = context.GetCurrentPart(xmlElement).GetPartById(relIdValue);
            if (drawingPart is DiagramPersistLayoutPart diagramPersistLayoutPart && diagramPersistLayoutPart.Drawing?.ShapeTree is not null)
            {
                foreach (var shape in diagramPersistLayoutPart.Drawing.ShapeTree.ChildElements.OfType<SmartArtShape>())
                {
                    FlattenShapeByPointListAndConnectionList(shape, pointList, connectionList, colorsPart, context);
                }
            }
        }

        private void FlattenShapeByPointListAndConnectionList(SmartArtShape shape, PointList pointList, ConnectionList connectionList, DiagramColorsPart? colorsPart, in ElementContext context)
        {
            var modelId = shape.ModelId;
            if (modelId is null)
            {
                return;
            }
            var shapeNonVisualProperties = shape.GetOrCreateElement<ShapeNonVisualProperties>();
            var nonVisualDrawingShapeProperties = shapeNonVisualProperties.NonVisualDrawingShapeProperties;


            /* SmartArt子元素的呈现层和数据层的逻辑关系
             *  呈现层drawing#.xml下面的元素，通过元素的ModelId关联数据层的data#.xml的父节点Point的ModelId
             * 元素的子元素，例如文本，通过元素的ModelId，关联数据层data#.xml链接列表cxnLst下的Connection的DestinationId，Type优先级PresentationOf>PresentationParentOf
             * 再通过关联到的Connection的SourceId去关联子节点Point的ModelId
             * 满足两个条件则呈现层需要继承数据层样式和数据：1.父节点不为空 2.关联到的Connection数量>0
             */
            var parentPoint = pointList.ChildElements.FirstOrDefault(t => t is Point point && point.ModelId == shape.ModelId) as Point;
            if (parentPoint is null)
            {
                return;
            }

            var connections = new List<Connection>();
            //优先查找Type为PresentationOf的Connection
            foreach (var element in connectionList.ChildElements)
            {
                if (element is Connection connection && connection.DestinationId == modelId && connection.Type?.Value == ConnectionValues.PresentationOf)
                {
                    connections.Add(connection);
                }
            }

            //假如不存在，再查找Type为PresentationParentOf的Connection
            if (!connections.Any())
            {
                foreach (var element in connectionList.ChildElements)
                {
                    if (element is Connection connection && connection.DestinationId == modelId && connection.Type?.Value == ConnectionValues.PresentationParentOf)
                    {
                        connections.Add(connection);
                    }
                }
            }


            foreach (var connection in connections)
            {
                foreach (var element in pointList.ChildElements)
                {
                    if (element is Point point && point.ModelId == connection.SourceId)
                    {
                        var pointNonVisualDrawingShapeProperties = point.ShapeProperties?.GetFirstChild<ShapeNonVisualProperties>()?.NonVisualDrawingShapeProperties;
                        nonVisualDrawingShapeProperties!.TextBox = ElementFlattenExtension.GetFlattenProperty(properties => properties.TextBox!, nonVisualDrawingShapeProperties,
                                pointNonVisualDrawingShapeProperties!);

                        FlattenShapePropertiesFromPoint(shape, point, parentPoint, colorsPart, context);

                    }
                }
            }
        }

        private void FlattenShapePropertiesFromPoint(SmartArtShape shape, Point point, Point parentPoint, DiagramColorsPart? colorsPart, in ElementContext context)
        {
            if (colorsPart is null)
            {
                return;
            }

            var shapeProperties = shape.GetOrCreateElement<ShapeProperties>();

            OpenXmlElement? backgroundColor = null;
            OpenXmlElement? outlineColor = null;
            OpenXmlElement? effectColor = null;
            OpenXmlElement? textFillColor = null;

            /* 继承colors#.xml的样式逻辑
             * 通过在data#.xml找到的父节点parentPoint，根据其PropertySet的PresentationStyleLabel从colors#.xml找到样式节点
             *  通过PropertySet的PresentationStyleIndex找到对应的索引样式，PresentationStyleIndex超过对应样式列表最大值则取最后个索引值样式
             */
            var styleLabelName = parentPoint.PropertySet?.PresentationStyleLabel?.Value;
            var styleIndex = parentPoint.PropertySet?.PresentationStyleIndex;
            if (styleLabelName is not null && styleIndex?.Value is not null)
            {
                foreach (var xmlElement in colorsPart.ColorsDefinition.ChildElements)
                {
                    if (xmlElement is ColorTransformStyleLabel styleLabel && styleLabel.Name == styleLabelName)
                    {
                        //填充样式
                        if (styleLabel.FillColorList is not null && styleLabel.FillColorList.ChildElements.Any())
                        {
                            var index = styleLabel.FillColorList.ChildElements.Count - 1;
                            backgroundColor = styleIndex!.Value > index ? styleLabel.FillColorList.ChildElements[index] : styleLabel.FillColorList.ChildElements[styleIndex];
                        }

                        //边框样式
                        if (styleLabel.LineColorList is not null && styleLabel.LineColorList.ChildElements.Any())
                        {
                            var index = styleLabel.LineColorList.ChildElements.Count - 1;
                            outlineColor = styleIndex!.Value > index ? styleLabel.LineColorList.ChildElements[index] : styleLabel.LineColorList.ChildElements[styleIndex];
                        }

                        //特效样式
                        if (styleLabel.EffectColorList is not null && styleLabel.EffectColorList.ChildElements.Any())
                        {
                            var index = styleLabel.EffectColorList.ChildElements.Count - 1;
                            effectColor = styleIndex!.Value > index ? styleLabel.EffectColorList.ChildElements[index] : styleLabel.EffectColorList.ChildElements[styleIndex];
                        }

                        //文本前景色样式
                        if (styleLabel.TextFillColorList is not null && styleLabel.TextFillColorList.ChildElements.Any())
                        {
                            var index = styleLabel.TextFillColorList.ChildElements.Count - 1;
                            textFillColor = styleIndex!.Value > index ? styleLabel.TextFillColorList.ChildElements[index] : styleLabel.TextFillColorList.ChildElements[styleIndex];
                        }
                    }
                }
            }

            FlattenTransformData(shapeProperties, point, context);
            FlattenPresetGeometry(shapeProperties, point, context);
            FlattenBackgroundBrush(shape, point, backgroundColor, context);
            FlattenOutline(shape, point, outlineColor, context);
            FlattenEffectList(shape, point, effectColor, context);
            FlattenTextBody(shape, point, textFillColor, context);

        }

        private void FlattenOutline(SmartArtShape shape, Point point, OpenXmlElement? outlineColor, in ElementContext context)
        {
            var pointOutline = point.ShapeProperties?.GetFirstChild<Outline>();
            if (pointOutline is null)
            {
                return;
            }

            var shapeOutline = shape.ShapeProperties?.GetOrCreateElement<Outline>();
            var provider = new OpenXmlCompositeElementFlattenProvider<Outline>
            (
                shapeOutline,
                pointOutline
            );

            var elementOutline = provider.Main;
            elementOutline.Width = provider.GetFlattenProperty(outline => outline.Width);
            elementOutline.Alignment = provider.GetFlattenProperty(outline => outline.Alignment);
            elementOutline.CapType = provider.GetFlattenProperty(outline => outline.CapType);
            elementOutline.CompoundLineType = provider.GetFlattenProperty(outline => outline.CompoundLineType);

            if (shapeOutline is not null)
            {
                var referenceSchemeColor = point.PropertySet?.Style?.LineReference?.GetFirstChild<SchemeColor>();
                FlattenBackgroundOrOutlineSolidFillFromPoint(shapeOutline, pointOutline, outlineColor, referenceSchemeColor);
            }
        }

        private void FlattenBackgroundOrOutlineSolidFillFromPoint(OpenXmlCompositeElement shapeElement, OpenXmlCompositeElement pointElement, OpenXmlElement? schemeColorElement, OpenXmlElement? referenceSchemeColorElement)
        {
            //当存在其他填充，则不拍平
            if (!IsFlattenSolidFill(pointElement) || !IsFlattenSolidFill(shapeElement))
            {
                return;
            }

            var pointSolidFill = pointElement.GetFirstChild<SolidFill>();
            if (pointSolidFill is null)
            {
                //假如DataPart不存在SolidFill且DrawingPart的SolidFill存在SchemeColor且带颜色变换值，则不拍平
                var shapeSchemeColor = shapeElement.GetFirstChild<SolidFill>()?.SchemeColor;
                if (shapeSchemeColor is not null && !IsFlattenSchemeColor(shapeSchemeColor))
                {
                    return;
                }

                //拍平SchemeColor优先级：FillReference > Point映射关系拿到的SchemeColor
                if (referenceSchemeColorElement is SchemeColor referenceSchemeColor)
                {
                    var solidFill = pointElement.GetOrCreateElement<SolidFill>();
                    var pointSchemeColor = solidFill.GetOrCreateElement<SchemeColor>();
                    pointSchemeColor.Val = referenceSchemeColor.Val;
                }
                else if (schemeColorElement is SchemeColor schemeColor)
                {
                    var solidFill = pointElement.GetOrCreateElement<SolidFill>();
                    var pointSchemeColor = solidFill.GetOrCreateElement<SchemeColor>();
                    pointSchemeColor.Val = schemeColor.Val;
                }
            }

            pointSolidFill = pointElement.GetFirstChild<SolidFill>();
            if (pointSolidFill is not null)
            {
                //假如获取到DataPart的SolidFill，且DrawingPart的SolidFill存在，则需要先删除DrawingPart的SolidFill
                var solidFill = shapeElement.GetFirstChild<SolidFill>();
                if (solidFill is not null)
                {
                    shapeElement.RemoveChild(solidFill);
                }
            }
            var provider = new OpenXmlCompositeElementFlattenProvider<SolidFill>
            (
                shapeElement.GetOrCreateElement<SolidFill>(),
                pointSolidFill
            );

            provider
                .FlattenMainElementProperty(fill => fill.SchemeColor)
                .FlattenMainElementProperty(fill => fill.PresetColor)
                .FlattenMainElementProperty(fill => fill.SystemColor)
                .FlattenMainElementProperty(fill => fill.HslColor)
                .FlattenMainElementProperty(fill => fill.RgbColorModelHex)
                .FlattenMainElementProperty(fill => fill.RgbColorModelPercentage);

        }

        static bool IsFlattenSolidFill(OpenXmlCompositeElement? element)
        {
            if (element?.GetFirstChild<NoFill>() is not null)
            {
                return false;
            }

            if (element?.GetFirstChild<BlipFill>() is not null)
            {
                return false;
            }

            if (element?.GetFirstChild<GradientFill>() is not null)
            {
                return false;
            }

            if (element?.GetFirstChild<PatternFill>() is not null)
            {
                return false;
            }

            return true;
        }

        private void FlattenEffectList(SmartArtShape shape, Point point, OpenXmlElement? effectColor, in ElementContext context)
        {
            var pointEffectList = point.ShapeProperties?.GetFirstChild<EffectList>();
            var provider = new OpenXmlCompositeElementFlattenProvider<EffectList>
            (
                shape.ShapeProperties?.GetOrCreateElement<EffectList>(),
                pointEffectList
            );

            provider.FlattenMainElementProperty(effectList => effectList.Blur)
                .FlattenMainElementProperty(effectList => effectList.FillOverlay)
                .FlattenMainElementProperty(effectList => effectList.Glow)
                .FlattenMainElementProperty(effectList => effectList.InnerShadow)
                .FlattenMainElementProperty(effectList => effectList.OuterShadow)
                .FlattenMainElementProperty(effectList => effectList.PresetShadow)
                .FlattenMainElementProperty(effectList => effectList.Reflection)
                .FlattenMainElementProperty(effectList => effectList.SoftEdge);

            if (effectColor is SchemeColor schemeColor)
            {
                var effectList = shape.ShapeProperties?.GetOrCreateElement<EffectList>();

                //目前有存在颜色的效果为：阴影和发光
                var innerShadowSchemeColor = effectList?.InnerShadow?.SchemeColor;
                var outerShadowSchemeColor = effectList?.OuterShadow?.SchemeColor;
                var presetShadowSchemeColor = effectList?.PresetShadow?.SchemeColor;
                var glowSchemeColor = effectList?.Glow?.SchemeColor;

                var pointEffectList1 = point.ShapeProperties?.GetFirstChild<EffectList>();
                if (pointEffectList1?.InnerShadow?.SchemeColor is not null && innerShadowSchemeColor is not null && IsFlattenSchemeColor(innerShadowSchemeColor))
                {
                    innerShadowSchemeColor.Val = schemeColor.Val;
                }

                if (pointEffectList1?.OuterShadow?.SchemeColor is not null && outerShadowSchemeColor is not null && IsFlattenSchemeColor(outerShadowSchemeColor))
                {
                    outerShadowSchemeColor.Val = schemeColor.Val;
                }

                if (pointEffectList1?.PresetShadow?.SchemeColor is not null && presetShadowSchemeColor is not null && IsFlattenSchemeColor(presetShadowSchemeColor))
                {
                    presetShadowSchemeColor.Val = schemeColor.Val;
                }

                if (pointEffectList1?.Glow?.SchemeColor is not null && glowSchemeColor is not null && IsFlattenSchemeColor(glowSchemeColor))
                {
                    glowSchemeColor.Val = schemeColor.Val;
                }

            }
        }

        /// <summary>
        /// 是否需要继承主题颜色样式
        /// </summary>
        /// <param name="schemeColor"></param>
        /// <returns></returns>
        private static bool IsFlattenSchemeColor(SchemeColor schemeColor)
        {
            //当SchemeColor存在子元素，则不需要继承Colors的主题色
            var tint = schemeColor.GetFirstChild<Tint>();
            var hueOffset = schemeColor.GetFirstChild<HueOffset>();
            var saturationOffset = schemeColor.GetFirstChild<SaturationOffset>();
            var luminanceOffset = schemeColor.GetFirstChild<LuminanceOffset>();
            var alphaOffset = schemeColor.GetFirstChild<AlphaOffset>();

            if (tint?.Val?.Value is not null)
            {
                return false;
            }

            if (saturationOffset?.Val?.Value is not null)
            {
                return false;
            }

            if (luminanceOffset?.Val?.Value is not null)
            {
                return false;
            }

            if (alphaOffset?.Val?.Value is not null)
            {
                return false;
            }

            if (hueOffset?.Val?.Value is not null)
            {
                return false;
            }

            return true;
        }

        private void FlattenBackgroundBrush(SmartArtShape shape, Point point, OpenXmlElement? backgroundColor, in ElementContext context)
        {
            if (point.ShapeProperties is null)
            {
                return;
            }

            if (shape.ShapeProperties is not null)
            {
                //拍平SchemeColor优先级：FillReference > Point映射关系拿到的SchemeColor
                //假如从Style的FillReference拿到SchemeColor，则直接拍平为该值
                var referenceSchemeColor = point.PropertySet?.Style?.FillReference?.GetFirstChild<SchemeColor>();
                FlattenBackgroundOrOutlineSolidFillFromPoint(shape.ShapeProperties, point.ShapeProperties, backgroundColor, referenceSchemeColor);
            }
        }

        private void FlattenTransformData(ShapeProperties shapeProperties, Point point, in ElementContext context)
        {
            var pointTransform2D = point.ShapeProperties?.Transform2D;
            var provider = new OpenXmlCompositeElementFlattenProvider<Transform2D>
            (
                shapeProperties.GetOrCreateElement<Transform2D>(),
                pointTransform2D
            );

            provider.FlattenMainElementProperty(transform2D => transform2D.Offset!)
                .FlattenMainElementProperty(transform2D => transform2D.Extents!);

            var elementTransform2D = provider.Main;
            elementTransform2D.Rotation = provider.GetFlattenProperty(transform2D => transform2D.Rotation);
            elementTransform2D.HorizontalFlip = provider.GetFlattenProperty(transform2D => transform2D.HorizontalFlip);
            elementTransform2D.VerticalFlip = provider.GetFlattenProperty(transform2D => transform2D.VerticalFlip);
        }

        private void FlattenPresetGeometry(ShapeProperties shapeProperties, Point point, in ElementContext context)
        {
            var pointPresetGeometry = point.ShapeProperties?.GetFirstChild<PresetGeometry>();

            if (shapeProperties.GetFirstChild<PresetGeometry>() is null && pointPresetGeometry is null)
            {
                return;
            }

            var provider = new OpenXmlCompositeElementFlattenProvider<PresetGeometry>
            (
                shapeProperties.GetOrCreateElement<PresetGeometry>(),
                pointPresetGeometry
            );

            provider.FlattenMainElementProperty(presetGeometry => presetGeometry.AdjustValueList);

            var elementPresetGeometry = provider.Main;
            elementPresetGeometry.Preset = provider.GetFlattenProperty(presetGeometry => presetGeometry.Preset);
            elementPresetGeometry.AdjustValueList = provider.GetFlattenProperty(presetGeometry => presetGeometry.AdjustValueList);
        }

        private void FlattenTextBody(SmartArtShape shape, Point point, OpenXmlElement? textFillColor, in ElementContext context)
        {
            if (point.TextBody is null)
            {
                return;
            }

            var textBody = shape.GetOrCreateElement<TextBody>();

            FlattenTextBodyProperties(textBody, point, context);
            FlattenParagraph(textBody, point, textFillColor, context);
        }

        private void FlattenTextBodyProperties(TextBody textBody, Point point, in ElementContext context)
        {
            var pointBodyProperties = point.TextBody?.GetFirstChild<BodyProperties>();

            if (textBody.GetFirstChild<BodyProperties>() is null && pointBodyProperties is null)
            {
                return;
            }

            var provider = new OpenXmlCompositeElementFlattenProvider<BodyProperties>
            (
                textBody.GetOrCreateElement<BodyProperties>(),
                pointBodyProperties
            );

            provider.FlattenMainElementProperty(bodyProperties => bodyProperties.PresetTextWrap);

            var elementBodyProperties = provider.Main;
            elementBodyProperties.Vertical = provider.GetFlattenProperty(bodyProperties => bodyProperties.Vertical);
            elementBodyProperties.Anchor = provider.GetFlattenProperty(bodyProperties => bodyProperties.Anchor);
            elementBodyProperties.AnchorCenter = provider.GetFlattenProperty(bodyProperties => bodyProperties.AnchorCenter);
            elementBodyProperties.BottomInset = provider.GetFlattenProperty(bodyProperties => bodyProperties.BottomInset);
            elementBodyProperties.ColumnCount = provider.GetFlattenProperty(bodyProperties => bodyProperties.ColumnCount);
            elementBodyProperties.ColumnSpacing = provider.GetFlattenProperty(bodyProperties => bodyProperties.ColumnSpacing);
            elementBodyProperties.CompatibleLineSpacing = provider.GetFlattenProperty(bodyProperties => bodyProperties.CompatibleLineSpacing);
            elementBodyProperties.ForceAntiAlias = provider.GetFlattenProperty(bodyProperties => bodyProperties.ForceAntiAlias);
            elementBodyProperties.FromWordArt = provider.GetFlattenProperty(bodyProperties => bodyProperties.FromWordArt);
            elementBodyProperties.HorizontalOverflow = provider.GetFlattenProperty(bodyProperties => bodyProperties.HorizontalOverflow);
            elementBodyProperties.LeftInset = provider.GetFlattenProperty(bodyProperties => bodyProperties.LeftInset);
            elementBodyProperties.PresetTextWrap = provider.GetFlattenProperty(bodyProperties => bodyProperties.PresetTextWrap);
            elementBodyProperties.RightInset = provider.GetFlattenProperty(bodyProperties => bodyProperties.RightInset);
            elementBodyProperties.RightToLeftColumns = provider.GetFlattenProperty(bodyProperties => bodyProperties.RightToLeftColumns);
            elementBodyProperties.Rotation = provider.GetFlattenProperty(bodyProperties => bodyProperties.Rotation);
            elementBodyProperties.TopInset = provider.GetFlattenProperty(bodyProperties => bodyProperties.TopInset);
            elementBodyProperties.UpRight = provider.GetFlattenProperty(bodyProperties => bodyProperties.UpRight);
            elementBodyProperties.UseParagraphSpacing = provider.GetFlattenProperty(bodyProperties => bodyProperties.UseParagraphSpacing);
            elementBodyProperties.Vertical = provider.GetFlattenProperty(bodyProperties => bodyProperties.Vertical);
            elementBodyProperties.VerticalOverflow = provider.GetFlattenProperty(bodyProperties => bodyProperties.VerticalOverflow);
            elementBodyProperties.Wrap = provider.GetFlattenProperty(bodyProperties => bodyProperties.Wrap);


        }

        private void FlattenParagraph(TextBody textBody, Point point, OpenXmlElement? textFillColor, in ElementContext context)
        {
            var pointTextBody = point.GetFirstChild<DocumentFormat.OpenXml.Drawing.Diagrams.TextBody>();
            if (pointTextBody is null)
            {
                return;
            }

            foreach (var paragraph in textBody.ChildElements.OfType<Paragraph>())
            {
                foreach (var pointParagraph in pointTextBody.ChildElements.OfType<Paragraph>())
                {
                    //假如InnerText相等，才去继承其样式
                    //todo 实际上point的paragraph有值，应该覆盖textBody的paragraph
                    if (!string.IsNullOrEmpty(paragraph.InnerText) && paragraph.InnerText.Equals(pointParagraph.InnerText, StringComparison.OrdinalIgnoreCase))
                    {
                        FlattenRun(paragraph, pointParagraph, textFillColor, context);
                    }
                }
            }
        }


        private void FlattenRun(Paragraph paragraph, Paragraph pointParagraph, OpenXmlElement? textFillColor, in ElementContext context)
        {
            foreach (var run in paragraph.ChildElements.OfType<Run>())
            {
                foreach (var pointRun in pointParagraph.ChildElements.OfType<Run>())
                {
                    if (string.IsNullOrEmpty(run.Text?.Text) || run.Text?.Text != pointRun.Text?.Text)
                    {
                        continue;
                    }
                    var provider = new OpenXmlCompositeElementFlattenProvider<Run>
                    (
                        run,
                        pointRun
                    );
                    provider.FlattenMainElementProperty(run1 => run1.RunProperties);

                    if (run.RunProperties is null || run.RunProperties.GetFirstChild<SolidFill>() is not null)
                    {
                        continue;
                    }

                    if (textFillColor is SchemeColor schemeColor)
                    {
                        var solidFill = run.RunProperties.GetOrCreateElement<SolidFill>();
                        var newSchemeColor = solidFill.GetOrCreateElement<SchemeColor>();
                        newSchemeColor.Val = schemeColor.Val;
                    }
                }
            }
        }
    }
}
