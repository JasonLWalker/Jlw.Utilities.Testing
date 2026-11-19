using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        /// <summary>
        /// Describes an expected constructor of <typeparamref name="TModel"/>: its access level and argument types.
        /// </summary>
        /// <remarks>
        /// The schema's name is the model type name, and its type is the model type. Used by the constructor tests
        /// to match against the constructors that the model actually declares.
        /// </remarks>
        public class ConstructorSchema : MemberSchema
        {
        /// <summary>
        /// Backing field for <see cref="Arguments"/>.
        /// </summary>
            protected List<Type> _arguments = new List<Type>();
        /// <summary>
        /// Gets the argument types of the expected constructor, in declaration order.
        /// </summary>
            public IEnumerable<Type> Arguments => _arguments;
            
        /// <summary>
        /// Creates a constructor schema with the given access level and argument types.
        /// </summary>
        /// <param name="access">The expected access level of the constructor.</param>
        /// <param name="args">The argument types of the constructor, in order. Use <c>null</c> or an empty list for a parameterless constructor.</param>
            public ConstructorSchema(AccessModifiers access, IEnumerable<Type> args = null) : base(typeof(TModel).Name, typeof(TModel), access, default)
            {
                if (args != null)
                {
                    _arguments.AddRange(args);
                }


            }

        /// <summary>
        /// Returns the constructor signature in the form <c>access TypeName(argTypes)</c>, for example <c>public MyModel(System.String)</c>.
        /// </summary>
        /// <returns>The constructor signature.</returns>
            public override string ToString()
            {
                string sArgList = DataUtility.GetTypeArgs(Arguments.ToArray());
                return $"{GetAccessString((MethodAttributes)Access)} {typeof(TModel).Name}({sArgList})";
            }
        }
    }
}
