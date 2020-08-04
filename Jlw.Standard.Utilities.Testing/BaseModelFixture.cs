using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Jlw.Standard.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{
    public class BaseModelFixture<TModel> where TModel : class, new()
    {
        protected static TModel DefaultInstance { get; set; } = new TModel();


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

            Assert.IsTrue(p.CanWrite, $"{sMemberName} is not a writable property.");
            return p;
        }

        public object AssertPropertyGet(string sMemberName, BindingFlags flags)
        {
            AssertPropertyIsReadable(sMemberName);
            var p = GetPropertyInfoByName(sMemberName, flags);
            var m = p.GetMethod;
            object o = null;
            Assert.IsNotNull(m, $"'{sMemberName}' does not have a get accessor.");
            Assert.ThrowsException<AssertSucceededException>(() =>
            {
                object t = Activator.CreateInstance(typeof(TModel));
                o = m.Invoke(t, new object[] { });
                throw new AssertSucceededException($"Successfully retrieved value {o}");
            });

            return o;
        }

        public object AssertPropertySet(string sMemberName, BindingFlags flags)
        {
            AssertPropertyIsWritable(sMemberName);
            object t = Activator.CreateInstance(typeof(TModel));
            var p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsTrue(p.CanWrite, $"{sMemberName} is not a writable property.");
            var m = p.SetMethod;
            object o = null;
            Assert.IsNotNull(m, $"'{sMemberName}' does not have a set accessor.");
            Assert.ThrowsException<AssertSucceededException>(() =>
            {
                m.Invoke(t, new object[] { DataUtility.ParseAs(p.PropertyType, "1234567890.1234567890") });
                throw new AssertSucceededException($"Successfully set value {o}");
            });
            return t;
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
