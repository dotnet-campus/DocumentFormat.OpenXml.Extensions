using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 表示openxml中的角度数值
    /// </summary>
    public class Degree
    {
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
        /// <see cref="http://officeopenxml.com/drwSp-custGeom.php"/>
        /// </summary>
        private const double Precision = 60000.0;

        /// <summary>
        /// 每个单位数值对应1/60000度
        /// </summary>
        /// <param name="value">范围 0-21600000,超过会被取模</param>
        public Degree(int value)
        {
            IntValue = value;
        }

        /// <summary>
        /// openxml表示的度数int值
        /// 在0-21600000之间
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
        /// openxml表示的度数的double值
        /// 0-360
        /// </summary>
        public double DoubleValue => IntValue / Precision;

        /// <summary>
        /// openxml表示的度数的弧度值
        /// 0-2pi
        /// </summary>
        public double ToRadiansValue() => DoubleValue / 180 * Math.PI;


        public override bool Equals(object obj)
        {
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Degree p = (Degree) obj;
                return IntValue == p.IntValue;
            }
        }

        public override int GetHashCode()
        {
            return this.IntValue.GetHashCode();
        }

        public static Degree operator +(Degree a) => a;
        public static Degree operator -(Degree a) => new Degree(-a.IntValue);

        public static Degree operator +(Degree a, Degree b)
            => new Degree(a.IntValue + b.IntValue);

        public static Degree operator -(Degree a, Degree b)
            => a + (-b);

        public static Degree operator *(Degree a, double b)
            => new Degree(((int) (a.IntValue * b)));

        public static Degree operator *(double a, Degree b)
            => new Degree(((int) (a * b.IntValue)));

        public static Degree operator /(Degree a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }

            return new Degree((int) (a.IntValue / b));
        }

        public static bool operator >(Degree a, Degree b)
            => a.IntValue > b.IntValue;

        public static bool operator <(Degree a, Degree b)
            => b > a;

        public static bool operator ==(Degree a, Degree b)
        {
            return a.IntValue == b.IntValue;
        }

        public static bool operator !=(Degree a, Degree b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 指定度数1单位数值代表1度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Degree FromDouble(double value)
        {
            int v = (int) (value * Precision);
            return new Degree(v);
        }
    }
}