using System;
using System.ComponentModel;
using DocumentFormat.OpenXml;

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
        ///     更推荐使用 <see cref="Percentage(Int32Value)"/> 构造函数，解决百分比内容包含百分号
        /// </summary>
        /// <param name="value"></param>
        public Percentage(int value)
        {
            IntValue = value;
        }

        /// <summary>
        /// 从一个 OpenXML 的数值转换为百分比
        /// </summary>
        /// [dotnet OpenXML 修复 Office 文档里面的百分比内容包含百分号](https://blog.lindexi.com/post/dotnet-OpenXML-%E4%BF%AE%E5%A4%8D-Office-%E6%96%87%E6%A1%A3%E9%87%8C%E9%9D%A2%E7%9A%84%E7%99%BE%E5%88%86%E6%AF%94%E5%86%85%E5%AE%B9%E5%8C%85%E5%90%AB%E7%99%BE%E5%88%86%E5%8F%B7.html )

        public Percentage(Int32Value int32Value) : this(int32Value.InnerText)
        {
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