using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 像素
    /// </summary>
    public readonly struct Pixel : IEquatable<Pixel>
    {
        /// <summary>
        /// 像素
        /// </summary>
        /// <param name="value"></param>
        public Pixel(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 像素
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// 表示值是 0 的像素
        /// </summary>
        public static Pixel ZeroPixel => new Pixel(0);

        /// <inheritdoc />
        public override string ToString() => $"Pixel:{Value:0.00}";

        /// <inheritdoc />
        public bool Equals(Pixel other)
        {
            return Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Pixel other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}