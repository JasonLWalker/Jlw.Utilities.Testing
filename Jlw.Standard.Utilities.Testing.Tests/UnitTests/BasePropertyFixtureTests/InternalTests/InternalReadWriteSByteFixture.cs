using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.InternalTests
{
    [TestClass]
    public class InternalReadWriteSByteFixture : BasePropertyFixture<SampleModelForTesting, SByte>
    {
        
        public InternalReadWriteSByteFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "InternalReadWriteSByte";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Internal)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Internal)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
