using System.Linq;
using System.Text;
using Word2NumberConverter.Utility;
using Xunit;

namespace Word2NumberConverterTests.UtilityTests
{
    /// <summary>
    /// Test class for ValidationUtility.
    /// </summary>
    public class ValidationUtilityTests
    {
        /// <summary>
        /// Tests for empty and null input.
        /// </summary>
        /// <param name="wordsForConversion">Input.</param>
        /// <param name="CountOfReturnValues">Expected number of results.</param>
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        public void CheckForInvalidCharctersOtherThanValidLetters_EmptyOrNullInput_ReturnsEmptyList(string wordsForConversion, int CountOfReturnValues)
        {
            var result = ValidationUtility.CheckForInvalidCharctersOtherThanValidLetters(wordsForConversion);

            Assert.Equal(CountOfReturnValues, result.Count());
        }

        [Theory]
        [InlineData("one thousand12345", 5)]
        [InlineData("123", 3)]
        public void CheckForInvalidCharctersOtherThanValidLetters__ValidInput_Success(string wordsForConversion, int CountOfReturnValues)
        {
            var result = ValidationUtility.CheckForInvalidCharctersOtherThanValidLetters(wordsForConversion);

            Assert.Equal(CountOfReturnValues, result.Count());
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        public void ValidateMinusWordInInput_EmptyOrNullInput_NoException(string wordsForConversion, int CountOfException)
        {
            var result = ValidationUtility.ValidateMinusWordInInput(wordsForConversion);

            Assert.Equal(CountOfException, result.Length);
        }

        [Theory]
        [InlineData("one thousand minus", "Minus word should be in the begining of the input.")]
        [InlineData("minus one thousand minus", "Minus word occurs more than once.")]
        public void ValidateMinusWordInInput_Success(string wordsForConversion, string expectedExceptionMessage)
        {
            var result = ValidationUtility.ValidateMinusWordInInput(wordsForConversion);

            Assert.Equal(expectedExceptionMessage, result);
        }
    }
}
