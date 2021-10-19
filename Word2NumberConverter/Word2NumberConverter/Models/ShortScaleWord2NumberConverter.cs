using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using Word2NumberConverter.Models;
using Word2NumberConverter.Utility;

namespace Word2NumberConverter
{
    /// <summary>
    /// Class to convert words to number.
    /// </summary>
    public class ShortScaleWord2NumberConverter : IWord2NumberConverter
    {

        /// <summary>
        /// Main method which validates the words input and returns the number.
        /// </summary>
        /// <param name="numberInWords">Words needs to be converted to number.</param>
        /// <returns>The number calculated from after translation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="numberInWords"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="numberInWords"/> has invalid characters or 
        /// Invalid words not supported for conversion.</exception>
        public BigInteger ConvertWordsToNumber(string numberInWords)
        {
            if (string.IsNullOrWhiteSpace(numberInWords))
                throw new ArgumentNullException(nameof(numberInWords));

            string exceptionMessage = "";

            // Check for valid letters within string of words.
            IEnumerable<char> invalidCharacters = ValidationUtility.CheckForInvalidCharctersOtherThanValidLetters(numberInWords);

            // Append invalid characters to exception message.
            if (invalidCharacters.Count() > 0)
                exceptionMessage += "Invalid characters in the input:" + String.Join(",", invalidCharacters) + ". " + Environment.NewLine;

            // Check for letters supported within predifined number list.
            IEnumerable<string> invalidNumbers = ShortScaleNumberUtility.CheckForNumbersNotInPreDefinedList(numberInWords);

            // Append invalid to words to exception message, which are not in expected predefined list. 
            if (invalidNumbers.Count() > 0)
                exceptionMessage += "Invalid words for number conversion:" + String.Join(",", invalidNumbers) + Environment.NewLine;

            // Validate minus word in input.
            string validationResult = ValidationUtility.ValidateMinusWordInInput(numberInWords);

            // Add validation result exceptions for minus.  
            if (validationResult.Length > 0)
                exceptionMessage += validationResult;

            // Throw all the appended exception, in case of invalid input.
            if (exceptionMessage.Length > 0)
                throw new ArgumentException(exceptionMessage);

            // Splitwords and validate with predefined list. 
            IEnumerable<string> validWordsList = ShortScaleNumberUtility.GetWordsToNumberMappingFromPredefinedList(numberInWords);

            // Calculate number from words list.
            return CalculateNumberFromValidWords(validWordsList);
        }


        /// <summary>
        /// Method to calculate number from predefined list of words.
        /// </summary>
        /// <param name="validWords"> List of valid words to be converted to number.</param>
        /// <returns> Number claculated from words input.</returns>
        private BigInteger CalculateNumberFromValidWords(IEnumerable<string> validWords)
        {
            // Logic with example.
            // Words higher than or equal to 100, will have to be pronounced with <100 numbers.
            // Ex 1: Nine Hundred
            // Nine
            // partialSum = 9 
            // Hundred
            // partialSum  = 9 * 100.
            // finalNumber (subTotal + partialSum)
            // Ex 2: One thousand Nine Hundred
            // One thousand
            // partialSum = 1
            // thousand
            // Numbers greater than or equal to 1000, will add (subTotal +  partialSum *  number).
            // subTotal = (0 + (1 * 1000)), partialSum=0
            // Nine
            // partialSum = 9 
            // Hundred
            // partialSum  = 9 * 100.
            // After all numbers are iterated get final number = sign * (subTotal + partialSum) 

            BigInteger partialSum = 0, subTotal = 0;
            BigInteger sign = validWords.FirstOrDefault().StartsWith("-1", StringComparison.InvariantCultureIgnoreCase) ? -1 : 1;

            foreach (var word in validWords)
            {

                // Convert string to BigInteger for number calculation.
                BigInteger number = BigInteger.Parse(word);

                if (number == -1)
                    continue;

                // For numbers greater than or equal 1000. subTotal = subTotal + (partialSum * number).
                if (number >= 1000)
                {
                    subTotal = BigInteger.Add(subTotal, BigInteger.Multiply(partialSum, number));

                    // Reset partial sum.
                    partialSum = 0;
                }
                else if (number >= 100)
                {
                    // For number with greater than or equal to hundred  partialSum = partialSum * number. 
                    partialSum = BigInteger.Multiply(partialSum, number);
                }
                else
                {
                    // For numbers < 100, add to partialSum.
                    partialSum = BigInteger.Add(partialSum, number);
                }
            }

            // final number = sign * (subTotal + partialSum).
            return BigInteger.Multiply(sign,BigInteger.Add(subTotal, partialSum));
        }
    }
}
