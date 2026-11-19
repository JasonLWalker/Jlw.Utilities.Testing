using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{
    /// <summary>
    /// Represents an exception that is thrown to indicate the successful execution of an assertion.
    /// </summary>
    /// <remarks>
    /// This exception is primarily used in testing scenarios to signal the success of an assertion 
    /// and is derived from <see cref="Microsoft.VisualStudio.TestTools.UnitTesting.UnitTestAssertException" />.
    /// </remarks>
    public class AssertSucceededException : UnitTestAssertException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AssertSucceededException" /> class.
        /// </summary>
        /// <param name="msg"> The message. </param>
        public AssertSucceededException(string msg)
            : base(msg)
        {
        }

    }
}