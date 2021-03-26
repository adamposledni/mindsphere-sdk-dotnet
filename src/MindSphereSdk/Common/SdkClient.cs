using MindSphereSdk.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Common
{
    public abstract class SdkClient
    {
        // https://gateway.{region}.{mindsphere-domain}/api/iottsaggregates/v3/aggregates/{assetId}/{aspectName}

        private IMindSphereConnector _mindSphereConnector;

        public SdkClient(ICredentials credentials, HttpClient httpClient)
        {
            _mindSphereConnector = CreateConnector(credentials, httpClient);
        }

        private IMindSphereConnector CreateConnector(ICredentials credentials, HttpClient httpClient)
        {
            if (credentials is AppCredentials)
            {
                return new BasicConnector((AppCredentials) credentials, httpClient);
            }
            else
            {
                throw new InvalidOperationException("Invalid credentials");
            }
        }

        protected async Task<string> HttpActionAsync(HttpMethod method, string specUri, HttpContent body = null)
        {
            string response = await _mindSphereConnector.HttpActionAsync(method, specUri, body);
            return response;
        }
    }
}
