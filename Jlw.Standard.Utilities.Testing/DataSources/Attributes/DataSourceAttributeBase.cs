using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public class DataSourceAttributeBase : Attribute
    {
        public virtual string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            switch (data?.Length)
            {
                case 2:
                    return string.Format(CultureInfo.CurrentCulture, "{0} ({1}, {2})", methodInfo.Name, (data[0] != null ? "" + data[0]?.GetType().Name + "<" + JsonConvert.SerializeObject(data[0]) + ">" : "null"), (data[1] != null ? "" + data[1]?.GetType().Name + "<" + JsonConvert.SerializeObject(data[1]) + ">" : "null"));
                default:
                    return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", methodInfo.Name, (data[0] != null ? "" + data[0]?.GetType().Name + "<" + JsonConvert.SerializeObject(data[0]) + ">" : "null"));
            }
        }


    }
}
