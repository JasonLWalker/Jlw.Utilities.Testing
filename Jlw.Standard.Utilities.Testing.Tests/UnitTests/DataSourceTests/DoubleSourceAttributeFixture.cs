using System;
using System.Reflection;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class DoubleSourceAttributeFixture
    {
        [TestMethod]
        [DoubleSource]
        public void Should_BeAssignableTo_Double(object o)
        {
            Double n = 0;
            switch (Type.GetTypeCode(o?.GetType()))
            {
                case TypeCode.Byte:
                    n = (Byte)(o??0);
                    break;
                case TypeCode.Char:
                    n = (Char)(o??0);
                    break;
                case TypeCode.Int16:
                    n = (Int16)(o??0);
                    break;
                case TypeCode.Int32:
                    n = (Int32)(o??0);
                    break;
                case TypeCode.Int64:
                    n = (Int64)(o??0);
                    break;
                case TypeCode.Single:
                    n = (Single)(o??0);
                    break;
                case TypeCode.Double:
                    n = (Double)(o??0);
                    break;
            }
            Assert.AreEqual(DataUtility.ParseDouble(o), n);
        }

        [TestMethod]
        [DoubleSource]
        public void Should_BeGreaterThanOrEqualTo_DoubleMinValue_ForParsedValue(object o)
        {
            Double d = DataUtility.ParseDouble(o);
            switch (d)
            {
                
                case Double.NegativeInfinity:
                    Assert.AreEqual(o, Double.NegativeInfinity);
                    break;
                case Double.PositiveInfinity:
                    Assert.AreEqual(o, Double.PositiveInfinity);
                    break;
                case Double.NaN:
                    Assert.AreEqual(o, Double.NaN);
                    break;
                default:
                    Assert.IsTrue(d >= Double.MinValue, $"The value returned <{o}> is less than Double.MinValue <{Double.MinValue}>");
                    break;
            }
        }


        [TestMethod]
        [DoubleSource]
        public void Should_BeLessThanOrEqualTo_DoubleMaxValue_ForParsedValue(object o)
        {
            double d = DataUtility.ParseDouble(o);
            switch (d)
            {
                case Double.NegativeInfinity:
                    Assert.AreEqual(o, Double.NegativeInfinity);
                    break;
                case Double.PositiveInfinity:
                    Assert.AreEqual(o, Double.PositiveInfinity);
                    break;
                case Double.NaN:
                    Assert.AreEqual(o, Double.NaN);
                    break;
                default:
                    Assert.IsTrue(d <= Double.MaxValue, $"The value returned <{o}> is greater than Double.MaxValue <{Double.MaxValue}>");
                    break;
            }
        }


        [TestMethod]
        [DoubleSource]
        public void ShouldNot_BeNull(object o)
        {
            Assert.IsNotNull(o);
        }

    }
}
