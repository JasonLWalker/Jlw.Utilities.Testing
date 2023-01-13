using Jlw.Utilities.Data;

namespace Jlw.Utilities.Testing
{
    public class InstanceMemberTestData<T>
    {
        public T SystemUnderTest { get; protected set; }
        public string MemberName { get; protected set; }
        public object ExpectedValue { get; protected set; }
        protected string _testDescription;
        protected string _sutDescription;

        public InstanceMemberTestData(T sut, string memberName, object expectedValue, string testDescription = null, string sutDescription=null)
        {
            SystemUnderTest = sut;
            MemberName = memberName;
            ExpectedValue = expectedValue;
            _testDescription = testDescription;
            _sutDescription = sutDescription;
        }

        public override string ToString()
        {
            string sutType = DataUtility.GetTypeName(SystemUnderTest.GetType());
            string expectedType = ExpectedValue == null ? "" : $"({DataUtility.GetTypeName(ExpectedValue?.GetType())})";
            string value = (ExpectedValue?.GetType() == typeof(string)) ? $"\"{ExpectedValue}\"" : ExpectedValue?.ToString() ?? "null";
            string sutDesc = _sutDescription ?? $"{sutType}";
            return _testDescription ?? $"{sutDesc}, \"{MemberName}\", {expectedType}{value}";
        }
    }
}