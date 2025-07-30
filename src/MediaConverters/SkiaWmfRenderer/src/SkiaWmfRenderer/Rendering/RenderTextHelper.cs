using System.Runtime.InteropServices;

using HarfBuzzSharp;

using SkiaSharp;

using Buffer = HarfBuzzSharp.Buffer;

namespace SkiaWmfRenderer.Rendering;

static class RenderTextHelper
{
    public static void DrawText(this SKCanvas canvas, string text, float x, float y, WmfRenderStatus renderStatus)
    {
        using var buffer = new Buffer();

        buffer.AddUtf16(text);
        buffer.GuessSegmentProperties();

        var font = renderStatus.HarfBuzzFont;

        font.Shape(buffer);

        // 经过实验测试，发现这里的 GetGlyphInfoSpan 方法返回的 Codepoint 就是 GlyphIndex 的值
        // 如 edd88c25d3b92b231df2ed5c7fc0ce9297b98d96 的实验，只要在 HarfBuzzSharp.Font 的 Shape 方法之后，那么获取的 Codepoint 就是 GlyphIndex 的值
        // 如其控制台输出的如下内容：
        // Typeface=Standard Symbols PS GlyphCount=191
        // ContainsGlyph('p')=True 81
        // TryGetGlyph=True 81
        // (int) 'p' == 112
        // Before HarfBuzzSharp.Font.Shape Codepoint=112
        // After HarfBuzzSharp.Font.Shape Codepoint=81
        // 可见在 StandardSymbolsPS.ttf 字体下，在 Shape 之前，获取到的 Codepoint 就是字符的 Codepoint 值，而不是 GlyphIndex 的值。在 Shape 之后，获取到的 Codepoint 就是 GlyphIndex 的值
        ReadOnlySpan<GlyphInfo> glyphInfoSpan = buffer.GetGlyphInfoSpan();
        Span<ushort> glyphsSpan = glyphInfoSpan.Length < 1024 ?
            stackalloc ushort[glyphInfoSpan.Length]
            : (Span<ushort>) new ushort[glyphInfoSpan.Length];

        for (int i = 0; i < glyphInfoSpan.Length; i++)
        {
            var codepoint = glyphInfoSpan[i].Codepoint;
            glyphsSpan[i] = (ushort) codepoint;
        }

        Span<byte> byteBuffer = MemoryMarshal.AsBytes(glyphsSpan);

        using var skTextBlob = SKTextBlob.Create(byteBuffer, SKTextEncoding.GlyphId, renderStatus.SKFont);
        canvas.DrawText(skTextBlob, x, y, renderStatus.Paint);
    }
}