using System;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class KvpObjectDoubleSourceAttributeFixture
    {

        [TestMethod]
        [KvpObjectDoubleSource]
        public void Should_BeInstanceOf_NullableObject_ForFirstArgument(object o, object b)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(object));
        }

        [TestMethod]
        [KvpObjectDoubleSource]
        public void Should_BeInstanceOf_Double_ForSecondArgument(object o, object b)
        {
            Assert.IsNotNull(b);
            Assert.IsInstanceOfType(b, typeof(Double));
        }

        [TestMethod]
        [KvpObjectDoubleSource]
        public void Should_ParseAs_Double(object o, object b)
        {
            Assert.IsInstanceOfType(DataUtility.ParseAs(typeof(Double), o), typeof(Double));
        }

        [TestMethod]
        [KvpObjectDoubleSource]
        public void Should_Match_ForParsedValue(object o, object b)
        {
            Double expected = (Double)(b ?? false);
            Assert.AreEqual(expected, DataUtility.ParseAs(typeof(Double), o));
        }


    }
}
