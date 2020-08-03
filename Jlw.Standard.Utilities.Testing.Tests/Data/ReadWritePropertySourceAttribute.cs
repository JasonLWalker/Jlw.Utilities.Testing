using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Standard.Utilities.Testing.Tests.Data
{
    public class ReadWritePropertyNameSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        protected Type _type;
        protected IEnumerable<PropertyInfo> _props;
        protected BindingFlags _flags;
        protected bool _canRead;
        protected bool _canWrite;

        public ReadWritePropertyNameSourceAttribute(Type type, bool canRead, bool canWrite)
        {
            _type = type;
            _flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            _props = _type?.GetProperties(_flags);
            _canRead = canRead;
            _canWrite = canWrite;
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            PropertyInfo o;
            foreach (var p in _props)
            {
                o = _type.GetProperty(p.Name, _flags);

                var bMatch = false;

                bMatch = (_canRead == (o?.CanRead == true)) & (_canWrite == (o?.CanWrite == true));

                if (o != null  && bMatch)
                {
                    yield return new object[] { p.Name };
                }
            }
        }

        public override string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            string name = data[0]?.ToString();
            return string.Format(CultureInfo.CurrentCulture, "{0}", name ?? "null");
        }

    }
}