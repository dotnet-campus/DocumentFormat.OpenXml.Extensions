using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using TextVisionComparer;

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

    public static FileInfo SaveAsTestImageFile(this Image<Rgba32> image)
    {
        var file = Path.Join(WorkingDirectory.FullName, Path.GetRandomFileName() + ".png");
        using var fileStream = File.OpenWrite(file);
        image.SaveAsPng(fileStream, new PngEncoder()
        {
            ColorType = PngColorType.RgbWithAlpha
        });
        return new FileInfo(file);
    }

    public static FileInfo SaveAndCompareTestFile(this Image<Rgba32> image, string? testFileName = null)
    {
        FileInfo testFile = image.SaveAsTestImageFile();

        if (testFileName != null)
        {
            var file = TestFileProvider.GetTestFile(testFileName);
            CompareImageFile(testFile, file);
        }

        return testFile;
    }

    public static void CompareImageFile(FileInfo file1, FileInfo file2)
    {
        var visionComparer = new VisionComparer();
        var visionCompareResult = visionComparer.Compare(file1,file2);
        Assert.IsTrue(visionCompareResult.IsSimilar());
    }

    public static void OpenFileInExplorer(FileInfo file)
    {
        if (File.Exists(file.FullName))
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                Process.Start(new ProcessStartInfo("explorer", $"\"{file.FullName}\"") { UseShellExecute = true });
            }
#endif
        }
        else
        {
            throw new FileNotFoundException($"The file '{file}' does not exist.");
        }
    }
}