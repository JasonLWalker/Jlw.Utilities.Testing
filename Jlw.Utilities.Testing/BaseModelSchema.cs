using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Jlw.Utilities.Testing
{
    public abstract class BaseModelSchema<TModel> where TModel : class
    {
        public const AccessModifiers Private = AccessModifiers.Private;
        public const AccessModifiers PrivateProtected = AccessModifiers.PrivateProtected;
        public const AccessModifiers Internal = AccessModifiers.Internal;
        public const AccessModifiers Protected = AccessModifiers.Protected;
        public const AccessModifiers ProtectedInternal = AccessModifiers.ProtectedInternal;
        public const AccessModifiers Public = AccessModifiers.Public;
        public const AccessModifiers Static = AccessModifiers.Static;

        #region Properies
        protected List<BaseModelUtility<TModel>.PropertySchema> _propertySchema = new List<BaseModelUtility<TModel>.PropertySchema>() { null };
        public IEnumerable<BaseModelUtility<TModel>.PropertySchema> PropertySchemaList => _propertySchema;//.Select(o => new object[] { o });

        public void AddProperty(Type type, string name, AccessModifiers? getAccess, AccessModifiers? setAccess, bool canTestValue = true)
        {
            AccessModifiers accessModifiers = BaseModelUtility<TModel>.GetPropertyAccess((MethodAttributes)(getAccess ?? default), (MethodAttributes)(setAccess ?? default));
            BindingFlags flags = BindingFlags.FlattenHierarchy;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;

            // Clear out null placeholder
            if (_propertySchema.Count == 1 && _propertySchema[0] == null)
                _propertySchema.Clear();

            _propertySchema.Add(new BaseModelUtility<TModel>.PropertySchema(name, type, flags, getAccess, setAccess, canTestValue));
        }
        #endregion

        #region Interfaces
        // ReSharper disable once StaticMemberInGenericType
        protected List<Type> _implementedInterfaceTypes = new List<Type> { null };

        public IEnumerable<Type> ImplementedInterfaceList => _implementedInterfaceTypes;

        public void AddInterface(Type type)
        {
            // Clear out null placeholder
            if (_implementedInterfaceTypes.Count == 1 && _implementedInterfaceTypes[0] == null)
                _implementedInterfaceTypes.Clear();

            _implementedInterfaceTypes.Add(type);
        }
        #endregion

        #region Fields
        // ReSharper disable once StaticMemberInGenericType
        protected List<BaseModelUtility<TModel>.MemberSchema> _fieldSchema = new List<BaseModelUtility<TModel>.MemberSchema> { null };

        public IEnumerable<BaseModelUtility<TModel>.MemberSchema> FieldList => _fieldSchema;

        public void AddField(AccessModifiers access, Type type, string name)
        {
            // Clear out null placeholder
            if (_fieldSchema.Count() == 1 && _fieldSchema[0] == null)
                _fieldSchema.Clear();

            BindingFlags flags = BindingFlags.FlattenHierarchy;
            flags |= access.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= access.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;

            _fieldSchema.Add(new BaseModelUtility<TModel>.MemberSchema(name, type, access, flags));
        }
        #endregion

        #region Constructors
        protected List<BaseModelUtility<TModel>.ConstructorSchema> _constructorSchema = new List<BaseModelUtility<TModel>.ConstructorSchema> { null };

        public IEnumerable<BaseModelUtility<TModel>.ConstructorSchema> ConstructorList => _constructorSchema;

        public void AddConstructor(AccessModifiers access, IEnumerable<Type> initArgs, IEnumerable<IEnumerable<object>> testArgs, Func<TModel, IEnumerable<object>, bool> fnCallback)
        {
            // Clear out null placeholder
            if (_constructorSchema.Count == 1 && _constructorSchema[0] == null)
                _constructorSchema.Clear();

            _constructorSchema.Add(new BaseModelUtility<TModel>.ConstructorSchema(access, initArgs, testArgs, fnCallback));
        }
        #endregion
    }
}