using System;
using System.Collections.Generic;

namespace Jlw.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        public static readonly object[] Int16Data =
        {
            (Int16)0,
            (Int16)1,
            (Int16)10,
            (Int16)100,
            Byte.MinValue,
            Byte.MaxValue,
            Int16.MinValue,
            Int16.MaxValue,
        };

        public static readonly KeyValuePair<object, Int16?>[] KvpObjectInt16Data = {
            new KeyValuePair<object, Int16?>(0, 0),
            new KeyValuePair<object, Int16?>(1, 1),
            new KeyValuePair<object, Int16?>(10, 10),
            new KeyValuePair<object, Int16?>(100, 100),
            new KeyValuePair<object, Int16?>(Byte.MinValue, (Int16)Byte.MinValue),
            new KeyValuePair<object, Int16?>(Byte.MaxValue, (Int16)Byte.MaxValue),
            new KeyValuePair<object, Int16?>(char.MinValue, (Int16)char.MinValue),
            new KeyValuePair<object, Int16?>('\u7fff', (Int16)'\u7fff'),
            new KeyValuePair<object, Int16?>(Int16.MinValue, (Int16)Int16.MinValue),
            new KeyValuePair<object, Int16?>(Int16.MaxValue, (Int16)Int16.MaxValue),
            new KeyValuePair<object, Int16?>(null, 0),
            new KeyValuePair<object, Int16?>(true, 1),
            new KeyValuePair<object, Int16?>(false, 0),
            new KeyValuePair<object, Int16?>("True", 0),
            new KeyValuePair<object, Int16?>("False", 0),
            new KeyValuePair<object, Int16?>("", 0),
            new KeyValuePair<object, Int16?>(" ", 0),
            new KeyValuePair<object, Int16?>(" \t\v\r\n", 0),
            new KeyValuePair<object, Int16?>("Test123", 123),
            new KeyValuePair<object, Int16?>("123Test", 123),
            new KeyValuePair<object, Int16?>("1234.567", 1234),
            new KeyValuePair<object, Int16?>("12345.678.9", 0),
            new KeyValuePair<object, Int16?>("127.0.1.2", 0),
            new KeyValuePair<object, Int16?>("1", 1),
            new KeyValuePair<object, Int16?>("10", 10),
            new KeyValuePair<object, Int16?>("100", 100),
        };
    }
}