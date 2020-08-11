using System;
using System.Reflection;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class Int32SourceAttributeFixture
    {
        [TestMethod]
        [Int32Source]
        public void Should_BeAssignableTo_Int32(object o)
        {
            Int32 n = 0;
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
            }
            Assert.AreEqual(DataUtility.ParseInt32(o), n);
        }

        [TestMethod]
        [Int32Source]
        public void Should_BeGreaterThanOrEqualTo_Int32MinValue_ForParsedValue(object o)
        {
            decimal d = DataUtility.ParseDecimal(o);

            Assert.IsTrue(d >= Int32.MinValue, $"The value returned <{o}> is less than Int32.MinValue <{Int32.MinValue}>");
        }


        [TestMethod]
        [Int32Source]
        public void Should_BeLessThanOrEqualTo_Int32MaxValue_ForParsedValue(object o)
        {
            decimal d = DataUtility.ParseDecimal(o);

            Assert.IsTrue(d <= Int32.MaxValue, $"The value returned <{o}> is greater than Int32.MaxValue <{Int32.MaxValue}>");
        }


        [TestMethod]
        [Int32Source]
        public void ShouldNot_BeNull(object o)
        {
            Assert.IsNotNull(o);
        }

    }
}
