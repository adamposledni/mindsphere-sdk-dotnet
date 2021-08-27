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
    public class SdkTest
    {
        private readonly AppCredentials _invalidAppCreds = new AppCredentials(
                "tenant-app-1.0.0",
                "qzfKw5TYL085a2SCV0vtWaPkSMM0zSBLGh5XZPNQr37",
                "app",
                "1.0.0",
                "tenant",
                "tenant");

        private readonly string _validAccessToken = "";
        private readonly string _invalidAccessToken = "";

        /// <summary>
        /// Negative test: SDK constructor - null credentials validation
        /// </summary>
        [Fact]
        public void ConstructWithNullCredentials()
        {
            // Act
            static MindSphereApiSdk act() => new MindSphereApiSdk(null, new ClientConfiguration());

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        /// <summary>
        /// Negative test: SDK constructor - null configuration validation
        /// </summary>
        [Fact]
        public void ConstructWithNullConfiguration()
        {
            // Arrange

            // Act
            MindSphereApiSdk act() => new MindSphereApiSdk(_invalidAppCreds, null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        /// <summary>
        /// Negative test: Mdsp API call with invalid AppCredentials
        /// </summary>
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

        /// <summary>
        /// Positive test: Mdsp API call with valid AppCredentials
        /// </summary>
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

        /// <summary>
        /// Negative test: Mdsp API call with invalid UserCredentials
        /// </summary>
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

        /// <summary>
        /// Positive test: Mdsp API call with valid UserCredentials
        /// </summary>
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
