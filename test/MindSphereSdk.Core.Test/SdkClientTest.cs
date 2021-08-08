using MindSphereSdk.Core.AssetManagement;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace MindSphereSdk.Core.Test
{
    public class SdkClientTest
    {
        [Fact]
        public void ConstructWithNullCredentials()
        {
            // Act
            Func<AssetManagementClient> act = () => new AssetManagementClient(null, new HttpClient());

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
