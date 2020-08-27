using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateProtectedTests
{
    [TestClass]
    public class PrivateProtectedWriteFloatFixture : BasePropertyFixture<SampleModelForTesting, float>
    {
        
        public PrivateProtectedWriteFloatFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PrivateProtectedWriteFloat";
        }

        [TestMethod]
        [DataRow(null)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.PrivateProtected)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
