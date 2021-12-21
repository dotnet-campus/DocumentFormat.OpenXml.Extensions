using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.Primitive
{
    /// <summary>
    /// 用 A R G B 表示的颜色
    /// </summary>
    public class ARgbColor
    {
        /// <summary>
        /// 创建 A R G B 颜色
        /// </summary>
        public ARgbColor()
        {
        }

        /// <summary>
        /// 创建 A R G B 颜色
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public ARgbColor(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// 表示透明色
        /// </summary>
        public byte A { set; get; }

        /// <summary>
        /// 表示红色
        /// </summary>
        public byte R { set; get; }

        /// <summary>
        /// 表示绿色
        /// </summary>
        public byte G { set; get; }

        /// <summary>
        /// 表示蓝色
        /// </summary>
        public byte B { set; get; }

        /// <summary>
        /// 转换为 <see cref="RgbColorModelHex"/> 类型
        /// </summary>
        /// <returns></returns>
        public ARgbColorModelHex ToRgbColorModelHex()
        {
            var rgbColorModelHex = new ARgbColorModelHex(this);
            return rgbColorModelHex;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{A:X2}{R:X2}{G:X2}{B:X2}";
        }
    }
}
