using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXMLUnitConverter.Tests
{
    [TestClass]
    public class PercentageTest
    {
        [ContractTestCase]
        public void ParsePercentageText()
        {
            "传入带百分号的数值，可以转换为百分号".Test(() =>
            {
                var percentageText = "100%";
                var percentage = new Percentage(percentageText);
                Assert.AreEqual(100000, percentage.IntValue);
            });

            "传入百分号带小数点的数值，能转换出 OpenXML 单位的整数".Test(() =>
            {
                var percentageText = "99.999%";
                var percentage = new Percentage(percentageText);
                Assert.AreEqual(99999, percentage.IntValue);
            });
        }
    }
}
