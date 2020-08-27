using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateProtectedTests
{
    [TestClass]
    public class PrivateProtectedStaticReadWriteFloatFixture : BasePropertyFixture<SampleModelForTesting, float>
    {
        
        public PrivateProtectedStaticReadWriteFloatFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PrivateProtectedStaticReadWriteFloat";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.PrivateProtected | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.PrivateProtected | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
