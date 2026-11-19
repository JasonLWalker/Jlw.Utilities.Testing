using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jlw.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        /// <summary>
        /// Describes an expected field or property of <typeparamref name="TModel"/>: its name, type, access level, and binding flags.
        /// </summary>
        /// <remarks>
        /// Used by the member tests to look up the matching member on the model and to compare its signature.
        /// <see cref="ConstructorSchema"/> derives from this class.
        /// </remarks>
        public class MemberSchema
        {
        /// <summary>
        /// Gets the name of the member.
        /// </summary>
            public virtual string Name { get; protected set; }
        /// <summary>
        /// Gets the type of the member.
        /// </summary>
            public virtual Type Type { get; protected set; }
        /// <summary>
        /// Gets the expected access level and static state of the member.
        /// </summary>
            public virtual AccessModifiers Access { get; protected set; }
        /// <summary>
        /// Gets the binding flags used to look up the member by reflection.
        /// </summary>
            public BindingFlags BindingFlags { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the member signature should be compared against the implemented signatures.
        /// </summary>
            public bool CanTestSignature { get; protected set; }

        /// <summary>
        /// Creates a member schema.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="type">The type of the member.</param>
        /// <param name="access">The expected access level and static state of the member.</param>
        /// <param name="bindingFlags">The binding flags used to look up the member by reflection.</param>
        /// <param name="canTestSignature">Whether the member signature should be compared against the implemented signatures.</param>
            public MemberSchema(string name, Type type = null, AccessModifiers access = AccessModifiers.Public, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy, bool canTestSignature = true)
            {
                Name = name;
                Type = type;
                Access = access;
                BindingFlags = bindingFlags;
                CanTestSignature = canTestSignature;
            }

        /// <summary>
        /// Returns the member signature in the form <c>access TypeName Name</c>, for example <c>public int Count</c>.
        /// </summary>
        /// <returns>The member signature.</returns>
            public override string ToString()
            {
                string type = DataUtility.GetTypeName(Type);
                string access = GetAccessString((MethodAttributes) Access);

                return $"{access} {type} {Name}";
            }
        }
    }
}
