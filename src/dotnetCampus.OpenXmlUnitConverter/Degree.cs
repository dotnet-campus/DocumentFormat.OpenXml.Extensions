using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 表示 OpenXml 中的角度数值，表示 0-360 度的角度值
    /// </summary>
    public sealed class Degree
    {
        /// <summary>
        /// 每个单位数值对应1/60000度
        /// </summary>
        /// <param name="value">范围 0-21600000 超过会被取模</param>
        public Degree(int value)
        {
            IntValue = value;
        }

        /// <summary>
        /// 在 OpenXml 表示的度数 int 值，范围 在 0-21600000 之间
        /// </summary>
        public int IntValue
        {
            get => _intValue;
            private set
            {
                var d = value % MaxDegreeValue;
                if (d < 0)
                {
                    d += MaxDegreeValue;
                }

                _intValue = d;
            }
        }

        /// <summary>
        /// 表示的度数的 double 值，是 OpenXml 的 <see cref="IntValue"/> 的 60000.0 分之一，也就是现实世界的度数值，范围是 0-360 度
        /// </summary>
        public double DoubleValue => IntValue / Precision;

        /// <summary>
        /// 270度
        /// </summary>
        public static readonly Degree Degree270 = Degree.FromDouble(270.0);

        /// <summary>
        /// 90度
        /// </summary>
        public static readonly Degree Degree90 = Degree.FromDouble(90);

        /// <summary>
        /// 180度
        /// </summary>
        public static readonly Degree Degree180 = Degree.FromDouble(180);

        private int _intValue;
        private const int MaxDegreeValue = 21600000;

        /// <summary>
        /// <see href="http://officeopenxml.com/drwSp-custGeom.php"/>
        /// </summary>
        private const double Precision = 60000.0;

        /// <summary>
        /// 表示的度数的弧度值，范围是 0-2 Math.PI 的值
        /// 
        /// </summary>
        public double ToRadiansValue() => DoubleValue / 180 * Math.PI;

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if ((obj == null)
                // 标记了 sealed 所以可以通过判断 type 即可，不需要使用 is 方法
                || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Degree p = (Degree)obj;
                return IntValue == p.IntValue;
            }
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return IntValue.GetHashCode();
        }

        /// <summary>
        /// 表示正数的角度
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Degree operator +(Degree a) => a;

        /// <summary>
        /// 表示负数的角度
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Degree operator -(Degree a) => new Degree(-a.IntValue);

        /// <summary>
        /// 两个角度的相加
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Degree operator +(Degree a, Degree b)
            => new Degree(a.IntValue + b.IntValue);

        /// <summary>
        /// 两个角度的相减
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Degree operator -(Degree a, Degree b)
            => a + (-b);

        /// <summary>
        /// 角度乘以倍数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Degree operator *(Degree a, double b)
            => new Degree(((int)(a.IntValue * b)));

        /// <summary>
        /// 倍数乘以角度，效果和角度乘以倍数相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Degree operator *(double a, Degree b)
            => new Degree(((int)(a * b.IntValue)));

        /// <summary>
        /// 角度除以倍数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Degree operator /(Degree a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }

            return new Degree((int)(a.IntValue / b));
        }

        /// <summary>
        /// 判断两个角度的大于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Degree a, Degree b)
            => a.IntValue > b.IntValue;

        /// <summary>
        /// 判断两个角度的小于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Degree a, Degree b)
            => b > a;

        /// <summary>
        /// 判断两个角度数值上是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Degree? a, Degree? b)
        {
            var aIsNull = ReferenceEquals(a, null);
            var bIsNull = ReferenceEquals(b, null);
            if (aIsNull && bIsNull)
            {
                return true;
            }

            if (aIsNull || bIsNull)
            {
                return false;
            }

            return a!.IntValue == b!.IntValue;
        }

        /// <summary>
        /// 判断两个角度数值上是否不相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Degree a, Degree b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 指定度数1单位数值代表1度，度数 <paramref name="value"/> 范围建议是 0-360 度
        /// </summary>
        /// <param name="value">范围建议是 0-360 度，超过了也不炸</param>
        /// <returns></returns>
        public static Degree FromDouble(double value)
        {
            int v = (int)(value * Precision);
            return new Degree(v);
        }
    }
}