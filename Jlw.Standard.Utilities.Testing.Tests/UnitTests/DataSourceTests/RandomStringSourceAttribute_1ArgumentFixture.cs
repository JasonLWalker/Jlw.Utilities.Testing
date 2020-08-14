using System;
using System.Text.RegularExpressions;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class RandomStringSourceAttribute_1ArgumentFixture
    {

        [TestMethod]
        [RandomStringSource(5)]
        public void Should_BeInstanceOf_String_ForArgument(object o)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(string));
            else
                Assert.IsNull(o);
        }

        [TestMethod]
        [RandomStringSource(8)]
        public void Should_BeAssignableTo_String(object o)
        {
            String s = (string)o;
            Assert.AreEqual(o?.ToString(), s);
        }

        [TestMethod]
        [RandomStringSource(12)]
        public void Should_HaveLength_GreaterThan0(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length > 0, $"Length should be greater than 0. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(14)]
        public void Should_HaveLength_LessThan16(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length < 16, $"Length should be less than 16. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(20)]
        public void Should_NotContain_InvalidCharacters(object o)
        {
            String s = (string)o;
            //Assert.IsTrue(s.Length < 16, $"Length should be less than 11. Instead, length is {s.Length}");
            StringAssert.Matches(s, new Regex("^[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopsrstuvwxyz1234567890]*$"));
        }



    }
}
