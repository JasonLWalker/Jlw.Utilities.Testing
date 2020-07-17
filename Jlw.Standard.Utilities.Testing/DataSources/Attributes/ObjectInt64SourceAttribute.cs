using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public class ObjectInt64SourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
             foreach (var kvp in DataSourceValues.ObjectInt64Data)
             {
                 yield return new object[] {kvp.Key, kvp.Value};
             }
        }
    }
}