using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies the boolean values <c>false</c> and <c>true</c> as test data for a test method parameter.
    /// </summary>
    /// <remarks>
    /// Each value is passed to the test method as a separate test case, using the values in <see cref="DataSourceValues.BoolData"/>.
    /// </remarks>
    public class BooleanSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        /// <summary>
        /// Gets the test data rows for the test method, one row per boolean value.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One single-element array per boolean value, containing that value.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            foreach (var value in DataSourceValues.BoolData)
            {
                yield return new object[] {value};
            }
        }
    }
}
