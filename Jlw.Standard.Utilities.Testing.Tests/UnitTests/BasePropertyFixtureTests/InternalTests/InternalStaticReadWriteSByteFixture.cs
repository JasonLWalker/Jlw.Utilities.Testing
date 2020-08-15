using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.InternalTests
{
    [TestClass]
    public class InternalStaticReadWriteSByteFixture : BasePropertyFixture<SampleModelForTesting, SByte>
    {
        
        public InternalStaticReadWriteSByteFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "InternalStaticReadWriteSByte";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Internal | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Internal | AccessScope.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
