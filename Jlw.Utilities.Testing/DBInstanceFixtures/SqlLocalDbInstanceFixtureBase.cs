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
    [TestClass]
    public class SqlLocalDbInstanceFixtureBase<TRepo> : DbInstanceFixtureBase<TemporarySqlLocalDbInstance, TRepo>
        where TRepo : class
    {
        // ReSharper disable StaticMemberInGenericType
        protected LoggerFactory _loggerFactory = new LoggerFactory();
        protected SqlLocalDbApi _localDb;
        // ReSharper restore StaticMemberInGenericType

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _localDb = new SqlLocalDbApi(_loggerFactory);
            var instance = _localDb.CreateTemporaryInstance(deleteFiles: true);
            string connString = instance.ConnectionString;
            var repo = (TRepo)Activator.CreateInstance(typeof(TRepo), new object[] { DbClient, connString });
            InitializeFixture(connString, instance, repo);
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Instance?.Dispose();
            _localDb?.Dispose();
        }

        protected virtual void InitializeInstanceData(string filename)
        {
            string initScript = File.ReadAllText(filename);
            var conn = new SqlConnection(ConnectionString);
            var server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(initScript);
        }
    }
}