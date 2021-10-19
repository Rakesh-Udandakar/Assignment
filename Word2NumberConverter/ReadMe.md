# Word2NumberConverter console application
Word2NumberConverter provides the capability to convert words into number.
Currently the Word2NumberConverter allows shortscale conversions of words and application can be extended to include words to other numbering systems.

## Algorithm Used:

1) Validate input for valid characters.

2) Validate input for supported words for translation.

3) Throw exception any of the above validation fails.

4) Split input into words (like thousand,hundred) and get corresponding number ("1000" , "100") for each word.

5) Calculate final number using each word as below:
	 
     number = Convert word in string format to BigInteger (BigInteger doesnt have higher limit and hence we will run into overflow issues.)   
     We will use partialSum=0 and subTotal=0 for calculation.

		number less than < 100
		Add to partialSum

		number >= 100
		Add to partialSum = partialSum * number

		number >= 1000
	    Add to subTotal = subTotal + (partialSum * number)

6) Calculate Final Number = sign (1 or -1) * (subTotal + partialSum)

## Maximum number supported:
centillion (a number equal to 1 followed by 303 zeros)

## Logic with example.

Words higher than or equal to 100, will have to be pronounced with <100 numbers.

	Ex 1: Nine Hundred
	      Initial: partialSum =0, subTotal=0

			Nine
			partialSum = 9 
			Hundred
			partialSum  = 9 * 100.
			finalNumber = (subTotal + partialSum) = 900

     Ex 2: One thousand Nine Hundred
		  Initial: partialSum =0, subTotal=0
			
			One
			partialSum = 1
			thousand (Numbers greater than or equal to 1000, will add (total sum +  partialSum *  number)).
			subTotal = (0 + (1 * 1000))=1000, partialSum=0
			Nine
			partialSum = 9 
			Hundred
			partialSum  = 9 * 100 = 900.

After all numbers are iterated get final number = sign * (subTotal + partialSum) = 1900

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
1. cd ./Word2NumberConverter

2. dotnet run

### Test in Visual Studio

1. Open the Test Explorer
2. Run all tests
   - Alternatively, you can run the tests via the command line with `dotnet test <test dll path>`.

