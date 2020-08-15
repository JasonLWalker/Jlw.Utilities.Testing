using System.Reflection;

namespace Jlw.Standard.Utilities.Testing
{
    public static class AccessScope
    {
        public const MethodAttributes Public = MethodAttributes.Public;
        public const MethodAttributes Private = MethodAttributes.Private;
        public const MethodAttributes Protected = MethodAttributes.Family;
        public const MethodAttributes PrivateProtected = MethodAttributes.FamANDAssem;
        public const MethodAttributes Internal = MethodAttributes.Assembly;
        public const MethodAttributes ProtectedInternal = MethodAttributes.FamORAssem;
        public const MethodAttributes Static = MethodAttributes.Static;
        public const MethodAttributes Final = MethodAttributes.Final;
        public const MethodAttributes Abstract = MethodAttributes.Abstract;
        
        public static class Accessors
        {
            public const MethodAttributes Public = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            public const MethodAttributes Private = MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            public const MethodAttributes Protected = MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            public const MethodAttributes PrivateProtected = MethodAttributes.FamANDAssem | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            public const MethodAttributes Internal = MethodAttributes.Assembly | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            public const MethodAttributes ProtectedInternal = MethodAttributes.FamORAssem | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
        }
    }
}