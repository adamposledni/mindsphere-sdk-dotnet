using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// Wrapper for MindSphere resource
    /// </summary>
    // TODO: pagination model
    internal class MindSphereResourceWrapper<T> where T : IEmbeddedResource
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }

    internal interface IEmbeddedResource
    {
    }

    /// <summary>
    /// Access Token model
    /// </summary>
    internal class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("jti")]
        public string Jti { get; set; }

    }
}
