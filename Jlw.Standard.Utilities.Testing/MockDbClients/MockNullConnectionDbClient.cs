using System;
using System.Data;
using Jlw.Standard.Utilities.Data.DbUtility;

namespace Jlw.Standard.Utilities.Testing
{
    public class MockNullConnectionDbClient : IModularDbClient
    {
        public IDbConnection GetConnection(string connString)
        {
            return null;
        }

        public IDbCommand GetCommand(string cmd, IDbConnection conn)
        {
            throw new NotImplementedException();
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
