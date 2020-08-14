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
        protected TModel DefaultInstance { get; set; } = new TModel();


        public PropertyInfo GetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(TModel).GetProperty(sMemberName, flags);

        }

        #region Assertion Helpers

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

        public PropertyInfo AssertPropertyScopeForGetAccessor(string sMemberName, MethodAttributes attrs)
        {
            var p = AssertPropertyIsReadable(sMemberName);
            var m = p.GetMethod;
            switch (attrs & MethodAttributes.MemberAccessMask)
            {
                case MethodAttributes.Public:   // Public
                    Assert.IsTrue(m?.IsPublic ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor should be public, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.FamORAssem:   // Public
                    Assert.IsTrue(m?.IsFamilyOrAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor should be internal, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.FamANDAssem:
                    Assert.IsTrue(m?.IsFamilyAndAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor should be private protected, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.Family:   // Protected
                    Assert.IsTrue(m?.IsFamily ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor should be protected, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.Private:   // Protected
                    Assert.IsTrue(m?.IsFamily ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor should be private, but is <{m?.Attributes}>");
                    break;
                default:
                    Assert.Fail($"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor has attributes that don't match expected values <{m?.Attributes}>");
                    break;
            }

            if (
                m.IsStatic && ((attrs & MethodAttributes.Static) != MethodAttributes.Static)
            ) {
                Assert.Fail($"<{DefaultInstance.GetType().Name}.{sMemberName}.Get> accessor has attributes that don't match expected values <{m?.Attributes}>");
            }

            return p;
        }

        public PropertyInfo AssertPropertyScopeForSetAccessor(string sMemberName, MethodAttributes attrs)
        {
            var p = AssertPropertyIsWritable(sMemberName);
            var m = p.SetMethod;
            switch (attrs & MethodAttributes.MemberAccessMask)
            {
                case MethodAttributes.Public:   // Public
                    Assert.IsTrue(m?.IsPublic ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor should be public, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.FamORAssem:   // Public
                    Assert.IsTrue(m?.IsFamilyOrAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor should be internal, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.FamANDAssem:
                    Assert.IsTrue(m?.IsFamilyAndAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor should be private protected, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.Family:   // Protected
                    Assert.IsTrue(m?.IsFamily ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor should be protected, but is <{m?.Attributes}>");
                    break;
                case MethodAttributes.Private:   // Protected
                    Assert.IsTrue(m?.IsFamily ?? false, $"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor should be private, but is <{m?.Attributes}>");
                    break;
                default:
                    Assert.Fail($"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor has attributes that don't match expected values <{m?.Attributes}>");
                    break;
            }

            if (
                m.IsStatic && ((attrs & MethodAttributes.Static) != MethodAttributes.Static)
            ) {
                Assert.Fail($"<{DefaultInstance.GetType().Name}.{sMemberName}.Set> accessor has attributes that don't match expected values <{m?.Attributes}>");
            }

            return p;
        }


        public void SetPropertyValueByName(TModel o, string sMemberName, object value, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
            var p = GetPropertyInfoByName(sMemberName, flags);
            p.SetValue(o, value);
        }

        public object GetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
            var p = GetPropertyInfoByName(sMemberName, flags);
            return p.GetValue(o);

        }


        public void AssertSetPropertyValueByName(TModel o, string sMemberName, object value)
        {
            var p = AssertPropertyIsWritable(sMemberName);
            Assert.ThrowsException<AssertSucceededException>(() =>
            {
                p.SetValue(o, value);
                throw new AssertSucceededException("");
            });
        }



        public object AssertGetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
        {
            // Assert member is public
            var p = GetPropertyInfoByName(sMemberName, flags);
            object val = null;
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'.");
            AssertPropertyIsReadable(sMemberName);
            Assert.ThrowsException<AssertSucceededException>(() =>
            {
                val = p.GetValue(o);
                throw new AssertSucceededException($"Successfully retrieved value {val}");
            });
            return val;
        }


        public void AssertTypeAssignmentForObjectProperty(TModel o, string sMemberName, Type type, BindingFlags flags)
        {
            //var v = AssertGetPropertyValueByName(o, sMemberName, flags);
            var p = AssertGetPropertyInfoByName(sMemberName, flags);
            var v = DataUtility.ParseAs(p.PropertyType, "1234567890.1234567890");
            if (v != null)
            {
                Assert.IsInstanceOfType(v, type, $"'{sMemberName}' <{v.GetType()}> is not an instance of <{type}>");
            }
            else
            {
                Assert.IsTrue(type.IsAssignableFrom(p.PropertyType), $"'{sMemberName}' <{type}> is not assignable from <{p.PropertyType}>");
            }
        }

        public void AssertInstanceImplementsType(object instance = null)
        {
            if (instance == null)
                instance = DefaultInstance;

            Assert.IsInstanceOfType(instance, typeof(TModel), $"<{instance.GetType().Name}> is not an instance of <{typeof(TModel).Name}>" );
        }

        protected void AssertTypeMatches(object o, Type t)
        {
            Assert.IsNotNull(t);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, t);
        }

        
        #endregion

        #region Base Model Tests


        public virtual void Should_BeInstanceOf(Type t)
        {
            Assert.IsNotNull(DefaultInstance);
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(DefaultInstance, t);
        }


        #endregion

    }

}
