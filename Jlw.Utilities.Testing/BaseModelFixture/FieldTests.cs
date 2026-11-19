using Jlw.Utilities.Data;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel, TSchema>
    {
        /// <summary>
        /// Gets the field schemas defined on the model schema.
        /// </summary>
        protected static IEnumerable<BaseModelUtility<TModel>.MemberSchema> _fieldSchema => modelSchema.FieldList;

        /// <summary>
        /// Static accessor to retrieve field schemas via DynamicDataAttribute argument pattern.
        /// </summary>
        public static IEnumerable<object[]> FieldList => _fieldSchema.Select(o => new object[] { o });

        /// <summary>
        /// Gets a value indicating whether the field schema is missing or contains no non-null entries.
        /// </summary>
        protected static bool IsFieldListEmpty => _fieldSchema?.Count(o => o != null) < 1;

        #region Field Tests
        /// <summary>
        /// Asserts that the number of fields on <typeparamref name="TModel"/> matches the number of schema entries
        /// for the given access modifiers and binding flags.
        /// </summary>
        /// <remarks>
        /// If no field schema is defined, the test passes when no matching fields exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="accessModifiers">The access level and static state to filter fields by.</param>
        /// <param name="flattenHierarchy">Whether to include inherited fields in the lookup.</param>
        [TestMethod]
        [DataRow(Public)]
        [DataRow(Public | Static)]
        public virtual void Field_Count_Should_Match(AccessModifiers accessModifiers, bool flattenHierarchy = true)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (_fieldSchema.Count(o => o != null) < 1) TestContext?.WriteLine($"\t✓ No field schema added. Skipping Test");
            //if (_fieldSchema.Count(o => o != null) < 1) Assert.Inconclusive();

            BindingFlags flags = flattenHierarchy ? BindingFlags.FlattenHierarchy : default;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;


            if (_fieldSchema.Count(o => o != null) < 1)
            {
                var fields = typeof(TModel).GetFields(flags);
                if (fields.Length < 1)
                {
                    TestContext?.WriteLine($"\t✓ No {accessModifiers} fields exist.");
#pragma warning disable MSTEST0032
                    Assert.AreEqual(0, 0);
#pragma warning restore MSTEST0032
                    return;
                }

                OutputFieldCountAndList(accessModifiers);
                Assert.Inconclusive();
            }


            var t = typeof(TModel);
            var aInfo = t.GetFields(flags);
            BindingFlags mask = ~(BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            Assert.IsNotNull(aInfo, $"Unable to retrieve FieldInfo for {DataUtility.GetTypeName(t)} with BindingFlags: {flags}");
            TestContext?.WriteLine($"\t✓ FieldInfo retrieved");

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
            TestContext?.WriteLine($"\t   Fields Retrieved:\n{sProps}");

            Assert.AreEqual(nCount, nMemCount, $"Number of fields is incorrect. Should be {nCount} for BindingFlags: {flags}, and AccessModifiers: {accessModifiers}");
            TestContext?.WriteLine($"\t✓ Number of fields is {nCount} for BindingFlags: {flags}, and AccessModifiers: {accessModifiers}");
        }

        /// <summary>
        /// Asserts that the field described by the schema exists on <typeparamref name="TModel"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public fields exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The field schema to look up by name.</param>
        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Should_Exist(MemberSchema schema)
        {
            // If schema is null, then skip the test. 
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var fields = GetImplementedFieldKeys();//typeof(TModel).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (!fields.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public fields exist.");
#pragma warning disable MSTEST0032
                    Assert.AreEqual(0, 0);
#pragma warning restore MSTEST0032
                    return;
                }
                OutputFieldCountAndList();
                Assert.Inconclusive();
            }


            var t = typeof(TModel);

            var info = AssertFieldExists(schema.Name);
            TestContext?.WriteLine($"\t✓ property [{schema.Name}] exists with PropertyType.Attributes: {info.FieldType.Attributes}");
        }

        /// <summary>
        /// Asserts that the schema's field type is assignable from the actual type of the field on <typeparamref name="TModel"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public fields exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The field schema whose type is compared against the actual field type.</param>
        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Type_Is_Assignable(MemberSchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null)
            {
                TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
                var fields = GetImplementedFieldKeys();//typeof(TModel).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (!fields.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public fields exist.");
#pragma warning disable MSTEST0032
                    Assert.AreEqual(0, 0);
#pragma warning restore MSTEST0032
                    return;
                }
                OutputFieldCountAndList();
                Assert.Inconclusive();
            }

            var t = typeof(TModel);
            var info = GetFieldInfoByName(schema.Name, schema.BindingFlags);

            Assert.IsTrue(schema.Type.IsAssignableFrom(info.FieldType), $"[{DataUtility.GetTypeName(schema.Type)}] is not assignable from [{DataUtility.GetTypeName(info.FieldType)}]");
            if (schema.Type == info.FieldType)
                TestContext?.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) matches field with the signature: \n\t\t\t{schema}");
            else
                TestContext?.WriteLine($"\t✓ typeof({DataUtility.GetTypeName(schema.Type)}) is implemented by field {schema}");

        }

        /// <summary>
        /// Asserts that the access modifiers of the field described by the schema match the actual field's attributes.
        /// </summary>
        /// <remarks>
        /// If <paramref name="schema"/> is <c>null</c>, the test passes when no public fields exist and is inconclusive otherwise.
        /// </remarks>
        /// <param name="schema">The field schema whose access level is compared against the actual field.</param>
        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Access_Should_Match(MemberSchema schema)
        {
            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (schema is null) TestContext?.WriteLine($"\t✓ schema is NULL. Skipping Test");
            if (schema is null)
            {
                var fields = GetImplementedFieldKeys();
                if (!fields.Any())
                {
                    TestContext?.WriteLine($"\t✓ No public fields exist.");
#pragma warning disable MSTEST0032
                    Assert.AreEqual(0, 0);
#pragma warning restore MSTEST0032
                    return;
                }
                
                OutputFieldCountAndList();
                Assert.Inconclusive();
            }
            
            var info = GetFieldInfoByName(schema.Name, schema.BindingFlags);


            Assert.AreEqual((FieldAttributes)schema.Access, (info?.Attributes ?? default), $"Access modifiers do not match for field with the signature:\n\t\t\t{schema}");

            TestContext?.WriteLine($"\t✓ field access modifiers match: {schema}");
        }

        /// <summary>
        /// Asserts that the implemented field signatures for the given access level match the expected field signatures, and vice versa.
        /// </summary>
        /// <remarks>
        /// Signatures whose schema entry sets <c>CanTestSignature</c> to <c>false</c> are excluded from the comparison.
        /// The test is inconclusive when no field schema is defined and fields exist.
        /// </remarks>
        /// <param name="access">The access level and static state to filter signatures by.</param>
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

            // If schema list is empty, then skip the test. (2 if statements are used to pass code coverage)
            if (IsFieldListEmpty) TestContext?.WriteLine($"\t-\tNo field schema added. Skipping Test");
            if (IsFieldListEmpty)
            {
                //var fields = typeof(TModel).GetFields(BindingFlags.Public | (access.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : 0) | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (!implementedKeys.Any())
                {
                    TestContext?.WriteLine($"\t✓ No {access} fields exist.");
#pragma warning disable MSTEST0032
                    Assert.AreEqual(0, 0);
#pragma warning restore MSTEST0032
                    return;
                }
                
                OutputFieldCountAndList(access);
                Assert.Inconclusive();
            }

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
        /// <summary>
        /// Builds the distinct string representations of the schema's expected fields that match the given access level.
        /// </summary>
        /// <param name="access">The access level to filter the expected fields by.</param>
        /// <returns>Distinct field schema strings.</returns>
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

        /// <summary>
        /// Builds the distinct signature keys of the fields implemented on <typeparamref name="TModel"/> that match the given access level.
        /// </summary>
        /// <param name="access">The access level to filter the implemented fields by.</param>
        /// <returns>Distinct signatures in the form <c>access TypeName FieldName</c>.</returns>
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

        /// <summary>
        /// Writes the implemented and expected field counts and keys for the given access level to the test output.
        /// </summary>
        /// <param name="accessModifiers">The access level to report on.</param>
        protected void OutputFieldCountAndList(AccessModifiers accessModifiers = AccessModifiers.Public)
        {
            // Retrieve the list of unique implemented constructor signatures
            var implementedKeys = GetImplementedFieldKeys(accessModifiers).ToArray();
            // Retrieve the list of unique expected constructor signatures
            var expectedKeys = GetExpectedFieldKeys(accessModifiers).ToArray();
            TestContext?.WriteLine($"\t✓\tNumber of implemented {accessModifiers} fields is {implementedKeys.Length}");
            OutputImplementedKeys(implementedKeys, expectedKeys);
        }

        #endregion
    }
}
