﻿using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Percentage
    {
        private const double Precision = 100000.0;

        public Percentage(int value)
        {
            IntValue = value;
        }

        /// <summary>
        /// 从一个 OpenXML 的数值转换为百分比
        /// </summary>
        /// <param name="percentageText"></param>
        public Percentage(string percentageText)
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
                        IntValue = (int)Math.Round(doubleValue * 1000);

                        return;
                    }
                }
            }

            throw new ArgumentException(
                $"Can not convert PercentageText={percentageText} to {nameof(OpenXmlUnitConverter.Percentage)} value.");
        }

        public static Percentage FromDouble(double value)
        {
            int v = (int)(value * Precision);
            return new Percentage(v);
        }

        public int IntValue { get; }

        public double DoubleValue => IntValue / Precision;

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
                Percentage p = (Percentage)obj;
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

        public static implicit operator Percentage(dotnetCampus.OpenXmlUnitConverter.Percentage newUnit)
        {
            return new Percentage(newUnit.IntValue);
        }
    }
}