using MindSphereSdk.Core.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace MindSphereSdk.Core.Authentication
{
    /// <summary>
    /// Application credentials for MindSphere API.
    /// </summary>
    public class AppCredentials : ICredentials
    {
        /// <summary>
        /// Client ID.
        /// </summary>
        [JsonProperty("keyStoreClientId")]
        public string KeyStoreClientId { get; private set; }

        /// <summary>
        /// Client secret.
        /// </summary>
        [JsonProperty("keyStoreClientSecret")]
        public string KeyStoreClientSecret { get; private set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        [JsonProperty("appName")]
        public string AppName { get; private set; }

        /// <summary>
        /// Version of the application.
        /// </summary>
        [JsonProperty("appVersion")]
        public string AppVersion { get; private set; }

        /// <summary>
        /// Host tenant.
        /// </summary>
        [JsonProperty("hostTenant")]
        public string HostTenant { get; private set; }

        /// <summary>
        /// User tenant.
        /// </summary>
        [JsonProperty("userTenant")]
        public string UserTenant { get; private set; }

        /// <summary>
        /// Create a new instance of the AppCredentials.
        /// </summary>
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
        /// Load application credentials from the JSON file.
        /// </summary>
        public static AppCredentials FromJsonFile(string path)
        {
            string jsonString = File.ReadAllText(path);
            AppCredentials appCredentials = JsonConvert.DeserializeObject<AppCredentials>(jsonString);
            return appCredentials;
        }
    }
}
