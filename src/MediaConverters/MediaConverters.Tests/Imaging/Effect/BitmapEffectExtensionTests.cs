using DotNetCampus.MediaConverters.Imaging.Effect;
using DotNetCampus.MediaConverters.Imaging.Effect.Colors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Tests.Imaging.Effect;

[TestClass()]
public class BitmapEffectExtensionTests
{
    [TestMethod()]
    public void TestGetMaxCountColor()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        var (rgba32, count) = image.GetMaxCountColor();

        var expected = new Rgba32(0xF1, 0xD7, 0xA6, 0xFF);
        Assert.AreEqual(expected, rgba32);
        Assert.AreEqual(1215, count);
    }

    [TestMethod()]
    public void ReplaceColorTest1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        var (rgba32, count) = image.GetMaxCountColor();
        _ = count;
        var targetColor = new Rgba32(0xFF, 0xFF, 0xFF, 0x00);
        image.ReplaceColor(rgba32, targetColor);

        var file = image.SaveAndCompareTestFile("ReplaceColorTest1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void ReplaceColorTest2()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        var (rgba32, count) = image.GetMaxCountColor();
        _ = count;
        var targetColor = new Rgba32(0xFF, 0xFF, 0xFF, 0x00);
        var sourceMetadata = new ColorMetadata(rgba32);
        var targetMetadata = new ColorMetadata(targetColor);
        image.ReplaceColor(sourceMetadata, targetMetadata);

        var file = image.SaveAndCompareTestFile("ReplaceColorTest1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void ReplaceColorTest3()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        var list = image.GetColorCount();
        list = list.OrderByDescending(t => t.Count).ToList();

        var targetColor = new Rgba32(0xFF, 0xFF, 0xFF, 0x00);
        var targetMetadata = new ColorMetadata(targetColor);

        Dictionary<ColorMetadata, ColorMetadata> colorInfos = [];
        for (var i = 0; i < list.Count && i < 10; i++)
        {
            colorInfos[new ColorMetadata(list[i].Color)] = targetMetadata;
        }

        image.ReplaceColor(colorInfos);
        var file = image.SaveAndCompareTestFile("ReplaceColorTest3.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetContrast1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetContrast(0.2f);
        var file = image.SaveAndCompareTestFile("SetContrast1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetContrast2()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetContrast(0);
        var file = image.SaveAndCompareTestFile("SetContrast2.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetContrast3()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetContrast(1);
        // 对比度不变，和原图一样
        var file = image.SaveAndCompareTestFile("file_example_PNG_500kB.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBrightness1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBrightness(0.2f);
        var file = image.SaveAndCompareTestFile("SetBrightness1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBrightness2()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBrightness(0);
        var file = image.SaveAndCompareTestFile("SetBrightness2.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBrightness3()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBrightness(1);
        var file = image.SaveAndCompareTestFile("file_example_PNG_500kB.png");
        TestHelper.OpenFileInExplorer(file);
    }
}