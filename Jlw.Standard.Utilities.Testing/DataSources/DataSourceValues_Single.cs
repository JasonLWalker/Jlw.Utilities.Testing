using System;
using System.Collections.Generic;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        public static readonly float[] SingleData =
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
            Single.MinValue,
            Single.MaxValue,
            Single.Epsilon,
            Single.NaN,
            Single.NegativeInfinity,
            Single.PositiveInfinity,
        };

        public static readonly KeyValuePair<object, float?>[] ObjectSingleData = {
            new KeyValuePair<object, float?>(0, 0),
            new KeyValuePair<object, float?>(1, 1),
            new KeyValuePair<object, float?>(10, 10),
            new KeyValuePair<object, float?>(100, 100),
            new KeyValuePair<object, float?>(Byte.MinValue, Byte.MinValue),
            new KeyValuePair<object, float?>(Byte.MaxValue, Byte.MaxValue),
            new KeyValuePair<object, float?>(char.MinValue, char.MinValue),
            new KeyValuePair<object, float?>(char.MaxValue, char.MaxValue),
            new KeyValuePair<object, float?>(int.MinValue, int.MinValue),
            new KeyValuePair<object, float?>(int.MaxValue, int.MaxValue),
            new KeyValuePair<object, float?>(Int16.MinValue, Int16.MinValue),
            new KeyValuePair<object, float?>(Int16.MaxValue, Int16.MaxValue),
            new KeyValuePair<object, float?>(Int32.MinValue, Int32.MinValue),
            new KeyValuePair<object, float?>(Int32.MaxValue, Int32.MaxValue),
            new KeyValuePair<object, float?>(Single.MinValue, Single.MinValue),
            new KeyValuePair<object, float?>(Single.MaxValue, Single.MaxValue),
            new KeyValuePair<object, float?>(Single.Epsilon, Single.Epsilon),
            new KeyValuePair<object, float?>(Single.NaN, Single.NaN),
            new KeyValuePair<object, float?>(Single.NegativeInfinity, Single.NegativeInfinity),
            new KeyValuePair<object, float?>(Single.PositiveInfinity, Single.PositiveInfinity),
            new KeyValuePair<object, float?>(null, 0),
            new KeyValuePair<object, float?>(true, 1),
            new KeyValuePair<object, float?>(false, 0),
            new KeyValuePair<object, float?>("True", 0),
            new KeyValuePair<object, float?>("False", 0),
            new KeyValuePair<object, float?>("", 0),
            new KeyValuePair<object, float?>(" ", 0),
            new KeyValuePair<object, float?>(" \t\v\r\n", 0),
            new KeyValuePair<object, float?>("Test123", 123),
            new KeyValuePair<object, float?>("123Test", 123),
            new KeyValuePair<object, float?>("1234.567", 1234),
            new KeyValuePair<object, float?>("12345.678.9", 0),
            new KeyValuePair<object, float?>("127.0.1.2", 0),
            new KeyValuePair<object, float?>("1", 1),
            new KeyValuePair<object, float?>("10", 10),
            new KeyValuePair<object, float?>("100", 100),
        };
    }
}