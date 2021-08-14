using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 采用 <see cref="Emu"/> 表示的尺寸
    /// </summary>
    public readonly struct EmuSize:IEquatable<EmuSize>
    {
        /// <summary>
        /// 创建使用 EMU 单位表示的 Size 尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public EmuSize(Emu width, Emu height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public Emu Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public Emu Height { get; }

        /// <inheritdoc />
        public bool Equals(EmuSize other)
        {
            return Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is EmuSize other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
            }
        }
    }
}