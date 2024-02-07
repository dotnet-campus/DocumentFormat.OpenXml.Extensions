using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXmlUnitConverter.Tests
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

        [ContractTestCase]
        public void TestCalculate()
        {
            "非零百分比除以自己等于 100%".Test((double value) =>
            {
                var percentage = Percentage.FromDouble(value);

                var result = percentage / percentage;
                Assert.AreEqual(true, Math.Abs(result.DoubleValue - 1) < 0.000001);
            }).WithArguments(1.5, 2.3, 3.6, 100.5, 100000.123);
        }
    }
}
