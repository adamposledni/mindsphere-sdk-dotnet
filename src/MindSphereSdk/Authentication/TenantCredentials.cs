using MindSphereSdk.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Authentication
{
    /// <summary>
    /// AppCredentials for MindSphere API
    /// </summary>
    public class TenantCredentials : ICredentials
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        [JsonProperty("subTenant")]
        public string SubTenant { get; set; }

        public TenantCredentials(
            string clientId,
            string clientSecret,
            string tenant,
            string subTenant = null)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Tenant = tenant;
            SubTenant = subTenant;
        }
    }
}
