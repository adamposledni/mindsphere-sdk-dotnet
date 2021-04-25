using MindSphereSdk.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.AssetManagement
{
    #region Assets

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

        [JsonProperty("etag")]
        public int Etag { get; set; }
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
    /// Asset object to add
    /// </summary>
    public class AssetAdd
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
    }

    /// <summary>
    /// Asset object to move
    /// </summary>
    public class AssetMove
    {
        [JsonProperty("newParentId")]
        public string NewParentId { get; set; }
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
        public AssetAdd Body { get; set; }
    }

    /// <summary>
    /// Request object for getting asset
    /// </summary>
    public class GetAssetRequest
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// Request object for updating asset
    /// </summary>
    public class UpdateAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AssetUpdate Body { get; set; }
    }

    /// <summary>
    /// Request object for deleting asset
    /// </summary>
    public class DeleteAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request object for moving asset
    /// </summary>
    public class MoveAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AssetMove MoveParameters { get; set; }
    }

    /// <summary>
    /// Request object for saving asset's file assignment
    /// </summary>
    public class SaveAssetFileAssignmentRequest
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string IfMatch { get; set; }
        public string FileId { get; set; }
    }

    /// <summary>
    /// Request object for deleting asset's file assignment
    /// </summary>
    public class DeleteAssetFileAssignmentRequest
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string IfMatch { get; set; }
    }

    #endregion

    #region Aspect types

    /// <summary>
    /// Wrapper object for aspect type list
    /// </summary>
    public class EmbeddedAspectTypeList : IEmbeddedResource
    {
        [JsonProperty("aspectTypes")]
        public IEnumerable<AspectType> AspectTypes { get; set; }
    }

    /// <summary>
    /// Aspect type
    /// </summary>
    public class AspectType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<AspectTypeVariable> Variables { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("etag")]
        public long Etag { get; set; }
    }

    /// <summary>
    /// Aspect type variable
    /// </summary>
    public class AspectTypeVariable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("length")]
        public int? Length { get; set; }

        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }

        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("searchable")]
        public bool Searchable { get; set; }

        [JsonProperty("qualityCode")]
        public bool QualityCode { get; set; }
    }

    /// <summary>
    /// Aspect type (create or update)
    /// </summary>
    public class AspectTypeUpdate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<AspectTypeVariable> Variables { get; set; }
    }


    /// <summary>
    /// Request object for listing aspect types
    /// </summary>
    public class ListAspectTypesRequest
    {
        public int? Page { get; set; }

        public int? Size { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }
    }

    /// <summary>
    /// Request object for getting aspect type
    /// </summary>
    public class GetAspectTypeRequest
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// Request object for deleting aspect type
    /// </summary>
    public class DeleteAspectTypeRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request object for putting aspect type
    /// </summary>
    public class PutAspectTypeRequest
    {
        public string Id { get; set; }
        public string IfNoneMatch { get; set; }
        public string IfMatch { get; set; }
        public AspectTypeUpdate AspectType { get; set; }
    }

    /// <summary>
    /// Request object for patching aspect type
    /// </summary>
    public class PatchAspectTypeRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AspectTypeUpdate AspectType { get; set; }
    }

    #endregion
}
