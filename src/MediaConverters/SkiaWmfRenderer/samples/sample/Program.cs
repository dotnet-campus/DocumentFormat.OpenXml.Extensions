// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

using DocSharp.Markdown;

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
    // Debug mode
    var folder = @"C:\lindexi\wmf公式\";

    ConvertImageFolder(folder);
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
