using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.UnitTests
{
    public class FailedModelFixture<TModel, TSchema> : BaseModelFixture<TModel, TSchema> where TSchema : BaseModelSchema<TModel>, new() where TModel : class
    {
        public new static IEnumerable<object[]> ConstructorList => _constructorSchema.Select(o => new object[] { o });
        public new static IEnumerable<object[]> ImplementedInterfaceList => _implementedInterfaceTypes.Select(o => new object[] { o });

        [TestMethod]
        [DataRow(Public)]
        [DataRow(Private | Static)]
        public override void Constructor_Count_Should_Match(AccessModifiers access)
        {
            Assert.Throws<AssertFailedException>(() =>
            {
                try
                {
                    base.Constructor_Count_Should_Match(access);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "\n\t✓\tAssertion failed for incorrect constructor count. (This is the correct result)");
                    Console.WriteLine($"\n\tAssertion details: {ex.Message}");
                    throw;
                }
            });
        }

        [TestMethod]
        [DynamicData(nameof(ConstructorList))]
        public override void Constructor_Should_Exist(ConstructorSchema schema)
        {
            Assert.Throws<AssertFailedException>(() =>
            {
                try
                {
                    base.Constructor_Should_Exist(schema);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "\n\t✓\tAssertion failed for constructor that doesn't exist. (This is the correct result)");
                    Console.WriteLine($"\n\tAssertion details: {ex.Message}");
                    throw;
                }
            });
    }

        [TestMethod]
        [DataRow(Public)]
        [DataRow(Private | Static)]
        public override void Constructor_Signatures_Should_Match(AccessModifiers access)
        {
            Assert.Throws<AssertFailedException>(() =>
            {
                try
                {
                    base.Constructor_Signatures_Should_Match(access);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "\n\t✓\tAssertion failed for incorrect constructor count. (This is the correct result)");
                    Console.WriteLine($"\n\tAssertion details: {ex.Message}");
                    throw;
                }
            });
        }

        [TestMethod]
        public override void Interface_Count_Should_Match()
        {
            Assert.Throws<AssertFailedException>(() =>
            {
                CatchFailedAssertion(() => { base.Interface_Count_Should_Match(); },
                    "\n\t✓\tAssertion failed for incorrect constructor count. (This is the correct result)");
            });
        }


        [TestMethod]
        [DynamicData(nameof(ImplementedInterfaceList))]
        public override void Interface_Is_Assignable(Type type)
        {
            Assert.Throws<AssertFailedException>(() =>
            {
                CatchFailedAssertion(() => { base.Interface_Is_Assignable(type); },
                    "\n\t✓\tAssertion failed for incorrect constructor count. (This is the correct result)");
            });
        }

        protected void CatchFailedAssertion(Action fn, string message="")
        {
            try
            {
                fn();
            }
            catch (Exception ex)
            {
                Console.WriteLine(message);
                Console.WriteLine($"\n\tAssertion details: {ex.Message}");
                throw;
            }

        }
    }
}