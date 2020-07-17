using System;
using System.Collections.Generic;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        public static readonly bool[] BoolData =
        {
            false,
            true
        };

        public static readonly KeyValuePair<object, bool?>[] ObjectBoolData = {
            new KeyValuePair<object, bool?>(0, false),
            new KeyValuePair<object, bool?>(1, true),
            new KeyValuePair<object, bool?>(10, false),
            new KeyValuePair<object, bool?>(100, false),
            new KeyValuePair<object, bool?>(Byte.MinValue, false),
            new KeyValuePair<object, bool?>(Byte.MaxValue, false),
            new KeyValuePair<object, bool?>(char.MinValue, false),
            new KeyValuePair<object, bool?>(char.MaxValue, false),
            new KeyValuePair<object, bool?>(int.MinValue, false),
            new KeyValuePair<object, bool?>(int.MaxValue, false),
            new KeyValuePair<object, bool?>(Int16.MinValue, false),
            new KeyValuePair<object, bool?>(Int16.MaxValue, false),
            new KeyValuePair<object, bool?>(Int32.MinValue, false),
            new KeyValuePair<object, bool?>(Int32.MaxValue, false),
            new KeyValuePair<object, bool?>(null, false),
            new KeyValuePair<object, bool?>(true, true),
            new KeyValuePair<object, bool?>(false, false),
            new KeyValuePair<object, bool?>("True", true),
            new KeyValuePair<object, bool?>("False", false),
            new KeyValuePair<object, bool?>("", false),
            new KeyValuePair<object, bool?>(" ", false),
            new KeyValuePair<object, bool?>(" \t\v\r\n", false),
            new KeyValuePair<object, bool?>("Test123", false),
            new KeyValuePair<object, bool?>("123Test", false),
            new KeyValuePair<object, bool?>("1234.567", false),
            new KeyValuePair<object, bool?>("12345.678.9", false),
            new KeyValuePair<object, bool?>("127.0.1.2", false),
            new KeyValuePair<object, bool?>("0", false),
            new KeyValuePair<object, bool?>("1", true),
            new KeyValuePair<object, bool?>("10", false),
            new KeyValuePair<object, bool?>("100", false),
            new KeyValuePair<object, bool?>("T", true),
            new KeyValuePair<object, bool?>("F", false),
            new KeyValuePair<object, bool?>("t", true),
            new KeyValuePair<object, bool?>("f", false),
            new KeyValuePair<object, bool?>("Y", true),
            new KeyValuePair<object, bool?>("N", false),
            new KeyValuePair<object, bool?>("y", true),
            new KeyValuePair<object, bool?>("n", false),
            new KeyValuePair<object, bool?>("yEs", true),
            new KeyValuePair<object, bool?>("nO", false),
        
        };
    }
}