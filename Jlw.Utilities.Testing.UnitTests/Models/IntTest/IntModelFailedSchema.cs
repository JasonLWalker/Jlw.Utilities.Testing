using System;
using System.Data;

namespace Jlw.Utilities.Testing.UnitTests
{

    /// <summary>
    /// A BaseModelSchema that should fail unit tests.
    /// </summary>
    public class IntModelFailedSchema : BaseModelSchema<IntModel>
    {
        protected void InitConstructors()
        {
            AddConstructor(Public, new Type[] { typeof(long), typeof(int) });
            AddConstructor(Public, new Type[] { typeof(int), typeof(long) });
            AddConstructor(Private | Static, new Type[] { typeof(long), typeof(int) });
            AddConstructor(Private | Static, new Type[] { typeof(int), typeof(long) });
        }

        protected void InitInterfaces()
        {
            AddInterface(typeof(IDataRecord));
        }

        protected void InitFields()
        {

        }

        public IntModelFailedSchema()
        {
            InitConstructors();
            InitInterfaces();
            InitFields();
        }
    }
}