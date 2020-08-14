using System;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class StringSourceAttributeFixture
    {

        [TestMethod]
        [StringSource]
        public void Should_BeInstanceOf_String_ForArgument(object o)
        {
            if (o != null)
                Assert.IsInstanceOfType(o, typeof(string));
            else
                Assert.IsNull(o);
        }

        [TestMethod]
        [StringSource]
        public void Should_BeAssignableTo_String(object o)
        {
            String s = (string)o;
            Assert.AreEqual(o?.ToString(), s);
        }

    }
}
