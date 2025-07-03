using System.Numerics;

using DotNetCampus.MediaConverters.Imaging.Effect;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DotNetCampus.MediaConverters.Tests.Imaging.Effect;

[TestClass()]
public class SoftEdgeHelperTests
{
    [TestMethod()]
    public void SetSoftEdgeMaskTest()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();

        SoftEdgeHelper.SetSoftEdgeMask(image, 50.0f);

        var file = image.SaveAndCompareTestFile("SetSoftEdgeMaskResult1.png");

        TestHelper.OpenFileInExplorer(file);
    }

    [TestMethod()]
    public void TestManipulation()
    {
        Image<Rgba32> image = TestFileProvider.GetDefaultTestImage();
        image.Mutate(c => c.ProcessPixelRowsAsVector4(row =>
        {
            for (int col = 0; col < row.Length; col++)
            {
                // We can apply any custom processing logic here
                ref var pixel = ref row[col];
                pixel = Vector4.SquareRoot(pixel);
                if (col < row.Length - 1)
                {
                    pixel = pixel * (1 + Vector4.DistanceSquared(pixel, row[col + 1]/100));
                }
            }
        }));

        var file = image.SaveAsTestImageFile();
        TestHelper.OpenFileInExplorer(file);
    }
}