using System;
using System.Reflection;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BaseModelUtilityTests
{
    [TestClass]
    public class BaseModelUtility_AssertPropertySet : BaseModelUtility<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, false)]
        public void Should_Fail_ForReadonlyProperties(string name)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                AssertPropertySet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                throw new AssertSucceededException($"the property '{name}' should fail, but didn't.");
            });
            StringAssert.Contains(ex.Message, $"{name} is not a writable property");
        }


        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, true)]
        public void Should_Succeed_ForReadWriteProperties(string name)
        {
            var o = AssertPropertySet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var p = GetPropertyInfoByName(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var val = p.GetValue(o);
            Console.WriteLine($"{name} : {val}");
            Assert.AreEqual(DataUtility.ParseAs(val.GetType(), "1234567890.1234567890"), val);
        }

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), false, true)]
        public void Should_Succeed_ForWriteonlyProperties(string name)
        {
            AssertPropertySet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }


        [TestMethod]
        [DataRow("foo")]
        [DataRow("bar")]
        [DataRow("_")]
        [DataRow("")]
        [DataRow(" \t\n\r")]

        public void Should_Fail_ForNonexistentProperty(string name)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                object o = AssertPropertySet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                Console.WriteLine($"{name} : {o}");
                throw new Exception($"the property '{name}' should not be found, but was.");
            });
            StringAssert.Contains(ex.Message, $"does not contain a property with the name '{name}'");
        }

    }
}