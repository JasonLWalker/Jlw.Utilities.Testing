using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.InternalTests
{
    [TestClass]
    public class InternalReadSByteFixture : BasePropertyFixture<SampleModelForTesting, SByte>
    {
        
        public InternalReadSByteFixture ()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "InternalReadSByte";
        }

        [TestMethod]
        [DataRow(AccessScope.Accessors.Internal)]
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
