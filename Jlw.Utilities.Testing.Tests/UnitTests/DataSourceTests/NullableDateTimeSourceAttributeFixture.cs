using System;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class NullableDateTimeSourceAttributeFixture
    {

        [TestMethod]
        [NullableDateTimeSource]
        public void Should_BeInstanceOf_NullableDateTime(object o)
        {
            if (o != null)
            {
                Assert.IsInstanceOfType(o, typeof(DateTime));
            }
            else
            {
                Assert.IsNull(o);
            }
        }
    }
}
