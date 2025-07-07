using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCampus.MediaConverters.Contexts;

namespace DotNetCampus.MediaConverters.Tests.Tool;

[TestClass]
public class MediaConverterTests
{
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
