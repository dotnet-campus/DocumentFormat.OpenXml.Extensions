using DotNetCampus.MediaConverters.Imaging.Effects.Colors;
using DotNetCampus.MediaConverters.Imaging.Effects.Extensions;

namespace DotNetCampus.MediaConverters.Tests.Imaging.Effects.Colors;

[TestClass()]
public class ColorMatricesTests
{
    [TestMethod()]
    public void BrightnessFilterTest()
    {
        var color = new ColorMetadata(68 / 255f, 114 / 255f, 196 / 255f);
        var filter = ColorMatrices.CreateBrightnessFilter(1.8f);

        var expected = color.ApplyMatrix(filter).ARGB8bit;
        var actual = (122, 205, 255, 255);

        Assert.AreEqual(expected.R, actual.Item1);
        Assert.AreEqual(expected.G, actual.Item2);
        Assert.AreEqual(expected.B, actual.Item3);
        Assert.AreEqual(expected.A, actual.Item4);
    }

    [TestMethod()]
    public void ContrastFilterTest()
    {
        var color = new ColorMetadata(68 / 255f, 114 / 255f, 196 / 255f);
        var filter = ColorMatrices.CreateContrastFilter(1.8f);

        var expected = color.ApplyMatrix(filter).ARGB8bit;
        var actual = (20, 103, 251, 255);

        Assert.AreEqual(expected.Item1, actual.Item1);
        Assert.AreEqual(expected.Item2, actual.Item2);
        Assert.AreEqual(expected.Item3, actual.Item3);
        Assert.AreEqual(expected.Item4, actual.Item4);
    }

    [TestMethod()]
    public void GrayFilterTest()
    {
        var color = new ColorMetadata(68 / 255f, 114 / 255f, 196 / 255f);
        var filter = ColorMatrices.CreateGrayScaleFilter(0.8f);

        var expected = color.ApplyMatrix(filter).ARGB8bit;
        var actual = (101, 113, 127, 255);

        Assert.AreEqual(expected.Item1, actual.Item1);
        Assert.AreEqual(expected.Item2, actual.Item2);
        Assert.AreEqual(expected.Item3, actual.Item3);
        Assert.AreEqual(expected.Item4, actual.Item4);
    }

    [TestMethod()]
    public void SaturationFilterTest()
    {
        var color = new ColorMetadata(68 / 255f, 114 / 255f, 196 / 255f);
        var filter = ColorMatrices.CreateSaturationFilter(1.8f);

        var expected = color.ApplyMatrix(filter).ARGB8bit;
        var actual = (34, 117, 255, 255);

        Assert.AreEqual(expected.Item1, actual.Item1);
        Assert.AreEqual(expected.Item2, actual.Item2);
        Assert.AreEqual(expected.Item3, actual.Item3);
        Assert.AreEqual(expected.Item4, actual.Item4);
    }
}