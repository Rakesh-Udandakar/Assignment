using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeQuiz;
using System;

namespace TrappedRainWaterCalculatorTests
{
    /// <summary>
    /// Class to unit test different input scenarios for TrappedRainWaterCalculator class in HomeQuiz namespace
    /// </summary>
    [TestClass]
    public class TrappedRainWaterCalculatorTests
    {
        /// <summary>
        /// Test for Null argument the ArgumentNullException is thrown.
        /// </summary>
        /// <param name="elevationMaps">Array of elevation maps.</param>
        /// <param name="expectedExceptionMessage">Expected exception message.</param>
        [TestMethod]
        //DataRow(elevationMaps, expectedExceptionMessage, parameterName)
        [DataRow(null, "Value cannot be null.", "elevationMap")]
        public void TrappedRainWaterCalculator_NullInput_ArgumentNullExceptionThrown(int[] elevationMaps, string expectedExceptionMessage, string parameterName)
        {
            //Action
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => TrappedRainWaterCalculator.CalculateTrappedRainWaterUnits(elevationMaps));

            //Assert
            Assert.IsTrue(exception.ParamName.Equals(parameterName));
            Assert.IsTrue(exception.Message.Contains(expectedExceptionMessage));
        }

        /// <summary>
        /// Test valid input's for calculating the trapped rain water.
        /// </summary>
        /// <param name="elevationMaps">Array of elevation maps.</param>
        /// <param name="resultExpected">Expected Count of water units trapped between the elevations.</param>
        /// <param name="message">The message to include in the exception when actual is not equal to expected.</param>
        [TestMethod]
        //DataRow(elevationMaps, resultExpected, message)
        [DataRow(new int[] { 2, 0, 2 }, 2, "Two units of rain water should be trapped.")]
        [DataRow(new int[] { 3, 0, 2, 0, 4 }, 7, "Seven units of rain water should be trapped.")]
        [DataRow(new int[] { }, 0, "Input array with no/empty elevations will not trap(0)any waterunits.")]
        [DataRow(new int[] { 1 }, 0, "Only one elevation will not trap(0) any waterunits.")]
        [DataRow(new int[] { 2 }, 0, "Only two elevation's will not trap(0) any waterunits.")]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 }, 0, "Increasing elevations will not trap(0) any waterunits.")]
        [DataRow(new int[] { 6, 5, 4, 3, 2, 1 }, 0, "Decreasing elevations will not trap(0) any waterunits.")]
        [DataRow(new int[] { 1, 1, 1, 1, 1, 1 }, 0, "All elevations having equal height will not trap(0) any rain water.")]
        [DataRow(new int[] { 1, 2, 3, 2, 1 }, 0, "Elevations with mountain structure will not trap(0) any rain water.")]
        [DataRow(new int[] { 1, 0, 2, 0, 3 }, 3, "Three units of rain water expected.")]
        public void TrappedRainWaterCalculator_ValidInput_Success(int[] elevationMaps, int resultExpected, string message)
        {
            //Action
           var result = TrappedRainWaterCalculator.CalculateTrappedRainWaterUnits(elevationMaps);

            //Assert
            Assert.AreEqual(resultExpected, result, message);
        }

        /// <summary>
        /// Test for Input containing negative elevation throws the ArgumentException.
        /// </summary>
        /// <param name="elevationMaps">Array of elevation maps.</param>
        /// <param name="expectedExceptionMessage">Expected exception message.</param>
        /// <param name="testDescription"></param>
        [TestMethod]
        //DataRow(elevationMaps, expectedExceptionMessage, testDescription)
        [DataRow(
            new int[] { 1, 0, -2, 0, 3 }, 
            "All the elevation values of elevationMap parameter should be non-negative integers.", 
            "Input with negative numbers throws ArgumentException."
            )]
        public void TrappedRainWaterCalculator_NegativeInput_ArgumentExceptionThrown(int[] elevationMaps, string expectedExceptionMessage, string testDescription)
        {
            //Action
            ArgumentException exception = Assert.ThrowsException<ArgumentException>(() => TrappedRainWaterCalculator.CalculateTrappedRainWaterUnits(elevationMaps));

            //Assert
            Assert.AreEqual(expectedExceptionMessage, exception.Message, testDescription);
        }
    }
}
