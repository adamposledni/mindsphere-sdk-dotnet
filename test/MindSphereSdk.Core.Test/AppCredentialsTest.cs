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
            // Act
            Action act = () => { new AppCredentials("  ", null, "   ", "  ", "  ", "  "); };

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
            // Arrange
            _ = new AppCredentials("a", "b", "c", "d", "e", "f");

            // Assert
        }

        /// <summary>
        /// AppCredentials values validation.
        /// </summary>
        [Fact]
        public void CreateFromFileWithInvalidData()
        {
            // Arrange
            // Act
            Action act = () => AppCredentials.FromJsonFile("mdspcreds-data-invalid.json");

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        /// <summary>
        /// AppCredentials values validation.
        /// </summary>
        [Fact]
        public void CreateFromFileWithValidData()
        {
            // Act
            // Arrange
            _ = AppCredentials.FromJsonFile("mdspcreds.json");

            // Assert
        }
    }
}
