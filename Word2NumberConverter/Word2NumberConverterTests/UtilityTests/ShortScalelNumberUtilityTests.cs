using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Word2NumberConverter.Utility;

namespace Word2NumberConverterTests
{
    /// <summary>
    /// Utility class containing word to number mapping dictionary and other methods to support number calculation. 
    /// </summary>
    [TestClass]
    public class ShortScalelNumberUtilityTests
    {
        /// <summary>
        /// Test CheckForNumbersNotInPreDefinedList with empty and null input.
        /// </summary>
        /// <param name="wordsForConversion">Words needs to be converted to number.</param>
        /// <param name="CountOfReturnValues">Count Of values not in pre defined list</param>
        [TestMethod]
        //DataRow(NumberInWords, CountOfReturnValues)
        [DataRow(null, 0)]
        [DataRow("", 0)]
        public void CheckForNumbersNotInPreDefinedList_EmptyOrNullInput_ReturnsEmptyList(string wordsForConversion, int CountOfReturnValues)
        {
            var result = ShortScaleNumberUtility.CheckForNumbersNotInPreDefinedList(wordsForConversion);

            Assert.AreEqual(CountOfReturnValues, result.Count(), "No return values when string is empty or null");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordsForConversion"></param>
        /// <param name="CountOfReturnValues"></param>
        /// <param name="testDescription"></param>
        [TestMethod]
        //DataRow(NumberInWords, CountOfReturnValues)
        [DataRow("random test of invalid number" , 5, "All the five words are invalid.")]
        [DataRow("one thousand", 0, "All the words are valid.")]
        public void CheckForNumbersNotInPreDefinedList_ValidInput(string wordsForConversion, int CountOfReturnValues, string testDescription)
        {
            var result = ShortScaleNumberUtility.CheckForNumbersNotInPreDefinedList(wordsForConversion);

            Assert.AreEqual(CountOfReturnValues, result.Count(), testDescription);
        }
    }
}
