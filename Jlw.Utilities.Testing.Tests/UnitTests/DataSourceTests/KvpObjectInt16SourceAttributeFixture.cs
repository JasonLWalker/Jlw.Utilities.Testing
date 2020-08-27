using System;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class KvpObjectInt16SourceAttributeFixture
    {

        [TestMethod]
        [KvpObjectInt16Source]
        public void Should_BeInstanceOf_NullableObject_ForFirstArgument(object o, object b)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(object));
        }

        [TestMethod]
        [KvpObjectInt16Source]
        public void Should_BeInstanceOf_Int16_ForSecondArgument(object o, object b)
        {
            Assert.IsNotNull(b);
            Assert.IsInstanceOfType(b, typeof(Int16));
        }

        [TestMethod]
        [KvpObjectInt16Source]
        public void Should_ParseAs_Int16(object o, object b)
        {
            Assert.IsInstanceOfType(DataUtility.ParseAs(typeof(Int16), o), typeof(Int16));
        }

        [TestMethod]
        [KvpObjectInt16Source]
        public void Should_Match_ForParsedValue(object o, object b)
        {
            Int16 expected = (Int16)(b ?? false);
            Assert.AreEqual(expected, DataUtility.ParseAs(typeof(Int16), o));
        }


    }
}
