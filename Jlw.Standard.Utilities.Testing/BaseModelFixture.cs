﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{
    public class BaseModelFixture<TModel>
    {
        protected static TModel DefaultInstance { get; set; }
        protected Type ModelType => typeof(TModel);


        public PropertyInfo GetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(TModel).GetProperty(sMemberName, flags);

        }

        public PropertyInfo AssertPropertyExists(string sMemberName)
        {
            var p = GetPropertyInfoByName(sMemberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'");

            return p;
        }

        public PropertyInfo AssertGetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            var p = AssertPropertyExists(sMemberName);

            p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a member with the name '{sMemberName}', and with the Binding Flags: {flags}");

            return p;
        }


        public PropertyInfo AssertPropertyIsReadable(string sMemberName)
        {
            var p = AssertPropertyExists(sMemberName);

            Assert.IsTrue(p.CanRead, $"{sMemberName} is not a readable property.");
            return p;
        }

        public PropertyInfo AssertPropertyIsWritable(string sMemberName)
        {
            var p = AssertPropertyExists(sMemberName);

            Assert.IsTrue(p.CanWrite, $"{sMemberName} is not a readable property.");
            return p;
        }

        public PropertyInfo AssertPropertyGet(string sMemberName, BindingFlags flags)
        {
            AssertPropertyIsReadable(sMemberName);
            var p = GetPropertyInfoByName(sMemberName, flags);
            var m = p.GetGetMethod();
            Assert.IsNotNull(m, $"'{sMemberName}' does not have a get accessor.");
            
            return p;
        }

        public PropertyInfo AssertPropertySet(string sMemberName, BindingFlags flags)
        {
            AssertPropertyIsWritable(sMemberName);
            var p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsTrue(p.CanWrite, $"{sMemberName} is not a writable property.");
            return p;
        }





        public object AssertGetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags)
        {
            // Assert member is public
            var p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a member with the name '{sMemberName}'.");

            AssertPropertyIsReadable(sMemberName);

            return p.GetValue(o);
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

        public void AssertTypeAssignmentForObjectProperty(TModel o, string sMemberName, Type type, BindingFlags flags)
        {
            var v = AssertGetPropertyValueByName(o, sMemberName, flags);
            if (v != null)
            {
                Assert.IsInstanceOfType(v, type, $"'{sMemberName}' <{v.GetType()}> is not an instance of <{type}>");
            }
            else
            {
                var p = GetPropertyInfoByName(sMemberName, flags);
                Assert.IsTrue(type.IsAssignableFrom(p.PropertyType), $"'{sMemberName}' <{type}> is not assignable from <{p.PropertyType}>");
            }
        }

        public void AssertInstanceImplementsType(object instance = null)
        {
            if (instance == null)
                instance = DefaultInstance;

            Assert.IsInstanceOfType(instance, typeof(TModel), $"<{instance.GetType().Name}> is not an instance of <{typeof(TModel).Name}>" );
        }
    }
}
