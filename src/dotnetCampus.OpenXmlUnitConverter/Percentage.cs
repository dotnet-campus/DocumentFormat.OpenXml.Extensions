using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    ///     表示一个百分比数值
    /// </summary>
    public class Percentage
    {
        /// <summary>
        ///     将一个OpenXml表示的百分比int值转换
        ///     每1000个单位代表1%
        ///     <param name="value"></param>
        /// </summary>
        public Percentage(int value)
        {
            IntValue = value;
        }

        /// <summary>
        /// 从一个 OpenXML 的数值转换为百分比
        /// </summary>
        /// <param name="percentageText"></param>
        internal Percentage(string percentageText)
        {
            if (int.TryParse(percentageText, out var intValue))
            {
                IntValue = intValue;
                return;
            }
            else
            {
                // 如果是带了百分比的
                if (percentageText.Length > 1 && percentageText.EndsWith("%"))
                {
                    var newPercentageText = percentageText.Substring(0, percentageText.Length - 1);
                    if (double.TryParse(newPercentageText, out var doubleValue))
                    {
                        IntValue = (int) Math.Round(doubleValue * 1000);

                        return;
                    }
                }
            }

            throw new ArgumentException(
                $"Can not convert PercentageText={percentageText} to {nameof(Percentage)} value.");
        }

        /// <summary>
        /// 百分比与 OpenXML 比例
        /// </summary>
        public const double Precision = 100000.0;

        /// <summary>
        ///     表示 0 的值
        /// </summary>
        public static readonly Percentage Zero = new Percentage(0);

        /// <summary>
        ///     OpenXml表示的百分比int值
        /// </summary>
        public int IntValue { get; }

        /// <summary>
        ///     OpenXml表示的百分比double值
        ///     0-1
        /// </summary>
        public double DoubleValue => IntValue / Precision;

        /// <summary>
        ///     将从一个double数值构建OpenXml表示的百分比
        ///     每0.01个double数值代表1%
        ///     会丢失精度
        ///     <param name="value"></param>
        /// </summary>
        public static Percentage FromDouble(double value)
        {
            var v = (int) (value * Precision);
            return new Percentage(v);
        }

        /// <summary>
        ///     获取在指定范围内的double值
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double DoubleValueWithRange(double min, double max)
        {
            if (min > max)
                throw new InvalidOperationException($"{nameof(max)}:{max} must greater than {nameof(min)}:{min}");

            var value = IntValue / Precision;
            value = value > max ? max : value;
            value = value < min ? min : value;
            return value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (ReferenceEquals(obj, this)) return true;

            if (GetType() != obj.GetType()) return false;

            var p = (Percentage) obj;
            return IntValue == p.IntValue;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return IntValue.GetHashCode();
        }

        /// <summary>
        ///     表示正号
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Percentage operator +(Percentage a)
        {
            return a;
        }

        /// <summary>
        ///     表示负数
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Percentage operator -(Percentage a)
        {
            return new Percentage(-a.IntValue);
        }

        /// <summary>
        ///     相加两个值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a.IntValue + b.IntValue);
        }

        /// <summary>
        ///     相减两个值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Percentage operator -(Percentage a, Percentage b)
        {
            return a + -b;
        }

        /// <summary>
        ///     两个值相乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Percentage operator *(Percentage a, Percentage b)
        {
            return new Percentage(a.IntValue * b.IntValue);
        }

        /// <summary>
        ///     两个值相除
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Percentage operator /(Percentage a, Percentage b)
        {
            if (b.IntValue == 0) throw new DivideByZeroException();

            return new Percentage(a.IntValue / b.IntValue);
        }

        /// <summary>
        ///     大于判断
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Percentage a, Percentage b)
        {
            return a.IntValue > b.IntValue;
        }

        /// <summary>
        ///     小于判断
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Percentage a, Percentage b)
        {
            return b > a;
        }
    }
}