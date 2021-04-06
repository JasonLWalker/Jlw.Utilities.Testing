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
        public class MemberSchema
        {
            public virtual string Name { get; protected set; }
            public virtual Type Type { get; protected set; }
            public virtual AccessModifiers Access { get; protected set; }
            public BindingFlags BindingFlags { get; protected set; }

            public bool CanTestSignature { get; protected set; }

            public MemberSchema(string name, Type type = null, AccessModifiers access = AccessModifiers.Public, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy, bool canTestSignature = true)
            {
                Name = name;
                Type = type;
                Access = access;
                BindingFlags = bindingFlags;
                CanTestSignature = canTestSignature;
            }

            public override string ToString()
            {
                string type = DataUtility.GetTypeName(Type);
                string access = GetAccessString((MethodAttributes) Access);

                return $"{access} {type} {Name}";
            }
        }
    }
}