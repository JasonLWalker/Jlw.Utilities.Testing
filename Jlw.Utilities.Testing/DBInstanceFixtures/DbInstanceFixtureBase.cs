using Jlw.Utilities.Data.DbUtility;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Base class for test fixtures that provide a database instance and a repository to each test.
    /// </summary>
    /// <remarks>
    /// The instance, repository, and connection string are stored in <see cref="TestContext"/> properties by <see cref="InitializeFixture"/>.
    /// </remarks>
    /// <typeparam name="TInstance">The type of the database instance.</typeparam>
    /// <typeparam name="TRepo">The type of the repository.</typeparam>
    [TestClass]
    public class DbInstanceFixtureBase<TInstance, TRepo>
    {
        /// <summary>
        /// Gets or sets the MSTest context for the current test run, used to read and store the fixture state.
        /// </summary>
        public TestContext TestContext { get; set; }
        /// <summary>
        /// Gets the database client used to create connections, commands, and parameters.
        /// </summary>
        public IModularDbClient DbClient { get; } = new ModularDbClient<SqlConnection, SqlCommand, SqlParameter>();

        /// <summary>
        /// Gets the database instance stored for the current test.
        /// </summary>
        public TInstance Instance => (TInstance)TestContext.Properties["DbFixture.Instance"];
        /// <summary>
        /// Gets the repository stored for the current test.
        /// </summary>
        public TRepo DefaultRepo => (TRepo)TestContext.Properties["DbFixture.Repository"];
        /// <summary>
        /// Gets the connection string stored for the current test, or <c>null</c> if none has been stored.
        /// </summary>
        public string ConnectionString => TestContext.Properties["DbFixture.ConnectionString"]?.ToString();


        /// <summary>
        /// Stores the connection string, database instance, and repository in the test context so that the tests can read them.
        /// </summary>
        /// <remarks>
        /// Call this once per test. Calling it again in the same test context throws, because the keys are already present.
        /// </remarks>
        /// <param name="connString">The connection string for the database instance.</param>
        /// <param name="instance">The database instance.</param>
        /// <param name="repo">The repository for the database instance.</param>
        public void InitializeFixture(string connString, TInstance instance, TRepo repo)
        {
            TestContext.Properties.Add("DbFixture.Repository", repo);
            TestContext.Properties.Add("DbFixture.Instance", instance);
            TestContext.Properties.Add("DbFixture.ConnectionString", connString);
        }
    }
}
