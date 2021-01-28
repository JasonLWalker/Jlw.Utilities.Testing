using System;
using System.Reflection;
using Jlw.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BaseModelUtilityTests
{
    [TestClass]
    public class BaseModelUtility_AssertPropertyGet : BaseModelUtility<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, false)]
        public void Should_Succeed_ForReadonlyProperties(string name)
        {
            object o = AssertPropertyGet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            Console.WriteLine($"{name} : {o}");
        }


        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, true)]
        public void Should_Succeed_ForReadWriteProperties(string name)
        {
            object o = AssertPropertyGet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            Console.WriteLine($"{name} : {o}");
        }

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), false, true)]
        public void Should_Fail_ForWriteonlyProperties(string name)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                object o = AssertPropertyGet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                Console.WriteLine($"{name} : {o}");
                throw new Exception($"the property '{name}' should fail, but didn't.");
            });
            StringAssert.Contains(ex.Message, $"{name} is not a readable property");
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
                object o = AssertPropertyGet(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                Console.WriteLine($"{name} : {o}");
                throw new Exception($"the property '{name}' should not be found, but was.");
            });
            StringAssert.Contains(ex.Message, $"does not contain a property with the name '{name}'");
        }

    }
}