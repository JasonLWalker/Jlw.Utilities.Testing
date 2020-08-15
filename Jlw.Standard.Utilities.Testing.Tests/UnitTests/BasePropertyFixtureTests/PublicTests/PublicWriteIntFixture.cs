using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BasePropertyFixtureTests.PublicTests
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
            base.Should_MatchAccessScope_ForGet(attr);
        }
        
    }
}
