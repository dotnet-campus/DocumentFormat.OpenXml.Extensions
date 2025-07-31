using System.Diagnostics.CodeAnalysis;
using System.Text;
using DotNetCampus.MediaConverter.SkiaWmfRenderer.Rendering;
using Oxage.Wmf;
using SkiaSharp;

namespace DotNetCampus.MediaConverter.SkiaWmfRenderer;

public static class SkiaWmfRenderHelper
{
    public static bool TryConvertToPng(FileInfo wmfFile, FileInfo outputPngFile, SkiaWmfRenderConfiguration? configuration = null)
    {
        configuration ??= new SkiaWmfRenderConfiguration();
        if (!TryRender(wmfFile, configuration, out var skBitmap))
        {
            return false;
        }

        using var outputStream = outputPngFile.OpenWrite();
        skBitmap.Encode(outputStream, SKEncodedImageFormat.Png, 100);

        return true;
    }

    public static bool TryRender(FileInfo wmfFile, SkiaWmfRenderConfiguration configuration, [NotNullWhen(true)] out SKBitmap? skBitmap)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        skBitmap = null;

        using var fileStream = wmfFile.OpenRead();
        var wmfDocument = new WmfDocument();

        try
        {
            wmfDocument.Load(fileStream);
        }
        catch (WmfException e)
        {
            Console.WriteLine($"[SkiaWmfRenderHelper] TryRender Fail. {e}");
            return false;
        }

        var wmfRenderer = new WmfRenderer()
        {
            WmfDocument = wmfDocument,
            RenderConfiguration = configuration
        };

        return wmfRenderer.TryRender(out skBitmap);
    }
}