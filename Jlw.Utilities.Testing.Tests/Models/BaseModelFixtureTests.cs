using System;
using System.Data;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests
{
    public class SampleSchema : BaseModelSchema<SampleModelForTesting>
    {
        protected void InitProperties()
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

        public void InitInterfaces()
        {
            //AddInterface(typeof(IDataRecord));
        }

        public void InitFields()
        {

            AddField(Internal, typeof(sbyte), "_internalSByte");
            AddField(Internal | Static, typeof(sbyte), "_internalStaticSByte");

            AddField(PrivateProtected, typeof(float), "_privateProtectedFloat");
            AddField(PrivateProtected | Static, typeof(float), "_privateProtectedStaticFloat");

            AddField(Protected, typeof(long), "_protectedLong");
            AddField(Protected | Static, typeof(long), "_protectedStaticLong");

            AddField(ProtectedInternal, typeof(double), "_protectedInternalDouble");
            AddField(ProtectedInternal | Static, typeof(double), "_protectedInternalStaticDouble");

            AddField(Public, typeof(int), "_publicInt");
            AddField(Public | Static, typeof(int), "_publicStaticInt");
        }

        public void InitConstructors()
        {
            AddConstructor(Public, new Type[] { });
        }


        public SampleSchema()
        {
            InitProperties();
            InitInterfaces();
            InitFields();
            InitConstructors();
        }
    }

    [TestClass]
    public class BaseModelFixtureTests : BaseModelFixture<SampleModelForTesting, SampleSchema>
    {
        // Helper constants
        private const MethodAttributes PublicMethod = MethodAttributes.Public;

        protected static object[] GenerateRandomInitModel => new object[]
        {

        };
        
        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            //InitFields();
            //InitProperties();
            //InitInterfaces();
            //InitConstructors();
        }


        
        // Test to Override the Property count to count ALL properties
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
        public override void Property_Count_ShouldMatch(AccessModifiers flags, bool flattenHierarchy = true) => base.Property_Count_ShouldMatch(flags, flattenHierarchy);
        

    }
}
