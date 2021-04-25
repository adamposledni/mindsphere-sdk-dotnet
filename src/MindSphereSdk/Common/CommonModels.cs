using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// Wrapper for any MindSphere resource
    /// </summary>
    public class MindSphereResourceWrapper<T> where T : IEmbeddedResource
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }

    public interface IEmbeddedResource
    {

    }
}
