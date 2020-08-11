using System;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class KvpObjectInt64SourceAttributeFixture
    {

        [TestMethod]
        [KvpObjectInt64Source]
        public void Should_BeInstanceOf_NullableObject_ForFirstArgument(object o, object b)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(object));
        }

        [TestMethod]
        [KvpObjectInt64Source]
        public void Should_BeInstanceOf_Int64_ForSecondArgument(object o, object b)
        {
            Assert.IsNotNull(b);
            Assert.IsInstanceOfType(b, typeof(Int64));
        }

        [TestMethod]
        [KvpObjectInt64Source]
        public void Should_ParseAs_Int64(object o, object b)
        {
            Assert.IsInstanceOfType(DataUtility.ParseAs(typeof(Int64), o), typeof(Int64));
        }

        [TestMethod]
        [KvpObjectInt64Source]
        public void Should_Match_ForParsedValue(object o, object b)
        {
            Int64 expected = (Int64)(b ?? false);
            Assert.AreEqual(expected, DataUtility.ParseAs(typeof(Int64), o));
        }


    }
}
