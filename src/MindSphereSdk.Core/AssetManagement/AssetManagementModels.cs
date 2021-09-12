using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MindSphereSdk.Core.AssetManagement
{
    #region Embedded

    /// <summary>
    /// Embedded asset list.
    /// </summary>
    internal class EmbeddedAssetList
    {
        [JsonProperty("assets")]
        public IEnumerable<Asset> Assets { get; set; }
    }

    /// <summary>
    /// Embedded aspect type list.
    /// </summary>
    internal class EmbeddedAspectTypeList
    {
        [JsonProperty("aspectTypes")]
        public IEnumerable<AspectType> AspectTypes { get; set; }
    }

    /// <summary>
    /// Embedded asset type list.
    /// </summary>
    internal class EmbeddedAssetTypeList
    {
        [JsonProperty("assetTypes")]
        public IEnumerable<AssetType> AssetTypes { get; set; }
    }

    /// <summary>
    /// Embedded variable list.
    /// </summary>
    internal class EmbeddedVariableList
    {
        [JsonProperty("variables")]
        public IEnumerable<VariableDetail> Variables { get; set; }
    }

    /// <summary>
    /// Embedded aspect list.
    /// </summary>
    internal class EmbeddedAspectList
    {
        [JsonProperty("aspects")]
        public IEnumerable<AspectFullDetail> Aspects { get; set; }
    }

    /// <summary>
    /// Embedded file list.
    /// </summary>
    internal class EmbeddedFileList
    {
        [JsonProperty("files")]
        public IEnumerable<File> Files { get; set; }
    }

    #endregion

    #region Aspect types

    /// <summary>
    /// Aspect type.
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
        public int Etag { get; set; }
    }

    /// <summary>
    /// Aspect type (create or update).
    /// </summary>
    public class AspectTypeAddUpdate
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
    /// Asset type.
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
        public int Etag { get; set; }

        [JsonProperty("aspects")]
        public List<AspectDetail> Aspects { get; set; }

        [JsonProperty("variables")]
        public List<VariableDetail> Variables { get; set; }

        [JsonProperty("fileAssignments")]
        public List<FileAssignment> FileAssignments { get; set; }
    }

    /// <summary>
    /// Asset type (create or update).
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

    #region Asset

    /// <summary>
    /// Asset.
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
        public string T2Tenant { get; set; }

        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("deleted")]
        public DateTime? Deleted { get; set; }

        [JsonProperty("etag")]
        public int Etag { get; set; }
    }

    /// <summary>
    /// Asset to update.
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
    /// Asset to add.
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
    /// Asset to move.
    /// </summary>
    public class AssetMove
    {
        [JsonProperty("newParentId")]
        public string NewParentId { get; set; }
    }

    #endregion

    #region Aspect

    /// <summary>
    /// Aspect.
    /// </summary>
    public class Aspect
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<Variable> Variables { get; set; }
    }

    /// <summary>
    /// Aspect for putting.
    /// </summary>
    public class AspectPut
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aspectTypeId")]
        public string AspectTypeId { get; set; }
    }

    /// <summary>
    /// Aspect detail.
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
    /// Aspect full detail.
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
    /// Location.
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
    /// File assignment.
    /// </summary>
    public class FileAssignment
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("fileId")]
        public string FileId { get; set; }
    }

    #endregion

    #region Variables

    /// <summary>
    /// Variable for asset/aspect type.
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
    /// Variable.
    /// </summary>
    public class Variable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string ValueString { get; set; }
    }

    /// <summary>
    /// Variable - update.
    /// </summary>
    public class VariableUpdate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("length")]
        public int? Length { get; set; }

        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }
    }

    #endregion

    #region Files

    /// <summary>
    /// File metadata.
    /// </summary>
    public class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("originalFileName")]
        public string OriginalFileName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }

        [JsonProperty("subTenant")]
        public string SubTenant { get; set; }

        [JsonProperty("uploaded")]
        public DateTime Uploaded { get; set; }

        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("isAssigned")]
        public bool IsAssigned { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("etag")]
        public int Etag { get; set; }
    }

    #endregion

    #region Asset model lock

    /// <summary>
    /// Lock state of an asset model.
    /// </summary>
    public class LockStateWithJobs
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("jobs")]
        public IEnumerable<string> Jobs { get; set; }
    }

    /// <summary>
    /// Lock state of an asset model.
    /// </summary>
    public class LockState
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }

    #endregion

    #region Request

    /// <summary>
    /// Request for listing aspect types.
    /// </summary>
    public class ListAspectTypesRequest
    {
        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for putting aspect type.
    /// </summary>
    public class PutAspectTypeRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        /// <remarks>
        /// Required for modification.
        /// </remarks>
        public string IfMatch { get; set; }

        /// <summary>
        /// Set ifNoneMatch header to "*" for ensuring create request.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Aspect type.
        /// </summary>
        public AspectTypeAddUpdate AspectType { get; set; }
    }

    /// <summary>
    /// Request for patching aspect type.
    /// </summary>
    public class PatchAspectTypeRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Aspect type.
        /// </summary>
        public AspectTypeAddUpdate AspectType { get; set; }
    }

    /// <summary>
    /// Request for getting aspect type.
    /// </summary>
    public class GetAspectTypeRequest
    {
        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for deleting aspect type.
    /// </summary>
    public class DeleteAspectTypeRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }


    /// <summary>
    /// Request for listing aspect types.
    /// </summary>
    public class ListAssetTypesRequest
    {
        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the asset type should include all of it’s inherited variables and aspects.
        /// </summary>
        public bool? Exploded { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for putting asset type.
    /// </summary>
    public class PutAssetTypeRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        /// <remarks>
        /// Required for modification
        /// </remarks>
        public string IfMatch { get; set; }

        /// <summary>
        /// Set ifNoneMatch header to "*" for ensuring create request.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the asset type should include all of it’s inherited variables and aspects.
        /// </summary>
        public bool? Exploded { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Asset type.
        /// </summary>
        public AssetTypeUpdate AssetType { get; set; }
    }

    /// <summary>
    /// Request for patching asset type.
    /// </summary>
    public class PatchAssetTypeRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the asset type should include all of it’s inherited variables and aspects.
        /// </summary>
        public bool? Exploded { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Asset type.
        /// </summary>
        public AssetTypeUpdate AssetType { get; set; }
    }

    /// <summary>
    /// Request for getting asset type.
    /// </summary>
    public class GetAssetTypeRequest
    {
        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the asset type should include all of it’s inherited variables and aspects.
        /// </summary>
        public bool? Exploded { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for deleting asset type.
    /// </summary>
    public class DeleteAssetTypeRequest
    {
        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for saving asset type file assignment.
    /// </summary>
    public class AddAssetTypeFileAssignmentRequest
    {
        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Keyword for the file to be assigned to an asset or asset type.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// File identifier.
        /// </summary>
        public string FileId { get; set; }
    }

    /// <summary>
    /// Request for deleting asset type file assignment.
    /// </summary>
    public class DeleteAssetTypeFileAssignmentRequest
    {
        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Keyword for the file to be assigned to an asset or asset type.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for updating asset type variables.
    /// </summary>
    public class PatchAssetTypeVariablesRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// The type’s id is a unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Variables map.
        /// </summary>
        public object VariableMap { get; set; }
    }


    /// <summary>
    /// Request for listing assets.
    /// </summary>
    public class ListAssetsRequest
    {
        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Specifies if the assets should include all of it’s inherited variables and aspects from assettype and aspecttype.
        /// </summary>
        public bool? BasicFieldsOnly { get; set; }
    }

    /// <summary>
    /// Request for adding asset.
    /// </summary>
    public class AddAssetRequest
    {
        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Asset.
        /// </summary>
        public AssetAdd Asset { get; set; }
    }

    /// <summary>
    /// Request for getting asset.
    /// </summary>
    public class GetAssetRequest
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for updating asset.
    /// </summary>
    public class UpdateAssetRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Asset.
        /// </summary>
        public AssetUpdate Asset { get; set; }
    }

    /// <summary>
    /// Request for deleting asset.
    /// </summary>
    public class DeleteAssetRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for moving asset.
    /// </summary>
    public class MoveAssetRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// New parent asset identifier.
        /// </summary>
        public string NewParentId { get; set; }
    }

    /// <summary>
    /// Request for saving asset's file assignment.
    /// </summary>
    public class SaveAssetFileAssignmentRequest
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Keyword for the file to be assigned to an asset or asset type.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// File identifier.
        /// </summary>
        public string FileId { get; set; }
    }

    /// <summary>
    /// Request for deleting asset's file assignment.
    /// </summary>
    public class DeleteAssetFileAssignmentRequest
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Keyword for the file to be assigned to an asset or asset type.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for getting root asset.
    /// </summary>
    public class GetRootAssetRequest
    {
        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }
    }


    /// <summary>
    /// Request for listing all asset's variables.
    /// </summary>
    public class ListAssetVariablesRequest
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for listing all asset's aspects.
    /// </summary>
    public class ListAssetAspectsRequest
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }


    /// <summary>
    /// Request for putting asset's location.
    /// </summary>
    public class PutAssetLocationRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// Location.
        /// </summary>
        public Location Location { get; set; }
    }

    /// <summary>
    /// Request for deleting asset's location.
    /// </summary>
    public class DeleteAssetLocationRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }


    /// <summary>
    /// Request for uploading file.
    /// </summary>
    public class UploadFileRequest
    {
        /// <summary>
        /// The file to upload.
        /// </summary>
        public FileStream File { get; set; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The scope of the file.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// The description of the file.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Request for listing files.
    /// </summary>
    public class ListFilesRequest
    {
        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }
    }

    /// <summary>
    /// Request for downloading file.
    /// </summary>
    public class DownloadFileRequest
    {
        /// <summary>
        /// Unique identifier of the file.
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// Request for getting file metadata.
    /// </summary>
    public class GetFileRequest
    {
        /// <summary>
        /// Unique identifier of the file.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }
    }

    /// <summary>
    /// Request for updating file.
    /// </summary>
    public class UpdateFileRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier of the file.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The file to upload.
        /// </summary>
        public FileStream File { get; set; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The scope of the file.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// The description of the file.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Request for deleting file.
    /// </summary>
    public class DeleteFileRequest
    {
        /// <summary>
        /// Last known version to facilitate optimistic locking.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Unique identifier of the file.
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// Request for putting lock state.
    /// </summary>
    public class PutLockStateRequest
    {
        /// <summary>
        /// Lock state of an asset model.
        /// </summary>
        public bool? Enabled { get; set; }
    }

    #endregion
}
