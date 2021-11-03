using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 像素尺寸
    /// </summary>
    public readonly struct PixelSize : IEquatable<PixelSize>
    {
        /// <summary>
        /// 像素尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PixelSize(Pixel width, Pixel height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public Pixel Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public Pixel Height { get; }

        /// <inheritdoc />
        public bool Equals(PixelSize other)
        {
            return Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is PixelSize other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
            }
        }

        /// <summary>
        /// 判断相等
        /// </summary>
        public static bool operator ==(PixelSize left, PixelSize right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 判断不相等
        /// </summary>
        public static bool operator !=(PixelSize left, PixelSize right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override string ToString() => $"W={Width} ;H={Height}";
    }
}