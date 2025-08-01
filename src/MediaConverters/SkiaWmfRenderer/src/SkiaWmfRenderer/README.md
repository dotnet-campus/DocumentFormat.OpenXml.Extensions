# DotNetCampus.MediaConverter.SkiaWmfRenderer

Rendering library for SkiaSharp to WMF (Windows Metafile) format.

## Usage

### Convert WMF to Png

```csharp
    FileInfo wmfFile = ...;
    FileInfo outputPngFile = ...;
    bool success = SkiaWmfRenderHelper.TryConvertToPng(wmfFile, outputPngFile);
```

### Render WMF to SKBitmap

```csharp
    FileInfo wmfFile = ...;
    var configuration = new SkiaWmfRenderConfiguration();
    bool success = SkiaWmfRenderHelper.TryRender(wmfFile, configuration, out SKBitmap? skBitmap);
````