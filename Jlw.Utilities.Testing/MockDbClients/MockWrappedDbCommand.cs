using System.Data;
using System.IO;

namespace Jlw.Utilities.Testing
{
    public class MockWrappedDbCommand<TCommand> : WrappedDbCommand, IDbCommand
        where TCommand : IDbCommand, new()
    {
        private bool _isStoredProc = false;

        public MockWrappedDbCommand()
        {
            _dbCmd = new TCommand();
        }

        public int CommandTimeout { get; set; }

        public virtual CommandType CommandType
        {
            get => _dbCmd.CommandType;
            set
            {
                if (value == CommandType.StoredProcedure)
                {
                    _isStoredProc = true;
                    _dbCmd.CommandType = CommandType.Text;
                    return;
                }
                _dbCmd.CommandType = value;
            }
        }

        public IDataParameterCollection Parameters => _dbCmd.Parameters;
        public IDbTransaction Transaction { get => _dbCmd.Transaction; set => _dbCmd.Transaction = value; }
        public UpdateRowSource UpdatedRowSource { get => _dbCmd.UpdatedRowSource; set => _dbCmd.UpdatedRowSource = value; }

        public void Cancel()
        {
            _dbCmd.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return _dbCmd.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            return _dbCmd.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return _dbCmd.ExecuteScalar();
        }

        public void Prepare()
        {
            _dbCmd.Prepare();
        }

        public virtual IDataReader ExecuteReader()
        {
            return ExecuteReader(CommandBehavior.Default);
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            if (_isStoredProc)
            {
                return ExecuteStoredProc();
            }

            return _dbCmd.ExecuteReader();
        }


        protected virtual IDataReader ExecuteStoredProc()
        {
            var path = $"{_sDataPath}{CommandText}.sql";

            if (File.Exists(path))
            {
                CommandText = File.ReadAllText(path); 
            }
            
            return _dbCmd.ExecuteReader();
        }

        public void Dispose()
        {
            _dbCmd.Dispose();
        }
    }
}