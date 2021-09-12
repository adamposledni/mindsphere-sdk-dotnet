using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Connectors
{
    /// <summary>
    /// Connector to the MindSphere API using user credentials.
    /// </summary>
    internal class UserMindSphereConnector : MindSphereConnector
    {
        private UserCredentials _credentials;

        /// <summary>
        /// Create a new instance of UserMindSphereConnector.
        /// </summary>
        public UserMindSphereConnector(UserCredentials credentials, ClientConfiguration configuration)
            : base(configuration)
        {
            _credentials = credentials;
        }

        /// <summary>
        /// Acquire MindSphere access token (no need - it is handled by MindSphere).
        /// </summary>
        protected override Task<string> AcquireTokenAsync()
        {
            return Task.FromResult(_credentials.Token);
        }

        /// <summary>
        /// Update the credentials object.
        /// </summary>
        /// <remarks>
        /// It is not possible to change the credential type in the runtime.
        /// </remarks>
        public override void UpdateCredentials(ICredentials credentials)
        {
            if (credentials is UserCredentials userCredentials)
            {
                _credentials = userCredentials;
                // reset token
                _accessToken = null;
            }
            else
            {
                throw new ArgumentException("Invalid credential type", nameof(credentials));
            }
        }
    }
}
