using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PublicTests
{
    [TestClass]
    public class PublicStaticReadWriteIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        
        public PublicStaticReadWriteIntFixture()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PublicStaticReadWriteInt";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Public | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Public | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
