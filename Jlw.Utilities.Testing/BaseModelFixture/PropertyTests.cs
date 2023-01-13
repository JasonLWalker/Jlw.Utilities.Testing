using System;
using System.Collections.Generic;
using System.Globalization;
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
        protected static IEnumerable<PropertySchema> _propertySchema => modelSchema.PropertySchemaList;//new List<PropertySchema>() { null };

        public static IEnumerable<object[]> PropertySchemaList => _propertySchema.Select(o => new object[] { o });

        public static IEnumerable<object[]> PropertyValueList
        {
            get
            {
                var val = _propertySchema.Where(o => o?.SetAttributes != null && o?.GetAttributes != null).Select(o => new object[] { o });

                return !val.Any() ? new List<object[]>(){new object[]{null}} :  val;
            }
        }

        public static IEnumerable<object[]> ValueTypePropertyValueList
        {
            get
            {
                var val = _propertySchema.Where(o => o?.SetAttributes != null && o?.GetAttributes != null && o.Type.IsValueType).Select(o => new object[] { o });

                return !val.Any() ? new List<object[]>() { new object[] { null } } : val;
            }
        }

        protected static bool IsPropertyListEmpty => _propertySchema?.Count(o => o != null) < 1;

        #region Property Tests
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        public virtual void Property_Count_Should_Match(AccessModifiers accessModifiers, bool flattenHierarchy = true)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (_propertySchema.Count(o => o != null) < 1) Console.WriteLine($"\t✓\tNo property schema added. Skipping Test");
            if (_propertySchema.Count(o => o != null) < 1) Assert.Inconclusive();
            

            BindingFlags flags = flattenHierarchy ? BindingFlags.FlattenHierarchy : default;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;


            var t = typeof(TModel);
            var aInfo = t.GetProperties(flags);

            Assert.IsNotNull(aInfo, $"✗\tUnable to retrieve PropertyInfo for {DataUtility.GetTypeName(t)} with BindingFlags: {flags}");
            Console.WriteLine($"\t✓\tPropertyInfo retrieved");
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
            Console.WriteLine($"\t\tProperties Retrieved:\n{sProps}");

            Assert.AreEqual(nCount, nPropCount, $"✗\tNumber of properties is incorrect. Should be {nCount} for BindingFlags: {flags}");
            Console.WriteLine($"\t✓\tNumber of properties is {nCount} for BindingFlags: {flags}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Should_Exist(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            var t = typeof(TModel);
            var info = AssertPropertyExists(schema.Name);
            Console.WriteLine($"\t✓ property [{schema.Name}] exists with PropertyType.Attributes: {info.PropertyType.Attributes}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Binding_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            var t = typeof(TModel);
            var info = AssertGetPropertyInfoByName(schema.Name, schema.BindingFlags);
            Console.WriteLine($"\t✓ property [{schema.Name}] retrieved with BindingFlags: {schema.BindingFlags}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Type_Is_Assignable(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            var t = typeof(TModel);
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);
            
            Assert.IsTrue(schema.Type.IsAssignableFrom(info.PropertyType), $"[{DataUtility.GetTypeName(schema.Type)}] is not assignable from [{DataUtility.GetTypeName(info.PropertyType)}]");
            if (schema.Type == info.PropertyType)
                Console.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) matches property with the signature: \n\t\t\t{schema}");
            else
                Console.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) is implemented by property {schema}");
        }


        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Access_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);


            // Assert
            Assert.AreEqual(schema.Access, GetPropertyAccess(info?.GetMethod?.Attributes ?? default, info?.SetMethod?.Attributes ?? default), $"Access modifiers do not match for property with the signature:\n\t\t\t{schema}");

            Console.WriteLine($"\t✓ property access modifiers match: {schema}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Get_Accessor_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);
            
            // Assert
            if (schema.GetAttributes == null || info.GetMethod == null)
            {
                Assert.AreEqual(DataUtility.ParseNullableLong(schema.GetAttributes), DataUtility.ParseNullableLong(info.GetMethod?.Attributes), schema.GetAttributes == null ? "Get method should not exist" : "Get method should exist");
                Console.WriteLine($"\t✓ property get method should not exist");
            }
            else
            {

                //Assert.IsTrue(info.GetMethod.Attributes.HasFlag((MethodAttributes) schema.GetAttributes), $"property [{schema.Name}] attributes [{info.GetMethod.Attributes}] do not match [{schema.GetAttributes}]");
                Assert.IsTrue((info.GetMethod.Attributes & AccessScope.AccessMask) == (MethodAttributes)schema.GetAttributes, $"property [{schema.Name}] attributes [{info.GetMethod.Attributes}] do not match [{schema.GetAttributes}]");
            }

            Console.WriteLine($"\t✓ property [{schema.Name}] attributes match: {schema.GetAttributes}");
        }

        [TestMethod]
        [DynamicData(nameof(PropertySchemaList))]
        public virtual void Property_Set_Accessor_Should_Match(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();


            // Arrange
            var info = GetPropertyInfoByName(schema.Name, schema.BindingFlags);

            // Assert
            if (schema.SetAttributes == null || info?.SetMethod == null)
            {
                Assert.AreEqual(DataUtility.ParseNullableLong(schema.SetAttributes), DataUtility.ParseNullableLong(info?.SetMethod?.Attributes), schema.SetAttributes == null ? "set accessor should not exist" : "set accessor should exist");
                Console.WriteLine($"\t✓ property set accessor should not exist");
            }
            else
            {
                //Assert.IsTrue(info.SetMethod.Attributes.HasFlag((MethodAttributes)schema.SetAttributes), $"property [{schema.Name}] attributes [{info.SetMethod?.Attributes}] do not match [{schema.SetAttributes}]");
                Assert.IsTrue((info.SetMethod.Attributes & AccessScope.AccessMask) == (MethodAttributes)schema.SetAttributes, $"property [{schema.Name}] attributes [{info.SetMethod?.Attributes}] do not match [{schema.SetAttributes}]");
            }

            Console.WriteLine($"\t✓ property [{schema.Name}] attributes match: {schema.SetAttributes}");
        }

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

            // Output count to console for information purposes
            Console.WriteLine($"\t✓\tNumber of implemented {GetAccessString(access)} properties is {implementedKeys.Length}");
            Console.WriteLine($"\t\tImplemented properties:");
            OutputImplementedKeys(implementedKeys, expectedKeys);
            Console.WriteLine($"\t\tExpected properties:");
            OutputExpectedKeys(implementedKeys, expectedKeys);

            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (IsPropertyListEmpty) Console.WriteLine($"\t-\tNo property schema added. Skipping Test");
            if (IsPropertyListEmpty) Assert.Inconclusive();

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
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            // If not flagged, then skip the test. (2 if statements are used to pass code coverage)
            if (!schema.CanTestValue) Console.WriteLine($"\t✓ Property is flagged to not test values. Skipping Test");
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



        }
        */

        [TestMethod]
        [DynamicData(nameof(PropertyValueList))]
        public virtual void Property_Value_Should_Match_When_Set(PropertySchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();


            // If not flagged, then skip the test. (2 if statements are used to pass code coverage)
            if (!schema.CanTestValue) Console.WriteLine($"\t✓ Property is flagged to not test values. Skipping Test");
            if (!schema.CanTestValue) //Assert.Inconclusive();
                return;

            Type t = typeof(TModel);
            string name = schema.Name;
            var ctor = t.GetConstructor(new Type[] { });
            Assert.IsNotNull(ctor, "Constructor is null. Unable to locate default constructor.");

            TModel sut = (TModel)ctor.Invoke(null);
            object origVal = GetPropertyValueByName(sut, name, schema.BindingFlags);

            object newValue = origVal;
            object prevVal = origVal;

            // Retrieve a snapshot of all the member values before changes occur
            var origSnapshot = new InstanceMemberSnapshot(sut);

            for (var i = 0; i < 5; i++) // Loop through 5 iterations of tests
            {
                // Random Value should not equal original Value. Try 3 times
                var n = 0;
                do
                {
                    prevVal = newValue;
                    newValue = DataUtility.GenerateRandom(Nullable.GetUnderlyingType(schema.Type) ?? schema.Type);
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

                var snapshot = new InstanceMemberSnapshot(sut);
                snapshot.AssertAreSame(origSnapshot, name);
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

                    var snapshot = new InstanceMemberSnapshot(sut);
                    snapshot.AssertAreSame(origSnapshot, name);
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

                    var snapshot = new InstanceMemberSnapshot(sut);
                    snapshot.AssertAreSame(origSnapshot, name);
                }
            }
            
        }

        #endregion

        #region Helper Methods

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

        #endregion
    }
}
