# Online Word2NumberConverter Azure Function App
Word2NumberConverter provides the capability to convert words into number.
Currently the Word2NumberConverter allows shortscale conversions of words.Application can also be extended to include other numeric scale systems.

Url of the application: https://word2numberconverter.azurewebsites.net/api/Word2NumberConverter?code=<Function App Key is required>

Input:

In the body of the request as a text type, enter the words to be converted.

## Algorithm Used:

1) Validate input for valid characters.

2) Validate input for supported words for translation.

3) Throw exception, if any of the above validation fails.

4) Split input into words (like thousand,hundred) and get corresponding number ("1000" , "100") for each word.

5) Calculate final number using each word as below:
	 
	We will use partialSum=0 and subTotal=0 for calculation.
       
	   For each word in Input perform below steps
        
     	number = Convert word in string format to BigInteger 
	 	(BigInteger doesnt have higher limit and hence we will not run into overflow issues.) 

		number less than < 100
		Add to partialSum

		number >= 100
		Add to partialSum = partialSum * number

		number >= 1000
	    Add to subTotal = subTotal + (partialSum * number)
	    Reset partialSum = 0

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
 - [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)
 - [XUnit](https://xunit.net/)
 - [Postman](https://www.postman.com/)


## Getting started
### Prerequisites:
- Visual Studio 2019 with .net core 3.1

### Build and run in Visual Studio
1. Open the solution in Visual Studio, build
2. In the dropdown for the Run button (green arrow) in Visual Studio, change iisexpress to Word2NumberConverter
3. Press the Run button to build the solution.

### Build and run service from the command line

1. Refer to [steps](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=in-process%2Cazure-cli&pivots=programming-runtime-functions-v3) for local setup.

### Testing through postman locally

local doesn''t need a function key to be provided 
1. Url: http://localhost:7071/api/Word2NumberConverter
2. In the Body, select format as text, Enter the word required to be converted as shown below

Example: one thousand two hundred


### Run tests in Visual Studio

1. Open the Test Explorer
2. Run all tests
   - Alternatively, you can run the tests via the command line with `dotnet test <test dll path>`.

