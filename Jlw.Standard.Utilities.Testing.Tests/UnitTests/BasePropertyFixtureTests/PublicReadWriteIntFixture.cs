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
        public PublicReadWriteIntFixture()
        {
            PropertyName = "PublicReadWriteInt";
        }

        [TestMethod]
        public void Should_MatchValue_ForDefaultConstructor()
        {
            var o = new SampleModelForTesting();
            base.Should_MatchValue(o, int.MinValue);
        }
    }
}
