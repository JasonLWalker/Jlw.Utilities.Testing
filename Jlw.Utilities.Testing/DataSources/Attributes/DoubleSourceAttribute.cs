using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies the <see cref="DataSourceValues.DoubleData"/> values as test data for a test method parameter.
    /// Each value is passed to the test method as a separate test case.
    /// </summary>
    public class DoubleSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        /// <summary>
        /// Gets the test data rows for the test method, one row per <see cref="DataSourceValues.DoubleData"/> value.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One single-element array per value, containing that value.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            foreach (var value in DataSourceValues.DoubleData)
            {
                yield return new object[] {value};
            }
        }
    }
}
