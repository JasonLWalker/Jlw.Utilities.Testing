using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateProtectedTests
{
    [TestClass]
    public class PrivateProtectedReadFloatFixture : BasePropertyFixture<SampleModelForTesting, float>
    {
        
        public PrivateProtectedReadFloatFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PrivateProtectedReadFloat";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.PrivateProtected)]
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
