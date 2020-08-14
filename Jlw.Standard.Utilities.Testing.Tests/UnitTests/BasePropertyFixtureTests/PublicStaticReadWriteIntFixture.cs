using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests
{
    [TestClass]
    public class PublicStaticReadWriteIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        
        public PublicStaticReadWriteIntFixture()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PublicStaticReadWriteInt";
        }

        [TestMethod]
        [DataRow(MethodAttributes.Public | MethodAttributes.Static)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(MethodAttributes.Public | MethodAttributes.Static)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

    }
}
