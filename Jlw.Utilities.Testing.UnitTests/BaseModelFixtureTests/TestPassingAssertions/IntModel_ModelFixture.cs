using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.UnitTests.BaseModelFixtureTests.TestPassingAssertions
{
    [TestClass]
    public class IntModel_ModelFixture : BaseModelFixture<IntModel, IntModelTestSchema>
    {
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Private)]
        [DataRow(Protected)]
        public override void Constructor_Signatures_Should_Match(AccessModifiers access)
        {
            base.Constructor_Signatures_Should_Match(access);
        }


    }
}