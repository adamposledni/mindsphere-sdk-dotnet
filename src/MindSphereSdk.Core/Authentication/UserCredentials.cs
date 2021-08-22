using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Authentication
{
    // TODO: user creds
    public class UserCredentials
    {
        public string Token { get; set; }
        public UserCredentials(string token)
        {
            Token = token;
        }
    }
}
