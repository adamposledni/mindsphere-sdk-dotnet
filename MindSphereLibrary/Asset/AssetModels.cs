using MindSphereLibrary.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereLibrary.Asset
{
    public class EmbeddedAssetResponse : IEmbeddedResponse
    {
        [JsonProperty("assets")]
        public IEnumerable<AssetResponse> Assets { get; set; }
    }
    
    public class AssetResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<Variable> Variables { get; set; }

        [JsonProperty("aspects")]
        public IEnumerable<Aspect> Aspects { get; set; }

        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        
        [JsonProperty("twinType")]
        public string TwinType { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("subTenant")]
        public string Subtenant { get; set; }

        [JsonProperty("t2Tenant")]
        public string T2tenant { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }
    }

    public class Aspect
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public IEnumerable<Variable> Variables { get; set; }
    }

    public class Variable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string ValueString { get; set; }
    }

}
