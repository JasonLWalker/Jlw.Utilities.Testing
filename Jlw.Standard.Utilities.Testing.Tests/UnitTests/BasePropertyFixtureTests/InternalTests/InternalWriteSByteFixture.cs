using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.InternalTests
{
    [TestClass]
    public class InternalWriteSByteFixture : BasePropertyFixture<SampleModelForTesting, SByte>
    {
        
        public InternalWriteSByteFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "InternalWriteSByte";
        }

        [TestMethod]
        [DataRow(null)]
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
