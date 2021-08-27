using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 采用 <see cref="Emu"/> 表示的点
    /// </summary>
    public readonly struct EmuPoint : IEquatable<EmuPoint>
    {
        /// <summary>
        /// 创建 <see cref="Emu"/> 表示的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public EmuPoint(Emu x, Emu y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 创建 <see cref="Emu"/> 表示的点
        /// </summary>
        /// <param name="xEmu">用EMU单位的 X 坐标</param>
        /// <param name="yEmu">用EMU单位的 Y 坐标</param>
        public EmuPoint(double xEmu, double yEmu) : this(new Emu(xEmu), new Emu(yEmu))
        {
        }

        /// <summary>
        /// 表示 X 坐标
        /// </summary>
        public Emu X { get; }

        /// <summary>
        /// 表示 Y 坐标
        /// </summary>
        public Emu Y { get; }

        /// <inheritdoc />
        public bool Equals(EmuPoint other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is EmuPoint other && Equals(other);
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
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(EmuPoint left, EmuPoint right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 判断不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(EmuPoint left, EmuPoint right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override string ToString() => $"X={X} ;Y={Y}";
    }
}