using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Jlw.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel, TSchema>
    {
        /// <summary>
        /// Static backing field to store list of constructors
        /// </summary>
        protected static IEnumerable<ConstructorSchema> _constructorSchema => modelSchema.ConstructorList;

        protected static bool IsConstructorListEmpty => _constructorSchema?.Count(o => o != null) < 1;
        /// <summary>
        /// Static accessor to retrieve schema list via DynamicDataAttribute argument pattern
        /// </summary>
        public static IEnumerable<object[]> ConstructorList => _constructorSchema.Select(o => new object[] { o });

        public static IEnumerable<object[]> InstanceMemberTestList => modelSchema.InstanceMemberTestList.Select(o => new object[] { o });

        #region Constructor Tests
        /// <summary>
        /// Asserts that the number of implemented constructors matches the number of distinct records in the schema list.
        /// Records are filtered by access modifiers.
        /// </summary>
        /// <param name="access">AccessModifier to filter by</param>
        [TestMethod]
        [DataRow(Public)]

        public virtual void Constructor_Count_Should_Match(AccessModifiers access)
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedConstructorKeys(access).ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedConstructorKeys(access).ToArray();

            // Output count to console for information purposes
            Console.WriteLine($"\t✓\tNumber of implemented {GetAccessString(access)} constructors is {implementedKeys.Length}");

            OutputImplementedConstructors(implementedKeys, expectedKeys);
            OutputExpectedConstructors(implementedKeys, expectedKeys);

            // retrieve count of expected constructors
            int nCount = expectedKeys.Length;

            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (IsConstructorListEmpty) Console.WriteLine($"\t-\tNo constructor schema added. Skipping Test");
            if (IsConstructorListEmpty) Assert.Inconclusive();

            // Assert that the count is correct
            Assert.AreEqual(nCount, implementedKeys.Length, $"\n\t✗\tNumber of implemented constructors is incorrect. Should be {nCount}.");
            
            // Output success to console for information purposes
            Console.WriteLine($"\t✓\tNumber of expected {GetAccessString(access)} constructors is {nCount}");
        }

        /// <summary>
        /// Asserts that all of the expected constructors match the implemented signatures and vice versa.
        /// </summary>
        /// <param name="access">AccessModifier to filter by</param>
        [TestMethod]
        [DataRow(Public)]
        public virtual void Constructor_Signatures_Should_Match(AccessModifiers access)
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedConstructorKeys(access).ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedConstructorKeys(access).ToArray();
            // Declare variable to hold Dictionary of matched values
            var matches = new Dictionary<string, bool>();

            // Output count to console for information purposes
            Console.WriteLine($"\t✓\tNumber of implemented {GetAccessString(access)} constructors is {implementedKeys.Length}");

            OutputImplementedConstructors(implementedKeys, expectedKeys);
            OutputExpectedConstructors(implementedKeys, expectedKeys);

            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (IsConstructorListEmpty) Console.WriteLine($"\t-\tNo constructor schema added. Skipping Test");
            if (IsConstructorListEmpty) Assert.Inconclusive();

            foreach (string sKey in implementedKeys)
            {
                matches[sKey] = expectedKeys.Contains(sKey);
            }
            foreach (string sKey in expectedKeys)
            {
                matches[sKey] = implementedKeys.Contains(sKey);
            }

            Assert.IsTrue(matches.All(o=>o.Value == true), $"\n\t✗\tNot all implemented {GetAccessString(access)} constructors match the expected {GetAccessString(access)} constructors.");

        }

        [TestMethod]
        [DynamicData(nameof(ConstructorList))]
        public virtual void Constructor_Should_Exist(ConstructorSchema schema)
        {
            // If schema is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema == null) Console.WriteLine($"\t-\tschema is NULL. Skipping Test");
            if (schema == null) Assert.Inconclusive();

            var t = typeof(TModel);
            var ctors = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.CreateInstance | BindingFlags.FlattenHierarchy);
            ConstructorInfo ctor = null;// = t.GetConstructor(schema.Arguments.ToArray());
            string sArgList = DataUtility.GetTypeArgs(schema.Arguments.ToArray());
            foreach (var ctx in ctors.Where(o=>(o.Attributes & (MethodAttributes)AccessModifiers.AccessMask) == (MethodAttributes)schema.Access))
            {
                if (sArgList == DataUtility.GetTypeArgs(ctx.GetParameters().Select(p => p.ParameterType).ToArray()))
                {
                    ctor = ctx;
                }
            }
            Assert.IsNotNull(ctor, $"\n\t✗\tUnable to match constructor {GetAccessString(schema.Access)} {typeof(TModel).Name}({sArgList})");
            Console.WriteLine($"\t✓\tExpected constructor {GetAccessString(schema.Access)} {typeof(TModel).Name}({sArgList}) exists");
        }

        
        [TestMethod]
        [DynamicData(nameof(InstanceMemberTestList))]
        public void Member_Should_Match_For_Instance(InstanceMemberTestData<TModel> data)
        {
            // If schema is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (data == null) Console.WriteLine($"\t-\tschema is NULL. Skipping Test");
            if (data == null) Assert.Inconclusive();

            // Act
            //var member = AssertGetFieldInfoByName(data.MemberName, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var prop = typeof(TModel).GetProperty(data.MemberName, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (prop != null)
            {
                object actual = prop.GetValue(data.SystemUnderTest);
                Assert.AreEqual(data.ExpectedValue, actual);
                return;
            }

            var field = typeof(TModel).GetField(data.MemberName, BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (field != null)
            {
                object actual = field.GetValue(data.SystemUnderTest);
                Assert.AreEqual(data.ExpectedValue, actual);
                return;
            }

            Assert.Fail($"{data.MemberName} is not a field or property of {DataUtility.GetTypeName(typeof(TModel))}");
        }


        #region Helper Methods
        protected IEnumerable<string> GetExpectedConstructorKeys(AccessModifiers access = AccessModifiers.Public)
        {
            var aReturn = new List<string>();
            var schemaList = _constructorSchema?.Where(o => o?.Access == access).ToArray();
            if (schemaList?.Length > 0)
            {
                foreach (var schema in schemaList)
                {
                    aReturn.Add($"{GetAccessString(schema.Access)} {typeof(TModel).Name}({DataUtility.GetTypeArgs(schema.Arguments.ToArray())})");
                }
            }

            return aReturn.Distinct();
        }

        protected IEnumerable<string> GetImplementedConstructorKeys(AccessModifiers access = AccessModifiers.Public)
        {
            var aReturn = new List<string>();
            var t = typeof(TModel);
            ConstructorInfo[] info = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.CreateInstance | BindingFlags.FlattenHierarchy);
            foreach (var i in info.Where(o=>(o.Attributes & (MethodAttributes)AccessModifiers.AccessMask) == (MethodAttributes)access))
            {
                var types = i.GetParameters().Select(o => o.ParameterType).ToArray();
                aReturn.Add($"{GetAccessString(i.Attributes)} {typeof(TModel).Name}({DataUtility.GetTypeArgs(types)})");
            }

            return aReturn.Distinct();
        }

        protected void OutputImplementedConstructors(string[] implementedKeys, string[] expectedKeys)
        {
            // Generate list of implemented constructors
            // Output list to console for information purposes
            if (implementedKeys.Length > 0)
            {
                Console.WriteLine($"\t\tConstructors Implemented:");
                OutputImplementedKeys(implementedKeys, expectedKeys);
            }
        }

        /// <summary>
        /// Generate list of implemented constructors, and outputs results to console
        /// </summary>
        /// <param name="implementedKeys"></param>
        /// <param name="expectedKeys"></param>
        protected void OutputExpectedConstructors(string[] implementedKeys, string[] expectedKeys)
        {
            // 
            if (expectedKeys.Length > 0)
            {
                Console.WriteLine($"\t\tConstructors Expected:");
                OutputExpectedKeys(implementedKeys, expectedKeys);
            }
        }
        #endregion

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
