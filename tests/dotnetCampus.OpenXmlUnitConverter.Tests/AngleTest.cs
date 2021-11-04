using System;
using DocumentFormat.OpenXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.OpenXmlUnitConverter.Tests
{
    [TestClass]
    public class AngleTest
    {
        [ContractTestCase]
        public void Calculate()
        {
            "传入两个相等的角度值，可以通过 == 判断相等".Test(() =>
            {
                var a = Angle.Degree90;
                var b = new Angle(new Int32Value(5400000));
                var c = Angle.FromRadiansValue(Math.PI / 2);

                Assert.AreEqual(true, a == b);
                Assert.AreEqual(true, a == c);
                Assert.AreEqual(true, b == c);

                Assert.AreEqual(false, a != b);
                Assert.AreEqual(false, a != c);
                Assert.AreEqual(false, b != c);
            });

            "传入90度的角度和270度的角度，可以返回270度的角度更大".Test(() =>
            {
                var a = Angle.Degree90;
                var b = Angle.Degree270;

                Assert.AreEqual(true, b > a);
                Assert.AreEqual(true, a < b);
            });

            "使用负数表示的角度，拿到的是原先的负数角度值".Test(() =>
            {
                var a = Angle.Degree180;
                var b = -a;
                Assert.AreEqual(a, b * -1);
                Assert.AreEqual(a, -b);
            });

            "使用正数表示的角度，和原先角度值相同".Test(() =>
            {
                var angle = Angle.Degree180;
                Assert.AreEqual(angle, +angle);
            });
        }
    }
}