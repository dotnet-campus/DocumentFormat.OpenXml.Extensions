using DocumentFormat.OpenXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXmlUnitConverter.Tests;

[TestClass]
public class EmuPercentageTest
{
    [ContractTestCase]
    public void CreateEmuPercentage()
    {
        "传入带百分号的 Int32Value 值，可以成功转换为 EmuPercentage 对象".Test(() =>
        {
            var int32Value = new Int32Value
            {
                InnerText = "10%"
            };

            var emuPercentage = new EmuPercentage(int32Value);
            var pixelPercentage = emuPercentage.ToPixelPercentage();
            Assert.AreEqual(10 * 1000, pixelPercentage.IntValue);
        });
    }
}