using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedInternalTests
{
    [TestClass]
    public class ProtectedInternalReadWriteDoubleFixture : BasePropertyFixture<SampleModelForTesting, double>
    {
        
        public ProtectedInternalReadWriteDoubleFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedInternalReadWriteDouble";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.ProtectedInternal)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.ProtectedInternal)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
