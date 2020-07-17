using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public class DoubleSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            foreach (var value in DataSourceValues.DoubleData)
            {
                yield return new object[] {value};
            }
        }
    }
}