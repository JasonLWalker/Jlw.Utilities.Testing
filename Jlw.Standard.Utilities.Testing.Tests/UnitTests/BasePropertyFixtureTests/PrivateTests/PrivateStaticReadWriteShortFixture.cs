using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PrivateTests
{
    [TestClass]
    public class PrivateStaticReadWriteShortFixture : BasePropertyFixture<SampleModelForTesting, short>
    {
        
        public PrivateStaticReadWriteShortFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PrivateStaticReadWriteShort";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Private | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Private | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
