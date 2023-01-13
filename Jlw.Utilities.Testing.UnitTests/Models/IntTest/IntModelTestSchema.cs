using System;
using System.Collections.Generic;
using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing.UnitTests
{
    /// <summary>
    /// A BaseModelSchema that should pass unit tests.
    /// </summary>
    public class IntModelTestSchema : BaseModelSchema<IntModel>
    {
        public override IEnumerable<InstanceMemberTestData<IntModel>> InstanceMemberTestList
        {
            get
            {
                var n = DataUtility.GenerateRandom<short>();
                var sut = new IntModel(n);

                yield return new InstanceMemberTestData<IntModel>(sut, nameof(sut.PublicGet), (int)n);
                yield return new InstanceMemberTestData<IntModel>(sut, nameof(sut._public), (int)n);
            }
        }

        protected void InitConstructors()
        {
            AddConstructor(Public, null);
            AddConstructor(Public, new Type[] { });
            AddConstructor(Public, new Type[] { typeof(short) });
            AddConstructor(Private | Static, null);
            AddConstructor(Private, new Type[] { typeof(long) });
            AddConstructor(Protected, new Type[] { typeof(int) });
        }

        protected void InitInterfaces()
        {
            AddInterface(typeof(INullModel));
            AddInterface(typeof(IIntModel));
        }

        protected void InitFields()
        {
            AddField(Public, typeof(int), "_public");
            AddField(Public | Static, typeof(int), "_publicStatic");
            AddField(Private, typeof(int), "_private");
            AddField(Private | Static, typeof(int), "_privateStatic");
            AddField(PrivateProtected, typeof(int), "_privateProtected");
            AddField(PrivateProtected | Static, typeof(int), "_privateProtectedStatic");
            AddField(Protected, typeof(int), "_protected");
            AddField(Protected | Static, typeof(int), "_protectedStatic");
            AddField(ProtectedInternal, typeof(int), "_protectedInternal");
            AddField(ProtectedInternal | Static, typeof(int), "_protectedInternalStatic");
            //AddField(Internal, typeof(int), "_internal");
            //AddField(Internal | Static, typeof(int), "_internalStatic");

            // One last field to test if object is of type or assignable 
            AddField(Public, typeof(object), "_publicObject", false);
        }

        protected void InitProperties()
        {
            AddProperty(typeof(int), "PublicGetSet", Public, Public);
            AddProperty(typeof(int), "PublicGet", Public, null);
            AddProperty(typeof(int), "PublicSet", null, Public);

            AddProperty(typeof(int), "PublicStaticGetSet", Public | Static, Public | Static);

            AddProperty(typeof(object), "PublicNoTest", Public, Public, false, false);

        }

        public IntModelTestSchema()
        {
            InitConstructors();
            InitInterfaces();
            InitFields();
            InitProperties();
        }
    }
}