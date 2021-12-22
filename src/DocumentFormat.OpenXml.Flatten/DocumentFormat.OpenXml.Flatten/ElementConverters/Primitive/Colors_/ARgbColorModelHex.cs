using DocumentFormat.OpenXml.Drawing;

using dotnetCampus.OpenXmlUnitConverter;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive
{
    /// <summary>
    /// 用 ARGB 表示的颜色，支持和 <see cref="RgbColorModelHex"/> 进行转换
    /// </summary>
    public class ARgbColorModelHex : RgbColorModelHex
    {
        /// <summary>
        /// 用 ARGB 表示的颜色
        /// </summary>
        /// <param name="color"></param>
        public ARgbColorModelHex(in ARgbColor color)
        {
            var hexString = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            Val = new HexBinaryValue(hexString);
            if (color.A != 0xFF)
            {
                var percentage = new Percentage(color.A * 100000 / 0xFF);
                Alpha alpha = new Alpha
                {
                    Val = percentage.IntValue
                };
                AddChild(alpha);
            }

            Color = color;
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public ARgbColor Color { get; }

        /// <inheritdoc />
        public override string ToString() => Color.ToString();

        /// <summary>
        /// 在 OpenXML SDK 里面判断的是对象的类型，因此如果给 SolidFill 设置 RgbColorModelHex 属性为 ARgbColorModelHex 对象，那么将会在 SolidFill 的 RgbColorModelHex 拿到的是空值
        /// </summary>
        /// <returns></returns>
        public RgbColorModelHex ToRgbColorModelHex()
        {
            return (RgbColorModelHex) Clone();
        }
    }
}
