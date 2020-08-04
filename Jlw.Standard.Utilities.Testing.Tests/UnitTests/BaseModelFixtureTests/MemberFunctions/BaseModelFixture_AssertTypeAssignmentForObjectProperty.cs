using System;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using Jlw.Standard.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixture_AssertTypeAssignmentForObjectProperty : BaseModelFixture<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, false)]
        public void Should_Succeed_ForReadonlyProperties(string name)
        {
            var p = GetPropertyInfoByName(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            AssertTypeAssignmentForObjectProperty(DefaultInstance, name, p.PropertyType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }


        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), true, true)]
        public void Should_Succeed_ForReadWriteProperties(string name)
        {
            var p = GetPropertyInfoByName(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            AssertTypeAssignmentForObjectProperty(DefaultInstance, name, p.PropertyType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }

        [TestMethod]
        [ReadWritePropertyNameSource(typeof(SampleModelForTesting), false, true)]
        public void Should_Succeed_ForWriteonlyProperties(string name)
        {
            var p = GetPropertyInfoByName(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            AssertTypeAssignmentForObjectProperty(DefaultInstance, name, p.PropertyType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
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
                AssertTypeAssignmentForObjectProperty(DefaultInstance, name, typeof(object), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                throw new Exception($"the property '{name}' should not be found, but was.");
            });
            StringAssert.Contains(ex.Message, $"does not contain a property with the name '{name}'");
        }

        [TestMethod]
        public void Should_Fail_ForNullPropertyName()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                AssertTypeAssignmentForObjectProperty(DefaultInstance, null, typeof(object), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            });
            StringAssert.Contains(ex.Message, $"Value cannot be null.");
        }

    }
}