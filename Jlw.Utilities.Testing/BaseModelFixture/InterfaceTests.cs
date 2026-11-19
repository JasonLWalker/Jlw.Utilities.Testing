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
        /// Gets the interface types the model schema expects <typeparamref name="TModel"/> to implement.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        protected static IEnumerable<Type> _implementedInterfaceTypes => modelSchema.ImplementedInterfaceList;

        /// <summary>
        /// Static accessor to retrieve expected interface types via DynamicDataAttribute argument pattern.
        /// </summary>
        public static IEnumerable<object[]> ImplementedInterfaceList => _implementedInterfaceTypes.Select(o => new object[] { o });
        /// <summary>
        /// Gets a value indicating whether the interface schema is missing or contains no non-null entries.
        /// </summary>
        protected static bool IsInterfaceListEmpty => _implementedInterfaceTypes?.Count(o => o != null) < 1;

        #region Interface Tests

        /// <summary>
        /// Asserts that <typeparamref name="TModel"/> implements the interface described by the schema, and that the
        /// total number of implemented interfaces matches the schema.
        /// </summary>
        /// <remarks>
        /// If <paramref name="type"/> is <c>null</c>, the test passes when no interfaces are implemented and is inconclusive otherwise.
        /// </remarks>
        /// <param name="type">The expected interface type to check for.</param>
        [TestMethod]
        [DynamicData(nameof(ImplementedInterfaceList))]
        public virtual void Interface_Is_Assignable(Type type)
        {
            var t = typeof(TModel);
            var types = t.GetInterfaces();

            if (type is null)
            {
                TestContext?.WriteLine($"\t✓ type is NULL. Skipping Test");
                if (types.Length < 1)
                {
                    TestContext?.WriteLine($"\t✓ No interfaces exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputInterfaceCountAndList();
                Assert.Inconclusive();
            }

            Assert.IsNotNull(types);
            TestContext?.WriteLine($"\t✓ type list is not NULL");

            string sImplemented = "";
            foreach (var k in types)
            {
                sImplemented += $"\t{DataUtility.GetTypeName(k)}\n";
            }

            Assert.IsTrue(types.Any(type.IsAssignableFrom), $"Does not implement {type}");
            TestContext?.WriteLine($"\t✓ implements interface {DataUtility.GetTypeName(type)}");

            Assert.AreEqual(_implementedInterfaceTypes.Count(), types.Length, $"Number of implemented interfaces is incorrect. Should be {_implementedInterfaceTypes.Count()}. Interfaces Implemented:\n{sImplemented}");
            TestContext?.WriteLine($"\t✓ Number of interfaces is {_implementedInterfaceTypes.Count()}");
        }


        /// <summary>
        /// Asserts that the number of interfaces implemented by <typeparamref name="TModel"/> matches the number of
        /// expected interfaces in the schema.
        /// </summary>
        /// <remarks>
        /// If no interface schema is defined, the test passes when no interfaces are implemented and is inconclusive otherwise.
        /// </remarks>
        [TestMethod]
        public virtual void Interface_Count_Should_Match()
        {
            // If interface list is empty, then skip the test. (2 IF statements are used to pass code coverage)
            var t = typeof(TModel);
            var types = t.GetInterfaces();

            if (IsInterfaceListEmpty)
            {
                TestContext?.WriteLine($"\t✓ No interface schema added. Skipping Test");
                if (types.Length < 1)
                {
                    TestContext?.WriteLine($"\t✓ No interfaces exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }


                OutputInterfaceCountAndList();
                Assert.Inconclusive();
            }

            var expectedKeys = GetExpectedInterfaceKeys().ToArray();
            var implementedKeys = GetImplementedInterfaceKeys().ToArray();

            TestContext?.WriteLine($"\t✓ Number of implemented interfaces is {implementedKeys.Length}");
            OutputImplementedInterfaces(implementedKeys, expectedKeys);
            OutputExpectedInterfaces(implementedKeys, expectedKeys);

            Assert.AreEqual(expectedKeys.Length, implementedKeys.Length, $"Number of implemented interfaces is incorrect. Should be {expectedKeys.Length}.");
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Builds the distinct names of the schema's expected interfaces.
        /// </summary>
        /// <returns>Distinct interface names.</returns>
        protected IEnumerable<string> GetExpectedInterfaceKeys()
        {
            var aReturn = new List<string>();
            var schemaList = _implementedInterfaceTypes?.ToArray();
            if (schemaList?.Length > 0)
            {
                foreach (var schema in schemaList)
                {
                    if (schema != null)
                        aReturn.Add(schema.Name);
                }
            }

            return aReturn.Distinct();
        }

        /// <summary>
        /// Builds the distinct names of the public interfaces implemented by <typeparamref name="TModel"/>.
        /// </summary>
        /// <returns>Distinct interface names.</returns>
        protected IEnumerable<string> GetImplementedInterfaceKeys()
        {
            var aReturn = new List<string>();
            var t = typeof(TModel);
            Type[] info = t.GetInterfaces();
            foreach (var i in info.Where(o => o.IsPublic))
            {
                //var types = i.GetParameters().Select(o => o.ParameterType).ToArray();
                aReturn.Add(i.Name);
            }

            return aReturn.Distinct();
        }

        /// <summary>
        /// Writes the implemented interface keys to the test output, marked against the expected keys.
        /// </summary>
        /// <param name="implementedKeys">The names of the interfaces implemented on the model.</param>
        /// <param name="expectedKeys">The names of the interfaces expected by the schema.</param>
        protected void OutputImplementedInterfaces(string[] implementedKeys, string[] expectedKeys)
        {
            // Generate list of implemented constructors
            // Output list to console for information purposes
            if (implementedKeys.Length > 0)
            {
                TestContext?.WriteLine($"\t\tInterfaces Implemented:");
                OutputImplementedKeys(implementedKeys, expectedKeys);
            }
        }

        /// <summary>
        /// Writes the expected interface keys to the test output, marked against the implemented keys.
        /// </summary>
        /// <param name="implementedKeys">The names of the interfaces implemented on the model.</param>
        /// <param name="expectedKeys">The names of the interfaces expected by the schema.</param>
        protected void OutputExpectedInterfaces(string[] implementedKeys, string[] expectedKeys)
        {
            if (expectedKeys.Length > 0)
            {
                TestContext?.WriteLine($"\t\tInterfaces Expected:");
                OutputExpectedKeys(implementedKeys, expectedKeys);
            }
        }

        /// <summary>
        /// Writes the implemented interface count and keys to the test output.
        /// </summary>
        protected void OutputInterfaceCountAndList()
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedInterfaceKeys().ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedInterfaceKeys().ToArray();
            TestContext?.WriteLine($"\t✓\tNumber of implemented interfaces is {implementedKeys.Length}");
            OutputImplementedKeys(implementedKeys, expectedKeys);
        }


        #endregion



    }
}
