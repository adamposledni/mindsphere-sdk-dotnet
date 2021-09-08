using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace MindSphereSdk.Core.Test
{
    public class ClientConfigurationTest
    {
        /// <summary>
        /// ClientConfiguration values validation.
        /// </summary>
        [Fact]
        public void CreateWithInvalidData()
        {
            // Arrange
            var creds = new UserCredentials("fake_token");
            var config = new ClientConfiguration(timeout: 0);
            // Act
            Action act = () => new MindSphereApiSdk(creds, config);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        /// <summary>
        /// ClientConfiguration values validation.
        /// </summary>
        [Fact]
        public void CreateWithValidData()
        {
            // Arrange
            var creds = new UserCredentials("fake_token");
            var config = new ClientConfiguration();
            // Act
            _ = new MindSphereApiSdk(creds, config);

            // Assert
        }
    }
}
