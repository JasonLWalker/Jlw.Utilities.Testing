using System;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using Jlw.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixture_AssertPropertyExists : BaseModelFixture<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        #region private keyword tests 
        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Private)]
        public void Should_Match_ForPrivateInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Private);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Private | MethodAttributes.Static)]
        public void Should_Match_ForPrivateStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Private | MethodAttributes.Static);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Private, false)]
        public void Should_NotMatch_ForNonPrivateInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Private);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Private | MethodAttributes.Static, false)]
        public void Should_NotMatch_ForNonPrivateNonStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Private | MethodAttributes.Static);
        }
        #endregion

        #region protected keyword tests 
        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Family)]
        public void Should_Match_ForProtectedInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Family);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Family | MethodAttributes.Static)]
        public void Should_Match_ForProtectedStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Family | MethodAttributes.Static);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Family, false)]
        public void Should_NotMatch_ForNonProtectedInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Family);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Family | MethodAttributes.Static, false)]
        public void Should_NotMatch_ForNonProtectedNonStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Family | MethodAttributes.Static);
        }
        #endregion

        #region internal keyword tests 
        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Assembly)]
        public void Should_Match_ForInternalInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Assembly);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Assembly | MethodAttributes.Static)]
        public void Should_Match_ForInternalStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Assembly | MethodAttributes.Static);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Assembly, false)]
        public void Should_NotMatch_ForNonInternalInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Assembly);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Assembly | MethodAttributes.Static, false)]
        public void Should_NotMatch_ForNonInternalNonStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Assembly | MethodAttributes.Static);
        }
        #endregion

        #region protected internal keyword tests 
        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamORAssem)]
        public void Should_Match_ForProtectedInternalInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.FamORAssem);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamORAssem | MethodAttributes.Static)]
        public void Should_Match_ForProtectedInternalStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.FamORAssem | MethodAttributes.Static);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamORAssem, false)]
        public void Should_NotMatch_ForNonProtectedInternalInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.FamORAssem);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamORAssem | MethodAttributes.Static, false)]
        public void Should_NotMatch_ForNonProtectedInternalNonStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.FamORAssem | MethodAttributes.Static);
        }
        #endregion

        #region private protected keyword tests 
        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamANDAssem)]
        public void Should_Match_ForPrivateProtectedInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.FamANDAssem);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamANDAssem | MethodAttributes.Static)]
        public void Should_Match_ForPrivateProtectedStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.FamANDAssem | MethodAttributes.Static);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamANDAssem, false)]
        public void Should_NotMatch_ForNonPrivateProtectedInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.FamANDAssem);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.FamANDAssem | MethodAttributes.Static, false)]
        public void Should_NotMatch_ForNonPrivateProtectedNonStaticInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.FamANDAssem | MethodAttributes.Static);
        }
        #endregion

        #region public keyword tests
        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Public)]
        public void Should_Match_ForPublicInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Public);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), BindingFlags.Public | BindingFlags.Static)]
        public void Should_Match_ForPublicStaticProperties(PropertyInfo pi)
        {
            AssertPropertyAccessMatches(pi.Name, MethodAttributes.Public | MethodAttributes.Static);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Public, false)]
        public void Should_NotMatch_ForNonPublicInstanceProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Public);
        }

        [TestMethod]
        [ModelPropertyInfoSource(typeof(SampleModelForTesting), MethodAttributes.Public | MethodAttributes.Static, false)]
        public void Should_NotMatch_ForNonPublicNonStaticProperties(PropertyInfo pi)
        {
            AssertPropertyAccessDoesNotMatch(pi.Name, MethodAttributes.Public | MethodAttributes.Static);
        }
        #endregion

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
                var p = AssertPropertyExists(name);
                throw new Exception($"the property '{name}' should not be found, but was.");
            });
            StringAssert.Contains(ex.Message, $"does not contain a property with the name '{name}'");

        }

        [TestMethod]
        [DataRow("PublicReadWriteInt", BindingFlags.Public | BindingFlags.Instance)]
        [DataRow("PublicStaticReadWriteInt", BindingFlags.Public | BindingFlags.Static)]
        [DataRow("PrivateReadWriteShort", BindingFlags.NonPublic | BindingFlags.Instance)]
        [DataRow("PrivateStaticReadWriteShort", BindingFlags.NonPublic | BindingFlags.Static)]
        public void Should_NotReturnNull_ForCorrectlyBoundProperty(string name, BindingFlags flags)
        {
            var p = AssertPropertyExists(name);
            Assert.IsNotNull(p);
        }

        // Use local assertion helpers to ensure only GetPropertyInfoByName is tested
        #region Assert helpers
        public void AssertPropertyAccessMatches(string name, MethodAttributes attrExpected)
        {
            PropertyInfo p = AssertPropertyExists(name);
            MethodAttributes getAttr = p?.GetMethod?.Attributes ?? 0;
            MethodAttributes setAttr = p?.SetMethod?.Attributes ?? 0;
            MethodAttributes m = getAttr | setAttr;

            Console.WriteLine($"Name: {p?.Name}, MemberType: {p?.MemberType}, getAttributes: {getAttr}, setAttributes: {setAttr}, Attributes: {m}");
            
            Assert.AreEqual(attrExpected & KeywordMask, m & KeywordMask);
        }

        public void AssertPropertyAccessDoesNotMatch(string name, MethodAttributes attrExpected)
        {
            PropertyInfo p = AssertPropertyExists(name);
            MethodAttributes getAttr = p?.GetMethod?.Attributes ?? 0;
            MethodAttributes setAttr = p?.SetMethod?.Attributes ?? 0;
            MethodAttributes m = getAttr | setAttr;

            Console.WriteLine($"Name: {p?.Name}, MemberType: {p?.MemberType}, getAttributes: {getAttr}, setAttributes: {setAttr}, Attributes: {m}");
            
            Assert.AreNotEqual(attrExpected & KeywordMask, m & KeywordMask);
        }
        #endregion
    }
}