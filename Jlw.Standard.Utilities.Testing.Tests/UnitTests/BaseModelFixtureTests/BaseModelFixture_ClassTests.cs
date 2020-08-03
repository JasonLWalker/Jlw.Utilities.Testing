using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixture_ClassTests : BaseModelFixture<SampleModelForTesting>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            DefaultInstance = new SampleModelForTesting();
        }

        [TestMethod]
        public void Should_Match_ForDefaultInstance()
        {
            Assert.IsNotNull(DefaultInstance);
            Assert.IsInstanceOfType(DefaultInstance, typeof(SampleModelForTesting));
        }
    }
}
