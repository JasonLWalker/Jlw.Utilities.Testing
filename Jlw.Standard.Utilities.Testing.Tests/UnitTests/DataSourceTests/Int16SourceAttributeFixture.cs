using System;
using System.Reflection;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.DataSourceTests
{
    [TestClass]
    public class Int16SourceAttributeFixture
    {
        [TestMethod]
        [Int16Source]
        public void Should_BeAssignableTo_Int16(object o)
        {
            Int16 n = 0;
            switch (Type.GetTypeCode(o?.GetType()))
            {
                case TypeCode.Byte:
                    n = (Byte)(o??0);
                    break;
                case TypeCode.Int16:
                    n = (Int16)(o??0);
                    break;
            }
            Assert.AreEqual(DataUtility.ParseInt16(o), n);
        }

        [TestMethod]
        [Int16Source]
        public void Should_BeGreaterThanOrEqualTo_Int16MinValue_ForParsedValue(object o)
        {
            decimal d = DataUtility.ParseDecimal(o);

            Assert.IsTrue(d >= Int16.MinValue, $"The value returned <{o}> is less than Int16.MinValue <{Int16.MinValue}>");
        }


        [TestMethod]
        [Int16Source]
        public void Should_BeLessThanOrEqualTo_Int16MaxValue_ForParsedValue(object o)
        {
            decimal d = DataUtility.ParseDecimal(o);

            Assert.IsTrue(d <= Int16.MaxValue, $"The value returned <{o}> is greater than Int16.MaxValue <{Int16.MaxValue}>");
        }


        [TestMethod]
        [Int16Source]
        public void ShouldNot_BeNull(object o)
        {
            Assert.IsNotNull(o);
        }

    }
}
