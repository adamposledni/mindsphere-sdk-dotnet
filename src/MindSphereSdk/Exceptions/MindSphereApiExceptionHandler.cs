using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Exceptions
{
    public class MindSphereApiExceptionHandler
    {
        public static async Task HandleUnsuccessfulResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                int statusCode = (int)response.StatusCode;
                string message = await response.Content.ReadAsStringAsync();

                throw new MindSphereApiException($"{statusCode}: {message}");
            }
        }
    }
}
