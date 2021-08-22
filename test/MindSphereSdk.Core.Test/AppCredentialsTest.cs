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
        [Fact]
        public void UseWithInvalidData()
        {
            // Arrange
            AppCredentials appCredentials = new AppCredentials("  ", null, "   ", "  ", "  ", "  ");

            // Act
            Func<AssetManagementClient> act = () => new AssetManagementClient(appCredentials, new ClientConfiguration(), new HttpClient());

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void UseWithValidData()
        {
            // Arrange
            AppCredentials appCredentials = new AppCredentials("a", "b", "c", "d", "e", "f");

            // Act
            new AssetManagementClient(appCredentials, new ClientConfiguration(), new HttpClient());

            // Assert
        }
    }
}
