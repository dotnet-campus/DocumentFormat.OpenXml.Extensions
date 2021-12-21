using System.Collections.Generic;
using System.Linq;
using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;
using DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators;
using DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters;
using DocumentFormat.OpenXml.Flatten.Utils;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.CustomGeometryConverters
{
    /// <summary>
    /// 自定义形状转换器
    /// </summary>
    /// 特殊行为：
    /// 在 PPT 里面，非闭合的形状，也就是不带Z但是首尾点相近的路径，将会被作为闭合图形进行填充
    /// 但是在 WPF 里面，需要明显写入 Z 才会作为闭合图形
    /// 为了适配，因此在首尾相连的路径，自动加上 Z 关闭
    /// 详细请参阅 `自由形状转为了自由线条.pptx` 文档，和内部BUG-26155内容
    class CustomGeometryConverter
    {
        public CustomGeometryConverter(CustomGeometry customGeometry, ElementEmuSize emuSize)
        {
            _customGeometry = customGeometry;
            ShapeGeometryFormulaCalculator = new ShapeGeometryFormulaCalculator(emuSize);
            _elementEmuSize = emuSize;
        }

        /// <summary>
        /// 转换自定义形状
        /// </summary>
        /// <returns></returns>
        public SvgPath? Convert()
        {
            ConvertShapeGuideList();
            ConvertShapeTextRectangle();
            return ConvertPathList();
        }

        private readonly CustomGeometry _customGeometry;
        private readonly ElementEmuSize _elementEmuSize;
        private ShapeGeometryFormulaCalculator ShapeGeometryFormulaCalculator { get; }
        private double ScaleX { set; get; }
        private double ScaleY { set; get; }

        private EmuShapeTextRectangle ShapeTextRectangle { set; get; }

        private void ConvertShapeTextRectangle()
        {
            var rectangle = _customGeometry.Rectangle;
            if (rectangle is null)
            {
                return;
            }

            var left = ShapeGeometryFormulaCalculator.GetEmuValue(rectangle.Left?.Value);
            var top = ShapeGeometryFormulaCalculator.GetEmuValue(rectangle.Top?.Value);
            var right = ShapeGeometryFormulaCalculator.GetEmuValue(rectangle.Right?.Value);
            var bottom = ShapeGeometryFormulaCalculator.GetEmuValue(rectangle.Bottom?.Value);

            ShapeTextRectangle = new EmuShapeTextRectangle(left, top, right, bottom);
        }

        private void ConvertShapeGuideList()
        {
            var shapeGuideList = _customGeometry.ShapeGuideList;
            if (shapeGuideList != null)
            {
                foreach (var shapeGuide in shapeGuideList.Elements().OfType<ShapeGuide>())
                {
                    var name = shapeGuide.Name?.Value;
                    var formula = shapeGuide.Formula?.Value;

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(formula))
                    {
                        ShapeGeometryFormulaCalculator.Calculate(name!, formula!);
                    }
                }
            }
        }

        private SvgPath? ConvertPathList()
        {
            var pathList = _customGeometry.PathList;
            if (pathList == null)
            {
                return default;
            }

            var svgPathList = new List<ShapePath>();
            var stringPath = new StringBuilder(128);
            foreach (var path in pathList.Elements().OfType<Path>())
            {
                ConvertPathScale(path);

                EmuPoint currentPixelPoint = default;
                EmuPoint? firstPathPoint = default;
                foreach (var pathData in path.ChildElements)
                {
                    var lastPixelPoint =
                        ConvertToPathString(pathData, stringPath, currentPixelPoint);
                    if (pathData is MoveTo)
                    {
                        TryClosePath();

                        firstPathPoint = lastPixelPoint;
                    }

                    currentPixelPoint = lastPixelPoint;
                }

                TryClosePath();

                void TryClosePath()
                {
                    if (firstPathPoint is null
                        // 如果已关闭，那么不需要再次关闭
                        || stringPath[stringPath.Length - 1] == CloseShapePathDefineKey)
                    {
                        return;
                    }

                    // 证明有上一段的存在，判断是否最后的点和首点相同，如果相同，那么加上 z 用于修复 `自由形状转为了自由线条.pptx` 此文档的坑
                    // 如果当前没有加上 Z 的话，那么加上结束
                    if (MathHelper.NearlyEquals(currentPixelPoint, firstPathPoint.Value))
                    {
                        stringPath.Append(CloseShapePathDefineKey);
                    }
                }

                svgPathList.Add(new ShapePath(stringPath.ToString()));
                stringPath.Clear();
            }

            if (svgPathList.Count == 1)
            {
                return new SvgPath(null, svgPathList.First().Path, ShapeTextRectangle, default);
            }
            else if (svgPathList.Count > 1)
            {
                return new SvgPath(null, svgPathList.First().Path, ShapeTextRectangle, svgPathList.ToArray());
            }
            else
            {
                return null;
            }
        }

        private void ConvertPathScale(Path path)
        {
            ScaleX = 1;
            ScaleY = 1;

            var width = path.Width?.Value;
            if (width != null)
            {
                ScaleX = _elementEmuSize.Width.Value / width.Value;
            }

            var height = path.Height?.Value;
            if (height != null)
            {
                ScaleY = _elementEmuSize.Height.Value / height.Value;
            }
        }

        private EmuPoint ConvertToPathString(OpenXmlElement pathData, StringBuilder stringPath,
            EmuPoint currentPoint)
        {
            switch (pathData)
            {
                case MoveTo moveTo:
                    {
                        // 关于定义的 Key 的值请百度参考 svg 规范
                        var defineKey = "M";
                        var moveToPoint = moveTo.Point;
                        if (moveToPoint?.X is not null && moveToPoint.Y is not null)
                        {
                            stringPath.Append(defineKey);
                            var emuPoint = PointToEmuPoint(moveToPoint);
                            var point = ToPixelPoint(emuPoint);
                            PointToString(point);
                            return emuPoint;
                        }

                        break;
                    }
                case LineTo lineTo:
                    {
                        var defineKey = "L";

                        var lineToPoint = lineTo.Point;
                        if (lineToPoint?.X is not null && lineToPoint.Y is not null)
                        {
                            stringPath.Append(defineKey);
                            var emuPoint = PointToEmuPoint(lineToPoint);
                            var point = ToPixelPoint(emuPoint);
                            PointToString(point);
                            return emuPoint;
                        }

                        break;
                    }
                case ArcTo arcTo:
                    {
                        Angle swingAngle = DegreeStringToAngle(arcTo.SwingAngle);
                        Angle startAngle = DegreeStringToAngle(arcTo.StartAngle);

                        var widthRadius = EmuStringToEmu(arcTo.WidthRadius);
                        var heightRadius = EmuStringToEmu(arcTo.HeightRadius);
                        widthRadius = new Emu(widthRadius.Value * ScaleX);
                        heightRadius = new Emu(heightRadius.Value * ScaleY);

                        return ShapeGeometryFormulaHelper.ArcToToString(stringPath, currentPoint, widthRadius, heightRadius, startAngle,
                            swingAngle);
                    }
                case QuadraticBezierCurveTo quadraticBezierCurveTo:
                    {
                        var defineKey = "Q";

                        return ConvertPointList(quadraticBezierCurveTo, defineKey, stringPath);
                    }
                case CubicBezierCurveTo cubicBezierCurveTo:
                    {
                        var defineKey = "C";

                        return ConvertPointList(cubicBezierCurveTo, defineKey, stringPath);
                    }
                // ReSharper disable once UnusedVariable
                case CloseShapePath closeShapePath:
                    {
                        var defineKey = CloseShapePathDefineKey;
                        stringPath.Append(defineKey);
                        break;
                    }
            }

            return default;

            void PointToString(PixelPoint point)
            {
                PixelPointToString(point, stringPath);
            }
        }

        private const string Comma = ",";
        private const char CloseShapePathDefineKey = 'Z';

        private EmuPoint ConvertPointList(OpenXmlCompositeElement element, string defineKey,
            StringBuilder stringPath)
        {
            var isFirstPoint = true;
            EmuPoint lastPoint = default;
            foreach (var point in element.Elements<Point>())
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

                var emuPoint = PointToEmuPoint(point);
                var pixelPoint = ToPixelPoint(emuPoint);
                PixelPointToString(pixelPoint, stringPath);
                lastPoint = emuPoint;
            }

            return lastPoint;
        }


        private static PixelPoint ToPixelPoint(EmuPoint emuPoint)
        {
            return new(emuPoint.X.ToPixel(), emuPoint.Y.ToPixel());
        }

        private EmuPoint PointToEmuPoint(Point? point)
        {
            var x = EmuStringToEmu(point?.X);
            var y = EmuStringToEmu(point?.Y);

            x = new Emu(x.Value * ScaleX);
            y = new Emu(y.Value * ScaleY);

            return new EmuPoint(x, y);
        }

        private Emu EmuStringToEmu(StringValue? emuString)
        {
            if (emuString is null)
            {
                return default;
            }

            var emuText = emuString.Value;

            if (string.IsNullOrEmpty(emuText))
            {
                return default;
            }

            var emuValue = ShapeGeometryFormulaCalculator.Calculate(string.Empty, emuText);

            var emu = new Emu(emuValue);
            return emu;
        }

        private Angle DegreeStringToAngle(StringValue? degreeString)
        {
            var degreeText = degreeString?.Value;

            if (!string.IsNullOrEmpty(degreeText))
            {
                var degree = ShapeGeometryFormulaCalculator.Calculate(string.Empty, degreeText);

                return Angle.FromOpenXmlDegree(degree);
            }

            return default;
        }

        private static void PixelPointToString(PixelPoint point, StringBuilder stringPath)
        {
            stringPath.Append(PixelToString(point.X))
                .Append(Comma)
                .Append(PixelToString(point.Y));
        }

        private static string PixelToString(Pixel x)
        {
            // 太小了很看不到形状，丢失精度，这里的值都是采用形状的大小进行填充，所以参数都是相对大小就可以
            return (x.Value * 1.000).ToString("0.000");
        }
    }
}
