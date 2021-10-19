using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word2NumberConverter.Utility;

namespace Word2NumberConverterTests.UtilityTests
{
    /// <summary>
    /// Test class for ValidationUtility.
    /// </summary>
    [TestClass]
    public class ValidationUtilityTests
    {
        /// <summary>
        /// Tests for empty and null input.
        /// </summary>
        /// <param name="wordsForConversion">Input </param>
        /// <param name="CountOfReturnValues"></param>
        [TestMethod]
        [DataRow(null, 0)]
        [DataRow("", 0)]
        public void CheckForInvalidCharctersOtherThanValidLetters_EmptyOrNullInput_ReturnsEmptyList(string wordsForConversion, int CountOfReturnValues)
        {
            var result = ValidationUtility.CheckForInvalidCharctersOtherThanValidLetters(wordsForConversion);

            Assert.AreEqual(CountOfReturnValues, result.Count(), "No return values when string is empty or null");
        }

        [TestMethod]
        [DataRow("one thousand12345", 5, "Five invalid characters 1,2,3,4,5.")]
        [DataRow("123", 3, "three invalid characters 1,2,3.")]
        public void CheckForInvalidCharctersOtherThanValidLetters__ValidInput_Success(string wordsForConversion, int CountOfReturnValues, string testDescription)
        {
            var result = ValidationUtility.CheckForInvalidCharctersOtherThanValidLetters(wordsForConversion);

            Assert.AreEqual(CountOfReturnValues, result.Count(), testDescription);
        }

        [TestMethod]
        [DataRow(null, 0)]
        [DataRow("", 0)]
        public void ValidateMinusWordInInput_EmptyOrNullInput_NoException(string wordsForConversion, int CountOfException)
        {
            var result = ValidationUtility.ValidateMinusWordInInput(wordsForConversion);

            Assert.AreEqual(CountOfException, result.Length, "No exception in the input.");
        }

        [TestMethod]
        [DataRow("one thousand minus", "Minus word should be in the begining of the input.")]
        [DataRow("minus one thousand minus", "Minus word occurs more than once.")]
        public void ValidateMinusWordInInput_Success(string wordsForConversion, string expectedExceptionMessage)
        {
            var result = ValidationUtility.ValidateMinusWordInInput(wordsForConversion);

            Assert.AreEqual(expectedExceptionMessage, result, expectedExceptionMessage);
        }
    }
}
