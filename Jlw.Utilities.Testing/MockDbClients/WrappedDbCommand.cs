using System.Data;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Base class that exposes an <see cref="IDbCommand"/> through properties that forward to a wrapped command.
    /// </summary>
    public class WrappedDbCommand
    {
        /// <summary>
        /// The directory that contains the SQL script files.
        /// </summary>
        protected string _sDataPath ;
        /// <summary>
        /// The wrapped command that the members forward to.
        /// </summary>
        protected IDbCommand _dbCmd;

        /// <summary>
        /// Gets or sets the command text of the wrapped command.
        /// </summary>
        public string CommandText { get=>_dbCmd.CommandText; set=>_dbCmd.CommandText=value; }
        /// <summary>
        /// Gets or sets the connection of the wrapped command.
        /// </summary>
        public IDbConnection Connection { get=>_dbCmd.Connection; set => _dbCmd.Connection = value; }

        /// <summary>
        /// Gets or sets the directory that contains the SQL script files used by the mock command.
        /// </summary>
        public string DataPath
        {
            get => _sDataPath;
            set => _sDataPath = value;
        }

        /// <summary>
        /// Gets or sets the wrapped command.
        /// </summary>
        public IDbCommand DbCommand { 
            get=>_dbCmd;
            set => _dbCmd = value;
        }
    }
}
