using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Numerics;
using Word2NumberConverter.Utility;

namespace Word2NumberConverter
{
    public static class Word2NumberConverterFunction
    {
        [FunctionName("Word2NumberConverter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            Guid activityId = Guid.NewGuid();

            log.LogTrace($"Activity Id: {activityId}.Input for Word2NumberConverter:", new object[] { req });

            // Expected input is of string format, throw exception in case of any other input format.
            dynamic wordsForConversion;
            try
            {
                wordsForConversion = await RequestUtility.GetTextFromRequestAsync(req);

                if (wordsForConversion != null && !(wordsForConversion is string))
                    return new BadRequestObjectResult("Words should be of type tex.");
            }
            catch (Exception ex)
            {
                log.LogError($"Activity Id: { activityId}.Exception occurred in Word2NumberConverter:", new object[] { ex });
                return new BadRequestObjectResult("Words should be of type text.");
            }

            // Check for null or empty input.
            if (string.IsNullOrWhiteSpace(wordsForConversion))
                return new BadRequestObjectResult("Words cannot be null or empty.");

            ShortScaleWord2NumberConverter shortScaleWord2NumberConverter = new ShortScaleWord2NumberConverter();

            try
            {
                // Call ConvertWordsToNumber in ShortScaleWord2NumberConverter to get the number
                BigInteger result = shortScaleWord2NumberConverter.ConvertWordsToNumber(wordsForConversion);

                return new OkObjectResult(result);
            }
            catch (ArgumentNullException ex)
            {
                log.LogTrace($"Activity Id: { activityId}.BadRequest:", new object[] { ex });
                return new BadRequestObjectResult("Words cannot be null or empty.");
            }
            catch (ArgumentException ex)
            {
                log.LogTrace($"Activity Id: { activityId}.BadRequest:", new object[] { ex });
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                // Logging error in case of any unknow exception other than the above argument exceptions. 
                log.LogError($"Activity Id: { activityId}.Exception occurred in Word2NumberConverter:", new object[] { ex });
                return new BadRequestObjectResult($"Exception occurred, please use activity id : {activityId} for troubleshooting");
            }
        }
    }
}
