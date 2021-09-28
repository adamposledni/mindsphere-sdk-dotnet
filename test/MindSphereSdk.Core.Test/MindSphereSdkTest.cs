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
    /// <summary>
    /// MindSphere SDK tests.
    /// </summary>
    public class MindSphereSdkTest
    {
        private readonly AppCredentials _invalidAppCreds = new AppCredentials(
                "tenant-app-1.0.0",
                "qzfKw5TYL085a2SCV0vtWaPkSMM0zSBLGh5XZPNQr37",
                "app",
                "1.0.0",
                "tenant",
                "tenant");

        /// <summary>
        /// SDK constructor - null credentials validation.
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
        /// SDK constructor - null configuration validation.
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
        /// Mdsp API call with invalid AppCredentials.
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
        /// Mdsp API call with valid AppCredentials.
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
        /// Mdsp API call with invalid UserCredentials.
        /// </summary>
        [Fact]
        public async Task MdspCallWithInvalidUserCreds()
        {
            // Arrange
            var userCreds = new UserCredentials(await GetInvalidTokenAsync());
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
        /// Mdsp API call with valid UserCredentials.
        /// </summary>
        [Fact]
        public async Task MdspCallWithValidUserCreds()
        {
            // Arrange
            var userCreds = new UserCredentials(await GetValidTokenAsync());
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(userCreds, config);
            var client = sdk.GetAssetManagementClient();
            var request = new ListAssetsRequest();

            // Act
            await client.ListAssetsAsync(request);

            // Assert
        }

        /// <summary>
        /// Update credentials object - first call with valid token then with invalid.
        /// </summary>
        [Fact]
        public async Task UpdateCredentials()
        {
            // Arrange
            var creds = new UserCredentials(await GetValidTokenAsync());
            var newCreds = new UserCredentials(await GetInvalidTokenAsync());
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(creds, config);
            var client = sdk.GetAssetManagementClient();
            var request = new ListAssetsRequest();

            // Act
            // make sure 1st call is ok
            await client.ListAssetsAsync(request);
            sdk.UpdateCredentials(newCreds);

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
        /// Update credentials with invalid type.
        /// </summary>
        [Fact]
        public async Task UpdateCredentialsWithInvalidType()
        {
            // Arrange
            var creds = new UserCredentials(await GetValidTokenAsync());
            var newCreds = AppCredentials.FromJsonFile("mdspcreds.json");
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(creds, config);
            var client = sdk.GetAssetManagementClient();

            // Act
            Action act = () => sdk.UpdateCredentials(newCreds);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        /// <summary>
        /// Get valid access token.
        /// </summary>
        private async Task<string> GetValidTokenAsync()
        {
            var creds = AppCredentials.FromJsonFile("mdspcreds.json");
            var config = new ClientConfiguration();
            var sdk = new MindSphereApiSdk(creds, config);

            return await sdk.GetAccessTokenAsync();
        }

        /// <summary>
        /// Get invalid access token.
        /// </summary>
        private async Task<string> GetInvalidTokenAsync()
        {
            string validToken = await GetValidTokenAsync();

            StringBuilder stringBuilder = new StringBuilder(validToken);
            stringBuilder[155] = (char)((int)stringBuilder[155] - 1);

            string invalidToken = stringBuilder.ToString();

            return invalidToken;
        }
    }
}
