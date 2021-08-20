using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MindSphereSdk.Core.Helpers;

namespace MindSphereSdk.Core.AssetManagement
{
    /// <summary>
    /// Configuring, reading and managing assets, asset types and aspect types
    /// </summary>
    public class AssetManagementClient : SdkClient
    {
        private readonly string _baseUri = "/api/assetmanagement/v3";

        public AssetManagementClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {

        }

        #region Asset

        /// <summary>
        /// List all available assets
        /// </summary>
        public async Task<IEnumerable<Asset>> ListAssetsAsync(ListAssetsRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);

            string uri = _baseUri + "/assets" + queryBuilder.ToString();

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAssetList>>(response);
            var assetList = assetListWrapper.Embedded.Assets;

            return assetList;
        }

        /// <summary>
        /// Create an asset
        /// </summary>
        public async Task<Asset> AddAssetAsync(AddAssetRequest request)
        {
            string uri = _baseUri + "/assets";

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Read a single asset 
        /// </summary>
        public async Task<Asset> GetAssetAsync(GetAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Update an asset
        /// </summary>
        public async Task<Asset> UpdateAssetAsync(UpdateAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body, 
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);
            
            return asset;
        }

        /// <summary>
        /// Patch an asset
        /// </summary>
        public async Task<Asset> PatchAssetAsync(UpdateAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete an asset 
        /// </summary>
        public async Task DeleteAssetAsync(DeleteAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Move an asset 
        /// </summary>
        public async Task<Asset> MoveAssetAsync(MoveAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/move";

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(new { newParentId = request.NewParentId }), Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Post, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);
            return asset;
        }

        /// <summary>
        /// Read the root asset of the user 
        /// </summary>
        public async Task<Asset> GetRootAssetAsync()
        {
            string uri = _baseUri + "/assets/root";

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Save a file assignment to an asset
        /// </summary>
        public async Task<Asset> SaveAssetFileAssignmentAsync(SaveAssetFileAssignmentRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/fileAssignments/" + request.Key;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            object fileIdObject = new { fileId = request.FileId };
            StringContent body = new StringContent(JsonConvert.SerializeObject(fileIdObject), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete a file assignment from an asset
        /// </summary>
        public async Task<Asset> DeleteAssetFileAssignmentAsync(DeleteAssetFileAssignmentRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/fileAssignments/" + request.Key;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        #endregion

        #region Structure

        /// <summary>
        /// Get all variables of an asset 
        /// </summary>
        public async Task<IEnumerable<VariableDetail>> ListAssetVariablesAsync(ListAssetVariablesRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);

            string uri = _baseUri + "/assets/" + request.Id + "/variables" + queryBuilder.ToString();

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var variableListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedVariableList>>(response);
            var variableList = variableListWrapper.Embedded.Variables;

            return variableList;
        }

        /// <summary>
        /// Get all aspects of an asset 
        /// </summary>
        public async Task<IEnumerable<AspectFullDetail>> ListAssetAspectsAsync(ListAssetAspectsRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);

            string uri = _baseUri + "/assets/" + request.Id + "/aspects" + queryBuilder.ToString();

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var aspectListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAspectList>>(response);
            var aspectList = aspectListWrapper.Embedded.Aspects;

            return aspectList;
        }

        #endregion

        #region Location

        /// <summary>
        /// Create or Update location assigned to given asset
        /// </summary>
        public async Task<Asset> PutAssetLocationAsync(PutAssetLocationRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/location";

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete location assigned to given asset
        /// </summary>
        public async Task<Asset> DeleteAssetLocationAsync(DeleteAssetLocationRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/location";

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        #endregion

        #region Files

        /// <summary>
        /// Get metadata of uploaded files.
        /// </summary>
        public async Task<IEnumerable<File>> ListFilesAsync(ListFilesRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);

            string uri = _baseUri + "/files/" + queryBuilder.ToString();

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var fileListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedFileList>>(response);

            if (fileListWrapper.Embedded != null && fileListWrapper.Embedded.Files != null)
            {
                return fileListWrapper.Embedded.Files;
            }

            return new List<File>();
        }

        /// <summary>
        /// Returns a file's metadata by its id
        /// </summary>
        public async Task<File> GetFileAsync(GetFileRequest request)
        {
            string uri = _baseUri + "/files/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var file = JsonConvert.DeserializeObject<File>(response);

            return file;
        }

        /// <summary>
        /// Returns a file by its id
        /// </summary>
        public async Task<string> DownloadFileAsync(DownloadFileRequest request)
        {
            string uri = _baseUri + "/files/" + request.Id + "/file";

            string response = await HttpActionAsync(HttpMethod.Get, uri);

            return response;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        public async Task DeleteFileAsync(DeleteFileRequest request)
        {
            string uri = _baseUri + "/files/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Upload file to be used in Asset Management
        /// </summary>
        public async Task<File> UploadFileAsync(UploadFileRequest request)
        {
            string uri = _baseUri + "/files/";

            // prepare HTTP request body
            MultipartFormDataContent body = new MultipartFormDataContent
            {
                { new StreamContent(request.File), "file", request.File.Name }
            };
            body.AddStringContentIfNotNull(request.Name, "name");
            body.AddStringContentIfNotNull(request.Scope, "scope");
            body.AddStringContentIfNotNull(request.Description, "description");

            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var file = JsonConvert.DeserializeObject<File>(response);

            return file;
        }

        /// <summary>
        /// Update a file
        /// </summary>
        public async Task<File> UpdateFileAsync(UpdateFileRequest request)
        {
            string uri = _baseUri + "/files/" + request.Id;

            // prepare HTTP request body
            MultipartFormDataContent body = new MultipartFormDataContent
            {
                { new StreamContent(request.File), "file", request.File.Name }
            };
            body.AddStringContentIfNotNull(request.Name, "name");
            body.AddStringContentIfNotNull(request.Scope, "scope");
            body.AddStringContentIfNotNull(request.Description, "description");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var file = JsonConvert.DeserializeObject<File>(response);

            return file;
        }

        #endregion

        #region Aspect type

        /// <summary>
        /// List all aspect types
        /// </summary>
        public async Task<IEnumerable<AspectType>> ListAspectTypesAsync(ListAspectTypesRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);

            string uri = _baseUri + "/aspecttypes" + queryBuilder.ToString();

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var aspectTypeListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAspectTypeList>>(response);
            var aspectTypeList = aspectTypeListWrapper.Embedded.AspectTypes;

            return aspectTypeList;
        }

        /// <summary>
        /// Read an aspect type 
        /// </summary>
        public async Task<AspectType> GetAspectTypeAsync(GetAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Delete an aspect type 
        /// </summary>
        public async Task DeleteAspectTypeAsync(DeleteAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Create or update an aspect type
        /// </summary>
        public async Task<AspectType> PutAspectTypeAsync(PutAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch),
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Patch an aspect type
        /// </summary>
        public async Task<AspectType> PatchAspectTypeAsync(PatchAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        #endregion

        #region Asset type

        /// <summary>
        /// List all asset types
        /// </summary>
        public async Task<IEnumerable<AssetType>> ListAssetTypesAsync(ListAssetTypesRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("filter", request.Filter);

            string uri = _baseUri + "/assettypes" + queryBuilder.ToString();

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetTypeListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAssetTypeList>>(response);
            var assetTypeList = assetTypeListWrapper.Embedded.AssetTypes;

            return assetTypeList;
        }

        /// <summary>
        /// Read an asset type 
        /// </summary>
        public async Task<AssetType> GetAssetTypeAsync(GetAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Create or update an asset type
        /// </summary>
        public async Task<AssetType> PutAssetTypeAsync(PutAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch),
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Patch an asset type
        /// </summary>
        public async Task<AssetType> PatchAssetTypeAsync(PatchAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Delete an asset type 
        /// </summary>
        public async Task DeleteAssetTypeAsync(DeleteAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Save a file assignment to an asset
        /// </summary>
        public async Task<AssetType> SaveAssetTypeFileAssignmentAsync(SaveAssetTypeFileAssignmentRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id + "/fileAssignments/" + request.Key;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            object fileIdObject = new { fileId = request.FileId };
            StringContent body = new StringContent(JsonConvert.SerializeObject(fileIdObject), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Delete a file assignment from an asset type
        /// </summary>
        public async Task<AssetType> DeleteAssetTypeFileAssignmentAsync(DeleteAssetTypeFileAssignmentRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id + "/fileAssignments/" + request.Key;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        #endregion
    }
}
