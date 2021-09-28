using MindSphereSdk.Core.Connectors;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// General SDK client.
    /// </summary>
    public abstract class SdkClient
    {
        private readonly MindSphereConnector _mindSphereConnector;

        internal SdkClient(MindSphereConnector mindSphereConnector)
        {
            _mindSphereConnector = mindSphereConnector;
        }

        /// <summary>
        /// Send HTTP request to the MindSphere API.
        /// </summary>
        protected async Task<string> HttpActionAsync(HttpMethod method, string specUri, HttpContent body = null, List<KeyValuePair<string, string>> headers = null)
        {
            string response = await _mindSphereConnector.HttpActionAsync(method, specUri, body, headers);
            return response;
        }
    }
}
