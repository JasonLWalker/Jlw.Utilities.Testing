using System;
using System.Reflection;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class SingleSourceAttributeFixture
    {
        [TestMethod]
        [SingleSource]
        public void Should_BeAssignableTo_Single(object o)
        {
            Single n = 0;
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
            }
            Assert.AreEqual(DataUtility.ParseSingle(o), n);
        }

        [TestMethod]
        [SingleSource]
        public void Should_BeGreaterThanOrEqualTo_SingleMinValue_ForParsedValue(object o)
        {
            double d = DataUtility.ParseDouble(o);
            Single min = Single.MinValue;
            switch (d)
            {
                case Single.NegativeInfinity:
                    Assert.AreEqual(o, Single.NegativeInfinity);
                    break;
                case Single.PositiveInfinity:
                    Assert.AreEqual(o, Single.PositiveInfinity);
                    break;
                case Single.NaN:
                    Assert.AreEqual(o, Single.NaN);
                    break;
                default:
                    Assert.IsTrue(d >= (double)min, $"The value returned <{o}> is less than Single.MinValue <{Single.MinValue}>");
                    break;
            }
        }


        [TestMethod]
        [SingleSource]
        public void Should_BeLessThanOrEqualTo_SingleMaxValue_ForParsedValue(object o)
        {
            double d = DataUtility.ParseDouble(o);
            Single max = Single.MaxValue;
            switch (d)
            {
                case Single.NegativeInfinity:
                    Assert.AreEqual(o, Single.NegativeInfinity);
                    break;
                case Single.PositiveInfinity:
                    Assert.AreEqual(o, Single.PositiveInfinity);
                    break;
                case Single.NaN:
                    Assert.AreEqual(o, Single.NaN);
                    break;
                default:
                    Assert.IsTrue(d <= (double)max, $"The value returned <{o}> is greater than Single.MaxValue <{Single.MaxValue}>");
                    break;
            }
        }


        [TestMethod]
        [SingleSource]
        public void ShouldNot_BeNull(object o)
        {
            Assert.IsNotNull(o);
        }

    }
}
