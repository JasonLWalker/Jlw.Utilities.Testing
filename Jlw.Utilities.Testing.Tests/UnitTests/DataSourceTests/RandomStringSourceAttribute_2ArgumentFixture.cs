using System;
using System.Text.RegularExpressions;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class RandomStringSourceAttribute_2ArgumentFixture
    {

        [TestMethod]
        [RandomStringSource(5,3)]
        public void Should_BeInstanceOf_String_ForArgument(object o)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(string));
            else
                Assert.IsNull(o);
        }

        [TestMethod]
        [RandomStringSource(8, 4)]
        public void Should_BeAssignableTo_String(object o)
        {
            String s = (string)o;
            Assert.AreEqual(o?.ToString(), s);
        }

        [TestMethod]
        [RandomStringSource(15, 5)]
        public void Should_HaveLength_GreaterThan0(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length > 0, $"Length should be greater than 0. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(16, 6)]
        public void Should_HaveLength_LessThan7(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length < 7, $"Length should be less than 7. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(20, 100)]
        public void Should_NotContain_InvalidCharacters(object o)
        {
            String s = (string)o;
            //Assert.IsTrue(s.Length < 16, $"Length should be less than 11. Instead, length is {s.Length}");
            StringAssert.Matches(s, new Regex("^[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopsrstuvwxyz1234567890]*$"));
        }



    }
}
