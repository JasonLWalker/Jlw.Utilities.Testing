using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Holds the data for one instance member test: the object to test, the member to read, and the value that member is expected to have.
    /// </summary>
    /// <typeparam name="T">The type of the system under test.</typeparam>
    public class InstanceMemberTestData<T>
    {
        /// <summary>
        /// Gets the object instance whose member is read by the test.
        /// </summary>
        public T SystemUnderTest { get; protected set; }
        /// <summary>
        /// Gets the name of the field or property to read from the system under test.
        /// </summary>
        public string MemberName { get; protected set; }
        /// <summary>
        /// Gets the value that the member is expected to have.
        /// </summary>
        public object ExpectedValue { get; protected set; }
        /// <summary>
        /// Optional custom description of the test, returned by <see cref="ToString"/> when set.
        /// </summary>
        protected string _testDescription;
        /// <summary>
        /// Optional custom description of the system under test, used in the default <see cref="ToString"/> output when set.
        /// </summary>
        protected string _sutDescription;

        /// <summary>
        /// Creates a new instance of the test data.
        /// </summary>
        /// <param name="sut">The object instance whose member is read by the test. May be <c>null</c> for placeholder entries.</param>
        /// <param name="memberName">The name of the field or property to read.</param>
        /// <param name="expectedValue">The value that the member is expected to have.</param>
        /// <param name="testDescription">Optional custom description of the test.</param>
        /// <param name="sutDescription">Optional custom description of the system under test.</param>
        public InstanceMemberTestData(T sut, string memberName, object expectedValue, string testDescription = null, string sutDescription=null)
        {
            SystemUnderTest = sut;
            MemberName = memberName;
            ExpectedValue = expectedValue;
            _testDescription = testDescription;
            _sutDescription = sutDescription;
        }

        /// <summary>
        /// Returns a display string for the test, used as the test name in the test runner.
        /// </summary>
        /// <remarks>
        /// Returns the custom test description when one was given. Otherwise, the result combines the system under test
        /// description, the member name, and the expected value, formatted with its type name.
        /// </remarks>
        /// <returns>A description of the test.</returns>
        public override string ToString()
        {

            string sutType = SystemUnderTest == null ? "null" : DataUtility.GetTypeName(SystemUnderTest?.GetType());
            string expectedType = ExpectedValue == null ? "" : $"({DataUtility.GetTypeName(ExpectedValue?.GetType())})";
            string value = (ExpectedValue?.GetType() == typeof(string)) ? $"\"{ExpectedValue}\"" : ExpectedValue?.ToString() ?? "null";
            string sutDesc = _sutDescription ?? $"{sutType}";
            return _testDescription ?? $"{sutDesc}, \"{MemberName}\", {expectedType}{value}";
        }
    }
}
