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
        // ReSharper disable once StaticMemberInGenericType
        protected static IEnumerable<Type> _implementedInterfaceTypes => modelSchema.ImplementedInterfaceList;

        public static IEnumerable<object[]> ImplementedInterfaceList => _implementedInterfaceTypes.Select(o => new object[] { o });
        protected static bool IsInterfaceListEmpty => _implementedInterfaceTypes?.Count(o => o != null) < 1;

        #region Interface Tests

        [TestMethod]
        [DynamicData(nameof(ImplementedInterfaceList))]
        public virtual void Interface_IsAssignable(Type type)
        {
            if (type is null) Console.WriteLine($"\t✓ type is NULL. Skipping Test");
            if (type is null) Assert.Inconclusive();

            var t = typeof(TModel);
            var types = t.GetInterfaces();

            Assert.IsNotNull(types);
            Console.WriteLine($"\t✓ type list is not NULL");

            string sImplemented = "";
            foreach (var k in types)
            {
                sImplemented += $"\t{DataUtility.GetTypeName(k)}\n";
            }

            Assert.IsTrue(types.Any(type.IsAssignableFrom), $"Does not implement {type}");
            Console.WriteLine($"\t✓ implements interface {DataUtility.GetTypeName(type)}");

            Assert.AreEqual(_implementedInterfaceTypes.Count(), types.Length, $"Number of implemented interfaces is incorrect. Should be {_implementedInterfaceTypes.Count()}. Interfaces Implemented:\n{sImplemented}");
            Console.WriteLine($"\t✓ Number of interfaces is {_implementedInterfaceTypes.Count()}");
        }


        [TestMethod]
        public virtual void Interface_Count_ShouldMatch()
        {
            // If interface list is empty, then skip the test. (2 IF statements are used to pass code coverage)
            if (IsInterfaceListEmpty) Console.WriteLine($"\t✓ No interface schema added. Skipping Test");
            if (IsInterfaceListEmpty) Assert.Inconclusive();

            var t = typeof(TModel);
            var types = t.GetInterfaces();

            var expectedKeys = GetExpectedInterfaceKeys().ToArray();
            var implementedKeys = GetImplementedInterfaceKeys().ToArray();

            Console.WriteLine($"\t✓ Number of implemented interfaces is {implementedKeys.Length}");
            OutputImplementedInterfaces(implementedKeys, expectedKeys);
            OutputExpectedInterfaces(implementedKeys, expectedKeys);

            Assert.AreEqual(expectedKeys.Length, implementedKeys.Length, $"Number of implemented interfaces is incorrect. Should be {expectedKeys.Length}.");
        }

        #endregion

        #region Helper Methods
        protected IEnumerable<string> GetExpectedInterfaceKeys()
        {
            var aReturn = new List<string>();
            var schemaList = _implementedInterfaceTypes?.ToArray();
            if (schemaList?.Length > 0)
            {
                foreach (var schema in schemaList)
                {
                    aReturn.Add(schema.Name);
                }
            }

            return aReturn.Distinct();
        }

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

        protected void OutputImplementedInterfaces(string[] implementedKeys, string[] expectedKeys)
        {
            // Generate list of implemented constructors
            // Output list to console for information purposes
            if (implementedKeys.Length > 0)
            {
                Console.WriteLine($"\t\tInterfaces Implemented:");
                OutputImplementedKeys(implementedKeys, expectedKeys);
            }
        }

        /// <summary>
        /// Generate list of implemented constructors, and outputs results to console
        /// </summary>
        /// <param name="implementedKeys"></param>
        /// <param name="expectedKeys"></param>
        protected void OutputExpectedInterfaces(string[] implementedKeys, string[] expectedKeys)
        {
            if (expectedKeys.Length > 0)
            {
                Console.WriteLine($"\t\tInterfaces Expected:");
                OutputExpectedKeys(implementedKeys, expectedKeys);
            }
        }
        #endregion



    }
}
