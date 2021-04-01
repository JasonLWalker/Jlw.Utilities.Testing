using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jlw.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel> where TModel : class
    {
        private TModel _defaultInstance;

        protected TModel DefaultInstance
        {
            get
            {
                if (_defaultInstance is null)
                {
                    var ctx = typeof(TModel).GetConstructor(new Type[] { });
                    if (ctx != null)
                        return (TModel) ctx.Invoke(default);
                    else
                        return null;
                }
                return _defaultInstance;
            }
            set => _defaultInstance = value;
        }

        /*
        public static IEnumerable<object> GenerateRandomTestValues<T>(int nCount = 5)
        {
            List<object> a = new List<object>();
            for (int i=0; i < nCount; i++)
            {
                a.Add(DataUtility.GenerateRandom<T>());
            }
            
            return a;
        }


        public static T GenerateRandomTestValue<T>()
        {
            return (T)DataUtility.GenerateRandom<T>();
        }
        */

        public static FieldInfo GetFieldInfoByName<T>(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(T).GetField(sMemberName, flags);
        }

        public static FieldInfo GetFieldInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default) => GetFieldInfoByName<TModel>(sMemberName, flags);

        public static PropertyInfo GetPropertyInfoByName<T>(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(T).GetProperty(sMemberName, flags);
        }

        public static PropertyInfo GetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default) => GetPropertyInfoByName<TModel>(sMemberName, flags);

        public static object GetPropertyValueByName<T>(T o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        {
            var p = GetPropertyInfoByName<T>(sMemberName, flags);
            return p?.GetValue(o); 
        }

        public static object GetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy) => GetPropertyValueByName<TModel>(o, sMemberName, flags);


        public static void SetPropertyValueByName<T>(T o, string sMemberName, object value, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
            var p = GetPropertyInfoByName(sMemberName, flags);
            p.SetValue(o, value);
        }
        

        public static void SetPropertyValueByName(TModel o, string sMemberName, object value, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static) => SetPropertyValueByName<TModel>(o, sMemberName, value, flags);


        #region Assertion Helpers

        public FieldInfo AssertFieldExists(string sMemberName)
        {
            var p = GetFieldInfoByName(sMemberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a field with the name '{sMemberName}'");

            return p;
        }


        public PropertyInfo AssertPropertyExists(string sMemberName)
        {
            var p = GetPropertyInfoByName(sMemberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'");

            return p;
        }

        public FieldInfo AssertGetFieldInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            var p = AssertFieldExists(sMemberName);

            p = GetFieldInfoByName(sMemberName, flags);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a field with the name '{sMemberName}', and with the Binding Flags: {flags}");

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
            AssertAccessScopeForMethodAttributes(m, attrs);
            return p;
        }

        public PropertyInfo AssertPropertyScopeForSetAccessor(string sMemberName, MethodAttributes attrs)
        {
            var p = AssertPropertyIsWritable(sMemberName);
            var m = p.SetMethod;
            AssertAccessScopeForMethodAttributes(m, attrs);
            return p;
        }
        
        public void AssertAccessScopeForMethodAttributes(MethodInfo m, MethodAttributes attrs)
        {
            switch (attrs & MethodAttributes.MemberAccessMask)
            {
                case AccessScope.Public:   // Public
                    Assert.IsTrue(m?.IsPublic ?? false, $"<{DefaultInstance.GetType().Name}.{m.Name}> should be public, but is <{m?.Attributes}>");
                    break;
                case AccessScope.Internal:   // Internal
                    Assert.IsTrue(m?.IsAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{m.Name}> should be internal, but is <{m?.Attributes}>");
                    break;
                case AccessScope.ProtectedInternal:   // Internal
                    Assert.IsTrue(m?.IsFamilyOrAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{m.Name}> should be protected internal, but is <{m?.Attributes}>");
                    break;
                case AccessScope.PrivateProtected:
                    Assert.IsTrue(m?.IsFamilyAndAssembly ?? false, $"<{DefaultInstance.GetType().Name}.{m.Name}> should be private protected, but is <{m?.Attributes}>");
                    break;
                case AccessScope.Protected:   // Protected
                    Assert.IsTrue(m?.IsFamily ?? false, $"<{DefaultInstance.GetType().Name}.{m.Name}> should be protected, but is <{m?.Attributes}>");
                    break;
                case AccessScope.Private:   // Private
                    Assert.IsTrue(m?.IsPrivate ?? false, $"<{DefaultInstance.GetType().Name}.{m.Name}> should be private, but is <{m?.Attributes}>");
                    break;
                default:
                    Assert.Fail($"<{DefaultInstance.GetType().Name}.{m.Name}> has attributes that don't match expected values <{m?.Attributes}>");
                    break;
            }

            if (
                ((m.IsStatic) && ((attrs & AccessScope.Static) != AccessScope.Static)) 
                || 
                (!(m.IsStatic) && ((attrs & AccessScope.Static) == AccessScope.Static)) 
                ||
                (m.IsHideBySig && ((attrs & MethodAttributes.HideBySig) != MethodAttributes.HideBySig)) 
                ||
                (!m.IsHideBySig && ((attrs & MethodAttributes.HideBySig) == MethodAttributes.HideBySig)) 
                ||
                (m.IsSpecialName && ((attrs & MethodAttributes.SpecialName) != MethodAttributes.SpecialName))
                ||
                (!m.IsSpecialName && ((attrs & MethodAttributes.SpecialName) == MethodAttributes.SpecialName))
            ) {
                Assert.Fail($"<{DefaultInstance.GetType().Name}.{m.Name}> has attributes that don't match expected values <{m?.Attributes}>");
            }
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

        #region Helper Methods

        public static AccessModifiers GetPropertyAccess(MethodAttributes getAttr, MethodAttributes setAttr)
        {
            int attr = Math.Max((int)(setAttr & MethodAttributes.MemberAccessMask), (int)(getAttr & MethodAttributes.MemberAccessMask));

            // Add static flag if it is set on either Get or Set.
            attr |= (int)((setAttr | getAttr) & MethodAttributes.Static);
            return (AccessModifiers) attr;
        }

        protected static string GetAccessString(PropertyInfo info)
        {
            var attr = GetPropertyAccess(info?.SetMethod?.Attributes ?? MethodAttributes.PrivateScope, info?.GetMethod?.Attributes ?? MethodAttributes.PrivateScope);
            
            // return parsed string from results
            return GetAccessString((MethodAttributes)attr);
        }

        protected static string GetAccessString(AccessModifiers attr) => GetAccessString((MethodAttributes) attr);

        protected static string GetAccessString(MethodAttributes attr)
        {
            string access = "";

            switch (attr & MethodAttributes.MemberAccessMask)
            {
                case var a when a.HasFlag(MethodAttributes.Public):
                    access += "public";
                    break;
                case var a when a.HasFlag(MethodAttributes.FamORAssem):
                    access += "protected internal";
                    break;
                case var a when a.HasFlag(MethodAttributes.Family):
                    access += "protected";
                    break;
                case var a when a.HasFlag(MethodAttributes.Assembly):
                    access += "internal";
                    break;
                case var a when a.HasFlag(MethodAttributes.FamANDAssem):
                    access += "private protected";
                    break;
                default:
                    access += "private";
                    break;
            }

            if (attr.HasFlag(MethodAttributes.Static))
            {
                access += " static";
            }

            return access;
        }

        #endregion
    }
}