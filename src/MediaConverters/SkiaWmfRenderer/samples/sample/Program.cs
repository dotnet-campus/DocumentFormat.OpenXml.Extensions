// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

using DocSharp.Markdown;
using SkiaSharp;
using SkiaWmfRenderer;

var markdownText = new StringBuilder();
var outputFolder = Path.Join(AppContext.BaseDirectory, $"Output_{Path.GetRandomFileName()}");
Directory.CreateDirectory(outputFolder);

//var testFile = @"C:\lindexi\wmf公式\sample.wmf";
//ConvertImageFile(testFile);

if (args.Length == 1)
{
    if (File.Exists(args[0]))
    {
        ConvertImageFile(args[0]);
    }
    else if (Directory.Exists(args[0]))
    {
        ConvertImageFolder(args[0]);
    }
    else
    {
        Console.WriteLine($"Can not recognition '{args[0]}' as File or Folder");
    }
}
else
{
    using var skBitmap = new SKBitmap(300,300,SKColorType.Bgra8888,SKAlphaType.Premul);
    skBitmap.Erase(SKColors.White);
    using var skCanvas = new SKCanvas(skBitmap);
    var text = "p"; // 这里的 p 是 Symbol 字体中的 Pi 符号
    using var skPaint = new SKPaint();
    skPaint.TextSize = 50;
    var symbolFontFile = Path.Join(AppContext.BaseDirectory, "StandardSymbolsPS.ttf");
    var skTypeface =
        SKFontManager.Default.CreateTypeface(symbolFontFile);
    //skTypeface = SKTypeface.FromFamilyName("Symbol");
    Console.WriteLine($"Font='{symbolFontFile}' SKTypeface={skTypeface.FamilyName} GlyphCount={skTypeface.GlyphCount}");
    Console.WriteLine($"ContainsGlyph={skTypeface.ContainsGlyph('p')} {skTypeface.GetGlyph('p')}");

    skPaint.Typeface = skTypeface;
    var skFont = skTypeface.ToFont(50);
    skPaint.Color = SKColors.Black;
    skPaint.IsAntialias = true;
    var skTextBlob = SKTextBlob.Create("p",skFont);
    skCanvas.DrawText(skTextBlob, 50, 100, skPaint);

    var outputFile = Path.Join(outputFolder, $"{DateTime.Now:HHmmss}.png");
    using var outputStream = File.OpenWrite(outputFile);
    skBitmap.Encode(outputStream, SKEncodedImageFormat.Png, 100);
    if (File.Exists(outputFile))
    {
        return;
    }

    if (OperatingSystem.IsWindows())
    {
        // Debug mode
        var folder = @"C:\lindexi\wmf公式\";

        ConvertImageFolder(folder);
    }
    else
    {
        var file = Path.Join(AppContext.BaseDirectory, "image.wmf");
        ConvertImageFile(file);
    }
}

var markdownFile = Path.Join(outputFolder, "README.md");
var markdown = markdownText.ToString();
File.WriteAllText(markdownFile, markdown);

var docxFile = Path.Join(outputFolder, "README.docx");
var markdownConverter = new MarkdownConverter
{
    ImagesBaseUri = outputFolder
};

MarkdownSource markdownSource = MarkdownSource.FromMarkdownString(markdown);
markdownConverter.ToDocx(markdownSource, docxFile);

Console.WriteLine("Hello, World!");

void ConvertImageFolder(string folder)
{
    foreach (var file in Directory.EnumerateFiles(folder, "*.wmf"))
    {
        ConvertImageFile(file);
    }
}

void ConvertImageFile(string file)
{
    Console.WriteLine($"Start convert '{file}'");

    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
    var gdiFileName = $"GDI_{fileNameWithoutExtension}.png";
    var gdiFile = Path.Join(outputFolder, gdiFileName);

    if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
    {
        var image = Image.FromFile(file);
        image.Save(gdiFile, ImageFormat.Png);
    }

    var wmfFileName = $"WMF_{fileNameWithoutExtension}.png";
    var testOutputFile = Path.Join(outputFolder, wmfFileName);
    var stopwatch = Stopwatch.StartNew();

    Console.WriteLine($"Finish convert '{file}' to '{testOutputFile}'");

    var success = SkiaWmfRenderHelper.TryConvertToPng(new FileInfo(file), new FileInfo(testOutputFile));
    stopwatch.Stop();

    Console.WriteLine($"SkiaWmfRenderHelper.TryConvertToPng success={success}");

    markdownText.AppendLine
    (
        $$"""
          ## {{fileNameWithoutExtension}}

          **GDI:**

          ![](./{{gdiFileName}}){width=250 height=120}

          **WMF:**

          """
    );

    if (success)
    {
        markdownText.AppendLine($"![](./{wmfFileName})" + "{width=250 height=120}");
    }
    else
    {
        markdownText.AppendLine("Rendering failed.");
    }

    markdownText.AppendLine();
    markdownText.AppendLine($"Rendering time: {stopwatch.ElapsedMilliseconds} ms");
}
