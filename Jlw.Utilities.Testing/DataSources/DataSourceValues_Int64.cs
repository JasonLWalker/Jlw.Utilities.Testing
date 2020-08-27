using System;
using System.Collections.Generic;

namespace Jlw.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        public static readonly object[] Int64Data =
        {
            0,
            1,
            10,
            100,
            Byte.MinValue,
            Byte.MaxValue,
            char.MinValue,
            char.MaxValue,
            Int16.MinValue,
            Int16.MaxValue,
            int.MinValue,
            int.MaxValue,
            long.MinValue,
            long.MaxValue,
        };

        public static readonly KeyValuePair<object, long?>[] KvpObjectInt64Data = {
            new KeyValuePair<object, long?>(0, 0),
            new KeyValuePair<object, long?>(1, 1),
            new KeyValuePair<object, long?>(10, 10),
            new KeyValuePair<object, long?>(100, 100),
            new KeyValuePair<object, long?>(Byte.MinValue, (long)Byte.MinValue),
            new KeyValuePair<object, long?>(Byte.MaxValue, (long)Byte.MaxValue),
            new KeyValuePair<object, long?>(char.MinValue, (long)char.MinValue),
            new KeyValuePair<object, long?>(char.MaxValue, (long)char.MaxValue),
            new KeyValuePair<object, long?>(int.MinValue, (long)int.MinValue),
            new KeyValuePair<object, long?>(int.MaxValue, (long)int.MaxValue),
            new KeyValuePair<object, long?>(Int16.MinValue, (long)Int16.MinValue),
            new KeyValuePair<object, long?>(Int16.MaxValue, (long)Int16.MaxValue),
            new KeyValuePair<object, long?>(Int32.MinValue, (long)int.MinValue),
            new KeyValuePair<object, long?>(Int32.MaxValue, (long)int.MaxValue),
            new KeyValuePair<object, long?>(Int64.MinValue, long.MinValue),
            new KeyValuePair<object, long?>(Int64.MaxValue, long.MaxValue),
            new KeyValuePair<object, long?>(null, 0),
            new KeyValuePair<object, long?>(true, 1),
            new KeyValuePair<object, long?>(false, 0),
            new KeyValuePair<object, long?>("True", 0),
            new KeyValuePair<object, long?>("False", 0),
            new KeyValuePair<object, long?>("", 0),
            new KeyValuePair<object, long?>(" ", 0),
            new KeyValuePair<object, long?>(" \t\v\r\n", 0),
            new KeyValuePair<object, long?>("Test123", 123),
            new KeyValuePair<object, long?>("123Test", 123),
            new KeyValuePair<object, long?>("1234.567", 1234),
            new KeyValuePair<object, long?>("12345.678.9", 0),
            new KeyValuePair<object, long?>("127.0.1.2", 0),
            new KeyValuePair<object, long?>("1", 1),
            new KeyValuePair<object, long?>("10", 10),
            new KeyValuePair<object, long?>("100", 100),
        };
    }
}