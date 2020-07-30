﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{

    public class BaseModelFixture<TModel>
    {
        public PropertyInfo GetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(TModel).GetProperty(sMemberName, flags);

        }

        public PropertyInfo AssertGetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            var p = GetPropertyInfoByName(sMemberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'");

            p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a member with the name '{sMemberName}', and with the Binding Flags: {flags}");

            return p;
        }

        public void AssertPropertyIsReadable(string sMemberName)
        {
            var p = GetPropertyInfoByName(sMemberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'");

            Assert.IsTrue(p.CanRead, $"{sMemberName} is not a readable property.");
        }



        public object AssertGetPublicPropertyValueByName(TModel o, string sMemberName)
        {
            // Assert member exists at all?

            // Assert member is public
            var p = GetPropertyInfoByName(sMemberName, BindingFlags.Instance | BindingFlags.Public);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a public member with the name '{sMemberName}'.");

            AssertPropertyIsReadable(sMemberName);

            return p.GetValue(o);
        }

        public void AssertTypeForObjectPublicMemberModel(TModel o, string sMemberName, Type type)
        {
            Assert.IsNotNull(type, "type argument cannot be null");

            Assert.IsInstanceOfType(type, typeof(Type));

            var p = AssertGetPublicPropertyValueByName(o, sMemberName);
            Assert.IsInstanceOfType(p, type);

        }
    }
}
