using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.UnitTests.BaseModelFixtureTests
{
    [TestClass]
    public class BaseModelFixtureTests : BaseModelFixture<SampleModelForTesting>
    {
        // Helper constants
        private const MethodAttributes PublicMethod = MethodAttributes.Public;

        protected static void InitProperties()
        {
            AddProperty(typeof(int), "PublicStaticReadWriteInt", Public | Static, Public | Static);

            AddProperty(typeof(int), "PublicReadWriteInt", Public, Public);
            AddProperty(typeof(int), "PublicReadInt", Public, null);
            AddProperty(typeof(int), "PublicWriteInt", null, Public);
            AddProperty(typeof(DateTime?), "PublicNullDateTest", Public, null);

            AddProperty(typeof(SByte), "InternalStaticReadWriteSByte", Internal | Static, Internal | Static);

            AddProperty(typeof(SByte), "InternalReadWriteSByte", Internal, Internal);
            AddProperty(typeof(SByte), "InternalReadSByte", Internal, null);
            AddProperty(typeof(SByte), "InternalWriteSByte", null, Internal);

            AddProperty(typeof(float), "PrivateProtectedStaticReadWriteFloat", PrivateProtected | Static, PrivateProtected | Static);

            AddProperty(typeof(float), "PrivateProtectedReadWriteFloat", PrivateProtected, PrivateProtected);
            AddProperty(typeof(float), "PrivateProtectedReadFloat", PrivateProtected, null);
            AddProperty(typeof(float), "PrivateProtectedWriteFloat", null, PrivateProtected);

            AddProperty(typeof(short), "PrivateStaticReadWriteShort", Private | Static, Private | Static);

            AddProperty(typeof(short), "PrivateReadWriteShort", Private, Private);
            AddProperty(typeof(short), "PrivateReadShort", Private, null);
            AddProperty(typeof(short), "PrivateWriteShort", null, Private);

            AddProperty(typeof(long), "ProtectedStaticReadWriteLong", Protected | Static, Protected | Static);

            AddProperty(typeof(long), "ProtectedReadWriteLong", Protected, Protected);
            AddProperty(typeof(long), "ProtectedWriteLong", null, Protected);
            AddProperty(typeof(long), "ProtectedReadLong", Protected, null);

            AddProperty(typeof(double), "ProtectedInternalStaticReadWriteDouble", ProtectedInternal | Static, ProtectedInternal | Static);

            AddProperty(typeof(double), "ProtectedInternalReadWriteDouble", ProtectedInternal, ProtectedInternal);
            AddProperty(typeof(double), "ProtectedInternalReadDouble", ProtectedInternal, null);
            AddProperty(typeof(double), "ProtectedInternalWriteDouble", null, ProtectedInternal);
        }

        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            InitProperties();
        }


        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        [DataRow(Protected)]
        [DataRow(Protected | Static)]
        [DataRow(Internal)]
        [DataRow(Internal | Static)]
        [DataRow(ProtectedInternal)]
        [DataRow(ProtectedInternal | Static)]
        [DataRow(PrivateProtected)]
        [DataRow(PrivateProtected | Static)]
        [DataRow(Private)]
        [DataRow(Private | Static)]
        public override void Property_Count_ShouldMatch(AccessModifiers flags, bool flattenHierarchy = true)
        {
            base.Property_Count_ShouldMatch(flags, flattenHierarchy);
        }
        
    }
}
