using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// AppCredentials for MindSphere API
    /// </summary>
    public class AppCredentials : ICredentials
    {
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

        // TODO: Validate data
        public AppCredentials(
            string keyStoreClientId, 
            string keyStoreClientSecret, 
            string appName, 
            string appVersion, 
            string hostTenant, 
            string userTenant
            )
        {
            KeyStoreClientId = keyStoreClientId;
            KeyStoreClientSecret = keyStoreClientSecret;
            AppName = appName;
            AppVersion = appVersion;
            HostTenant = hostTenant;
            UserTenant = userTenant;
        }

        /// <summary>
        /// Load AppCredentials from the JSON file
        /// </summary>
        public static AppCredentials FromJsonFile(string path)
        {
            string jsonString = File.ReadAllText(path);
            AppCredentials appCredentials = JsonConvert.DeserializeObject<AppCredentials>(jsonString);
            return appCredentials;
        }
    }
}
