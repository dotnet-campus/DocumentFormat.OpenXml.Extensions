using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Imaging.Effects;
using DotNetCampus.MediaConverters.Imaging.Effects.Colors;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

using System.Text.Json;
using System.Text.Json.Serialization;

using TextVisionComparer;

namespace DotNetCampus.MediaConverters.Tests.Tool;

[TestClass]
public class MediaConverterTests
{
    [TestMethod]
    public async Task SetDuotoneEffectTask1()
    {
        var imageConvertContext = new ImageConvertContext()
        {
            ImageConvertTaskList =
            [
                new SetDuotoneEffectTask()
                {
                    ArgbFormatColor1 = "#FFF1D7A6",
                    ArgbFormatColor2 = "#FFFFF2C8",
                }
            ]
        };

        var options = ToOptions(TestFileProvider.DefaultTestImageName, imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task ReplaceColorTask1()
    {
        var imageConvertContext = new ImageConvertContext()
        {
            ImageConvertTaskList =
                [
                    new ReplaceColorTask()
                    {
                    },
                ]
        };

        var options = ToOptions(TestFileProvider.DefaultTestImageName, imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task ReplaceColorTask2()
    {
        using Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        var list = image.GetColorCountList();
        list = list.OrderByDescending(t => t.Count).ToList();

        var replaceColorInfoList = new List<ReplaceColorInfo>();

        for (var i = 0; i < list.Count && i < 10; i++)
        {
            var color = list[i].Color;
            replaceColorInfoList.Add(new ReplaceColorInfo($"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}", "#00FFFFFF"));
        }

        var imageConvertContext = new ImageConvertContext()
        {
            ImageConvertTaskList =
            [
                new ReplaceColorTask()
                {
                    ReplaceColorInfoList = replaceColorInfoList
                },
            ]
        };

        var options = ToOptions(TestFileProvider.DefaultTestImageName, imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        AssertReplaceColor(options);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task ReplaceColorTask3()
    {
        var name = "file_example_TIFF_1MB.tiff";
        Image<Rgba32> image = await Image.LoadAsync<Rgba32>(TestFileProvider.GetTestFilePath(name));
        var list = image.GetColorCountList();
        list = list.OrderByDescending(t => t.Count).ToList();

        var replaceColorInfoList = new List<ReplaceColorInfo>();

        for (var i = 0; i < list.Count && i < 10; i++)
        {
            var color = list[i].Color;
            replaceColorInfoList.Add(new ReplaceColorInfo($"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}", "#00FFFFFF"));
        }

        var imageConvertContext = new ImageConvertContext()
        {
            ImageConvertTaskList =
            [
                new ReplaceColorTask()
                {
                    ReplaceColorInfoList = replaceColorInfoList
                },
            ]
        };

        var options = ToOptions(name, imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        AssertReplaceColor(options);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    private void AssertReplaceColor(Options options)
    {
        var inputFile = options.InputFile;
        using var image = Image.Load<Rgba32>(inputFile);

        var list = image.GetColorCountList();
        list = list.OrderByDescending(t => t.Count).ToList();
        var replaceList = new List<(ColorMetadata SourceMetadata, ColorMetadata TargetMetadata)>();
        for (var i = 0; i < list.Count && i < 10; i++)
        {
            var color = list[i].Color;
            var targetMetadata = new ColorMetadata(new Rgba32(0xFF, 0xFF, 0xFF, 0x00));
            replaceList.Add((new ColorMetadata(color), targetMetadata));
        }

        image.ReplaceColor(replaceList);

        var tempFile = Path.Join(options.WorkingFolder, $"Assert_{Path.GetRandomFileName()}.png");
        image.SaveAsPng(tempFile, new PngEncoder()
        {
            ColorType = PngColorType.RgbWithAlpha
        });

        var visionComparer = new VisionComparer();
        var visionCompareResult = visionComparer.Compare(new FileInfo(tempFile), new FileInfo(options.OutputFile));
        Assert.IsTrue(visionCompareResult.IsSimilar());
    }

    [TestMethod]
    public async Task OptimizeImageFile1()
    {
        var imageConvertContext = new ImageConvertContext();
        var options = ToOptions(TestFileProvider.DefaultTestImageName, imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task OptimizeImageFile2()
    {
        var imageConvertContext = new ImageConvertContext();
        var options = ToOptions("file_example_TIFF_1MB.tiff", imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task OptimizeImageFile3()
    {
        var imageConvertContext = new ImageConvertContext();
        var options = ToOptions("file_example_WEBP_50kB.webp", imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task OptimizeImageFile4()
    {
        var imageConvertContext = new ImageConvertContext();
        var options = ToOptions("sample_640×426.tga", imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    [TestMethod]
    public async Task OptimizeImageFile5()
    {
        var imageConvertContext = new ImageConvertContext();
        var options = ToOptions("EXIF Orientation.png", imageConvertContext);

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
        TestHelper.OpenFileInExplorer(new FileInfo(options.OutputFile));
    }

    private Options ToOptions(string fileName, ImageConvertContext imageConvertContext)
    {
        var testFolder = Path.Join(TestHelper.WorkingDirectory.FullName, Path.GetRandomFileName());
        Directory.CreateDirectory(testFolder);
        var workingFolder = Path.Join(testFolder, "Working");
        var outputFile = Path.Join(testFolder, "Output.png");
        var configFile = Path.Join(testFolder, "Config.json");

        var inputFile = TestFileProvider.GetTestFile(fileName);

        var jsonText = JsonSerializer.Serialize(imageConvertContext, new JsonSerializerOptions(SourceGenerationContext.Default.Options)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        });
        File.WriteAllText(configFile, jsonText);

        return new Options()
        {
            WorkingFolder = workingFolder,
            InputFile = inputFile.FullName,
            OutputFile = outputFile,
            ConvertConfigurationFile = configFile,
        };
    }
}
