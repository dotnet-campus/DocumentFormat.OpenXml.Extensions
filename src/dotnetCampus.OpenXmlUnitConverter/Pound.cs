using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 磅
    /// </summary>
    public readonly struct Pound : IEquatable<Pound>
    {
        /// <summary>
        /// 磅
        /// </summary>
        /// <param name="value"></param>
        public Pound(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 磅
        /// </summary>
        public double Value { get; }

        /// <inheritdoc />
        public override string ToString() => $"Pound:{Value:0.00}";

        /// <inheritdoc />
        public bool Equals(Pound other)
        {
            return Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Pound other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// 判断相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Pound left, Pound right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 判断不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Pound left, Pound right)
        {
            return !left.Equals(right);
        }
    }
}