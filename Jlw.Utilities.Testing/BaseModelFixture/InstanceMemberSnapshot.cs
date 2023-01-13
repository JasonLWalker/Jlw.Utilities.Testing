using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    public class InstanceMemberSnapshot 
    {
        protected IDictionary<string, object> _memberData = new Dictionary<string, object>();

        public InstanceMemberSnapshot(object o)
        {
            var fields = o?.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            var props = o?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            foreach (var field in fields)
            {
                if (field.CustomAttributes.Count() < 1)
                    _memberData[field.Name] = field.GetValue(o);
            }

            foreach (var prop in props)
            {
                if (prop.GetMethod != null)
                    _memberData[prop.Name] = prop.GetValue(o);
            }
        }

        public void AssertAreSame(InstanceMemberSnapshot expectedInstance, string excludeMember) => AssertAreSame(expectedInstance, new[] {excludeMember});

        public void AssertAreSame(InstanceMemberSnapshot expectedInstance, IEnumerable<string> excludeMembers = null)
        {
            var excluded = excludeMembers ?? new string[] { };
            var actual = _memberData.Where(o => excluded.All(s => s != o.Key)).ToDictionary(o=>o.Key, o=>o.Value);
            var expected = expectedInstance._memberData.Where(o => excluded.All(s => s != o.Key)).ToDictionary(o => o.Key, o => o.Value);

            foreach (var kvp in actual)
            {
                Assert.IsTrue(expected.ContainsKey(kvp.Key), $"Member [{kvp.Key}] does not exist in expected instance");
                Assert.AreEqual(expected.FirstOrDefault(o=>o.Key == kvp.Key).Value, kvp.Value, $"AssertAreSame Failed: Member [{kvp.Key}] contains different values");
            }
            foreach (var kvp in expected)
            {
                Assert.IsTrue(actual.ContainsKey(kvp.Key), $"Member [{kvp.Key}] does not exist in expected instance");
                Assert.AreEqual(actual.FirstOrDefault(o => o.Key == kvp.Key).Value, kvp.Value, $"AssertAreSame Failed: Member [{kvp.Key}] contains different values");
            }
        }
    }
}