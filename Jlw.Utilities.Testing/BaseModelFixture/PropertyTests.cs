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

        protected static IEnumerable<PropertySchema> _propertySchema = modelSchema.PropertySchemaList;//new List<PropertySchema>() { null };

        public static IEnumerable<object[]> PropertySchemaList => _propertySchema.Select(o => new object[] { o });

        public static IEnumerable<object[]> PropertyValueList
        {
            get
            {
                var val = _propertySchema.Where(o => o?.SetAttributes != null && o?.GetAttributes != null).Select(o => new object[] { o });

                return !val.Any() ? new List<object[]>(){new object[]{null}} :  val;
            }
        }


        #region Property Tests
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        /*
        [DataRow(Protected)]
        [DataRow(Protected | Static)]
        [DataRow(Internal)]
        [DataRow(Internal | Static)]
        [DataRow(ProtectedInternal)]
        [DataRow(ProtectedInternal | Static)]
        [DataRow(PrivateProtected)]
        [DataRow(PrivateProtected | Static)]
        */
        public virtual void Property_Count_ShouldMatch(AccessModifiers accessModifiers, bool flattenHierarchy = true)
        {
            if (_propertySchema.Count(o => o != null) < 1)
            {
                Console.WriteLine($"\t✓ No property schema added. Skipping Test");
                Assert.Inconclusive();
            }

            BindingFlags flags = flattenHierarchy ? BindingFlags.FlattenHierarchy : default;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;


            var t = typeof(TModel);
            var aInfo = t.GetProperties(flags);

            Assert.IsNotNull(aInfo, $"Unable to retrieve PropertyInfo for {GetTypeName(t)} with BindingFlags: {flags}");
            Console.WriteLine($"\t✓ PropertyInfo retrieved");
            BindingFlags mask = ~(BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            int nCount = _propertySchema.Count(o => o != null && ((o.BindingFlags & mask) == (flags & mask)) && o.Access.Equals(accessModifiers));
            int nPropCount = 0;
            string sProps = "";
            foreach (var info in aInfo)
            {
                if (accessModifiers.Equals(GetPropertyAccess(info.GetMethod?.Attributes ?? default, info.SetMethod?.Attributes ?? default)))
                {
                    string access = GetAccessString(info);
                    sProps += $"\t\t{access} {GetTypeName(info.PropertyType)} {info.Name}\n";
                    nPropCount++;
                }
            }
            Console.WriteLine($"\t✓ Properties Retrieved:\n{sProps}");

            Assert.AreEqual(nCount, nPropCount, $"Number of properties is incorrect. Should be {nCount} for BindingFlags: {flags}");
            Console.WriteLine($"\t✓ Number of properties is {nCount} for BindingFlags: {flags}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_ShouldExist(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }

            var t = typeof(TModel);
            var info = AssertPropertyExists(schema.Name);
            Console.WriteLine($"\t✓ property [{schema.Name}] exists with PropertyType.Attributes: {info.PropertyType.Attributes}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Binding_ShouldMatch(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }

            var t = typeof(TModel);
            var info = AssertGetPropertyInfoByName(schema.Name, schema.BindingFlags);
            Console.WriteLine($"\t✓ property [{schema.Name}] retrieved with BindingFlags: {schema.BindingFlags}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Type_IsAssignable(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }

            var t = typeof(TModel);
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);
            
            Assert.IsTrue(schema.Type.IsAssignableFrom(info.PropertyType), $"[{GetTypeName(schema.Type)}] is not assignable from [{GetTypeName(info.PropertyType)}]");
            if (schema.Type == info.PropertyType)
                Console.WriteLine($"\t✓ typeof({GetTypeName(schema.Type)}) matches property with the signature: \n\t\t\t{schema}");
            else
                Console.WriteLine($"\t✓ typeof({GetTypeName(schema.Type)}) is implemented by property {schema}");
        }


        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_AccessModifiers_ShouldMatch(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);


            // Assert
            Assert.AreEqual(schema.Access, GetPropertyAccess(info?.GetMethod?.Attributes ?? default, info?.SetMethod?.Attributes ?? default), $"Access modifiers do not match for property with the signature:\n\t\t\t{schema}");

            Console.WriteLine($"\t✓ property access modifiers match: {schema}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_GetAccessor_ShouldMatch(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);
            
            // Assert
            if (schema.GetAttributes == null || info.GetMethod == null)
            {
                Assert.AreEqual(schema.GetAttributes, info.GetMethod, schema.GetAttributes == null ? "Get method should not exist" : "Get method should exist");
                Console.WriteLine($"\t✓ property get method should not exist");
            }
            else
            {

                Assert.IsTrue(info.GetMethod.Attributes.HasFlag((MethodAttributes) schema.GetAttributes), $"property [{schema.Name}] attributes [{info.GetMethod.Attributes}] do not match [{schema.GetAttributes}]");
            }

            Console.WriteLine($"\t✓ property [{schema.Name}] attributes match: {schema.GetAttributes}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_SetAccessor_ShouldMatch(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);

            // Assert
            if (schema.SetAttributes == null || info.SetMethod == null)
            {
                Assert.AreEqual(schema.SetAttributes, info.SetMethod, schema.SetAttributes == null ? "set accessor should not exist" : "set accessor should exist");
                Console.WriteLine($"\t✓ property set accessor should not exist");
            }
            else
            {

                Assert.IsTrue(info.SetMethod.Attributes.HasFlag((MethodAttributes)schema.SetAttributes), $"property [{schema.Name}] attributes [{info.SetMethod?.Attributes}] do not match [{schema.SetAttributes}]");
            }

            Console.WriteLine($"\t✓ property [{schema.Name}] attributes match: {schema.SetAttributes}");
        }

        [TestMethod()]
        [DynamicData(nameof(PropertyValueList))]
        public virtual void PropertyValue_ShouldMatch_WhenSet(PropertySchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
                return;
            }

            if (!schema.CanTestValue)
            {
                Console.WriteLine($"\t✓ Property is flagged to not test values. Skipping Test");
                Assert.Inconclusive();
                return;
            }

            Type t = typeof(TModel);
            string name = schema.Name;
            var ctor = t.GetConstructor(new Type[] { });
            Assert.IsNotNull(ctor, "Constructor is null. Unable to locate default constructor.");

            TModel sut = (TModel)ctor.Invoke(null);
            object origVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);
            object newValue = origVal;
            object prevVal = origVal;

            for (var i = 0; i < 5; i++) // Loop through 5 iterations of tests
            {
                // Random Value should not equal original Value. Try 3 times
                var n = 0;
                do
                {
                    prevVal = newValue;
                    newValue = DataUtility.GenerateRandom(Nullable.GetUnderlyingType(schema.Type) != null ? Nullable.GetUnderlyingType(schema.Type) : schema.Type);
                } while ((++n < 3) && ((prevVal?.Equals(newValue) ?? false) || (origVal?.Equals(newValue) ?? false)));

                if ((origVal?.Equals(newValue) ?? false) || (prevVal?.Equals(newValue) ?? false) || newValue is null)
                {
                    Console.WriteLine($"Unable to generate random value for {schema.Type}");
                    if (schema.Type.IsValueType)
                        Assert.Inconclusive();
                    
                    break;
                }

                // Values should be different
                Console.WriteLine($"Previous Value: {prevVal ?? "<NULL>"}, New Value: {newValue}");

                //Assert.AreNotEqual(origVal, newValue, "Original value should not match random value");
                Assert.AreNotEqual(prevVal, newValue, "Previous value should not match random value");

                // Set the property
                AssertSetPropertyValueByName(sut, name, newValue);

                // Retrieve the property value
                prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                // Do the values match?
                Assert.AreEqual(prevVal, newValue);
            }

            // Try default parameter-less Constructor
            if (!schema.Type.IsValueType)
            {
                prevVal = newValue;
                ctor = schema.Type.GetConstructor(new Type[] { });
                if (ctor != null)
                {
                    newValue = ctor.Invoke(null);
                    Console.WriteLine($"Previous Value: <{prevVal ?? "NULL"}>, New Value: <{newValue ?? "NULL"}>");

                    // Set the property
                    AssertSetPropertyValueByName(sut, name, newValue);

                    // Retrieve the property value
                    prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                    // Do the values match?
                    Assert.AreEqual(prevVal, newValue);
                }

            }

            // Test Null
            if (prevVal == null)
            {
                Assert.Inconclusive();
            }
            else
            {
                if (Nullable.GetUnderlyingType(schema.Type) != null || !schema.Type.IsValueType)
                {
                    if (Nullable.GetUnderlyingType(schema.Type) != null || schema.Type == typeof(string))
                        Console.WriteLine($"Previous Value: {prevVal ?? "<NULL>"}, New Value: <NULL>");
                    else
                        Console.WriteLine($"Previous Value: <{prevVal ?? "NULL"}>, New Value: <NULL>");

                    // Set the property
                    AssertSetPropertyValueByName(sut, name, null);

                    // Retrieve the property value
                    prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                    // Do the values match?
                    Assert.AreEqual(prevVal, null);
                }
            }
        }

        #endregion

    }
}
