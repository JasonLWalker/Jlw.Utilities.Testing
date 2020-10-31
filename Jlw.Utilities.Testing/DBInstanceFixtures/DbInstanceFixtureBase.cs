using Jlw.Utilities.Data.DbUtility;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    [TestClass]
    public class DbInstanceFixtureBase<TInstance, TRepo>
    {
        public TestContext TestContext { get; set; }
        public IModularDbClient DbClient { get; } = new ModularDbClient<SqlConnection, SqlCommand, SqlParameter>();

        public TInstance Instance => (TInstance)TestContext.Properties["DbFixture.Instance"];
        public TRepo DefaultRepo => (TRepo)TestContext.Properties["DbFixture.Repository"];
        public string ConnectionString => TestContext.Properties["DbFixture.ConnectionString"]?.ToString();


        public void InitializeFixture(string connString, TInstance instance, TRepo repo)
        {
            TestContext.Properties.Add("DbFixture.Repository", repo);
            TestContext.Properties.Add("DbFixture.Instance", instance);
            TestContext.Properties.Add("DbFixture.ConnectionString", connString);
        }
    }
}