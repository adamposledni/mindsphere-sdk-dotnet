using MindSphereSdk.Core.AssetManagement;
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
        public void SuccessfullyConstruct()
        {
            // Arrange
            var config = new ClientConfiguration();

            // Act
            new AssetManagementClient(_invalidAppCreds, config, new HttpClient());

            // Assert
            // not needed checking in it does not throws exception
        }

        [Fact]
        public async Task MindSphereCallWithInvalidCredentials()
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
        public async Task MindSphereCallWithValidCredentials()
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
            // not needed checking in it does not throws exception
        }
    }
}
