using System;
using System.Globalization;
using System.Text;

using DocumentFormat.OpenXml.Flatten.Contexts;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters
{
    /// <summary>
    /// OpenXml几何形状公式帮助类
    /// </summary>
    public static class ShapeGeometryFormulaHelper
    {
        /// <summary>
        ///     OpenXml的Pin函数
        /// </summary>
        /// <returns></returns>
        public static double Pin(double x, double y, double z)
        {
            if (y < x)
            {
                return x;
            }

            if (y > z)
            {
                return z;
            }

            return y;
        }

        /// <summary>
        ///     OpenXml 三角函数的Sin函数：sin x y = (x * sin( y )) = (x * Math.Sin(y))
        /// </summary>
        /// <param name="x">ppt的数值</param>
        /// <param name="y">ppt表示角度的值</param>
        /// <returns></returns>
        /// 而形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double Sin(double x, int y)
        {
            var angle = GetAngle(y);
            return x * System.Math.Sin(angle);
        }

        /// <summary>
        ///     OpenXml 三角函数的Cos函数：cos x y = (x * cos( y )) = (x * Math.Cos(y))
        /// </summary>
        /// <param name="x">ppt的数值</param>
        /// <param name="y">ppt表示角度的值</param>
        /// <returns></returns>
        /// 而形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double Cos(double x, int y)
        {
            var angle = GetAngle(y);
            return x * System.Math.Cos(angle);
        }

        /// <summary>
        ///  OpenXml ATan2函数：at2 x y = arctan(y / x) 
        /// </summary>
        /// <param name="x">笛卡尔平面的x坐标</param>
        /// <param name="y">笛卡尔平面的y坐标</param>
        /// <returns>Emu单位的角度值</returns>
        /// 而形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double ATan2(double x, double y)
        {
            var radians = System.Math.Atan2(y, x);
            var angle = radians * 180 / System.Math.PI;
            return angle * 60000;
        }

        /// <summary>
        /// OpenXml Tan函数：tan x y = (x * tan( y )) = (x * Tan(y))
        /// </summary>
        /// <param name="x">笛卡尔平面的x坐标</param>
        /// <param name="y">笛卡尔平面的y坐标</param>
        /// <returns>Emu单位的角度值</returns>
        /// 形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double Tan(double x, int y)
        {
            var angle = GetAngle(y);
            return x * System.Math.Tan(angle);
        }

        /// <summary>
        ///  OpenXml Cat2函数：cat2 x y z = x * Cos(Math.Atan2(z, y))
        /// </summary>
        /// <param name="x">ppt的数值</param>
        /// <param name="y">笛卡尔平面的x坐标</param>
        /// <param name="z">笛卡尔平面的y坐标</param>
        /// <returns>Emu单位的角度值</returns>
        /// 而形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double Cat2(double x, double y, double z)
        {
            var angle = ATan2(y, z);
            var result = Cos(x, (int) angle);
            return result;
        }

        /// <summary>
        ///  OpenXml Sat2函数：sat2 x y z = x * Sin(Math.Atan2(z, y))
        /// </summary>
        /// <param name="x">ppt的数值</param>
        /// <param name="y">笛卡尔平面的x坐标</param>
        /// <param name="z">笛卡尔平面的y坐标</param>
        /// <returns>Emu单位的角度值</returns>
        /// 而形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double Sat2(double x, double y, double z)
        {
            var angle = ATan2(y, z);
            var result = Sin(x, (int) angle);
            return result;
        }

        /// <summary>
        ///  OpenXml Mod函数：mod x y z = sqrt(x^2 + b^2 + c^2) = Math.Sqrt(x * x + y * y + z * z)
        /// </summary>
        /// <param name="x">ppt的数值</param>
        /// <param name="y">笛卡尔平面的x坐标</param>
        /// <param name="z">笛卡尔平面的y坐标</param>
        /// <returns>Emu单位的值</returns>
        /// 形状的计算符号定义在 ECMA 376 的 20.1.9.11 章文档
        public static double Mod(double x, double y, double z)
        {
            return System.Math.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        ///     ppt的值转为角度
        /// </summary>
        /// <param name="value">ppt表示角度的值</param>
        /// <returns></returns>
        /// 参考 [Office Open XML 的测量单位](https://blog.lindexi.com/post/Office-Open-XML-%E7%9A%84%E6%B5%8B%E9%87%8F%E5%8D%95%E4%BD%8D.html)
        private static double GetAngle(int value)
        {
            return Angle.FromOpenXmlDegree(value).ToRadiansValue();
        }

        /// <summary>
        /// OpenXml的Abs函数：abs x=Math.Abs(x)
        /// </summary>
        /// <param name="eumValue"></param>
        /// <returns></returns>
        public static double Abs(double eumValue)
        {
            return System.Math.Abs(eumValue);
        }

        private const string Comma = ",";

        /// <summary>
        /// 传入 Arc 的参数，转换为路径内容
        /// </summary>
        /// <param name="stringPath"></param>
        /// <param name="currentPoint"></param>
        /// <param name="widthRadius"></param>
        /// <param name="heightRadius"></param>
        /// <param name="startAngleString"></param>
        /// <param name="swingAngleString"></param>
        /// <param name="unitPrecision"></param>
        /// <returns></returns>
        public static EmuPoint ArcToToString(StringBuilder stringPath, EmuPoint currentPoint,
            Emu widthRadius,
            Emu heightRadius,
            Angle startAngleString, Angle swingAngleString, int unitPrecision = 3)
        {
            const string comma = Comma;

            var stAng = startAngleString.ToRadiansValue();
            var swAng = swingAngleString.ToRadiansValue();

            var wR = widthRadius.Value;
            var hR = heightRadius.Value;

            //修复当椭圆弧线进行360°时，起始点和终点一样，会导致弧线变成点，因此-1°才进行计算
            if (System.Math.Abs(swAng) == 2 * System.Math.PI)
            {
                swAng = swAng - swAng / 360;
            }

            var p1 = GetEllipsePoint(wR, hR, stAng);
            var p2 = GetEllipsePoint(wR, hR, stAng + swAng);
            var pt = new EmuPoint(currentPoint.X.Value - p1.X.Value + p2.X.Value,
                currentPoint.Y.Value - p1.Y.Value + p2.Y.Value);

            var isLargeArcFlag = System.Math.Abs(swAng) >= System.Math.PI;
            var isClockwise = swAng > 0;
            currentPoint = pt;

            // 格式如下
            // A rx ry x-axis-rotation large-arc-flag sweep-flag x y
            // 这里 large-arc-flag 是 1 和 0 表示
            stringPath.Append("A")
                .Append(EmuToPixelString(wR, unitPrecision)) //rx
                .Append(comma)
                .Append(EmuToPixelString(hR, unitPrecision)) //ry
                .Append(comma)
                .Append("0") // x-axis-rotation
                .Append(comma)
                .Append(isLargeArcFlag ? "1" : "0") //large-arc-flag
                .Append(comma)
                .Append(isClockwise ? "1" : "0") // sweep-flag
                .Append(comma)
                .Append(EmuToPixelString(pt.X, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(pt.Y, unitPrecision))
                .Append(' ');
            return currentPoint;
        }

        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        /// <param name="stringPath">路径字符串</param>
        /// <param name="x1">第一个控制点的X坐标</param>
        /// <param name="y1">第一个控制点的Y坐标</param>
        /// <param name="x2">第二个控制点的X坐标</param>
        /// <param name="y2">第二个控制点的Y坐标</param>
        /// <param name="x">目标点的X坐标</param>
        /// <param name="y">目标点的Y坐标</param>
        /// <param name="unitPrecision">转Pixel的精度</param>
        /// <returns></returns>
        public static EmuPoint CubicBezToString(StringBuilder stringPath, Emu x1, Emu y1, Emu x2, Emu y2, Emu x, Emu y, int unitPrecision = 3)
        {
            const string comma = Comma;

            stringPath.Append("C")
                .Append(EmuToPixelString(x1, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(y1, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(x2, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(y2, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(x, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(y, unitPrecision))
                .Append(' ');
            return new EmuPoint(x, y);
        }

        /// <summary>
        /// 二次贝塞尔曲线
        /// </summary>
        /// <param name="stringPath">路径字符串</param>
        /// <param name="x1">第一个控制点的X坐标</param>
        /// <param name="y1">第一个控制点的Y坐标</param>
        /// <param name="x">目标点的X坐标</param>
        /// <param name="y">目标点的Y坐标</param>
        /// <param name="unitPrecision">转Pixel的精度</param>
        /// <returns></returns>
        public static EmuPoint QuadBezToString(StringBuilder stringPath, Emu x1, Emu y1, Emu x, Emu y, int unitPrecision = 3)
        {
            const string comma = Comma;

            stringPath.Append("Q")
                .Append(EmuToPixelString(x1, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(y1, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(x, unitPrecision))
                .Append(comma)
                .Append(EmuToPixelString(y, unitPrecision))
                .Append(' ');
            return new EmuPoint(x, y);
        }

        private static EmuPoint GetEllipsePoint(double a, double b, double theta)
        {
            //修复当wR或者hR为0时，a * bCosTheta / circleRadius和b * aSinTheta / circleRadius 结果为NaN的问题
            if (NearlyEquals(a, 0) || NearlyEquals(b, 0))
            {
                return new EmuPoint(0, 0);
            }

            var aSinTheta = a * System.Math.Sin(theta);
            var bCosTheta = b * System.Math.Cos(theta);
            var circleRadius = System.Math.Sqrt((aSinTheta * aSinTheta) + (bCosTheta * bCosTheta));
            return new EmuPoint(a * bCosTheta / circleRadius, b * aSinTheta / circleRadius);
        }

        private static bool NearlyEquals(double expected, double actual)
        {
            return System.Math.Abs(expected - actual) <= 0.001;
        }

        /// <summary>
        /// 线条坐标信息转字符串
        /// </summary> 
        /// <param name="stringPath">路径字符串</param>
        /// <param name="x">eum单位的x坐标</param>
        /// <param name="y">eum单位的y坐标</param>
        /// <param name="unitPrecision">转Pixel的精度</param>
        /// <returns></returns>
        public static EmuPoint LineToToString(StringBuilder stringPath, Emu x, Emu y, int unitPrecision = 3)
        {
            stringPath.Append($"L {EmuToPixelString(x, unitPrecision)},{EmuToPixelString(y, unitPrecision)} ");

            return new EmuPoint(x, y);
        }

        private static string EmuToPixelString(double emuValue, int unitPrecision = 3)
        {
            var emu = new Emu(emuValue);
            return EmuToPixelString(emu, unitPrecision);
        }

        /// <summary>
        /// Eum转Pixel字符串
        /// </summary>
        /// <param name="emu"><see cref="T:dotnetCampus.OpenXmlUnitConverter.Emu" />信息</param>
        /// <param name="unitPrecision">单位精度</param>
        /// <returns></returns>
        public static string EmuToPixelString(Emu emu, int unitPrecision = 3)
        {
            return PixelToString(emu.ToPixel(), unitPrecision);
        }

        private static string PixelToString(Pixel x, int unitPrecision = 3)
        {
            // 太小了很看不到形状，丢失精度，这里的值都是采用形状的大小进行填充，所以参数都是相对大小就可以
            return (x.Value * 1.000).ToString($"F{unitPrecision}", CultureInfo.InvariantCulture);
        }
    }
}
