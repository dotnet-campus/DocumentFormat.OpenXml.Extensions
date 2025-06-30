using DotNetCampus.MediaConverters.Imaging.Effect;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace DotNetCampus.MediaConverters.Tests.Imaging.Effect;

[TestClass()]
public class SoftEdgeHelperTests
{
    [TestMethod()]
    public void SetSoftEdgeMaskTest()
    {
        Image<Rgba32> bitmap = TestFileProvider.GetDefaultTestImage();
        var alphaRepresentation = bitmap.PixelType.AlphaRepresentation;

        SoftEdgeHelper.SetSoftEdgeMask(bitmap, 50.0f);

        var file = bitmap.SaveAsTestImageFile();

        Assert.IsTrue(File.Exists(file));
        TestHelper.OpenFileInExplorer(file);
    }
}