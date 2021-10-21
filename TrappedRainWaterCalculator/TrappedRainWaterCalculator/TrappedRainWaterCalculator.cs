using System;
using System.Linq;

namespace HomeQuiz
{
    /// <summary>
    /// Class to calculate trapped rainWater units between the elevations after raining.
    /// </summary>
    public class TrappedRainWaterCalculator
    {
        /// <summary>
        /// Method to find total number of water units trapped between the elevations after the rain.
        /// </summary>
        /// <param name="elevationMap">Array of non-negative integers representing an elevation map where the width of each bar is 1.</param>
        /// <returns>Total number of water units trapped after the rain.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="elevationMap"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="elevationMap"/> contains negative elevations.</exception>
        public static int CalculateTrappedRainWaterUnits(int[] elevationMap)
        {

            // Input value should not be null.
            if (elevationMap == null)
                throw new ArgumentNullException(nameof(elevationMap));

            // All the elevations are expected to be non-negative integers.
            if (elevationMap.Any(elevation => elevation < 0))
                throw new ArgumentException($"All the elevation values of {nameof(elevationMap)} parameter should be non-negative integers.");

            // Need at least 3 elevations for trapping water.
            if (elevationMap.Length <= 2)
                return 0;

            // Initialize first value on left side of elevationMap array as leftMax.
            int leftMax = elevationMap[0];

            // Initialize first value on right side of elevationMap array as rightMax.
            int rightMax = elevationMap[elevationMap.Length - 1];

            // From left side starting index will be second element,
            // As the first elevation doesn't have any thing on the left to trap the water. 
            int leftIndex = 1;

            // From right side starting index will be second element from last,
            // As the last elevation doesnt have any thing on right to trap the water. 
            int rightIndex = elevationMap.Length - 2;

            // Initial value of Trapped water units.
            int sumOfTrappedWater = 0;

            // Traverse all the elevations from leftIndex to rightIndex.
            while (leftIndex <= rightIndex)
            {
                // Update maximum elevation on the left side including the current element.
                leftMax = Math.Max(leftMax, elevationMap[leftIndex]);

                // Update maximum elevation on the right side including the current element.
                rightMax = Math.Max(rightMax, elevationMap[rightIndex]);

                // Change the traversal direction to the lower of leftMax and rightMax.
                // Water is trapped only between the minimum height among the two elevations.
                if (leftMax < rightMax)
                {
                    // Add the water units to total sum, that can be stored by current elevation and maximum
                    // elevation on left when there is higher elevation exists on the right side.
                    // Traverse to the next element on the left side.
                    sumOfTrappedWater += leftMax - elevationMap[leftIndex++];
                }
                else
                {
                    // Add the water units to total sum, that can be stored by current elevation and maximum
                    // elevation on right when there is higher or equal elevation exists on the left side.
                    // Traverse to the next element on the right side.
                    sumOfTrappedWater += rightMax - elevationMap[rightIndex--];
                }
            }

            //Return total number of water units that can be trapped,between the elevations after the rain.
            return sumOfTrappedWater;
        }
    }
}
