using MindSphereLibrary.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereLibrary.Common
{
    public abstract class SdkClient
    {
        // https://gateway.{region}.{mindsphere-domain}/api/iottsaggregates/v3/aggregates/{assetId}/{aspectName}
        // https://gateway.eu1.mindsphere.io/api/assetmanagement/v3/assets

        private AppCredentials _credentials;
        private AccessToken _accessToken;
        private HttpClient httpClient;

        public SdkClient(AppCredentials credentials, HttpClient _httpClient)
        {
            _credentials = credentials;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            httpClient = _httpClient;
        }

        protected async Task<string> HttpGetRequestAsync(string specUri)
        {
            // tmp, it should validate token
            await AcquireTokenAsync();

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = GetFullUri("/api/assetmanagement/v3/assets");
            request.Headers.Add("Authorization", "Bearer " + _accessToken.Token);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody.Substring(0, 20);
        }

        private async Task AcquireTokenAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = GetFullUri("/api/technicaltokenmanager/v3/oauth/token");
            request.Headers.Add("X-SPACE-AUTH-KEY", GetBasicAuth());
            request.Content = new StringContent(JsonConvert.SerializeObject(_credentials), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            _accessToken = JsonConvert.DeserializeObject<AccessToken>(responseBody);
        }

        private Uri GetFullUri(string specUri)
        {
            string basePart = $"https://gateway.{_credentials.Region}.{_credentials.MindSphereDomain}";
            return new Uri(basePart + specUri);
        }

        private string GetBasicAuth()
        {
            string creds = _credentials.KeyStoreClientId + ":" + _credentials.KeyStoreClientSecret;
            byte[] bytes = Encoding.UTF8.GetBytes(creds);
            return ("Basic " + Convert.ToBase64String(bytes));
        }
    }
}
