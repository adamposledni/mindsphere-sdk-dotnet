using Newtonsoft.Json;

namespace MindSphereSdk.Core.Serialization
{
    /// <summary>
    /// JSON converter.
    /// </summary>
    public static class JsonConverter
    {
        /// <summary>
        /// Serialize object.
        /// </summary>
        public static string Serialize(object obj, bool ignoreNull = false, bool utc = false)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            if (ignoreNull)
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
            }
            if (utc)
            {
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            }
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Deserialize object.
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
