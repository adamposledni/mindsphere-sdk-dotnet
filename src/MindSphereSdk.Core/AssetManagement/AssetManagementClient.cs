using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.AssetManagement
{
    /// <summary>
    /// Configuring, reading and managing assets, asset types and aspect types
    /// </summary>
    public class AssetManagementClient : SdkClient
    {
        private readonly string _baseUri = "/api/assetmanagement/v3";

        internal AssetManagementClient(MindSphereConnector mindSphereConnector)
            : base(mindSphereConnector)
        {
        }

        #region Aspect type

        /// <summary>
        /// List all aspect types
        /// </summary>
        public async Task<ResourceList<AspectType>> ListAspectTypesAsync(ListAspectTypesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/aspecttypes" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // makte request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var aspectTypeListWrapper = JsonConvert.DeserializeObject<MindSphereApiResource<EmbeddedAspectTypeList>>(response);

            // format output
            var output = new ResourceList<AspectType>
            {
                Data = aspectTypeListWrapper.Embedded.AspectTypes,
                Page = aspectTypeListWrapper.Page
            };
            return output;
        }

        /// <summary>
        /// Create or update an aspect type
        /// </summary>
        public async Task<AspectType> PutAspectTypeAsync(PutAspectTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/aspecttypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch),
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.AspectType);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");


            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Patch an aspect type
        /// </summary>
        public async Task<AspectType> PatchAspectTypeAsync(PatchAspectTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/aspecttypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.AspectType);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // make request
            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Read an aspect type 
        /// </summary>
        public async Task<AspectType> GetAspectTypeAsync(GetAspectTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/aspecttypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Delete an aspect type 
        /// </summary>
        public async Task DeleteAspectTypeAsync(DeleteAspectTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/aspecttypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        #endregion

        #region Asset type

        /// <summary>
        /// List all asset types
        /// </summary>
        public async Task<ResourceList<AssetType>> ListAssetTypesAsync(ListAssetTypesRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("exploded", request.Exploded);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var assetTypeListWrapper = JsonConvert.DeserializeObject<MindSphereApiResource<EmbeddedAssetTypeList>>(response);

            // format output
            var output = new ResourceList<AssetType>
            {
                Data = assetTypeListWrapper.Embedded.AssetTypes,
                Page = assetTypeListWrapper.Page
            };
            return output;
        }

        /// <summary>
        /// Create or update an asset type
        /// </summary>
        public async Task<AssetType> PutAssetTypeAsync(PutAssetTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("exploded", request.Exploded);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch),
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.AssetType, seriSettings);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Patch an asset type
        /// </summary>
        public async Task<AssetType> PatchAssetTypeAsync(PatchAssetTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("exploded", request.Exploded);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.AssetType, seriSettings);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // make request
            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Read an asset type 
        /// </summary>
        public async Task<AssetType> GetAssetTypeAsync(GetAssetTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("exploded", request.Exploded);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Delete an asset type 
        /// </summary>
        public async Task DeleteAssetTypeAsync(DeleteAssetTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Save a file assignment to an asset
        /// </summary>
        public async Task<AssetType> AddAssetTypeFileAssignmentAsync(AddAssetTypeFileAssignmentRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + "/fileAssignments/" + request.Key + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            object fileIdObject = new { fileId = request.FileId };
            StringContent body = new StringContent(JsonConvert.SerializeObject(fileIdObject), Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Delete a file assignment from an asset type
        /// </summary>
        public async Task<AssetType> DeleteAssetTypeFileAssignmentAsync(DeleteAssetTypeFileAssignmentRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + "/fileAssignments/" + request.Key + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Update variables from an asset type
        /// </summary>
        public async Task PatchAssetTypeVariablesAsync(PatchAssetTypeVariablesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assettypes/" + request.Id + "/variables" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.VariableMap, seriSettings);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // make request
            await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
        }

        #endregion

        #region Asset

        /// <summary>
        /// List all available assets
        /// </summary>
        public async Task<ResourceList<Asset>> ListAssetsAsync(ListAssetsRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            queryBuilder.AddQuery("basicFieldsOnly", request.BasicFieldsOnly);
            string uri = _baseUri + "/assets" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var assetListWrapper = JsonConvert.DeserializeObject<MindSphereApiResource<EmbeddedAssetList>>(response);

            // format output
            var output = new ResourceList<Asset>
            {
                Data = assetListWrapper.Embedded.Assets,
                Page = assetListWrapper.Page
            };
            return output;
        }

        /// <summary>
        /// Create an asset
        /// </summary>
        public async Task<Asset> AddAssetAsync(AddAssetRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets" + queryBuilder.ToString();

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(request.Asset), Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Read a single asset 
        /// </summary>
        public async Task<Asset> GetAssetAsync(GetAssetRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Update an asset
        /// </summary>
        public async Task<Asset> PutAssetAsync(UpdateAssetRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.Asset, seriSettings);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Patch an asset
        /// </summary>
        public async Task<Asset> PatchAssetAsync(UpdateAssetRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.Asset, seriSettings);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // make request
            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete an asset 
        /// </summary>
        public async Task DeleteAssetAsync(DeleteAssetRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Move an asset 
        /// </summary>
        public async Task<Asset> MoveAssetAsync(MoveAssetRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/move" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(new { newParentId = request.NewParentId }), Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Post, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Save a file assignment to an asset
        /// </summary>
        public async Task<Asset> SaveAssetFileAssignmentAsync(SaveAssetFileAssignmentRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/fileAssignments/" + request.Key + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            object fileIdObject = new { fileId = request.FileId };
            StringContent body = new StringContent(JsonConvert.SerializeObject(fileIdObject), Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete a file assignment from an asset
        /// </summary>
        public async Task<Asset> DeleteAssetFileAssignmentAsync(DeleteAssetFileAssignmentRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/fileAssignments/" + request.Key + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Read the root asset of the user 
        /// </summary>
        public async Task<Asset> GetRootAssetAsync(GetRootAssetRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/assets/root";

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        #endregion

        #region Structure

        /// <summary>
        /// Get all variables of an asset 
        /// </summary>
        public async Task<ResourceList<VariableDetail>> ListAssetVariablesAsync(ListAssetVariablesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/variables" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var variableListWrapper = JsonConvert.DeserializeObject<MindSphereApiResource<EmbeddedVariableList>>(response);

            // format output
            var output = new ResourceList<VariableDetail>
            {
                Data = variableListWrapper.Embedded.Variables,
                Page = variableListWrapper.Page
            };
            return output;
        }

        /// <summary>
        /// Get all aspects of an asset 
        /// </summary>
        public async Task<ResourceList<AspectFullDetail>> ListAssetAspectsAsync(ListAssetAspectsRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/aspects" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var aspectListWrapper = JsonConvert.DeserializeObject<MindSphereApiResource<EmbeddedAspectList>>(response);

            // format output
            var output = new ResourceList<AspectFullDetail>
            {
                Data = aspectListWrapper.Embedded.Aspects,
                Page = aspectListWrapper.Page
            };
            return output;
        }

        #endregion

        #region Location

        /// <summary>
        /// Create or Update location assigned to given asset
        /// </summary>
        public async Task<Asset> PutAssetLocationAsync(PutAssetLocationRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/location" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            var seriSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(request.Location, seriSettings);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete location assigned to given asset
        /// </summary>
        public async Task<Asset> DeleteAssetLocationAsync(DeleteAssetLocationRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/assets/" + request.Id + "/location" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        #endregion

        #region File

        /// <summary>
        /// Upload file to be used in Asset Management
        /// </summary>
        public async Task<File> UploadFileAsync(UploadFileRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/files/";

            // prepare HTTP request body
            MultipartFormDataContent body = new MultipartFormDataContent
            {
                { new StreamContent(request.File), "file", request.File.Name }
            };
            body.AddStringContentIfNotNull(request.Name, "name");
            body.AddStringContentIfNotNull(request.Scope, "scope");
            body.AddStringContentIfNotNull(request.Description, "description");

            // make request
            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var file = JsonConvert.DeserializeObject<File>(response);

            return file;
        }

        /// <summary>
        /// Get metadata of uploaded files.
        /// </summary>
        public async Task<ResourceList<File>> ListFilesAsync(ListFilesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);
            string uri = _baseUri + "/files/" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var fileListWrapper = JsonConvert.DeserializeObject<MindSphereApiResource<EmbeddedFileList>>(response);

            // format output
            var output = new ResourceList<File>();
            if (fileListWrapper.Embedded != null && fileListWrapper.Embedded.Files != null)
            {
                output.Data = fileListWrapper.Embedded.Files;
                output.Page = fileListWrapper.Page;
            }
            return output;
        }

        /// <summary>
        /// Returns a file by its id
        /// </summary>
        public async Task<string> DownloadFileAsync(DownloadFileRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/files/" + request.Id + "/file";

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri);
            return response;
        }

        /// <summary>
        /// Returns a file's metadata by its id
        /// </summary>
        public async Task<File> GetFileAsync(GetFileRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/files/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var file = JsonConvert.DeserializeObject<File>(response);

            return file;
        }

        /// <summary>
        /// Update a file
        /// </summary>
        public async Task<File> UpdateFileAsync(UpdateFileRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/events/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            MultipartFormDataContent body = new MultipartFormDataContent
            {
                { new StreamContent(request.File), "file", request.File.Name }
            };
            body.AddStringContentIfNotNull(request.Name, "name");
            body.AddStringContentIfNotNull(request.Scope, "scope");
            body.AddStringContentIfNotNull(request.Description, "description");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var file = JsonConvert.DeserializeObject<File>(response);

            return file;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        public async Task DeleteFileAsync(DeleteFileRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/files/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        #endregion

        #region Assetmodellock

        /// <summary>
        /// Returns lock state of an asset model
        /// </summary>
        public async Task<LockStateWithJobs> GetLockStateAsync()
        {
            // prepare URI string
            string uri = _baseUri + "/model/lock";

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var lockState = JsonConvert.DeserializeObject<LockStateWithJobs>(response);

            return lockState;
        }

        /// <summary>
        /// Enable/disable lock state of an asset model
        /// </summary>
        public async Task<LockState> PutLockStateAsync(PutLockStateRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("enabled", request.Enabled);
            string uri = _baseUri + "/model/lock" + queryBuilder.ToString();

            // prepare HTTP request body
            // endpoint needs content type header
            StringContent body = new StringContent("", Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body);
            var lockState = JsonConvert.DeserializeObject<LockState>(response);

            return lockState;
        }


        #endregion
    }
}
