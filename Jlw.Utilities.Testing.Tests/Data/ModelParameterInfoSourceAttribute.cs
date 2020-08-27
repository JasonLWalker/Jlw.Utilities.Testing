using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Utilities.Testing.Tests.Data
{
    public class ModelPropertyInfoSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;
        protected Type _type;
        protected IEnumerable<PropertyInfo> _props;
        protected BindingFlags _flags;
        protected MethodAttributes _methodAttr;
        protected bool _shouldMatch;

        public ModelPropertyInfoSourceAttribute(Type type, BindingFlags flags = 0, bool bShouldMatch = true)
        {
            _type = type;
            _props = _type?.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            _flags = flags;
            _methodAttr = 0;
            _shouldMatch = bShouldMatch;

        }

        public ModelPropertyInfoSourceAttribute(Type type, MethodAttributes methodAttr, bool bShouldMatch = true)
        {
            _type = type;
            _flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            _props = _type?.GetProperties(_flags);
            _methodAttr = methodAttr;
            _shouldMatch = bShouldMatch;

        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            PropertyInfo o;
            MethodAttributes m;
            MethodAttributes getAttr;
            MethodAttributes setAttr;
            bool bMatch;
            foreach (var p in _props)
            {

                o = _type.GetProperty(p.Name, _flags);
                getAttr = o?.GetMethod?.Attributes ?? 0;
                setAttr = o?.SetMethod?.Attributes ?? 0;
                m = getAttr | setAttr;
                if (_methodAttr == 0)
                {
                    bMatch = true;
                }
                else
                {
                    bMatch = (m & KeywordMask) == (_methodAttr & KeywordMask);
                }

                if ((o != null && _shouldMatch) && bMatch)
                {
                    yield return new object[] { p };
                }
                else if ((o == null && !_shouldMatch) || (!bMatch && !_shouldMatch))
                {
                    yield return new object[] { p };
                }
            }
        }

        public override string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            PropertyInfo p = data[0] as PropertyInfo;
            return string.Format(CultureInfo.CurrentCulture, "{0} : {1}", p?.Name ?? "null", p?.PropertyType.Name ?? "null");
        }

    }
}