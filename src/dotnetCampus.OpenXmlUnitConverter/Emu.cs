using System;

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 英制公制单位（English Metric Unit）。用于对接厘米（<see cref="Cm"/>）和英寸（<see cref="Inch"/>）的虚拟单位。
    /// <para>其特殊的数值设计，便于让你在转换百位以内的英寸和毫米、像素长度时，不会产生小数。</para>
    /// <para>1 <see cref="Inch"/> = 914400 <see cref="Emu"/></para>
    /// </summary>
    public readonly struct Emu : IEquatable<Emu>
    {
        /// <summary>
        /// 创建 <see cref="Emu"/> 单位。
        /// </summary>
        /// <param name="value"><see cref="Emu"/> 单位数值。</param>
        public Emu(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取 <see cref="Emu"/> 单位数值。
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// 表示已初始化为零的 <see cref="Emu"/> 长度。
        /// </summary>
        public static Emu Zero => new Emu(0);

        /// <inheritdoc />
        public override string ToString() => $"EMU: {Value:0.00}";

        /// <inheritdoc />
        public bool Equals(Emu other)
        {
            return Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Emu other && Equals(other);
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
        public static bool operator ==(Emu left, Emu right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 判断不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Emu left, Emu right)
        {
            return !left.Equals(right);
        }
    }
}