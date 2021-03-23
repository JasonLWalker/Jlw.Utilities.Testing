using System.Reflection;
using Jlw.Utilities.Testing.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BaseModelUtilityTests
{
    [TestClass]
    public class BaseModelUtility_AssertInstanceImplementsType : BaseModelUtility<SampleModelForTesting>
    {
        const MethodAttributes KeywordMask = MethodAttributes.MemberAccessMask | MethodAttributes.Static;

        [TestMethod]
        [DataRow("This is a test")]
        [DataRow(1234)]
        [DataRow(1.1234)]
        public void Should_Fail_ForNonImplementingTypes(object o)
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => AssertInstanceImplementsType(o));
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