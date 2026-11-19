using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Jlw.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel, TSchema>
    {
        /// <summary>
        /// Gets the property schemas defined on the model schema.
        /// </summary>
        protected static IEnumerable<PropertySchema> _propertySchema => modelSchema.PropertySchemaList;//new List<PropertySchema>() { null };

        /// <summary>
        /// Static accessor to retrieve property schemas via DynamicDataAttribute argument pattern.
        /// </summary>
        public static IEnumerable<object[]> PropertySchemaList => _propertySchema.Select(o => new object[] { o });

        /// <summary>
        /// Static accessor to retrieve property schemas that have both get and set accessors defined, for value tests.
        /// </summary>
        /// <remarks>
        /// Returns a single <c>null</c> entry when no such schema exists, so the data-driven test runs and can be skipped.
        /// </remarks>
        public static IEnumerable<object[]> PropertyValueList
        {
            get
            {
                var val = _propertySchema.Where(o => o?.SetAttributes != null && o?.GetAttributes != null).Select(o => new object[] { o });

                return !val.Any() ? new List<object[]>(){new object[]{null}} :  val;
            }
        }

        /// <summary>
        /// Static accessor to retrieve value-type property schemas that have both get and set accessors defined.
        /// </summary>
        /// <remarks>
        /// Returns a single <c>null</c> entry when no such schema exists, so the data-driven test runs and can be skipped.
        /// </remarks>
        public static IEnumerable<object[]> ValueTypePropertyValueList
        {
            get
            {
                var val = _propertySchema.Where(o => o?.SetAttributes != null && o?.GetAttributes != null && o.Type.IsValueType).Select(o => new object[] { o });

                return !val.Any() ? new List<object[]>() { new object[] { null } } : val;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property schema is missing or contains no non-null entries.
        /// </summary>
        protected static bool IsPropertyListEmpty => _propertySchema?.Count(o => o != null) < 1;

        
        #region Property Tests
        /// <summary>
        /// Asserts that the number of properties on <typeparamref name="TModel"/> matches the number of schema entries
        /// for the given access modifiers and binding flags.
        /// </summary>
        /// <remarks>
        /// If no property schema is defined, the test passes when no matching properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="accessModifiers">The access level and static state to filter properties by.</param>
        /// <param name="flattenHierarchy">Whether to include inherited properties in the lookup.</param>
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        public virtual void Property_Count_Should_Match(AccessModifiers accessModifiers, bool flattenHierarchy = true)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (_propertySchema.Count(o => o != null) < 1)
            {
                TestContext?.WriteLine($"\t✓\tNo property schema added. Skipping Test");
                var props = GetImplementedPropertyKeys(accessModifiers);
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No {accessModifiers} properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList(accessModifiers);
                Assert.Inconclusive();
            }
            

            BindingFlags flags = flattenHierarchy ? BindingFlags.FlattenHierarchy : default;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;


            var t = typeof(TModel);
            var aInfo = t.GetProperties(flags);

            Assert.IsNotNull(aInfo, $"✗\tUnable to retrieve PropertyInfo for {DataUtility.GetTypeName(t)} with BindingFlags: {flags}");
            TestContext?.WriteLine($"\t✓\tPropertyInfo retrieved");
            BindingFlags mask = ~(BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            int nCount = _propertySchema.Count(o => o != null && ((o.BindingFlags & mask) == (flags & mask)) && o.Access.Equals(accessModifiers));
            int nPropCount = 0;
            string sProps = "";
            foreach (var info in aInfo)
            {
                if (accessModifiers.Equals(GetPropertyAccess(info.GetMethod?.Attributes ?? default, info.SetMethod?.Attributes ?? default)))
                {
                    string access = GetAccessString(info);
                    sProps += $"\t\t{(_propertySchema.Any(o => o.Name.Equals(info.Name)) ? "✓" : "✗")}\t{access} {DataUtility.GetTypeName(info.PropertyType)} {info.Name}\n";
                    nPropCount++;
                }
            }
            TestContext?.WriteLine($"\t\tProperties Retrieved:\n{sProps}");

            Assert.AreEqual(nCount, nPropCount, $"✗\tNumber of properties is incorrect. Should be {nCount} for BindingFlags: {flags}");
            TestContext?.WriteLine($"\t✓\tNumber of properties is {nCount} for BindingFlags: {flags}");
        }

        /// <summary>
        /// Asserts that the property described by the schema exists on <typeparamref name="TModel"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The property schema to look up by name.</param>
        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Should_Exist(PropertySchema schema)
        {
            // If schema list is empty, then skip the test.
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();

                Assert.Inconclusive();
            }

            var t = typeof(TModel);
            var info = AssertPropertyExists(schema.Name);
            TestContext?.WriteLine($"\t✓ property [{schema.Name}] exists with PropertyType.Attributes: {info.PropertyType.Attributes}");
        }

        /// <summary>
        /// Asserts that the property described by the schema can be retrieved using the binding flags of that schema.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The property schema whose name and binding flags are used for the lookup.</param>
        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Binding_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. 
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();
                Assert.Inconclusive();
            }
            

            var t = typeof(TModel);
            var info = AssertGetPropertyInfoByName(schema.Name, schema.BindingFlags);
            TestContext?.WriteLine($"\t✓ property [{schema.Name}] retrieved with BindingFlags: {schema.BindingFlags}");
        }

        /// <summary>
        /// Asserts that the property type of the schema is assignable from the actual type of the property on <typeparamref name="TModel"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The property schema whose type is compared against the actual property type.</param>
        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Type_Is_Assignable(PropertySchema schema)
        {
            // If schema list is empty, then skip the test.
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();
                Assert.Inconclusive();
            }
            
            var t = typeof(TModel);
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);
            
            Assert.IsTrue(schema.Type.IsAssignableFrom(info.PropertyType), $"[{DataUtility.GetTypeName(schema.Type)}] is not assignable from [{DataUtility.GetTypeName(info.PropertyType)}]");
            if (schema.Type == info.PropertyType)
                TestContext?.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) matches property with the signature: \n\t\t\t{schema}");
            else
                TestContext?.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) is implemented by property {schema}");
        }


        /// <summary>
        /// Asserts that the access level of the property described by the schema matches the access level of the actual property accessors.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The property schema whose access level is compared against the actual property.</param>
        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Access_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");

                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();
                Assert.Inconclusive();
            }
            


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);


            // Assert
            Assert.AreEqual(schema.Access, GetPropertyAccess(info?.GetMethod?.Attributes ?? default, info?.SetMethod?.Attributes ?? default), $"Access modifiers do not match for property with the signature:\n\t\t\t{schema}");

            TestContext?.WriteLine($"\t✓ property access modifiers match: {schema}");
        }

        /// <summary>
        /// Asserts that the get accessor of the property described by the schema matches the expected attributes in the schema.
        /// </summary>
        /// <remarks>
        /// If the schema or the property has no get accessor, the test asserts that both agree on its absence.
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The property schema whose get accessor attributes are compared against the actual accessor.</param>
        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Get_Accessor_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test.
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();
                Assert.Inconclusive();
            }
            


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);
            
            // Assert
            if (schema.GetAttributes == null || info.GetMethod == null)
            {
                Assert.AreEqual(DataUtility.ParseNullableLong(schema.GetAttributes), DataUtility.ParseNullableLong(info.GetMethod?.Attributes), schema.GetAttributes == null ? "Get method should not exist" : "Get method should exist");
                TestContext?.WriteLine($"\t✓ property get method should not exist");
            }
            else
            {

                //Assert.IsTrue(info.GetMethod.Attributes.HasFlag((MethodAttributes) schema.GetAttributes), $"property [{schema.Name}] attributes [{info.GetMethod.Attributes}] do not match [{schema.GetAttributes}]");
                Assert.IsTrue((info.GetMethod.Attributes & AccessScope.AccessMask) == (MethodAttributes)schema.GetAttributes, $"property [{schema.Name}] attributes [{info.GetMethod.Attributes}] do not match [{schema.GetAttributes}]");
            }

            TestContext?.WriteLine($"\t✓ property [{schema.Name}] attributes match: {schema.GetAttributes}");
        }

        /// <summary>
        /// Asserts that the set accessor of the property described by the schema matches the expected attributes in the schema.
        /// </summary>
        /// <remarks>
        /// If the schema or the property has no set accessor, the test asserts that both agree on its absence.
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public properties exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The property schema whose set accessor attributes are compared against the actual accessor.</param>
        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Set_Accessor_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test.
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();
                Assert.Inconclusive();
            }
            

            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);

            // Assert
            if (schema.SetAttributes == null || info?.SetMethod == null)
            {
                Assert.AreEqual(DataUtility.ParseNullableLong(schema.SetAttributes), DataUtility.ParseNullableLong(info?.SetMethod?.Attributes), schema.SetAttributes == null ? "set accessor should not exist" : "set accessor should exist");
                TestContext?.WriteLine($"\t✓ property set accessor should not exist");
            }
            else
            {
                //Assert.IsTrue(info.SetMethod.Attributes.HasFlag((MethodAttributes)schema.SetAttributes), $"property [{schema.Name}] attributes [{info.SetMethod?.Attributes}] do not match [{schema.SetAttributes}]");
                Assert.IsTrue((info.SetMethod.Attributes & AccessScope.AccessMask) == (MethodAttributes)schema.SetAttributes, $"property [{schema.Name}] attributes [{info.SetMethod?.Attributes}] do not match [{schema.SetAttributes}]");
            }

            TestContext?.WriteLine($"\t✓ property [{schema.Name}] attributes match: {schema.SetAttributes}");
        }

        /// <summary>
        /// Asserts that the implemented property signatures for the given access level match the expected property signatures, and vice versa.
        /// </summary>
        /// <remarks>
        /// Signatures whose schema entry sets <c>CanTestSignature</c> to <c>false</c> are excluded from the comparison.
        /// The test is inconclusive when no property schema is defined and properties exist.
        /// </remarks>
        /// <param name="access">The access level and static state to filter signatures by.</param>
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        public virtual void Property_Signatures_Should_Match(AccessModifiers access)
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedPropertyKeys(access).ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedPropertyKeys(access).ToArray();
            // Declare variable to hold Dictionary of matched values
            var matches = new Dictionary<string, bool>();

            /*
            // Output count to console for information purposes
            TestContext?.WriteLine($"\t✓\tNumber of implemented {GetAccessString(access)} properties is {implementedKeys.Length}");
            TestContext?.WriteLine($"\t\tImplemented properties:");
            OutputImplementedKeys(implementedKeys, expectedKeys);
            TestContext?.WriteLine($"\t\tExpected properties:");
            OutputExpectedKeys(implementedKeys, expectedKeys);
            */
            
            
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (IsPropertyListEmpty)
            {
                TestContext?.WriteLine($"\t-\tNo property schema added. Skipping Test");
                var props = GetImplementedPropertyKeys(access);
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No {access} properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList(access);
                Assert.Inconclusive();
            }
            OutputPropertyCountAndList(access);


            var re = new Regex(@"\s+(\w+)\s+{");

            foreach (string sKey in implementedKeys)
            {
                var m = re.Match(sKey);
                var s = m.Groups[1].Value;
                bool bTest = (_propertySchema.FirstOrDefault(o => o.Name == s)?.CanTestSignature) ?? true; // Set to false if signature isn't to be tested.
                if (bTest)
                    matches[sKey] = expectedKeys.Contains(sKey);
            }
            foreach (string sKey in expectedKeys)
            {
                var m = re.Match(sKey);
                var s = m.Groups[1].Value;
                bool bTest = (_propertySchema.FirstOrDefault(o => o.Name == s)?.CanTestSignature) ?? true; // Set to false if signature isn't to be tested.
                if (bTest)
                    matches[sKey] = implementedKeys.Contains(sKey);
            }

            Assert.IsTrue(matches.All(o => o.Value == true), $"\n\t✗\tNot all implemented {GetAccessString(access)} properties match the expected {GetAccessString(access)} properties.");

        }

        /*
        [TestMethod]
        [DynamicData(nameof(ValueTypePropertyValueList))]
        public virtual void Property_Value_Should_Match_When_Set_For_Value_Type(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. 
            if (schema is null) TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            // If not flagged, then skip the test. (2 if statements are used to pass code coverage)
            if (!schema.CanTestValue) TestContext?.WriteLine($"\t✓ Property is flagged to not test values. Skipping Test");
            if (!schema.CanTestValue) //Assert.Inconclusive();
                return;
            string name = schema.Name;

            TModel sut = AssertGetObjectInstance<TModel>();
            object origVal = GetPropertyValueByName(sut, name, schema.BindingFlags);

            object newValue = origVal;
            object prevVal = origVal;

            for (var i = 0; i < 5; i++) // Loop through 5 iterations of tests
            {
                newValue = GetUniqueRandomValue(schema.Type, prevVal, origVal);


                if ((origVal?.Equals(newValue) ?? false) || (prevVal?.Equals(newValue) ?? false) || newValue is null)
                {
                    TestContext?.WriteLine($"Unable to generate random value for {schema.Type}");
                    if (schema.Type.IsValueType)
                        Assert.Inconclusive();

                    break;
                }

                // Values should be different
                TestContext?.WriteLine($"Previous Value: {prevVal ?? "<NULL>"}, New Value: {newValue}");

                //Assert.AreNotEqual(origVal, newValue, "Original value should not match random value");
                Assert.AreNotEqual(prevVal, newValue, "Previous value should not match random value");

                // Set the property
                AssertSetPropertyValueByName(sut, name, newValue);

                // Retrieve the property value
                prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                // Do the values match?
                Assert.AreEqual(prevVal, newValue);

            }



        }
        */

        /// <summary>
        /// Asserts that setting a property on a new <typeparamref name="TModel"/> instance stores the assigned value,
        /// and that setting it does not change any other member.
        /// </summary>
        /// <remarks>
        /// Runs five iterations with random values, then tests a default instance of the property type, then tests <c>null</c>
        /// for reference and nullable types. Properties flagged with <c>CanTestValue</c> set to <c>false</c> are skipped.
        /// </remarks>
        /// <param name="schema">The property schema to set and verify.</param>
        [TestMethod]
        [DynamicData(nameof(PropertyValueList))]
        public virtual void Property_Value_Should_Match_When_Set(PropertySchema schema)
        {
            // If schema list is empty, then skip the test.
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var props = GetImplementedPropertyKeys();
                if (!props.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public properties exist.");
                    Assert.AreEqual(0, 0);
                    return;
                }
                OutputPropertyCountAndList();
                Assert.Inconclusive();
            }

            // If not flagged, then skip the test. (2 if statements are used to pass code coverage)
            if (!schema.CanTestValue) TestContext?.WriteLine($"\t✓ {schema.Name} Property is flagged to not test values. Skipping Test");
            if (!schema.CanTestValue) //Assert.Inconclusive();
                return;

            Type t = typeof(TModel);
            string name = schema.Name;
            List<string> excludeList = new List<string>() { name };

            foreach (var prop in _propertySchema)
            {
                if (!prop.CanTestValue) excludeList.Add(prop.Name);
            }
            
            var ctor = t.GetConstructor(new Type[] { });
            Assert.IsNotNull(ctor, "Constructor is null. Unable to locate default constructor.");

            TModel sut = (TModel)ctor.Invoke(null);
            object origVal = GetPropertyValueByName(sut, name, schema.BindingFlags);

            object newValue = origVal;
            object prevVal = origVal;

            // Retrieve a snapshot of all the member values before changes occur
            var origSnapshot = new InstanceMemberSnapshot(sut);

            TestContext?.WriteLine($"=== Test changing value with 5 random iterations ===");
            for (var i = 0; i < 5; i++) // Loop through 5 iterations of tests
            {
                // Random Value should not equal original Value. Try 3 times
                var n = 0;
                do
                {
                    prevVal = newValue;
                    newValue = DataUtility.GenerateRandom(Nullable.GetUnderlyingType(schema.Type) ?? schema.Type);
                } while ((++n < 3) && ((prevVal?.Equals(newValue) ?? false) || (origVal?.Equals(newValue) ?? false)));


                // Values should be different
                TestContext?.WriteLine($"Property: {schema.Name}, Previous Value: {prevVal ?? "<NULL>"}, New Value: {newValue}");
                if ((Nullable.GetUnderlyingType(schema.Type) ?? schema.Type) != typeof(bool))
                {

                    if ((origVal?.Equals(newValue) ?? false) || (prevVal?.Equals(newValue) ?? false) || newValue is null)
                    {
                        TestContext?.WriteLine($"{schema.Name} - Unable to generate random value for {schema.Type}");
                        if (schema.Type.IsValueType)
                            Assert.Inconclusive();

                        break;
                    }


                    //Assert.AreNotEqual(origVal, newValue, "Original value should not match random value");
                    Assert.AreNotEqual(prevVal, newValue, "Previous value should not match random value");
                }

                // Set the property
                AssertSetPropertyValueByName(sut, name, newValue);

                // Retrieve the property value
                prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                // Do the values match?
                Assert.AreEqual(prevVal, newValue);

                var snapshot = new InstanceMemberSnapshot(sut);
                snapshot.AssertAreSame(origSnapshot, excludeList);
            }

            // Try default parameter-less Constructor
            if (!schema.Type.IsValueType)
            {
                prevVal = newValue;
                ctor = schema.Type.GetConstructor(new Type[] { });
                if (ctor != null)
                {
                    TestContext?.WriteLine($"=== Test setting value with Parameter-less constructor ===");
                    newValue = ctor.Invoke(null);
                    TestContext?.WriteLine($"Previous Value: <{prevVal ?? "NULL"}>, New Value: <{newValue ?? "NULL"}>");

                    // Set the property
                    AssertSetPropertyValueByName(sut, name, newValue);

                    // Retrieve the property value
                    prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                    // Do the values match?
                    Assert.AreEqual(prevVal, newValue);

                    var snapshot = new InstanceMemberSnapshot(sut);
                    snapshot.AssertAreSame(origSnapshot, excludeList);
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
                    TestContext?.WriteLine($"=== Test setting value to <NULL> ===");
                    if (Nullable.GetUnderlyingType(schema.Type) != null || schema.Type == typeof(string))
                        TestContext.WriteLine($"Previous Value: {prevVal ?? "<NULL>"}, New Value: <NULL>");
                    else
                        TestContext.WriteLine($"Previous Value: <{prevVal ?? "NULL"}>, New Value: <NULL>");

                    // Set the property
                    AssertSetPropertyValueByName(sut, name, null);

                    // Retrieve the property value
                    prevVal = AssertGetPropertyValueByName(sut, name, schema.BindingFlags);

                    // Do the values match?
                    Assert.AreEqual(prevVal, null);

                    var snapshot = new InstanceMemberSnapshot(sut);
                    snapshot.AssertAreSame(origSnapshot, excludeList);
                }
            }
            
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Generates a random value of the given type that differs from both the previous and original values, retrying up to three times.
        /// </summary>
        /// <param name="t">The type of value to generate.</param>
        /// <param name="prevVal">The previously generated value.</param>
        /// <param name="origVal">The original value of the property.</param>
        /// <returns>A random value of type <paramref name="t"/>. It may still equal one of the inputs if no distinct value was produced.</returns>
        protected object GetUniqueRandomValue(Type t, object prevVal, object origVal)
        {
            object newValue = origVal;
            // Random Value should not equal original Value. Try 3 times
            var n = 0;
            do
            {
                prevVal = newValue;
                newValue = DataUtility.GenerateRandom(Nullable.GetUnderlyingType(t) ?? t);
            } while ((++n < 3) && ((prevVal?.Equals(newValue) ?? false) || (origVal?.Equals(newValue) ?? false)));

            return newValue;
        }

        /// <summary>
        /// Builds the distinct string representations of the expected properties in the schema that match the given access level.
        /// </summary>
        /// <param name="access">The access level to filter the expected properties by.</param>
        /// <returns>Distinct property schema strings.</returns>
        protected IEnumerable<string> GetExpectedPropertyKeys(AccessModifiers access = AccessModifiers.Public)
        {
            var aReturn = new List<string>();
            var schemaList = _propertySchema?.Where(o => o?.Access == access).ToArray();
            if (schemaList?.Length > 0)
            {
                foreach (var schema in schemaList)
                {
                    aReturn.Add(schema.ToString());
                }
            }

            return aReturn.Distinct();
        }

        /// <summary>
        /// Builds the distinct string representations of the properties implemented on <typeparamref name="TModel"/>
        /// that match the given access level.
        /// </summary>
        /// <param name="access">The access level to filter the implemented properties by.</param>
        /// <returns>Distinct property signatures.</returns>
        protected IEnumerable<string> GetImplementedPropertyKeys(AccessModifiers access = AccessModifiers.Public)
        {
            var aReturn = new List<string>();
            var t = typeof(TModel);
            PropertyInfo[] info = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.CreateInstance | BindingFlags.FlattenHierarchy);
            foreach (var i in info.Where(o => (GetPropertyAccess(o.GetMethod?.Attributes ?? default, o.SetMethod?.Attributes ?? default) & AccessModifiers.AccessMask) == (AccessModifiers)access))
            {
                PropertySchema ps = new PropertySchema(i.Name, i.PropertyType, default,
                     (AccessModifiers?)(i.GetMethod?.Attributes) ?? null,
                    (AccessModifiers?)(i.SetMethod?.Attributes) ?? null);
                aReturn.Add(ps.ToString());
            }

            return aReturn.Distinct();
        }

        /// <summary>
        /// Writes the implemented and expected property counts and keys for the given access level to the test output.
        /// </summary>
        /// <param name="accessModifiers">The access level to report on.</param>
        protected void OutputPropertyCountAndList(AccessModifiers accessModifiers = AccessModifiers.Public)
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedPropertyKeys(accessModifiers).ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedPropertyKeys(accessModifiers).ToArray();
            TestContext?.WriteLine($"\t✓\tNumber of implemented {accessModifiers} properties is {implementedKeys.Length}");
            OutputImplementedKeys(implementedKeys, expectedKeys);
        }



        #endregion
    }
}
