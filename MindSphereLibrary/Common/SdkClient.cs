using MindSphereLibrary.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereLibrary.Common
{
    public abstract class SdkClient
    {
        // https://gateway.{region}.{mindsphere-domain}/api/iottsaggregates/v3/aggregates/{assetId}/{aspectName}
        // https://gateway.eu1.mindsphere.io/api/assetmanagement/v3/assets

        private IMindSphereConnector _mindSphereConnector;

        public SdkClient(ICredentials credentials)
        {
            _mindSphereConnector = CreateConnector(credentials);
        }

        private IMindSphereConnector CreateConnector(ICredentials credentials)
        {
            if (credentials is AppCredentials)
            {
                return new BasicConnector((AppCredentials) credentials);
            }
            else
            {
                throw new InvalidOperationException("Invalid credentials");
            }
        }

        protected async Task<string> HttpActionAsync(HttpMethod method, string specUri, string body = null)
        {
            string response = await _mindSphereConnector.HttpActionAsync(method, specUri, body);
            return response;
        }
    }
}
