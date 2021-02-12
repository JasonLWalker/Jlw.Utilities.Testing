using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel, TSchema>
    {
        protected static IEnumerable<BaseModelUtility<TModel>.ConstructorSchema> _constructorSchema = modelSchema.ConstructorList;

        public static IEnumerable<object[]> ConstructorList => _constructorSchema.Select(o => new object[] { o });



        #region Constructor Tests
        [TestMethod]
        public virtual void Constructor_Count_ShouldMatch()
        {
            if (_constructorSchema.Count(o => o != null) < 1)
            {
                Console.WriteLine($"\t✓ No constructor schema added. Skipping Test");
                Assert.Inconclusive();
            }

            var t = typeof(TModel);
            ConstructorInfo[] info = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            Assert.IsNotNull(info, "public ConstructorInfo is null");
            Console.WriteLine($"\t✓ public ConstructorInfo is not NULL");

            string sImplemented = "";
            foreach (var i in info)
            {
                var types = i.GetParameters().Select(o => o.ParameterType).ToArray();
                sImplemented += $"\t{typeof(TModel).Name}({GetTypeArgs(types)})\n";
            }

            int nCount = _constructorSchema.Count(o => o != null);
            Assert.AreEqual(nCount, info.Length, $"Number of implemented constructors is incorrect. Should be {nCount}. Constructors implemented:\n{sImplemented}");
            Console.WriteLine($"\t✓ Number of public constructors is {nCount}");
        }

        [TestMethod]
        [DynamicData(nameof(ConstructorList))]
        public virtual void Constructors_ShouldExist(ConstructorSchema schema)
        {
            if (schema == null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
            }

            var t = typeof(TModel);
            ConstructorInfo[] info = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            Assert.IsNotNull(info, "public ConstructorInfo is null");

            string sImplemented = "";
            foreach (var i in info)
            {
                var types = i.GetParameters().Select(o => o.ParameterType).ToArray();
                sImplemented += $"\t{typeof(TModel).Name}({GetTypeArgs(types)})\n";
            }

            var ctor = t.GetConstructor(schema.Arguments.ToArray());
            string sArgList = GetTypeArgs(schema.Arguments.ToArray());
            Assert.IsNotNull(ctor, $"Unable to match constructor {typeof(TModel).Name}({sArgList})");
            Console.WriteLine($"\t✓ constructor {typeof(TModel).Name}({sArgList}) exists");

            Assert.AreEqual(_constructorSchema.Count(), info.Length, $"Number of implemented constructors is incorrect. Should be {_constructorSchema.Count()}. Constructors implemented:\n{sImplemented}");
            Console.WriteLine($"\t✓ Number of public constructors is {_constructorSchema.Count()}");
        }

        /*
        [TestMethod]
        [DynamicData(nameof(ConstructorTests))]
        public virtual void Constructor_Initialization_ShouldMatch(ConstructorAssertion model)
        {
            if (model?.Schema == null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                return;
            }

            if (model.AssertionCallback == null)
            {
                Console.WriteLine($"\t✓ Assertion Callback is NULL. Skipping Test");
                return;
            }

            var t = typeof(TModel);
            var schema = model.Schema;
            object[] initData = model.InitializationData?.Select(o => (object)o).ToArray();


            var ctor = t.GetConstructor(schema.Arguments.ToArray());
            string sArgList = GetTypeArgs(schema.Arguments.ToArray());
            Assert.IsNotNull(ctor, $"Unable to match constructor {typeof(TModel).Name}({sArgList})");

            if (model.Sut == null)
            {
                var sut = (TModel)ctor.Invoke(initData);
                model.Sut = sut;
            }
            model.AssertionCallback(model);
        }
        */
        #endregion

    }
}
