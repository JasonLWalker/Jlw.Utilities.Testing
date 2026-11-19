using System;
using System.Reflection;
using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        /// <summary>
        /// Describes an expected property of <typeparamref name="TModel"/>: its name, type, and the expected access levels of its getter and setter.
        /// </summary>
        /// <remarks>
        /// Used by the property tests to look up the matching property on the model and to compare its accessors.
        /// A <c>null</c> getter or setter access level means the property is expected to have no accessor of that kind.
        /// </remarks>
        public class PropertySchema : MemberSchema
        {
        /// <summary>
        /// Gets the expected access level of the getter, or <c>null</c> if the property has no getter.
        /// </summary>
            public AccessModifiers? GetAttributes { get; protected set; }
        /// <summary>
        /// Gets the expected access level of the setter, or <c>null</c> if the property has no setter.
        /// </summary>
            public AccessModifiers? SetAttributes { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the property value should be tested by setting and reading it.
        /// </summary>
            public bool CanTestValue { get; protected set; }

        /// <summary>
        /// Gets the combined access level and static state of the property, derived from <see cref="GetAttributes"/> and <see cref="SetAttributes"/>.
        /// </summary>
            public override AccessModifiers Access => GetPropertyAccess((MethodAttributes)(GetAttributes ?? default), (MethodAttributes)(SetAttributes ?? default));

        /// <summary>
        /// Creates a property schema.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="type">The type of the property.</param>
        /// <param name="flags">The binding flags used to look up the property by reflection.</param>
        /// <param name="getAttr">The expected access level of the getter, or <c>null</c> if the property has no getter.</param>
        /// <param name="setAttr">The expected access level of the setter, or <c>null</c> if the property has no setter.</param>
        /// <param name="canTestValue">Whether the property value should be tested by setting and reading it.</param>
        /// <param name="canTestSignature">Whether the property signature should be compared against the implemented signatures.</param>
            public PropertySchema(string name, Type type, BindingFlags flags = BindingFlags.Public, AccessModifiers? getAttr = AccessModifiers.Public, AccessModifiers? setAttr = AccessModifiers.Protected, bool canTestValue = true, bool canTestSignature = true) : base(name, type, default, flags, canTestSignature)
            {
                GetAttributes = getAttr;
                SetAttributes = setAttr;
                CanTestValue = canTestValue;
            }

        /// <summary>
        /// Returns the property signature in the form <c>access TypeName Name { get; set; }</c>,
        /// where the get and set parts are shown only when the accessor exists, and are prefixed with their access level if it differs from the property access.
        /// </summary>
        /// <returns>The property signature.</returns>
            public override string ToString()
            {
                string type = DataUtility.GetTypeName(Type);
                string access = GetAccessString((MethodAttributes)Access);
                string get = GetAccessString((MethodAttributes)(GetAttributes ?? default));
                string set = GetAccessString((MethodAttributes)(SetAttributes ?? default));
                
                get = GetAttributes == null ? "" : (get == access ? "get; " : get + " get; ");
                set = SetAttributes == null ? "" : (set == access ? "set; " : set + " set; ");


                return $"{access} {type} {Name} {{ {get}{set}}}";
            }
        }
    }
}
