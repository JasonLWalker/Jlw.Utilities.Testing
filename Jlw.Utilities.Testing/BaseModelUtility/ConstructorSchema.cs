using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        public class ConstructorSchema : MemberSchema
        {
            protected List<Type> _arguments = new List<Type>();
            public IEnumerable<Type> Arguments => _arguments;
            
            public ConstructorSchema(AccessModifiers access, IEnumerable<Type> args = null, IEnumerable<IEnumerable<object>> testArgs = null, Func<TModel, object[], bool> fnCallback = null) : base(typeof(TModel).Name, typeof(TModel), access, default)
            {
                if (args != null)
                {
                    _arguments.AddRange(args);
                }


            }

            public override string ToString()
            {
                string sArgList = DataUtility.GetTypeArgs(Arguments.ToArray());
                return $"{GetAccessString((MethodAttributes)Access)} {typeof(TModel).Name}({sArgList})";
            }
        }
    }
}