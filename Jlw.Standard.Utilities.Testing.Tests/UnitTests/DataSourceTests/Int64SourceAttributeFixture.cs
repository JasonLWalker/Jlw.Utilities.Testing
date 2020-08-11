using System;
using System.Reflection;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class Int64SourceAttributeFixture
    {
        [TestMethod]
        [Int64Source]
        public void Should_BeAssignableTo_Int64(object o)
        {
            Int64 n = 0;
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
            }
            Assert.AreEqual(DataUtility.ParseInt64(o), n);
        }

        [TestMethod]
        [Int64Source]
        public void Should_BeGreaterThanOrEqualTo_Int64MinValue_ForParsedValue(object o)
        {
            decimal d = DataUtility.ParseDecimal(o);

            Assert.IsTrue(d >= Int64.MinValue, $"The value returned <{o}> is less than Int64.MinValue <{Int64.MinValue}>");
        }


        [TestMethod]
        [Int64Source]
        public void Should_BeLessThanOrEqualTo_Int64MaxValue_ForParsedValue(object o)
        {
            decimal d = DataUtility.ParseDecimal(o);

            Assert.IsTrue(d <= Int64.MaxValue, $"The value returned <{o}> is greater than Int64.MaxValue <{Int64.MaxValue}>");
        }


        [TestMethod]
        [Int64Source]
        public void ShouldNot_BeNull(object o)
        {
            Assert.IsNotNull(o);
        }

    }
}
