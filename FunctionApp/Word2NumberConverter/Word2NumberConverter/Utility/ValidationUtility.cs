using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Word2NumberConverter.Utility
{
    /// <summary>
    /// Utility class used for validating input string.
    /// </summary>
    public sealed class ValidationUtility
    {
        /// <summary>
        /// Utility function to get invalid characters in the input string. 
        /// </summary>
        /// <param name="numberInWords">Input string containing words for conversion.</param>
        /// <returns>List of invalid charcters in the input string.</returns>
        public static IEnumerable<char> CheckForInvalidCharctersOtherThanValidLetters(string numberInWords)
        {
            List<char> invalidCharacters = new List<char>();

            if (!string.IsNullOrWhiteSpace(numberInWords))
            {
                invalidCharacters = numberInWords.Select(e => e)
                              .Where(e => !Char.IsLetter(e) && !Char.IsWhiteSpace(e))
                              .Select(value => value)
                              .ToList<char>();
            }

            return invalidCharacters;
        }

        /// <summary>
        /// Validate minus word in the input
        /// </summary>
        /// <param name="numberInWords">Input string containing words for conversion.</param>
        /// <returns>Return exception message if it contains invalid values for minus word</returns>
        public static string ValidateMinusWordInInput(string numberInWords)
        {
            string exceptionMessage = "";
            IEnumerable<string> numberList = new List<string>();
            if (!string.IsNullOrWhiteSpace(numberInWords))
            {
                numberList = Regex.Matches(numberInWords, @"\w+").Cast<Match>()
                    .Where(e => e.Value.ToLowerInvariant() == "minus")
                    .Select(e => e.Value);

                if (numberList.Count() == 1 && !numberInWords.StartsWith("minus", StringComparison.InvariantCultureIgnoreCase))
                    exceptionMessage = "Minus word should be in the begining of the input.";
                if (numberList.Count() > 1)
                    exceptionMessage += "Minus word occurs more than once.";
            }

            return exceptionMessage;
        }
    }
}
