using System;

namespace Jlw.Utilities.Testing.UnitTests
{
    /// <summary>
    /// A BaseModelSchema that should pass unit tests.
    /// </summary>
    public class DateTimeModelTestSchema : BaseModelSchema<DateTimeModel>
    {
        protected void InitConstructors()
        {
            AddConstructor(Public, null);
            AddConstructor(Private | Static, null);
        }

        protected void InitInterfaces()
        {
            AddInterface(typeof(INullModel));
        }

        protected void InitFields()
        {
            AddField(Public, typeof(DateTime), "_public");
            AddField(Public | Static, typeof(DateTime), "_publicStatic");
            AddField(Private, typeof(DateTime), "_private");
            AddField(Private | Static, typeof(DateTime), "_privateStatic");
            AddField(PrivateProtected, typeof(DateTime), "_privateProtected");
            AddField(PrivateProtected | Static, typeof(DateTime), "_privateProtectedStatic");
            AddField(Protected, typeof(DateTime), "_protected");
            AddField(Protected | Static, typeof(DateTime), "_protectedStatic");
            AddField(ProtectedInternal, typeof(DateTime), "_protectedInternal");
            AddField(ProtectedInternal | Static, typeof(DateTime), "_protectedInternalStatic");
            //AddField(Internal, typeof(DateTime), "_internal");
            //AddField(Internal | Static, typeof(DateTime), "_internalStatic");

            // One last field to test if object is of type or assignable 
            AddField(Public, typeof(object), "_publicObject", false);
        }

        protected void InitProperties()
        {
            AddProperty(typeof(DateTime), "PublicGetSet", Public, Public);
            AddProperty(typeof(DateTime), "PublicGet", Public, null);
            AddProperty(typeof(DateTime), "PublicSet", null, Public);

            AddProperty(typeof(DateTime), "PublicStaticGetSet", Public | Static, Public | Static);

            AddProperty(typeof(object), "PublicNoTest", Public, Public, false, false);

        }

        public DateTimeModelTestSchema()
        {
            InitConstructors();
            InitInterfaces();
            InitFields();
            InitProperties();
        }
    }
}