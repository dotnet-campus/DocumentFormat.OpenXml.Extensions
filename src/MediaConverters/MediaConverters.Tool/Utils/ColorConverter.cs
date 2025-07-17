using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Utils;

static class ColorConverter
{
    /// <summary>
    /// 将16进制的颜色字符串转为<see cref="Rgba32"/>
    /// </summary>
    /// <param name="hexColorText">颜色格式是 AARRGGBB</param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool TryConvertToColor(string hexColorText, out Rgba32 color)
    {
        color = new Rgba32();

        bool startWithPoundSign = hexColorText.StartsWith('#');
        var colorStringLength = hexColorText.Length;
        if (startWithPoundSign) colorStringLength -= 1;
        int currentOffset = startWithPoundSign ? 1 : 0;
        // 可以采用的格式如下
        // #FFDFD991   8 个字符 存在 Alpha 通道
        // #DFD991     6 个字符
        // #FD92       4 个字符 存在 Alpha 通道
        // #DAC        3 个字符
        if (colorStringLength == 8
            || colorStringLength == 6
            || colorStringLength == 4
            || colorStringLength == 3)
        {
            bool success;
            byte result;
            byte a;

            int readCount;
            // #DFD991     6 个字符
            // #FFDFD991   8 个字符 存在 Alpha 通道
            //if (colorStringLength == 8 || colorStringLength == 6)
            if (colorStringLength > 5)
            {
                readCount = 2;
            }
            else
            {
                readCount = 1;
            }

            bool includeAlphaChannel = colorStringLength == 8 || colorStringLength == 4;

            if (includeAlphaChannel)
            {
                (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
                if (!success) return false;
                a = result;
                currentOffset += readCount;
            }
            else
            {
                a = 0xFF;
            }

            (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
            if (!success) return false;
            byte r = result;
            currentOffset += readCount;

            (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
            if (!success) return false;
            byte g = result;
            currentOffset += readCount;

            (success, result) = HexCharToNumber(hexColorText, currentOffset, readCount);
            if (!success) return false;
            byte b = result;

            color = new Rgba32(r, g, b, a);
            return true;
        }

        return false;
    }

    static (bool success, byte result) HexCharToNumber(string input, int offset, int readCount)
    {
        Debug.Assert(readCount == 1 || readCount == 2, "要求 readCount 只能是 1 或者 2 的值，这是框架限制，因此不做判断");

        byte result = 0;

        for (int i = 0; i < readCount; i++, offset++)
        {
            var c = input[offset];
            byte n;
            if (c >= '0' && c <= '9')
            {
                n = (byte) (c - '0');
            }
            else if (c >= 'a' && c <= 'f')
            {
                n = (byte) (c - 'a' + 10);
            }
            else if (c >= 'A' && c <= 'F')
            {
                n = (byte) (c - 'A' + 10);
            }
            else
            {
                return default;
            }

            result *= 16;
            result += n;
        }

        if (readCount == 1)
        {
            result = (byte) (result * 16 + result);
        }

        return (true, result);
    }
}