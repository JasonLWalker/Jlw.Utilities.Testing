using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies the <see cref="DataSourceValues.DateTimeData"/> values as test data for a test method parameter.
    /// Each value is passed to the test method as a separate test case.
    /// </summary>
    public class DateTimeSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        /// <summary>
        /// Gets the test data rows for the test method, one row per <see cref="DataSourceValues.DateTimeData"/> value.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One single-element array per value, containing that value.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            foreach (var value in DataSourceValues.DateTimeData)
            {
                yield return new object[] { value };
            }
        }
    }
}
