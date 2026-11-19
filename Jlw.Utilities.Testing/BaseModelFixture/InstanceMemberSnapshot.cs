using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Captures the values of an object's fields and readable properties so that they can be compared before and after an operation.
    /// </summary>
    /// <remarks>
    /// Used by the property value tests to confirm that setting one member does not change any other member.
    /// </remarks>
    public class InstanceMemberSnapshot 
    {
        /// <summary>
        /// The captured member values, keyed by member name.
        /// </summary>
        protected IDictionary<string, object> _memberData = new Dictionary<string, object>();

        /// <summary>
        /// Creates a snapshot of the instance and its members.
        /// </summary>
        /// <remarks>
        /// Captures every instance, static, public, and non-public field that has no custom attributes,
        /// and every property that has a getter. A <c>null</c> object produces an empty snapshot.
        /// </remarks>
        /// <param name="o">The object to capture.</param>
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

        /// <summary>
        /// Asserts that this snapshot matches the expected snapshot, excluding a single member.
        /// </summary>
        /// <param name="expectedInstance">The snapshot to compare against.</param>
        /// <param name="excludeMember">The name of the member to exclude from the comparison.</param>
        public void AssertAreSame(InstanceMemberSnapshot expectedInstance, string excludeMember) => AssertAreSame(expectedInstance, new[] {excludeMember});

        /// <summary>
        /// Asserts that this snapshot matches the expected snapshot, excluding the given members.
        /// </summary>
        /// <remarks>
        /// Each member present in one snapshot must also be present in the other, with an equal value.
        /// Excluded members are ignored on both sides.
        /// </remarks>
        /// <param name="expectedInstance">The snapshot to compare against.</param>
        /// <param name="excludeMembers">The names of the members to exclude from the comparison, or <c>null</c> to exclude none.</param>
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
