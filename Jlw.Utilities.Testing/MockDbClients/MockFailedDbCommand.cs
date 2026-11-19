using System.Data;
using System.IO;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// A mock command that simulates a failing stored procedure. If a failure script exists, it is run instead of the normal script.
    /// </summary>
    /// <remarks>
    /// The failure script is named <c>CommandText_failed.sql</c> and is read from <see cref="WrappedDbCommand.DataPath"/>.
    /// </remarks>
    /// <typeparam name="TCommand">The type of the wrapped command.</typeparam>
    public class MockFailedDbCommand<TCommand> : MockWrappedDbCommand<TCommand>
        where TCommand : IDbCommand, new()
    {

        /// <summary>
        /// Runs the failure script named <c>CommandText_failed.sql</c> if it exists. Otherwise, runs the normal stored procedure script.
        /// </summary>
        /// <returns>A data reader for the results.</returns>
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
