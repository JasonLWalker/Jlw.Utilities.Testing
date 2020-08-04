using System;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using Jlw.Standard.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixture_AssertGetPropertyValueByName : BaseModelFixture<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, false)]
        public void Should_Succeed_ForReadonlyProperties(string name)
        {
            object o = AssertGetPropertyValueByName(DefaultInstance, name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            Console.WriteLine($"{name} : {o}");
        }


        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, true)]
        public void Should_Succeed_ForReadWriteProperties(string name)
        {
            object o = AssertGetPropertyValueByName(DefaultInstance, name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            Console.WriteLine($"{name} : {o}");
        }

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), false, true)]
        public void Should_Fail_ForWriteonlyProperties(string name)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                object o = AssertGetPropertyValueByName(DefaultInstance, name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
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
                object o = AssertGetPropertyValueByName(DefaultInstance, name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                Console.WriteLine($"{name} : {o}");
                throw new Exception($"the property '{name}' should not be found, but was.");
            });
            StringAssert.Contains(ex.Message, $"does not contain a property with the name '{name}'");
        }

    }
}