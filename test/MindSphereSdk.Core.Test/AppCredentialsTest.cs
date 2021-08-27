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
        /// Negative test: AppCredentials values validation
        /// </summary>
        [Fact]
        public void UseWithInvalidData()
        {
            // Arrange
            AppCredentials appCredentials = new AppCredentials("  ", null, "   ", "  ", "  ", "  ");

            // Act
            MindSphereApiSdk act() => new MindSphereApiSdk(appCredentials, new ClientConfiguration());

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        /// <summary>
        /// Positive test: AppCredentials values validation
        /// </summary>
        [Fact]
        public void UseWithValidData()
        {
            // Arrange
            AppCredentials appCredentials = new AppCredentials("a", "b", "c", "d", "e", "f");

            // Act
            new MindSphereApiSdk(appCredentials, new ClientConfiguration());

            // Assert
        }
    }
}
