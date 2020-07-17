using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public class ObjectDoubleSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
             foreach (var kvp in DataSourceValues.ObjectDoubleData)
             {
                 yield return new object[] {kvp.Key, kvp.Value};
             }
        }
    }
}