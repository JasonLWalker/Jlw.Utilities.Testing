using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedInternalTests
{
    [TestClass]
    public class ProtectedInternalWriteDoubleFixture : BasePropertyFixture<SampleModelForTesting, double>
    {
        
        public ProtectedInternalWriteDoubleFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedInternalWriteDouble";
        }

        [TestMethod]
        [DataRow(null)]
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
