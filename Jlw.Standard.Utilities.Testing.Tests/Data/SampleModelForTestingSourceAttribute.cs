using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Standard.Utilities.Testing.Tests.Data
{
    public class SampleModelForTestingSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {

        public class Child1 : SampleModelForTesting { }
        class Child2 : SampleModelForTesting { }
        class GrandChild1 : Child1 { }
        class GrandChild2 : Child2 { }

        /*
        class AdoptedChild1 : ISampleModelForTesting
        {
            public int PublicReadWriteInt { get; set; }
            public int PublicReadInt { get; }
            public int PublicWriteInt { get; set; }
            public uint PublicUIntMax()
            {
                throw new NotImplementedException();
            }
        }
        */

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { new SampleModelForTesting() };
            yield return new object[] { new Child1() };
            yield return new object[] { new Child2() };
            yield return new object[] { new GrandChild1() };
            yield return new object[] { new GrandChild2() };
        }

        public override string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            string name = data[0]?.GetType().Name;
            return string.Format(CultureInfo.CurrentCulture, "Instance of {0}", name ?? "null");
        }

    }
}