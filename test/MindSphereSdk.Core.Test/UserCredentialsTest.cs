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
        /// <summary>
        /// UserCredentials values validation.
        /// </summary>
        [Fact]
        public void UseWithInvalidData()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new UserCredentials(""));
        }

        /// <summary>
        /// UserCredentials values validation.
        /// </summary>
        [Fact]
        public void UseWithValidData()
        {
            // Arrange
            // Act
            _ = new UserCredentials("asd");

            // Assert
        }

        /// <summary>
        /// Bearer prefix removal.
        /// </summary>
        [Fact]
        public void RemoveBearerPrefix()
        {
            // Arrange
            var credentials = new UserCredentials("Bearer asd");

            // Act
            // Assert
            Assert.True(credentials.Token == "asd");
        }


    }
}
