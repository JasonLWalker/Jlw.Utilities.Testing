using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public class Int64SourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            foreach (var value in DataSourceValues.Int64Data)
            {
                yield return new object[] {value};
            }
        }
    }
}