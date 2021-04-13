using MindSphereSdk.Authentication;
using MindSphereSdk.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Common
{
    /// <summary>
    /// Connector to the MindSphere API using tenant credentials
    /// </summary>
    public class TenantMindSphereConnector : MindSphereConnector
    {
        private TenantCredentials _credentials;

        public TenantMindSphereConnector(TenantCredentials credentials, HttpClient httpClient)
            : base(httpClient)
        {
            _credentials = credentials;
        }

        /// <summary>
        /// Acquire MindSphere access token with tenant credentials
        /// </summary>
        public override async Task AcquireTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
