using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelFixture<TModel, TSchema>
    {
        protected static IEnumerable<BaseModelUtility<TModel>.MemberSchema> _fieldSchema => modelSchema.FieldList;

        public static IEnumerable<object[]> FieldList => _fieldSchema.Select(o => new object[] { o });


        #region Field Tests
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
        public virtual void Field_Count_ShouldMatch(AccessModifiers accessModifiers, bool flattenHierarchy = true)
        {
            if (_fieldSchema.Count(o => o != null) < 1)
            {
                Console.WriteLine($"\t✓ No field schema added. Skipping Test");
                Assert.Inconclusive();
            }

            BindingFlags flags = flattenHierarchy ? BindingFlags.FlattenHierarchy : default;
            flags |= accessModifiers.HasFlag(AccessModifiers.Public) ? BindingFlags.Public : BindingFlags.NonPublic;
            flags |= accessModifiers.HasFlag(AccessModifiers.Static) ? BindingFlags.Static : BindingFlags.Instance;

            var t = typeof(TModel);
            var aInfo = t.GetFields(flags);
            BindingFlags mask = ~(BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            Assert.IsNotNull(aInfo, $"Unable to retrieve FieldInfo for {GetTypeName(t)} with BindingFlags: {flags}");
            Console.WriteLine($"\t✓ FieldInfo retrieved");

            int nCount = _fieldSchema.Count(o => o != null && ((o.BindingFlags & mask) == (flags & mask)) && o.Access.Equals(accessModifiers));
            int nMemCount = 0;
            string sProps = "";
            foreach (var info in aInfo)
            {
                if (accessModifiers.Equals((AccessModifiers)info?.Attributes))
                {
                    string access = GetAccessString((AccessModifiers)info.Attributes);
                    sProps += $"\t\t{access} {GetTypeName(info.FieldType)} {info.Name}\n";
                    nMemCount++;
                }
            }
            Console.WriteLine($"\t✓ Fields Retrieved:\n{sProps}");

            Assert.AreEqual(nCount, nMemCount, $"Number of fields is incorrect. Should be {nCount} for BindingFlags: {flags}, and AccessModifiers: {accessModifiers}");
            Console.WriteLine($"\t✓ Number of fields is {nCount} for BindingFlags: {flags}, and AccessModifiers: {accessModifiers}");
        }

        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_ShouldExist(MemberSchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
            }

            var t = typeof(TModel);

            var info = AssertFieldExists(schema.Name);
            Console.WriteLine($"\t✓ property [{schema.Name}] exists with PropertyType.Attributes: {info.FieldType.Attributes}");
        }

        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_Type_IsAssignable(MemberSchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
            }

            var t = typeof(TModel);
            var info = GetFieldInfoByName(schema.Name, schema.BindingFlags);

            Assert.IsTrue(schema.Type.IsAssignableFrom(info.FieldType), $"[{GetTypeName(schema.Type)}] is not assignable from [{GetTypeName(info.FieldType)}]");
            if (schema.Type == info.FieldType)
                Console.WriteLine($"\t✓ typeof({GetTypeName(schema.Type)}) matches field with the signature: \n\t\t\t{schema}");
            else
                Console.WriteLine($"\t✓ typeof({GetTypeName(schema.Type)}) is implemented by field {schema}");

        }

        [TestMethod]
        [DynamicData(nameof(FieldList))]
        public virtual void Field_AccessModifiers_ShouldMatch(MemberSchema schema)
        {
            if (schema is null)
            {
                Console.WriteLine($"\t✓ schema is NULL. Skipping Test");
                Assert.Inconclusive();
            }

            var info = GetFieldInfoByName(schema.Name, schema.BindingFlags);


            Assert.AreEqual((FieldAttributes)schema.Access, (info?.Attributes ?? default), $"Access modifiers do not match for field with the signature:\n\t\t\t{schema}");

            Console.WriteLine($"\t✓ field access modifiers match: {schema}");
        }


        #endregion

    }
}
