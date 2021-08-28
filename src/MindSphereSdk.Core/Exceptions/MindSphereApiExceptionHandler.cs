using System.Net.Http;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Exceptions
{
    /// <summary>
    /// MindSphere API exception handler
    /// </summary>
    internal static class MindSphereApiExceptionHandler
    {
        /// <summary>
        /// Handle unsuccessful response
        /// </summary>
        internal static async Task HandleUnsuccessfulResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                int statusCode = (int)response.StatusCode;
                string message = await response.Content.ReadAsStringAsync();

                throw new MindSphereApiException(
                    statusCode,
                    $"HTTP call to the MindSphere failed with status code: {statusCode} and message: {message}"
                );
            }
        }
    }
}
