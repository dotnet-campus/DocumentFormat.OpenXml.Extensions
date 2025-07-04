using DotNetCampus.MediaConverters.Imaging.Optimizations;

using SixLabors.ImageSharp;

namespace DotNetCampus.MediaConverters.Tests.Imaging.Optimizations;

[TestClass()]
public class ImageFileOptimizationTests
{
    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest1()
    {
        var file = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName);
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory, 100, null);

        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
    }

    [TestMethod("传入不存在的图片文件，可以返回文件不存在，优化失败")]
    public async Task OptimizeImageFileAsyncTest2()
    {
        var file = new FileInfo("The Not Exists Image File.png");
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory, 100, null);

        Assert.AreEqual(false, imageFileOptimizationResult.IsSuccess);
        Assert.AreEqual(ImageFileOptimizationFailureReason.FileNotFound, imageFileOptimizationResult.FailureReason);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_LimitSize1()
    {
        var file = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName);
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory, 100, 100);
        // 预期此时通过面积限制，依然宽度高度超过了限制的最大宽度高度
        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
        ImageInfo imageInfo = await Image.IdentifyAsync(imageFileOptimizationResult.OptimizedImageFile!.FullName);
        Assert.AreEqual(true, imageInfo.Width > 100);
        Assert.AreEqual(true, imageInfo.Width * imageInfo.Height < 100 * 100);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_LimitSize2()
    {
        var file = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName);
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory, 100, 100, useAreaSizeLimit: false);
        // 不通过面积限制，宽度高度绝对都不超过限制的最大宽度高度
        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
        ImageInfo imageInfo = await Image.IdentifyAsync(imageFileOptimizationResult.OptimizedImageFile!.FullName);
        Assert.AreEqual(true, imageInfo.Width <= 100);
        Assert.AreEqual(true, imageInfo.Height <= 100);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_LimitSize3()
    {
        var file = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName);
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory, 600, 20, useAreaSizeLimit: false);

        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
        ImageInfo imageInfo = await Image.IdentifyAsync(imageFileOptimizationResult.OptimizedImageFile!.FullName);
        Assert.AreEqual(true, imageInfo.Width <= 600);
        Assert.AreEqual(true, imageInfo.Height <= 20);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_FormatTiff()
    {
        var file = TestFileProvider.GetTestFile("file_example_TIFF_1MB.tiff");
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory);
        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
        TestHelper.OpenFileInExplorer(imageFileOptimizationResult.OptimizedImageFile!);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_FormatWebp()
    {
        var file = TestFileProvider.GetTestFile("file_example_WEBP_50kB.webp");
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory);
        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
        TestHelper.OpenFileInExplorer(imageFileOptimizationResult.OptimizedImageFile!);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_FormatTga()
    {
        var file = TestFileProvider.GetTestFile("sample_640×426.tga");
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory);
        Assert.AreEqual(true, imageFileOptimizationResult.IsSuccess);
        TestHelper.OpenFileInExplorer(imageFileOptimizationResult.OptimizedImageFile!);
    }

    [TestMethod()]
    public async Task OptimizeImageFileAsyncTest_FormatWmf()
    {
        var file = TestFileProvider.GetTestFile("sample.wmf");
        var imageFileOptimizationResult = await ImageFileOptimization.OptimizeImageFileAsync(file, TestHelper.WorkingDirectory);
        Assert.AreEqual(false, imageFileOptimizationResult.IsSuccess);
        Assert.AreEqual(ImageFileOptimizationFailureReason.UnknownImageFormat, imageFileOptimizationResult.FailureReason);
    }
}