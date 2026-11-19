using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies each value of <see cref="DataSourceValues.ObjectData"/> as a key, with its <c>ToString</c> form as the expected value.
    /// Each pair is passed to the test method as two parameters, the key and the expected string.
    /// </summary>
    public class KvpObjectStringSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        /// <summary>
        /// Gets the test data rows for the test method, one row per value in <see cref="DataSourceValues.ObjectData"/>.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One two-element array per value, containing the value and its string form. The string is <c>null</c> when the value is <c>null</c>.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
             foreach (var o in DataSourceValues.ObjectData)
             {
                 yield return new object[] {o, o?.ToString()};
             }
        }
    }
}
