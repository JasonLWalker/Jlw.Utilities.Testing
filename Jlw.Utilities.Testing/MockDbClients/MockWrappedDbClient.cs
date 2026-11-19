using System.Data;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// A mock database client that creates commands of type <typeparamref name="TWrap"/> which read their SQL scripts from a data path.
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <typeparam name="TWrap">The type of the wrapped command created for each command text.</typeparam>
    /// <typeparam name="TCommand">The type of the underlying command wrapped by <typeparamref name="TWrap"/>.</typeparam>
    /// <typeparam name="TParam">The type of the parameters.</typeparam>
    public class MockWrappedDbClient<TConnection, TWrap, TCommand, TParam> : ModularDbClient<TConnection, TWrap, TParam> 
        where TConnection : IDbConnection, new() 
        where TWrap : WrappedDbCommand, IDbCommand, new()
        where TCommand: IDbCommand, new()
        where TParam: IDbDataParameter, new()
    {
        /// <summary>
        /// The path to the directory that contains the mock SQL scripts.
        /// </summary>
        protected string _path;

        /// <summary>
        /// Creates a client that reads its mock SQL scripts from the given path.
        /// </summary>
        /// <param name="path">The path to the directory that contains the mock SQL scripts.</param>
        public MockWrappedDbClient(string path)
        {
            _path = path;
        }
        
        /*
        public override IDbCommand GetCommand(string cmd, IDbConnection conn)
        {
            var wrap = new TWrap() {DataPath = _path, DbCommand = new TCommand(){CommandText = cmd, Connection = conn}};
            

            return wrap;
        }
        */
    }
}
