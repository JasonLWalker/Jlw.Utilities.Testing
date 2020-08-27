using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedInternalTests
{
    [TestClass]
    public class ProtectedInternalStaticReadWriteDoubleFixture : BasePropertyFixture<SampleModelForTesting, double>
    {
        
        public ProtectedInternalStaticReadWriteDoubleFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedInternalStaticReadWriteDouble";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.ProtectedInternal | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.ProtectedInternal | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
