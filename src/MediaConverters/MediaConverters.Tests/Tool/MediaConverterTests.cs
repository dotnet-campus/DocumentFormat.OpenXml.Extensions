using DotNetCampus.MediaConverters.Contexts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DotNetCampus.MediaConverters.Tests.Tool;

[TestClass]
public class MediaConverterTests
{
    [TestMethod]
    public async Task OptimizeImageFile()
    {
        var testFolder = Path.Join(TestHelper.WorkingDirectory.FullName, Path.GetRandomFileName());
        Directory.CreateDirectory(testFolder);
        var workingFolder = Path.Join(testFolder, "Working");
        var outputFile = Path.Join(testFolder, "Output.png");
        var configFile = Path.Join(testFolder, "Config.json");

        var inputFile = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName);

        var imageConvertContext = new ImageConvertContext();

        var jsonText = JsonSerializer.Serialize(imageConvertContext, new JsonSerializerOptions(SourceGenerationContext.Default.Options)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        });
        await File.WriteAllTextAsync(configFile, jsonText);

        var options = new Options()
        {
            WorkingFolder = workingFolder,
            InputFile = inputFile.FullName,
            OutputFile = outputFile,
            ConvertConfigurationFile = configFile,
        };

        var result = await Program.RunAsync(options);
        Assert.AreEqual(ErrorCode.Success, result);
    }
}
