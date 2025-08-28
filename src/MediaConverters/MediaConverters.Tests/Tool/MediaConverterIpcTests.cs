using System.Text.Json;
using System.Text.Json.Serialization;
using dotnetCampus.Ipc.IpcRouteds.DirectRouteds;
using DotNetCampus.MediaConverters.CommandLineHandlers;
using DotNetCampus.MediaConverters.Contexts;
using DotNetCampus.MediaConverters.Contexts.IpcContexts;

namespace DotNetCampus.MediaConverters.Tests.Tool;

[TestClass]
public class MediaConverterIpcTests
{
    [TestMethod]
    public async Task TestBatchImage()
    {
        var testFolder = Path.Join(TestHelper.WorkingDirectory.FullName, Path.GetRandomFileName());
        Directory.CreateDirectory(testFolder);

        var ipcHandler = new IpcHandler()
        {
            IpcName = Guid.NewGuid().ToString(),
            WorkingFolder = testFolder,
            ShouldLogToConsole = true,
            ShouldLogToFile = true,
        };

        var task = Task.Run(async () =>
        {
            var provider = new JsonIpcDirectRoutedProvider();
            var clientProxy = await provider.GetAndConnectClientAsync(ipcHandler.IpcName);

            var response = await clientProxy.GetResponseAsync<IpcConvertImageResponse>(IpcPaths.RequestConvertImage, new IpcConvertImageRequest()
            {
                TraceId = "TraceId-1",
                InputFile = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName).FullName,
                OutputFile = Path.Join(testFolder, "Output1.png"),
                ConvertConfigurationFile = ToConfigurationFile(new ImageConvertContext()
                {
                    ImageConvertTaskList =
                    [
                        new SetSoftEdgeEffectTask()
                        {
                            Radius = 20
                        }
                    ]
                },testFolder)
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(MediaConverterErrorCode.Success.Code, response.Code);

            response = await clientProxy.GetResponseAsync<IpcConvertImageResponse>(IpcPaths.RequestConvertImage, new IpcConvertImageRequest()
            {
                TraceId = "TraceId-2",
                InputFile = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName).FullName,
                OutputFile = Path.Join(testFolder, "Output2.png"),
                ConvertConfigurationFile = ToConfigurationFile(new ImageConvertContext()
                {
                    ImageConvertTaskList =
                    [
                        new SetLuminanceEffectTask()
                    ],
                    ShouldCopyNewFile = false,
                }, testFolder)
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(MediaConverterErrorCode.Success.Code, response.Code);

            response = await clientProxy.GetResponseAsync<IpcConvertImageResponse>(IpcPaths.RequestConvertImage, new IpcConvertImageRequest()
            {
                TraceId = "TraceId-3",
                InputFile = TestFileProvider.GetTestFile(TestFileProvider.DefaultTestImageName).FullName,
                OutputFile = Path.Join(testFolder, "Output3.png"),
                ConvertConfigurationFile = ToConfigurationFile(new ImageConvertContext()
                {
                    ImageConvertTaskList =
                    [
                        new SetContrastTask()
                        {
                            Percentage = 0.7f
                        }
                    ]
                }, testFolder)
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(MediaConverterErrorCode.Success.Code, response.Code);

            var ipcExitResponse = await clientProxy.GetResponseAsync<IpcExitResponse>(IpcPaths.Exit, new IpcExitRequest()
            {
                Reason = "正常退出"
            });
            Assert.IsNotNull(ipcExitResponse);
            Assert.AreEqual(MediaConverterErrorCode.Success.Code, response.Code);
        });

        await ipcHandler.RunAsync();

        await task;
    }

    private static string ToConfigurationFile(ImageConvertContext imageConvertContext,string testFolder)
    {
        var jsonText = JsonSerializer.Serialize(imageConvertContext,
            new JsonSerializerOptions(MediaConverterJsonSerializerSourceGenerationContext.Default.Options)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            });
        var configFile = Path.Join(testFolder, "Config.json");
        File.WriteAllText(configFile, jsonText);
        return configFile;
    }
}