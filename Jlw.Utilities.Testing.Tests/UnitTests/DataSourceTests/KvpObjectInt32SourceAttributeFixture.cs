using System;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class KvpObjectInt32SourceAttributeFixture
    {

        [TestMethod]
        [KvpObjectInt32Source]
        public void Should_BeInstanceOf_NullableObject_ForFirstArgument(object o, object b)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(object));
        }

        [TestMethod]
        [KvpObjectInt32Source]
        public void Should_BeInstanceOf_Int32_ForSecondArgument(object o, object b)
        {
            Assert.IsNotNull(b);
            Assert.IsInstanceOfType(b, typeof(Int32));
        }

        [TestMethod]
        [KvpObjectInt32Source]
        public void Should_ParseAs_Int32(object o, object b)
        {
            Assert.IsInstanceOfType(DataUtility.ParseAs(typeof(Int32), o), typeof(Int32));
        }

        [TestMethod]
        [KvpObjectInt32Source]
        public void Should_Match_ForParsedValue(object o, object b)
        {
            Int32 expected = (Int32)(b ?? false);
            Assert.AreEqual(expected, DataUtility.ParseAs(typeof(Int32), o));
        }


    }
}
