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
            // Act
            Action act = () => new ClientConfiguration(timeout: 0);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        /// <summary>
        /// ClientConfiguration values validation.
        /// </summary>
        [Fact]
        public void CreateWithValidData()
        {
            // Act
            // Arrange
            _ = new ClientConfiguration("hello", "something", 1);

            // Assert
        }
    }
}
