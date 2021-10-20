using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Word2NumberConverter.Utility
{
    public sealed class RequestUtility
    {
        /// <summary>
        /// Fetch input sent in the request body
        /// </summary>
        /// <param name="request">Http request containing input data</param>
        /// <returns>Value of dynamic type after deserialization</returns>
        public static async Task<dynamic> GetTextFromRequestAsync(HttpRequest request)
        {
            string requestBody = new StreamReader(request.Body).ReadToEnd();
            return requestBody;
        }
    }
}
