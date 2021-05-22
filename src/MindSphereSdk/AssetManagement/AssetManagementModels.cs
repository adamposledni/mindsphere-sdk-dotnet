using MindSphereSdk.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.AssetManagement
{
    #region Embedded

    /// <summary>
    /// Wrapper for asset list
    /// </summary>
    public class EmbeddedAssetList : IEmbeddedResource
    {
        [JsonProperty("assets")]
        public IEnumerable<Asset> Assets { get; set; }
    }

    /// <summary>
    /// Wrapper for aspect type list
    /// </summary>
    public class EmbeddedAspectTypeList : IEmbeddedResource
    {
        [JsonProperty("aspectTypes")]
        public IEnumerable<AspectType> AspectTypes { get; set; }
    }

    /// <summary>
    /// Wrapper for asset type list
    /// </summary>
    public class EmbeddedAssetTypeList : IEmbeddedResource
    {
        [JsonProperty("assetTypes")]
        public IEnumerable<AssetType> AssetTypes { get; set; }
    }

    /// <summary>
    /// Wrapper for variable list
    /// </summary>
    public class EmbeddedVariableList : IEmbeddedResource
    {
        [JsonProperty("variables")]
        public IEnumerable<VariableDetail> Variables { get; set; }
    }

    /// <summary>
    /// Wrapper for aspect list
    /// </summary>
    public class EmbeddedAspectList : IEmbeddedResource
    {
        [JsonProperty("aspects")]
        public IEnumerable<AspectFullDetail> Aspects { get; set; }
    }

    #endregion

    #region Asset

    /// <summary>
    /// Asset
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
    /// Asset to update
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
    /// Asset to add
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
    /// Asset to move
    /// </summary>
    public class AssetMove
    {
        [JsonProperty("newParentId")]
        public string NewParentId { get; set; }
    }

    #endregion

    #region Aspect

    /// <summary>
    /// Aspect
    /// </summary>
    public class Aspect
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<Variable> Variables { get; set; }
    }

    /// <summary>
    /// Aspect for putting
    /// </summary>
    public class AspectPut
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aspectTypeId")]
        public string AspectTypeId { get; set; }
    }

    /// <summary>
    /// Aspect detail
    /// </summary>
    public class AspectDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aspectId")]
        public string AspectId { get; set; }

        [JsonProperty("aspectType")]
        public AspectType AspectType { get; set; }
    }

    /// <summary>
    /// Aspect full detail
    /// </summary>
    public class AspectFullDetail
    {
        [JsonProperty("aspectTypeId")]
        public string AspectTypeId { get; set; }

        [JsonProperty("holderAssetId")]
        public string HolderAssetId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aspectId")]
        public string AspectId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<VariableDetail> Variables { get; set; }
    }

    #endregion

    #region Location

    /// <summary>
    /// Location
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

    #endregion

    #region File assignment

    /// <summary>
    /// File assignment
    /// </summary>
    public class FileAssignment
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("fileId")]
        public string FileId { get; set; }
    }

    #endregion

    #region Aspect types

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
        public IEnumerable<VariableDetail> Variables { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("etag")]
        public long Etag { get; set; }
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
        public IEnumerable<VariableDetail> Variables { get; set; }
    }

    #endregion

    #region Asset types

    /// <summary>
    /// Asset type
    /// </summary>
    public class AssetType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parentTypeId")]
        public string ParentTypeId { get; set; }

        [JsonProperty("instantiable")]
        public bool Instantiable { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("etag")]
        public long Etag { get; set; }

        [JsonProperty("aspects")]
        public List<AspectDetail> Aspects { get; set; }

        [JsonProperty("variables")]
        public List<VariableDetail> Variables { get; set; }

        [JsonProperty("fileAssignments")]
        public List<FileAssignment> FileAssignments { get; set; }
    }

    /// <summary>
    /// Asset type (create or update)
    /// </summary>
    public class AssetTypeUpdate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parentTypeId")]
        public string ParentTypeId { get; set; }

        [JsonProperty("instantiable")]
        public bool? Instantiable { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("aspects")]
        public List<AspectPut> Aspects { get; set; }

        [JsonProperty("variables")]
        public List<VariableDetail> Variables { get; set; }

        [JsonProperty("fileAssignments")]
        public List<FileAssignment> FileAssignments { get; set; }
    }

    #endregion

    #region Variables

    /// <summary>
    /// Variable for asset/aspect type
    /// </summary>
    public class VariableDetail
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
    /// Variable
    /// </summary>
    public class Variable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string ValueString { get; set; }
    }

    #endregion

    #region Request

    /// <summary>
    /// Request for listing assets
    /// </summary>
    public class ListAssetsRequest
    {
        public int? Page { get; set; }

        public int? Size { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }
    }

    /// <summary>
    /// Request for adding asset
    /// </summary>
    public class AddAssetRequest
    {
        public AssetAdd Body { get; set; }
    }

    /// <summary>
    /// Request for getting asset
    /// </summary>
    public class GetAssetRequest
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// Request for updating asset
    /// </summary>
    public class UpdateAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AssetUpdate Body { get; set; }
    }

    /// <summary>
    /// Request for deleting asset
    /// </summary>
    public class DeleteAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request for moving asset
    /// </summary>
    public class MoveAssetRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AssetMove MoveParameters { get; set; }
    }

    /// <summary>
    /// Request for saving asset's file assignment
    /// </summary>
    public class SaveAssetFileAssignmentRequest
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string IfMatch { get; set; }
        public string FileId { get; set; }
    }

    /// <summary>
    /// Request for deleting asset's file assignment
    /// </summary>
    public class DeleteAssetFileAssignmentRequest
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request for listing aspect types
    /// </summary>
    public class ListAspectTypesRequest
    {
        public int? Page { get; set; }

        public int? Size { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }
    }

    /// <summary>
    /// Request for getting aspect type
    /// </summary>
    public class GetAspectTypeRequest
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// Request for deleting aspect type
    /// </summary>
    public class DeleteAspectTypeRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request for putting aspect type
    /// </summary>
    public class PutAspectTypeRequest
    {
        public string Id { get; set; }
        public string IfNoneMatch { get; set; }
        public string IfMatch { get; set; }
        public AspectTypeUpdate Body { get; set; }
    }

    /// <summary>
    /// Request for patching aspect type
    /// </summary>
    public class PatchAspectTypeRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AspectTypeUpdate Body { get; set; }
    }

    /// <summary>
    /// Request for listing aspect types
    /// </summary>
    public class ListAssetTypesRequest
    {
        public int? Page { get; set; }

        public int? Size { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }
    }

    /// <summary>
    /// Request for getting asset type
    /// </summary>
    public class GetAssetTypeRequest
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// Request for putting asset type
    /// </summary>
    public class PutAssetTypeRequest
    {
        public string Id { get; set; }
        public string IfNoneMatch { get; set; }
        public string IfMatch { get; set; }
        public AssetTypeUpdate Body { get; set; }
    }

    /// <summary>
    /// Request for patching asset type
    /// </summary>
    public class PatchAssetTypeRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public AssetTypeUpdate Body { get; set; }
    }

    /// <summary>
    /// Request for deleting asset type
    /// </summary>
    public class DeleteAssetTypeRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request for saving asset type file assignment
    /// </summary>
    public class SaveAssetTypeFileAssignmentRequest
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string IfMatch { get; set; }
        public string FileId { get; set; }
    }

    /// <summary>
    /// Request for deleting asset type file assignment
    /// </summary>
    public class DeleteAssetTypeFileAssignmentRequest
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string IfMatch { get; set; }
    }

    /// <summary>
    /// Request for listing all asset's variables
    /// </summary>
    public class ListAssetVariablesRequest
    {
        public string Id { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string Sort { get; set; }
        public string Filter { get; set; }
    }

    /// <summary>
    /// Request for listing all asset's aspects
    /// </summary>
    public class ListAssetAspectsRequest
    {
        public string Id { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string Sort { get; set; }
        public string Filter { get; set; }
    }

    /// <summary>
    /// Request for putting asset's location
    /// </summary>
    public class PutAssetLocationRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
        public Location Body { get; set; }
    }

    /// <summary>
    /// Request for deleting asset's location
    /// </summary>
    public class DeleteAssetLocationRequest
    {
        public string Id { get; set; }
        public string IfMatch { get; set; }
    }
    #endregion
}
