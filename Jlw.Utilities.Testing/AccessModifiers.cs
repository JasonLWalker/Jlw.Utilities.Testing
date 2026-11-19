using System;
using System.Reflection;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Specifies the access modifiers and scope of a member (method, property, field, or constructor)
    /// that is used when reflecting over a type under test.
    /// </summary>
    /// <remarks>
    /// The values for the access levels are mapped directly from <see cref="MethodAttributes"/>
    /// so they can be compared against a member's <c>Attributes</c> value.
    /// Because this enum is marked with <see cref="FlagsAttribute"/>, values can be combined
    /// (for example, <c>AccessModifiers.Public | AccessModifiers.Static</c>).
    /// </remarks>
    [Flags]
    public enum AccessModifiers
    {
        /// <summary>
        /// The member is accessible only within its declaring type (<c>private</c>).
        /// </summary>
        Private = (int)MethodAttributes.Private,

        /// <summary>
        /// The member is accessible within its declaring type and derived types in the same assembly
        /// (<c>private protected</c>).
        /// </summary>
        PrivateProtected = (int)MethodAttributes.FamANDAssem,

        /// <summary>
        /// The member is accessible within the same assembly (<c>internal</c>).
        /// </summary>
        Internal = (int)MethodAttributes.Assembly,

        /// <summary>
        /// The member is accessible within its declaring type and derived types (<c>protected</c>).
        /// </summary>
        Protected = (int)MethodAttributes.Family,

        /// <summary>
        /// The member is accessible within the same assembly or within derived types
        /// (<c>protected internal</c>).
        /// </summary>
        ProtectedInternal = (int)MethodAttributes.FamORAssem,

        /// <summary>
        /// The member is accessible from any code (<c>public</c>).
        /// </summary>
        Public = (int)MethodAttributes.Public,

        /// <summary>
        /// The member is a static member that belongs to the type rather than an instance (<c>static</c>).
        /// </summary>
        Static = (int)MethodAttributes.Static,

        /// <summary>
        /// A combined value of every access level and <see cref="Static"/>.
        /// Use this to compare a member's access level and static state against this enum in one operation.
        /// </summary>
        AccessMask = Public | Private | Protected | PrivateProtected | Internal | ProtectedInternal | Static
    }
}
