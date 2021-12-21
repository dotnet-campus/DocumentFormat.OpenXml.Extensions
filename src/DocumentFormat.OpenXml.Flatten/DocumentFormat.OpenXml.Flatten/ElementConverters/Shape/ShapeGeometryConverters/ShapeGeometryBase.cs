using System.Text;

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Flatten.Contexts;
using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;

using dotnetCampus.OpenXmlUnitConverter;

using ElementEmuSize = dotnetCampus.OpenXmlUnitConverter.EmuSize;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    ///OpenXml Shape的抽象基类
    /// </summary>
    public abstract class ShapeGeometryBase
    {
        /// <summary>
        /// Shape 文本框
        /// </summary>
        public EmuShapeTextRectangle ShapeTextRectangle { get; private set; }

        /// <summary>
        /// Shape 单路径字符串
        /// </summary>
        /// <param name="emuSize">Emu的size</param>
        /// <param name="adjusts">调整点集合</param>
        /// <returns></returns>
        public abstract string? ToGeometryPathString(ElementEmuSize emuSize, AdjustValueList? adjusts = null);

        /// <summary>
        /// Shape 多路径
        /// </summary>
        /// <param name="emuSize">Emu的size</param>
        /// <param name="adjusts">调整点集合</param>
        /// <returns></returns>
        public virtual ShapePath[]? GetMultiShapePaths(ElementEmuSize emuSize, AdjustValueList? adjusts = null)
        {
            return default;
        }

        /// <summary>
        /// 初始化形状中文本框
        /// </summary>
        /// <param name="emuLeft">Emu单位的l</param>
        /// <param name="emuTop">Emu单位的t</param>
        /// <param name="emuRight">Emu单位的r</param>
        /// <param name="emuBottom">Emu单位的b</param>
        /// 可参考Ecma-376- Ecma Office Open XML Part 1 - Fundamentals And Markup Language Reference 20.1.9.22 以及presetShapeDefinitions.xml
        protected void InitializeShapeTextRectangle(double emuLeft, double emuTop, double emuRight, double emuBottom)
        {
            var emuWidth = new Emu(emuRight - emuLeft);
            var emuHeight = new Emu(emuBottom - emuTop);
            var originPoint = new EmuPoint(emuLeft, emuTop);
            ShapeTextRectangle = new EmuShapeTextRectangle(new EmuSize(emuWidth, emuHeight), originPoint);
        }

        /// <summary>
        ///     获取ppt形状计算公式的属性值
        /// </summary>
        /// <returns></returns>
        /// 可参考文档 dotnet OpenXML SDK 形状几何 Geometry 的计算公式含义.md
        public static (double h, double w, double l, double r, double t, double b,
            double hd2, double hd4, double hd5, double hd6, double hd8,
            double ss, double hc, double vc, double ls, double ss2,
            double ss4, double ss6, double ss8, double wd2, double wd4,
            double wd5, double wd6, double wd8, double wd10, double cd2,
            double cd4, double cd6, double cd8) GetFormulaProperties(ElementEmuSize emuSize)
        {
            var width = emuSize.Width.Value;
            var height = emuSize.Height.Value;
            var h = height;
            var w = width;
            var l = 0d;
            var r = width;
            var t = 0d;
            var b = height;
            var hd2 = height / 2;
            var hd4 = height / 4;
            var hd5 = height / 5;
            var hd6 = height / 6;
            var hd8 = height / 8;
            var ss = System.Math.Min(height, width);
            var hc = width / 2;
            var vc = height / 2;
            var ls = System.Math.Max(height, width);
            var ss2 = ss / 2;
            var ss4 = ss / 4;
            var ss6 = ss / 6;
            var ss8 = ss / 8;
            var wd2 = width / 2;
            var wd4 = width / 4;
            var wd5 = width / 5;
            var wd6 = width / 6;
            var wd8 = width / 8;
            var wd10 = width / 10;
            // 1°= 60000.0 Degree   c = circle = 360°= 60000.0 * 360 = 21600000 Degree
            const double c = 21600000d;
            const double cd2 = c / 2;
            const double cd4 = c / 4;
            const double cd6 = c / 6;
            const double cd8 = c / 8;
            return (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6,
                wd8, wd10, cd2, cd4, cd6, cd8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringPath">路径字符串</param>
        /// <param name="currentPoint">起始点坐标</param>
        /// <param name="widthRadiusEmu">Emu的圆弧X轴半径</param>
        /// <param name="heightRadiusEmu">Emu的圆弧Y轴半径</param>
        /// <param name="startAngleDegree">圆弧的起始角度</param>
        /// <param name="swingAngleDegree">圆弧的摆动角度</param>
        /// <returns></returns>
        protected EmuPoint ArcToToString(StringBuilder stringPath, EmuPoint currentPoint,
            double widthRadiusEmu,
            double heightRadiusEmu,
            double startAngleDegree, double swingAngleDegree)
        {
            return ShapeGeometryFormulaHelper.ArcToToString(stringPath, currentPoint, new Emu(widthRadiusEmu),
                new Emu(heightRadiusEmu), new Angle((int) startAngleDegree), new Angle((int) swingAngleDegree), UnitPrecision);
        }

        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        /// <param name="stringPath">路径字符串</param>
        /// <param name="x1Emu">第一个控制点的Emu单位的X坐标</param>
        /// <param name="y1Emu">第一个控制点的Emu单位的Y坐标</param>
        /// <param name="x2Emu">第二个控制点的Emu单位的X坐标</param>
        /// <param name="y2Emu">第二个控制点的Emu单位的Y坐标</param>
        /// <param name="xEmu">目标点的Emu单位的X坐标</param>
        /// <param name="yEmu">目标点的Emu单位的Y坐标</param>
        /// <returns></returns>
        protected EmuPoint CubicBezToString(StringBuilder stringPath, double x1Emu, double y1Emu, double x2Emu, double y2Emu, double xEmu, double yEmu)
        {
            return ShapeGeometryFormulaHelper.CubicBezToString(stringPath, new Emu(x1Emu), new Emu(y1Emu), new Emu(x2Emu),
                new Emu(y2Emu), new Emu(xEmu), new Emu(yEmu), UnitPrecision);
        }

        /// <summary>
        /// 二次贝塞尔曲线
        /// </summary>
        /// <param name="stringPath">路径字符串</param>
        /// <param name="x1Emu">第一个控制点的Emu单位的X坐标</param>
        /// <param name="y1Emu">第一个控制点的Emu单位的Y坐标</param>
        /// <param name="xEmu">目标点的Emu单位的X坐标</param>
        /// <param name="yEmu">目标点的Emu单位的Y坐标</param>
        /// <returns></returns>
        protected EmuPoint QuadBezToString(StringBuilder stringPath, double x1Emu, double y1Emu, double xEmu, double yEmu)
        {
            return ShapeGeometryFormulaHelper.QuadBezToString(stringPath, new Emu(x1Emu), new Emu(y1Emu), new Emu(xEmu), new Emu(yEmu), UnitPrecision);
        }

        /// <summary>
        /// 线条坐标信息转字符串
        /// </summary> 
        /// <param name="stringPath">路径字符串</param>
        /// <param name="xEmu">eum单位的x坐标</param>
        /// <param name="yEmu">eum单位的y坐标</param>
        /// <returns>目标点坐标</returns>
        protected EmuPoint LineToToString(StringBuilder stringPath, double xEmu, double yEmu)
        {
            return ShapeGeometryFormulaHelper.LineToToString(stringPath, new Emu(xEmu), new Emu(yEmu), UnitPrecision);
        }

        /// <summary>
        /// Eum转Pixel字符串
        /// </summary>
        /// <param name="emu"><see cref="T:dotnetCampus.OpenXmlUnitConverter.Emu" />信息</param>
        /// <returns></returns>
        protected string EmuToPixelString(Emu emu)
        {
            return ShapeGeometryFormulaHelper.EmuToPixelString(emu, UnitPrecision);
        }

        /// <summary>
        /// 单位精度
        /// </summary>
        protected int UnitPrecision { get; set; } = 3;
    }
}
