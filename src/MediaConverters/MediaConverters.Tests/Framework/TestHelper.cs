using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Tests;

[TestClass]
public static class TestHelper
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        WorkingDirectory = Directory.CreateDirectory(Path.Join(context.TestRunDirectory, "Working"));
    }

    private static DirectoryInfo WorkingDirectory { get; set; } = null!;

    public static string SaveAsTestImageFile(this Image<Rgba32> image)
    {
        var file = Path.Join(WorkingDirectory.FullName, Path.GetRandomFileName() + ".png");
        using var fileStream = File.OpenWrite(file);
        image.SaveAsPng(fileStream, new PngEncoder()
        {
            ColorType = PngColorType.RgbWithAlpha
        });
        return file;
    }

    public static void OpenFileInExplorer(string filePath)
    {
        if (File.Exists(filePath))
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                Process.Start(new ProcessStartInfo("explorer", $"\"{filePath}\"") { UseShellExecute = true });
            }
#endif
        }
        else
        {
            throw new FileNotFoundException($"The file '{filePath}' does not exist.");
        }
    }
}