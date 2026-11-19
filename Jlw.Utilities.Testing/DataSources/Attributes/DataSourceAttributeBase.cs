using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Base class for the data source attributes. Provides the display name shown for each generated test case.
    /// </summary>
    public class DataSourceAttributeBase : Attribute
    {
        /// <summary>
        /// Gets the display name for a test case, showing the test method name and the data values passed to it.
        /// </summary>
        /// <remarks>
        /// When the test case has two values, both are shown with their type names and JSON values.
        /// Otherwise only the first value is shown.
        /// </remarks>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <param name="data">The data values for the test case.</param>
        /// <returns>The display name for the test case.</returns>
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
