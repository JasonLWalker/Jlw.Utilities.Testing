using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.UnitTests.BaseModelFixtureTests.TestInconclusiveAssertions
{
    [TestClass]
    public class NullTestModel_ModelFixture : BaseModelFixture<NullModel, NullTestSchema>
    {
        [TestMethod]
        [DataRow(null)]
        public override void Constructor_ShouldExist(ConstructorSchema schema)
        {
            base.Constructor_ShouldExist(schema);
        }
    }
}