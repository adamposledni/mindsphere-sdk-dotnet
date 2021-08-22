using MindSphereSdk.Core.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// General SDK client
    /// </summary>
    public abstract class SdkClient
    {
        private readonly MindSphereConnector _mindSphereConnector;

        public SdkClient(Credentials credentials, ClientConfiguration configuration, HttpClient httpClient)
        {
            if (credentials != null)
            {
                _mindSphereConnector = credentials.GetConnector(configuration, httpClient);
            }
            else
            {
                throw new ArgumentNullException(nameof(credentials));
            }
        }

        /// <summary>
        /// Send HTTP request to the MindSphere API
        /// </summary>
        protected async Task<string> HttpActionAsync(HttpMethod method, string specUri, HttpContent body = null, List<KeyValuePair<string, string>> headers = null)
        {
            string response = await _mindSphereConnector.HttpActionAsync(method, specUri, body, headers);
            return response;
        }
    }
}
