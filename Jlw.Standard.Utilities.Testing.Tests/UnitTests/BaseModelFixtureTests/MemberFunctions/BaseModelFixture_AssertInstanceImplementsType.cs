using System;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using Jlw.Standard.Utilities.Data;
using Jlw.Standard.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Standard.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixture_AssertInstanceImplementsType : BaseModelFixture<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        [TestMethod]
        [DataRow("This is a test")]
        [DataRow(1234)]
        [DataRow(1.1234)]
        public void Should_Fail_ForNonImplementingTypes(object o)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() =>
            {
                AssertInstanceImplementsType(o);
            });
            StringAssert.Contains(ex.Message, $"is not an instance of");

        }

        [TestMethod]

        public void Should_Succeed_ForNull()
        {
            AssertInstanceImplementsType(null); // Null will cause the DefaultInstance to be used.
        }

        [TestMethod]
        [SampleModelForTestingSource]
        public void Should_Succeed_ForInstance(object o)
        {
            AssertInstanceImplementsType(o); // Null will cause the DefaultInstance to be used.
        }


    }
}