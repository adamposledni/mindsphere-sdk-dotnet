using MindSphereSdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.AssetManagement
{
    /// <summary>
    /// Wrapper object for asset list
    /// </summary>
    public class EmbeddedAssetListResource : IEmbeddedResource
    {
        [JsonProperty("assets")]
        public IEnumerable<AssetResource> Assets { get; set; }
    }
    
    /// <summary>
    /// Asset resource object
    /// </summary>
    public class AssetResource
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

        // TODO: fileAssignments

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

        // TODO: deleted
    }

    /// <summary>
    /// Aspect object
    /// </summary>
    public class Aspect
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public IEnumerable<Variable> Variables { get; set; }
    }

    /// <summary>
    /// Variable object
    /// </summary>
    public class Variable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string ValueString { get; set; }
    }

    /// <summary>
    /// Request object for listing assets
    /// </summary>
    public class ListAssetsRequest
    {
        public int? Page { get; set; }

        public int? Size { get; set; }
        
        public string Sort { get; set; }

        public string Filter { get; set; }

        //public string IfNoneMatch { get; set; }

        //public bool? IncludeShared { get; set; }

        //public bool? BasicFieldsOnly { get; set; }
    }

    /// <summary>
    /// Request object for adding asset
    /// </summary>
    public class AddAssetRequest
    {
        public AssetResource Body { get; set; }
    }
}
