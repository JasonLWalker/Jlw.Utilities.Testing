using System;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class KvpObjectSingleSourceAttributeFixture
    {

        [TestMethod]
        [KvpObjectSingleSource]
        public void Should_BeInstanceOf_NullableObject_ForFirstArgument(object o, object b)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(object));
        }

        [TestMethod]
        [KvpObjectSingleSource]
        public void Should_BeInstanceOf_Single_ForSecondArgument(object o, object b)
        {
            Assert.IsNotNull(b);
            Assert.IsInstanceOfType(b, typeof(Single));
        }

        [TestMethod]
        [KvpObjectSingleSource]
        public void Should_ParseAs_Single(object o, object b)
        {
            Assert.IsInstanceOfType(DataUtility.ParseAs(typeof(Single), o), typeof(Single));
        }

        [TestMethod]
        [KvpObjectSingleSource]
        public void Should_Match_ForParsedValue(object o, object b)
        {
            Single expected = (Single)(b ?? false);
            Assert.AreEqual(expected, DataUtility.ParseAs(typeof(Single), o));
        }


    }
}
