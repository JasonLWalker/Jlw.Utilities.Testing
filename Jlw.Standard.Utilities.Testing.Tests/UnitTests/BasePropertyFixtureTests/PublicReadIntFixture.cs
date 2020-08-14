using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests
{
    [TestClass]
    public class PublicReadIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        public PublicReadIntFixture()
        {
            PropertyName = "PublicReadInt";
            DefaultInstance = new SampleModelForTesting();
        }

        [TestMethod]
        [DataRow(null)]
        public override void Should_MatchAccessScope_ForSet(MethodAttributes attr)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                base.Should_MatchAccessScope_ForSet(attr);
            });
            StringAssert.Contains(ex.ToString(), $"{PropertyName} is not a writable property");
        }
    }
}
