using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Common
{
    internal interface IEmbeddedResource
    {

    }

    /// <summary>
    /// Wrapper for MindSphere resource
    /// </summary>
    // TODO: pagination model
    internal class MindSphereResourceWrapper<T> where T: IEmbeddedResource
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }
    }

    // TODO: docs
    public class ResourceList<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public Page Page { get; set; }
    }

    // TODO: docs
    public class Page
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }
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
