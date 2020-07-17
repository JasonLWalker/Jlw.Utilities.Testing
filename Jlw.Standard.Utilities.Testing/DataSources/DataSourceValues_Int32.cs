using System;
using System.Collections.Generic;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        public static readonly object[] Int32Data =
        {
            0,
            1,
            10,
            100,
            Byte.MinValue,
            Byte.MaxValue,
            char.MinValue,
            char.MaxValue,
            int.MinValue,
            int.MaxValue,
            Int16.MinValue,
            Int16.MaxValue,
        };

        public static readonly KeyValuePair<object, int?>[] ObjectInt32Data = {
            new KeyValuePair<object, int?>(0, 0),
            new KeyValuePair<object, int?>(1, 1),
            new KeyValuePair<object, int?>(10, 10),
            new KeyValuePair<object, int?>(100, 100),
            new KeyValuePair<object, int?>(Byte.MinValue, (int)Byte.MinValue),
            new KeyValuePair<object, int?>(Byte.MaxValue, (int)Byte.MaxValue),
            new KeyValuePair<object, int?>(char.MinValue, (int)char.MinValue),
            new KeyValuePair<object, int?>(char.MaxValue, (int)char.MaxValue),
            new KeyValuePair<object, int?>(int.MinValue, int.MinValue),
            new KeyValuePair<object, int?>(int.MaxValue, int.MaxValue),
            new KeyValuePair<object, int?>(Int16.MinValue, (int)Int16.MinValue),
            new KeyValuePair<object, int?>(Int16.MaxValue, (int)Int16.MaxValue),
            new KeyValuePair<object, int?>(Int32.MinValue, int.MinValue),
            new KeyValuePair<object, int?>(Int32.MaxValue, int.MaxValue),
            new KeyValuePair<object, int?>(null, 0),
            new KeyValuePair<object, int?>(true, 1),
            new KeyValuePair<object, int?>(false, 0),
            new KeyValuePair<object, int?>("True", 0),
            new KeyValuePair<object, int?>("False", 0),
            new KeyValuePair<object, int?>("", 0),
            new KeyValuePair<object, int?>(" ", 0),
            new KeyValuePair<object, int?>(" \t\v\r\n", 0),
            new KeyValuePair<object, int?>("Test123", 123),
            new KeyValuePair<object, int?>("123Test", 123),
            new KeyValuePair<object, int?>("1234.567", 1234),
            new KeyValuePair<object, int?>("12345.678.9", 0),
            new KeyValuePair<object, int?>("127.0.1.2", 0),
            new KeyValuePair<object, int?>("1", 1),
            new KeyValuePair<object, int?>("10", 10),
            new KeyValuePair<object, int?>("100", 100),
        };
    }
}