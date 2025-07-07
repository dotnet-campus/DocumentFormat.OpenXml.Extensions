using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Tests;

internal static class TestFileProvider
{
    public const string DefaultTestImageName = "file_example_PNG_500kB.png";

    public static Image<Rgba32> GetDefaultTestImage()
    {
        var imageFile = GetTestFile(DefaultTestImageName);
        using var fileStream = imageFile.OpenRead();
        return Image.Load<Rgba32>(new DecoderOptions(), fileStream);
    }

    public static string GetTestFilePath(string fileName)
    {
        if (File.Exists(fileName))
        {
            return Path.GetFullPath(fileName);
        }

        return System.IO.Path.Join(AppContext.BaseDirectory, "Assets", "TestFiles", fileName);
    }

    public static FileInfo GetTestFile(string fileName)
    {
        return new FileInfo(GetTestFilePath(fileName));
    }
}
