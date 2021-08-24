using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MindSphereSdk.Core.Authentication
{
    /// <summary>
    /// User credentials for MindSphere API
    /// </summary>
    public class UserCredentials : ICredentials
    {
        public string Token { get; private set; }
        public UserCredentials(string token)
        {
            Token = token.Replace("Bearer ", "");
        }
    }
}
