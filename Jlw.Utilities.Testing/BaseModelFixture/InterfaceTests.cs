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

        #region Interface Tests

        [TestMethod]
        [DynamicData(nameof(ImplementedInterfaceList))]
        public void Interface_IsAssignable(Type type)
        {
            if (type is null)
            {
                Console.WriteLine($"\t✓ type is NULL. Skipping Test");
                Assert.Inconclusive();
            }

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
        public void Interface_Count_ShouldMatch()
        {
            if (_implementedInterfaceTypes.Count(o => o != null) < 1)
            {
                Console.WriteLine($"\t✓ No property schema added. Skipping Test");
                Assert.Inconclusive();
            }

            var t = typeof(TModel);
            var types = t.GetInterfaces();

            Assert.IsNotNull(types);
            Console.WriteLine($"\t✓ class interface list is not NULL");

            string sImplemented = "";
            foreach (var k in types)
            {
                sImplemented += $"\t{DataUtility.GetTypeName(k)}\n";
            }

            int nCount = _implementedInterfaceTypes.Count(o => o != null);


            Assert.AreEqual(nCount, types.Length, $"Number of implemented interfaces is incorrect. Should be {nCount}. Interfaces Implemented:\n{sImplemented}");
            Console.WriteLine($"\t✓ Number of implemented interfaces is {nCount}");
        }

        #endregion

    }
}
