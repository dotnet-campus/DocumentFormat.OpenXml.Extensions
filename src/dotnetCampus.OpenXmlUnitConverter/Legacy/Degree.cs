using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Degree
    {
        public static readonly Degree Degree270 = Degree.FromDouble(270.0);

        public static readonly Degree Degree90 = Degree.FromDouble(90);

        public static readonly Degree Degree180 = Degree.FromDouble(180);


        private int _intValue;
        private const int MaxDegreeValue = 21600000;
        private const double Precision = 60000.0;

        public Degree(int value)
        {
            IntValue = value;
        }

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

        public double DoubleValue => IntValue / Precision;

        public double ToRadiansValue() => DoubleValue / 180 * Math.PI;


        public override bool Equals(object obj)
        {
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Degree p = (Degree)obj;
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
            => new Degree(((int)(a.IntValue * b)));

        public static Degree operator *(double a, Degree b)
            => new Degree(((int)(a * b.IntValue)));

        public static Degree operator /(Degree a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }

            return new Degree((int)(a.IntValue / b));
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

        public static Degree FromDouble(double value)
        {
            int v = (int)(value * Precision);
            return new Degree(v);
        }

        public static implicit operator dotnetCampus.OpenXmlUnitConverter.Degree(Degree legacyUnit)
        {
            return new dotnetCampus.OpenXmlUnitConverter.Degree(legacyUnit.IntValue);
        }

        public static implicit operator Degree(dotnetCampus.OpenXmlUnitConverter.Degree newUnit)
        {
            return new Degree(newUnit.IntValue);
        }
    }
}
