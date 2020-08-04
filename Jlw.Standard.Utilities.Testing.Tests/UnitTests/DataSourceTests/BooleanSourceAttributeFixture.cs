using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class BooleanSourceAttributeFixture
    {

        [TestMethod]
        [BooleanSource]
        public void Should_BeInstanceOf_Bool_ForArgument(object o)
        {
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(bool));
        }
    }
}
