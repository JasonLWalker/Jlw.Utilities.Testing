using System;
using System.Text.RegularExpressions;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class RandomStringSourceAttribute_3ArgumentFixture
    {

        [TestMethod]
        [RandomStringSource(5,0,3)]
        public void Should_BeInstanceOf_String_ForArgument(object o)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(string));
            else
                Assert.IsNull(o);
        }

        [TestMethod]
        [RandomStringSource(8, 1,4)]
        public void Should_BeAssignableTo_String(object o)
        {
            String s = (string)o;
            Assert.AreEqual(o?.ToString(), s);
        }

        [TestMethod]
        [RandomStringSource(15, 3,4)]
        public void Should_HaveLength_GreaterThan2(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length > 2, $"Length should be greater than 2. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(16, 5,6)]
        public void Should_HaveLength_LessThan7(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length < 7, $"Length should be less than 7. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(5, 0,0)]
        public void Should_HaveLength_EqualTo0(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length == 0, $"Length should be 0. Instead, length is {s.Length}");
        }

        [TestMethod]
        [RandomStringSource(5, 10,10)]
        public void Should_HaveLength_EqualTo10(object o)
        {
            String s = (string)o;
            Assert.IsTrue(s.Length == 10, $"Length should be 10. Instead, length is {s.Length}");
        }


        [TestMethod]
        [RandomStringSource(20, 50, 100)]
        public void Should_NotContain_InvalidCharacters(object o)
        {
            String s = (string)o;
            //Assert.IsTrue(s.Length < 16, $"Length should be less than 11. Instead, length is {s.Length}");
            StringAssert.Matches(s, new Regex("^[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopsrstuvwxyz1234567890]*$"));
        }



    }
}
