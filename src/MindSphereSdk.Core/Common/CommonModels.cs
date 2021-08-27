using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// Wrapper for MindSphere resource
    /// </summary>
    internal class MindSphereResourceWrapper<T>
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }
    }

    /// <summary>
    /// MindSphere resource list
    /// </summary>
    public class ResourceList<T>
    {
        /// <summary>
        /// Embedded data
        /// </summary>
        public IEnumerable<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Pagination data
        /// </summary>
        public Page Page { get; set; }
    }

    /// <summary>
    /// Pagination data
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Number of elements in a page
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Total number of elements in all pages
        /// </summary>
        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Current page number
        /// </summary>
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
