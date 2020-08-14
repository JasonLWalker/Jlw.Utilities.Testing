using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixture_ClassTests : BaseModelFixture<SampleModelForTesting>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            DefaultInstance = new SampleModelForTesting();
        }

        [TestMethod]
        [DataRow(typeof(ISampleModelForTesting))]
        [DataRow(typeof(SampleModelForTesting))]
        public override void Should_BeInstanceOf(Type t)
        {
            base.Should_BeInstanceOf(t);
        }
    }
}
