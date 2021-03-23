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
        private string _token = "eyJhbGciOiJSUzI1NiIsImprdSI6Imh0dHBzOi8vdmR0Y3oucGlhbS5ldTEubWluZHNwaGVyZS5pby90b2tlbl9rZXlzIiwia2lkIjoia2V5LWlkLTMiLCJ0eXAiOiJKV1QifQ.eyJqdGkiOiJkMjA3NDJlNzU3MTk0Mzc4OTU2ZjgxZjFhNmE5ZDdmZiIsInN1YiI6InZkdGN6LWNvbW9zaW50ZWdyYXRpb24tMS4wLjAyNyIsInNjb3BlIjpbIm1kc3A6Y29yZTpBZG1pbjNyZFBhcnR5VGVjaFVzZXIiXSwiY2xpZW50X2lkIjoidmR0Y3otY29tb3NpbnRlZ3JhdGlvbi0xLjAuMDI3IiwiY2lkIjoidmR0Y3otY29tb3NpbnRlZ3JhdGlvbi0xLjAuMDI3IiwiYXpwIjoidmR0Y3otY29tb3NpbnRlZ3JhdGlvbi0xLjAuMDI3IiwiZ3JhbnRfdHlwZSI6ImNsaWVudF9jcmVkZW50aWFscyIsInJldl9zaWciOiJhMmI2NDJmNyIsImlhdCI6MTYxNjUyMzA0NiwiZXhwIjoxNjE2NTI0ODQ2LCJpc3MiOiJodHRwczovL3ZkdGN6LnBpYW0uZXUxLm1pbmRzcGhlcmUuaW8vb2F1dGgvdG9rZW4iLCJ6aWQiOiJ2ZHRjeiIsImF1ZCI6WyJ2ZHRjei1jb21vc2ludGVncmF0aW9uLTEuMC4wMjciXSwidGVuIjoidmR0Y3oiLCJzY2hlbWFzIjpbInVybjpzaWVtZW5zOm1pbmRzcGhlcmU6aWFtOnYxIl0sImNhdCI6ImNsaWVudC10b2tlbjp2MSJ9.XsfxQd0ECJ7XoOnN9czVX2qY12n2BQga1dBWfKnmNaTQNE4pzwsNEWTK6ejCd7Su1qfzcxWE4d_LVP02B4q0W4TE9uKEAzuagnKMU5Rr8YY3Ii7o_vPHzRGWjIB7MDSkorugIqR1pduopjH1gccEH_oUPQiiN9c-4Bjh5-JNPPkPxJzasYZTiDu-FzPtpixqixIo1UvVNR4O5b8QnxmVoD8MtwTvTWDtRDso8K4FUAgDCFGNItFAynOWmuxlN_sqWmt4nN_h7g-3Cx404o6I5r626zlZSePdZg7kzEFKPOMh9aj7DnfZI4N6U_KrW8Jxf_cEQ2QX_chyHHel0nTprg";

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

            string uri = "https://gateway.eu1.mindsphere.io/api/assetmanagement/v3/assets";

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(uri);
            request.Headers.Add("Authorization", "Bearer " + _accessToken.Token);

            string responseBody;
            try
            {
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseBody.Substring(0, 20);
        }

        private async Task AcquireTokenAsync()
        {

            string uri = "https://gateway.eu1.mindsphere.io/api/technicaltokenmanager/v3/oauth/token";

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(uri);
            request.Headers.Add("X-SPACE-AUTH-KEY", "Basic dmR0Y3otY29tb3NpbnRlZ3JhdGlvbi0xLjAuMDo0ekswbDlHTWhTemM3Nzd3OXdIYTFBU2dBU09MTW12VGluOVQ0T0k5VDJr");
            request.Content = new StringContent(JsonConvert.SerializeObject(_credentials), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                string test = responseBody.Substring(0, 20);
                // Above three lines can be replaced with new helper method below
                //string responseBody = await client.GetStringAsync("https://gateway.eu1.mindsphere.io/api/assetmanagement/v3/assets");
                _accessToken = JsonConvert.DeserializeObject<AccessToken>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetFullUri(string specUri)
        {
            string basePart = $"https://gateway.{_credentials.Region}.{_credentials.MindSphereDomain}";
            return basePart + specUri;
        }

        private string GetBasicAuth()
        {
            string creds = _credentials.KeyStoreClientId + ":" + _credentials.KeyStoreClientSecret;
            byte[] bytes = Encoding.UTF8.GetBytes(creds);
            return ("Basic " + Convert.ToBase64String(bytes));
        }
    }
}
