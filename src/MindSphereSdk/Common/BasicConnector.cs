using MindSphereSdk.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Common
{
    /// <summary>
    /// Connector to the MindSphere API using AppCredentials
    /// </summary>
    public class BasicConnector : IMindSphereConnector
    {
        private HttpClient _httpClient;
        
        private AccessToken _accessToken;

        private AppCredentials _credentials;

        private string _region = "eu1";
        private string _domain = "mindsphere.io";

        public BasicConnector(AppCredentials credentials, HttpClient httpClient)
        {           
            _credentials = credentials;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Sending HTTP request to the MindSphere API
        /// </summary>
        public async Task<string> HttpActionAsync(HttpMethod method, string specUri, HttpContent body = null, List<KeyValuePair<string, string>> headers = null)
        {
            // always try to validate / renew token
            await RenewTokenAsync();

            // prepare HTTP request
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = method;
            request.RequestUri = GetFullUri(specUri);
            request.Headers.Add("Authorization", "Bearer " + _accessToken.Token);

            // headers from parametr
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            request.Content = body;

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();                      
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        /// <summary>
        /// Acquire MindSphere access token with AppCredentials
        /// </summary>
        public async Task AcquireTokenAsync()
        {
            // prepare HTTP request
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = GetFullUri("/api/technicaltokenmanager/v3/oauth/token");
            // X-SPACE-AUTH-KEY is needed
            request.Headers.Add("X-SPACE-AUTH-KEY", GetBasicAuth());
            request.Content = new StringContent(JsonConvert.SerializeObject(_credentials), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            _accessToken = JsonConvert.DeserializeObject<AccessToken>(responseBody);
        }

        /// <summary>
        /// Renew MindSphere access token
        /// </summary>
        public async Task RenewTokenAsync()
        {
            if (_accessToken != null)
            {
                bool tokenValid = ValidateToken();
                if (!tokenValid)
                {
                    _accessToken = null;
                }
            }

            if (_accessToken == null)
            {
                await AcquireTokenAsync();
                bool tokenValid = ValidateToken();
                if (!tokenValid)
                {
                    throw new InvalidOperationException("Error in aquiering new token");
                }
            }
        }

        // TODO: implement token validation (https://developer.mindsphere.io/concepts/concept-authentication.html#token-validation)
        /// <summary>
        /// Validate MindSphere access token 
        /// </summary>
        public bool ValidateToken()
        {
            if (_accessToken == null) return false;

            double minutesSkew = 5.0;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(_accessToken.Token);

            string expString = token.Claims.First(claim => claim.Type == "exp").Value;
            DateTime exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expString)).LocalDateTime;
            // if exp is in the past (with minutes skew)
            if (DateTime.Now.AddMinutes(minutesSkew) >= exp) return false;

            string iatString = token.Claims.First(claim => claim.Type == "iat").Value;
            DateTime iat = DateTimeOffset.FromUnixTimeSeconds(long.Parse(iatString)).LocalDateTime;
            // if iat is in the future (with minutes skew)
            if (DateTime.Now.AddMinutes(minutesSkew) <= iat) return false;

            return true;            
        }

        /// <summary>
        /// Generate full URI
        /// </summary>
        private Uri GetFullUri(string specUri)
        {
            string basePart = $"https://gateway.{_region}.{_domain}";
            return new Uri(basePart + specUri);
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
