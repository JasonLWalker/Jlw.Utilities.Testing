using System.Reflection;

namespace Jlw.Utilities.Testing
{
    public static class AccessScope
    {
        public const MethodAttributes Public = (MethodAttributes)AccessModifiers.Public;
        public const MethodAttributes Private = (MethodAttributes)AccessModifiers.Private;
        public const MethodAttributes Protected = (MethodAttributes)AccessModifiers.Protected;
        public const MethodAttributes PrivateProtected = (MethodAttributes)AccessModifiers.PrivateProtected;
        public const MethodAttributes Internal = (MethodAttributes)AccessModifiers.Internal;
        public const MethodAttributes ProtectedInternal = (MethodAttributes)AccessModifiers.ProtectedInternal;
        public const MethodAttributes Static = (MethodAttributes)AccessModifiers.Static;
        public const MethodAttributes Final = MethodAttributes.Final;
        public const MethodAttributes Abstract = MethodAttributes.Abstract;
        public const MethodAttributes AccessMask = (MethodAttributes)AccessModifiers.AccessMask;
    }
}