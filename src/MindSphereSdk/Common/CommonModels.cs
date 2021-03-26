using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Common
{
    public class MindSphereResourceWrapper<T> where T : IEmbeddedResource
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }

    public interface IEmbeddedResource
    {

    }

    public class Location
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
        
        [JsonProperty("locality")]
        public string Locality { get; set; }
        
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }
                
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
    }
}
