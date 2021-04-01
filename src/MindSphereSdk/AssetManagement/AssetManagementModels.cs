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
    public class EmbeddedAssetList : IEmbeddedResource
    {
        [JsonProperty("assets")]
        public IEnumerable<Asset> Assets { get; set; }
    }
    
    /// <summary>
    /// Asset object
    /// </summary>
    public class Asset
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

        [JsonProperty("fileAssignments")]
        public IEnumerable<FileAssignment> FileAssignments { get; set; }

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

        [JsonProperty("deleted")]
        public DateTime? Deleted { get; set; }
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
    /// Location object
    /// </summary>
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

    /// <summary>
    /// File assignment object
    /// </summary>
    public class FileAssignment
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("fileId")]
        public string FileId { get; set; }
    }

    /// <summary>
    /// Asset object to update
    /// </summary>
    public class AssetUpdate
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

        [JsonProperty("fileAssignments")]
        public IEnumerable<FileAssignment> FileAssignments { get; set; }
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
    }

    /// <summary>
    /// Request object for adding asset
    /// </summary>
    public class AddAssetRequest
    {
        public Asset Body { get; set; }
    }

    /// <summary>
    /// Request object for getting asset
    /// </summary>
    public class GetAssetRequest
    {
        public string Id { get; set; }
    }

    public class UpdateAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AssetUpdate Body { get; set; }
    }
}
