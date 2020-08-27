using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateTests
{
    [TestClass]
    public class PrivateReadWriteShortFixture : BasePropertyFixture<SampleModelForTesting, short>
    {
        public PrivateReadWriteShortFixture()
        {
            PropertyName = "PrivateReadWriteShort";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Private)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }


        [TestMethod]
        [DataRow(AccessScope.Accessors.Private)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
