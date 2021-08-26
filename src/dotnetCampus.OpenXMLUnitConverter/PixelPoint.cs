using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 采用 <see cref="Pixel"/> 表示的点
    /// </summary>
    public readonly struct PixelPoint : IEquatable<PixelPoint>
    {
        /// <summary>
        /// 创建 <see cref="Pixel"/> 表示的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PixelPoint(Pixel x, Pixel y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 表示 X 坐标
        /// </summary>
        public Pixel X { get; }

        /// <summary>
        /// 表示 Y 坐标
        /// </summary>
        public Pixel Y { get; }

        /// <inheritdoc />
        public override string ToString() => $"X={X} ;Y={Y}";

        /// <inheritdoc />
        public bool Equals(PixelPoint other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is PixelPoint other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        /// <summary>
        /// 判断相等
        /// </summary>
        public static bool operator ==(PixelPoint left, PixelPoint right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 判断不相等
        /// </summary>
        public static bool operator !=(PixelPoint left, PixelPoint right)
        {
            return !left.Equals(right);
        }
    }
}