using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateProtectedTests
{
    [TestClass]
    public class PrivateProtectedReadWriteFloatFixture : BasePropertyFixture<SampleModelForTesting, float>
    {
        
        public PrivateProtectedReadWriteFloatFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PrivateProtectedReadWriteFloat";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.PrivateProtected)]
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
