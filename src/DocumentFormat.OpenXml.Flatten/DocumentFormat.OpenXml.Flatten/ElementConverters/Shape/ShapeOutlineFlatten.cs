using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive;
using DocumentFormat.OpenXml.Flatten.Framework;
using DocumentFormat.OpenXml.Flatten.Framework.Context;
using DocumentFormat.OpenXml.Flatten.Utils;

using Shape = DocumentFormat.OpenXml.Presentation.Shape;
using ShapeProperties = DocumentFormat.OpenXml.Presentation.ShapeProperties;
using ShapeStyle = DocumentFormat.OpenXml.Presentation.ShapeStyle;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters
{
    class ShapeOutlineFlatten
    {
        public ShapeOutlineFlatten(Shape element, ElementContext context)
        {
            element.ShapeProperties ??= new ShapeProperties();
            ShapeProperties shapeProperties = element.ShapeProperties;

            ShapeStyle? shapeStyle = element.ShapeStyle;

            var themeOutline = GetThemeLineProperty(shapeStyle, context.SlideContext);

            // Outline = a:ln
            Outline? shapeOutline;
            if (themeOutline is null)
            {
                shapeOutline = shapeProperties.GetFirstChild<Outline>();
            }
            else
            {
                shapeOutline = shapeProperties.GetOrCreateElement<Outline>();
            }

            if (shapeOutline is not null)
            {
                Provider = new ShapeOutlineFlattenProvider(shapeOutline, themeOutline, shapeStyle?.LineReference, context.SlideContext);
            }
            else
            {
                // 如果形状没有定义 Outline 而且也没有主题样式，那么就不要给此形状加上边框 Outline 的值
            }
        }

        private ShapeOutlineFlattenProvider? Provider { get; }

        public void Convert()
        {
            Provider?.Convert();
        }

        private Outline? GetThemeLineProperty(ShapeStyle? shapeStyle, SlideContext context)
        {
            var formatScheme = context.RootElement.GetFormatScheme();
            var lineStyleList = formatScheme?.LineStyleList;
            if (lineStyleList != null)
            {
                /*
                       <a:lnRef idx="1">
                         <a:scrgbClr r="1" g="1" b="1">
                           <a:tint val="1"/>
                         </a:scrgbClr>
                       </a:lnRef>
                */

                var lineReferenceIndex = shapeStyle?.LineReference?.Index?.Value ?? 0;

                // 文档规定，Index是从1开始的
                // https://docs.microsoft.com/en-za/dotnet/api/documentformat.openxml.drawing.linereference?view=openxml-2.8.1
                //  The @idx attribute refers the index of a line style within the <fillStyleLst> element.
                // http://c-rex.net/projects/samples/ooxml/e1/Part4/OOXML_P4_DOCX_lnRef_topic_ID0EDHGKB.html
                // http://www.datypic.com/sc/ooxml/e-a_lnRef-1.html

                if (lineReferenceIndex > 0)
                {
                    /*
                        <lnStyleLst>  
                          <ln w="9525" cap="flat" cmpd="sng" algn="ctr">  
                            <solidFill>  
                              <schemeClr val="phClr">  
                                <shade val="50000"/>  
                                <satMod val="103000"/>  
                              </schemeClr>  
                            </solidFill>  
                            <prstDash val="solid"/>  
                          </ln>  
                          <ln w="25400" cap="flat" cmpd="sng" algn="ctr">  
                            <solidFill>  
                              <schemeClr val="phClr"/>  
                            </solidFill>  
                            <prstDash val="solid"/>  
                          </ln>  
                        </lnStyleLst>  
                    */
                    // 样式定义请看上面代码，在样式定义里面包含了很多 ln 也就是 Outline 的值
                    // 通过 ChildElements 就可以获取，根据文档，这里面的值全部都是 Outline 类
                    // https://docs.microsoft.com/en-us/dotnet/api/documentformat.openxml.drawing.linestylelist?view=openxml-2.8.1
                    var index = (int) (lineReferenceIndex - 1);
                    if (lineStyleList.ChildElements.Count > index)
                        // 根据上面文档，这里的值不需要做判断
                        return (Outline) lineStyleList.ChildElements[index];
                }
            }

            return null;
        }

        class ShapeOutlineFlattenProvider : OpenXmlCompositeElementFlattenProvider<Outline>
        {
            public ShapeOutlineFlattenProvider(Outline? shapeOutline, Outline? themeOutline,
                LineReference? lineReference, SlideContext context) : base(shapeOutline, themeOutline)
            {
                _themeOutline = themeOutline;
                _lineReference = lineReference;
                _context = context;
            }

            public void Convert()
            {
                // 线条宽度
                Main.Width = GetFlattenProperty(e => e!.Width);

                // 线条是虚线还是什么线
                var presetDash = GetOrCreateElement<PresetDash>();
                presetDash.Val = GetFlattenProperty(e => e.GetFirstChild<PresetDash>()?.Val);

                Main.Alignment = GetFlattenProperty(e => e.Alignment);

                Main.CapType = GetFlattenProperty(e => e.CapType);

                Main.CompoundLineType = GetFlattenProperty(e => e.CompoundLineType);

                var headEnd = GetOrCreateElement<HeadEnd>();
                headEnd.Type = GetFlattenProperty(e => e.GetFirstChild<HeadEnd>()?.Type);

                var tailEnd = GetOrCreateElement<TailEnd>();
                tailEnd.Type = GetFlattenProperty(e => e.GetFirstChild<TailEnd>()?.Type);

                FlattenBrush();
            }

            private void FlattenBrush()
            {
                // 颜色，颜色属于画刷，画刷需要重新开策略
                // 测试 `形状立体几何默认边框颜色丢失.pptx` 文档

                var shapeBrushFill = BrushHelper.GetFlattenFill(Main, _context);
                if (shapeBrushFill != null)
                {
                    BrushHelper.ReplaceBrushFill(Main, shapeBrushFill);
                }
                else
                {
                    // 需要从样式获取颜色
                    // 先尝试获取 LineReference 的颜色
                    ARgbColor? placeHolderColor = ColorHelper.BuildColor(_lineReference, _context);
                    var brushFill = BrushHelper.GetFlattenFill(_themeOutline, _context, null, placeHolderColor);
                    if (brushFill != null)
                    {
                        BrushHelper.ReplaceBrushFill(Main, brushFill);
                    }
                }
            }

            private readonly Outline? _themeOutline;
            private readonly LineReference? _lineReference;
            private readonly SlideContext _context;
        }
    }
}
