using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies the values of <see cref="DataSourceValues.ObjectData"/> converted to strings as test data for a test method parameter.
    /// Each value is passed to the test method as a separate test case.
    /// </summary>
    public class StringSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        /// <summary>
        /// Gets the test data rows for the test method, one row per <see cref="DataSourceValues.ObjectData"/> value, converted with <c>ToString</c>.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One single-element array per value, containing the string form of that value. The string is <c>null</c> when the value is <c>null</c>.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            foreach (var value in DataSourceValues.ObjectData)
            {
                yield return new object[] {value?.ToString()};
            }
        }
    }
}
