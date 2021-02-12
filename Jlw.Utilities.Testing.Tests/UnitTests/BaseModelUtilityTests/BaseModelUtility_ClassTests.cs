using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BaseModelUtilityTests
{
    [TestClass]
    public class BaseModelUtility_ClassTests : BaseModelUtility<SampleModelForTesting>
    {
        /*
        public BaseModelFixture_ClassTests()
        {
            DefaultInstance = new SampleModelForTesting();
        }
        */

        [TestMethod]
        [DataRow(typeof(ISampleModelForTesting))]
        [DataRow(typeof(SampleModelForTesting))]
        public override void Should_BeInstanceOf(Type t)
        {
            base.Should_BeInstanceOf(t);
        }
    }
}
