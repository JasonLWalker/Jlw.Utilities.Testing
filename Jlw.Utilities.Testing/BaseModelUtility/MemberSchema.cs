using System;
using System.Reflection;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        public class MemberSchema
        {
            public string Name { get; protected set; }
            public Type Type { get; protected set; }
            public virtual AccessModifiers Access { get; protected set; }
            public BindingFlags BindingFlags { get; protected set; }

            public MemberSchema(string name, Type type = null, AccessModifiers access = AccessModifiers.Public, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
            {
                Name = name;
                Type = type;
                Access = access;
                BindingFlags = bindingFlags;
            }

            public override string ToString()
            {
                string type = GetTypeName(Type);
                string access = GetAccessString((MethodAttributes) Access);

                return $"{access} {type} {Name}";
            }
        }
    }
}