using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests
{
    [TestClass]
    public class PublicReadWriteIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            PropertyName = "PublicReadWriteInt";
        }

        [TestMethod]
        [DataRow(typeof(int))]
        public override void Should_BeInstanceOf(Type t)
        {
            base.Should_BeInstanceOf(t);
        }

        [TestMethod]
        [DataRow(MethodAttributes.Public)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForGet(attr);
        }

        [TestMethod]
        [DataRow(MethodAttributes.Public)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            base.Should_MatchAccessScope_ForSet(attr);
        }

        [TestMethod]
        public void Should_MatchValue_ForDefaultConstructor()
        {
            var o = new SampleModelForTesting();
            base.Should_MatchValue(o, int.MinValue);
        }
    }
}
