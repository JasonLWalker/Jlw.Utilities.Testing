using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PublicTests
{
    [TestClass]
    public class PublicReadIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        public PublicReadIntFixture()
        {
            PropertyName = "PublicReadInt";
            DefaultInstance = new SampleModelForTesting();
        }

        [TestMethod]
        [DataRow(null)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }
    }
}
