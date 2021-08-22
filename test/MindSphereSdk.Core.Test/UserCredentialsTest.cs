using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;
using System.Net.Http;
using Xunit;

namespace MindSphereSdk.Core.Test
{
    public class UserCredentialsTest
    {
        [Fact]
        public void UseWithInvalidData()
        {
            // Arrange
            var credentials = new UserCredentials("");

            // Act
            Func<AssetManagementClient> act = () => new AssetManagementClient(credentials, new ClientConfiguration(), new HttpClient());

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void RemoveBearerPrefix()
        {
            // Arrange
            var credentials = new UserCredentials("Bearer asd");

            // Act

            // Assert
            Assert.True(credentials.Token == "asd");
        }

        [Fact]
        public void UseWithValidData()
        {
            // Arrange
            var credentials = new UserCredentials("asd");

            // Act
            new AssetManagementClient(credentials, new ClientConfiguration(), new HttpClient());

            // Assert
        }
    }
}
