using System;
using System.Reflection;

namespace Jlw.Utilities.Testing
{
    [Flags]
    public enum AccessModifiers
    {
        Private = (int)MethodAttributes.Private,
        PrivateProtected = (int)MethodAttributes.FamANDAssem,
        Internal = (int)MethodAttributes.Assembly,
        Protected = (int)MethodAttributes.Family,
        ProtectedInternal = (int)MethodAttributes.FamORAssem,
        Public = (int)MethodAttributes.Public,
        Static = (int)MethodAttributes.Static,
    }
}