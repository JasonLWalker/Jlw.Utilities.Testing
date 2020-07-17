using System;
using System.Collections.Generic;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public partial class DataSourceValues
    {
        public static readonly double[] DoubleData =
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
            Double.MinValue,
            Double.MaxValue,
            Double.Epsilon,
        };

        public static readonly KeyValuePair<object, double?>[] ObjectDoubleData = {
            new KeyValuePair<object, double?>(0, 0),
            new KeyValuePair<object, double?>(1, 1),
            new KeyValuePair<object, double?>(10, 10),
            new KeyValuePair<object, double?>(100, 100),
            new KeyValuePair<object, double?>(Byte.MinValue, Byte.MinValue),
            new KeyValuePair<object, double?>(Byte.MaxValue, Byte.MaxValue),
            new KeyValuePair<object, double?>(char.MinValue, char.MinValue),
            new KeyValuePair<object, double?>(char.MaxValue, char.MaxValue),
            new KeyValuePair<object, double?>(int.MinValue, int.MinValue),
            new KeyValuePair<object, double?>(int.MaxValue, int.MaxValue),
            new KeyValuePair<object, double?>(Int16.MinValue, Int16.MinValue),
            new KeyValuePair<object, double?>(Int16.MaxValue, Int16.MaxValue),
            new KeyValuePair<object, double?>(Int32.MinValue, Int32.MinValue),
            new KeyValuePair<object, double?>(Int32.MaxValue, Int32.MaxValue),
            new KeyValuePair<object, double?>(Single.MinValue, Single.MinValue),
            new KeyValuePair<object, double?>(Single.MaxValue, Single.MaxValue),
            new KeyValuePair<object, double?>(Single.Epsilon, Single.Epsilon),
            new KeyValuePair<object, double?>(Single.NaN, Single.NaN),
            new KeyValuePair<object, double?>(Single.NegativeInfinity, Single.NegativeInfinity),
            new KeyValuePair<object, double?>(Single.PositiveInfinity, Single.PositiveInfinity),
            new KeyValuePair<object, double?>(Double.MinValue, Double.MinValue),
            new KeyValuePair<object, double?>(Double.MaxValue, Double.MaxValue),
            new KeyValuePair<object, double?>(Double.Epsilon, Double.Epsilon),
            new KeyValuePair<object, double?>(Double.NaN, Double.NaN),
            new KeyValuePair<object, double?>(Double.NegativeInfinity, Double.NegativeInfinity),
            new KeyValuePair<object, double?>(Double.PositiveInfinity, Double.PositiveInfinity),
            new KeyValuePair<object, double?>(null, 0),
            new KeyValuePair<object, double?>(true, 1),
            new KeyValuePair<object, double?>(false, 0),
            new KeyValuePair<object, double?>("True", 0),
            new KeyValuePair<object, double?>("False", 0),
            new KeyValuePair<object, double?>("", 0),
            new KeyValuePair<object, double?>(" ", 0),
            new KeyValuePair<object, double?>(" \t\v\r\n", 0),
            new KeyValuePair<object, double?>("Test123", 123),
            new KeyValuePair<object, double?>("123Test", 123),
            new KeyValuePair<object, double?>("1234.567", 1234.567),
            new KeyValuePair<object, double?>("12345.678.9", 0),
            new KeyValuePair<object, double?>("127.0.1.2", 0),
            new KeyValuePair<object, double?>("1", 1),
            new KeyValuePair<object, double?>("10", 10),
            new KeyValuePair<object, double?>("100", 100),
        };
    }
}