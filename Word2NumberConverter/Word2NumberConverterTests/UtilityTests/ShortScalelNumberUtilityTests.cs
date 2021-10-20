using System.Linq;
using Word2NumberConverter.Utility;
using Xunit;

namespace Word2NumberConverterTests
{
    /// <summary>
    /// Utility class containing word to number mapping dictionary and other methods to support number calculation. 
    /// </summary>
    public class ShortScalelNumberUtilityTests
    {
        /// <summary>
        /// Test CheckForNumbersNotInPreDefinedList with empty and null input.
        /// </summary>
        /// <param name="wordsForConversion">Words needs to be converted to number.</param>
        [Theory]
        //InlineData(NumberInWords, CountOfReturnValues)
        [InlineData(null, 0)]
        [InlineData("", 0)]
        public void CheckForNumbersNotInPreDefinedList_EmptyOrNullInput_ReturnsEmptyList(string wordsForConversion, int CountOfReturnValues)
        {
            var result = ShortScaleNumberUtility.CheckForNumbersNotInPreDefinedList(wordsForConversion);

            Assert.Equal(CountOfReturnValues, result.Count());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordsForConversion"></param>
        /// <param name="CountOfReturnValues"></param>
        //InlineData(NumberInWords, CountOfReturnValues)
        [Theory]
        [InlineData("random test of invalid number" , 5)]
        [InlineData("one thousand", 0)]
        public void CheckForNumbersNotInPreDefinedList_ValidInput(string wordsForConversion, int CountOfReturnValues)
        {
            var result = ShortScaleNumberUtility.CheckForNumbersNotInPreDefinedList(wordsForConversion);

            Assert.Equal(CountOfReturnValues, result.Count());
        }
    }
}
