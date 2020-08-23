using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing
{
    public class AssertSucceededException : UnitTestAssertException
    {
        /*
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AssertSucceededException" /> class.
        /// </summary>
        /// <param name="msg"> The message. </param>
        /// <param name="ex"> The exception. </param>
        public AssertSucceededException(string msg, Exception ex)
            : base(msg, ex)
        {
        }
        */
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AssertSucceededException" /> class.
        /// </summary>
        /// <param name="msg"> The message. </param>
        public AssertSucceededException(string msg)
            : base(msg)
        {
        }

        /*
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AssertSucceededException" /> class.
        /// </summary>
        public AssertSucceededException()
        {
        }
        */
    }
}