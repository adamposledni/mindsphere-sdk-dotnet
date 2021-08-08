using MindSphereSdk.Core.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// General SDK client
    /// </summary>
    public abstract class SdkClient
    {
        private readonly MindSphereConnector _mindSphereConnector;

        public SdkClient(ICredentials credentials, HttpClient httpClient)
        {
            _mindSphereConnector = CreateConnector(credentials, httpClient);
        }

        /// <summary>
        /// Create specified MindSphere connector based on provided credentials
        /// </summary>
        private MindSphereConnector CreateConnector(ICredentials credentials, HttpClient httpClient)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }
            else if (credentials is AppCredentials castedCredentials)
            {
                return new AppMindSphereConnector(castedCredentials, httpClient);
            }
            //else if (credentials is TenantCredentials)
            //{
            //    return new TenantMindSphereConnector((TenantCredentials)credentials, httpClient);
            //}
            else
            {
                throw new InvalidOperationException("Invalid credentials");
            }
        }

        /// <summary>
        /// Sending HTTP request to the MindSphere API
        /// </summary>
        protected async Task<string> HttpActionAsync(HttpMethod method, string specUri, HttpContent body = null, List<KeyValuePair<string, string>> headers = null)
        {
            string response = await _mindSphereConnector.HttpActionAsync(method, specUri, body, headers);
            return response;
        }
    }
}
