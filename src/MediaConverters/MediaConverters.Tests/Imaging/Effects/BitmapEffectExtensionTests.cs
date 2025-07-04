using DotNetCampus.MediaConverters.Imaging.Effects;
using DotNetCampus.MediaConverters.Imaging.Effects.Colors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Tests.Imaging.Effects;

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

    [TestMethod("替换不存在的颜色，不会影响原来的图片")]
    public void ReplaceColorTest_NotExists()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();

        // 这是一个不存在图片里的颜色
        var source = new Rgba32(0x36, 0x55, 0x23, 0xFF);

        var targetColor = new Rgba32(0xFF, 0xFF, 0xFF, 0x00);
        image.ReplaceColor(source, targetColor);

        // 替换不存在的颜色，不会影响原来的图片
        var file = image.SaveAndCompareTestFile(TestFileProvider.DefaultTestImageName);
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
        var file = image.SaveAndCompareTestFile(TestFileProvider.DefaultTestImageName);
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

        image.AssertSolidBlackColorImage();

        var file = image.SaveAndCompareTestFile("SetBrightness2.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBrightness3()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBrightness(1);
        var file = image.SaveAndCompareTestFile(TestFileProvider.DefaultTestImageName);
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBrightness4()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBrightness(0.5f);
        var file = image.SaveAndCompareTestFile("SetBrightness4.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBrightness5()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBrightness(0.7f);
        var file = image.SaveAndCompareTestFile("SetBrightness5.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetSoftEdgeEffect1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetSoftEdgeEffect(50.0f);
        var file = image.SaveAndCompareTestFile("SetSoftEdgeMaskResult1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetSoftEdgeEffect2()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetSoftEdgeEffect(10.0f);
        var file = image.SaveAndCompareTestFile("SetSoftEdgeEffect2.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetSoftEdgeEffect3()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetSoftEdgeEffect(0);
        // 算法原因，设置为 0 时，还是有微弱差别
        var file = image.SaveAndCompareTestFile();
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetDuotoneEffect1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        var list = image.GetColorCount();
        list = list.OrderByDescending(t => t.Count).ToList();

        ColorMetadata color1 = new ColorMetadata(list[0].Color);
        ColorMetadata color2 = new ColorMetadata(list[1].Color);

        image.SetDuotoneEffect(color1, color2);
        var file = image.SaveAndCompareTestFile("SetDuotoneEffect1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBlackWhiteEffect1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBlackWhiteEffect(0.5f);
        var file = image.SaveAndCompareTestFile("SetBlackWhiteEffect1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBlackWhiteEffect2()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBlackWhiteEffect(0.7f);
        var file = image.SaveAndCompareTestFile("SetBlackWhiteEffect2.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBlackWhiteEffect3()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBlackWhiteEffect(0.2f);
        var file = image.SaveAndCompareTestFile("SetBlackWhiteEffect3.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBlackWhiteEffect4()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBlackWhiteEffect(0);
        image.AssertSolidWhiteColorImage();
        var file = image.SaveAndCompareTestFile("SetBlackWhiteEffect4.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetBlackWhiteEffect5()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetBlackWhiteEffect(1);
        image.AssertSolidBlackColorImage();
        var file = image.SaveAndCompareTestFile("SetBlackWhiteEffect5.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetGrayScaleEffect1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetGrayScaleEffect();
        var file = image.SaveAndCompareTestFile("SetGrayScaleEffect1.png");
        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void SetLuminanceEffect1()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.SetLuminanceEffect();
        var file = image.SaveAndCompareTestFile("SetLuminanceEffect1.png");
        TestHelper.OpenFileInExplorer(file);
    }
}