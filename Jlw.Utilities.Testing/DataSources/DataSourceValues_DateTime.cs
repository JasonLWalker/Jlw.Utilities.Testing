using System;
using System.Collections.Generic;

namespace Jlw.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        /// <summary>
        /// Nullable date and time values used as test data, including <c>null</c> and several boundary dates.
        /// </summary>
        public static readonly DateTime?[] NullableDateTimeData =
        {
            null,
            DateTime.MinValue,
            DateTime.MaxValue,
            DateTime.Now, 
            DateTime.Today, 
            DateTime.UtcNow, 
        };

        /// <summary>
        /// Date and time values used as test data, including boundary dates and the current date and time.
        /// </summary>
        public static readonly DateTime[] DateTimeData =
        {
            DateTime.MinValue,
            DateTime.MaxValue,
            DateTime.Now, 
            DateTime.Today, 
            DateTime.UtcNow, 
        };

        /// <summary>
        /// The local date and time captured when the class is first used, used as a test value.
        /// </summary>
        public static readonly DateTime dtNow = DateTime.Now;
        /// <summary>
        /// The UTC date and time captured when the class is first used, used as a test value.
        /// </summary>
        public static readonly DateTime dtUtcNow = DateTime.UtcNow;

        /// <summary>
        /// Key and expected value pairs for nullable date and time tests. Each key is a value of another type, such as a number or string,
        /// and the expected value is the date it is expected to convert to, or <c>null</c>.
        /// </summary>
        public static readonly KeyValuePair<object, DateTime?>[] KvpObjectNullableDateTimeData = {
            new KeyValuePair<object, DateTime?>(0, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(1, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(10, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(100, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Byte.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Byte.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(char.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(char.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(int.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(int.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Int16.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Int16.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Int32.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Int32.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Int64.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Int64.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Single.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Single.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Single.Epsilon, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Single.NaN, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Single.NegativeInfinity, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Single.PositiveInfinity, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Double.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Double.MaxValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Double.Epsilon, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Double.NaN, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Double.NegativeInfinity, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(Double.PositiveInfinity, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(null, null),
            new KeyValuePair<object, DateTime?>(true, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(false, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("True", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("False", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("", null),
            new KeyValuePair<object, DateTime?>(" ", null),
            new KeyValuePair<object, DateTime?>(" \t\v\r\n", null),
            new KeyValuePair<object, DateTime?>("Test123", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("123Test", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("1234.567", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("12345.678.9", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("127.0.1.2", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("0", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("1", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("10", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("100", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("T", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("F", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("t", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("f", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("Y", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("N", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("y", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("n", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("yEs", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("nO", DateTime.MinValue),
            new KeyValuePair<object, DateTime?>("2001-02-03", new DateTime(2001, 02, 03)),
            new KeyValuePair<object, DateTime?>("04/05/2006", new DateTime(2006, 04, 05)),
            new KeyValuePair<object, DateTime?>("1976-05-31T09:15:30", new DateTime(1976, 05, 31, 09, 15, 30)),
            new KeyValuePair<object, DateTime?>(DateTime.MinValue, DateTime.MinValue),
            new KeyValuePair<object, DateTime?>(DateTime.MaxValue, DateTime.MaxValue),
            new KeyValuePair<object, DateTime?>(DateTime.Today, DateTime.Today),
            new KeyValuePair<object, DateTime?>(dtNow, dtNow),
            new KeyValuePair<object, DateTime?>(dtUtcNow, dtUtcNow),
        };
    }
}
