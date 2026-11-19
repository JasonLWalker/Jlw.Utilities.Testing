using System;
using System.IO;
using MartinCostello.SqlLocalDb;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Test fixture that creates a temporary SQL LocalDB instance for each test and deletes it afterwards.
    /// </summary>
    /// <typeparam name="TRepo">The type of the repository created for the temporary instance.</typeparam>
    [TestClass]
    public class SqlLocalDbInstanceFixtureBase<TRepo> : DbInstanceFixtureBase<TemporarySqlLocalDbInstance, TRepo>
        where TRepo : class
    {
        // ReSharper disable StaticMemberInGenericType
        /// <summary>
        /// The logger factory passed to the LocalDB API.
        /// </summary>
        protected LoggerFactory _loggerFactory = new LoggerFactory();
        /// <summary>
        /// The LocalDB API used to create and delete the temporary instance.
        /// </summary>
        protected SqlLocalDbApi _localDb;
        // ReSharper restore StaticMemberInGenericType

        /// <summary>
        /// Creates a temporary LocalDB instance and a repository for it, and stores both in the test context.
        /// </summary>
        [TestInitialize]
        public virtual void TestInitialize()
        {
            _localDb = new SqlLocalDbApi(_loggerFactory);
            var instance = _localDb.CreateTemporaryInstance(deleteFiles: true);
            string connString = instance.ConnectionString;
            var repo = (TRepo)Activator.CreateInstance(typeof(TRepo), new object[] { DbClient, connString });
            InitializeFixture(connString, instance, repo);
        }

        /// <summary>
        /// Disposes the temporary LocalDB instance and the LocalDB API.
        /// </summary>
        [TestCleanup]
        public virtual void TestCleanup()
        {
            Instance?.Dispose();
            _localDb?.Dispose();
        }

        /// <summary>
        /// Runs the SQL script in the given file against the current database instance.
        /// </summary>
        /// <param name="filename">The path to the SQL script file.</param>
        protected virtual void InitializeInstanceData(string filename)
        {
            string initScript = File.ReadAllText(filename);
            var conn = new SqlConnection(ConnectionString);
            var server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(initScript);
        }
    }
}
