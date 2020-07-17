using System;
using System.Data;
using Jlw.Standard.Utilities.Data.DbUtility;

namespace Jlw.Standard.Utilities.Testing
{
    public class MockNullCommandDbClient<TConn> : IModularDbClient 
        where TConn : IDbConnection, new()
    {
        public IDbConnection GetConnection(string connString)
        {
            return new TConn {ConnectionString = connString};
        }

        public IDbCommand GetCommand(string cmd, IDbConnection conn)
        {
            return null;
        }

        public IDbDataParameter AddParameterWithValue(string paramName, object value, IDbCommand cmd)
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter GetNewParameter()
        {
            throw new NotImplementedException();
        }
    }
}