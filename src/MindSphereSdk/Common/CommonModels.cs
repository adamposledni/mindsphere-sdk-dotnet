using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Common
{
    public class MindSphereResponseWrapper<T> where T : IEmbeddedResponse
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }

    public interface IEmbeddedResponse
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
