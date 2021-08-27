using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXmlUnitConverter.Tests
{
    [TestClass]
    public class PixelPointTest
    {
        [ContractTestCase]
        public void Convert()
        {
            "传入 Emu 表示的点，可以转换为像素表示的点".Test(() =>
            {
                var emuPoint = new EmuPoint(new Emu(952500), new Emu(952500));
                var pixelPoint = emuPoint.ToPixelPoint();
                Assert.AreEqual(100, pixelPoint.X.Value);
                Assert.AreEqual(100, pixelPoint.Y.Value);

                Assert.AreEqual(emuPoint, pixelPoint.ToEmuPoint());
            });
        }
    }
}