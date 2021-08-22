using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MindSphereSdk.Core.Test
{
    public class SdkClientTest
    {
        private readonly AppCredentials _invalidAppCreds = new AppCredentials(
                "tenant-app-1.0.0",
                "qzfKw5TYL085a2SCV0vtWaPkSMM0zSBLGh5XZPNQr37",
                "app",
                "1.0.0",
                "tenant",
                "tenant");

        private readonly string _validAccessToken = "eyJhbGciOiJSUzI1NiIsImprdSI6Imh0dHBzOi8vaWlvdGRnbGkucGlhbS5ldTEubWluZHNwaGVyZS5pby90b2tlbl9rZXlzIiwia2lkIjoia2V5LWlkLTMiLCJ0eXAiOiJKV1QifQ.eyJqdGkiOiIyNDkwMjI5N2NlZWQ0ZjZlODg1MzkzYmY0MjZlNzBiMSIsInN1YiI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYyIiwic2NvcGUiOlsibWRzcDpjb3JlOkFkbWluM3JkUGFydHlUZWNoVXNlciJdLCJjbGllbnRfaWQiOiJpaW90ZGdsaS10ZXN0YXBwbGljYXRpb24tMS4wLjA2MiIsImNpZCI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYyIiwiYXpwIjoiaWlvdGRnbGktdGVzdGFwcGxpY2F0aW9uLTEuMC4wNjIiLCJncmFudF90eXBlIjoiY2xpZW50X2NyZWRlbnRpYWxzIiwicmV2X3NpZyI6IjUzODZmNjgiLCJpYXQiOjE2Mjk2MzMxNTMsImV4cCI6MTYyOTYzNDk1MywiaXNzIjoiaHR0cHM6Ly9paW90ZGdsaS5waWFtLmV1MS5taW5kc3BoZXJlLmlvL29hdXRoL3Rva2VuIiwiemlkIjoiaWlvdGRnbGkiLCJhdWQiOlsiaWlvdGRnbGktdGVzdGFwcGxpY2F0aW9uLTEuMC4wNjIiXSwidGVuIjoiaWlvdGRnbGkiLCJzY2hlbWFzIjpbInVybjpzaWVtZW5zOm1pbmRzcGhlcmU6aWFtOnYxIl0sImNhdCI6ImNsaWVudC10b2tlbjp2MSJ9.MnFRAgWwmEEnhe32MErq6x1NdnRQ0dk3TrfwZm_hngPaxcuWm7PfpYNnyKOofIPzBsXBUVoc0gt2YX3EeN2Rwv7ff6OdfMQh5j9zzEFDZCFTEnl0WrOK6WiqfN2L9CmfezSUJFHtYpgwhe1kxOdr6FXsZoDPwNFymji5JIDPa3q1meynyZ1lahc-qIMwfVCIJbpyQPMLuVheLnus7G9-kJ9WqF9T3rVSXZemFmAtY2tKTYwlY0VQn_o4EWq6_wDwck7okcIW4P2djoLKEYSejbYihiybnCC34-M3dFCSOlL-GqX2azQtTuaT3ehwCxV-Tyg0JmIsOV5Y_IruOjogxw";
        private readonly string _invalidAccessToken = "eyJhbGciOiJSUzI1NiIsImprdSI6Imh0dHByOi8vaWlvdGRnbGkucGlhbS5ldTEubWluZHNwaGVyZS5pby90b2tlbl9rZXlzIiwia2lkIjoia2V5LWlkLTMiLCJ0eXAiOiJKV1QifQ.eyJqdGkiOiIyNDkwMjI5N2NlZWQ0ZjZlODg1MzkzYmY0MjZlNzBiMSIsInN1YiI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYyIiwic2NvcGUiOlsibWRzcDpjb3JlOkFkbWluM3JkUGFydHlUZWNoVXNlciJdLCJjbGllbnRfaWQiOiJpaW90ZGdsaS10ZXN0YXBwbGljYXRpb24tMS4wLjA2MiIsImNpZCI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYyIiwiYXpwIjoiaWlvdGRnbGktdGVzdGFwcGxpY2F0aW9uLTEuMC4wNjIiLCJncmFudF90eXBlIjoiY2xpZW50X2NyZWRlbnRpYWxzIiwicmV2X3NpZyI6IjUzODZmNjgiLCJpYXQiOjE2Mjk2MzMxNTMsImV4cCI6MTYyOTYzNDk1MywiaXNzIjoiaHR0cHM6Ly9paW90ZGdsaS5waWFtLmV1MS5taW5kc3BoZXJlLmlvL29hdXRoL3Rva2VuIiwiemlkIjoiaWlvdGRnbGkiLCJhdWQiOlsiaWlvdGRnbGktdGVzdGFwcGxpY2F0aW9uLTEuMC4wNjIiXSwidGVuIjoiaWlvdGRnbGkiLCJzY2hlbWFzIjpbInVybjpzaWVtZW5zOm1pbmRzcGhlcmU6aWFtOnYxIl0sImNhdCI6ImNsaWVudC10b2tlbjp2MSJ9.MnFRAgWwmEEnhe32MErq6x1NdnRQ0dk3TrfwZm_hngPaxcuWm7PfpYNnyKOofIPzBsXBUVoc0gt2YX3EeN2Rwv7ff6OdfMQh5j9zzEFDZCFTEnl0WrOK6WiqfN2L9CmfezSUJFHtYpgwhe1kxOdr6FXsZoDPwNFymji5JIDPa3q1meynyZ1lahc-qIMwfVCIJbpyQPMLuVheLnus7G9-kJ9WqF9T3rVSXZemFmAtY2tKTYwlY0VQn_o4EWq6_wDwck7okcIW4P2djoLKEYSejbYihiybnCC34-M3dFCSOlL-GqX2azQtTuaT3ehwCxV-Tyg0JmIsOV5Y_IruOjogxw";

        [Fact]
        public void ConstructWithNullCredentials()
        {
            // Act
            Func<AssetManagementClient> act = () => new AssetManagementClient(null, new ClientConfiguration(), new HttpClient());

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ConstructWithNullConfiguration()
        {
            // Arrange

            // Act
            Func<AssetManagementClient> act = () => new AssetManagementClient(_invalidAppCreds, null, new HttpClient());

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ConstructWithNullHttpClient()
        {
            // Arrange
            var config = new ClientConfiguration();

            // Act
            Func<AssetManagementClient> act = () => new AssetManagementClient(_invalidAppCreds, config, null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task MdspCallWithInvalidAppCreds()
        {
            // Arrange
            var httpClient = new HttpClient();
            var config = new ClientConfiguration();
            var client = new AssetManagementClient(_invalidAppCreds, config, httpClient);
            var request = new ListAssetsRequest();

            // Act
            try
            {
                await client.ListAssetsAsync(request);
            }
            // Assert
            catch (MindSphereApiException ex)
            {
                Assert.True(ex.StatusCode == 401);  
            }
        }

        [Fact]
        public async Task MdspCallWithValidAppCreds()
        {
            // Arrange
            var httpClient = new HttpClient();
            var appCreds = AppCredentials.FromJsonFile("mdspcreds.json");
            var config = new ClientConfiguration();
            var client = new AssetManagementClient(appCreds, config, httpClient);
            var request = new ListAssetsRequest();

            // Act
            await client.ListAssetsAsync(request);

            // Assert
        }

        [Fact]
        public async Task MdspCallWithInvalidUserCreds()
        {
            // Arrange
            var userCreds = new UserCredentials(_invalidAccessToken);
            var httpClient = new HttpClient();
            var config = new ClientConfiguration();
            var client = new AssetManagementClient(userCreds, config, httpClient);
            var request = new ListAssetsRequest();

            // Act
            try
            {
                await client.ListAssetsAsync(request);
            }
            // Assert
            catch (MindSphereApiException ex)
            {
                Assert.True(ex.StatusCode == 401);
            }
        }

        [Fact]
        public async Task MdspCallWithValidUserCreds()
        {
            // Arrange
            var httpClient = new HttpClient();
            var appCreds = new UserCredentials(_validAccessToken);
            var config = new ClientConfiguration();
            var client = new AssetManagementClient(appCreds, config, httpClient);
            var request = new ListAssetsRequest();

            // Act
            await client.ListAssetsAsync(request);

            // Assert
        }
    }
}
