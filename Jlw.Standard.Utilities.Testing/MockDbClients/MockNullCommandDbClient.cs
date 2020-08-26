using System;
using System.Data;
using Jlw.Standard.Utilities.Data.DbUtility;

namespace Jlw.Standard.Utilities.Testing
{
    /*
    public class MockNullCommandDbClient<TConn> : ModularDbClient<TConn, NullDbCommand, NullDbParameter>, IModularDbClient 
        where TConn : IDbConnection, new()
    {
        public override IDbConnection GetConnection(string connString)
        {
            return new TConn {ConnectionString = connString};
        }

        public override IDbCommand GetCommand(string cmd, IDbConnection conn)
        {
            return null;
        }

        public override IDbDataParameter AddParameterWithValue(string paramName, object value, IDbCommand cmd)
        {
            throw new NotImplementedException();
        }

        public override IDbDataParameter GetNewParameter()
        {
            throw new NotImplementedException();
        }
    }
    */
}