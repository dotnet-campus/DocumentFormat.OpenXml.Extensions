using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.VariantTypes;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators
{
    /// <summary>
    /// 预设几何计算类
    /// </summary>
    public static class PresetGeometryCalculator
    {
        /// <summary>
        /// 通过给定的预设 XML 内容和元素尺寸，计算出几何
        /// </summary>
        /// <returns></returns>
        public static ShapePath[] Calculate(string xml, ElementEmuSize elementEmuSize)
        {
            var xDocument = XDocument.Parse(xml);

            return Calculate(xDocument, elementEmuSize);
        }

        /// <summary>
        /// 通过给定的预设 XML 内容和元素尺寸，计算出几何
        /// </summary>
        /// <returns></returns>
        public static ShapePath[] Calculate(XDocument xDocument, ElementEmuSize elementEmuSize)
        {
            var xElement = xDocument.Root;
            return Calculate(xElement, elementEmuSize);
        }

        /// <summary>
        /// 通过给定的预设 XML 内容和元素尺寸，计算出几何
        /// </summary>
        public static ShapePath[] Calculate(XElement? xElement, ElementEmuSize elementEmuSize)
        {
            if (xElement is null)
            {
                return new ShapePath[0];
            }

            var calculator = new ShapeGeometryFormulaCalculator(elementEmuSize);

            var avLstElement = xElement.Element(XName.Get("avLst", DrawingMLNameSpace));
            if (avLstElement != null)
            {
                CalculateShapeGuideList(avLstElement, calculator);
            }

            var gdLstElement = xElement.Element(XName.Get("gdLst", DrawingMLNameSpace));
            if (gdLstElement != null)
            {
                CalculateShapeGuideList(gdLstElement, calculator);
            }

            var pathLstElement = xElement.Element(XName.Get("pathLst", DrawingMLNameSpace));
            if (pathLstElement == null)
            {
                return new ShapePath[0];
            }

            var shapePathList = new List<ShapePath>();

            foreach (var pathElement in pathLstElement.Elements(XName.Get("path", DrawingMLNameSpace)))
            {
                var shapePath = CalculateShapePath(pathElement, calculator);

                shapePathList.Add(shapePath);
            }

            return shapePathList.ToArray();
        }

        // ReSharper disable once InconsistentNaming
        private const string DrawingMLNameSpace = "http://schemas.openxmlformats.org/drawingml/2006/main";
        private const string Comma = ",";

        private static ShapePath CalculateShapePath(XElement pathElement, ShapeGeometryFormulaCalculator calculator)
        {
            CalculatorContext context = GetCalculatorContext(pathElement, calculator);

            var stringPath = new StringBuilder();
            EmuPoint currentPoint = default;
            foreach (var xElement in pathElement.Elements())
            {
                switch (xElement.Name.LocalName)
                {
                    case "moveTo":
                        {
                            var ptElement = xElement.Element(XName.Get("pt", DrawingMLNameSpace));
                            if (ptElement == null)
                            {
                                continue;
                            }

                            currentPoint = PointElementToEmuPoint(ptElement, calculator, context);

                            stringPath.Append(
                                $"M {EmuToPixelString(currentPoint.X)},{EmuToPixelString(currentPoint.Y)} ");

                            break;
                        }
                    case "arcTo":
                        {
                            var wRName = xElement.Attribute("wR")?.Value;
                            var hRName = xElement.Attribute("hR")?.Value;
                            var stAngName = xElement.Attribute("stAng")?.Value;
                            var swAngName = xElement.Attribute("swAng")?.Value;

                            // 既然缩放不对，那就不缩放
                            //// 这里的缩放应该是不对的
                            //var wR = new Emu(calculator.GetEmuValue(wRName).Value * context.WidthFactor);
                            //var hR = new Emu(calculator.GetEmuValue(hRName).Value * context.HeightFactor);
                            var wR = calculator.GetEmuValue(wRName);
                            var hR = calculator.GetEmuValue(hRName);
                            var stAng = calculator.GetEmuValue(stAngName);
                            var swAng = calculator.GetEmuValue(swAngName);
                            currentPoint = ArcToToString(stringPath, currentPoint, wR, hR,
                                Angle.FromOpenXmlDegree(stAng.Value), Angle.FromOpenXmlDegree(swAng.Value));
                            break;
                        }
                    case "lnTo":
                        {
                            var ptElement = xElement.Element(XName.Get("pt", DrawingMLNameSpace));
                            if (ptElement == null)
                            {
                                continue;
                            }

                            var point = PointElementToEmuPoint(ptElement, calculator, context);

                            currentPoint = LineToToString(stringPath, point.X, point.Y);

                            break;
                        }
                    case "quadBezTo":
                        {
                            /*
                                             <quadBezTo>
                                               <pt x="cx1" y="cy1" />
                                               <pt x="x3" y="y1" />
                                             </quadBezTo>
                                         */

                            currentPoint = ConvertPointList(xElement, "Q", stringPath, calculator, context);
                            break;
                        }
                    case "cubicBezTo":
                        {
                            /*
                                               <cubicBezTo>
                                                  <pt x="10800" y="17322" />
                                                  <pt x="10800" y="23922" />
                                                  <pt x="0" y="20172" />
                                                </cubicBezTo>
                                         */
                            currentPoint = ConvertPointList(xElement, "C", stringPath, calculator, context);
                            break;
                        }
                    case "close":
                        {
                            stringPath.Append("z ");
                            break;
                        }
                }
            }

            var shapePathSize = GetShapePathSize(pathElement, calculator);
            PathFillModeValues fillMode = GetPathFillModeValues(pathElement);
            var shapePath = new ShapePath(stringPath.ToString(), fillMode,
                emuWidth: shapePathSize.Width.Value,
                emuHeight: shapePathSize.Height.Value);
            return shapePath;
        }

        /// <summary>
        /// 获取路径的填充颜色模式
        /// </summary>
        /// <param name="pathElement"></param>
        /// <returns></returns>
        private static PathFillModeValues GetPathFillModeValues(XElement pathElement)
        {
            var fillValue = pathElement.Attribute("fill")?.Value;
            return fillValue switch
            {
                // 不做填充
                "none" => PathFillModeValues.None,
                // 没有特效的填充，原来是啥就填充啥
                "norm" => PathFillModeValues.Norm,
                // 亮的填充特效
                "lighten" => PathFillModeValues.Lighten,
                // 弱亮的填充特效
                "lightenLess" => PathFillModeValues.LightenLess,
                // 暗的填充特效
                "darken" => PathFillModeValues.Darken,
                // 弱暗的填充特效
                "darkenLess" => PathFillModeValues.DarkenLess,
                _ => PathFillModeValues.Norm
            };

            // 填充特效相当于在形状上面加上颜色蒙层，对应的蒙层值如下
            /*
               case FillMode.None:
                   return null;
               case FillMode.Darken:
                   return BrushCreator.GetOrCreate("#64000000");
               case FillMode.DarkenLess:
                   return BrushCreator.GetOrCreate("#32000000");
               case FillMode.Lighten:
                   return BrushCreator.GetOrCreate("#64FFFFFF");
               case FillMode.LightenLess:
                   return BrushCreator.GetOrCreate("#32FFFFFF");
             */
        }

        private static EmuSize GetShapePathSize(XElement pathElement,
            ShapeGeometryFormulaCalculator calculator)
        {
            // 在 Path 里，将会给定 w 和 h 表示路径绘制的尺寸
            // 也就是说 Path 绘制出来的内容大小和形状元素的大小无关，而是采用被固定的画布大小进行绘制，需要形状根据大小和画布大小（画布大小是 Path 的 w 和 h 属性）进行缩放
            var wAttribute = pathElement.Attribute("w");
            var hAttribute = pathElement.Attribute("h");

            Emu? width = null;
            Emu? height = null;

            if (wAttribute is not null)
            {
                if (double.TryParse(wAttribute.Value, out var widthEmuValue))
                {
                    var w = calculator.GetEmuValue("w");
                    width = w;
                }
            }

            if (hAttribute is not null)
            {
                if (double.TryParse(hAttribute.Value, out var heightEmuValue))
                {
                    var h = calculator.GetEmuValue("h");
                    height = h;
                }
            }

            if (width is not null && height is not null)
            {
                return new EmuSize(width.Value, height.Value);
            }
            else if (width is null || height is null)
            {
                // 我还没见到这么离谱
                return default;
            }
            else
            {
                return default;
            }
        }

        private static CalculatorContext GetCalculatorContext(XElement pathElement,
            ShapeGeometryFormulaCalculator calculator)
        {
            return new CalculatorContext();
        }


        private static void CalculateShapeGuideList(XElement element, ShapeGeometryFormulaCalculator calculator)
        {
            foreach (var xElement in element.Elements().Where(e => e.Name.LocalName == "gd"))
            {
                var name = xElement.Attribute("name")?.Value;
                var fmla = xElement.Attribute("fmla")?.Value;

                if (!string.IsNullOrEmpty(name))
                {
                    calculator.Calculate(name!, fmla);
                }
            }
        }

        private static EmuPoint ConvertPointList(XElement element, string defineKey,
            StringBuilder stringPath, ShapeGeometryFormulaCalculator calculator, CalculatorContext context)
        {
            var isFirstPoint = true;
            EmuPoint lastPoint = default;

            foreach (var point in element.Elements().Where(e => e.Name.LocalName == "pt"))
            {
                if (isFirstPoint)
                {
                    isFirstPoint = false;
                    stringPath.Append(defineKey);
                }
                else
                {
                    // 同类型的点之间用空格分开
                    stringPath.Append(" ");
                }

                var emuPoint = PointElementToEmuPoint(point, calculator, context);
                var pixelPoint = ToPixelPoint(emuPoint);
                PixelPointToString(pixelPoint, stringPath);
                lastPoint = emuPoint;
            }

            return lastPoint;
        }

        private static PixelPoint ToPixelPoint(EmuPoint emuPoint)
        {
            return new PixelPoint(emuPoint.X.ToPixel(), emuPoint.Y.ToPixel());
        }

        private static void PixelPointToString(PixelPoint point, StringBuilder stringPath)
        {
            stringPath.Append(PixelToString(point.X))
                .Append(Comma)
                .Append(PixelToString(point.Y));
        }

        private static string PixelToString(Pixel x)
        {
            return x.Value.ToString("0.00000");

            //// 太小了很看不到形状，丢失精度，这里的值都是采用形状的大小进行填充，所以参数都是相对大小就可以
            //return (x.Value * 1.000).ToString("0.000");
        }

        private static EmuPoint PointElementToEmuPoint(XElement element, ShapeGeometryFormulaCalculator calculator,
            CalculatorContext context)
        {
            var xName = element.Attribute("x")?.Value;
            var yName = element.Attribute("y")?.Value;

            var x = calculator.GetEmuValue(xName).Value /* * context.WidthFactor */;
            var y = calculator.GetEmuValue(yName).Value /* * context.HeightFactor */;
            return new EmuPoint(x, y);
        }

        private readonly struct CalculatorContext
        {
            //public CalculatorContext(double widthFactor, double heightFactor)
            //{
            //    WidthFactor = widthFactor;
            //    HeightFactor = heightFactor;
            //}

            // 不再修改比例进行缩放，让渲染时进行缩放
            //public double WidthFactor { get; }
            //public double HeightFactor { get; }
        }
    }
}
