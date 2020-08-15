using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedTests
{
    [TestClass]
    public class ProtectedReadWriteLongFixture : BasePropertyFixture<SampleModelForTesting, long>
    {
        
        public ProtectedReadWriteLongFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedReadWriteLong";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Protected)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Protected)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
