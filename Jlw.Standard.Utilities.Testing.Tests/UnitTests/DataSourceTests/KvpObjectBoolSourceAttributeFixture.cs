using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class KvpObjectBoolSourceAttributeFixture
    {

        [TestMethod]
        [KvpObjectBoolSource]
        public void Should_BeInstanceOf_NullableObject_ForFirstArgument(object o, object b)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(object));
            //Assert.IsInstanceOfType(b, typeof(object));
        }

        [TestMethod]
        [KvpObjectBoolSource]
        public void Should_BeInstanceOf_Bool_ForSecondArgument(object o, object b)
        {
            Assert.IsNotNull(b);
            Assert.IsInstanceOfType(b, typeof(bool));
        }

        [TestMethod]
        [KvpObjectBoolSource]
        public void Should_ParseAs_Bool(object o, object b)
        {
            Assert.IsInstanceOfType(DataUtility.ParseAs(typeof(bool), o), typeof(bool));
        }

        [TestMethod]
        [KvpObjectBoolSource]
        public void Should_Match_ForParsedValue(object o, object b)
        {
            bool expected = (bool)(b ?? false);
            Assert.AreEqual(expected, DataUtility.ParseAs(typeof(bool), o));
        }


    }
}
