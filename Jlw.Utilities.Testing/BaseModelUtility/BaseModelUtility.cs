using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jlw.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Provides shared reflection helpers and assertions for a model type, used by the test fixtures.
    /// </summary>
    /// <remarks>
    /// Most assertions are written so that a failed check throws an <see cref="AssertFailedException"/> from MSTest,
    /// and a successful check of a value-reading or value-setting operation throws an <see cref="AssertSucceededException"/>.
    /// </remarks>
    /// <typeparam name="TModel">The model type under test.</typeparam>
    public partial class BaseModelUtility<TModel> where TModel : class
    {
        /// <summary>
        /// Backing field for <see cref="DefaultInstance"/>.
        /// </summary>
        private TModel _defaultInstance;

        /// <summary>
        /// Gets or sets an instance of <typeparamref name="TModel"/> used by the assertions.
        /// </summary>
        /// <remarks>
        /// When no instance has been set, the getter creates one with the parameterless constructor.
        /// It returns <c>null</c> if the type has no parameterless constructor.
        /// </remarks>
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

        /// <summary>
        /// Gets the field with the given name from type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to search.</typeparam>
        /// <param name="sMemberName">The name of the field.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        /// <returns>The field information, or <c>null</c> if no matching field exists.</returns>
        public static FieldInfo GetFieldInfoByName<T>(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(T).GetField(sMemberName, flags);
        }

        /// <summary>
        /// Gets the field with the given name from <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="sMemberName">The name of the field.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        /// <returns>The field information, or <c>null</c> if no matching field exists.</returns>
        public static FieldInfo GetFieldInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default) => GetFieldInfoByName<TModel>(sMemberName, flags);

        /// <summary>
        /// Gets the property with the given name from type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to search.</typeparam>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        /// <returns>The property information, or <c>null</c> if no matching property exists.</returns>
        public static PropertyInfo GetPropertyInfoByName<T>(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            return typeof(T).GetProperty(sMemberName, flags);
        }

        /// <summary>
        /// Gets the property with the given name from <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        /// <returns>The property information, or <c>null</c> if no matching property exists.</returns>
        public static PropertyInfo GetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default) => GetPropertyInfoByName<TModel>(sMemberName, flags);

        /// <summary>
        /// Gets the value of the named property on an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type that declares the property.</typeparam>
        /// <param name="o">The instance to read from.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        /// <returns>The property value, or <c>null</c> if the property does not exist.</returns>
        public static object GetPropertyValueByName<T>(T o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        {
            var p = GetPropertyInfoByName<T>(sMemberName, flags);
            return p?.GetValue(o); 
        }

        /// <summary>
        /// Gets the value of the named property on an instance of <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="o">The instance to read from.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        /// <returns>The property value, or <c>null</c> if the property does not exist.</returns>
        public static object GetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy) => GetPropertyValueByName<TModel>(o, sMemberName, flags);


        /// <summary>
        /// Sets the value of the named property on an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        /// The property must exist. If it does not, a <see cref="NullReferenceException"/> is thrown.
        /// </remarks>
        /// <typeparam name="T">The type that declares the property.</typeparam>
        /// <param name="o">The instance to write to.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="value">The value to assign.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        public static void SetPropertyValueByName<T>(T o, string sMemberName, object value, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        {
            var p = GetPropertyInfoByName(sMemberName, flags);
            p.SetValue(o, value);
        }
        

        /// <summary>
        /// Sets the value of the named property on an instance of <typeparamref name="TModel"/>.
        /// </summary>
        /// <remarks>
        /// The property must exist. If it does not, a <see cref="NullReferenceException"/> is thrown.
        /// </remarks>
        /// <param name="o">The instance to write to.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="value">The value to assign.</param>
        /// <param name="flags">The binding flags to use for the search.</param>
        public static void SetPropertyValueByName(TModel o, string sMemberName, object value, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static) => SetPropertyValueByName<TModel>(o, sMemberName, value, flags);


        /// <summary>
        /// Gets the parameterless constructor of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to search.</typeparam>
        /// <returns>The constructor, or <c>null</c> if no parameterless constructor exists.</returns>
        public static ConstructorInfo GetDefaultConstructor<T>()
        {
            return typeof(T).GetConstructor(new Type[] { });
        }

        /// <summary>
        /// Gets the parameterless constructor of <typeparamref name="TModel"/>.
        /// </summary>
        /// <returns>The constructor, or <c>null</c> if no parameterless constructor exists.</returns>
        public static ConstructorInfo GetDefaultConstructor() => GetDefaultConstructor<TModel>();

        /// <summary>
        /// Gets the parameterless constructor of type <typeparamref name="T"/>, and asserts that it exists.
        /// </summary>
        /// <typeparam name="T">The type to search.</typeparam>
        /// <returns>The constructor.</returns>
        public static ConstructorInfo AssertGetDefaultConstructor<T>()
        {
            var ctor = GetDefaultConstructor<T>();
            Assert.IsNotNull(ctor, "Constructor is null. Unable to locate default constructor.");
            return ctor;
        }

        /// <summary>
        /// Gets the parameterless constructor of <typeparamref name="TModel"/>, and asserts that it exists.
        /// </summary>
        /// <returns>The constructor.</returns>
        public static ConstructorInfo AssertGetDefaultConstructor()
        {
            var ctor = GetDefaultConstructor();
            Assert.IsNotNull(ctor, "Constructor is null. Unable to locate default constructor.");
            return ctor;
        }

        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/> using its parameterless constructor, if one exists.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The new instance, or the default value of <typeparamref name="T"/> if no parameterless constructor exists.</returns>
        public static T GetObjectInstance<T>()
        {
            var ctor = GetDefaultConstructor();
            if (ctor != null)
                return (T)ctor.Invoke(null);

            return default(T);
        }

        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/> using its parameterless constructor, and asserts that the constructor exists.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The new instance.</returns>
        public static T AssertGetObjectInstance<T>()
        {
            var ctor = AssertGetDefaultConstructor();
            T obj = (T)ctor.Invoke(null);
            return obj;
        }



        #region Assertion Helpers

        /// <summary>
        /// Asserts that <typeparamref name="TModel"/> declares a field with the given name, of any access level.
        /// </summary>
        /// <param name="sMemberName">The name of the field.</param>
        /// <returns>The field information.</returns>
        public FieldInfo AssertFieldExists(string sMemberName)
        {
            var p = GetFieldInfoByName(sMemberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a field with the name '{sMemberName}'");

            return p;
        }


        /// <summary>
        /// Asserts that <typeparamref name="TModel"/> declares a property with the given name, of any access level.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <returns>The property information.</returns>
        public PropertyInfo AssertPropertyExists(string sMemberName)
        {
            var p = GetPropertyInfoByName(sMemberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'");

            return p;
        }

        /// <summary>
        /// Asserts that a field with the given name exists, then gets it using the given binding flags.
        /// </summary>
        /// <param name="sMemberName">The name of the field.</param>
        /// <param name="flags">The binding flags to use for the lookup.</param>
        /// <returns>The field information.</returns>
        public FieldInfo AssertGetFieldInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            var p = AssertFieldExists(sMemberName);

            p = GetFieldInfoByName(sMemberName, flags);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a field with the name '{sMemberName}', and with the Binding Flags: {flags}");

            return p;
        }


        /// <summary>
        /// Asserts that a property with the given name exists, then gets it using the given binding flags.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the lookup.</param>
        /// <returns>The property information.</returns>
        public PropertyInfo AssertGetPropertyInfoByName(string sMemberName, BindingFlags flags = BindingFlags.Default)
        {
            var p = AssertPropertyExists(sMemberName);

            p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a member with the name '{sMemberName}', and with the Binding Flags: {flags}");

            return p;
        }


        /// <summary>
        /// Asserts that the named property exists and has a getter.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <returns>The property information.</returns>
        public PropertyInfo AssertPropertyIsReadable(string sMemberName)
        {
            var p = AssertPropertyExists(sMemberName);

            Assert.IsTrue(p.CanRead, $"{sMemberName} is not a readable property.");
            return p;
        }

        /// <summary>
        /// Asserts that the named property exists and has a setter.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <returns>The property information.</returns>
        public PropertyInfo AssertPropertyIsWritable(string sMemberName)
        {
            var p = AssertPropertyExists(sMemberName);

            Assert.IsTrue(p.CanWrite, $"{sMemberName} is not a writable property.");
            return p;
        }

        /// <summary>
        /// Asserts that the named property can be read from a new instance of <typeparamref name="TModel"/>,
        /// and returns the value read.
        /// </summary>
        /// <remarks>
        /// The getter is invoked inside an assertion that expects an <see cref="AssertSucceededException"/>,
        /// which signals that the read succeeded.
        /// </remarks>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the lookup.</param>
        /// <returns>The value read from the property.</returns>
        public object AssertPropertyGet(string sMemberName, BindingFlags flags)
        {
            AssertPropertyIsReadable(sMemberName);
            var p = GetPropertyInfoByName(sMemberName, flags);
            var m = p.GetMethod;
            object o = null;
            Assert.IsNotNull(m, $"'{sMemberName}' does not have a get accessor.");
            Assert.Throws<AssertSucceededException>(() =>
            {
                object t = Activator.CreateInstance(typeof(TModel));
                o = m.Invoke(t, new object[] { });
                throw new AssertSucceededException($"Successfully retrieved value {o}");
            });

            return o;
        }

        /// <summary>
        /// Asserts that the named property can be written on a new instance of <typeparamref name="TModel"/>.
        /// </summary>
        /// <remarks>
        /// The setter is invoked with a sample value inside an assertion that expects an <see cref="AssertSucceededException"/>,
        /// which signals that the write succeeded.
        /// </remarks>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the lookup.</param>
        /// <returns>The instance that the property was set on.</returns>
        public object AssertPropertySet(string sMemberName, BindingFlags flags)
        {
            AssertPropertyIsWritable(sMemberName);
            object t = Activator.CreateInstance(typeof(TModel));
            var p = GetPropertyInfoByName(sMemberName, flags);
            Assert.IsTrue(p.CanWrite, $"{sMemberName} is not a writable property.");
            var m = p.SetMethod;
            object o = null;
            Assert.IsNotNull(m, $"'{sMemberName}' does not have a set accessor.");
            Assert.Throws<AssertSucceededException>(() =>
            {
                m.Invoke(t, new object[] { DataUtility.ParseAs(p.PropertyType, "1234567890.1234567890") });
                throw new AssertSucceededException($"Successfully set value {o}");
            });
            return t;
        }

        /// <summary>
        /// Asserts that the named property is readable, and that its getter has the expected access attributes.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="attrs">The expected method attributes, built from <see cref="AccessScope"/> values.</param>
        /// <returns>The property information.</returns>
        public PropertyInfo AssertPropertyScopeForGetAccessor(string sMemberName, MethodAttributes attrs)
        {
            var p = AssertPropertyIsReadable(sMemberName);
            var m = p.GetMethod;
            AssertAccessScopeForMethodAttributes(m, attrs);
            return p;
        }

        /// <summary>
        /// Asserts that the named property is writable, and that its setter has the expected access attributes.
        /// </summary>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="attrs">The expected method attributes, built from <see cref="AccessScope"/> values.</param>
        /// <returns>The property information.</returns>
        public PropertyInfo AssertPropertyScopeForSetAccessor(string sMemberName, MethodAttributes attrs)
        {
            var p = AssertPropertyIsWritable(sMemberName);
            var m = p.SetMethod;
            AssertAccessScopeForMethodAttributes(m, attrs);
            return p;
        }
        
        /// <summary>
        /// Asserts that a method has the expected access level, static state, and other method attributes.
        /// </summary>
        /// <remarks>
        /// Checks the access level, then checks that the static, hide-by-sig, and special-name flags match the expected attributes.
        /// Fails the test if any check does not match.
        /// </remarks>
        /// <param name="m">The method to check.</param>
        /// <param name="attrs">The expected method attributes, built from <see cref="AccessScope"/> values.</param>
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

        /// <summary>
        /// Asserts that the named property is writable, and that setting it on the given instance succeeds.
        /// </summary>
        /// <remarks>
        /// The setter is invoked inside an assertion that expects an <see cref="AssertSucceededException"/>,
        /// which signals that the write succeeded.
        /// </remarks>
        /// <param name="o">The instance to write to.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="value">The value to assign.</param>
        public void AssertSetPropertyValueByName(TModel o, string sMemberName, object value)
        {
            var p = AssertPropertyIsWritable(sMemberName);
            Assert.Throws<AssertSucceededException>(() =>
            {
                p.SetValue(o, value);
                throw new AssertSucceededException("");
            });
        }



        /// <summary>
        /// Asserts that the named property is readable on the given instance, and returns the value read.
        /// </summary>
        /// <remarks>
        /// The getter is invoked inside an assertion that expects an <see cref="AssertSucceededException"/>,
        /// which signals that the read succeeded.
        /// </remarks>
        /// <param name="o">The instance to read from.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="flags">The binding flags to use for the lookup.</param>
        /// <returns>The value read from the property.</returns>
        public object AssertGetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
        {
            // Assert member is public
            var p = GetPropertyInfoByName(sMemberName, flags);
            object val = null;
            Assert.IsNotNull(p, $"{typeof(TModel)} does not contain a property with the name '{sMemberName}'.");
            AssertPropertyIsReadable(sMemberName);
            Assert.Throws<AssertSucceededException>(() =>
            {
                val = p.GetValue(o);
                throw new AssertSucceededException($"Successfully retrieved value {val}");
            });
            return val;
        }


        /// <summary>
        /// Asserts that a property's type is compatible with the expected type.
        /// </summary>
        /// <remarks>
        /// A sample value of the property type is parsed. If it is not <c>null</c>, it must be an instance of <paramref name="type"/>.
        /// Otherwise, <paramref name="type"/> must be assignable from the property type.
        /// </remarks>
        /// <param name="o">The instance that declares the property. Not used directly by the check.</param>
        /// <param name="sMemberName">The name of the property.</param>
        /// <param name="type">The expected type.</param>
        /// <param name="flags">The binding flags to use for the lookup.</param>
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

        /// <summary>
        /// Asserts that an instance is an instance of <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="instance">The instance to check. If <c>null</c>, <see cref="DefaultInstance"/> is checked.</param>
        public void AssertInstanceImplementsType(object instance = null)
        {
            if (instance == null)
                instance = DefaultInstance;

            Assert.IsInstanceOfType(instance, typeof(TModel), $"<{instance.GetType().Name}> is not an instance of <{typeof(TModel).Name}>" );
        }

        /// <summary>
        /// Asserts that an object is not <c>null</c> and is an instance of the given type.
        /// </summary>
        /// <param name="o">The object to check.</param>
        /// <param name="t">The expected type.</param>
        protected void AssertTypeMatches(object o, Type t)
        {
            Assert.IsNotNull(t);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, t);
        }

        
        #endregion

        #region Base Model Tests


        /// <summary>
        /// Asserts that the default instance is an instance of the given type.
        /// </summary>
        /// <param name="t">The expected type.</param>
        public virtual void Should_BeInstanceOf(Type t)
        {
            Assert.IsNotNull(DefaultInstance);
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(DefaultInstance, t);
        }


        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the combined access level and static state of a property, from its getter and setter attributes.
        /// </summary>
        /// <remarks>
        /// The more accessible of the getter and setter access levels is used. The static flag is set if either accessor is static.
        /// </remarks>
        /// <param name="getAttr">The getter method attributes. Use <c>default</c> if there is no getter.</param>
        /// <param name="setAttr">The setter method attributes. Use <c>default</c> if there is no setter.</param>
        /// <returns>The combined access modifiers.</returns>
        public static AccessModifiers GetPropertyAccess(MethodAttributes getAttr, MethodAttributes setAttr)
        {
            int attr = Math.Max((int)(setAttr & MethodAttributes.MemberAccessMask), (int)(getAttr & MethodAttributes.MemberAccessMask));

            // Add static flag if it is set on either Get or Set.
            attr |= (int)((setAttr | getAttr) & MethodAttributes.Static);
            return (AccessModifiers) attr;
        }

        /// <summary>
        /// Gets a display string for the access level and static state of a property.
        /// </summary>
        /// <param name="info">The property to describe.</param>
        /// <returns>A string such as <c>public static</c> or <c>private</c>.</returns>
        protected static string GetAccessString(PropertyInfo info)
        {
            var attr = GetPropertyAccess(info?.SetMethod?.Attributes ?? MethodAttributes.PrivateScope, info?.GetMethod?.Attributes ?? MethodAttributes.PrivateScope);
            
            // return parsed string from results
            return GetAccessString((MethodAttributes)attr);
        }

        /// <summary>
        /// Gets a display string for the access level and static state described by the given access modifiers.
        /// </summary>
        /// <param name="attr">The access modifiers to describe.</param>
        /// <returns>A string such as <c>protected internal</c>, optionally followed by <c>static</c>.</returns>
        protected static string GetAccessString(AccessModifiers attr) => GetAccessString((MethodAttributes) attr);

        /// <summary>
        /// Gets a display string for the access level and static state described by the given method attributes.
        /// </summary>
        /// <param name="attr">The method attributes to describe.</param>
        /// <returns>A string such as <c>protected internal</c>, optionally followed by <c>static</c>.</returns>
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
