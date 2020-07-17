using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Jlw.Standard.Utilities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Standard.Utilities.Testing.DataSources
{
    public class RandomStringSourceAttribute : DataSourceAttributeBase, ITestDataSource
    {
        private string _validChars = null;
        private int _stringsToReturn = 10;
        private int _minLength = 0;
        private int _maxLength = 0;
        private static readonly Random Rand = new Random();
        
        
        public RandomStringSourceAttribute()
        {
        }
        

        public RandomStringSourceAttribute(int numStrings, int length = 0, string validChars = null)
        {
            _stringsToReturn = numStrings;


            _validChars = validChars ?? _validChars;

            if (length > 0)
                SetLength(length, length);
        }

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

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            for (int n = 0; n < _stringsToReturn; n++)
            {
                yield return new object[] { DataUtility.GenerateRandom<string>(_minLength, _maxLength, _validChars)};
            }
        }

        /*
        public static string Generate(int minLength = 10, int? maxLength = null, string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopsrstuvwxyz1234567890")
        {
            int min = Math.Min(minLength, maxLength ?? minLength);
            int max = Math.Max(minLength, maxLength ?? minLength);
            int len = Rand.Next(min, max);
            var s = new StringBuilder("", max);

            while (s.Length < len)
            {
                s.Append(validChars[Rand.Next(0, validChars.Length)]);
            }

            return s.ToString();
        }
        */
    }
}