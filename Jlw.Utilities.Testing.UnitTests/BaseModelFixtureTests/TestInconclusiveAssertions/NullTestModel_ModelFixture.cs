using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.UnitTests.BaseModelFixtureTests.TestInconclusiveAssertions
{
    [TestClass]
    public class NullTestModel_ModelFixture : BaseModelFixture<NullModel, NullTestSchema>
    {
        [TestMethod]
        [DataRow(null)]
        public override void Constructor_Should_Exist(ConstructorSchema schema)
        {
            base.Constructor_Should_Exist(schema);
        }
    }
}