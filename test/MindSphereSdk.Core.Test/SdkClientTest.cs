using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
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

        private readonly string _validAccessToken = "eyJhbGciOiJSUzI1NiIsImprdSI6Imh0dHBzOi8vaWlvdGRnbGkucGlhbS5ldTEubWluZHNwaGVyZS5pby90b2tlbl9rZXlzIiwia2lkIjoia2V5LWlkLTMiLCJ0eXAiOiJKV1QifQ.eyJqdGkiOiIyZDEwMWIwZWE5NDY0OGE5OTk2MTczNjdiMzdjYzQwMyIsInN1YiI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYwIiwic2NvcGUiOlsibWRzcDpjb3JlOkFkbWluM3JkUGFydHlUZWNoVXNlciJdLCJjbGllbnRfaWQiOiJpaW90ZGdsaS10ZXN0YXBwbGljYXRpb24tMS4wLjA2MCIsImNpZCI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYwIiwiYXpwIjoiaWlvdGRnbGktdGVzdGFwcGxpY2F0aW9uLTEuMC4wNjAiLCJncmFudF90eXBlIjoiY2xpZW50X2NyZWRlbnRpYWxzIiwicmV2X3NpZyI6IjY3MTFhNzBiIiwiaWF0IjoxNjI5ODMyMjE2LCJleHAiOjE2Mjk4MzQwMTYsImlzcyI6Imh0dHBzOi8vaWlvdGRnbGkucGlhbS5ldTEubWluZHNwaGVyZS5pby9vYXV0aC90b2tlbiIsInppZCI6Imlpb3RkZ2xpIiwiYXVkIjpbImlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYwIl0sInRlbiI6Imlpb3RkZ2xpIiwic2NoZW1hcyI6WyJ1cm46c2llbWVuczptaW5kc3BoZXJlOmlhbTp2MSJdLCJjYXQiOiJjbGllbnQtdG9rZW46djEifQ.TnL518EXUKPZrfgvi4EyCibT9yCieRJ7O6VFg7ufiO5kDv-l1bYxbqOefmjrT7Ev96PWdcQHsvv6sCHDwBSDSbob79k36KN0C3Eass3saq1T6FA5nzx8sopwsxtJxMEL1ekuPz9idc_PugmaE1SrL3n_P4UzjxtYYmedWAm7UYYphEFsf9I6NJuz7QB90vDemAWzcpHv4-9kZbCp7EHYBpLRB_vgRutoeQahwrloJF0v2RKp2YcVxun4cQP2rxqbO4mwqzoS7ijlm4Sgt-quvje51--Gum0btm1IowDsr40mBUw6dRj63g4xnd7u5itqW_YwuXvwnDDmwzBBcH_CZQ";
        private readonly string _invalidAccessToken = "eyJhbGciOiJSUzI1NiIsImprdSI6Imh0dHbzOi8vaWlvdGRnbGkucGlhbS5ldTEubWluZHNwaGVyZS5pby90b2tlbl9rZXlzIiwia2lkIjoia2V5LWlkLTMiLCJ0eXAiOiJKV1QifQ.eyJqdGkiOiIyZDEwMWIwZWE5NDY0OGE5OTk2MTczNjdiMzdjYzQwMyIsInN1YiI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYwIiwic2NvcGUiOlsibWRzcDpjb3JlOkFkbWluM3JkUGFydHlUZWNoVXNlciJdLCJjbGllbnRfaWQiOiJpaW90ZGdsaS10ZXN0YXBwbGljYXRpb24tMS4wLjA2MCIsImNpZCI6Imlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYwIiwiYXpwIjoiaWlvdGRnbGktdGVzdGFwcGxpY2F0aW9uLTEuMC4wNjAiLCJncmFudF90eXBlIjoiY2xpZW50X2NyZWRlbnRpYWxzIiwicmV2X3NpZyI6IjY3MTFhNzBiIiwiaWF0IjoxNjI5ODMyMjE2LCJleHAiOjE2Mjk4MzQwMTYsImlzcyI6Imh0dHBzOi8vaWlvdGRnbGkucGlhbS5ldTEubWluZHNwaGVyZS5pby9vYXV0aC90b2tlbiIsInppZCI6Imlpb3RkZ2xpIiwiYXVkIjpbImlpb3RkZ2xpLXRlc3RhcHBsaWNhdGlvbi0xLjAuMDYwIl0sInRlbiI6Imlpb3RkZ2xpIiwic2NoZW1hcyI6WyJ1cm46c2llbWVuczptaW5kc3BoZXJlOmlhbTp2MSJdLCJjYXQiOiJjbGllbnQtdG9rZW46djEifQ.TnL518EXUKPZrfgvi4EyCibT9yCieRJ7O6VFg7ufiO5kDv-l1bYxbqOefmjrT7Ev96PWdcQHsvv6sCHDwBSDSbob79k36KN0C3Eass3saq1T6FA5nzx8sopwsxtJxMEL1ekuPz9idc_PugmaE1SrL3n_P4UzjxtYYmedWAm7UYYphEFsf9I6NJuz7QB90vDemAWzcpHv4-9kZbCp7EHYBpLRB_vgRutoeQahwrloJF0v2RKp2YcVxun4cQP2rxqbO4mwqzoS7ijlm4Sgt-quvje51--Gum0btm1IowDsr40mBUw6dRj63g4xnd7u5itqW_YwuXvwnDDmwzBBcH_CZQ";

        [Fact]
        public void ConstructWithNullCredentials()
        {
            // Act
            Func<MindSphereApiSdk> act = () => new MindSphereApiSdk(null, new ClientConfiguration());

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ConstructWithNullConfiguration()
        {
            // Arrange

            // Act
            Func<MindSphereApiSdk> act = () => new MindSphereApiSdk(_invalidAppCreds, null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task MdspCallWithInvalidAppCreds()
        {
            // Arrange
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(_invalidAppCreds, config);
            var client = sdk.GetAssetManagementClient();

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
            var appCreds = AppCredentials.FromJsonFile("mdspcreds.json");
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(appCreds, config);
            var client = sdk.GetAssetManagementClient();
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
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(userCreds, config);
            var client = sdk.GetAssetManagementClient();
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
            var userCreds = new UserCredentials(_validAccessToken);
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(userCreds, config);
            var client = sdk.GetAssetManagementClient();
            var request = new ListAssetsRequest();

            // Act
            await client.ListAssetsAsync(request);

            // Assert
        }
    }
}
