using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel, TSchema> : BaseModelUtility<TModel> 
        where TModel : class, new()
        where TSchema : BaseModelSchema<TModel>, new()
    {
        public static TSchema modelSchema = new TSchema();;
        
        public const AccessModifiers Private = AccessModifiers.Private;
        public const AccessModifiers PrivateProtected = AccessModifiers.PrivateProtected;
        public const AccessModifiers Internal = AccessModifiers.Internal;
        public const AccessModifiers Protected = AccessModifiers.Protected;
        public const AccessModifiers ProtectedInternal = AccessModifiers.ProtectedInternal;
        public const AccessModifiers Public = AccessModifiers.Public;
        public const AccessModifiers Static = AccessModifiers.Static;

        #region Class Tests
        [TestMethod]
        public virtual void Should_Be_Class()
        {
            var t = typeof(TModel);

            Assert.IsTrue(t.IsClass, $"{GetTypeName(t)} is a class");
            Console.WriteLine($"\t✓ {typeof(TModel).Name} is a class");
        }

        [TestMethod]
        public virtual void Should_Be_Public()
        {
            var t = typeof(TModel);
            Assert.IsTrue(t.IsPublic, $"{GetTypeName(t)} is not public");
            Console.WriteLine($"\t✓ {typeof(TModel).Name} is public");
        }
        #endregion


    }
}
