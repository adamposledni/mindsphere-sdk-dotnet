using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MindSphereSdk.Core.Test
{
    public class AppMindSphereConnectorTest
    {
        [Fact]
        public void ConstructWithNullHttpClient()
        {
            // Arrange
            var appCreds = new AppCredentials(
                "tenant-app-1.0.0", 
                "qzfKw5TYL085a2SCV0vtWaPkSMM0zSBLGh5XZPNQr37", 
                "app", 
                "1.0.0", 
                "tenant", 
                "tenant");

            // Act
            Func<AppMindSphereConnector> act = () => new AppMindSphereConnector(appCreds, null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ConstructWithNullAppCredentials()
        {
            // Arrange
            var httpClient = new HttpClient();

            // Act
            Func<AppMindSphereConnector> act = () => new AppMindSphereConnector(null, httpClient);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task AcquireTokenWithInvalidCredentials()
        {
            // Arrange
            var httpClient = new HttpClient();
            var appCreds = new AppCredentials(
                "tenant-app-1.0.0",
                "qzfKw5TYL085a2SCV0vtWaPkSMM0zSBLGh5XZPNQr37",
                "app",
                "1.0.0",
                "tenant",
                "tenant");

            var connector = new AppMindSphereConnector(appCreds, httpClient);

            // Act
            Func<Task> act = () => { return connector.AcquireTokenAsync(); };

            // Assert
            await Assert.ThrowsAsync<MindSphereApiException>(act);
        }

        [Fact]
        public async Task AcquireTokenWithValidCredentials()
        {
            // Arrange
            var httpClient = new HttpClient();
            var appCreds = AppCredentials.FromJsonFile(@"..\..\..\..\..\mdspcreds.json");

            var connector = new AppMindSphereConnector(appCreds, httpClient);

            // Act
            await connector.AcquireTokenAsync();

            // Assert
            Assert.True(connector.ValidateToken());
        }
    }
}
