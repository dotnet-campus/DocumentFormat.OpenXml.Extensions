using System;

namespace dotnetCampus.OpenXMLUnitConverter
{
    /// <summary>
    /// 表示一个百分比数值
    /// </summary>
    public class Percentage
    {
        private const double Precision = 100000.0;

        /// <summary>
        /// 将一个openxml表示的百分比int值转换
        /// 每1000个单位代表1%
        /// <param name="value"></param>
        /// </summary>
        public Percentage(int value)
        {
            IntValue = value;
        }

        /// <summary>
        /// 将从一个double数值构建openxml表示的百分比
        /// 每0.01个double数值代表1%
        /// 会丢失精度
        /// <param name="value"></param>
        /// </summary>
        public static Percentage FromDouble(double value)
        {
            int v = (int) (value * Precision);
            return new Percentage(v);
        }

        /// <summary>
        /// openxml表示的百分比int值
        /// </summary>
        public int IntValue { get; }

        /// <summary>
        /// openxml表示的百分比double值
        /// 0-1
        /// </summary>
        public double DoubleValue => IntValue / Precision;

        /// <summary>
        /// 获取在指定范围内的double值
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double DoubleValueWithRange(double min, double max)
        {
            if (min > max)
            {
                throw new InvalidOperationException($"{nameof(max)}:{max} must greater than {nameof(min)}:{min}");
            }

            var value = IntValue / Precision;
            value = value > max ? max : value;
            value = value < min ? min : value;
            return value;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Percentage p = (Percentage) obj;
                return IntValue == p.IntValue;
            }
        }

        public override int GetHashCode()
        {
            return this.IntValue.GetHashCode();
        }

        public static Percentage operator +(Percentage a) => a;
        public static Percentage operator -(Percentage a) => new Percentage(-a.IntValue);

        public static Percentage operator +(Percentage a, Percentage b)
            => new Percentage(a.IntValue + b.IntValue);

        public static Percentage operator -(Percentage a, Percentage b)
            => a + (-b);

        public static Percentage operator *(Percentage a, Percentage b)
            => new Percentage(a.IntValue * b.IntValue);

        public static Percentage operator /(Percentage a, Percentage b)
        {
            if (b.IntValue == 0)
            {
                throw new DivideByZeroException();
            }

            return new Percentage(a.IntValue / b.IntValue);
        }

        public static bool operator >(Percentage a, Percentage b)
        {
            return a.IntValue > b.IntValue;
        }

        public static bool operator <(Percentage a, Percentage b)
        {
            return b > a;
        }

        public static implicit operator dotnetCampus.OpenXmlUnitConverter.Percentage(Percentage legacyUnit)
        {
            return new dotnetCampus.OpenXmlUnitConverter.Percentage(legacyUnit.IntValue);
        }
    }
}