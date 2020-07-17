using System.Data;
using System.IO;

namespace Jlw.Standard.Utilities.Testing
{
    public class MockFailedDbCommand<TCommand> : MockWrappedDbCommand<TCommand>
        where TCommand : IDbCommand, new()
    {

        protected override IDataReader ExecuteStoredProc()
        {
            var path = $"{_sDataPath}{CommandText}_failed.sql";

            if (File.Exists(path))
            {
                CommandText = File.ReadAllText(path); 
                return _dbCmd.ExecuteReader();
            }

            return base.ExecuteStoredProc();
        }


    }
}