using System;
using DocumentFormat.OpenXml;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 角度的表示，角度有两个表示方法，一个是 0-360 度的 Degree 角度，另一个是 0-2π 范围的 Radians 弧度表示。默认 OpenXML 里面采用 60000 倍的 Degree 角度数值
    /// </summary>
    public readonly struct Angle : IEquatable<Angle>
    {
        /// <summary>
        /// 创建角度
        /// </summary>
        /// <param name="openXmlDegree">在 OpenXML 表示的度数的 double 值</param>
        public Angle(Int32Value openXmlDegree)
        {
            if (openXmlDegree is null) throw new ArgumentNullException(nameof(openXmlDegree));

            Degree = openXmlDegree.Value / Precision;
        }

        private Angle(double degree) => Degree = degree;

        /// <summary>
        /// 270度
        /// </summary>
        public static Angle Degree270 => FromDegreeValue(270.0);

        /// <summary>
        /// 90度
        /// </summary>
        public static Angle Degree90 => FromDegreeValue(90);

        /// <summary>
        /// 180度
        /// </summary>
        public static Angle Degree180 => FromDegreeValue(180);

        /// <summary>
        /// 转换为 0-360 度的角度
        /// </summary>
        /// <returns></returns>
        public double ToDegreeValue() => Degree;

        /// <summary>
        /// 转换为 OpenXML 的角度单位，值是 60000 倍的 Degree 角度数值，表示 60000 倍 0-360 度
        /// </summary>
        /// <returns></returns>
        public double ToOpenXmlDegree() => Degree * Precision;

        /// <summary>
        /// 表示的度数的弧度值，范围是 0-2 Math.PI 的值
        /// </summary>
        public double ToRadiansValue() => Degree / 180 * Math.PI;

        /// <summary>
        /// 指定度数1单位数值代表1度，度数 <paramref name="value"/> 范围建议是 0-360 度
        /// </summary>
        /// <param name="value">范围建议是 0-360 度，超过了也不炸</param>
        /// <returns></returns>
        public static Angle FromDegreeValue(double value) => new Angle(value);

        /// <summary>
        /// 从 OpenXML 的角度单位进行创建，传入的参数是 OpenXML 的角度单位，值是 0-360 度角度的 60000 倍
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Angle FromOpenXmlDegree(double value) => new Angle(value / Precision);

        /// <summary>
        /// 采用范围是 0-2π 范围的值创建角度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Angle FromRadiansValue(double value) => new Angle(value / Math.PI * 180);

        /// <summary>
        /// 角度，表示 0-360 度的角度。实际可以超过 360 度
        /// </summary>
        private double Degree { get; }

        /// <summary>
        /// 在 OpenXML 表示的度数的 double 值，是 OpenXml 的 <see cref="Int32Value"/> 的 60000.0 分之一，也就是现实世界的度数值，范围是 0-360 度
        /// </summary>
        private const double Precision = 60000.0;

        /// <inheritdoc />
        public bool Equals(Angle other)
        {
            return Degree.Equals(other.Degree);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Angle other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Degree.GetHashCode();
        }

        /// <summary>
        /// 表示正数的角度
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Angle operator +(Angle a) => a;

        /// <summary>
        /// 表示负数的角度
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Angle operator -(Angle a) => Angle.FromDegreeValue(-a.Degree);

        /// <summary>
        /// 两个角度的相加
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator +(Angle a, Angle b)
            => Angle.FromDegreeValue(a.Degree + b.Degree);

        /// <summary>
        /// 两个角度的相减
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator -(Angle a, Angle b)
            => a + (-b);

        /// <summary>
        /// 角度乘以倍数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator *(Angle a, double b)
            => Angle.FromDegreeValue(a.Degree * b);

        /// <summary>
        /// 倍数乘以角度，效果和角度乘以倍数相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator *(double a, Angle b) => b * a;

        /// <summary>
        /// 角度除以倍数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator /(Angle a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }

            return Angle.FromDegreeValue(a.Degree / b);
        }

        /// <summary>
        /// 判断两个角度的大于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Angle a, Angle b)
            => a.Degree > b.Degree;

        /// <summary>
        /// 判断两个角度的小于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Angle a, Angle b)
            => b > a;

        /// <summary>
        /// 判断两个角度相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Angle a, Angle b) => a.Equals(b);

        /// <summary>
        /// 判断两个角度不相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Angle a, Angle b) => !a.Equals(b);

        /// <inheritdoc />
        public override string ToString() => $"{Degree:0.000}°";
    }
}