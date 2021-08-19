using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXmlUnitConverter.Tests
{
    [TestClass]
    public class EmuRectangleTest
    {
        [ContractTestCase]
        public void CreateEmuRectangle()
        {
            "给定矩形各个边角的值，可以计算出矩形的宽度和高度".Test(() =>
            {
                // Arrange
                var emuRectangle = new EmuRectangle(new Emu(0), new Emu(0), new Emu(100), new Emu(100));

                // Action
                var width = emuRectangle.Width;
                var height = emuRectangle.Height;

                // Assert
                Assert.AreEqual(100, width.Value);
                Assert.AreEqual(100, height.Value);
            });
        }
    }
}