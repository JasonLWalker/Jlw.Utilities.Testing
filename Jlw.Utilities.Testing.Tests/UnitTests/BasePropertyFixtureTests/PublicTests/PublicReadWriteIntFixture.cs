using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PublicTests
{
    [TestClass]
    public class PublicReadWriteIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        public PublicReadWriteIntFixture()
        {
            PropertyName = "PublicReadWriteInt";
        }

        [TestMethod]
        public void Should_MatchValue_ForDefaultConstructor()
        {
            var o = new SampleModelForTesting();
            base.Should_MatchValue(o, int.MinValue);
        }
    }
}
