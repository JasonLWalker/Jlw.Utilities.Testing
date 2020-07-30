using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{

    public class BaseModelFixture
    {
        public object GetPublicPropertyValueByName(object o, string sMemberName)
        {
            var p = typeof(object).GetProperty(sMemberName);
            Assert.IsNotNull(p, $"Object does not contain a propery with the name '{sMemberName}'");

            p = typeof(object).GetProperty(sMemberName, BindingFlags.Public);
            Assert.IsNotNull(p, $"Object does not contain a Public member with the name '{sMemberName}'");

            Assert.IsTrue(p.CanRead, $"Object does not contain a readable member with the name '{sMemberName}'");
            
            return p.GetValue(o);
        }

        public void AssertTypeForObjectPublicMemberModel(object o, string sMemberName, Type type)
        {
            Assert.IsInstanceOfType(type, typeof(Type), "type argument cannot be null");

            var p = GetPublicPropertyValueByName(o, sMemberName);
            Assert.IsInstanceOfType(p, type);
        }
    }
}
