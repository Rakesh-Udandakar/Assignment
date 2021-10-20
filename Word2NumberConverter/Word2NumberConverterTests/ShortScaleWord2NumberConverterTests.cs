using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;
using Word2NumberConverter;

namespace Word2NumberConverterTests
{
    /// <summary>
    /// Test class for ShortScaleWord2NumberConverter class related tests
    /// </summary>
    [TestClass]
    public class ShortScaleWord2NumberConverterTests
    {
        /// <summary>
        /// Test for Null Input, the ArgumentNullException should be thrown.
        /// </summary>
        [TestMethod]
        //DataRow(numberInWords, expectedExceptionMessage, parameterName)
        [DataRow(null, "Value cannot be null.", "numberInWords")]
        public void ConvertWordsToNumber_NullInput_ArgumentNullExceptionThrown(string wordsForConversion, string expectedExceptionMessage, string parameterName)
        {
            //Action
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => new ShortScaleWord2NumberConverter().ConvertWordsToNumber(wordsForConversion));

            //Assert
            Assert.IsTrue(exception.ParamName.Equals(parameterName));
            Assert.IsTrue(exception.Message.Contains(expectedExceptionMessage));
        }

        /// <summary>
        /// Test for Invalid Input, the ArgumentException should be thrown.
        /// Invalid Input Scenario's:
        /// 1) invalid characters other than valid letter. 
        /// 2) Invalid words not supported for conversion.
        /// </summary>
        [TestMethod]
        //DataRow(numberInWords, expectedExceptionMessage)
        [DataRow("One thousand 200", "Invalid words for number conversion:200")]
        [DataRow("One thousand dsdsgdfg", "Invalid words for number conversion:dsdsgdfg")]
        public void ConvertWordsToNumber_InvalidInput_ArgumentExceptionThrown(string wordsForConversion, string expectedExceptionMessage)
        {
            //Action
            ArgumentException exception = Assert.ThrowsException<ArgumentException>(() => new ShortScaleWord2NumberConverter().ConvertWordsToNumber(wordsForConversion));

            //Assert
            Assert.IsTrue(exception.Message.Contains(expectedExceptionMessage),expectedExceptionMessage);
        }


        /// <summary>
        /// Test for valid Inputs."Invalid characters in the input:2,0,0. \r\nInvalid words for number conversion:200\r\n"
        /// </summary>
        [TestMethod]
        [DynamicData(nameof(ValidInput), DynamicDataSourceType.Property)]
        public void ConvertWordsToNumber_ValidInput_Success(string wordsForConversion, BigInteger expectedNumber)
        {
            //Action
            ShortScaleWord2NumberConverter shortScaleNumberConverter = new ShortScaleWord2NumberConverter();
            BigInteger result = shortScaleNumberConverter.ConvertWordsToNumber(wordsForConversion);

            //Assert
            Assert.AreEqual(expectedNumber, result, $"Expected Number: {expectedNumber}");
        }


        /// <summary>
        /// Test for case in-sensitivity tests.
        /// </summary>
        [TestMethod]
        [DataRow("One thousand", 1000)]
        [DataRow("one THousand", 1000)]
        [DataRow("ONE THousand", 1000)]
        public void ConvertWordsToNumber_ValidInput_WithIgnoringCase_Success(string wordsForConversion, int expectedNumber)
        {
            //Action
            ShortScaleWord2NumberConverter shortScaleNumberConverter = new ShortScaleWord2NumberConverter();
            BigInteger result = shortScaleNumberConverter.ConvertWordsToNumber(wordsForConversion);

            //Assert
            Assert.AreEqual(expectedNumber, result, $"Expected Number: {expectedNumber}");
        }


        /// <summary>
        /// Input containing valid conversions, this can be factored for validating all tests.
        /// We need to add more tests in a generic way.
        /// </summary>
        public static IEnumerable<object[]> ValidInput
        {
            get
            {
                //Input in the below format (Words to be converted, expectedNumber,testDescription)

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
