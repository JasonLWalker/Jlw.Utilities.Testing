using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Jlw.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.DataSources
{
    /// <summary>
    /// Supplies randomly generated strings as test data for a test method parameter.
    /// </summary>
    /// <remarks>
    /// By default, ten strings are generated, each between 1 and 15 characters long, using the default character set.
    /// </remarks>
    public class RandomStringSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        private string _validChars = null;
        private int _stringsToReturn = 10;
        private int _minLength = 1;
        private int _maxLength = 15;

        /// <summary>
        /// Creates an attribute that supplies ten random strings, each between 1 and 15 characters long.
        /// </summary>
        public RandomStringSourceAttribute()
        {
        }

        /// <summary>
        /// Creates an attribute that supplies a number of random strings of a fixed or default length.
        /// </summary>
        /// <remarks>
        /// If <paramref name="length"/> is greater than zero, every string has exactly that length.
        /// Otherwise the strings are between 1 and 15 characters long.
        /// </remarks>
        /// <param name="numStrings">The number of strings to generate.</param>
        /// <param name="length">The length of each string, or zero to use the default length range.</param>
        /// <param name="validChars">The characters to use, or <c>null</c> to use the default character set.</param>
        public RandomStringSourceAttribute(int numStrings, int length = 0, string validChars = null)
        {
            _stringsToReturn = numStrings;


            _validChars = validChars ?? _validChars;

            if (length > 0)
                SetLength(length, length);
        }

        /// <summary>
        /// Creates an attribute that supplies a number of random strings with lengths in a range.
        /// </summary>
        /// <param name="numStrings">The number of strings to generate.</param>
        /// <param name="minLength">The minimum length of each string.</param>
        /// <param name="maxLength">The maximum length of each string.</param>
        /// <param name="validChars">The characters to use, or <c>null</c> to use the default character set.</param>
        public RandomStringSourceAttribute(int numStrings, int minLength, int maxLength, string validChars = null)
        {
            _stringsToReturn = numStrings;
            _validChars = validChars ?? _validChars;
            SetLength(minLength, maxLength);
        }

        private void SetLength(int minLength, int maxLength)
        {
            _minLength = Math.Min(minLength, maxLength);
            _maxLength = Math.Max(minLength, maxLength);
        }

        /// <summary>
        /// Gets the test data rows for the test method, one row per generated random string.
        /// </summary>
        /// <param name="methodInfo">The test method that receives the data.</param>
        /// <returns>One single-element array per string, containing that string.</returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            for (int n = 0; n < _stringsToReturn; n++)
            {
                yield return new object[] { DataUtility.GenerateRandom<string>(_minLength, _maxLength, _validChars) };
            }
        }
    }
}
