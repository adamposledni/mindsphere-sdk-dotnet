using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Exceptions;
using MindSphereSdk.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// Connector to the MindSphere API using app credentials
    /// </summary>
    internal class AppMindSphereConnector : MindSphereConnector
    {
        private readonly AppCredentials _credentials;

        public AppMindSphereConnector(AppCredentials credentials, ClientConfiguration configuration)
            : base(configuration)
        {
            _credentials = credentials;
        }

        /// <summary>
        /// Acquire MindSphere access token with app credentials
        /// </summary>
        protected override async Task AcquireTokenAsync()
        {
            // prepare HTTP request
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = GetFullUri("/api/technicaltokenmanager/v3/oauth/token")
            };
            // X-SPACE-AUTH-KEY is needed
            request.Headers.Add("X-SPACE-AUTH-KEY", GetBasicAuth());
            request.Content = new StringContent(JsonConvert.SerializeObject(_credentials), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            
            // handle error response
            await MindSphereApiExceptionHandler.HandleUnsuccessfulResponseAsync(response);

            string responseBody = await response.Content.ReadAsStringAsync();
            _accessToken = JsonConvert.DeserializeObject<AccessToken>(responseBody).Token;
        }

        /// <summary>
        /// Generate BasicAuth string (Basic abc123...)
        /// </summary>
        private string GetBasicAuth()
        {
            string creds = _credentials.KeyStoreClientId + ":" + _credentials.KeyStoreClientSecret;
            byte[] bytes = Encoding.UTF8.GetBytes(creds);
            return ("Basic " + Convert.ToBase64String(bytes));
        }
    }
}
