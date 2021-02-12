using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        public class MemberSchema
        {
            protected List<MemberTest> _testItems = new List<MemberTest>() { default };
            
            public virtual string Name { get; protected set; }
            public virtual Type Type { get; protected set; }
            public virtual AccessModifiers Access { get; protected set; }
            public BindingFlags BindingFlags { get; protected set; }
            public IEnumerable<object> TestData => _testItems;
            public Action<MemberTest> TestCallback { get; protected set; }

            public MemberSchema(string name, Type type = null, AccessModifiers access = AccessModifiers.Public, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy, IEnumerable<object> testValues = null, IDictionary<string, object> expectedData = null, Action<MemberTest> fnCallback = null)
            {
                Name = name;
                Type = type;
                Access = access;
                BindingFlags = bindingFlags;
                TestCallback = fnCallback ?? DefaultTestCallback;

                if (testValues != null)
                {
                    foreach (var val in testValues)
                    {
                        _testItems.Add(val is IEnumerable
                            ? new MemberTest(this, (IEnumerable<object>)val, expectedData, TestCallback)
                            : new MemberTest(this, new[] { val }, expectedData, TestCallback)
                        );
                    }
                }

            }

            public override string ToString()
            {
                string type = GetTypeName(Type);
                string access = GetAccessString((MethodAttributes) Access);

                return $"{access} {type} {Name}";
            }

            public static void DefaultTestCallback(MemberTest test) {
                Console.WriteLine($"\t✓ No Test Assertions have been specified. Skipping Test");
                Assert.Inconclusive();
            }
        }
    }
}