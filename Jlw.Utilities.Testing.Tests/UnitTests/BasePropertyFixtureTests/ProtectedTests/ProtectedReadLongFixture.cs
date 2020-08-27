using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedTests
{
    [TestClass]
    public class ProtectedReadLongFixture : BasePropertyFixture<SampleModelForTesting, long>
    {
        
        public ProtectedReadLongFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedReadLong";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Protected)]
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
