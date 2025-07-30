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

        ReadOnlySpan<GlyphInfo> glyphInfoSpan = buffer.GetGlyphInfoSpan();
        Span<ushort> glyphsSpan = glyphInfoSpan.Length < 1024 ?
            stackalloc ushort[glyphInfoSpan.Length]
            : (Span<ushort>) new ushort[glyphInfoSpan.Length];

        for (int i = 0; i < glyphInfoSpan.Length; i++)
        {
            var codepoint = glyphInfoSpan[i].Codepoint;
            if (font.TryGetGlyph(codepoint, out uint glyphId))
            {
                glyphsSpan[i] = (ushort) glyphId;
            }
            else
            {
                // 暂时不知道怎么处理
                glyphsSpan[i] = 0;
            }
        }

        Span<byte> byteBuffer = MemoryMarshal.AsBytes(glyphsSpan);

        using var skTextBlob = SKTextBlob.Create(byteBuffer, SKTextEncoding.GlyphId, renderStatus.SKFont);
        canvas.DrawText(skTextBlob, x, y, renderStatus.Paint);
    }
}