using MindSphereLibrary.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereLibrary.Common
{
    public class BasicConnector : IMindSphereConnector
    {
        private HttpClient _httpClient;
        
        private AccessToken _accessToken;

        private AppCredentials _credentials;
        private string _region = "eu1";
        private string _domain = "mindsphere.io";

        public BasicConnector(AppCredentials credentials)
        {           
            _credentials = credentials;
            _httpClient = new HttpClient();
        }

        public async Task<string> HttpActionAsync(HttpMethod method, string specUri, string body = null)
        {
            await RenewTokenAsync();

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = method;
            request.RequestUri = GetFullUri(specUri);
            request.Headers.Add("Authorization", "Bearer " + _accessToken.Token);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task AcquireTokenAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = GetFullUri("/api/technicaltokenmanager/v3/oauth/token");
            request.Headers.Add("X-SPACE-AUTH-KEY", GetBasicAuth());
            request.Content = new StringContent(JsonConvert.SerializeObject(_credentials), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            _accessToken = JsonConvert.DeserializeObject<AccessToken>(responseBody);
        }


        public async Task RenewTokenAsync()
        {
            if (!ValidateToken())
            {
                await AcquireTokenAsync();
            }

        }

        public bool ValidateToken()
        {
            return false;
            
        }

        private Uri GetFullUri(string specUri)
        {
            string basePart = $"https://gateway.{_region}.{_domain}";
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
