using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MindSphereSdk.Core.Authentication
{
    /// <summary>
    /// User credentials for the MindSphere API
    /// </summary>
    public class UserCredentials : ICredentials
    {
        /// <summary>
        /// Access token
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Create a new instance of UserCredentials
        /// </summary>
        public UserCredentials(string token)
        {
            Token = token.Replace("Bearer ", "");
        }
    }
}
