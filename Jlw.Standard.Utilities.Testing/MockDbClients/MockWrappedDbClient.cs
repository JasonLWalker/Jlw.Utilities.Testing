using System.Data;
using Jlw.Standard.Utilities.Data.DbUtility;

namespace Jlw.Standard.Utilities.Testing
{
    public class MockWrappedDbClient<TConnection, TWrap, TCommand, TParam> : ModularDbClient<TConnection, TWrap, TParam> 
        where TConnection : IDbConnection, new() 
        where TWrap : WrappedDbCommand, IDbCommand, new()
        where TCommand: IDbCommand, new()
        where TParam: IDbDataParameter, new()
    {
        protected string _path;

        public MockWrappedDbClient(string path)
        {
            _path = path;
        }
        public override IDbCommand GetCommand(string cmd, IDbConnection conn)
        {
            var wrap = new TWrap() {DataPath = _path, DbCommand = new TCommand(){CommandText = cmd, Connection = conn}};
            

            return wrap;
        }
    }
}
