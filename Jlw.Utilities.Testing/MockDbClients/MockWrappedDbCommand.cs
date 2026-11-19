using System.Data;
using System.IO;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// A mock command that wraps a command of type <typeparamref name="TCommand"/> and can run stored procedures from SQL script files.
    /// </summary>
    /// <remarks>
    /// When <see cref="CommandType"/> is set to <c>StoredProcedure</c>, the wrapped command is set to <c>Text</c>, and
    /// executing the command runs the script named <c>CommandText.sql</c> from <see cref="WrappedDbCommand.DataPath"/> instead, if it exists.
    /// </remarks>
    /// <typeparam name="TCommand">The type of the wrapped command.</typeparam>
    public class MockWrappedDbCommand<TCommand> : WrappedDbCommand, IDbCommand
        where TCommand : IDbCommand, new()
    {
        private bool _isStoredProc = false;

        /// <summary>
        /// Creates the wrapped command as a new instance of <typeparamref name="TCommand"/>.
        /// </summary>
        public MockWrappedDbCommand()
        {
            _dbCmd = new TCommand();
        }

        /// <summary>
        /// Gets or sets the wait time, in seconds, before the command times out. The value is kept on the mock and is not forwarded to the wrapped command.
        /// </summary>
        public int CommandTimeout { get; set; }

        /// <summary>
        /// Gets or sets the type of the command. Setting <c>StoredProcedure</c> sets the wrapped command to <c>Text</c>
        /// and marks the command to run from a script file when it is executed.
        /// </summary>
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

        /// <summary>
        /// Gets the parameters of the wrapped command.
        /// </summary>
        public IDataParameterCollection Parameters => _dbCmd.Parameters;
        /// <summary>
        /// Gets or sets the transaction of the wrapped command.
        /// </summary>
        public IDbTransaction Transaction { get => _dbCmd.Transaction; set => _dbCmd.Transaction = value; }
        /// <summary>
        /// Gets or sets how the results of the wrapped command are applied to the <see cref="DataRow"/>.
        /// </summary>
        public UpdateRowSource UpdatedRowSource { get => _dbCmd.UpdatedRowSource; set => _dbCmd.UpdatedRowSource = value; }

        /// <summary>
        /// Cancels the execution of the wrapped command.
        /// </summary>
        public void Cancel()
        {
            _dbCmd.Cancel();
        }

        /// <summary>
        /// Creates a new parameter for the wrapped command.
        /// </summary>
        /// <returns>The new parameter.</returns>
        public IDbDataParameter CreateParameter()
        {
            return _dbCmd.CreateParameter();
        }

        /// <summary>
        /// Executes the wrapped command and returns the number of rows affected.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery()
        {
            return _dbCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes the wrapped command and returns the first column of the first row of the result.
        /// </summary>
        /// <returns>The first column of the first row, or <c>null</c> if there are no rows.</returns>
        public object ExecuteScalar()
        {
            return _dbCmd.ExecuteScalar();
        }

        /// <summary>
        /// Prepares the wrapped command for execution.
        /// </summary>
        public void Prepare()
        {
            _dbCmd.Prepare();
        }

        /// <summary>
        /// Executes the command and returns a data reader, using the default command behavior.
        /// </summary>
        /// <returns>A data reader for the results.</returns>
        public virtual IDataReader ExecuteReader()
        {
            return ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Executes the command and returns a data reader. When the command is a stored procedure, the script is run instead of the wrapped command.
        /// </summary>
        /// <param name="behavior">The command behavior. It is not used when the command runs a stored procedure script.</param>
        /// <returns>A data reader for the results.</returns>
        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            if (_isStoredProc)
            {
                return ExecuteStoredProc();
            }

            return _dbCmd.ExecuteReader();
        }


        /// <summary>
        /// Loads the SQL script named <c>CommandText.sql</c> from <see cref="WrappedDbCommand.DataPath"/>, if it exists,
        /// uses its text as the command text, and executes the wrapped command.
        /// </summary>
        /// <returns>A data reader for the results.</returns>
        protected virtual IDataReader ExecuteStoredProc()
        {
            var path = $"{_sDataPath}{CommandText}.sql";

            if (File.Exists(path))
            {
                CommandText = File.ReadAllText(path); 
            }
            
            return _dbCmd.ExecuteReader();
        }

        /// <summary>
        /// Releases the resources used by the wrapped command.
        /// </summary>
        public void Dispose()
        {
            _dbCmd.Dispose();
        }
    }
}
