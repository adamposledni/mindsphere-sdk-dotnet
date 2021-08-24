using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// Connector to the MindSphere API using user credentials
    /// </summary>
    internal class UserMindSphereConnector : MindSphereConnector
    {
        private readonly UserCredentials _credentials;

        public UserMindSphereConnector(UserCredentials credentials, ClientConfiguration configuration, HttpClient httpClient)
            : base(configuration, httpClient)
        {
            _credentials = credentials;
        }

        /// <summary>
        /// Acquire MindSphere access token (no need - it is handled by MindSphere)
        /// </summary>
        protected override Task AcquireTokenAsync()
        {
            _accessToken = _credentials.Token;
            return Task.CompletedTask;
        }
    }
}
