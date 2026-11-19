using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Base class for declaring the expected members of a model type, used by <see cref="BaseModelFixture{TModel, TSchema}"/>.
    /// </summary>
    /// <remarks>
    /// Derived schemas call the Add methods in their constructor to describe the properties, fields, interfaces,
    /// and constructors that the model should implement. Each list starts with a <c>null</c> placeholder that is
    /// removed when the first real entry is added.
    /// </remarks>
    /// <typeparam name="TModel">The model type described by the schema.</typeparam>
    public abstract class BaseModelSchema<TModel> where TModel : class
    {
        /// <summary>
        /// Alias for <see cref="AccessModifiers.Private"/>, for use when declaring expected access levels in a schema.
        /// </summary>
        public const AccessModifiers Private = AccessModifiers.Private;
        /// <summary>
        /// Alias for <see cref="AccessModifiers.PrivateProtected"/>, for use when declaring expected access levels in a schema.
        /// </summary>
        public const AccessModifiers PrivateProtected = AccessModifiers.PrivateProtected;
        /// <summary>
        /// Alias for <see cref="AccessModifiers.Internal"/>, for use when declaring expected access levels in a schema.
        /// </summary>
        public const AccessModifiers Internal = AccessModifiers.Internal;
        /// <summary>
        /// Alias for <see cref="AccessModifiers.Protected"/>, for use when declaring expected access levels in a schema.
        /// </summary>
        public const AccessModifiers Protected = AccessModifiers.Protected;
        /// <summary>
        /// Alias for <see cref="AccessModifiers.ProtectedInternal"/>, for use when declaring expected access levels in a schema.
        /// </summary>
        public const AccessModifiers ProtectedInternal = AccessModifiers.ProtectedInternal;
        /// <summary>
        /// Alias for <see cref="AccessModifiers.Public"/>, for use when declaring expected access levels in a schema.
        /// </summary>
        public const AccessModifiers Public = AccessModifiers.Public;
        /// <summary>
        /// Alias for <see cref="AccessModifiers.Static"/>, for use when declaring expected member modifiers in a schema.
        /// </summary>
        public const AccessModifiers Static = AccessModifiers.Static;

        #region Properties
        /// <summary>
        /// The property schemas declared for the model. Holds a single <c>null</c> placeholder until a property is added.
        /// </summary>
        protected List<BaseModelUtility<TModel>.PropertySchema> _propertySchema = new List<BaseModelUtility<TModel>.PropertySchema>() { null };
        /// <summary>
        /// Gets the declared property schemas, including the <c>null</c> placeholder when no properties have been added.
        /// </summary>
        public IEnumerable<BaseModelUtility<TModel>.PropertySchema> PropertySchemaList => _propertySchema;//.Select(o => new object[] { o });

        /// <summary>
        /// Adds a property to the schema.
        /// </summary>
        /// <remarks>
        /// The access level and static state are derived from the getter and setter access levels.
        /// The first call replaces the <c>null</c> placeholder.
        /// </remarks>
        /// <param name="type">The property type.</param>
        /// <param name="name">The property name.</param>
        /// <param name="getAccess">The expected access level of the getter, or <c>null</c> if the property has no getter.</param>
        /// <param name="setAccess">The expected access level of the setter, or <c>null</c> if the property has no setter.</param>
        /// <param name="canTestValue">Whether the property value should be tested by setting and reading it.</param>
        /// <param name="canTestSignature">Whether the property signature should be compared against the implemented signatures.</param>
        public void AddProperty(Type type, string name, AccessModifiers? getAccess, AccessModifiers? setAccess, bool canTestValue = true, bool canTestSignature = true)
        {
            AccessModifiers accessModifiers = BaseModelUtility<TModel>.GetPropertyAccess((MethodAttributes)(getAccess ?? default), (MethodAttributes)(setAccess ?? default));
            BindingFlags flags = BindingFlags.FlattenHierarchy;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;

            // Clear out null placeholder
            if (_propertySchema.Count == 1 && _propertySchema[0] == null)
                _propertySchema.Clear();

            _propertySchema.Add(new BaseModelUtility<TModel>.PropertySchema(name, type, flags, getAccess, setAccess, canTestValue, canTestSignature));
        }
        #endregion

        #region Interfaces
        /// <summary>
        /// The interface types the model is expected to implement. Holds a single <c>null</c> placeholder until an interface is added.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        protected List<Type> _implementedInterfaceTypes = new List<Type> { null };

        /// <summary>
        /// Gets the declared interface types, including the <c>null</c> placeholder when no interfaces have been added.
        /// </summary>
        public IEnumerable<Type> ImplementedInterfaceList => _implementedInterfaceTypes;

        /// <summary>
        /// Adds an interface type that the model is expected to implement.
        /// </summary>
        /// <remarks>
        /// The first call replaces the <c>null</c> placeholder.
        /// </remarks>
        /// <param name="type">The expected interface type.</param>
        public void AddInterface(Type type)
        {
            // Clear out null placeholder
            if (_implementedInterfaceTypes.Count == 1 && _implementedInterfaceTypes[0] == null)
                _implementedInterfaceTypes.Clear();

            _implementedInterfaceTypes.Add(type);
        }
        #endregion

        #region Fields
        /// <summary>
        /// The field schemas declared for the model. Holds a single <c>null</c> placeholder until a field is added.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        protected List<BaseModelUtility<TModel>.MemberSchema> _fieldSchema = new List<BaseModelUtility<TModel>.MemberSchema> { null };

        /// <summary>
        /// Gets the declared field schemas, including the <c>null</c> placeholder when no fields have been added.
        /// </summary>
        public IEnumerable<BaseModelUtility<TModel>.MemberSchema> FieldList => _fieldSchema;

        /// <summary>
        /// Adds a field to the schema.
        /// </summary>
        /// <remarks>
        /// The first call replaces the <c>null</c> placeholder.
        /// </remarks>
        /// <param name="access">The expected access level and static state of the field.</param>
        /// <param name="type">The field type.</param>
        /// <param name="name">The field name.</param>
        /// <param name="canTestSignature">Whether the field signature should be compared against the implemented signatures.</param>
        public void AddField(AccessModifiers access, Type type, string name, bool canTestSignature = true)
        {
            // Clear out null placeholder
            if (_fieldSchema.Count() == 1 && _fieldSchema[0] == null)
                _fieldSchema.Clear();

            BindingFlags flags = BindingFlags.FlattenHierarchy;
            flags |= access.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= access.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;

            _fieldSchema.Add(new BaseModelUtility<TModel>.MemberSchema(name, type, access, flags, canTestSignature));
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The constructor schemas declared for the model. Holds a single <c>null</c> placeholder until a constructor is added.
        /// </summary>
        protected List<BaseModelUtility<TModel>.ConstructorSchema> _constructorSchema = new List<BaseModelUtility<TModel>.ConstructorSchema> { null };

        /// <summary>
        /// Gets the instance member test data for the model.
        /// </summary>
        /// <remarks>
        /// The base implementation yields one entry with no system under test, which the instance member test
        /// uses to report public members that the schema does not cover. Derived schemas can override this to
        /// supply test data for specific members.
        /// </remarks>
        public virtual IEnumerable<InstanceMemberTestData<TModel>> InstanceMemberTestList
        {
            get { yield return new InstanceMemberTestData<TModel>(null, null, null, "[Other Instance Members]");  }
        }

        /// <summary>
        /// Gets the declared constructor schemas, including the <c>null</c> placeholder when no constructors have been added.
        /// </summary>
        public IEnumerable<BaseModelUtility<TModel>.ConstructorSchema> ConstructorList => _constructorSchema;

        /// <summary>
        /// Adds a constructor to the schema.
        /// </summary>
        /// <remarks>
        /// The first call replaces the <c>null</c> placeholder.
        /// </remarks>
        /// <param name="access">The expected access level of the constructor.</param>
        /// <param name="initArgs">The argument types of the constructor, in order.</param>
        public void AddConstructor(AccessModifiers access, IEnumerable<Type> initArgs)
        {
            // Clear out null placeholder
            if (_constructorSchema.Count == 1 && _constructorSchema[0] == null)
                _constructorSchema.Clear();

            _constructorSchema.Add(new BaseModelUtility<TModel>.ConstructorSchema(access, initArgs));
        }
        #endregion
    }
}
