using System;
using System.Reflection;
using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        public class PropertySchema : MemberSchema
        {
            public AccessModifiers? GetAttributes { get; protected set; }
            public AccessModifiers? SetAttributes { get; protected set; }

            public bool CanTestValue { get; protected set; }

            public override AccessModifiers Access => GetPropertyAccess((MethodAttributes)(GetAttributes ?? default), (MethodAttributes)(SetAttributes ?? default));

            public PropertySchema(string name, Type type, BindingFlags flags = BindingFlags.Public, AccessModifiers? getAttr = AccessModifiers.Public, AccessModifiers? setAttr = AccessModifiers.Protected, bool canTestValue = true) : base(name, type, default, flags)
            {
                GetAttributes = getAttr;
                SetAttributes = setAttr;
                CanTestValue = canTestValue;
            }

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