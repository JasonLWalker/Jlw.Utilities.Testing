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
        protected static IEnumerable<BaseModelUtility<TModel>.MemberSchema> _fieldSchema => modelSchema.FieldList;

        public static IEnumerable<object[]> FieldList => _fieldSchema.Select(o => new object[] { o });

        protected static bool IsFieldListEmpty => _fieldSchema?.Count(o => o != null) < 1;

        #region Field Tests
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        public virtual void Field_Count_Should_Match(AccessModifiers accessModifiers, bool flattenHierarchy = true)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (_fieldSchema.Count(o => o != null) < 1) Console.WriteLine($"\t✓ No field schema added. Skipping Test");
            if (_fieldSchema.Count(o => o != null) < 1) Assert.Inconclusive();

            BindingFlags flags = flattenHierarchy ? BindingFlags.FlattenHierarchy : default;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;

            var t = typeof(TModel);
            var aInfo = t.GetFields(flags);
            BindingFlags mask = ~(BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            Assert.IsNotNull(aInfo, $"Unable to retrieve FieldInfo for {DataUtility.GetTypeName(t)} with BindingFlags: {flags}");
            Console.WriteLine($"\t✓ FieldInfo retrieved");

            int nCount = _fieldSchema.Count(o => o != null && ((o.BindingFlags & mask) == (flags & mask)) && o.Access.Equals(accessModifiers));
            int nMemCount = 0;
            string sProps = "";
            foreach (var info in aInfo)
            {
                if (accessModifiers.Equals((AccessModifiers)info.Attributes))
                {
                    string access = GetAccessString((AccessModifiers)info.Attributes);
                    sProps += $"\t\t{(_fieldSchema.Any(o=>o.Name.Equals(info.Name)) ? "✓" : "✗")} {access} {DataUtility.GetTypeName(info.FieldType)} {info.Name}\n";
                    nMemCount++;
                }
            }
            Console.WriteLine($"\t   Fields Retrieved:\n{sProps}");

            Assert.AreEqual(nCount, nMemCount, $"Number of fields is incorrect. Should be {nCount} for BindingFlags: {flags}, and AccessModifiers: {accessModifiers}");
            Console.WriteLine($"\t✓ Number of fields is {nCount} for BindingFlags: {flags}, and AccessModifiers: {accessModifiers}");
        }

        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Should_Exist(MemberSchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            var t = typeof(TModel);

            var info = AssertFieldExists(schema.Name);
            Console.WriteLine($"\t✓ property [{schema.Name}] exists with PropertyType.Attributes: {info.FieldType.Attributes}");
        }

        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Type_Is_Assignable(MemberSchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();

            var t = typeof(TModel);
            var info = GetFieldInfoByName(schema.Name, schema.BindingFlags);

            Assert.IsTrue(schema.Type.IsAssignableFrom(info.FieldType), $"[{DataUtility.GetTypeName(schema.Type)}] is not assignable from [{DataUtility.GetTypeName(info.FieldType)}]");
            if (schema.Type == info.FieldType)
                Console.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) matches field with the signature: \n\t\t\t{schema}");
            else
                Console.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) is implemented by field {schema}");

        }

        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Access_Should_Match(MemberSchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null) Assert.Inconclusive();
            

            var info = GetFieldInfoByName(schema.Name, schema.BindingFlags);


            Assert.AreEqual((FieldAttributes)schema.Access, (info?.Attributes ?? default), $"Access modifiers do not match for field with the signature:\n\t\t\t{schema}");

            Console.WriteLine($"\t✓ field access modifiers match: {schema}");
        }

        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        public virtual void Field_Signatures_Should_Match(AccessModifiers access)
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedFieldKeys(access).ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedFieldKeys(access).ToArray();
            // Declare variable to hold Dictionary of matched values
            var matches = new Dictionary<string, bool>();

            // Output count to console for information purposes
            Console.WriteLine($"\t✓\tNumber of implemented {GetAccessString(access)} fields is {implementedKeys.Length}");
            Console.WriteLine($"\t\tImplemented fields:");
            OutputImplementedKeys(implementedKeys, expectedKeys);
            Console.WriteLine($"\t\tExpected fields:");
            OutputExpectedKeys(implementedKeys, expectedKeys);

            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (IsFieldListEmpty) Console.WriteLine($"\t-\tNo field schema added. Skipping Test");
            if (IsFieldListEmpty) Assert.Inconclusive();

            foreach (string sKey in implementedKeys)
            {
                var s = sKey.Split(' ').Last();
                bool bTest = (_fieldSchema.FirstOrDefault(o => o.Name == s)?.CanTestSignature) ?? true; // Set to false if signature isn't to be tested.
                if (bTest)
                    matches[sKey] = expectedKeys.Contains(sKey);
            }
            foreach (string sKey in expectedKeys)
            {
                var s = sKey.Split(' ').Last();
                bool bTest = (_fieldSchema.FirstOrDefault(o => o.Name == s)?.CanTestSignature) ?? true; // Set to false if signature isn't to be tested.
                if (bTest)
                    matches[sKey] = implementedKeys.Contains(sKey);
            }

            Assert.IsTrue(matches.All(o => o.Value == true), $"\n\t✗\tNot all implemented {GetAccessString(access)} fields match the expected {GetAccessString(access)} fields.");

        }



        #endregion

        #region Helper Methods
        protected IEnumerable<string> GetExpectedFieldKeys(AccessModifiers access = AccessModifiers.Public)
        {
            var aReturn = new List<string>();
            var schemaList = _fieldSchema?.Where(o => o?.Access == access).ToArray();
            if (schemaList?.Length > 0)
            {
                foreach (var schema in schemaList)
                {
                    aReturn.Add(schema.ToString());
                }
            }

            return aReturn.Distinct();
        }

        protected IEnumerable<string> GetImplementedFieldKeys(AccessModifiers access = AccessModifiers.Public)
        {
            var aReturn = new List<string>();
            var t = typeof(TModel);
            FieldInfo[] info = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.CreateInstance | BindingFlags.FlattenHierarchy);
            foreach (var i in info.Where(o => ((AccessModifiers)o.Attributes & AccessModifiers.AccessMask) == (AccessModifiers)access))
            {
                aReturn.Add($"{GetAccessString((AccessModifiers)i.Attributes)} {DataUtility.GetTypeName(i.FieldType)} {i.Name}");
            }

            return aReturn.Distinct();
        }

        #endregion
    }
}
