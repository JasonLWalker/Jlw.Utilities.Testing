using System.Data;

namespace Jlw.Utilities.Testing
{
    public class WrappedDbCommand
    {
        protected string _sDataPath ;
        protected IDbCommand _dbCmd;

        public string CommandText { get=>_dbCmd.CommandText; set=>_dbCmd.CommandText=value; }
        public IDbConnection Connection { get=>_dbCmd.Connection; set => _dbCmd.Connection = value; }

        public string DataPath
        {
            get => _sDataPath;
            set => _sDataPath = value;
        }

        public IDbCommand DbCommand { 
            get=>_dbCmd;
            set => _dbCmd = value;
        }
    }
}