using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel> : BaseModelUtility<TModel> where TModel : class, new()
    {
        
        public const AccessModifiers Private = AccessModifiers.Private;
        public const AccessModifiers PrivateProtected = AccessModifiers.PrivateProtected;
        public const AccessModifiers Internal = AccessModifiers.Internal;
        public const AccessModifiers Protected = AccessModifiers.Protected;
        public const AccessModifiers ProtectedInternal = AccessModifiers.ProtectedInternal;
        public const AccessModifiers Public = AccessModifiers.Public;
        public const AccessModifiers Static = AccessModifiers.Static;

    }
}
