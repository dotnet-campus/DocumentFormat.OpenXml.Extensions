using System;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXmlUnitConverter.Tests
{
    [TestClass]
    public class MillisecondTimeTest
    {
        [ContractTestCase]
        public void FromMilliseconds()
        {
            "提供从毫秒创建的方法，可以从给定的毫秒进行创建 MillisecondTime 结构体".Test(() =>
            {
                var milliseconds = 300;
                var millisecondTime = MillisecondTime.FromMilliseconds(milliseconds);
                Assert.AreEqual(TimeSpan.FromMilliseconds(milliseconds), millisecondTime.ToTimeSpan());
            });
        }

        [ContractTestCase]
        public void ParseMillisecondTimeText()
        {
            "传入 null 的 OpenXML 字符串，可以说明未解析".Test(() =>
            {
                StringValue stringValue = null!;
                var millisecondTime = new MillisecondTime(stringValue);
                Assert.AreEqual(true, millisecondTime.IsIndefinite);
            });

            "传入未定义的 OpenXML 字符串，可以说明未解析".Test(() =>
            {
                var stringValue = new StringValue(IndefiniteTimeDeclarationValues.Indefinite.ToString());
                var millisecondTime = new MillisecondTime(stringValue);
                Assert.AreEqual(true, millisecondTime.IsIndefinite);
            });

            "传入数值表示的毫秒时间的 OpenXML 字符串，可以解析出毫秒时间".Test(() =>
            {
                var n = 123;
                var stringValue = new StringValue(n.ToString());
                var millisecondTime = new MillisecondTime(stringValue);
                Assert.AreEqual(n, millisecondTime.Milliseconds);
                Assert.AreEqual(1230000, millisecondTime.ToTicks());
                Assert.AreEqual(TimeSpan.FromMilliseconds(n), millisecondTime.ToTimeSpan());
            });
        }
    }
}