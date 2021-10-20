using System;
using System.Collections.Generic;
using System.Numerics;
using Word2NumberConverter;
using Xunit;

namespace Word2NumberConverterTests
{
    /// <summary>
    /// Test class for ShortScaleWord2NumberConverter class related tests
    /// </summary>
    public class ShortScaleWord2NumberConverterTests
    {
        /// <summary>
        /// Test for Null Input, the ArgumentNullException should be thrown.
        /// </summary>
        //InlineData(numberInWords, expectedExceptionMessage, parameterName)
        [Theory]
        [InlineData(null, "Value cannot be null.", "numberInWords")]
        public void ConvertWordsToNumber_NullInput_ArgumentNullExceptionThrown(string wordsForConversion, string expectedExceptionMessage, string parameterName)
        {
            //Action
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ShortScaleWord2NumberConverter().ConvertWordsToNumber(wordsForConversion));

            //Assert
            Assert.Contains(parameterName,exception.ParamName);
            Assert.Contains(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Test for Invalid Input, the ArgumentException should be thrown.
        /// Invalid Input Scenario's:
        /// 1) invalid characters other than valid letter. 
        /// 2) Invalid words not supported for conversion.
        /// </summary>
        [Theory]
        //InlineData(numberInWords, expectedExceptionMessage)
        [InlineData("One thousand 200", "Invalid words for number conversion:200")]
        [InlineData("One thousand dsdsgdfg", "Invalid words for number conversion:dsdsgdfg")]
        public void ConvertWordsToNumber_InvalidInput_ArgumentExceptionThrown(string wordsForConversion, string expectedExceptionMessage)
        {
            //Action
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new ShortScaleWord2NumberConverter().ConvertWordsToNumber(wordsForConversion));

            //Assert
            Assert.Contains(expectedExceptionMessage, exception.Message);
        }


        /// <summary>
        /// Test for valid Inputs."Invalid characters in the input:2,0,0. \r\nInvalid words for number conversion:200\r\n"
        /// </summary>
        [Theory, MemberData(nameof(ValidInput))]
        public void ConvertWordsToNumber_ValidInput_Success(string wordsForConversion, BigInteger expectedNumber)
        {
            //Action
            ShortScaleWord2NumberConverter shortScaleNumberConverter = new ShortScaleWord2NumberConverter();
            BigInteger result = shortScaleNumberConverter.ConvertWordsToNumber(wordsForConversion);

            //Assert
            Assert.Equal(expectedNumber, result);
        }

        /// <summary>
        /// Test for case in-sensitivity tests.
        /// </summary>
        [Theory]
        [InlineData("One thousand", 1000)]
        [InlineData("one THousand", 1000)]
        [InlineData("ONE THousand", 1000)]
        public void ConvertWordsToNumber_ValidInput_WithIgnoringCase_Success(string wordsForConversion, int expectedNumber)
        {
            //Action
            ShortScaleWord2NumberConverter shortScaleNumberConverter = new ShortScaleWord2NumberConverter();
            BigInteger result = shortScaleNumberConverter.ConvertWordsToNumber(wordsForConversion);

            //Assert
            Assert.Equal(expectedNumber, result);
        }


        /// <summary>
        /// Input containing valid conversions, this can be factored for validating all tests.
        /// We need to add more tests in a generic way.
        /// </summary>
        public static IEnumerable<object[]> ValidInput
        {
            get
            {
                //Input in the below format(Words to be converted, expectedNumber, testDescription)

                yield return new object[] { "twenty", new BigInteger(20) };

                yield return new object[] { "one hundred", new BigInteger(100) };

                yield return new object[] { "minus one hundred", new BigInteger(-100) };

                yield return new object[] { "One thousand two hundred", new BigInteger(1200) };

                yield return new object[] { "One million fifty five hundred", new BigInteger(1005500) };

                yield return new object[] { "One Billion twenty five million fifty five thousand twenty two", new BigInteger(1025055022) };

                yield return new object[] { "one hundred trillion two hundred fifty", new BigInteger(100000000000250) };

                yield return new object[] { "ten septillion twenty five trillion", BigInteger.Parse("10000000000025000000000000") };

                yield return new object[] { "one duodecillion two octillion five hundred septillion", BigInteger.Parse("1000000000002500000000000000000000000000") };

                yield return new object[] { "ten sexvigintillion five quattuorvigintillion three hundred forty two trevigintillion two unvigintillion five hundred vigintillion", BigInteger.Parse("10000005342000002500000000000000000000000000000000000000000000000000000000000000000"), };

                //Max Value supported is centillion
                yield return new object[] { "one centillion twenty five", BigInteger.Parse("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000025")}; ;

            }
        }
    }
}
