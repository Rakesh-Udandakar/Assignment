using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Word2NumberConverter;
using Word2NumberConverterTests.Functions.Tests;
using Xunit;

namespace Word2NumberConverterTests
{
    public class Word2NumberConverterFunctionTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Theory]
        [InlineData("one thousand", "1000")]
        [InlineData("one thousand two hundred twenty four", "1224")]
        [InlineData("one billion", "1000000000")]
        [InlineData("one million twenty thousand", "1020000")]
        public async void Http_trigger_validinput_should_return_converted_number(string wordsForConversion, string expectedNumber)
        {
            // Arrange
            var request = TestFactory.CreateHttpRequest(wordsForConversion);

            // Act
            var response = (OkObjectResult)await Word2NumberConverterFunction.Run(request, logger);

            // Assert
            Assert.Equal(BigInteger.Parse(expectedNumber), response.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("one 1234")]
        [InlineData("one sdsd")]
        public async void Http_trigger_invalidinput_should_return_BadRequest(string wordsForConversion)
        {
            // Arrange
            var request = TestFactory.CreateHttpRequest(wordsForConversion);

            // Act
            var response = (BadRequestObjectResult)await Word2NumberConverterFunction.Run(request, logger);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<string>(badRequestResult.Value);

        }

        [Fact]
        public async Task Http_trigger_should_log_messageAsync()
        {
            // Arrange
            var logger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);

            // Act
            await Word2NumberConverterFunction.Run(null, logger);
            
            // Assert
            var msg = logger.Logs[0];
            Assert.Contains("Input for Word2NumberConverter:", msg);
        }
    }

}