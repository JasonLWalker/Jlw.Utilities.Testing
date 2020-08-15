using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateTests
{
    [TestClass]
    public class PrivateReadShortFixture : BasePropertyFixture<SampleModelForTesting, short>
    {
        public PrivateReadShortFixture()
        {
            PropertyName = "PrivateReadShort";
            DefaultInstance = new SampleModelForTesting();
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Private)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(null)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }
    }
}
