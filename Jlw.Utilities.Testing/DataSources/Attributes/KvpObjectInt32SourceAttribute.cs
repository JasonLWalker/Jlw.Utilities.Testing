using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies the key and expected value pairs in <see cref="DataSourceValues.KvpObjectInt32Data"/> as test data for a test method.
    /// Each pair is passed to the test method as two parameters, the key and the expected value.
    /// </summary>
    public class KvpObjectInt32SourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        /// <summary>
        /// Gets the test data rows for the test method, one row per key and expected value pair in <see cref="DataSourceValues.KvpObjectInt32Data"/>.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One two-element array per pair, containing the key and the expected value.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
             foreach (var kvp in DataSourceValues.KvpObjectInt32Data)
             {
                 yield return new object[] {kvp.Key, kvp.Value};
             }
        }
    }
}
