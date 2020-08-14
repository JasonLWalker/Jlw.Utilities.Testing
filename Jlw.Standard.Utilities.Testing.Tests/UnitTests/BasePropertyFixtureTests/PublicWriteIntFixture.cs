using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests
{
    [TestClass]
    public class PublicWriteIntFixture : BasePropertyFixture<SampleModelForTesting, int>
    {
        
        public PublicWriteIntFixture()
        {
            DefaultInstance = new SampleModelForTesting();
            PropertyName = "PublicWriteInt";
        }

        
        [TestMethod]
        [DataRow(null)]
        public override void Should_MatchAccessScope_ForGet(MethodAttributes attr)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                base.Should_MatchAccessScope_ForGet(attr);
            });
            StringAssert.Contains(ex.ToString(), $"{PropertyName} is not a readable property");
        }
        
    }
}
