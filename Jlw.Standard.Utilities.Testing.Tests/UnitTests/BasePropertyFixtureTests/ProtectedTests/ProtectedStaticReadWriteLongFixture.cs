using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.ProtectedTests
{
    [TestClass]
    public class ProtectedStaticReadWriteLongFixture : BasePropertyFixture<SampleModelForTesting, long>
    {
        
        public ProtectedStaticReadWriteLongFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "ProtectedStaticReadWriteLong";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Protected | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Protected | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
