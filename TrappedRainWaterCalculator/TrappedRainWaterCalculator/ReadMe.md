# Solution to Trapping Rain Water Problem
Provides a utility to calculate the total number of rain water units trapped between within the group of elevations

##Problem:##
Given n non-negative integers representing an elevation map where the width of each bar is 1, compute how much water it can trap after raining.
 
 Example 1:

    Input: array[] = {2, 0, 2}
	Output: 2
    We can trap 2 units of water in the middle gap

 Example 2:
    Input: array[] = {3, 0, 2, 0, 4}
    Output: 7.
    We can trap 3 units of water between 3 and 2, 1 unit on top of bar 2 and 3 units between 2 and 4

##Algorithm##

            1) Check at least there are 3 elevations for trapping water else return 0 as no water can be trapped.
            2) Initialize first value on left side of elevationMap array as leftMax.
            3) Initialize first value on right side of elevationMap array as rightMax.
            
            4) As there is no water that can be trapped for the first and last elevation.
                we will check the storage of water units between second element from left and n-2 (second from last) elevation.
                Initialize LeftIndex = second element from left.
                Initialize Rightindex = n-2 (second from last) right.
            
            5) Initial value of total Trapped water units will be SumOfTrappedWater=0.
            
            6) Traverse all the elevations from leftIndex to rightIndex.
                    Update leftMax with maximum elevation on the left side including the current element.
                            
                    Update rightMax with maximum elevation on the right side including the current element.
                
            7) Change the traversal direction to the lower of leftMax and rightMax as water is trapped only between the 
               minimum height among the two elevations.

               if (leftMax < rightMax)
                    When there is a higher elevation exists on the right side
                    Add the water units to total sum, that can be stored by current elevation and maximum elevation on left.
                    Traverse to the next element on the left side.
                    SumOfTrappedWater += leftMax - elevationMap[leftIndex++].
                else
                    When there is a higher elevation exists on the right side.
                    Add the water units to total sum, that can be stored by current elevation and maximum elevation on right.
                    Traverse to the next element on the right side.
                    sumOfTrappedWater += rightMax - elevationMap[rightIndex--].

            8) Return total number (sumOfTrappedWater) of water units that can be trapped,between the elevations after the rain.

## Technologies Used
List of technologies used for developing the solution

 - [CSharp](https://docs.microsoft.com/en-us/dotnet/csharp/)
 - [Asp.Net Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
 - [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)

## Getting started
### Prerequisites:
- Visual Studio 2019 with .net core 3.1

### Build and run in Visual Studio
1. Open the solution in Visual Studio, build
2. In the dropdown for the Run button (green arrow) in Visual Studio, change iisexpress to Word2NumberConverter
3. Press the Run button to build the solution.

### Build and run service from the command line
cd ./Word2NumberConverter
dotnet run

### Test in Visual Studio

1. Open the Test Explorer
2. Run all tests
   - Alternatively, you can run the tests via the command line with `dotnet test <test dll path>`.
