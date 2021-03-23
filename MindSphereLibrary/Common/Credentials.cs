using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereLibrary.Common
{
    public class AppCredentials
    {
        [JsonIgnore]
        public string Region { get; set; } = "eu1";

        [JsonIgnore]
        public string MindSphereDomain { get; set; } = "mindsphere.eu";

        [JsonProperty("keyStoreClientId")]
        public string KeyStoreClientId { get; set; }

        [JsonProperty("keyStoreClientSecret")]
        public string KeyStoreClientSecret { get; set; }

        [JsonProperty("appName")]
        public string AppName { get; set; }

        [JsonProperty("appVersion")]
        public string AppVersion { get; set; }

        [JsonProperty("hostTenant")]
        public string HostTenant { get; set; }

        [JsonProperty("userTenant")]
        public string UserTenant { get; set; }

        public AppCredentials(
            string keyStoreClientId, 
            string keyStoreClientSecret, 
            string appName, 
            string appVersion, 
            string hostTenant, 
            string userTenant
            )
            :base()
        {
            KeyStoreClientId = keyStoreClientId;
            KeyStoreClientSecret = keyStoreClientSecret;
            AppName = appName;
            AppVersion = appVersion;
            HostTenant = hostTenant;
            UserTenant = userTenant;
        }
    }
}
