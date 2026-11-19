using System.Reflection;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Provides <see cref="MethodAttributes"/> constants that represent access levels, static state,
    /// and other method modifiers, for comparing against a reflected member's <c>Attributes</c> value.
    /// </summary>
    /// <remarks>
    /// Each access level constant is cast from the matching <see cref="AccessModifiers"/> value,
    /// which is itself mapped from <see cref="MethodAttributes"/>. Use <see cref="AccessMask"/>
    /// to isolate the access level and static bits of a member's attributes before comparing them.
    /// </remarks>
    public static class AccessScope
    {
        /// <summary>
        /// Represents a <c>public</c> member.
        /// </summary>
        public const MethodAttributes Public = (MethodAttributes)AccessModifiers.Public;

        /// <summary>
        /// Represents a <c>private</c> member, accessible only within its declaring type.
        /// </summary>
        public const MethodAttributes Private = (MethodAttributes)AccessModifiers.Private;

        /// <summary>
        /// Represents a <c>protected</c> member, accessible within its declaring type and derived types.
        /// </summary>
        public const MethodAttributes Protected = (MethodAttributes)AccessModifiers.Protected;

        /// <summary>
        /// Represents a <c>private protected</c> member, accessible within its declaring type and
        /// derived types in the same assembly.
        /// </summary>
        public const MethodAttributes PrivateProtected = (MethodAttributes)AccessModifiers.PrivateProtected;

        /// <summary>
        /// Represents an <c>internal</c> member, accessible within the same assembly.
        /// </summary>
        public const MethodAttributes Internal = (MethodAttributes)AccessModifiers.Internal;

        /// <summary>
        /// Represents a <c>protected internal</c> member, accessible within the same assembly or
        /// within derived types.
        /// </summary>
        public const MethodAttributes ProtectedInternal = (MethodAttributes)AccessModifiers.ProtectedInternal;

        /// <summary>
        /// Represents a <c>static</c> member that belongs to the type rather than an instance.
        /// </summary>
        public const MethodAttributes Static = (MethodAttributes)AccessModifiers.Static;

        /// <summary>
        /// Represents a <c>final</c> method that cannot be overridden (sealed override).
        /// </summary>
        public const MethodAttributes Final = MethodAttributes.Final;

        /// <summary>
        /// Represents an <c>abstract</c> method that must be implemented by a derived type.
        /// </summary>
        public const MethodAttributes Abstract = MethodAttributes.Abstract;

        /// <summary>
        /// A combined mask of every access level and <see cref="Static"/>.
        /// Apply this with a bitwise AND to a member's <c>Attributes</c> value to extract its
        /// access level and static state, ignoring other modifiers such as <see cref="Final"/> or <see cref="Abstract"/>.
        /// </summary>
        public const MethodAttributes AccessMask = (MethodAttributes)AccessModifiers.AccessMask;
    }
}
