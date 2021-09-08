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
    public class AppCredentialsTest
    {
        /// <summary>
        /// AppCredentials values validation.
        /// </summary>
        [Fact]
        public void CreateWithInvalidData()
        {
            // Arrange
            var config = new ClientConfiguration();
            var creds = new AppCredentials("  ", null, "   ", "  ", "  ", "  ");
            // Act
            Action act = () => { new MindSphereApiSdk(creds, config); };

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        /// <summary>
        /// AppCredentials values validation.
        /// </summary>
        [Fact]
        public void CreateWithValidData()
        {
            // Act
            var creds = new AppCredentials("a", "b", "c", "d", "e", "f");
            var config = new ClientConfiguration();
            // Arrange
            _ = new MindSphereApiSdk(creds, config);
            // Assert
        }
    }
}
